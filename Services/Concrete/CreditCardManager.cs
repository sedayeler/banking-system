using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using Models;
using Models.DTOs.CreditCard;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.ValidationRules.CreditCard;
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

        [ValidationAspect(typeof(CreateCreditCardValidator))]
        public IResult Add(CreateCreditCardDto dto)
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == dto.CustomerId);
            if (customer == null)
            {
                return new ErrorResult("Customer not found.");
            }

            if (dto.Limit > customer.RiskLimit)
            {
                return new ErrorResult("Invalid limit.");
            }

            string dateFormat = "MM/yy";
            var formattedDate = dto.ExpirationDate.ToString(dateFormat, CultureInfo.InvariantCulture);
            CreditCard newCreditCard = new CreditCard
            {
                CustomerId = dto.CustomerId,
                CardNumber = _generatorService.GenerateCardNumber(),
                ExpirationDate = formattedDate,
                CCV = _generatorService.GenerateCCV(),
                Limit = dto.Limit,
                IsActive = true
            };
            _creditCardDal.Add(newCreditCard);
            return new SuccessResult("Credit card created.");
        }

        [ValidationAspect(typeof(UpdateCreditCardValidator))]
        public IResult Update(UpdateCreditCardDto dto)
        {
            var creditCard = _creditCardDal.Get(c => c.Id == dto.Id);
            if (creditCard == null)
            {
                return new ErrorResult("Credit card not found.");
            }

            var customer = _context.customers.Any(c => c.Id == dto.CustomerId);
            if (!customer)
            {
                return new ErrorResult("Customer not found.");
            }

            CreditCard updateCreditCard = _mapper.Map(dto, creditCard);
            _creditCardDal.Update(updateCreditCard);
            return new SuccessResult("Credit card updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }

            var creditCard = _creditCardDal.Get(c => c.Id == id);
            if (creditCard == null)
            {
                return new ErrorResult("Credit card not found.");
            }

            _creditCardDal.Delete(creditCard);
            return new SuccessResult("Credit card deleted.");
        }

        public IDataResult<ListCreditCardDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListCreditCardDto>("Invalid id.");
            }

            var creditCard = _creditCardDal.Get(c => c.Id == id);
            if (creditCard == null)
            {
                return new ErrorDataResult<ListCreditCardDto>("Credit card not found.");
            }

            var listCreditCard = _mapper.Map<ListCreditCardDto>(creditCard);
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
