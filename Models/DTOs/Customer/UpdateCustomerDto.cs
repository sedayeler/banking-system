using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }
        public string BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }
        public decimal RiskLimit { get; set; }
    }
}
