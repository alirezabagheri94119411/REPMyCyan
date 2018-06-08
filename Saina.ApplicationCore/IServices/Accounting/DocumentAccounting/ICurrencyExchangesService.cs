using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// تسعیر ارز
    /// </summary>
    public interface ICurrencyExchangesService
    {
        Task<List<AccDocumentItemGroupedDTO>> GetCurrencyDocAsync();
        Task<DateTime?> GetLastDateDocAsync(int date);
        Task<DateTime> GetStartFinancialYear(int date);
        DateTime GetStartFinancialYear1(int date);
        Task<DateTime> GetEndFinancialYear(int date);
        DateTime GetEndFinancialYear1(int date);
        Task<List<AccDocumentItem>> GetLastAccDocumentItemsAsync(DateTime fromDate,DateTime toDate);
        Task<double?> GetRemainExchangesAsync(DateTime fromDate, DateTime toDate);
        double? GetRemainExchanges(DateTime fromDate, DateTime toDate);
        Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync(DateTime fromDate,DateTime toDate);
        List<AccDocumentItemGroupedDTO> GetGroupedAccDocumentItems(DateTime fromDate, DateTime toDate);
        Task<AccDocumentHeader> AddAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader);
    
        bool ContextHasChanges { get; }
        long GetLastAccDocumentHeaderCode();
   
        Task<DocumentNumbering> GetDocumentNumberingAsync();
        double GetLastCurrencyDocAsync();
         Task<Dictionary<int?, double>> GetLastCurrencyDocDictionayAsync();
       DateTime? GetEndExchangeDoc(int date);
    }
}
