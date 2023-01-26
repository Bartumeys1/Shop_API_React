using DAL.Data.Constants;
using DAL.Data.ViewModels;
using DAL.Entities.Identity;
using DAL.Validation.Account;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, RoleManager<RoleEntity> roleManager)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _roleManager = roleManager;
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
    }
}
