using DAL.Data.Constants;
using DAL.Data.ViewModels;
using DAL.Entities.Identity;
using DAL.Validation.Account;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Services;
using Services.Services;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            AccountLoginValidation validator = new AccountLoginValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            UserEntity user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { error = "Not found." });

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
                return BadRequest(new { error = "Password incorrect." });

            string token = await _jwtTokenService.CreateTokenAsync(user);

            return Ok(new { token });
        }

        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            try
            {
                var payload = await _jwtTokenService.VerifyGoogleTokenAsync(request);
                if (payload == null)
                {
                    return BadRequest(new { error = "Щось пішло не так!" });
                }
                var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        user = new UserEntity
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName
                        };
                        var resultCreate = await _userManager.CreateAsync(user);
                        var res = await _userManager.AddToRoleAsync(user, Roles.User);
                        if (!resultCreate.Succeeded)
                        {
                            return BadRequest(new { error = "Помилка створення користувача" });
                        }
                    }
                    var resultLOgin = await _userManager.AddLoginAsync(user, info);
                    if (!resultLOgin.Succeeded)
                    {
                        return BadRequest(new { error = "Створення входу через гугл" });
                    }
                }
                string token = await _jwtTokenService.CreateTokenAsync(user);
                return Ok(new { info.LoginProvider, token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody]RegistrationViewModel model)
        {
            AccountRegistrationValidation validator = new AccountRegistrationValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            UserEntity user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return BadRequest(new { error = "User with this email is already registrated." });

            if (!model.Password.Equals(model.ConfirmPassword))
                return BadRequest(new { error = "Error passwords are not equal." });

            user = new UserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email,
            };
            var resultCreate = await _userManager.CreateAsync(user,model.Password);
            if (!resultCreate.Succeeded)
            {
                return BadRequest(new { error = "Помилка реєстрації користувача" });
            }
            var res = await _userManager.AddToRoleAsync(user, Roles.User);


            return Ok(new ServiceResponse { IsSuccess = true , Message="Реєстрація успішна."} );
        }
    }
}
