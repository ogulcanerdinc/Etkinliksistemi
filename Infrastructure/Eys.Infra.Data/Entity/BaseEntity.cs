using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
    public abstract class BaseEntityWithDate : BaseEntity
    {
        [JsonIgnore]
        public bool IsDeletable { get; set; } = true;
        //[HiddenInput(DisplayValue = false)]
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        //[HiddenInput(DisplayValue = false)]
        [JsonIgnore]
        public DateTime? DateModified { get; set; }
        [JsonIgnore]
        public Guid AddedById { get; set; }
        [JsonIgnore]
        public Guid UpdatedById { get; set; }

    }
    public abstract class BaseEntityOnlyId
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? DateCreated { get; set; }

    }
}
