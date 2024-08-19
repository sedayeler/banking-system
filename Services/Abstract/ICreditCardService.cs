using Core.Utilities.Result;
using Models.DTOs.CreditCard;
using Models.DTOs.DebitCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreateCreditCardDto dto);
        IResult Update(UpdateCreditCardDto dto);
        IResult Delete(int id);
        IDataResult<ListCreditCardDto> GetById(int id);
        IDataResult<List<ListCreditCardDto>> GetAll();
    }
}
