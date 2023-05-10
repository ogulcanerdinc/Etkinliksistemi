using Eys.Domain.Models.Base;
using Eys.Infra.CrossCutting.AppUserIdentity.Data;
using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Services
{

    public class TokenService : ITokenService
    {
        private readonly AppUserDbContext appIdentityDbContext;

        public TokenService(AppUserDbContext appIdentityDbContext)
        {
            this.appIdentityDbContext = appIdentityDbContext;
        }

        public ServiceResult<UserTokenModel> GenerateUserToken(AppUser appUser, string jwtKey, string jwtIssuer)
        {
            var response = new ServiceResult<UserTokenModel>();

            response.Result = new UserTokenModel(this.GenerateAccessToken(appUser, jwtKey, jwtIssuer));

            return response;
        }

        private string GenerateAccessToken(AppUser appUser, string jwtKey, string jwtIssuer)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtIssuer,
                jwtIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
       

    }
}
