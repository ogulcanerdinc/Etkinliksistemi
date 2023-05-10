using Eys.Domain.Models.Base;
using Eys.Domain.Models;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Services.Impl.Services;
using Eys.Domain.Services.Services;
using Eys.Domain.Validations;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eys.Domain.Helper;

namespace Eys.Web.Main.Controllers
{
	public class EventsController : BaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IEventsService _eventsService;
		private readonly ICityService _cityService;
		private readonly IEventTicketsService _eventTicketsService;
		public EventsController(ICategoryService categoryService, IEventsService eventsService, ICityService cityService, IEventTicketsService eventTicketsService)
		{
			_categoryService = categoryService;
			_eventsService = eventsService;
			_cityService = cityService;
			_eventTicketsService = eventTicketsService;
		}

		public IActionResult Index()
		{
			return View();
        }
        #region Edit
        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> EventsEdit(Guid Id)
        {
            var model = new EventsViewModel();

            model = Id == Guid.Empty ? new EventsViewModel
            {
                IsActive = true,
                Id = new Guid()
            } : await _eventsService.GetEventsById(Id);
            model.CategoryList = await _categoryService.GetAll();
            model.CityList = await _cityService.GetAll();
            return View("_GetEvents", model);

        }
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<JsonResult> EventsEdit(EventsViewModel model)
        {
            var result = new ServiceResult<Events>();
            if (result.Validation(new EventsValidation().Validate(model)))
            {
                if (model.Id == Guid.Empty)
                {
                    result = await _eventsService.Add(model);
                }
                else
                {
                    result = await _eventsService.Update(model);
                }
            }

            return Json(result);
        }
        #endregion
        [Route("kategori")]
		public async Task<IActionResult> CategoryEventList()
		{
			//ViewData["canonicalLink"] = $"https://wingrupseydisehir.com/urunler/{slug}";
			//var serviceResponse = _categoryService.GetCategoryBySlug(slug);
			//ViewData["Title"] = serviceResponse.Result.CategoryName;
			//if (serviceResponse.Result.Id == null)
			//	return NotFound();
			//ViewBag.CategoryId=serviceResponse.Result.Id;
			return View();
        }
        [HttpPost]
        public JsonResult EventsUserListQuery([FromBody] EventsDTParameter dtParameters)
        {
            try
            {
                //var serviceResponse = _categoryService.GetCategoryBySlug(dtParameters.CategoryName);
                //dtParameters.CategoryId= serviceResponse.Result.Id;
                dtParameters.UserId = UserId;
                var baseModel = DTTableBaseModel(dtParameters, "status");
                var data = _eventsService.GetAllForDatatables(baseModel, dtParameters);
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
        [HttpPost]
		public JsonResult EventsListQuery([FromBody] EventsDTParameter dtParameters)
		{
			try
			{
				//var serviceResponse = _categoryService.GetCategoryBySlug(dtParameters.CategoryName);
				//dtParameters.CategoryId= serviceResponse.Result.Id;
				var baseModel = DTTableBaseModel(dtParameters, "status");
				var data = _eventsService.GetAllForDatatables(baseModel, dtParameters);
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
        public async Task<IActionResult> EventDetail(Guid Id)
        {
			
            var serviceResponse = await _eventsService.GetEventsById(Id);
			if(serviceResponse.Id!=Guid.Empty)
				return View(serviceResponse);
			return BadRequest();
		}
		[HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<JsonResult> BuyTicket(EventTicketsViewModel model)
		{
			var result = new ServiceResult<bool>();
			model.UserId = UserId;
			result = await _eventTicketsService.BuyTicket(model);


			return Json(result);
		}
	}
}
