using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo
{
    /// <summary>
    /// هدر سند کل
    /// </summary>
    public class EditableTLDocumentHeader : ValidatableBindableBase
    {
        public EditableTLDocumentHeader()
        {
            TLDocumentExport = TLDocumentExportEnum.Daily;
        }
        private int _tLDocumentHeaderId;
        /// <summary>
        /// آی دی هدر سند کل
        /// </summary>
        public int TLDocumentHeaderId
        {
            get { return _tLDocumentHeaderId; }
            set
            {
                SetProperty(ref _tLDocumentHeaderId, value);

            }

        }
        /// <summary>
        /// شماره سند
        /// </summary>
        /// <summary>
        ///شماره سند کل
        /// </summary>
        private int _tLDocumentNumber;

        public int TLDocumentNumber
        {
            get { return _tLDocumentNumber; }
            set { SetProperty(ref _tLDocumentNumber, value); }
        }
        private DateTime _tLDocumentHeaderDate;
        /// <summary>
        /// تاریخ سند کل
        /// </summary>

        public DateTime TLDocumentHeaderDate
        {
            get { return _tLDocumentHeaderDate; }
            set
            {
                SetProperty(ref _tLDocumentHeaderDate, value);
            }
        }
        private TLDocumentTypeEnum _tLDocumentType;
        /// <summary>
        /// نوع سند کل
        /// </summary>
        public TLDocumentTypeEnum TLDocumentType
        {
            get { return _tLDocumentType; }
            set
            {
                SetProperty(ref _tLDocumentType, value);
            }
        }
        private TLDocumentExportEnum _tLDocumentExport;
        /// <summary>
        /// نوع صدور سند کل
        /// </summary>
        public TLDocumentExportEnum TLDocumentExport
        {
            get { return _tLDocumentExport; }
            set
            {
                SetProperty(ref _tLDocumentExport, value);
            }
        }
        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }


    }
}
