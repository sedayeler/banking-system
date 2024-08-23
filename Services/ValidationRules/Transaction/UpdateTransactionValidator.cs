using FluentValidation;
using Models.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.Transaction
{
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionDto>
    {
        public UpdateTransactionValidator()
        {
            RuleFor(t => t.Id)
                .NotNull()
                .Must(t => t > 0);

            RuleFor(t => t.DebitCardId)
                .Must(t => t == null || t > 0);

            RuleFor(t => t.CreditCardId)
                .Must(t => t == null || t > 0);

            RuleFor(t => t.Description)
                .MaximumLength(100);

            RuleFor(t => t.Amount)
                .NotNull()
                .Must(t => t > 0);

            RuleFor(t => t.TransactionType)
                .NotNull()
                .NotEmpty();
        }
    }
}
