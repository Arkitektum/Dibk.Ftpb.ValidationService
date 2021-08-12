using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IInputDataService _inputDataService;
        private readonly IXsdValidationService _xsdValidationService;
        private readonly IValidationHandler _validationOrchestrator;
        private readonly IChecklistService _checklistService;
        private Models.InputData _inputData;
        private List<string> _errorMessages;
        private ValidationResult _validationResult;

        public ValidationService(
            IInputDataService inputDataService,
            IXsdValidationService xsdValidationService,
            IValidationHandler validationOrchestrator,
            IChecklistService checklistService)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
            _validationOrchestrator = validationOrchestrator;
            _checklistService = checklistService;
        }


        public ValidationReport GetValidationReport(ValidationInput validationInput)
        {
            var validationReport = new ValidationReport();
            var validationResult = Validate(validationInput);
            validationReport.ValidationResult = validationResult;

            if (!validationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.ERROR)))
            {
                if (!Helpers.ObjectIsNullOrEmpty(_inputData?.Config?.DataFormatVersion))
                {
                    var prefillChecklist = PrefillChecklistAnswerBuilder.Build(validationResult, _checklistService, _inputData.Config.DataFormatVersion);
                    validationReport.PrefillChecklist = prefillChecklist;

                    return validationReport;
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException("Missing DataFormatVersion");
                }

                throw new System.ArgumentOutOfRangeException("Illegal DataFormatVersion");
            }

            return validationReport;
        }

        public ValidationResult Validate(ValidationInput validationInput)
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
                validationResult = _validationOrchestrator.ValidateAsync(inputData?.Config?.DataFormatVersion, errorMessages, validationInput).Result;
            }
            else
            {
                validationResult.ValidationMessages = new List<ValidationMessage> { new() { Message = "Can't Get DataFormatId" } };
            }
            
            validationResult.Errors = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.ERROR).Count(); 
            validationResult.Warnings = validationResult.ValidationMessages.Where(x => x.Messagetype == Enums.ValidationResultSeverityEnum.WARNING).Count();
            validationResult.messages = validationResult.ValidationMessages;
            validationResult.rulesChecked = validationResult.ValidationRules;

            return validationResult;
        }

        public List<string> Validate(IFormFile xmlFile)
        {
            using var inputData = _inputDataService.GetInputData(xmlFile);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }
    }
}
