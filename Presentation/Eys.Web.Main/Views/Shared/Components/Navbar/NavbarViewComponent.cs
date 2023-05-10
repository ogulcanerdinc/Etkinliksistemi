using Eys.Domain.Models;
using Eys.Domain.Services.Impl.Services;
using Eys.Domain.Services.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eys.Web.Main.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public NavbarViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            if(UserClaimsPrincipal.Identity.IsAuthenticated)
            {
                ViewBag.UserId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            }
            ViewBag.Categories = _categoryService.GetAll().Result
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                }).ToList();
            return View();
        }
    }
}
