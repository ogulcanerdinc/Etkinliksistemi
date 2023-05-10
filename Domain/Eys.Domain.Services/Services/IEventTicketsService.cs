using Eys.Domain.Models.Base;
using Eys.Domain.Models;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Services
{
	public interface IEventTicketsService
	{
		Task<ServiceResult<bool>> BuyTicket(EventTicketsViewModel model);
		Task<ServiceResult<bool>> CheckTicket(string ticketNumber);
	}
}
