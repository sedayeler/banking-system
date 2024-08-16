using Core.Repositories;
using Models;
using Models.DTOs;
using Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
    }
}
