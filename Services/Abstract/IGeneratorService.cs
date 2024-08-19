using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IGeneratorService
    {
        public string GenerateAccountNumber();
        public string GenerateIBAN();
        public string GenerateCardNumber();
        public string GenerateCCV();
    }
}
