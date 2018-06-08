using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
   public interface IPropertiesService
    {
        Task<List<Property>> GetPropertiesAsync();
        Task<Property> GetPropertyAsync(int id);
        Task<Property> AddPropertyAsync(Property property);
        Task<Property> UpdatePropertyAsync(Property property);
        Task DeletePropertyAsync(int propertyId);
        bool ContextHasChanges { get; }
    }
}
