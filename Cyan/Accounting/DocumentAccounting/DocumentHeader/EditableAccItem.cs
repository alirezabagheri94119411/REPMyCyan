using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
     
     public class EditableAccItem : BindableBase
    {
        /// <summary>
        /// آی دی آیتم سند حسابداری
        /// </summary>
        private int _accDocumentItemId;

        public int AccDocumentItemId
        {
            get { return _accDocumentItemId; }
            set { _accDocumentItemId = value; }
        }

     
        /// <summary>
        /// آی دی d
        /// </summary>
        private int _sLId;
        public int SLId
        {
            get { return _sLId; }
            set { SetProperty(ref _sLId, value); }
        }
        /// <summary>
        /// عنوان تفصیل 1 معین
        /// </summary>
       
        /// کد تفصیل 1 معین
        /// </summary>
        private int? _dL1Id;
        public int? DL1Id
        {
            get { return _dL1Id; }
            set { SetProperty(ref _dL1Id, value); }
        }
       
        /// <summary>
        /// کد تفصیل 2 معین
        /// </summary>
        private int? _dL2Id;

        public int? DL2Id
        {
            get { return _dL2Id; }
            set { SetProperty(ref _dL2Id, value); }
        }
        /// <summary>
        /// آرتیکل 1
        /// </summary>
        private string _description1;

        public string Description1
        {
            get { return _description1; }
            set { SetProperty(ref _description1, value); }
        }
        /// <summary>
        /// آرتیکل 2
        /// </summary>
        private string _description2;

        public string Description2
        {
            get { return _description2; }
            set { SetProperty(ref _description2, value); }
        }
        /// <summary>
        /// بدهکار
        /// </summary>
        private double _debit;

        public double Debit
        {
            get { return _debit; }
            set
            {
                SetProperty(ref _debit, value);
                //if (_debit != 0) { _credit = 0; OnPropertyChanged(nameof(Credit)); }
            }
        }
        /// <summary>
        /// بستانکار
        /// </summary>
        private double _credit;

        public double Credit
        {
            get { return _credit; }
            set
            {
                SetProperty(ref _credit, value);
                //if(_credit!=0)_debit = 0;OnPropertyChanged(nameof(Debit)); }
            }
        }
        /// <summary>
        /// مبلغ ارز
        /// </summary>
        private double _currencyAmount;

        public double CurrencyAmount
        {
            get { return _currencyAmount; }
            set { SetProperty(ref _currencyAmount, value); }
        }
        ///// <summary>
        ///// نرخ ارز
        ///// </summary>
        //private ExchangeRate _exchangeRate;

        //public virtual ExchangeRate ExchangeRate
        //{
        //    get { return _exchangeRate; }
        //    set { _exchangeRate = value; }
        //}
        /// <summary>
        /// نرخ ارز
        /// </summary>
        private double _exchangeRate;

        public double ExchangeRate
        {
            get { return _exchangeRate; }
            set { SetProperty(ref _exchangeRate, value); }
        }
        /// <summary>
        /// نام ارز
        /// </summary>
       
        private int? _currencyId;

        public int? CurrencyId
        {
            get { return _currencyId; }
            set { SetProperty(ref _currencyId, value); }
        }
        /// <summary>
        /// تاریخ پیگیری
        /// </summary>
        //private DateTime _trackingDate;

        //public DateTime TrackingDate
        //{
        //    get { return _trackingDate; }
        //    set { _trackingDate = value; }
        //}
        //  [Column(TypeName = "smalldatetime")]
        //  public DateTime? TrackingDate { get; set; }
        private DateTime? _TrackingDate;
        public DateTime? TrackingDate
        {
            get { return _TrackingDate; }
            set { SetProperty(ref _TrackingDate, value); }
        }

       
        /// <summary>
        /// شماره پیگیری
        /// </summary>
        private long _trackingNumber;

        public long TrackingNumber
        {
            get { return _trackingNumber; }
            set { SetProperty(ref _trackingNumber, value); }
        }

        /// <summary>
        /// هدر سند حسابداری
        /// </summary>
      
        /// <summary>
        /// هدر سند حسابداری
        /// </summary>
        private int _accDocumentHeaderId;

        public int AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { SetProperty(ref _accDocumentHeaderId, value); }
        }
        //[NotMapped]
        //public double Remain => Debit - Credit;

    }
}
