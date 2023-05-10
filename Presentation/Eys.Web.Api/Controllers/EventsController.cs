using Eys.Domain.Models.FilterModel;
using Eys.Domain.Services.Impl.Services;
using Eys.Domain.Services.Services;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eys.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpPost("EventList")]
        [SwaggerResponse(200, "Etkinlik Listesi")]
        public async Task<IActionResult> GetEventList(EventFilterModel model)
        {
            var response = await _eventsService.GetAllByFilterModel(model);

            return response.HttpPostResponse();
        }
    }
}
