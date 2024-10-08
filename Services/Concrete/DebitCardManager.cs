﻿using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using Microsoft.VisualBasic;
using Models;
using Models.DTOs.DebitCard;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.ValidationRules.Customer;
using Services.ValidationRules.DebitCard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly IGeneratorService _generatorService;

        public DebitCardManager(IDebitCardDal debitCardDal, BankingSystemContext context, IMapper mapper, IGeneratorService generatorService)
        {
            _debitCardDal = debitCardDal;
            _context = context;
            _mapper = mapper;
            _generatorService = generatorService;
        }

        [ValidationAspect(typeof(CreateDebitCardValidator))]
        public IResult Add(CreateDebitCardDto dto)
        {
            var account = _context.accounts.Any(a => a.Id == dto.AccountId);
            if (!account)
            {
                return new ErrorResult("Account not found.");
            }

            string dateFormat = "MM/yy";
            var formattedDate = dto.ExpirationDate.ToString(dateFormat, CultureInfo.InvariantCulture);
            DebitCard newDebitCard = new DebitCard()
            {
                AccountId = dto.AccountId,
                CardNumber = _generatorService.GenerateCardNumber(),
                ExpirationDate = formattedDate,
                CCV = _generatorService.GenerateCCV(),
                Balance = dto.Balance,
                IsActive = true
            };
            _debitCardDal.Add(newDebitCard);
            return new SuccessResult("Debit card created.");
        }

        [ValidationAspect(typeof(UpdateDebitCardValidator))]
        public IResult Update(UpdateDebitCardDto dto)
        {
            var debitCard = _debitCardDal.Get(d => d.Id == dto.Id);
            if (debitCard == null)
            {
                return new ErrorResult("Debit card not found.");
            }

            var account = _context.accounts.Any(a => a.Id == dto.AccountId);
            if (!account)
            {
                return new ErrorResult("Account not found.");
            }

            DebitCard updateDebitCard = _mapper.Map(dto, debitCard);
            _debitCardDal.Update(updateDebitCard);
            return new SuccessResult("Debit card updated.");
        }

        public IResult Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResult("Invalid id.");
            }

            var debitCard = _debitCardDal.Get(d => d.Id == id);
            if (debitCard == null)
            {
                return new ErrorResult("Debit card not found.");
            }

            _debitCardDal.Delete(debitCard);
            return new SuccessResult("Debit card deleted.");
        }

        public IDataResult<ListDebitCardDto> GetById(int id)
        {
            if (id <= 0)
            {
                return new ErrorDataResult<ListDebitCardDto>("Invalid id.");
            }

            var debitCard = _debitCardDal.Get(d => d.Id == id);
            if (debitCard == null)
            {
                return new ErrorDataResult<ListDebitCardDto>("Debit card not found.");
            }

            var listDebitCard = _mapper.Map<ListDebitCardDto>(debitCard);
            return new SuccessDataResult<ListDebitCardDto>(listDebitCard, "Debit card listed.");
        }

        public IDataResult<List<ListDebitCardDto>> GetAll()
        {
            var debitCards = _debitCardDal.GetAll();
            var listDebitCards = _mapper.Map<List<ListDebitCardDto>>(debitCards);
            return new SuccessDataResult<List<ListDebitCardDto>>(listDebitCards, "Debit cards listed.");
        }
    }
}
