using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.DebitCard
{
    public class ListDebitCardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CCV { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
