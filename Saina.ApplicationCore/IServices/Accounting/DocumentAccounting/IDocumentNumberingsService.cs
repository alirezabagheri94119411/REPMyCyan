using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// شماره گذاری اسناد
    /// </summary>
   public interface IDocumentNumberingsService
    {
        Task<List<DocumentNumbering>> GetDocumentNumberingsAsync();
        Task<DocumentNumbering> GetDocumentNumberingAsync(int id);
        Task<DocumentNumbering> AddDocumentNumberingAsync(DocumentNumbering documentNumbering);
        Task<DocumentNumbering> UpdateDocumentNumberingAsync(DocumentNumbering documentNumbering);
        Task DeleteDocumentNumberingAsync(int documentNumberingId);
        bool ContextHasChanges { get; }
        Task<bool> HasTitle(int? Id);
        Task<bool> GetAccDocumentHeaderAsync(int? id);
       

    }
}
