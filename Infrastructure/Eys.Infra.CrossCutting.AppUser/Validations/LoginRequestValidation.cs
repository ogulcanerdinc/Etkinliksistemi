using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Validations
{
	public class LoginRequestValidation : AbstractValidator<LoginRequestModel>
	{
		public LoginRequestValidation()
		{
			RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş olamaz.");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz.");
		}
	}
}
