using Eys.Domain.Models.Base;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models
{
    public class EventsViewModel:BaseViewModel
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventShortDescription { get; set; }
        public string EventRules { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int Quota { get; set; }
		public Guid CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CategoryViewModel> CategoryList { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<CityViewModel> CityList { get; set; }
		public EventImages EventImage { get; set; }
        public List<EventImages> EventImages { get; set; } = new List<EventImages>();
        #region EventLocation
        public City City { get; set; }
        public int CityId { get; set; }
        //public City City { get; set; }
        public string EventAdress { get; set; }
        //Enlem
        public string Latitude { get; set; }
        //Boylam
        public string Longitude { get; set; }

        #endregion
        #region EventImages

        //public virtual List<EventImages> EventImages { get; set; }
        #endregion

    }
}
