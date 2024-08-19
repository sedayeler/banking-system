using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.CreditCard
{
    public class ListCreditCardDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CCV { get; set; }
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
        public bool IsActive { get; set; }
    }
}
