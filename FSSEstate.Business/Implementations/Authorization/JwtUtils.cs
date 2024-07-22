using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Repository.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FSSEstate.Business.Implementations.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly string _appSettings;

        public JwtUtils(IConfiguration appSettings)
        {

            if (string.IsNullOrEmpty(appSettings.GetSection("JwtSecret")!.ToString()))
            {
                throw new Exception("JWT secret not configured");
            }
            else
            {
                _appSettings = appSettings.GetSection("JwtSecret").ToString();

            }

        }

        public string GenerateJwtToken(UserEntity user)
        {
            var identityClaims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim("Id", user.Id.ToString()),
                new Claim("FullName", user.Fullname),
            };
            var key = Encoding.ASCII.GetBytes(_appSettings);
            var keyCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                claims: identityClaims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserClaimModel> GetUserClaimsAsync(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var result = new UserClaimModel();
                result.Email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                result.PhoneNumber = jwtToken.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
                result.Id = jwtToken.Claims.First(x => x.Type == "Id").Value;
                result.FullName = jwtToken.Claims.First(x => x.Type == "FullName").Value;

                // return user id from JWT token if validation successful
                return result;
            }
            catch (Exception ex)
            {
                // return null if validation fails
                throw new Exception(ex.Message);
            }
        }
        public string? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var Email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                // return user id from JWT token if validation successful
                return Email;
            }
            catch (Exception ex)
            {
                // return null if validation fails
                throw new Exception(ex.Message);
            }
        }
    }
}
