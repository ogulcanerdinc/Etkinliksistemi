using Eys.Domain.Models.Base;
using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Services
{
    public interface ITokenService
    {
        ServiceResult<UserTokenModel> GenerateUserToken(AppUser appUser, string jwtKey, string jwtIssuer);
    }
}
