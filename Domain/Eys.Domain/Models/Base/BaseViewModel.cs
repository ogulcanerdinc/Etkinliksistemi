using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eys.Domain.Models.Base
{
    public class BaseViewModel
    {
        public Guid? Id { get; set; }
        public DateTime DataCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }
        public Guid UpdatedById { get; set; }
    }
}
