using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    [ApiController]
    public class ValidateController : BaseController
    {
        private readonly IValidationService _validationService;
        private readonly ICodeListService _codeListService;

        public ValidateController(IValidationService validationService, ILogger<ValidateController> logger, ICodeListService codeListService)
            : base(logger)
        {
            _validationService = validationService;
            _codeListService = codeListService;
        }

        [Route("api/validate")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ValidationResult> ValidateForm([FromBody] ValidationInput input)
        {
            //// Authentication?
            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            var messages = _validationService.Validate(input);

            return Ok(messages);
        }

        [Route("api/validationReport")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ValidationReport> VaalidationReportForm([FromBody] ValidationInput input)
        {
            //// Authentication?
            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            //var validationResult = _validationService.Validate(input);
            var validationReport = _validationService.GetValidationReport(input);
            //var validationReport = new ValidationReport() { ValidationResult = validationResult, PrefillChecklist = prefillChecklist };
            
            return Ok(validationReport);
        }

        [Route("api/validate/file")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]  // TODO!
        public IActionResult ValidateFile(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            try
            {
                var messages = _validationService.Validate(file);

                return Ok(messages);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        private bool VerifyInput(ValidationInput input)
        {
            return !string.IsNullOrWhiteSpace(input.FormData);
        }
    }
}
