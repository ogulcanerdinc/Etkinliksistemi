using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models.FilterModel
{
    public class EventFilterModel
    {
        public string? UserId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? EventName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int? CityId { get; set; }
    }
}
