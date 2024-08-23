using AutoMapper;
using Core.CrossCuttingConcerns.Validation;
using Core.Models;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Customer;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.ValidationRules.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly BankingSystemContext _context;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, BankingSystemContext context, IMapper mapper)
        {
            _customerDal = customerDal;
            _context = context;
            _mapper = mapper;
        }

        public IResult Add(CreateCustomerDto dto)
        {
            ValidationTool.Validate(new CreateCustomerValidator(), dto);

            var customer = _context.customers.Any(c => c.NationalId == dto.NationalId);
            if (customer)
            {
                return new ErrorResult("National ID number is already in use.");
            }
            
            Customer newCustomer = _mapper.Map<Customer>(dto);
            _customerDal.Add(newCustomer);
            return new SuccessResult("Customer added.");
        }

        public IResult Update(UpdateCustomerDto dto)
        {
            ValidationTool.Validate(new UpdateCustomerValidator(), dto);

            var customer = _customerDal.Get(c => c.Id == dto.Id);
            if (customer == null)
            {
                return new ErrorResult("Customer not found.");
            }

            Customer updateCustomer = _mapper.Map(dto, customer);
            _customerDal.Update(updateCustomer);
            return new SuccessResult("Customer updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }

            var customer = _customerDal.Get(c => c.Id == id);
            if (customer == null)
            {
                return new ErrorResult("Customer not found.");
            }

            _customerDal.Delete(customer);
            return new SuccessResult("Customer deleted.");
        }

        public IDataResult<ListCustomerDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListCustomerDto>("Invalid id.");
            }

            var customer = _customerDal.Get(c => c.Id == id);
            if (customer == null)
            {
                return new ErrorDataResult<ListCustomerDto>("Customer not found.");
            }

            var listCustomer = _mapper.Map<ListCustomerDto>(customer);
            return new SuccessDataResult<ListCustomerDto>(listCustomer, "Customer listed.");
        }

        public IDataResult<List<ListCustomerDto>> GetAll()
        {
            var customers = _customerDal.GetAll();
            var listCustomers = _mapper.Map<List<ListCustomerDto>>(customers);
            return new SuccessDataResult<List<ListCustomerDto>>(listCustomers, "Customers listed.");
        }
    }
}
