using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HrSystem.DTOs;
using HrSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HrSystem.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            //converting 15mins in appsettings.json to double
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));
            //var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                   issuer: jwtSettings.GetSection("Issuer").Value,
                   audience: jwtSettings.GetSection("Audience").Value,
                   claims: claims,
                   expires: expiration,
                   signingCredentials: signingCredentials
            );

            return token;
        }

        //VALIDATING USER CLAIMS OR IDENTITY
        private async Task<List<Claim>> GetClaims()
        {
            //Checking username
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName )
            };

            //Checking user roles
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            //Does the user exist in database
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            //if user is found check password too if its valid return user.
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));

        }
    }
}
