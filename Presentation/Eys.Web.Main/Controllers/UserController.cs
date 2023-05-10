using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eys.Web.Main.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IAppUserAccountService _appUserAccountService;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(IAppUserAccountService appUserAccountService, SignInManager<AppUser> signInManager)
        {
            _appUserAccountService = appUserAccountService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UserUpdate(SignUpRequestModel model)
        {
            if (model.Id != UserId)
            {
                return Json(new { Success = false });
            }
            var response = await _appUserAccountService.UserUpdate(model);
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> ChangePassword(PasswordChangeRequestModel model)
        {
            if (model.Id != UserId)
            {
                return Json(new { Success = false });
            }
            var response = await _appUserAccountService.PasswordChange(model);
            return Json(response);
        }
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
