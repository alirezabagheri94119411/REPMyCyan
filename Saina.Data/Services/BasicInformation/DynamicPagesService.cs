using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.Data.Context;
using System.Data.Entity;
using Saina.Common.Security;
using System.Data.Entity.Migrations;

namespace Saina.Data.Services.BasicInformation
{
    public class DynamicPagesService : IDynamicPagesService
    {
        private readonly SainaDbContext _uow;
        private readonly IAppContextService _appContextService;

        public DynamicPagesService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
        }

        public async  Task<List<DynamicPage>> GetDynamicPagesAsync()
        {

            var res = await  _uow.DynamicPages.Where(x => x.ParentId == null).AsNoTracking().ToListAsync().ConfigureAwait(false);
            return res;
        }
        public async Task<IEnumerable<DynamicPage>> GetDynamicPagesAsync(bool hasAllNodes)
        {
            //var temp = _uow.DynamicPages.Where(x => x.ParentId == null).AsNoTracking().ToList();
            //var res = temp.Where(x=> _appContextService.CurrentUser?.HasAccess(x.Name)!=null && _appContextService.CurrentUser?.HasAccess(x.Name) != false).ToList();
            IEnumerable<DynamicPage> res;
            if (_appContextService.CurrentUser?.Group != null)
                res = _appContextService.CurrentUser.Group.DynamicPages.Select(x => x.DynamicPage).Where(x => x.ParentId == null || hasAllNodes).ToList();
            else
                res = _appContextService.CurrentUser?.DynamicPages.ToList().Select(x => x.DynamicPage).Where(x => x.ParentId == null);
            if (res?.Any()==true)
                res = res.Where(x => _appContextService.CurrentUser?.HasAccess(x.Name) == true);
            //else
            //    res = await InitializeAsync();
            var orderedRes= res.OrderBy(x => x.Order).ToList();
            return res.OrderBy(x=>x.Order);
        }
        //private async Task<List<DynamicPage>> InitializeAsync()
        //{
        //    if (_appContextService.CurrentUser.DynamicPages.Any())
        //    {

        //    var customersParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Customers");
        //    _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = customersParent });

        //        var financialYearsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "FinancialYears");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = financialYearsParent });

        //        var currenciesParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Currencies");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = currenciesParent });

        //        var accountDocumentsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "AccountDocuments");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = accountDocumentsParent });

        //        var documentNumberingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "DocumentNumberings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = documentNumberingsParent });

        //        var exchangeRatesParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "ExchangeRates");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = exchangeRatesParent });

        //        var dLsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "DLs");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = dLsParent });

        //        var sLsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SLs");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = sLsParent });

        //        var tLsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "TLs");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = tLsParent });

        //        var gLsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "GLs");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = gLsParent });

        //        var usersParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Users");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = usersParent });

        //        var groupsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Groups");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = groupsParent });
        //        // var aboutsParent = new DynamicPage { Name = "Abouts", Title = "درباره ما", Group=_appContextService.CurrentUser,User=_appContextService.CurrentUser };
        //        var banksParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Customers");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = banksParent });

        //        var bankAccountsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "BankAccounts");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = bankAccountsParent });

        //        var companiesParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "Companies");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = companiesParent });

        //        var peopleParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "People");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = peopleParent });

        //        var systemAccountingSettingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SystemAccountingSettings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = systemAccountingSettingsParent });

        //        var companyInformationsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "CompanyInformations");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = companyInformationsParent });

        //        var generalSystemSettingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "GeneralSystemSettings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = generalSystemSettingsParent });

        //        var systemReceivePaymentSettingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SystemReceivePaymentSettings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = systemReceivePaymentSettingsParent });

        //        var systemSettingRetailsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SystemSettingRetails");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = systemSettingRetailsParent });

        //        var salarySystemSettingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SalarySystemSettings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = salarySystemSettingsParent });

        //        var systemSettingSalesParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "SystemSettingSales");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = systemSettingSalesParent });

        //        var shoppingSystemSettingsParent = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == "ShoppingSystemSettings");
        //        _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = shoppingSystemSettingsParent });
        //    // var changeProfilesParent = new DynamicPage { Name = "ChangeProfiles", Title = "تغییر اطلاعات کاربری",  };

        //    var allPageNames = PageAuthorizationScanner.TypesWithPageAuthorizationAttributeCache
        //        .Select(x => new TempDynamicPage { Name = x.Name, Title = x.GetProperty("Tag")?.GetValue(Activator.CreateInstance(x), null), ParentName = x.GetProperty("ParentName")?.GetValue(Activator.CreateInstance(x), null), IconPath = x.GetProperty("IconPath")?.GetValue(Activator.CreateInstance(x), null) }).ToList();

        //    allPageNames.Add(new TempDynamicPage { Name = customersParent.Name, Title = customersParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = groupsParent.Name, Title = groupsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = financialYearsParent.Name, Title = financialYearsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = currenciesParent.Name, Title = currenciesParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = accountDocumentsParent.Name, Title = accountDocumentsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = documentNumberingsParent.Name, Title = documentNumberingsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = exchangeRatesParent.Name, Title = exchangeRatesParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = dLsParent.Name, Title = dLsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = sLsParent.Name, Title = sLsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = tLsParent.Name, Title = tLsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = gLsParent.Name, Title = gLsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = usersParent.Name, Title = usersParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = banksParent.Name, Title = banksParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = bankAccountsParent.Name, Title = bankAccountsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = companiesParent.Name, Title = companiesParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = peopleParent.Name, Title = peopleParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = systemAccountingSettingsParent.Name, Title = systemAccountingSettingsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = companyInformationsParent.Name, Title = companyInformationsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = generalSystemSettingsParent.Name, Title = generalSystemSettingsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = systemReceivePaymentSettingsParent.Name, Title = systemReceivePaymentSettingsParent.Title });
        //    // _uow.DynamicPages.AddOrUpdate(x => x.Name, systemReceivePaymentSettingsParent);
        //    //  _uow.DynamicPages.AddOrUpdate(x => x.Name, customersParent);

        //    allPageNames.Add(new TempDynamicPage { Name = systemSettingRetailsParent.Name, Title = systemSettingRetailsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = salarySystemSettingsParent.Name, Title = salarySystemSettingsParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = systemSettingSalesParent.Name, Title = systemSettingSalesParent.Title });

        //    allPageNames.Add(new TempDynamicPage { Name = shoppingSystemSettingsParent.Name, Title = shoppingSystemSettingsParent.Title });
        //        foreach (var pageName in allPageNames)
        //        {
        //            var dynamicPage = await _uow.DynamicPages.FirstOrDefaultAsync(x => x.Name == pageName.Name); //new DynamicPage { Name = pageName.Name, Title = pageName.Title as string, IconPath = pageName.IconPath as string };
        //            if (pageName.ParentName != null)
        //                _appContextService.CurrentUser.DynamicPages.Add(new UGDP { DynamicPage = dynamicPage });
        //        }


        //        await _uow.SaveChangesAsync();
        //}
        //    return  _appContextService.CurrentUser.DynamicPages.Select(x => x.DynamicPage).ToList();
        //}

    }
}

