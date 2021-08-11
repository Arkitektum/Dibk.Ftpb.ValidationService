using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Microsoft.Extensions.Logging;

namespace Dibk.Ftpb.Validation.Application.Process
{
    public class ValidationOrchestrator : IValidationOrchestrator
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ValidationOrchestrator> _logger;

        public ValidationResult ValidationResult { get; set; }
        public ValidationReport ValidationReport { get; set; }

        public ValidationOrchestrator(IServiceProvider services, ILogger<ValidationOrchestrator> logger)
        {
            _services = services;
            _logger = logger;
            ValidationReport = new ValidationReport();
        }

        public async Task<ValidationResult> ValidateAsync(string dataFormatVersion, List<string> errorMessages, ValidationInput validationInput)
        {
            ValidationResult = new ValidationResult();
            ValidationResult.ValidationRules = new();
            ValidationResult.ValidationMessages = new();

            ValidationReport.ValidationResult = ValidationResult;


            if (errorMessages.Count > 0)
            {
                foreach (var message in errorMessages)
                {
                    ValidationResult.ValidationMessages.Add(new ValidationMessage() { Rule = "XsdValidationErrors", Message = message });
                }
            }
            else
            {
                //TODO Validate Xml structure 
                List<ValidationRule> validationXmlMessages = new List<ValidationRule>();
                ValidationResult.ValidationRules.AddRange(validationXmlMessages);

                ValidateMainForm(dataFormatVersion, validationInput);

            }

            // Todo: On ERRORS
            //_logger.Debug($"{archiveReference} failed validation for structure issues");
            //// Send notificaiton to Altinn user 
            //string title = String.Format(Resources.TextStrings.ShippingErrorTitle, archivedForm.ArchiveReference);
            //string summary = Resources.TextStrings.ShippingErrorSummary;
            //string body = String.Format(Resources.TextStrings.ShippingErrorBody, archivedForm.ArchiveReference) + Environment.NewLine + Resources.TextStrings.ShippingErrorStructure + valResultStruct.ToString();

            //_logEntryService.Save(new LogEntry(archiveReference, Resources.TextStrings.ShippingErrorStructure + valResultStruct.ToString(), "Error"));
            //_formMetadataService.UpdateValidationResultToFormMetadata(archiveReference, "Feil - strukturvalidering", valResultStruct.Errors, valResultStruct.Warnings);
            //_correspondenceHelper.SendSimpleNotificaitonToReportee(archivedForm.Reportee, title, summary, body, archivedForm.ArchiveReference);


            // Todo: On WARNINGS
            //validationWarnings = valResultData.Warnings;
            ////Logge advarsler
            //_logEntryService.Save(new LogEntry(archiveReference, "Valideringsresultat: " + valResultData.ToString(), "Info"));




            //Todo
            //Logge metadata om innsending    
            //_formMetadataService.SaveFormDataToFormMetadataLog(formData);
            //_formMetadataService.UpdateValidationResultToFormMetadata(archiveReference, "Under behandling", 0, validationWarnings);
                
            //_formMetadataService.UpdateValidationResultToFormMetadata(archiveReference, "Ok", 0, validationWarnings);


            return ValidationReport.ValidationResult;
        }

        private void ValidateMainForm(string dataFormatVersion, ValidationInput validationInput)
        {
            IFormValidator formValidator = GetValidator(dataFormatVersion); //45957
            ValidationResult valResult = formValidator.StartValidation(dataFormatVersion, validationInput);

            ValidationResult.ValidationRules.AddRange(valResult.ValidationRules);
            ValidationResult.ValidationMessages.AddRange(valResult.ValidationMessages);
        }

        private IFormValidator GetValidator(string dataFormatVersion)
        {
            //Retrieves classes implementing IForm, having FormDataFormatAttribute and filtering by its DataFormatId
            object formLogicInstance = null;

            try
            {
                var type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsDefined(typeof(FormDataAttribute), true))
                    .Where(t => t.GetCustomAttribute<FormDataAttribute>().DataFormatVersion == dataFormatVersion)
                    .SingleOrDefault();

                formLogicInstance = _services.GetService(type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when attempting to find validator for {DataFormatVersion}", dataFormatVersion);
                throw;
            }

            return formLogicInstance as IFormValidator;
        }
    }
}
