﻿using Core.Utilities.Result;
using Models;
using Models.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Abstract
{
    public interface IAccountService
    {
        IResult Add(CreateAccountDto dto);
        IResult Update(UpdateAccountDto dto);
        IResult Delete(int id);
        IDataResult<ListAccountDto> GetById(int id);
        IDataResult<List<ListAccountDto>> GetAll();
    }
}
