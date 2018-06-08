using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
   public interface ISLStandardDescriptionsService
    {
        Task<List<SLStandardDescription>> GetSLStandardDescriptionsAsync(int sLId);
        Task<SLStandardDescription> GetSLStandardDescriptionAsync(int id);
        Task<SLStandardDescription> AddSLStandardDescriptionAsync(SLStandardDescription sLStandardDescription);
        Task<SLStandardDescription> UpdateSLStandardDescriptionAsync(SLStandardDescription sLStandardDescription);
        Task DeleteSLStandardDescriptionAsync(int sLStandardDescriptionId);
        bool ContextHasChanges { get; }
    }
}
