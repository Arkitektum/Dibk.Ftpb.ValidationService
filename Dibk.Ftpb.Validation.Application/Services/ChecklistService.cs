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
        private readonly IOptions<DataFormatVersionSettings> _dataFormatVersionSettings;
        private readonly IOptions<FormPropertiesSettings> _formPropertiesSettings;
        private readonly IConfiguration _configuration;

        //protected string ChecklistName { get { return _atilChecklistApiHttpClient._httpClient.BaseAddress.ToString(); } }


        public ChecklistService(AtilChecklistApiHttpClient atilChecklistApiHttpClient
                                , DibkChecklistApiHttpClient dibkChecklistApiHttpClient
                                , IOptions<DataFormatVersionSettings> dataFormatVersionSettings
                                , IOptions<FormPropertiesSettings> formPropertiesSettings
            , IConfiguration configuration)
        {
            _atilChecklistApiHttpClient = atilChecklistApiHttpClient;
            _dibkChecklistApiHttpClient = dibkChecklistApiHttpClient;
            _dataFormatVersionSettings = dataFormatVersionSettings;
            _formPropertiesSettings = formPropertiesSettings;
            _configuration = configuration;
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
            var checklistRelatedValidations = (List<ChecklistValidationRelations>)_checklistApiHttpClient.GetChecklistValidationRelations().Result;

            List<ValidationMessage> filteredMessages = new List<ValidationMessage>();
            foreach (var message in validationMessages)
            {
                var messageWODataFormatVersion = message.Reference.Substring(5);
                
                if (!checklistRelatedValidations.Any(x => x.SupportingDataValidationRuleId.Any(y => y.Equals(messageWODataFormatVersion))))
                {
                    //ValidationID does not exist in sjekklist
                    filteredMessages.Add(message);
                }
                else
                {
                    //ValidationID DOES exist in sjekklist. Checking if enterprise terms (tiltakstyper) match
                    //Find checkpt with validationID

                    var firstChecklistPointMatching = checklistRelatedValidations.First(x => x.SupportingDataValidationRuleId.Any(y => y.Equals(messageWODataFormatVersion)));
                    //var tiltakstyperNotInEnterpriseTerms = firstChecklistPointMatching.EnterpriseTerms.Where(t2 => !tiltakstyper.Any(t1 => t2.Contains(t1)));
                    var tiltakstyperAmongstEnterpriseTerms = firstChecklistPointMatching.EnterpriseTerms.Any(t2 => tiltakstyper.Any(t1 => t2.Contains(t1)));

                    if (tiltakstyperAmongstEnterpriseTerms)
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
            var checkPoints = (List<Sjekk>)_checklistApiHttpClient.GetChecklist(formProperties.Soknadstype, filter).Result;

            return checkPoints;
        }


        private FormProperties GetFormProperties(string dataFormatVersion)
        {
            try
            {
                FormProperties formFound = new();
                var formProperties = _configuration.GetSection("FormProperties").GetChildren().ToList()
                            .Select(x =>
                                 (
                                      x.GetValue<string>("DataFormatVersion"),
                                      x.GetValue<string>("ServiceAuthority"),
                                      x.GetValue<string>("Soknadstype")
                                  )
                            )
                            .ToList<(string DataFormatVersion, string ServiceAuthority, string Soknadstype)>();

                foreach (var form in formProperties)
                {
                    if (form.DataFormatVersion.Equals(dataFormatVersion))
                    {
                        formFound.DataFormatVersion = form.DataFormatVersion;
                        formFound.ServiceAuthority = form.ServiceAuthority;
                        formFound.Soknadstype = form.Soknadstype;
                        break;
                    }
                }

                if (formFound == null)
                    throw new NullReferenceException($"Illegal dataFormatVersion '{dataFormatVersion}'");

                if (formFound.ServiceAuthority.Equals(Enum.GetName(ServiceOwnerEnum.ATIL)))
                    _checklistApiHttpClient = _atilChecklistApiHttpClient;
                else
                    _checklistApiHttpClient = _dibkChecklistApiHttpClient;

                return formFound;
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
