using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Database;
using Eys.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Impl.Services
{
	public class EventTicketsService : IEventTicketsService
	{
		private readonly EysBaseContext context;
		public EventTicketsService(EysBaseContext context)
		{
			this.context = context;
		}
		public async Task<ServiceResult<bool>> BuyTicket(EventTicketsViewModel model)
		{
			var response=new ServiceResult<bool>();
			var checkEvent = await context.Events.FirstOrDefaultAsync(x => x.Id == model.EventsId);
			if(checkEvent != null)
			{
				var chckTicket = context.EventTickets.Count(x => x.EventsId == model.EventsId && x.UserId == model.UserId);
				if(chckTicket<2) //Etkinlik oluşturulurken alınabilecek max kota sorulup kayıt edilebilir.
				{
					if (checkEvent.Quota > 0)
					{

						var addModel = new EventTickets
						{
							EventsId = model.EventsId,
							UserId = model.UserId,
							TicketNumber=model.EventsId.ToString()+ "-" + RandomTicketNumber(),
							
						};
						await context.EventTickets.AddAsync(addModel);
						checkEvent.Quota=checkEvent.Quota-1;
						context.Update(checkEvent);
						var SaveResult = await context.SaveChangesAsync();
						if (SaveResult > 0)
						{
							response.IsSuccess = true;
							response.Message = "Bilet Alma İşlemi Başarılı.";

						}
					}
					else
					{
						response.IsSuccess = false;
						response.Message = "Bilet Alma İşlemi Başarısız. Yeterli Kota yok.";
					}

				}
				else
				{
					response.IsSuccess = false;
					response.Message = "Çok Fazla Bilet Aldınız.";

				}
			}
			else
			{
				response.IsSuccess = false;
				response.Message = "Etkinlik bulunamadı.";
			}
			return response;
		}

		public async Task<ServiceResult<bool>> CheckTicket(string ticketNumber)
		{
			var response = new ServiceResult<bool>();
			var chckTicket = await context.EventTickets.FirstOrDefaultAsync(x => x.TicketNumber == ticketNumber);
			if(chckTicket != null)
			{
				response.IsSuccess = true;
				response.Message = "Bilet Mevcut";
				response.Result= true;
			}
			else
			{
				response.Message = "Bilet Bulunamadı";
			}

			return response;
		}

		public string RandomTicketNumber()
		{
			Random rand = new Random();
			string TicketNumber = rand.Next(1, 9999999).ToString();
			return TicketNumber;
		}
	}
}
