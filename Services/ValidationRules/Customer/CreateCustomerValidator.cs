using FluentValidation;
using Models.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationRules.Customer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerValidator()
        {
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
        }
    }
}
