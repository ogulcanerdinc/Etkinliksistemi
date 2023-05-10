using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models.DataTableModel
{
    public class DataTableBaseModel
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderCriteria { get; set; } = string.Empty;
        public bool OrderAscDirection { get; set; } = true;
        public string SearchBy { get; set; }
    }

    public class DTJsonModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
    }
}
