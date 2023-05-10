using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class EventImages:BaseEntityWithDate
    {
        public Guid? UploadedImageId { get; set; }
        public UploadedImage UploadedImage { get; set; }
        public int Order { get; set; }
    }
}
