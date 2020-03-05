using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Interfaces.Identity;
using ptPKT.Core.Services.Identity;
using System;
using System.Threading.Tasks;

namespace ptPKT.WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var response = await _identityService.SignIn(model);
                return Ok(response);
            }
            catch (AppUserNotFoundException)
            {
                return Unauthorized();
            }
            catch (AppUserIncorrectPasswordException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserRegistedModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _identityService.SignUp(model);
                return Ok(response);
            }
            catch (AppUserIdentityException)
            {
                // 
            }

            return Unauthorized();
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