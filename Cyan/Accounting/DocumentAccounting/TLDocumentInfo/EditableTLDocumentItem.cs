using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo
{
    /// <summary>
    /// آیتم سند کل
    /// </summary>
   public class EditableTLDocumentItem:ValidatableBindableBase
    {
        private int _tLDocumentItemId;

        /// <summary>
        /// آی دی حساب کل
        /// </summary>
        public int TLDocumentItemId
        {
            get { return _tLDocumentItemId; }
            set { SetProperty(ref _tLDocumentItemId, value); }
        }
        private long _tLDocumentItemCode;

        /// <summary>
        /// کد سند کل
        /// </summary>
        public long TLDocumentItemCode
        {
            get { return _tLDocumentItemCode; }
            set { SetProperty(ref _tLDocumentItemCode, value);  }
        }
        private DateTime _tLDocumentItemDate;
        /// <summary>
        /// تاریخ سند
        /// </summary>
        

        public DateTime TLDocumentItemDate
        {
            get { return _tLDocumentItemDate; }
            set { SetProperty(ref _tLDocumentItemDate, value); _tLDocumentItemDate = value; }
        }
        
        private int? _tLId;
        /// <summary>
        /// آی دی حساب کل
        /// </summary>
        public int? TLId
        {
            get { return _tLId; }
            set { SetProperty(ref _tLId, value);}
        }
        private double _credit;

        public double Credit
        /// <summary>
        /// مانده بستانکار
        /// </summary>
        {
            get { return _credit; }
            set { SetProperty(ref _credit, value);  }
        }
        private double _debit;
        /// <summary>
        /// مانده بدهکار
        /// </summary>

        public double Debit
        {
            get { return _debit; }
            set { SetProperty(ref _debit, value); }
        }
        /// <summary>
        /// هدر سند کل
        /// </summary>
       
        private int _tLDocumentHeaderId;

        public int TLDocumentHeaderId
        {
            get { return _tLDocumentHeaderId; }
            set { SetProperty(ref _tLDocumentHeaderId, value); }
        }

    }
}
