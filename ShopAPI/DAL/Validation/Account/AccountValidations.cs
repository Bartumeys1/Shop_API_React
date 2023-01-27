using DAL.Data.ViewModels;
using FluentValidation;
using System.Text.RegularExpressions;

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

    public class AccountRegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public AccountRegistrationValidation()
        {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Email).EmailAddress().NotEmpty();
            RuleFor(r => r.PhoneNumber);//.Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{2}-\d{2}")).WithMessage("PhoneNumber not valid");
            RuleFor(r => r.Password).MinimumLength(6).Equal(r => r.ConfirmPassword).When(r => !string.IsNullOrWhiteSpace(r.Password));
        }
    }
}
