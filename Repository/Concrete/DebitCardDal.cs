using Core.Repositories;
using Models;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class DebitCardDal : EntityRepositoryBase<DebitCard, BankingSystemContext>, IDebitCardDal
    {       
    }
}
