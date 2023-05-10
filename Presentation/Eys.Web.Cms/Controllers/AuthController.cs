using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Eys.Infra.CrossCutting.AppUserIdentity.Entity;

namespace Eys.Web.Cms.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAppUserAccountService _appUserAccountService;
        private readonly UserManager<AppUser> _userManager;


        public AuthController(IAppUserAccountService appUserAccountService, UserManager<AppUser> userManager)
        {
            _appUserAccountService = appUserAccountService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            CreateUser();
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SignUp(SignUpRequestModel model)
        {
            var response = await _appUserAccountService.UserSignUp(model);
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginRequestModel model)
        {
            var response = await _appUserAccountService.PasswordSignIn(model);
            if (response.IsSuccess)
            {
                var getData = (List<string>)response.Data;
                GetClaim(getData[0].ToString(), getData[1].ToString());
            }
            return Json(response);
        }

        public void GetClaim(string authUser, string Role)
        {
            try
            {

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, authUser)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
            }
            catch (Exception e)
            {

                throw;
            }
            return;
        }

        [HttpPost]
        private async Task<bool> CreateUser()
        {
            try
            {
                var model =(new SignUpRequestModel
                {
                    Email = "admin@test",
                    Name = "Ogulcan",
                    SurName = "Erdinc",
                    Password = "123Asd!!"
                });

                var response = await _appUserAccountService.AdminSignUp(model);

                return true;
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
