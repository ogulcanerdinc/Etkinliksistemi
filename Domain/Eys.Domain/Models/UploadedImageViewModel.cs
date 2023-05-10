using Eys.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Models
{
    public class UploadedImageViewModel : BaseViewModel
    {
        public IFormFile File { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public bool IsNeedAuth { get; set; }
        public string ReferanceId { get; set; }
        public string ReferanceFunction { get; set; }
        public string ContentType { get; set; }
    }
}
