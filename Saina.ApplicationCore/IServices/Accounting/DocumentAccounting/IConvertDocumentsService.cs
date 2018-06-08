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
    /// سورت و تبدیل اسناد موقت به دائم
    /// </summary>
   public interface IConvertDocumentsService
    {
        Task<long> GetLastNumberPermanentAsync(int date);
        Task<DateTime?> GetLastDatePermanentAsync(int date);
       // Task<List<AccDocumentHeaderDTO>> GetAccDocumentHeadersAsync(int date);
       Task<int> UpdateAccToNumberAsync(int date, StatusEnum status, int toNumber, StatusEnum convert);
       Task<int> UpdateAccToDateAsync(int date ,StatusEnum status,DateTime toDate, StatusEnum convert,string user);
        Task<bool> CanUpdate(int date, DateTime toDate);
        DateTime GetEndFinancialYear1(int date);
        bool ContextHasChanges { get; }
    }
}
