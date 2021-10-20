using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.Extensions.Logging;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public class ValidationHandler : IValidationHandler
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ValidationHandler> _logger;

        public ValidationResult ValidationResult { get; set; }
        //public ValidationReport ValidationReport { get; set; }

        public ValidationHandler(IServiceProvider services, ILogger<ValidationHandler> logger)
        {
            _services = services;
            _logger = logger;
            //ValidationReport = new ValidationReport();
        }

        public async Task<ValidationResult> ValidateAsync(string dataFormatId, string dataFormatVersion, List<string> errorMessages, ValidationInput validationInput)
        {
            ValidationResult = new ValidationResult();
            ValidationResult.ValidationRules = new();
            ValidationResult.ValidationMessages = new();

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

                ValidateMainForm(dataFormatId, dataFormatVersion, validationInput);
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

            return ValidationResult;
        }

        private void ValidateMainForm(string dataFormatId, string dataFormatVersion, ValidationInput validationInput)
        {
            IFormValidator formValidator = GetValidator(dataFormatId, dataFormatVersion); //45957
            ValidationResult valResult = formValidator.StartValidation(validationInput);

            ValidationResult.ValidationRules.AddRange(valResult.ValidationRules);
            ValidationResult.ValidationMessages.AddRange(valResult.ValidationMessages);
            ValidationResult.PrefillChecklist = valResult.PrefillChecklist;
            ValidationResult.TiltakstyperISoeknad = valResult.TiltakstyperISoeknad;
        }

        private IFormValidator GetValidator(string dataFormatId, string dataFormatVersion)
        {
            //Retrieves classes implementing IForm, having FormDataFormatAttribute and filtering by its DataFormatId
            object formLogicInstance = null;

            try
            {
                var liste = Assembly.GetExecutingAssembly()
                    .GetTypes().OrderBy(y => y.Name);

                var type = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(t => t.IsDefined(typeof(FormDataAttribute), true)).SingleOrDefault(t => t.GetCustomAttribute<FormDataAttribute>()?.DataFormatVersion == dataFormatVersion && t.GetCustomAttribute<FormDataAttribute>()?.DataFormatId == dataFormatId);

                if (type != null)
                {
                    formLogicInstance = _services.GetService(type);
                }
                else
                {
                    _logger.LogError("No validator found for {DataFormatVersion}:{DataFormatId}", dataFormatVersion, dataFormatId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when attempting to find validator for {DataFormatVersion}:{DataFormatId}", dataFormatVersion, dataFormatId);
                throw;
            }

            return formLogicInstance as IFormValidator;
        }
    }
}
