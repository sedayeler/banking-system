using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ListAccountDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
