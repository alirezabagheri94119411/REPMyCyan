
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.AccountDocumentAccounting
{
    /// <summary>
    /// سرویس اسناد
    /// </summary>
   public class AccountDocumentsService:IAccountDocumentsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public AccountDocumentsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
          
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<AccountDocument>> GetAccountDocumentsAsync()
        {
            return await _uow.Set<AccountDocument>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<AccountDocument> GetAccountDocumentAsync(int id)
        {
            return await _uow.Set<AccountDocument>().FirstOrDefaultAsync(c => (c.AccountDocumentId == id)).ConfigureAwait(false);
        }
        public async Task<AccountDocument> AddAccountDocumentAsync(AccountDocument accountDocument)
        {
            _uow.AccountDocuments.Add(accountDocument);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accountDocument;
        }
        public async Task<AccountDocument> UpdateAccountDocumentAsync(AccountDocument accountDocument)
        {
            //if (!_uow.Set<AccountDocument>().Local.Any(c => c.AccountDocumentId == accountDocument.AccountDocumentId))
            //{
            //_uow.Set<AccountDocument>().Attach(accountDocument);
            ////}
            ////_uow.Entry(accountDocument).State = EntityState.Modified;
            //await _uow.SaveChangesAsync();
            //      var cmd = $"EXEC AccountDocument_Update @AccountDocumentId = {accountDocument.AccountDocumentId}," +
            //$" @AccountDocumentTitle = N'{accountDocument.AccountDocumentTitle}'";
            //      await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(accountDocument).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accountDocument;
        }
        public async Task DeleteAccountDocumentAsync(int accountDocumentId)
        {
            var accountDocument = _uow.Set<AccountDocument>().FirstOrDefault(c => c.AccountDocumentId == accountDocumentId);
            if (accountDocument != null)
            {
                _uow.Set<AccountDocument>().Remove(accountDocument);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<bool> HasTitle(string title)
        {
            return await _uow.AccountDocuments.AnyAsync(x => x.AccountDocumentTitle == title).ConfigureAwait(false);

        }

        public async Task<bool> GetDocumentNumberingAsync(int id)
        {
            return await _uow.DocumentNumberings.AnyAsync(x => x.AccountDocumentId == id).ConfigureAwait(false);

        }
        #endregion
    }
}
