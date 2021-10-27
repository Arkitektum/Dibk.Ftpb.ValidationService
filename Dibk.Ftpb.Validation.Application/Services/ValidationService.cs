using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IInputDataService _inputDataService;
        private readonly IXsdValidationService _xsdValidationService;
        private readonly IValidationHandler _validationHandler;
        private readonly IChecklistService _checklistService;
        private FormPropertyService _formPropertyService;

        //private Models.InputData _inputData;
        //private List<string> _errorMessages;
        //private ValidationResult _validationResult;
        private List<ChecklistAnswer> _outputlist = new List<ChecklistAnswer>();
        private IEnumerable<PrefillDemo> _alleSjekklistepunkter;

        public ValidationService(IInputDataService inputDataService, IXsdValidationService xsdValidationService,
            IValidationHandler validationOrchestrator, IChecklistService checklistService, FormPropertyService formPropertyService)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
            _validationHandler = validationOrchestrator;
            _checklistService = checklistService;
            _formPropertyService = formPropertyService;
        }

        public Validations GetValidationResult(ValidationInput validationInput)
        {
            var result = ValidateForm(validationInput);
            //Clearing out prefilled checklist answers before returning the validation result - not to be part of the response when validating
            //result.PrefillChecklist = null;

            var validations = new Validations();
            validations.Messages = result.ValidationMessages;
            validations.RulesChecked = result.ValidationRules;
            validations.Warnings = result.Warnings;
            validations.Errors = result.Errors;
            validations.Soknadtype = result.Soknadtype;
            validations.TiltakstyperISoeknad = result.TiltakstyperISoeknad;

            return validations;
        }
        public ValidationResult GetValidationResultWithChecklistAnswers(ValidationInput validationInput)
        {
            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            var validationResult = ValidateForm(validationInput);

            if (!string.IsNullOrEmpty(inputData?.Config?.DataFormatVersion) && !string.IsNullOrEmpty(inputData?.Config?.DataFormatId))
            {
                var formProperties = _formPropertyService.GetFormProperties(inputData?.Config?.DataFormatId, inputData?.Config?.DataFormatVersion);

                var prefilledAnswersFromChecklist = _checklistService.GetPrefillChecklist(validationResult, inputData?.Config?.DataFormatId, inputData?.Config?.DataFormatVersion, formProperties.ProcessCategory);

                validationResult.PrefillChecklist.AddRange(prefilledAnswersFromChecklist.ChecklistAnswer);

                foreach (var answer in validationResult.PrefillChecklist)
                {
                    if (answer.SupportingDataValidationRuleId != null)
                    {
                        answer.SupportingDataXpathField = new List<string>();
                        foreach (var ruleId in answer.SupportingDataValidationRuleId)
                        {
                            var foundXPath = validationResult.ValidationRules.First(x => x.Id.Equals(ruleId)).XpathField;
                            var xPathsIfNotAlreadyExisting = validationResult.ValidationRules.Where
                                (x => ruleId.Equals(x.Id) && !answer.SupportingDataXpathField.Any(y => y.Equals(x.XpathField))).Select(z => z.XpathField).ToList();

                            answer.SupportingDataXpathField.AddRange(xPathsIfNotAlreadyExisting);
                        }
                    }
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("Missing DataFormatVersion");
            }

            return validationResult;
        }

        public ValidationRule[] GetFormValidationRules(string dataFormatId, string dataFormVersion)
        {
            var rules =_validationHandler.GetformRulesAsync(dataFormatId, dataFormVersion).Result;
            return rules;
        }
        private ValidationResult ValidateForm(ValidationInput validationInput)
        {
            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            var errorMessages = _xsdValidationService.Validate(inputData);
            var validationResult = new ValidationResult();

            //Verify stuff - functionality as it is in Alpha
            //**************         START             *************************************************************************************

            //_logger.Debug($"{archiveReference} is being validated for structure issues");
            ////Validate structure
            //string formDataAsXml = archivedForm.Forms.First().FormData;
            //string mainDataFormatID = archivedForm.Forms.First().DataFormatID;
            //string mainDataFormatVersionID = archivedForm.Forms.First().DataFormatVersionID.ToString();

            //ftbId = XmlUtil.GetElementValue(formDataAsXml, "ftbid");


            //FormValidationService val = new FormValidationService();
            //if (!val.IsKnownForm(mainDataFormatID, mainDataFormatVersionID))
            //{
            //    _logger.Debug($"{archiveReference} form is not supported");
            //    _logEntryService.Save(new LogEntry(archiveReference, Resources.TextStrings.ShippingErrorDownloadAndDeserialize + " skjema (" + archivedForm.ServiceCode + "/" + archivedForm.ServiceEditionCode + ") støttes ikke", "Error"));

            //    string title = String.Format(Resources.TextStrings.ShippingErrorTitle, archivedForm.ArchiveReference);
            //    string summary = Resources.TextStrings.ShippingErrorSummary;
            //    string body = String.Format(Resources.TextStrings.ShippingErrorBody, archivedForm.ArchiveReference) + Environment.NewLine + Resources.TextStrings.ShippingErrorDownloadAndDeserialize + " skjema (" + archivedForm.ServiceCode + "/" + archivedForm.ServiceEditionCode + ") støttes ikke";
            //    _correspondenceHelper.SendSimpleNotificaitonToReportee(archivedForm.Reportee, title, summary, body, archivedForm.ArchiveReference);
            //    _formMetadataService.UpdateValidationResultToFormMetadata(archiveReference, "Feil", 1, 0);

            //**************         END             *************************************************************************************

            if (!string.IsNullOrEmpty(inputData?.Config?.DataFormatId) || !string.IsNullOrEmpty(inputData?.Config?.DataFormatVersion))
            {
                var dataFormatVersion = inputData?.Config?.DataFormatVersion;
                var dataFormatId = inputData?.Config?.DataFormatId;
                validationResult = _validationHandler.ValidateAsync(dataFormatId, dataFormatVersion, errorMessages, validationInput).Result;

                var formProperties = _formPropertyService.GetFormProperties(dataFormatId, dataFormatVersion);
                validationResult.Soknadtype = formProperties.ProcessCategory;
            }
            else
            {
                validationResult.ValidationMessages = new List<ValidationMessage> { new() { Message = "Can't Get DataFormatId, DataFormatVersion from xml data" } };
            }

            validationResult.Errors = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.ERROR).Count();
            validationResult.Warnings = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.WARNING).Count();

            validationResult.Messages = validationResult.ValidationMessages;
            validationResult.RulesChecked = validationResult.ValidationRules;

            //validationResult.messages = validationResult.ValidationMessages;
            //validationResult.rulesChecked = validationResult.ValidationRules;

            return validationResult;
        }

        public List<string> ValidateXmlFile(IFormFile xmlFile)
        {
            using var inputData = _inputDataService.GetInputData(xmlFile);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }

        public string PrefillDemo()
        {
            _alleSjekklistepunkter = JsonConvert.DeserializeObject<IEnumerable<PrefillDemo>>(_checklistService.GetPrefillDemo());
            //Returner XML
            List<string> done = new();
            int i = 0;
            foreach (var item in _alleSjekklistepunkter.Where(x => x.ParentActivityAction == null).Select(y => y))
            {
                i++;
                LagDings(item.ChecklistReference);
            }

            var xx = MakeValidationreport(_outputlist);
            return xx;
        }

        private void LagDings(string checklistReference)
        {
            var outputpt = new ChecklistAnswer();
            var hovedpktSomHarUnderpkt = _alleSjekklistepunkter.FirstOrDefault(x => x.ChecklistReference.Equals(checklistReference) && x.ActionTypeCode.Equals("SU"));
            var hovedpktSomHarDok = _alleSjekklistepunkter.FirstOrDefault(x => x.ChecklistReference.Equals(checklistReference) && x.ActionTypeCode.Equals("DOK"));
            var hovedpkt = _alleSjekklistepunkter.FirstOrDefault(x => x.ChecklistReference.Equals(checklistReference));

            var sjekkpktHaandert = _outputlist.FirstOrDefault(x => x.ChecklistReference.Equals(checklistReference)) != null;
            if (!sjekkpktHaandert)
            {
                outputpt = AddStuff(outputpt);
                if (hovedpktSomHarUnderpkt != null)
                {
                    outputpt.ChecklistReference = hovedpktSomHarUnderpkt.ChecklistReference;
                    outputpt.ChecklistQuestion = hovedpktSomHarUnderpkt.ChecklistQuestion;
                    outputpt.YesNo = hovedpktSomHarUnderpkt.YesNo;
                    _outputlist.Add(outputpt);

                    foreach (var underpkt in _alleSjekklistepunkter.Where(x => hovedpktSomHarUnderpkt.ChecklistReference.Equals(x.ParentActivityAction)))
                    {
                        LagDings(underpkt.ChecklistReference);
                    }
                }
                else if (hovedpktSomHarDok != null)
                {
                    outputpt.ChecklistReference = hovedpktSomHarDok.ChecklistReference;
                    outputpt.ChecklistQuestion = hovedpktSomHarDok.ChecklistQuestion;
                    outputpt.YesNo = hovedpktSomHarDok.YesNo;
                    outputpt.Documentation = $"Dette er dokumentasjon for sjekklistepunkt {hovedpktSomHarDok.ChecklistReference}";
                    _outputlist.Add(outputpt);
                }
                else
                {
                    outputpt.ChecklistReference = hovedpkt.ChecklistReference;
                    outputpt.ChecklistQuestion = hovedpkt.ChecklistQuestion;
                    var randomBool = new Random().Next(2) == 1; // 0 = false, 1 = true;
                    outputpt.YesNo = randomBool;
                    _outputlist.Add(outputpt);
                }
            }
        }

        private string MakeValidationreport(List<ChecklistAnswer> _outputlist)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<ChecklistAnswer>));
            var subReq = _outputlist;
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString(); // Your XML
                }
            }

            return xml;
        }
        private ChecklistAnswer AddStuff(ChecklistAnswer input)
        {
            input.SupportingDataValidationRuleId = new List<string>();
            input.SupportingDataValidationRuleId.Add("999.99");
            input.SupportingDataValidationRuleId.Add("999.66");

            input.SupportingDataXpathField = new List<string>();
            input.SupportingDataXpathField.Add("ArbeidstilsynetsSamtykke/aaa");
            input.SupportingDataXpathField.Add("ArbeidstilsynetsSamtykke/bbb");

            return input;
        }

        //public FormProperties GetFormProperties(string dataFormatVersion)
        //{



        //    throw new NotImplementedException();
        //}
    }
}
