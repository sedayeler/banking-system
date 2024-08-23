using AutoMapper;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using Models;
using Models.DTOs.Account;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.ValidationRules.Account;
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
        private readonly IGeneratorService _generatorService;

        public AccountManager(IAccountDal accountDal, BankingSystemContext context, IMapper mapper, IGeneratorService generatorService)
        {
            _accountDal = accountDal;
            _context = context;
            _mapper = mapper;
            _generatorService = generatorService;
        }

        public IResult Add(CreateAccountDto dto)
        {
            ValidationTool.Validate(new CreatAccountValidator(), dto);

            var customer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!customer)
            {
                return new ErrorResult("Customer not found.");
            }

            Account newAccount = new Account
            {
                CustomerId = dto.CustomerId,
                AccountName = dto.AccountName,
                IBAN = _generatorService.GenerateIBAN(),
                AccountNumber = _generatorService.GenerateAccountNumber(),
                Balance = dto.Balance,
                Date = DateTime.UtcNow,
                IsActive = true
            };
            _accountDal.Add(newAccount);
            return new SuccessResult("Account created.");
        }

        public IResult Update(UpdateAccountDto dto)
        {
            ValidationTool.Validate(new UpdateAccountValidator(), dto);

            var account = _accountDal.Get(a => a.Id == dto.Id);
            if (account == null)
            {
                return new ErrorResult("Account not found.");
            }

            var customer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!customer)
            {
                return new ErrorResult("Customer not found.");
            }

            Account updateAccount = _mapper.Map(dto, account);
            _accountDal.Update(updateAccount);
            return new SuccessResult("Account updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }

            var account = _accountDal.Get(a => a.Id == id);
            if (account == null)
            {
                return new ErrorResult("Account not found.");
            }

            _accountDal.Delete(account);
            return new SuccessResult("Account deleted.");
        }

        public IDataResult<ListAccountDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListAccountDto>("Invalid id.");
            }

            var account = _accountDal.Get(c => c.Id == id);
            if (account == null)
            {
                return new ErrorDataResult<ListAccountDto>("Account not found.");
            }

            var listAccount = _mapper.Map<ListAccountDto>(account);
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
