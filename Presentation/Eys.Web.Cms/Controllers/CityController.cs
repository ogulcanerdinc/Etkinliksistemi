using Eys.Domain.Helper;
using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Eys.Web.Cms.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CityController : BaseController
    {
        private readonly ICityService _CityService;

        public CityController(ICityService CityService)
        {
            _CityService = CityService;
        }

        #region List
        [HttpGet]
        //[UserPermission("City.List")]
        public async Task<ActionResult> CityList()
        {
            var model = new List<CityViewModel>();
            TempData["Title"] = "Şehirler";
            return View("_GetCityList", model);
        }
        [HttpPost]
        public JsonResult CityListQuery([FromBody] CityDTParameter dtParameters)
        {
            try
            {
                var baseModel = DTTableBaseModel(dtParameters, "status");
                var data = _CityService.GetAllForDatatables(baseModel, dtParameters);
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
        public async Task<IActionResult> CityEdit(int Id)
        {
            var model = new CityViewModel();

            model = Id == 0 ? new CityViewModel
            {
                id = 0
            } : await _CityService.GetCityById(Id);
            return View("_GetCity", model);

        }
        [HttpPost]
        public async Task<JsonResult> CityEdit(City model)
        {
            var result = new ServiceResult<City>();
           
                if (model.id == 0)
                {
                    result = await _CityService.Add(model);
                }
                else
                {
                    result = await _CityService.Update(model);
                }
            

            return Json(result);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> CityDelete(int Id)
        {
            var result = new ServiceResult<bool>();
            result = await _CityService.Delete(Id);
            return Json(result);
        }

        #endregion
    }
}
