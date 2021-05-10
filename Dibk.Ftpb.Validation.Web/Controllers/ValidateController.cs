using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    [ApiController]
    public class ValidateController : BaseController
    {
        private readonly IValidationService _validationService;

        public ValidateController(IValidationService validationService, ILogger<ValidateController> logger) 
            : base(logger)
        {
            _validationService = validationService;
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

            var messages = _validationService.Validate(input.FormData);

            return Ok(messages);
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
