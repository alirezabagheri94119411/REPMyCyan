//using Microsoft.Practices.Unity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Unity;
//using Unity.Lifetime;
//using Saina.WPF.FinancialYears;
//using Saina.Data.Services;
//using Saina.Data.Services1;
//using Unity.RegistrationByConvention;
//using Unity.Registration;

//namespace Saina.WPF
//{
//    public static class ContainerHelper
//    {
//        private static UnityContainer _container;
//        static ContainerHelper()
//        {
//             _container = new UnityContainer();

//            _container.RegisterType<IFinancialYearsRepository, FinancialYearsRepository>(new ContainerControlledLifetimeManager());
//            _container.RegisterType<IOrdersRepository, OrdersRepository>(new ContainerControlledLifetimeManager());
//        }

//        public static IUnityContainer Container
//        {
//            get { return _container; }
//        }
//    }
//}
