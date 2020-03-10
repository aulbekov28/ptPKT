using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Interfaces.Identity;
using ptPKT.Core.Services.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ptPKT.WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _identityService.SignIn(model);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserRegistedModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _identityService.SignUp(model);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult SignOut()
        {
            var result = _identityService.SignOut();

            return Ok(result);
        }
    }
}