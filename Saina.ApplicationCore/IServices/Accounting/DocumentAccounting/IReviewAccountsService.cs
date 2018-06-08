using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.DTOs.Accounting.ReviewAcc;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    public enum GroupStatusEnum
    {
        DetailedGL = 1,
        DetailedTL = 2,
        DetailedSL = 4,
        DetailedDL1 = 8,
        DetailedDL2 = 16,
        DetailedCurrency = 32,
        DetailedTracking = 64,
        All=DetailedGL | DetailedTL | DetailedSL | DetailedDL1 | DetailedDL2 |DetailedCurrency | DetailedTracking | Undo,
        Undo = 128
    }
    /// <summary>
    /// مرور حساب ها
    /// </summary>
    public interface IReviewAccountsService
    {
        AccDocumentHeaderFilter AccDocumentHeaderFilter { get; set; }

        Task<List<AccDocumentItemDTO>> GetDetailedCurrencyAsync();
        Task<List<AccDocumentItemDTO>> GetDetailedDL1Async();
        Task<List<AccDocumentItemDTO>> GetDetailedDL2Async();
        Task<List<AccDocumentItemDTO>> GetDetailedGLAsync();
        Task<List<AccDocumentItemDTO>> GetDetailedSLAsync();
        Task<List<AccDocumentItemDTO>> GetDetailedTLAsync();
        Task<List<AccDocumentItemDTO>> GetDetailedTrackingAsync();
        Task<List<AccDocumentItemDTO>> GetGroupedCurrencyAsync();
        Task<List<AccDocumentItemDTO>> GetGroupedDL1Async();
        Task<List<AccDocumentItemDTO>> GetGroupedDL2Async();
        Task<List<AccDocumentItemDTO>> GetGroupedGLAsync();
        Task<List<AccDocumentItemDTO>> GetGroupedSLAsync();
        Task<List<AccDocumentItemDTO>> GetGroupedTLAsync();
        Task<List<AccDocumentItemDTO>> GetGroupedTrackingAsync();
        SL GetSL();
    }
}
