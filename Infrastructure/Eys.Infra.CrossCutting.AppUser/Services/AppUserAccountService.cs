using Eys.Domain.Helper;
using Eys.Domain.Models.Base;
using Eys.Infra.CrossCutting.AppUserIdentity.Data;
using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Services
{
    public class AppUserAccountService : IAppUserAccountService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AppUserDbContext _appUserDbContext;
        private readonly SignInManager<AppUser> _signInManager;


        public AppUserAccountService(UserManager<AppUser> userManager, AppUserDbContext appUserDbContext, SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._appUserDbContext = appUserDbContext;
            _signInManager = signInManager;
        }

        public async Task<ServiceResult<AppUser>> PasswordSignIn(LoginRequestModel loginRequest)
        {

            var response = new ServiceResult<AppUser>();

            if (response.Validation(new LoginRequestValidation().Validate(loginRequest)))
            {
                try
                {

                    var usermailcheck = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);
                    if (usermailcheck != null)
                    {
                        var checkPassword = await _userManager.CheckPasswordAsync(usermailcheck, loginRequest.Password);
                        if (checkPassword == true)
                        {
                            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: false);
                            if(result.Succeeded)
                            {
                                response.IsSuccess = true;
                                response.Message = "Giriş Başarılı";
                                var dataList = new List<string>();
                                var getUserRole = await _userManager.GetRolesAsync(usermailcheck);
                                dataList.Add(usermailcheck.Id.ToString());
                                dataList.Add(getUserRole[0].ToString());
                                response.Data = dataList;
                                response.Result = usermailcheck;
                                await _userManager.AddClaimAsync(usermailcheck, new Claim("Name", usermailcheck.Id));
                                await _userManager.AddClaimAsync(usermailcheck, new Claim("Role", getUserRole[0].ToString()));

                            }

                        }
                    }
                    else
                    {
                        response.Message = "Mail adresi veya şifre hatalı";
                    }

                }
                catch (Exception e)
                {
                    response.SetException(e);
                }



            }

            return response;
        }



        public async Task<ServiceResult<UserViewModel>> GetUserById(string Id)
        {
            var response = new ServiceResult<UserViewModel>();
            var model= await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(model != null)
            {
                var getUserRole = await _userManager.GetRolesAsync(model);
                response.Result = new UserViewModel
                {
                    Id = model.Id,
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.Surname,
                    Role = getUserRole.First().ToString()
                };
            }
            return response;
        }

        public async Task<ServiceResult<bool>> UserSignUp(SignUpRequestModel signUpRequest)
        {
            var response = new ServiceResult<bool>();
            if (response.Validation(new SignUpRequestValidation().Validate(signUpRequest)))
            {
                var usermailcheck = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == signUpRequest.Email);
                if (usermailcheck != null)
                {
                    response.Message = "E-mail Sistemde Mevcut";
                }
                else
                {
                    AppUser user = new AppUser { Email = signUpRequest.Email, Name = signUpRequest.Name, Surname = signUpRequest.SurName, UserName = signUpRequest.Email };
                    var createUser = await _userManager.CreateAsync(user, signUpRequest.Password);
                    if (createUser.Succeeded)
                    {

                        var currentUser = await _userManager.FindByNameAsync(user.UserName);
                        var roleresult = await _userManager.AddToRoleAsync(currentUser, UserRoles.User);
                        response.IsSuccess = createUser.Succeeded;
                        response.Result = true;
                        response.Message = "Kayıt Başarılı!";
                    }

                }
            }
            return response;
        }
        public async Task<ServiceResult<AppUser>> UserUpdate(SignUpRequestModel signUpRequest)
        {
            var response = new ServiceResult<AppUser>();
            if (response.Validation(new SignUpRequestValidation().Validate(signUpRequest)))
            {
                var usermailcheck = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == signUpRequest.Email);
                var userIdCheck = await _userManager.FindByIdAsync(signUpRequest.Id.ToString());
                if (usermailcheck != null && usermailcheck.Id != signUpRequest.Id)
                {
                    response.Message = "E-mail Sistemde Mevcut";
                }
                else if (usermailcheck != null)
                {
                    userIdCheck.Name = signUpRequest.Name;
                    userIdCheck.Surname = signUpRequest.SurName;
                    userIdCheck.Email = signUpRequest.Email;
                    userIdCheck.UserName = signUpRequest.Email;

                    var update = _userManager.UpdateAsync(userIdCheck);
                    if (update.Result.Succeeded)
                    {
                        var currentUser = await _userManager.FindByNameAsync(userIdCheck.UserName?.Trim());
                        var roleresult = await _userManager.AddToRoleAsync(currentUser, "User");
                        response.Message = "Kayıt Başarıyla Güncellendi.";
                        response.Data = userIdCheck.Id;
                        response.IsSuccess = true;
                    }
                }
                else
                {
                    response.Message = "Kayıt Bulunamadı.";
                }
            }

            return response;
        }

        public async Task<ServiceResult<bool>> PasswordChange(PasswordChangeRequestModel passwordChangeRequestModel)
        {
            var response = new ServiceResult<bool>();
            if (response.Validation(new PasswordChangeValidation().Validate(passwordChangeRequestModel)))
            {
                var userIdCheck = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == passwordChangeRequestModel.Id);
                if (userIdCheck != null)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(userIdCheck, passwordChangeRequestModel.Password, passwordChangeRequestModel.NewPassword);
                    if (changePassword.Succeeded)
                    {
                        var update = await _userManager.UpdateAsync(userIdCheck);
                        response.IsSuccess = update.Succeeded;
                        if (update.Succeeded)
                        {
                            response.Message = "Şifre Başarıyla Güncellendi";
                            response.IsSuccess = true;

                        }

                    }
                }
                else
                {
                    response.Message = "Kullanıcı Bulunamadı";
                }
            }
            return response;
        }

        public async Task<ServiceResult<AppUser>> AdminSignUp(SignUpRequestModel signUpRequest)
        {
            var response = new ServiceResult<AppUser>();
            if (response.Validation(new SignUpRequestValidation().Validate(signUpRequest)))
            {
                var usermailcheck = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == signUpRequest.Email);
                if (usermailcheck != null)
                {
                    response.Message = "E-mail Sistemde Mevcut";
                }
                else
                {
                    AppUser user = new AppUser { Email = signUpRequest.Email, Name = signUpRequest.Name, Surname = signUpRequest.SurName, UserName = signUpRequest.Email };
                    var createUser = await _userManager.CreateAsync(user, signUpRequest.Password);
                    if (createUser.Succeeded)
                    {

                        var currentUser = await _userManager.FindByNameAsync(user.UserName);
                        var roleresult = await _userManager.AddToRoleAsync(currentUser, UserRoles.Admin);
                        response.IsSuccess = createUser.Succeeded;
                        response.Message = "Kayıt Başarılı!";
                    }

                }
            }
            return response;
        }
    }
}
