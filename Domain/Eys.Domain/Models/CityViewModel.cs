using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models
{
    public class CityViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } = true;
    }
}
