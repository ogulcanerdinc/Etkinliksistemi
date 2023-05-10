using Eys.Domain.Models;
using Eys.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Validations
{
    public class EventsValidation : AbstractValidator<EventsViewModel>
    {

        public EventsValidation()
        {
            RuleFor(x => x.EventName).NotEmpty().WithMessage("Etkinlik Adı Boş Bırakılamaz");
            RuleFor(x => x.Quota).NotEmpty()
                    .WithMessage(
                    "Kota 0 dan büyük olmalıdır.");
            RuleFor(x => x.EventStartDate).NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");
            RuleFor(x => x.EventEndDate).NotEmpty().WithMessage("Bitiş tarihi boş olamaz.");

        }
    }
}
