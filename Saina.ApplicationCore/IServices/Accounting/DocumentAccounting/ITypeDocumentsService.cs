using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// آی سرویس نوع سند حسابداری
    /// </summary>
  public  interface ITypeDocumentsService
    {
        Task<List<TypeDocument>> GetTypeDocumentsAsync();
        Task<TypeDocument> GetTypeDocumentAsync(int id);
        Task<TypeDocument> AddTypeDocumentAsync(TypeDocument typeDocument);
        Task<TypeDocument> UpdateTypeDocumentAsync(TypeDocument typeDocument);
        Task DeleteTypeDocumentAsync(int typeDocumentId);
        bool ContextHasChanges { get; }
        Task<bool> HasTitle(string title);

    }
}
