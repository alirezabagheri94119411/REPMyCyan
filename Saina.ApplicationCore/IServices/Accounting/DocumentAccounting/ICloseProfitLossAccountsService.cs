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
    /// بستن حساب ها
    /// </summary>
   public interface ICloseProfitLossAccountsService
    {
        Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int date);
        Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync1 (DateTime fromDate, DateTime toDate);
        Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync (int date);
        List<AccDocumentItemGroupedDTO> GetGroupedAccDocumentItems(int date);
        bool ContextHasChanges { get; }

    }
}
