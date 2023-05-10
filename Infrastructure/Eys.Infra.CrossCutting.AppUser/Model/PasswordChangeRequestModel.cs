using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Model
{
    public class PasswordChangeRequestModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
