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
    /// آی سرویس هدر سند حسابداری
    /// </summary>
   public interface IAccDocumentHeadersService
    {
        Task<List<AccDocumentHeaderDTO>> GetAccDocumentHeadersAsync(int date);
        Task<List<AccDocumentHeader>> GetAccSystemDocumentHeadersAsync(int date, int typeDocumentId);
        Task<List<AccDocumentHeader>> GetOpenCloseHeadersAsync(int date, int typeDocumentId,int typeDocumentId2);
        Task<AccDocumentHeader> GetAccDocumentHeaderAsync(int id);
        Task<List<TypeDocument>> GetTypeDocumentsAsync();

        Task<AccDocumentHeader> AddAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader);
        AccDocumentHeader AddAccDocumentHeader(AccDocumentHeader accDocumentHeader);
        Task<AccDocumentHeader> UpdateAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader);
        Task DeleteAccDocumentHeaderAsync(int accDocumentHeaderId);
        bool ContextHasChanges { get; }
        long GetLastAccDocumentHeaderCode(int date);
        Task<long> GetLastDailyNumberCode();
        long GetLastDailyNumberCode1();
        Task<DocumentNumbering> GetDocumentNumberingAsync();
       DocumentNumbering GetDocumentNumbering();
        Task SaveHeaderAndItemsAsync(AccDocumentHeader accDocumentHeader, IEnumerable<AccDocumentItem> accDocumentItems, IEnumerable<AccDocumentItem> itemsToBeDeleted);
       DateTime? GetFirstAcc(int date);
    }
}
