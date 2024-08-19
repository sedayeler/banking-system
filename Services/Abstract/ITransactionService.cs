using Core.Utilities.Result;
using Models.DTOs.DebitCard;
using Models.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ITransactionService
    {
        IResult Add(CreateTransactionDto dto);
        IResult Update(UpdateTransactionDto dto);
        IResult Delete(int id);
        IDataResult<ListTransactionDto> GetById(int id);
        IDataResult<List<ListTransactionDto>> GetAll();
    }
}
