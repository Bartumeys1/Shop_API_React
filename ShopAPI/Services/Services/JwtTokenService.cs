using DAL.Data.ViewModels;
using DAL.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(UserEntity user);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(ExternalLoginRequest request);
    }
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
        public JwtTokenService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(UserEntity user)
        {
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("name",user.UserName)
            };
            foreach (string role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            string key = _configuration.GetValue<string>("JwtKey");
            SymmetricSecurityKey signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: "MyAuthServer",//Константа ISSUER представляет издателя токена. Здесь можно определить любое название.
                audience: "MyAuthClient",//Константа AUDIENCE представляет потребителя токена - опять же может быть любая строка, обычно это сайт, на котором применяется токен.
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(ExternalLoginRequest request)
        {
            string clientID = _configuration["GoogleAuthSettings:ClientId"];
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { clientID }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
            return payload;
        }
    }
}
