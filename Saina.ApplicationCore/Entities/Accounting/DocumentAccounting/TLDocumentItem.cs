using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// جدول سند کل
    /// </summary>
    [Table("TLDocumentItems", Schema = "Acc")]
    public class TLDocumentItem:BaseEntity
    {
        private int _tLDocumentItemId;

        /// <summary>
        /// آی دی حساب کل
        /// </summary>
        public int TLDocumentItemId
        {
            get { return _tLDocumentItemId; }
            set { _tLDocumentItemId = value; }
        }
        private long _tLDocumentItemCode;

        /// <summary>
        /// کد سند کل
        /// </summary>
        public long TLDocumentItemCode
        {
            get { return _tLDocumentItemCode; }
            set { _tLDocumentItemCode = value; }
        }
        private DateTime _tLDocumentItemDate;
        /// <summary>
        /// تاریخ سند
        /// </summary>
     

        public DateTime TLDocumentItemDate
        {
            get { return _tLDocumentItemDate; }
            set { _tLDocumentItemDate = value; }
        }
        [NotMapped]
        public string TLDocumentItemDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_tLDocumentItemDate)}/{pc.GetMonth(_tLDocumentItemDate)}/{pc.GetDayOfMonth(_tLDocumentItemDate)}");
                return result;
            }
        }
        private  string tLTitle;
        /// <summary>
        /// نام حساب کل
        /// </summary>
        public virtual string TLTitle
        {
            get { return tLTitle; }
            set { tLTitle = value; }
        }
        private int _tLId;
        /// <summary>
        /// آی دی حساب کل
        /// </summary>
        public int TLId
        {
            get { return _tLId; }
            set { _tLId = value; }
        }
        private double _credit;

        public double Credit
        /// <summary>
        /// مانده بستانکار
        /// </summary>
        {
            get { return _credit; }
            set { _credit = value; }
        }
        private double _debit;
        /// <summary>
        /// مانده بدهکار
        /// </summary>

        public double Debit
        {
            get { return _debit; }
            set { _debit = value; }
        }
        /// <summary>
        /// هدر سند کل
        /// </summary>
        private TLDocumentHeader _tLDocumentHeader;

        public TLDocumentHeader TLDocumentHeader
        {
            get { return _tLDocumentHeader; }
            set { _tLDocumentHeader = value; }
        }  /// <summary>
           /// هدر سند کل
           /// </summary>
        private int _tLDocumentHeaderId;

        public int TLDocumentHeaderId
        {
            get { return _tLDocumentHeaderId; }
            set { _tLDocumentHeaderId = value; }
        }

    }
}
