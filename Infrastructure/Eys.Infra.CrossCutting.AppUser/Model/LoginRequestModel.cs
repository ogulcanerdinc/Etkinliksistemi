using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Model
{
	public class LoginRequestModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Beni Hatırla")] public bool RememberMe { get; set; } = false;
	}
}
