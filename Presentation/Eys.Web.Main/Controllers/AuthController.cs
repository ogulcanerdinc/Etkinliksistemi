using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Security.Claims;

namespace Eys.Web.Main.Controllers
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
        public IActionResult Login()
        {
            return View();
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
        //private async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAndAddClaims(AppUser appUser, LoginRequestModel model)
        //{
        //    //TODO: claimler eklenmeden önce hali hazırda eklenmiş mi kontrol edilecek, varsa güncellenecek
        //    //TODO: password şifrelenecek o şekilde eklenecek

        //    var appUserClaims = await _userManager.GetClaimsAsync(appUser);
        //    IdentityResult result = await _userManager.RemoveClaimsAsync(appUser, appUserClaims);

        //    return _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, model.RememberMe, false).Result;
        //}
    }
}
