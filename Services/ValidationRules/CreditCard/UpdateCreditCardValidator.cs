using FluentValidation;
using Models.DTOs.CreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.CreditCard
{
    public class UpdateCreditCardValidator : AbstractValidator<UpdateCreditCardDto>
    {
        public UpdateCreditCardValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .Must(c => c > 0);

            RuleFor(c => c.CustomerId)
                .NotNull()
                .Must(c => c > 0);

            RuleFor(c => c.Limit)
                .NotNull()
                .Must(c => c > 0); 

            RuleFor(c => c.Debt)
                .NotNull()
                .Must(c => c >= 0);

            RuleFor(c => c.IsActive)
                .NotNull();
        }
    }
}
