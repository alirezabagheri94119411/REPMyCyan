using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    ///اینترفیس حساب معین  
    /// </summary>
    public interface ISLsService
    {
        Task<List<SL>> GetSLsAsync();
        Task<List<SL>> GetSLsActiveAsync();
        Task<List<DL>> GetDLsAsync(int slId,string dlType);
        Task<List<Currency>> GetCurrenciesAsync();
        Task<List<ExchangeRate>> GetExchangeRatesAsync(int slId,string exchangeRate); 
         Task<List<SLStandardDescription>> GetSLStandardDescriptionsAsync(int slId);
        Task<SL> GetSLAsync(int id);
        Task<SL> AddSLAsync(SL sl);
        Task<SL> UpdateSLAsync(SL sl);
        Task DeleteSLAsync(int slId);
        bool ContextHasChanges { get; }
        bool HasDuplicate(long code, int id);
        bool HasTitle(string title, int id);
        long GetLastSLCode(int tlId);
        Task<bool> HasduplicateTree(long code, int id);
        Task<bool> HasTitleTree(string title, int id);
        List<SL> GetSL();
        //Task<bool> HasTL(int tLId);
    }
}
