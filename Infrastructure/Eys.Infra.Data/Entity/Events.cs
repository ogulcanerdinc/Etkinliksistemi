using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class Events: BaseEntityWithDate
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventShortDescription { get; set; }
        public string EventRules { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int Quota { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsCancelled { get; set; } = false;

        #region EventLocation
        public int CityId { get; set; }
        public City City { get; set; }
        public string EventAdress { get; set; }
        //Enlem
        public string Latitude { get; set; }
        //Boylam
        public string Longitude { get; set; }

        #endregion
        #region EventImages

        public virtual List<EventImages> EventImages { get; set; }
        #endregion


    }
}
