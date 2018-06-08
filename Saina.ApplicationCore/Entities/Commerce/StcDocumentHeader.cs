using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
  public  enum StcTypeDocumentEnum
    {
        [Description("خرید داخلی")]
        BuyLocal=1,
        [Description("خریدوارداتی")]
        BuyImported = 2,
        [Description("تولید")]
        Production = 4,
        [Description("سایر")]
        Other = 8,
        [Description("موجودی اول دوره")]
        FirstStockVolume = 16
        //Input=1,
        //Output=2,
        //ReturnInput=4,
        //ReturnOutput=8,
        //TransferBetweenStock =16  
    }
   public enum OptionTypeDocEnum
    {

    }
   public enum BaseDocumentEnum
    {

    }
   public enum StcStatusEnum
    {

    }
  public  class StcDocumentHeader:BaseEntity
    {
        private int _StcDocumentHeaderId;
        /// <summary>
        /// آی دی هدر سند
        /// </summary>
        public int StcDocumentHeaderId
        {
            get { return _StcDocumentHeaderId; }
            set { SetProperty(ref _StcDocumentHeaderId, value); }
        }
        private string _StcDocumentHeaderTitle;
        /// <summary>
        /// عنوان هدر سند انبار
        /// </summary>
        public string StcDocumentHeaderTitle
        {
            get { return _StcDocumentHeaderTitle; }
            set { SetProperty(ref _StcDocumentHeaderTitle, value); }
        }
        private StcTypeDocument _StcTypeDocument;
        public virtual StcTypeDocument StcTypeDocument
        {
            get { return _StcTypeDocument; }
            set { SetProperty(ref _StcTypeDocument, value); }
        }

        //private StcTypeDocumentEnum _StcTypeDocumentEnum;
        ///// <summary>
        ///// نوع سند انبار
        ///// </summary>
        //public StcTypeDocumentEnum StcTypeDocumentEnum
        //{
        //    get { return _StcTypeDocumentEnum; }
        //    set { SetProperty(ref _StcTypeDocumentEnum, value); }
        //}
        //private OptionTypeDocEnum _TypeOptionEnum;
        ///// <summary>
        ///// نوع انتخابی سند
        ///// </summary>
        //public OptionTypeDocEnum TypeOptionEnum
        //{
        //    get { return _TypeOptionEnum; }
        //    set { SetProperty(ref _TypeOptionEnum, value); }
        //}

        private DL _DL1;
        /// <summary>
        /// تفصیل1
        /// </summary>
        public DL DL1
        {
            get { return _DL1; }
            set { SetProperty(ref _DL1, value); }
        }
        private int? _DL1Id;
        [ForeignKey("DL1")]
        public int? DL1Id
        {
            get { return _DL1Id; }
            set { SetProperty(ref _DL1Id, value); }
        }

        private DL _DL2;
        /// <summary>
        /// تفصیل2
        /// </summary>
        public DL DL2
        {
            get { return _DL2; }
            set { SetProperty(ref _DL2, value); }
        }
        private int? _DL2Id;
        [ForeignKey("DL2")]
        public int? DL2Id
        {
            get { return _DL2Id; }
            set { SetProperty(ref _DL2Id, value); }
        }

        private SL _SL;
        /// <summary>
        /// معین
        /// </summary>
        public SL SL
        {
            get { return _SL; }
            set { SetProperty(ref _SL, value); }
        }
        private int _SLId;
        public int SLId
        {
            get { return _SLId; }
            set { SetProperty(ref _SLId, value); }
        }


        private BaseDocumentEnum _BaseDocumentEnum;
        /// <summary>
        /// سند مبنا
        /// </summary>
        public BaseDocumentEnum BaseDocumentEnum
        {
            get { return _BaseDocumentEnum; }
            set { SetProperty(ref _BaseDocumentEnum, value); }
        }
        private int _ReceiptNumber;
        /// <summary>
        /// شماره رسید
        /// </summary>
        public int ReceiptNumber
        {
            get { return _ReceiptNumber; }
            set { SetProperty(ref _ReceiptNumber, value); }
        }
        private DateTime? _ReceiptDate;
        /// <summary>
        /// تاریخ رسید
        /// </summary>
        public DateTime? ReceiptDate
        {
            get { return _ReceiptDate; }
            set { SetProperty(ref _ReceiptDate, value); }
        }
        private int _InvoiceNumber;
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public int InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { SetProperty(ref _InvoiceNumber, value); }
        }
        private DateTime? _InvoiceDate;
        /// <summary>
        /// تاریخ فاکتور
        /// </summary>
        public DateTime? InvoiceDate
        {
            get { return _InvoiceDate; }
            set { SetProperty(ref _InvoiceDate, value); }
        }
        private DateTime? _TrackingDate;
        /// <summary>
        /// تاریخ پیگیری
        /// </summary>
        public DateTime? TrackingDate
        {
            get { return _TrackingDate; }
            set { SetProperty(ref _TrackingDate, value); }
        }
        private StcStatusEnum _StcStatusEnum;
        /// <summary>
        /// وضعیت سند انبار
        /// </summary>
        public StcStatusEnum StcStatusEnum
        {
            get { return _StcStatusEnum; }
            set { SetProperty(ref _StcStatusEnum, value); }
        }

        private string _Exporter;
        /// <summary>
        /// صادر کننده
        /// </summary>
        public string Exporter
        {
            get { return _Exporter; }
            set { SetProperty(ref _Exporter, value); }
        }
        private string _StcDiscription;
        /// <summary>
        /// شرح سند
        /// </summary>
        public string StcDiscription
        {
            get { return _StcDiscription; }
            set { SetProperty(ref _StcDiscription, value); }
        }

    }
}
