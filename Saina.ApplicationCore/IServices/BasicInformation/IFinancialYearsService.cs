using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    public interface IFinancialYearsService
    {

       List<FinancialYear> GetFinancialYears();
        Task<List<FinancialYear>> GetFinancialYearsAsync();
        Task<List<FinancialYear>> GetFinancialYearsActiveAsync();
        Task<FinancialYear> GetFinancialYearAsync(int id);
        Task<FinancialYear> AddFinancialYearAsync(FinancialYear financialYear);
        Task<FinancialYear> UpdateFinancialYearAsync(FinancialYear financialYear);
        Task DeleteFinancialYearAsync(int financialYearId);
//        List<FinancialYear> GetFinancialYearsList(int count = 1000);
        bool ContextHasChanges { get; }
        Task<bool> HasduplicateStart(DateTime startDate);
        Task<bool> HasduplicateEnd(DateTime endDate);
        Task<bool> HasFinancialYear(int financialYear);
        int GetLastFinancialYear();

    }
}
