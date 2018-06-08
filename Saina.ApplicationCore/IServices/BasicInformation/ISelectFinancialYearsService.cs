using Saina.ApplicationCore.Entities.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
   public interface ISelectFinancialYearsService
    {
       
        Task<List<FinancialYear>> GetFinancialYearsActiveAsync();
      
        Task<FinancialYear> UpdateFinancialYearAsync(FinancialYear financialYear);
        Task<FinancialYear> GetSelectedFinancialYearAsync();
        DateTime? GetFirstFinancialYear();
        DateTime? GetLastFinancialYear();


        bool ContextHasChanges { get; }
     
    }
}
