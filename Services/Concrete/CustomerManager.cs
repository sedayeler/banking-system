using AutoMapper;
using Core.Models;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Customer;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
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
            if (dto.FullName.Length > 100)
            {
                return new ErrorResult("The full name field cannot exceed 100 characters.");
            }
            var existingCustomer = _context.customers.Any(c => c.NationalId == dto.NationalId);
            if (existingCustomer)
            {
                return new ErrorResult("This national ID number is already in use.");
            }
            if (dto.NationalId.Length != 11)
            {
                return new ErrorResult("The national ID number must be 11 characters long.");
            }
            Customer newCustomer = _mapper.Map<Customer>(dto);
            _customerDal.Add(newCustomer);
            return new SuccessResult("Customer added.");
        }

        public IResult Update(UpdateCustomerDto dto)
        {
            if (dto.Id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingCustomer = _customerDal.Get(c => c.Id == dto.Id);
            if (existingCustomer == null)
            {
                return new ErrorResult("Customer not found.");
            }
            if (dto.NationalId.Length != 11)
            {
                return new ErrorResult("The national ID number must be 11 characters long.");
            }
            if (dto.RiskLimit <= 0)
            {
                return new ErrorResult("The risk limit cannot be equal to or less than 0.");
            }
            Customer updateCustomer = _mapper.Map(dto, existingCustomer);
            _customerDal.Update(updateCustomer);
            return new SuccessResult("Customer updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingCustomer = _customerDal.Get(c => c.Id == id);
            if (existingCustomer == null)
            {
                return new ErrorResult("Customer not found.");
            }
            _customerDal.Delete(existingCustomer);
            return new SuccessResult("Customer deleted.");
        }

        public IDataResult<ListCustomerDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListCustomerDto>("Invalid id.");
            }
            var existingCustomer = _customerDal.Get(c => c.Id == id);
            if (existingCustomer == null)
            {
                return new ErrorDataResult<ListCustomerDto>("Customer not found.");
            }
            var listCustomer = _mapper.Map<ListCustomerDto>(existingCustomer);
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
