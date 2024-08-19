using AutoMapper;
using Core.Utilities.Result;
using Models;
using Models.DTOs.CreditCard;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;
        private readonly BankingSystemContext _context;
        private readonly IMapper _mapper;
        private readonly IGeneratorService _generatorService;

        public CreditCardManager(ICreditCardDal creditCardDal, BankingSystemContext context, IMapper mapper, IGeneratorService generatorService)
        {
            _creditCardDal = creditCardDal;
            _context = context;
            _mapper = mapper;
            _generatorService = generatorService;
        }

        public IResult Add(CreateCreditCardDto dto)
        {
            if (dto.CustomerId <= 0)
            {
                return new ErrorResult("Invalid id");
            }
            var existingCustomer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!existingCustomer)
            {
                return new ErrorResult("Customer not found.");
            }
            if (dto.Limit <= 0)
            {
                return new ErrorResult("The limit cannot be equal to or less than 0.");
            }
            string dateFormat = "MM/yy";
            var formattedDate = dto.ExpirationDate.ToString(dateFormat, CultureInfo.InvariantCulture);
            CreditCard newCreditCard = new CreditCard
            {
                CustomerId = dto.CustomerId,
                CardNumber = _generatorService.GenerateCardNumber(),
                ExpirationDate = formattedDate,
                CCV = _generatorService.GenerateCCV(),
                Limit = dto.Limit
            };
            _creditCardDal.Add(newCreditCard);
            return new SuccessResult("Credit card created.");
        }

        public IResult Update(UpdateCreditCardDto dto)
        {
            if (dto.Id <= 0 || dto.CustomerId <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingCreditCard = _creditCardDal.Get(c => c.Id == dto.Id);
            if (existingCreditCard == null)
            {
                return new ErrorResult("Credit card not found.");
            }
            var existingCustomer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!existingCustomer)
            {
                return new ErrorResult("Customer not found.");
            }
            if (dto.Limit <= 0)
            {
                return new ErrorResult("The limit cannot be equal to or less than 0.");
            }
            if (dto.Debt < 0)
            {
                return new ErrorResult("The debt cannot be less than 0.");
            }
            CreditCard updateCreditCard = _mapper.Map(dto, existingCreditCard);
            _creditCardDal.Update(updateCreditCard);
            return new SuccessResult("Credit card updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }
            var existingCreditCard = _creditCardDal.Get(c => c.Id == id);
            if (existingCreditCard == null)
            {
                return new ErrorResult("Credit card not found.");
            }
            _creditCardDal.Delete(existingCreditCard);
            return new SuccessResult("Credit card deleted.");
        }

        public IDataResult<ListCreditCardDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListCreditCardDto>("Invalid id.");
            }
            var existingCreditCard = _creditCardDal.Get(c => c.Id == id);
            if (existingCreditCard == null)
            {
                return new ErrorDataResult<ListCreditCardDto>("Credit card not found.");
            }
            var listCreditCard = _mapper.Map<ListCreditCardDto>(existingCreditCard);
            return new SuccessDataResult<ListCreditCardDto>(listCreditCard, "Credit card listed.");
        }

        public IDataResult<List<ListCreditCardDto>> GetAll()
        {
            var creditCards = _creditCardDal.GetAll();
            var listCreditCards = _mapper.Map<List<ListCreditCardDto>>(creditCards);
            return new SuccessDataResult<List<ListCreditCardDto>>(listCreditCards, "Credit cards listed.");
        }
    }
}
