using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Model
{
	public class SignUpRequestModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
		public string SurName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
