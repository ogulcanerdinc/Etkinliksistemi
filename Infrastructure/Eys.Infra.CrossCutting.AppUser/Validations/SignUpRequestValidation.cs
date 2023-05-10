using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Validations
{
    public class SignUpRequestValidation : AbstractValidator<SignUpRequestModel>
    {
        public SignUpRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş olamaz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyad boş olamaz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz.");
        }
    }
}
