using Eys.Domain.Helper;
using Eys.Domain.Models.Base;
using Eys.Domain.Models;
using Eys.Domain.Services.Impl.Services;
using Eys.Domain.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Eys.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IEventTicketsService _eventTicketsService;
        public TicketController(IEventTicketsService eventTicketsService)
        {
            _eventTicketsService = eventTicketsService;
        }

        [HttpPost("TicketCheck")]
        [SwaggerResponse(200, "Bilet Bilgileri Doğru")]
        [SwaggerResponse(400, "Hatalı Bilet Bilgileri")]
        public async Task<IActionResult> CheckTicket(string TicketNumber)
        {
            var result = new ServiceResult<bool>();
            result = await _eventTicketsService.CheckTicket(TicketNumber);


            return result.HttpPostResponse();
        }
    }
}
