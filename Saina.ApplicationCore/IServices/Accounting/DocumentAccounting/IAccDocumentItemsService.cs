using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// آی سرویس آیتم سند حسابداری
    /// </summary>
   public interface IAccDocumentItemsService
    {
         Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int headerId);
         Task<AccDocumentItem> GetFirstAccDocumentItemAsync(int id);
         Task<double> GetSumDebitAccDocumentItemAsync();
         Task<double> GetSumCreditAccDocumentItemAsync();
         Task<double> GetDifferenceAccDocumentItemAsync();
        
        Task<AccDocumentItem> GetAccDocumentItemAsync(int id);
        AccDocumentItem AddAccDocumentItemAsync(AccDocumentItem accDocumentItem);
        Task<AccDocumentItem> UpdateAccDocumentItemAsync(AccDocumentItem accDocumentItem);
        Task DeleteAccDocumentItemAsync(int accDocumentItemId);
        Task<AccDocumentItem> AddItemsAccDocumentItemAsync(AccDocumentItem temp);
        Task<int> DeleteItemsAccDocumentItemAsync(IEnumerable<int> ids);
        void AddAccDocumentItemsAsync(IEnumerable<AccDocumentItem> accDocumentItem);

        bool ContextHasChanges { get; }
        Task<bool> HasCurrency(int id);
        Task<bool> HasTracking(int id);
        Task<List<AccDocumentItem>> GetAccDocumentItemDateAsync(int date);
       bool HasAccDocumentItemDate(int date);
        long FirstNumber(int date);
        DateTime Firstdate(int date);
        //  int GetLastRow();



    }
}
