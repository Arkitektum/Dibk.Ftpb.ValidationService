using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Models.Standard;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Newtonsoft.Json.Linq;

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    [ApiController]
    public class ValidateController : BaseController
    {
        private readonly IValidationService _validationService;
        //private readonly ICodeListService _codeListService;
        private FormPropertyService _formPropertyService;

        public ValidateController(IValidationService validationService, ILogger<ValidateController> logger, ICodeListService codeListService, FormPropertyService formPropertyService)
            : base(logger)
        {
            _validationService = validationService;
            //_codeListService = codeListService;
            _formPropertyService = formPropertyService;
        }

        [Route("api/validate")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Validations> ValidateForm([FromBody] ValidationInput input)
        {
            //// Authentication?
            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            var messages = _validationService.GetValidationResult(input);

            return Ok(messages);
        }

        [Route("api/validationReport")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ValidationResult> VaalidationReportForm([FromBody] ValidationInput input)
        {
            //// Authentication?
            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            var validationResult = _validationService.GetValidationResultWithChecklistAnswers(input);

            return Ok(validationResult);
        }
        [Route("api/formrules/{dataFormatId}/{dataFormatVersion}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RuleDocumentationModel[]> FormDocumentation(string dataFormatId, string dataFormatVersion)
        {
            //// Authentication?
            if (string.IsNullOrEmpty(dataFormatId) || string.IsNullOrEmpty(dataFormatVersion))
            {
                return BadRequest();
            }

            var formValidationRules = _validationService.GetFormValidationRules(dataFormatId,dataFormatVersion);
            if (!formValidationRules.Any())
            {
                return BadRequest($"No rules found for '{dataFormatId}'.'{dataFormatVersion}'");
            }
            var documentationModel = RuleDocumentationComposer.GetRuleDocumentationModel(formValidationRules.ToList());
            return Ok(documentationModel);
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
                var messages = _validationService.ValidateXmlFile(file);

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

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/formproperties")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FormProperties> ChecklistUrlInfo([FromBody] string dataFormatId, string dataFormatVersion)
        {
            if (string.IsNullOrEmpty(dataFormatVersion) && string.IsNullOrEmpty(dataFormatVersion))
            {
                return BadRequest();
            }

            var props = _formPropertyService.GetFormProperties(dataFormatId, dataFormatVersion);

            return Ok(props);
        }

        private bool VerifyInput(ValidationInput input)
        {
            return !string.IsNullOrWhiteSpace(input.FormData);
        }
    }
}
