using DAL.Data.ViewModels;
using FluentValidation;

namespace DAL.Validation.Account
{
    public class AccountLoginValidation:AbstractValidator<LoginViewModel>
    {
        public AccountLoginValidation()
        {
            RuleFor(r => r.Email).EmailAddress().NotEmpty();
            RuleFor(r => r.Password).MinimumLength(6).NotEmpty();
        }
    }
}
