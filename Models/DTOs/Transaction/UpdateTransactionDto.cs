using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Transaction
{
    public class UpdateTransactionDto
    {
        public int Id { get; set; }
        public int? DebitCardId { get; set; }
        public int? CreditCardId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public CardType CardType { get; set; }
        public TransactionType Type { get; set; }
    }
}
