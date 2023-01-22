using DAL.Data.ViewModels;
using DAL.Entities.Identity;
using DAL.Validation.Account;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        public AccountController(UserManager<UserEntity> userManager)
        {
            _userManager= userManager;
        }

        [HttpPost("Login")]
        //[Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            AccountLoginValidation validator = new AccountLoginValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            UserEntity user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { error = "Not found."});

            bool checkPassword = await _userManager.CheckPasswordAsync(user,model.Password);
            if (!checkPassword) 
                return BadRequest(new {error = "Password incorrect."});

            return Ok(model);
        }
    }
}
