using Core.Utilities.Result;
using Models.DTOs.DebitCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IDebitCardService
    {
        IResult Add(CreateDebitCardDto dto);
        IResult Update(UpdateDebitCardDto dto);
        IResult Delete(int id);
        IDataResult<ListDebitCardDto> GetById(int id);
        IDataResult<List<ListDebitCardDto>> GetAll();
    }
}
