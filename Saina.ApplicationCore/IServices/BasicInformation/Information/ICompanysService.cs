using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
   public interface ICompaniesService
    {
        Task<List<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyAsync(int id);
        Task<Company> AddCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int companyId);
        bool ContextHasChanges { get; }
    }
}
