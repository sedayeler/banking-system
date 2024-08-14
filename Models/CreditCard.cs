using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreditCard : Card
    {
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
    }
}
