using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Web.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dibk.Ftpb.Validation.Web.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {

        // POST api/<ValidateController>
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

        private bool VerifyInput(ValidationInput input)
        {
            return !string.IsNullOrWhiteSpace(input.FormData);
        }



    }
}
