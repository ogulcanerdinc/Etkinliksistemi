using Eys.Domain.Models.DataTableModel;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Eys.Web.Cms.Controllers
{
    public class BaseController : Controller
    {

        protected string UserId { get; private set; }
		protected UserViewModel CurrentUser;
		public override void OnActionExecuting(ActionExecutingContext context)
        {
			if (User.Identity.IsAuthenticated)
			{
				string mail = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
				var x = HttpContext.RequestServices.GetRequiredService<IAppUserAccountService>();
				if (mail != null && mail != null)
				{
					var getUser = x.GetUserById(mail).Result;
					CurrentUser = getUser.Result;
					ViewBag.User = CurrentUser;
					UserId = getUser.Result.Id;
					TempData["UserId"] = UserId;
				}
			}
			else
			{

				context.Result = new RedirectResult("/Auth/Index");



			}
		}

        protected virtual DataTableBaseModel DTTableBaseModel(DTParameters dtParameters, string baseCriteria)
        {
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            if (dtParameters == null)
            {
                dtParameters = new DTParameters();
            }

            if (dtParameters != null && dtParameters.Order != null)
            {
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                orderCriteria = baseCriteria;
                orderAscendingDirection = true;
            }
            if (orderCriteria == null) orderCriteria = baseCriteria;

            return new DataTableBaseModel
            {
                Length = dtParameters.Length,
                Start = dtParameters.Start,
                OrderAscDirection = orderAscendingDirection,
                OrderCriteria = orderCriteria,
                SearchBy = dtParameters.Search?.Value,
            };
        }
    }
}
