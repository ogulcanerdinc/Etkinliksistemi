using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models.DataTableModel
{
    public class CategoryDTParameter : DTParameters
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
