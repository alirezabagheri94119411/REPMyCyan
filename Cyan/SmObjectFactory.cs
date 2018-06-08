//using MyWpfFramework.Common.Config;
//using MyWpfFramework.Common.MVVM;
//using MyWpfFramework.DataLayer.Context;
//using MyWpfFramework.ServiceLayer.BasicInformation;
//using MyWpfFramework.ServiceLayer.BasicInformation.Contracts;
using StructureMap;
using System;
using System.Threading;
using Saina.Data.Services;
using Saina.ApplicationCore.IServices;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Services.BasicInformation;
using Saina.Common.Config;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Services.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Services.BasicInformation.Information;
using Saina.Data.Services.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.Data.Services.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Data.Services.Setting;
using Saina.Data.Context;
using Saina.Data.Services.Commerce;
using Saina.ApplicationCore.IServices.Commerce;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using System.Data.Entity;

namespace Saina.WPF
{
    /// <summary>
    /// تنظيمات تزريق وابستگي‌هاي برنامه در اينجا انجام مي‌شوند
    /// </summary>
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// SM Container.
        /// </summary>
        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        /// <summary>
        /// اعمال تنظيمات تزريق وابستگي‌ها
        /// </summary>ummary>
        private static Container defaultContainer()
        {
            return new Container(cfg =>
            {
                //// نکته: در برنامه‌های دسکتاپ نیازی به استفاده از حالت
                //// HybridHttpOrThreadLocalScoped
                //// نیست چون سبب عدم رها شدن منابع می‌گردد و در این حالت کل صفحات برنامه با یک کانتکست کار خواهد کرد
                //// مگر اینکه مانند برنامه‌های وب در آخر هر درخواست، کار رها سازی منابع به صورت دستی انجام شود
                //cfg.For<IUnitOfWork>().Use(() => new SainaDbContext());

                ////علت سینگلتون تعریف شدن وهله در اینجا:
                ////هربار فقط یک کاربر در برنامه دسکتاپ وارد می‌شود
                ////همچنین نیاز داریم اطلاعات کاربر لاگین شده را به صورت سراسری
                //جهت اعتبارسنجی‌های ویژه صفحات مختلف نگه داری کنیم
                //cfg.For<IAppContextService>().Singleton().Use<AppContextService>();
                //cfg.For<IFinancialYearsService>().Singleton().Use<FinancialYearsService>();
                //cfg.For<IGroupsService>().Singleton().Use<GroupsService>();
                //cfg.For<IFinancialYearsService>().Singleton().Use<FinancialYearsService>();
                //cfg.For<ICompanyInformationsService>().Singleton().Use<CompanyInformationsService>();

                //cfg.Policies.SetAllProperties(properties =>
                //{
                //    properties.OfType<IAppContextService>();
                //});

                //todo: اگر نیاز است سایر تنظیمات در اینجا اضافه خواهند شد
                cfg.For<SainaDbContext>().Use<SainaDbContext>().Transient();

                cfg.For<ICustomersRepository>().Singleton().Use<CustomersRepository>();
                cfg.For<IOrdersRepository>().Singleton().Use<OrdersRepository>();
                cfg.For<IUsersService>().Singleton().Use<UsersService>();
                cfg.For<IGroupsService>().Singleton().Use<GroupsService>();
                cfg.For<IConfigSetGet>().Singleton().Use<ConfigSetGet>();
                cfg.For<IAppContextService>().Singleton().Use<AppContextService>();
                cfg.For<IFinancialYearsService>().Singleton().Use<FinancialYearsService>();
                cfg.For<ICompanyInformationsService>().Singleton().Use<CompanyInformationsService>();
               // cfg.For<IAccountGroupsService>().Singleton().Use<AccountGroupsService>();
                cfg.For<IGLsService>().Singleton().Use<GLsService>();
                cfg.For<ITLsService>().Singleton().Use<TLsService>();
                cfg.For<ISLsService>().Singleton().Use<SLsService>();
                cfg.For<IDLsService>().Singleton().Use<DLsService>();
                cfg.For<IBanksService>().Singleton().Use<BanksService>();
                cfg.For<IBankAccountsService>().Singleton().Use<BankAccountsService>();
                cfg.For<IStocksService>().Singleton().Use<StocksService>();
                cfg.For<ISelectAgentsService>().Singleton().Use<SelectAgentsService>();

                cfg.For<ICurrenciesService>().Singleton().Use<CurrenciesService>();
                cfg.For<IExchangeRatesService>().Singleton().Use<ExchangeRatesService>();
                cfg.For<IPeopleService>().Singleton().Use<PeopleService>();
                cfg.For<ICompaniesService>().Singleton().Use<CompaniesService>();
                cfg.For<IAccountDocumentsService>().Singleton().Use<AccountDocumentsService>();
                cfg.For<IDocumentNumberingsService>().Singleton().Use<DocumentNumberingsService>();
                cfg.For<ISystemAccountingSettingsService>().Singleton().Use<SystemAccountingSettingsService>();
                cfg.For<IGeneralSystemSettingsService>().Singleton().Use<GeneralSystemSettingsService>();
                cfg.For<IShoppingSystemSettingsService>().Singleton().Use<ShoppingSystemSettingsService>();
                cfg.For<ISystemReceivePaymentSettingsService>().Singleton().Use<SystemReceivePaymentSettingsService>();
                cfg.For<ISystemSettingRetailsService>().Singleton().Use<SystemSettingRetailsService>();
                cfg.For<ISalarySystemSettingsService>().Singleton().Use<SalarySystemSettingsService>();
                cfg.For<ISystemSettingSalesService>().Singleton().Use<SystemSettingSalesService>();
                cfg.For<IDynamicPagesService>().Singleton().Use<DynamicPagesService>();
                cfg.For<ISLStandardDescriptionsService>().Singleton().Use<SLStandardDescriptionsService>();
                cfg.For<IProductsService>().Singleton().Use<ProductsService>();
                cfg.For<IDLTypesService>().Singleton().Use<DLTypesService>();
                cfg.For<IAccountsNaturesService>().Singleton().Use<AccountsNaturesService>();
                cfg.For<IDLAccountsNaturesService>().Singleton().Use<DLAccountsNaturesService>();
                cfg.For<ITypeDocumentsService>().Singleton().Use<TypeDocumentsService>();
                cfg.For<IPropertiesService>().Singleton().Use<PropertiesService>();
                cfg.For<IBankTypesService>().Singleton().Use<BankTypesService>();
                cfg.For<IAccountTypesService>().Singleton().Use<AccountTypesService>();
                cfg.For<ICountingWaysService>().Singleton().Use<CountingWaysService>();
                cfg.For<IStylesService>().Singleton().Use<StylesService>();
                cfg.For<IRelatedPeopleService>().Singleton().Use<RelatedPeopleService>();
                cfg.For<IAccDocumentItemsService>().Singleton().Use<AccDocumentItemsService>();
                cfg.For<IAccDocumentHeadersService>().Singleton().Use<AccDocumentHeadersService>();
                cfg.For<IReviewAccountsService>().Singleton().Use<ReviewAccountsService>();
                cfg.For<IConvertDocumentsService>().Singleton().Use<ConvertDocumentsService>();
                cfg.For<ICloseProfitLossAccountsService>().Singleton().Use<CloseProfitLossAccountsService>();
                cfg.For<IOpeningClosingsService>().Singleton().Use<OpeningClosingsService>();
                cfg.For<ISelectFinancialYearsService>().Singleton().Use<SelectFinancialYearsService>();
                cfg.For<ICurrencyExchangesService>().Singleton().Use<CurrencyExchangesService>();
                cfg.For<ITLDocumentsService>().Singleton().Use<TLDocumentsService>();
               
             //   cfg.For<ITreeAccountsService>().Singleton().Use<TreeAccountsService>();

                //  cfg.For<IWindow1Service>().Singleton().Use<Window1Service>();

                //  cfg.For<ITypeDocumentsService>().Singleton().Use<TypeDocumentsService>();


                cfg.Scan(scan =>
                {
                   // scan.TheCallingAssembly();
                    ////scan.AssemblyContainingType<IFinancialYearsService>();
                    //scan.AssemblyContainingType<IConfigSetGet>();

                    //// Add all types that implement IView into the container,
                    //// and name each specific type by the short type name.
                    //scan.With(new SingletonConvention<BindableBase>()); // The lifetime of added ViewModels is Transient. Use this method to change them.
                    scan.AddAllTypesOf<BindableBase>().NameBy(type => type.Name); // with default lifecycle => (Transient)

                    // این نکته حجم زیادی از کدهای تکراری تعاریف اولیه را کاهش می‌دهد
                    // Wire up all I`Test` interfaces with `Test` classes.
                    
                    scan.WithDefaultConventions();
                });

            });
        }
    }
}