using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class BankingSystemContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=banking_system; Username=postgres; Password=12345");
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Account> accounts { get; set; }
        //public DbSet<Card> cards { get; set; }
        //public DbSet<DebitCard> debit_cards { get; set; }
        //public DbSet<CreditCard> credit_cards { get; set; }
        //public DbSet<Transaction> transactions { get; set; }
    }
}
