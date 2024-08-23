using FluentValidation;
using Models.DTOs.CreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.CreditCard
{
    public class CreateCreditCardValidator : AbstractValidator<CreateCreditCardDto>
    {
        public CreateCreditCardValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotNull()
                .Must(c => c > 0);

            RuleFor(c => c.ExpirationDate)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Limit)
                .NotNull()
                .Must(c => c > 0);
        }
    }
}
