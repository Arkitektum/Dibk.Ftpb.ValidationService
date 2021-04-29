using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    [ApiController]
    public class ValidateController : BaseController
    {
        private readonly IValidationService _validationService;

        public ValidateController(
            IValidationService validationService,
            ILogger<ValidateController> logger) : base(logger)
        {
            _validationService = validationService;
        }

        [Route("api/validate")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]  // TODO!
        public IActionResult ValidateForm([FromBody] ValidationInput input)
        {

            // Authentication?

            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            // er det innhold ... send det videre!

            return Ok();

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
