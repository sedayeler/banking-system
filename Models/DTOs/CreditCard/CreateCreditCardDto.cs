using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.CreditCard
{
    public class CreateCreditCardDto
    {
        public int CustomerId { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public decimal Limit { get; set; }
    }
}
