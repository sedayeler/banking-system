using Microsoft.EntityFrameworkCore;
using Repositories.Concrete;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class GeneratorManager : IGeneratorService
    {
        private readonly BankingSystemContext _context;

        public GeneratorManager(BankingSystemContext context)
        {
            _context = context;
        }

        Random random = new Random();
        public string GenerateAccountNumber()
        {
            string accountNumber;

            do
            {
                accountNumber = string.Empty;

                for (int i = 0; i < 16; i++)
                {
                    accountNumber += random.Next(0, 10).ToString();
                }
            }
            while (_context.accounts.Any(c => c.AccountNumber == accountNumber));

            return accountNumber;
        }

        public string GenerateIBAN()
        {
            string IBAN;

            do
            {
                IBAN = "TR";

                for (int i = 0; i < 24; i++)
                {
                    IBAN += random.Next(0, 10).ToString();
                }
            }
            while (_context.accounts.Any(c => c.IBAN == IBAN));

            return IBAN;
        }

        public string GenerateCardNumber()
        {
            string cardNumber;

            do
            {
                cardNumber = string.Empty;

                for (int i = 0; i < 16; i++)
                {
                    cardNumber += random.Next(0, 10).ToString();
                }
            }
            while (_context.debit_cards.Any(c => c.CardNumber == cardNumber));

            return cardNumber;
        }

        public string GenerateCCV()
        {          
            string CCV = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                CCV += random.Next(0, 10).ToString();
            }

            return CCV;
        }

    }
}
