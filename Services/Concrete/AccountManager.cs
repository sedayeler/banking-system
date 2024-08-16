using AutoMapper;
using Core.Models;
using Core.Utilities.Result;
using Models;
using Models.DTOs;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDal _accountDal;
        private readonly BankingSystemContext _context;
        private readonly IMapper _mapper;

        public AccountManager(IAccountDal accountDal, BankingSystemContext context, IMapper mapper)
        {
            _accountDal = accountDal;
            _context = context;
            _mapper = mapper;
        }

        public string GenerateAccountNumber()
        {
            return new Random().Next(100000000, 999999999).ToString();
        }

        public string GenerateIBAN()
        {
            return "TR" + new Random().Next(100000000, 999999999).ToString();
        }

        public IResult Add(CreateAccountDto dto)
        {
            if (dto.CustomerId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingCustomer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!existingCustomer)
            {
                return new ErrorResult("Customer not found.");
            }
            if (dto.AccountName.Length > 50)
            {
                return new ErrorResult("The account name field cannot exceed 50 characters.");
            }
            if (dto.Balance < 0)
            {
                return new ErrorResult("Balance cannot be less than 0.");
            }
            var newAccount = new Account
            {
                CustomerId = dto.CustomerId,
                AccountName = dto.AccountName,
                IBAN = GenerateIBAN(),
                AccountNumber = GenerateAccountNumber(),
                Balance = dto.Balance,
                Date = DateTime.UtcNow,
                IsActive = true
            };
            _accountDal.Add(newAccount);
            return new SuccessResult("Account created.");
        }

        public IResult Update(UpdateAccountDto dto)
        {
            if (dto.Id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingAccount = _accountDal.Get(a => a.Id == dto.Id);
            if (existingAccount == null)
            {
                return new ErrorResult("Account not found.");
            }
            var existingCustomer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!existingCustomer)
            {
                return new ErrorResult("Customer not found.");
            }
            if (dto.AccountName.Length > 50)
            {
                return new ErrorResult("The account name field cannot exceed 50 characters.");
            }
            if (dto.Balance < 0)
            {
                return new ErrorResult("Balance cannot be less than 0.");
            }
            Account updateAccount = _mapper.Map(dto, existingAccount);
            _accountDal.Update(updateAccount);
            return new SuccessResult("Account updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingAccount = _accountDal.Get(a => a.Id == id);
            if (existingAccount == null)
            {
                return new ErrorResult("Account not found.");
            }
            _accountDal.Delete(existingAccount);
            return new SuccessResult("Account deleted.");
        }

        public IDataResult<ListAccountDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListAccountDto>("Invalid id.");
            }
            var existingAccount = _accountDal.Get(c => c.Id == id);
            if (existingAccount == null)
            {
                return new ErrorDataResult<ListAccountDto>("Account not found.");
            }
            var listAccount = _mapper.Map<ListAccountDto>(existingAccount);
            return new SuccessDataResult<ListAccountDto>(listAccount, "Account listed.");
        }

        public IDataResult<List<ListAccountDto>> GetAll()
        {
            var accounts = _accountDal.GetAll();
            var listAccounts = _mapper.Map<List<ListAccountDto>>(accounts);
            return new SuccessDataResult<List<ListAccountDto>>(listAccounts, "Accounts listed.");
        }
    }
}
