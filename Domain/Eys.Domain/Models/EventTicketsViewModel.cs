using Eys.Domain.Models.Base;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models
{
	public class EventTicketsViewModel:BaseViewModel
	{
		public Guid? EventsId { get; set; }
		public EventsViewModel Events { get; set; }
		public string UserId { get; set; }
        public string TicketNumber { get; set; }
    }
}
