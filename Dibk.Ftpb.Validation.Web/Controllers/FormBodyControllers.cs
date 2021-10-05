using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Models.Web;
using System.Collections.Generic;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    [ApiController]

    public class FormBodyControllers : BaseController
    {
        private readonly IValidationService _validationService;
        private readonly ICodeListService _codeListService;
        public FormBodyControllers(IValidationService validationService, ILogger<ValidateController> logger, ICodeListService codeListService)
            : base(logger)
        {
            _validationService = validationService;
            _codeListService = codeListService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/validate/45957")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Validations_45957> ValidateForm_45957([FromBody] ValidationInput input)
        {
            //// Authentication?
            if (!VerifyInput(input))
            {
                return BadRequest();
            }

            var messages = _validationService.GetValidationResult(input);
            Validations_45957 res = new Validations_45957() { Validations = messages, ArbeidstilsynetsSamtykkeXml = input.FormData };


            return Ok(res);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/prefill-demo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> PrefillDemo()
        {

            var resul = _validationService.PrefillDemo();

            return Ok(resul);
        }

        private bool VerifyInput(ValidationInput input)
        {
            return !string.IsNullOrWhiteSpace(input.FormData);
        }
    }
}
