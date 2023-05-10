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
	public interface IAppUserAccountService
	{
		Task<ServiceResult<AppUser>> PasswordSignIn(LoginRequestModel loginRequest);
		Task<ServiceResult<bool>> UserSignUp(SignUpRequestModel signUpRequest);
        Task<ServiceResult<AppUser>> AdminSignUp(SignUpRequestModel signUpRequest);
        Task<ServiceResult<AppUser>> UserUpdate(SignUpRequestModel signUpRequest);
		//ServiceResult<AppUser> GetUserByUsername(string username);
		Task<ServiceResult<bool>> PasswordChange(PasswordChangeRequestModel passwordChangeRequestModel);
        Task<ServiceResult<UserViewModel>> GetUserById(string Id);
	}
}
