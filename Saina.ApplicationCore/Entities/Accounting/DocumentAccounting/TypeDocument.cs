using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// جدول انواع سند حسابداری
    /// </summary>
    [Table("TypeDocuments", Schema = "Acc")]
    public class TypeDocument : BaseEntity
    {
        /// <summary>
        /// آی دی انواع سند حسابداری
        /// </summary>
        private int _typeDocumentId;

        public int TypeDocumentId
        {
            get { return _typeDocumentId; }
            set { _typeDocumentId = value; }
        }
        /// <summary>
        /// عنوان انواع سند حسابداری
        /// </summary>
        private string _typeDocumentTitle;

        public string TypeDocumentTitle
        {
            get { return _typeDocumentTitle; }
            set { _typeDocumentTitle = value; }
        }


    }
}
