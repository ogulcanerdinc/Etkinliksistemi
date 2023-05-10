using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models;
using Eys.Domain.Services.Services;
using Eys.Domain.Validations;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Eys.Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Eys.Web.Cms.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class EventsController : BaseController
    {
        private readonly IEventsService _EventsService;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly ICityService _cityService;
		public EventsController(IEventsService EventsService, ICategoryService categoryService, IFileService fileService, ICityService cityService)
		{
			_EventsService = EventsService;
			_categoryService = categoryService;
			_fileService = fileService;
			_cityService = cityService;
		}

		#region List
		[HttpGet]
        //[UserPermission("Events.List")]
        public async Task<ActionResult> EventsList()
        {
            var model = new List<EventsViewModel>();
            TempData["Title"] = "Etkinlikler";
            return View("_GetEventsList", model);
        }
        [HttpPost]
        public JsonResult EventsListQuery([FromBody] EventsDTParameter dtParameters)
        {
            try
            {
                var baseModel = DTTableBaseModel(dtParameters, "status");
                var data = _EventsService.GetAllForDatatables(baseModel, dtParameters);
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
        public async Task<IActionResult> EventsEdit(Guid Id)
        {
            var model = new EventsViewModel();

            model = Id == Guid.Empty ? new EventsViewModel
            {
                IsActive = true,
                Id = new Guid()
            } : await _EventsService.GetEventsById(Id);
            model.CategoryList = await _categoryService.GetAll();
            model.CityList=await _cityService.GetAll();
            return View("_GetEvents", model);

        }
        [HttpPost]
        public async Task<JsonResult> EventsEdit(EventsViewModel model)
        {
            var result = new ServiceResult<Events>();
            if (result.Validation(new EventsValidation().Validate(model)))
            {
                if (model.Id == Guid.Empty)
                {
                    result = await _EventsService.Add(model);
                }
                else
                {
                    result = await _EventsService.Update(model);
                }
            }

            return Json(result);
        }
		#endregion

		#region Delete
		[HttpPost]
        public async Task<JsonResult> EventsDelete(Guid Id)
        {
            var result = new ServiceResult<bool>();
            result = await _EventsService.Delete(Id,"");
            return Json(result);
		}
		[HttpPost]
		public async Task<JsonResult> EventsCancelled(Guid Id)
		{
			var result = new ServiceResult<bool>();
			result = await _EventsService.EventCancelled(Id, "");
			return Json(result);
		}
		[HttpPost]
        public async Task<JsonResult> EventsImage(Guid Id)
        {
            var result = new ServiceResult<bool>();
            result = await _fileService.DeleteImage(Id);
            return Json(result);
        }

        #endregion
    }
}
