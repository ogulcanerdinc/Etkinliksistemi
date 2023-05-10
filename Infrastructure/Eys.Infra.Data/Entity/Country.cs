using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class City
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } = true;
    }
    //public class District
    //{
    //    [Key]
    //    public int id { get; set; }
    //    public string DistrictName { get; set; }
    //    public string Cityid { get; set; }
    //}
}
