using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CCV { get; set; }
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
        public bool IsActive { get; set; } 
        public Customer customer;
        public ICollection<Transaction> Transactions { get; set; }  
    }
}
