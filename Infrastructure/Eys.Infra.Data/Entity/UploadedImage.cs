using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class UploadedImage : BaseEntityWithDate
    {
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public bool IsNeedAuth { get; set; }
        public string? ReferanceId { get; set; }
        public string? ReferanceFunction { get; set; }
        public string ContentType { get; set; }
    }
}
