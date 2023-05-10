using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Validations
{
    public class PasswordChangeValidation : AbstractValidator<PasswordChangeRequestModel>
    {
        public PasswordChangeValidation()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Yeni Şifre boş olamaz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş olamaz.");
        }
    }
}
