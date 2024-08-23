using AutoMapper;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using Models;
using Models.DTOs.Transaction;
using Models.Enums;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.ValidationRules.Transaction;
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
            ValidationTool.Validate(new CreateTransactionValidator(), dto);

            if (dto.DebitCardId != null && dto.CreditCardId != null || dto.DebitCardId == null && dto.CreditCardId == null)
            {
                return new ErrorResult("Transaction must have either DebitCardId or CreditCardId.");
            }
          
            var transactionTypeMapping = new Dictionary<string, TransactionType>
            {
                 { "Income", TransactionType.Income },
                 { "Outcome", TransactionType.Outcome }
            };
            if (!transactionTypeMapping.TryGetValue(dto.TransactionType, out TransactionType transactionType))
            {
                return new ErrorResult("Invalid transaction type.");
            }

            CardType cardType = default;
            if (dto.DebitCardId != null)
            {
                cardType = CardType.DebitCard;
                var debitCard = _context.debit_cards.SingleOrDefault(d => d.Id == dto.DebitCardId);
                if (debitCard == null)
                {
                    return new ErrorResult("Debit card not found.");
                }

                if (!debitCard.IsActive)
                {
                    return new ErrorResult("Debit card is not active.");
                }

                if (dto.TransactionType == "Income")
                {
                    debitCard.Balance += dto.Amount;
                }

                if (dto.Amount <= debitCard.Balance && dto.TransactionType == "Outcome")
                {
                    debitCard.Balance -= dto.Amount;
                }

                if (dto.Amount > debitCard.Balance && dto.TransactionType == "Outcome")
                {
                    return new ErrorResult("Transaction amount exceeds current balance.");
                }
                _context.SaveChanges();
            }

            if (dto.CreditCardId != null)
            {
                cardType = CardType.CreditCard;
                var creditCard = _context.credit_cards.SingleOrDefault(c => c.Id == dto.CreditCardId);
                if (creditCard == null)
                {
                    return new ErrorResult("Credit card not found.");
                }

                if (!creditCard.IsActive)
                {
                    return new ErrorResult("Credit card is not active.");
                }

                if (dto.TransactionType == "Income")
                {
                    if (creditCard.Debt > 0)
                    {
                        if (creditCard.Debt >= dto.Amount)
                        {
                            creditCard.Debt -= dto.Amount;
                        }
                        else
                        {
                            creditCard.Debt = 0;
                            decimal remainder = dto.Amount - creditCard.Debt;
                            creditCard.Limit += remainder;
                        }
                    }
                    else
                    {
                        creditCard.Limit += dto.Amount;
                    }
                }

                if (dto.Amount <= creditCard.Limit && dto.TransactionType == "Outcome")
                {
                    creditCard.Limit -= dto.Amount;
                    creditCard.Debt += dto.Amount;
                }

                if (dto.Amount > creditCard.Limit && dto.TransactionType == "Outcome")
                {
                    return new ErrorResult("Transaction amount exceeds current limit.");
                }
                _context.SaveChanges();
            }

            Transaction newTransaction = new Transaction
            {
                DebitCardId = dto.DebitCardId,
                CreditCardId = dto.CreditCardId,
                Description = dto.Description,
                Amount = dto.Amount,
                CardType = cardType.ToString(),
                TransactionType = dto.TransactionType,
                Date = DateTime.UtcNow
            };
            _transactionDal.Add(newTransaction);
            return new SuccessResult("Transaction created.");
        }

        public IResult Update(UpdateTransactionDto dto)
        {
            ValidationTool.Validate(new UpdateTransactionValidator(), dto);

            var transaction = _transactionDal.Get(t => t.Id == dto.Id);
            if (transaction == null)
            {
                return new ErrorResult("Transaction not found.");
            }

            if (dto.DebitCardId != null && dto.CreditCardId != null || dto.DebitCardId == null && dto.CreditCardId == null)
            {
                return new ErrorResult("Transaction must have either DebitCardId or CreditCardId.");
            }

            var transactionTypeMapping = new Dictionary<string, TransactionType>
            {
                 { "Income", TransactionType.Income },
                 { "Outcome", TransactionType.Outcome }
            };
            if (!transactionTypeMapping.TryGetValue(dto.TransactionType, out TransactionType transactionType))
            {
                return new ErrorResult("Invalid transaction type.");
            }

            CardType cardType = default;
            if (dto.DebitCardId != null)
            {
                cardType = CardType.DebitCard;
                var debitCard = _context.debit_cards.SingleOrDefault(d => d.Id == dto.DebitCardId);
                if (debitCard == null)
                {
                    return new ErrorResult("Debit card not found.");
                }

                if (!debitCard.IsActive)
                {
                    return new ErrorResult("Debit card is not active.");
                }

                if (dto.TransactionType == "Income")
                {
                    debitCard.Balance += dto.Amount;
                }

                if (dto.Amount <= debitCard.Balance && dto.TransactionType == "Outcome")
                {
                    debitCard.Balance -= dto.Amount;
                }

                if (dto.Amount > debitCard.Balance && dto.TransactionType == "Outcome")
                {
                    return new ErrorResult("Transaction amount exceeds current balance.");
                }
                _context.SaveChanges();
            }

            if (dto.CreditCardId != null)
            {
                cardType = CardType.CreditCard;
                var creditCard = _context.credit_cards.SingleOrDefault(c => c.Id == dto.CreditCardId);
                if (creditCard == null)
                {
                    return new ErrorResult("Credit card not found.");
                }

                if (!creditCard.IsActive)
                {
                    return new ErrorResult("Credit card is not active.");
                }

                if (dto.TransactionType == "Income")
                {
                    if (creditCard.Debt > 0)
                    {
                        if (creditCard.Debt >= dto.Amount)
                        {
                            creditCard.Debt -= dto.Amount;
                        }
                        else
                        {
                            creditCard.Debt = 0;
                            decimal remainder = dto.Amount - creditCard.Debt;
                            creditCard.Limit += remainder;
                        }
                    }
                    else
                    {
                        creditCard.Limit += dto.Amount;
                    }
                }

                if (dto.Amount <= creditCard.Limit && dto.TransactionType == "Outcome")
                {
                    creditCard.Limit -= dto.Amount;
                    creditCard.Debt += dto.Amount;
                }

                if (dto.Amount > creditCard.Limit && dto.TransactionType == "Outcome")
                {
                    return new ErrorResult("Transaction amount exceeds current limit.");
                }
                _context.SaveChanges();
            }

            var updateTransaction = _mapper.Map(dto, transaction);
            _transactionDal.Update(updateTransaction);
            return new SuccessResult("Transaction updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }

            var transaction = _transactionDal.Get(c => c.Id == id);
            if (transaction == null)
            {
                return new ErrorResult("Transaction not found.");
            }

            _transactionDal.Delete(transaction);
            return new SuccessResult("Transaction deleted.");
        }

        public IDataResult<ListTransactionDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListTransactionDto>("Invalid id.");
            }

            var transaction = _transactionDal.Get(c => c.Id == id);
            if (transaction == null)
            {
                return new ErrorDataResult<ListTransactionDto>("Transaction not found.");
            }

            var listTransaction = _mapper.Map<ListTransactionDto>(transaction);
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
