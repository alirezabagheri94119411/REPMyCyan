using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// سرویس شماره گذاری اسناد
    /// </summary>
    public class DocumentNumberingsService:IDocumentNumberingsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public DocumentNumberingsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _documents = _uow.Set<AccountDocument>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        private IDbSet<AccountDocument> _documents;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<DocumentNumbering>> GetDocumentNumberingsAsync()
        {
            return await _uow.Set<DocumentNumbering>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<DocumentNumbering> GetDocumentNumberingAsync(int id)
        {
            return await _uow.Set<DocumentNumbering>().FirstOrDefaultAsync(c => (c.DocumentNumberingId == id)).ConfigureAwait(false);
        }
        public async Task<DocumentNumbering> AddDocumentNumberingAsync(DocumentNumbering documentNumbering)
        {
            _uow.DocumentNumberings.Add(documentNumbering);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return documentNumbering;
        }
        public async Task<DocumentNumbering> UpdateDocumentNumberingAsync(DocumentNumbering documentNumbering)
        {

            //var cmd = $"EXEC DocumentNumbering_Update @DocumentNumberingId = {documentNumbering.DocumentNumberingId}," +
            // $" @AccountDocumentId = {documentNumbering.AccountDocumentId}," +
            // $" @CountingWayId = {documentNumbering.CountingWayId}," +
            // $" @StartNumber = {documentNumbering.StartNumber}," +
            // $" @EndNumber = {documentNumbering.EndNumber}," +
            // $" @StyleId = {documentNumbering.StyleId}";


            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(documentNumbering).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return documentNumbering;
        }
        public async Task DeleteDocumentNumberingAsync(int documentNumberingId)
        {
            var documentNumbering = _uow.Set<DocumentNumbering>().FirstOrDefault(c => c.DocumentNumberingId == documentNumberingId);
            if (documentNumbering != null)
            {
                _uow.Set<DocumentNumbering>().Remove(documentNumbering);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> HasTitle(int? Id)
        {
            return await _uow.DocumentNumberings.AnyAsync(x => x.AccountDocumentId == Id).ConfigureAwait(false);

        }

        public async Task<bool> GetAccDocumentHeaderAsync(int? id)
        {
            return await _uow.AccDocumentHeaders.AnyAsync(x => x.TypeDocumentId == id).ConfigureAwait(false);

        }

      


        #endregion
    }
}
