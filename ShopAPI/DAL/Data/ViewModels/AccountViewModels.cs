
using DAL.Data.Constants;

namespace DAL.Data.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ExternalLoginRequest
    {
        public string Provider { get; set; }
        public string Token { get; set; }
    }

    
}
