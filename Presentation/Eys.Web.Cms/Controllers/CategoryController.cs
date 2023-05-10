using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models;
using Eys.Domain.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System;
using Eys.Domain.Models.Base;
using Eys.Domain.Validations;
using Eys.Infra.Data.Entity;
using Eys.Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Eys.Web.Cms.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region List
        [HttpGet]
        //[UserPermission("Category.List")]
        public async Task<ActionResult> CategoryList()
        {
            var model = new List<CategoryViewModel>();
            TempData["Title"] = "Kategoriler";
            return View("_GetCategoryList", model);
        }
        [HttpPost]
        public JsonResult CategoryListQuery([FromBody] CategoryDTParameter dtParameters)
        {
            try
            {
                var baseModel = DTTableBaseModel(dtParameters, "status");
                var data = _categoryService.GetAllForDatatables(baseModel, dtParameters);
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsFiltered = data.total,
                    recordsTotal = data.list.Count(),
                    data = data.list,
                });
            }
            catch (Exception e)
            {
                return Json("");
            }
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> CategoryEdit(Guid Id)
        {
            var model = new CategoryViewModel();

            model = Id == Guid.Empty ? new CategoryViewModel
            {
                IsActive = true,
                Id = new Guid()
            } : await _categoryService.GetCategoryById(Id);
            return View("_GetCategory", model);

        }
        [HttpPost]
        public async Task<JsonResult> CategoryEdit(Category model)
        {
            var result = new ServiceResult<Category>();
            if (result.Validation(new CategoryValidation().Validate(model)))
            {
                if (model.Id == Guid.Empty)
                {
                    result = await _categoryService.Add(model);
                }
                else
                {
                    result = await _categoryService.Update(model);
                }
            }

            return Json(result);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> CategoryDelete(Guid Id)
        {
            var result = new ServiceResult<bool>();
            result = await _categoryService.Delete(Id);
            return Json(result);
        }

        #endregion
    }
}

