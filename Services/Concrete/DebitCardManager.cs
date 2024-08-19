using AutoMapper;
using Core.Utilities.Result;
using Microsoft.VisualBasic;
using Models;
using Models.DTOs.DebitCard;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services.Concrete
{
    public class DebitCardManager : IDebitCardService
    {
        private readonly IDebitCardDal _debitCardDal;
        private readonly BankingSystemContext _context;
        private readonly IMapper _mapper;

        public DebitCardManager(IDebitCardDal debitCardDal, BankingSystemContext context, IMapper mapper)
        {
            _debitCardDal = debitCardDal;
            _context = context;
            _mapper = mapper;
        }

        private string GenerateCardNumber()
        {
            Random random = new Random();
            string cardNumber = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 10).ToString();
            }

            return cardNumber;
        }

        public IResult Add(CreateDebitCardDto dto)
        {
            if (dto.AccountId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingAccount = _context.accounts.Any(a => a.Id == dto.AccountId);
            if (!existingAccount)
            {
                return new ErrorResult("Account not found.");
            }
            if (dto.CCV.Length != 3)
            {
                return new ErrorResult("The CVV code must be 3 characters long.");
            }
            if (dto.Balance < 0)
            {
                return new ErrorResult("Balance cannot be less than 0.");
            }
            string dateFormat = "MM/yy";
            var formattedDate = dto.ExpirationDate.ToString(dateFormat, CultureInfo.InvariantCulture);
            DebitCard newDebitCard = new DebitCard()
            {
                AccountId = dto.AccountId,
                CardNumber = GenerateCardNumber(),
                ExpirationDate = formattedDate,
                CCV = dto.CCV,
                Balance = dto.Balance,
                IsActive = true
            };
            _debitCardDal.Add(newDebitCard);
            return new SuccessResult("Debit card created.");
        }

        public IResult Update(UpdateDebitCardDto dto)
        {
            if (dto.Id <= 0 && dto.AccountId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingDebitCard = _debitCardDal.Get(d => d.Id == dto.Id);
            if (existingDebitCard == null)
            {
                return new ErrorResult("Debit card not found.");
            }
            var existingAccount = _context.accounts.Any(a => a.Id == dto.AccountId);
            if (!existingAccount)
            {
                return new ErrorResult("Account not found.");
            }
            if (dto.Balance < 0)
            {
                return new ErrorResult("Balance cannot be less than 0.");
            }
            DebitCard updateDebitCard = _mapper.Map(dto, existingDebitCard);
            _debitCardDal.Update(updateDebitCard);
            return new SuccessResult("Debit card updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingDebitCard = _debitCardDal.Get(d => d.Id == id);
            if (existingDebitCard == null)
            {
                return new ErrorResult("Debit card not found.");
            }
            _debitCardDal.Delete(existingDebitCard);
            return new SuccessResult("Debit card deleted.");
        }

        public IDataResult<ListDebitCardDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListDebitCardDto>("Invalid id.");
            }
            var existingDebitCard = _debitCardDal.Get(d => d.Id == id);
            if (existingDebitCard == null)
            {
                return new ErrorDataResult<ListDebitCardDto>("Debit card not found.");
            }
            var listDebitCard = _mapper.Map<ListDebitCardDto>(existingDebitCard);
            return new SuccessDataResult<ListDebitCardDto>(listDebitCard, "Debit card listed.");
        }

        public IDataResult<ListDebitCardDto> GetAll()
        {
            var debitCards = _debitCardDal.GetAll();
            var existingDebitCards = _mapper.Map<ListDebitCardDto>(debitCards);
            return new SuccessDataResult<ListDebitCardDto>(existingDebitCards, "Debit cards listed.");
        }
    }
}
