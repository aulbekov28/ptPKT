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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IIdentityService identityService)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
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
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
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