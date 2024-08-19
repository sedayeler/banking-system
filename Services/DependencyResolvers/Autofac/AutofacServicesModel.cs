using Autofac;
using AutoMapper;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.Concrete;
using Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DependencyResolvers.Autofac
{
    public class AutofacServicesModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<CustomerDal>().As<ICustomerDal>().SingleInstance();
            builder.RegisterType<AccountManager>().As<IAccountService>().SingleInstance();
            builder.RegisterType<AccountDal>().As<IAccountDal>().SingleInstance();
            builder.RegisterType<DebitCardManager>().As<IDebitCardService>().SingleInstance();
            builder.RegisterType<DebitCardDal>().As<IDebitCardDal>().SingleInstance();
            builder.RegisterType<CreditCardManager>().As<ICreditCardService>().SingleInstance();
            builder.RegisterType<CreditCardDal>().As<ICreditCardDal>().SingleInstance();
            builder.RegisterType<GeneratorManager>().As<IGeneratorService>().SingleInstance();
            builder.RegisterType<BankingSystemContext>().AsSelf().InstancePerLifetimeScope();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfigProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();
        }
    }
}
