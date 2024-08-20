using AutoMapper;
using Models;
using Models.DTOs.Account;
using Models.DTOs.CreditCard;
using Models.DTOs.Customer;
using Models.DTOs.DebitCard;
using Models.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Config
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<Customer, ListCustomerDto>();

            CreateMap<CreateAccountDto, Account>();
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<Account, ListAccountDto>();

            CreateMap<CreateDebitCardDto, DebitCard>();
            CreateMap<UpdateDebitCardDto, DebitCard>();
            CreateMap<DebitCard, ListDebitCardDto>();

            CreateMap<CreateCreditCardDto, CreditCard>();
            CreateMap<UpdateCreditCardDto, CreditCard>();
            CreateMap<CreditCard, ListCreditCardDto>();

            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, Transaction>();
            CreateMap<Transaction, ListTransactionDto>();
        }
    }
}
