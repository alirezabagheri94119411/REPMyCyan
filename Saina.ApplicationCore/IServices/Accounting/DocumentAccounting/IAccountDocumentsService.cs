using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting
{
    /// <summary>
    /// اسناد
    /// </summary>
   public interface IAccountDocumentsService
    {
        Task<List<AccountDocument>> GetAccountDocumentsAsync();
        Task<AccountDocument> GetAccountDocumentAsync(int id);
        Task<AccountDocument> AddAccountDocumentAsync(AccountDocument accountDocument);
        Task<AccountDocument> UpdateAccountDocumentAsync(AccountDocument accountDocument);
        Task DeleteAccountDocumentAsync(int accountDocumentId);
        bool ContextHasChanges { get; }
        Task<bool> HasTitle(string title);
        Task<bool> GetDocumentNumberingAsync(int id);


    }
}
