using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Interfaces.Identity;
using ptPKT.Core.Services.Identity;
using ptPKT.WebUI.Models;
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
        public async Task<IActionResult> SignIn([FromBody] UserLoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var response = await _accountService.SignIn(new UserLoginModel()
                {
                    Email = model.Email,
                    Password = model.Password
                });
                return Ok(response);
            }
            catch (AppUserNotFoundException ex)
            {
                // TODO 
                // logger 
            }
            catch (AppUserIncorrectPasswordException ex)
            {

            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _accountService.SignUp(new UserRegistedModel()
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Email = model.Email,
                    Password = model.Password
                });
                return Ok(response);
            }
            catch (AppUserCreationException ex)
            {
                // 
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await _accountService.SignOut();

            return Ok(new UserLoginResultDTO()
            {
                Token = "fakentokenresult"
            });
        }


        [HttpGet]
        public IActionResult UserList()
        {
            var users = _accountService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Check()
        {
            // TODO
            //var userId = User.Claims.First(u => u.Type == "id").Value;
            //var _appUser = _userManager.FindByIdAsync(userId).Result;
            //return Ok(_appUser);
            throw new NotImplementedException();
        }
    }
}