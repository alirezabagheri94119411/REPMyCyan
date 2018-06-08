using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    public enum TLDocumentExportEnum
    {
        [Description("روزانه")]
        Daily = 1,
        [Description("ماهانه")]
       Monthly = 2,
       
    }
  public  enum TLDocumentTypeEnum
    {
        [Description("عمومی")]

        None = 1,
        [Description("افتتاحیه")]

        Opening = 2,
        [Description("اختتامیه")]

        Closing = 4,
        [Description("تسعیر")]

        Litter = 8,
        [Description("سود و زیانی")]

        Profit = 16,
        All=None|Opening|Closing|Litter
    }
    /// <summary>
    /// جدول هدر سند کل
    /// </summary>
    [Table("TLDocumentHeaders", Schema = "Acc")]
    public class TLDocumentHeader : BaseEntity
    {
        public TLDocumentHeader()
        {
            TLDocumentItems = new ObservableCollection<TLDocumentItem>();
            TLDocumentExport = TLDocumentExportEnum.Daily;
        }
        private int _tLDocumentHeaderId;
        /// <summary>
        /// آی دی هدر سند کل
        /// </summary>
        public int TLDocumentHeaderId
        {
            get { return _tLDocumentHeaderId; }
            set { _tLDocumentHeaderId = value; }
        }
        /// <summary>
        ///شماره سند کل
        /// </summary>
        private int _tLDocumentNumber;

        public int TLDocumentNumber
        {
            get { return _tLDocumentNumber; }
            set { _tLDocumentNumber = value; }
        }

        private DateTime _tLDocumentHeaderDate;
        /// <summary>
        /// تاریخ سند کل
        /// </summary>
      

        public DateTime TLDocumentHeaderDate
        {
            get { return _tLDocumentHeaderDate; }
            set { _tLDocumentHeaderDate = value; }
        }
        [NotMapped]
        public string TLDocumentHeaderDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_tLDocumentHeaderDate)}/{pc.GetMonth(_tLDocumentHeaderDate)}/{pc.GetDayOfMonth(_tLDocumentHeaderDate)}");
                return result;
            }
        }
        private TLDocumentTypeEnum _tLDocumentType;
        /// <summary>
        /// نوع سند کل
        /// </summary>
        public TLDocumentTypeEnum TLDocumentType
        {
            get { return _tLDocumentType; }
            set { _tLDocumentType = value; }
        }
        private TLDocumentExportEnum _tLDocumentExport;
        /// <summary>
        /// نوع صدور سند کل
        /// </summary>
        public TLDocumentExportEnum TLDocumentExport
        {
            get { return _tLDocumentExport; }
            set { _tLDocumentExport = value; }
        }
        /// <summary>
        /// لیستی از آیتم های سند حسابداری
        /// </summary>
        private DateTime _toDate;

        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }
        [NotMapped]
        public string ToDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_toDate)}/{pc.GetMonth(_toDate)}/{pc.GetDayOfMonth(_toDate)}");
                return result;
            }
        }
        private long _toNumber;

        public long ToNumber
        {
            get { return _toNumber; }
            set { _toNumber = value; }
        }

        public virtual ICollection<TLDocumentItem> TLDocumentItems { get; set; }


    }
}
