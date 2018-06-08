using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
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
    /// سرویس نوع سند حسابداری
    /// </summary>
   public class TypeDocumentsService: ITypeDocumentsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public TypeDocumentsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<TypeDocument>> GetTypeDocumentsAsync()
        {
            return await _uow.Set<TypeDocument>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<TypeDocument> GetTypeDocumentAsync(int id)
        {
            return await _uow.Set<TypeDocument>().FirstOrDefaultAsync(c => (c.TypeDocumentId == id)).ConfigureAwait(false);
        }

        public async Task<TypeDocument> AddTypeDocumentAsync(TypeDocument typeDocument)
        {
            _uow.TypeDocuments.Add(typeDocument);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return typeDocument;
        }
        public async Task<TypeDocument> UpdateTypeDocumentAsync(TypeDocument typeDocument)
        {
            //    var cmd = $"EXEC TypeDocument_Update @TypeDocumentId = {typeDocument.TypeDocumentId}," +
            //$" @TypeDocumentTitle = N'{typeDocument.TypeDocumentTitle}'";
            //    await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(typeDocument).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return typeDocument;
        }
        public async Task DeleteTypeDocumentAsync(int typeDocumentId)
        {
            var typeDocument = _uow.Set<TypeDocument>().FirstOrDefault(c => c.TypeDocumentId == typeDocumentId);
            if (typeDocument != null)
            {
                _uow.Set<TypeDocument>().Remove(typeDocument);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> HasTitle(string title)
        {
            return await _uow.TypeDocuments.AnyAsync(x => x.TypeDocumentTitle == title).ConfigureAwait(false);

        }
        #endregion
    }
}
