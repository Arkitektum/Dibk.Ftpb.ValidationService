using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
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
        private ChecklistApiHttpClient _checklistApiHttpClient;
        private readonly IConfiguration _configuration;
        private List<FormProperties> _formProperties;

        public ChecklistService(AtilChecklistApiHttpClient atilChecklistApiHttpClient
                                , DibkChecklistApiHttpClient dibkChecklistApiHttpClient
            , IConfiguration configuration)
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
            _formProperties = formPropertiesFromConfig.Select(x => new FormProperties() 
            { DataFormatVersion = x.DataFormatVersion, ProcessCategory = x.ProcessCategory,  ServiceAuthority = x.ServiceAuthority }).ToList();

        }
        public IEnumerable<ChecklistAnswer> GetPrefillChecklistAnswer(string dataFormatVersion, PrefillChecklistInput prefillChecklistInput)
        {
            var formProperties = GetFormProperties(dataFormatVersion);
            var prefilledChecklist = (List<ChecklistAnswer>)_checklistApiHttpClient.GetPrefillChecklistAnswer(prefillChecklistInput).Result;

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
            var checklistRelatedValidations = (List<ChecklistValidationRelations>)_checklistApiHttpClient.GetChecklistValidationRelations(formProperties.ProcessCategory).Result;

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

        public IEnumerable<Sjekk> GetChecklist(string dataFormatVersion, string filter)
        {
            var formProperties = GetFormProperties(dataFormatVersion);
            var checkPoints = (List<Sjekk>)_checklistApiHttpClient.GetChecklist(formProperties.ProcessCategory, filter).Result;

            return checkPoints;
        }


        public FormProperties GetFormProperties(string dataFormatVersion)
        {
            try
            {
                foreach (var form in _formProperties)
                {
                    if (form.DataFormatVersion.Equals(dataFormatVersion))
                    {
                        if (form.ServiceAuthority.Equals(Enum.GetName(ServiceOwnerEnum.ATIL)))
                            _checklistApiHttpClient = _atilChecklistApiHttpClient;
                        else
                            _checklistApiHttpClient = _dibkChecklistApiHttpClient;
                        
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

        private IEnumerable<ChecklistAnswer> AtilSpecificPrefilledChecklist(PrefillChecklistInput prefillChecklistInput)
        {
            throw new NotImplementedException();
        }
    }
}
