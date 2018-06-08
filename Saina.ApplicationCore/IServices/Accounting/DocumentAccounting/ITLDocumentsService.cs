using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
   public interface ITLDocumentsService
    {
        Task<int> GetLastNumberDocAsync(int date);
      //  int GetLastNumberDocAsync(int date);
        Task<DateTime?> GetLastDateDocAsync(int date);
       int GetLastNumberDoc(int date);
        DateTime? GetLastDateDoc(int date);
        void  DeleteTLDocumentHeaderAsync(int TLDocumentHeaderId);
        Task<List<TLDocumentHeader>> GetTLDocumentHeadersAsync(int date);
        Task<List<TLDocumentItem>> GetTLDocumentItemsAsync(int Id);
     //   Task GetExportDocumentDate(DateTime fromDate,DateTime toDate,int fromNumber, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum, DateTime TLDocumentDate);
        Task GetExportDocumentNumber( int toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum);
      //  Task<int> GetExportDocumentDate(int date, long toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum);
        int GetExportDocumentDate(int date, long toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum);
        //   Task<List<TLDocumentItem>> GetMonthlyDocument(DateTime fromDate, DateTime toDate);
        //Task<List<TLDocumentGroupedDTO>> GetDocumentNumber(int fromNumber, int toNumber);
        //Task<List<TLDocumentGroupedDTO>> GetDocumentDate(DateTime fromDate, DateTime toDate);
        bool HasTLDocumentItem(int date);
       int LastNumber(int date);
        DateTime LastDate(int date);
        bool ContextHasChanges { get; }
        int HasType(int date);
        bool GetAccDocumentHeaders(DateTime fromDate, DateTime toDate);
    }
}
