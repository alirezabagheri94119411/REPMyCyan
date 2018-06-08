using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
   public interface IPeopleService
    {
        Task<List<Person>> GetPeopleAsync();
        Task<List<Person>> GetPeopleCodeAsync(long? Code);
        Task<Person> GetPersonAsync(int id);
        Task<Person> AddPersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Person person);
        Task DeletePersonAsync(int personId);
        bool ContextHasChanges { get; }
    }
}
