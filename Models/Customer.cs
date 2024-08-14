using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalityId { get; set; }
        public string BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }
        public decimal RiskLimit { get; set; } = 10000m;
        public ICollection<Account> Accounts { get; set; }
    }
}
