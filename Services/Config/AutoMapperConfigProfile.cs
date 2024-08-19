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
            CreateMap<Customer, CreateCustomerDto>();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<Customer, UpdateCustomerDto>();
            CreateMap<ListCustomerDto, Customer>();
            CreateMap<Customer, ListCustomerDto>();

            CreateMap<CreateAccountDto, Account>();
            CreateMap<Account, CreateAccountDto>();
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<Account, UpdateAccountDto>();
            CreateMap<ListAccountDto, Account>();
            CreateMap<Account, ListAccountDto>();

            CreateMap<CreateDebitCardDto, DebitCard>();
            CreateMap<DebitCard, CreateDebitCardDto>();
            CreateMap<UpdateDebitCardDto, DebitCard>();
            CreateMap<DebitCard, UpdateDebitCardDto>();
            CreateMap<ListDebitCardDto, DebitCard>();
            CreateMap<DebitCard, ListDebitCardDto>();

            CreateMap<CreateCreditCardDto, CreditCard>();
            CreateMap<CreditCard, CreateCreditCardDto>();
            CreateMap<UpdateCreditCardDto, CreditCard>();
            CreateMap<CreditCard, UpdateCreditCardDto>();
            CreateMap<ListCreditCardDto, CreditCard>();
            CreateMap<CreditCard, ListCreditCardDto>();

            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<Transaction, CreateTransactionDto>();
            CreateMap<UpdateTransactionDto, Transaction>();
            CreateMap<Transaction, UpdateTransactionDto>();
            CreateMap<ListTransactionDto, Transaction>();
            CreateMap<Transaction, ListTransactionDto>();
        }
    }
}
