using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ChecklistService : IChecklistService
    {
        private readonly AtilChecklistApiHttpClient _atilChecklistApiHttpClient;
        private readonly DibkChecklistApiHttpClient _dibkChecklistApiHttpClient;
        private readonly IConfiguration _configuration;
        private List<FormProperties> _forms;

//        private bool TolerateErrors(string dataFormatVersion) { return GetFormProperties(dataFormatVersion).ServiceAuthority == "DIBK"; }
        private bool TolerateErrors(string dataFormatVersion) { return true; }

        public ChecklistService(AtilChecklistApiHttpClient atilChecklistApiHttpClient, DibkChecklistApiHttpClient dibkChecklistApiHttpClient, IConfiguration configuration)
        {
            _atilChecklistApiHttpClient = atilChecklistApiHttpClient;
            _dibkChecklistApiHttpClient = dibkChecklistApiHttpClient;
            _configuration = configuration;

            var formPropertiesFromConfig = _configuration.GetSection("FormProperties").GetChildren().ToList()
                .Select(x =>
                                 (
                                      x.GetValue<string>("DataFormatVersion"),
                                      x.GetValue<string>("ServiceAuthority"),
                                      x.GetValue<string>("ProcessCategory")
                                  )
                            ).ToList<(string DataFormatVersion, string ServiceAuthority, string ProcessCategory)>();
            
            _forms = formPropertiesFromConfig.Select(x => new FormProperties() 
            { 
                DataFormatVersion = x.DataFormatVersion, 
                ProcessCategory = x.ProcessCategory,  
                ServiceAuthority = x.ServiceAuthority 
            }).ToList();

        }
        private IEnumerable<ChecklistAnswer> GetPrefillChecklistAnswer(string dataFormatVersion, PrefillChecklistInput prefillChecklistInput)
        {
            var formProperties = GetFormProperties(dataFormatVersion);
            var httpClient = GetChecklistApiHttpClient(dataFormatVersion);
            var prefilledChecklist = (List<ChecklistAnswer>)httpClient.GetPrefillChecklistAnswer(prefillChecklistInput).Result;

            if (formProperties.ServiceAuthority.Equals(ServiceOwnerEnum.ATIL))
            {
                var AtilSpecificChecklist = AtilSpecificPrefilledChecklist(prefillChecklistInput);
                prefilledChecklist.AddRange(AtilSpecificChecklist);
            }
            
            return prefilledChecklist;
        }

        public IEnumerable<ValidationMessage> FilterValidationResult(string dataFormatVersion, IEnumerable<ValidationMessage> validationMessages, IEnumerable<string> tiltakstyper)
        {
            var formProperties = GetFormProperties(dataFormatVersion);
            var httpClient = GetChecklistApiHttpClient(dataFormatVersion);
            var checklistRelatedValidations = (List<ChecklistValidationRelations>)httpClient.GetChecklistValidationRelations(formProperties.ProcessCategory).Result;

            List<ValidationMessage> filteredMessages = new List<ValidationMessage>();
            foreach (var message in validationMessages)
            {
                var messageWODataFormatVersion = message.Reference.Substring(6);
                if (!checklistRelatedValidations.Any(x => x.SupportingDataValidationRuleId.Any(y => y.ValidationRuleIds.Any(z => z.Equals(messageWODataFormatVersion)))))
                {
                    //ValidationID does not exist in sjekklist
                    filteredMessages.Add(message);
                }
                else
                {
                    //Condition: ValidationID DOES exist in sjekklist, in one or several checklist points.
                    //Checking if enterprise terms (tiltakstyper) from application is present in at least one of the checklist points
                    var checklistPointsMatching = checklistRelatedValidations.Any(x => x.EnterpriseTerms.Any(t2 => tiltakstyper.Any(t1 => t2.Contains(t1))));

                    if (checklistPointsMatching)
                    {
                        filteredMessages.Add(message);
                    }
                }
            }

            return filteredMessages;
        }

        public ValidationResult GetValidationReport(ValidationResult validationResult, string dataFormatVersion)
        {
            var validationResultContainsErrors = validationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.ERROR));
            if (ValidationResultIsAcceptableForFurtherProcessing(validationResultContainsErrors, dataFormatVersion))
            {
                PrefillChecklistInput prefillChecklistInput = new();
                var errors = validationResult.messages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.ERROR).Select(y => y.Reference).ToList();
                var warnings = validationResult.messages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.WARNING).Select(y => y.Reference).ToList();
                prefillChecklistInput.ExecutedValidations = validationResult.ValidationRules.Select(y => y.Id).Distinct();

                //Errors and warnings must be supplemented with all their children
                prefillChecklistInput.Errors = GetRuleChildren(errors, validationResult);
                prefillChecklistInput.Warnings = GetRuleChildren(warnings, validationResult);

                var prefillChecklist = GetPrefillChecklistAnswer(dataFormatVersion, prefillChecklistInput);
                validationResult.PrefillChecklist = new PrefillChecklist() { ChecklistAnswers = prefillChecklist };

                return validationResult;
            }
            else
            {
                //Abort sending due to not form data is not complete
                return null;
            }
        }

        private List<string> GetRuleChildren(List<string> inputMessages, ValidationResult validationResult)
        {
            List<string> messagesWithChildren = new();
            foreach (var message in inputMessages)
            {
                //messagesWithChildren.Add(message);
                if (message.EndsWith(".1"))  //utfylt
                {
                    var prefix = message.Substring(0, message.Length - 1);
                    var childrenList = validationResult.ValidationRules.Where(x => x.Id.Contains(prefix)).Select(y => y.Id);
                    messagesWithChildren.AddRange(childrenList);
                }
            }

            return messagesWithChildren;
        }

        private bool ValidationResultIsAcceptableForFurtherProcessing(bool validationResultContainsErrors, string dataFormatVersion)
        {
            var tolerateErrors = TolerateErrors(dataFormatVersion);

            return !validationResultContainsErrors || (tolerateErrors && validationResultContainsErrors);
        }

        public IEnumerable<Sjekk> GetChecklist(string dataFormatVersion, string filter)
        {
            var formProperties = GetFormProperties(dataFormatVersion);
            var httpClient = GetChecklistApiHttpClient(dataFormatVersion);

            var checkPoints = (List<Sjekk>)httpClient.GetChecklist(formProperties.ProcessCategory, filter).Result;

            return checkPoints;
        }


        private FormProperties GetFormProperties(string dataFormatVersion)
        {
            try
            {
                foreach (var form in _forms)
                {
                    if (form.DataFormatVersion.Equals(dataFormatVersion))
                    {
                        return form;
                    }
                }

                throw new NullReferenceException($"Illegal dataFormatVersion '{dataFormatVersion}'");
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException($"Illegal dataFormatVersion '{dataFormatVersion}'");
            }
        }


        private ChecklistApiHttpClient GetChecklistApiHttpClient(string dataFormatVersion)
        {
            try
            {
                foreach (var form in _forms)
                {
                    if (form.DataFormatVersion.Equals(dataFormatVersion))
                    {
                        if (form.ServiceAuthority.Equals(Enum.GetName(ServiceOwnerEnum.ATIL)))
                            return _atilChecklistApiHttpClient;
                        else
                            return _dibkChecklistApiHttpClient;
                        
                    }
                }

                throw new NullReferenceException($"Illegal dataFormatVersion '{dataFormatVersion}'");
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException($"Illegal dataFormatVersion '{dataFormatVersion}'");
            }
        }




        private IEnumerable<ChecklistAnswer> AtilSpecificPrefilledChecklist(PrefillChecklistInput prefillChecklistInput)
        {
            throw new NotImplementedException();
        }
    }
}
