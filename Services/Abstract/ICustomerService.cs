using Core.Utilities.Result;
using Models;
using Models.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICustomerService
    {
        IResult Add(CreateCustomerDto dto);
        IResult Update(UpdateCustomerDto dto);
        IResult Delete(int id);
        IDataResult<ListCustomerDto> GetById(int id);
        IDataResult<List<ListCustomerDto>> GetAll();
    }
}
