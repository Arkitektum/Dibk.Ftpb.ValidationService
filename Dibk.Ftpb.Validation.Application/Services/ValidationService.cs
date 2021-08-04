using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IInputDataService _inputDataService;
        private readonly IXsdValidationService _xsdValidationService;
        private readonly IValidationOrchestrator _validationOrchestrator;

        public ValidationService(
            IInputDataService inputDataService,
            IXsdValidationService xsdValidationService,
            IValidationOrchestrator validationOrchestrator)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
            _validationOrchestrator = validationOrchestrator;
        }


        public ValidationReport GetValidationReport(ValidationInput validationInput)
        {
            var xx = new ValidationReport();
            xx.ValidationResult = Validate(validationInput);



            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            var prefillChecklist = new PrefillChecklist();
            if (!Helpers.ObjectIsNullOrEmpty(inputData?.Config?.DataFormatVersion))
            {
                xx.PrefillChecklist = _validationOrchestrator.GetPrefillChecklistAsync(inputData?.Config?.DataFormatVersion, validationInput).Result;
            }

            return xx;
        }

        public ValidationResult Validate(ValidationInput validationInput)
        {
            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            //inputData.Config.DataFormatVersion
            //inputData.Data
            //inputData.IsValid
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
                validationResult = _validationOrchestrator.ValidateAsync(inputData?.Config?.DataFormatVersion, errorMessages, validationInput).Result.ValidationResult;
                //return taskValidationResult.Result;
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
