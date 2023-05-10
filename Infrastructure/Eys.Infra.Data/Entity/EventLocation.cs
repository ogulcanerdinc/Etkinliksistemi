using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class EventLocation : BaseEntityWithDate
    {
        public string EventLocationName { get; set; }
        public string EventLocationDescription { get; set;}
        public string EventLocationAdress { get; set;}
        //Enlem
        public string Latitude { get; set; }
        //Boylam
        public string Longitude { get; set; }
    }
}
