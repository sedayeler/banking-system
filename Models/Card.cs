using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Card : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string ExpirationDateString
        {
            get
            {
                return ExpirationDate.ToString("MM/yy");
            }
        }
        public string CCV { get; set; }
        public bool IsActive { get; set; } = true;
        public Account Account { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
