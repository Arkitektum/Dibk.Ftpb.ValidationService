using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Enums;
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
        //private Models.InputData _inputData;
        //private List<string> _errorMessages;
        //private ValidationResult _validationResult;
        private List<ChecklistAnswer> _outputlist = new List<ChecklistAnswer>();
        private IEnumerable<PrefillDemo> _alleSjekklistepunkter;

        public ValidationService(IInputDataService inputDataService, IXsdValidationService xsdValidationService, IValidationHandler validationOrchestrator, IChecklistService checklistService)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
            _validationHandler = validationOrchestrator;
            _checklistService = checklistService;
        }

        public ValidationResult GetValidationResult(ValidationInput validationInput)
        {
            var result = ValidateForm(validationInput);
            //Clearing out prefilled checklist answers before returning the validation result - not to be part of the response when validating
            result.PrefillChecklist = null;

            return result;
        }
        public ValidationResult GetValidationResultWithChecklistAnswers(ValidationInput validationInput)
        {
            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            var validationResult = ValidateForm(validationInput);

            if (!Helpers.ObjectIsNullOrEmpty(inputData?.Config?.DataFormatVersion))
            {
                var formProperties = _checklistService.GetFormProperties(inputData?.Config?.DataFormatVersion);
                var prefilledAnswersFromChecklist = _checklistService.GetPrefillChecklist(validationResult, inputData?.Config?.DataFormatVersion, formProperties.ProcessCategory);

                validationResult.PrefillChecklist.ChecklistAnswers.AddRange(prefilledAnswersFromChecklist.ChecklistAnswers);

                foreach (var answer in validationResult.PrefillChecklist.ChecklistAnswers)
                {
                    if (answer.supportingDataValidationRuleId != null)
                    {
                        answer.supportingDataXpathField = new List<string>();
                        foreach (var ruleId in answer.supportingDataValidationRuleId)
                        {
                            var foundXPath = validationResult.ValidationRules.First(x => x.Id.Equals(ruleId)).Xpath;
                            var xPathsIfNotAlreadyExisting = validationResult.ValidationRules.Where
                                (x => ruleId.Equals(x.Id) && !answer.supportingDataXpathField.Any(y => y.Equals(x.Xpath))).Select(z => z.Xpath).ToList();
                            
                            answer.supportingDataXpathField.AddRange(xPathsIfNotAlreadyExisting);
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

            if (!Helpers.ObjectIsNullOrEmpty(inputData?.Config?.DataFormatVersion))
            {
                validationResult = _validationHandler.ValidateAsync(inputData?.Config?.DataFormatVersion, errorMessages, validationInput).Result;

                var formProperties = _checklistService.GetFormProperties(inputData?.Config?.DataFormatVersion);
                validationResult.Soknadtype = formProperties.ProcessCategory;
            }
            else
            {
                validationResult.ValidationMessages = new List<ValidationMessage> { new() { Message = "Can't Get DataFormatId" } };
            }

            validationResult.Errors = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.ERROR).Count();
            validationResult.Warnings = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.WARNING).Count();

            validationResult.messages = new Messages() { ValidationMessage = validationResult.ValidationMessages };
            validationResult.rulesChecked = new RulesChecked() { ValidationRule = validationResult.ValidationRules };

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

            var sjekkpktHaandert = _outputlist.FirstOrDefault(x => x.checklistReference.Equals(checklistReference)) != null;
            if (!sjekkpktHaandert)
            {
                outputpt = AddStuff(outputpt);
                if (hovedpktSomHarUnderpkt != null)
                {
                    outputpt.checklistReference = hovedpktSomHarUnderpkt.ChecklistReference;
                    outputpt.checklistQuestion = hovedpktSomHarUnderpkt.ChecklistQuestion;
                    outputpt.yesNo = hovedpktSomHarUnderpkt.YesNo;
                    _outputlist.Add(outputpt);

                    foreach (var underpkt in _alleSjekklistepunkter.Where(x => hovedpktSomHarUnderpkt.ChecklistReference.Equals(x.ParentActivityAction)))
                    {
                        LagDings(underpkt.ChecklistReference);
                    }
                }
                else if (hovedpktSomHarDok != null)
                {
                    outputpt.checklistReference = hovedpktSomHarDok.ChecklistReference;
                    outputpt.checklistQuestion = hovedpktSomHarDok.ChecklistQuestion;
                    outputpt.yesNo = hovedpktSomHarDok.YesNo;
                    outputpt.documentation = $"Dette er dokumentasjon for sjekklistepunkt {hovedpktSomHarDok.ChecklistReference}";
                    _outputlist.Add(outputpt);
                }
                else
                {
                    outputpt.checklistReference = hovedpkt.ChecklistReference;
                    outputpt.checklistQuestion = hovedpkt.ChecklistQuestion;
                    var randomBool = new Random().Next(2) == 1; // 0 = false, 1 = true;
                    outputpt.yesNo = randomBool;
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
            input.supportingDataValidationRuleId = new List<string>();
            input.supportingDataValidationRuleId.Add("999.99");
            input.supportingDataValidationRuleId.Add("999.66");

            input.supportingDataXpathField = new List<string>();
            input.supportingDataXpathField.Add("ArbeidstilsynetsSamtykke/aaa");
            input.supportingDataXpathField.Add("ArbeidstilsynetsSamtykke/bbb");

            return input;
        }


    }
}
