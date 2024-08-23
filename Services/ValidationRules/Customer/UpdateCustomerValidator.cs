using FluentValidation;
using Models.DTOs.Account;
using Models.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.Customer
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .Must(a => a > 0);

            RuleFor(c => c.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(c => c.NationalId)
                .NotNull()
                .NotEmpty()
                .Length(11);

            RuleFor(c => c.BirthPlace)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.BirthDate)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.RiskLimit)
                .NotNull()
                .Must(a => a > 0);
        }
    }
}
