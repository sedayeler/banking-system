using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class CustomerDal : EntityRepositoryBase<Customer, BankingSystemContext>, ICustomerDal
    {
    }
}
