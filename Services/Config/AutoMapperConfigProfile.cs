using AutoMapper;
using Models;
using Models.DTOs;
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
        }
    }
}
