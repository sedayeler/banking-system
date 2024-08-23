using FluentValidation;
using Models.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.Account
{
    public class CreatAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreatAccountValidator()
        {
            RuleFor(a => a.CustomerId)
                .NotNull()
                .Must(a => a > 0);

            RuleFor(a => a.AccountName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(a => a.Balance)
                .NotNull()
                .Must(a => a >= 0);
        }
    }
}
