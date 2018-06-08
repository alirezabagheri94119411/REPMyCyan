using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
   public interface IRelatedPeopleService
    {
        Task<List<RelatedPerson>> GetRelatedPersonsAsync(int relatedPersonId);
        Task<RelatedPerson> GetRelatedPersonAsync(int id);
        Task<RelatedPerson> AddRelatedPersonAsync(RelatedPerson relatedPerson);
        Task<RelatedPerson> UpdateRelatedPersonAsync(RelatedPerson relatedPerson);
        Task DeleteRelatedPersonAsync(int relatedPersonId);
        bool ContextHasChanges { get; }
    }
}
