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
    /// صدور سند افتتاحیه و اختتامیه
    /// </summary>
  public  interface IOpeningClosingsService
    {
        Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int date);
        Task<bool> GetCloseProfitLossAccountAsync(int date);
        Task<bool> GetCloseAccAsync(int date);
       bool GetCloseAcc(int date);
        Task<bool> GetOpenAccAsync(int date);
        Task<List<AccDocumentItemOpenCloseDTO>> AddOpenAccAsync(DateTime start, DateTime end);

        Task<List<AccDocumentItemOpenCloseDTO>> AddCloseAccAsync(DateTime start, DateTime end);
       // Task<AccDocumentItem> UpdateAccDocumentItemAsync(AccDocumentItem accDocumentItem);
        bool ContextHasChanges { get; }

    }
}
