using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
	public class EventTickets:BaseEntityWithDate
	{
		public Guid? EventsId { get; set; }
		public Events Events { get; set; }
        public string UserId { get; set; }
		public string TicketNumber { get; set; }

	}
}
