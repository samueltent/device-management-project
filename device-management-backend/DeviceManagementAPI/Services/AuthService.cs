using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        //private string tokenIssuer = 'https://localhost:44395';
        //private string tokenAudience = 'https://localhost:4200';

        private readonly IAuthenticationConfiguration _authConfig;
        
        public AuthService(IAuthenticationConfiguration authConfig)
        {
            _authConfig = authConfig;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id),
                new Claim("email", user.Email),
                new Claim("name", user.Name)
            };

            JwtSecurityToken token = new JwtSecurityToken(null,
                                    null,
                                    claims,
                                    DateTime.Now,
                                    DateTime.Now.AddHours(4),
                                    credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
