using FluentValidation;
using Models.DTOs.DebitCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.DebitCard
{
    public class CreateDebitCardValidator : AbstractValidator<CreateDebitCardDto>
    {
        public CreateDebitCardValidator()
        {
            RuleFor(d => d.AccountId)
                .NotNull()
                .Must(d => d > 0);

            RuleFor(d => d.ExpirationDate)
                .NotNull();

            RuleFor(d => d.Balance)
                .NotNull()
                .Must(d => d >= 0);
        }
    }
}
