using Core.Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public int? DebitCardId { get; set; }
        public int? CreditCardId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string CardType { get; set; }
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }
        public DebitCard DebitCard { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
