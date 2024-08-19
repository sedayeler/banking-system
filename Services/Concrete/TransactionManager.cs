using AutoMapper;
using Core.Utilities.Result;
using Models.DTOs.Transaction;
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
    public class TransactionManager : ITransactionService
    {
        private readonly ITransactionDal _transactionDal;
        private readonly BankingSystemContext _context;
        private readonly IMapper _mapper;

        public TransactionManager(ITransactionDal transactionDal, BankingSystemContext context, IMapper mapper)
        {
            _transactionDal = transactionDal;
            _context = context;
            _mapper = mapper;
        }

        public IResult Add(CreateTransactionDto dto)
        {
            if (dto.DebitCardId <= 0 || dto.CreditCardId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            return new SuccessResult("Transaction created.");
        }

        public IResult Update(UpdateTransactionDto dto)
        {
            if (dto.Id <= 0 || dto.DebitCardId <= 0 || dto.CreditCardId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            return new SuccessResult("Transaction updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingTransaction = _transactionDal.Get(c => c.Id == id);
            if (existingTransaction == null)
            {
                return new ErrorResult("Transaction not found.");
            }
            _transactionDal.Delete(existingTransaction);
            return new SuccessResult("Transaction deleted.");
        }

        public IDataResult<ListTransactionDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListTransactionDto>("Invalid id.");
            }
            var existingTransaction = _transactionDal.Get(c => c.Id == id);
            if (existingTransaction == null)
            {
                return new ErrorDataResult<ListTransactionDto>("Transaction not found.");
            }
            var listTransaction = _mapper.Map<ListTransactionDto>(existingTransaction);
            return new SuccessDataResult<ListTransactionDto>(listTransaction, "Transaction listed.");
        }

        public IDataResult<List<ListTransactionDto>> GetAll()
        {
            var transactions = _transactionDal.GetAll();
            var listTransactions = _mapper.Map<List<ListTransactionDto>>(transactions);
            return new SuccessDataResult<List<ListTransactionDto>>(listTransactions, "Transactions listed.");
        }
    }
}
