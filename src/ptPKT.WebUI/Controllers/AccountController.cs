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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var response = await _accountService.SignIn(model);
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
                var response = await _accountService.SignUp(model);
                return Ok(response);
            }
            catch (AppUserCreationException)
            {
                // 
            }

            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        public IActionResult SignOut()
        {
            var result = _accountService.SignOut();

            return Ok(result);
        }


        [HttpGet]
        [Authorize]
        public IActionResult UserList()
        {
            var users = _accountService.GetUsers();
            return Ok(users);
        }
    }
}