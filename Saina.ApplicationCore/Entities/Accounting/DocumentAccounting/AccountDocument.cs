using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// جدول اسناد
    /// </summary>
    [Table("AccountDocuments", Schema = "Info")]
    public class AccountDocument : BaseEntity
    {

        public AccountDocument()
        {
            DocumentNumberings = new ObservableCollection<DocumentNumbering>();
        }

      
        /// <summary>
        /// آی دی سند
        /// </summary>
        private int _accountDocumentId;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountDocumentId
        {
            get { return _accountDocumentId; }
            set { _accountDocumentId = value; }

        }
        /// <summary>
        /// عنوان سند
        /// </summary>
        private string _accountDocumentTitle;

        public string AccountDocumentTitle
        {
            get { return _accountDocumentTitle; }
            set { _accountDocumentTitle = value; }
        }

        /// <summary>
        /// لیستی از شماره گذاری اسناد
        /// </summary>
        public virtual ICollection<DocumentNumbering> DocumentNumberings { get; set; }
    }
}
