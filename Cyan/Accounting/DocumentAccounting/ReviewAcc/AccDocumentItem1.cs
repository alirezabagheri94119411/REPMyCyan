using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Globalization;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    public class AccDocumentItem1:BindableBase
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
        /// عنوان کد معین
        /// </summary>
        private SL _sL;
        public SL SL
        {
            get { return _sL; }
            set { _sL = value; }
        }
        /// <summary>
        /// آی دی کد معین
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
        private DL dL1;


        public virtual DL DL1
        {
            get { return dL1; }
            set { dL1 = value; }

        }
        /// <summary>
        /// کد تفصیل 1 معین
        /// </summary>
        private int? _dL1Id;
        public int? DL1Id
        {
            get { return _dL1Id; }
            set { SetProperty(ref _dL1Id, value); }
        }
        /// <summary>
        /// عنوان تفصیل 2 معین
        /// </summary>
        private DL _dL2;

        public virtual DL DL2
        {
            get { return _dL2; }
            set { _dL2 = value; }
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
        private Currency _currency;

        public virtual Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
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
        public DateTime? TrackingDate { get; set; }
        /// <summary>
        /// شماره پیگیری
        /// </summary>
        private long? _trackingNumber;

        public long? TrackingNumber
        {
            get { return _trackingNumber; }
            set { SetProperty(ref _trackingNumber, value); }
        }

        /// <summary>
        /// هدر سند حسابداری
        /// </summary>
        private AccDocumentHeader _accDocumentHeader;

        public virtual AccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { _accDocumentHeader = value; }
        }
        /// <summary>
        /// هدر سند حسابداری
        /// </summary>
        private int _accDocumentHeaderId;

        public int AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { SetProperty(ref _accDocumentHeaderId, value); }
        }
        private long _documentNumber;
        public long DocumentNumber
        {
            get { return _documentNumber; }
            set { _documentNumber = value; }
        }


        //[NotMapped]
        //public double Remain => Debit - Credit;

        private DateTime _DocumentDate;

        public DateTime DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }
        public string DocumentDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                var getMonth = string.Format($"{pc.GetMonth(_DocumentDate)}");
                var getDay = string.Format($"{pc.GetDayOfMonth(_DocumentDate)}");
                if (Convert.ToInt16(getMonth) < 10)
                {
                    getMonth = "0" + getMonth;
                }
                if (Convert.ToInt16(getDay) < 10)
                {
                    getDay = "0" + getDay;
                }
                string result = string.Format($"{pc.GetYear(_DocumentDate)}/{getMonth}/{getDay}");
                // string result = string.Format($"{pc.GetYear(_documentDate)}/{pc.GetMonth(_documentDate)}/{pc.GetDayOfMonth(_documentDate)}");
                return result;
            }
        }
        private int _Status;

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        public bool HasCurrency => SL?.Property.HasFlag(PropertyEnum.Exchange) == true;

        public double Runningtotal { get; internal set; }
        public string SLTitle { get; internal set; }
        public long SLCode { get; internal set; }
        public int? DLCode2 { get; internal set; }
        public int? DLCode1 { get; internal set; }
        public string CurrencyTitle { get; internal set; }
        public string DLTitle1 { get; internal set; }
        public string DLTitle2 { get; internal set; }
        public double  Result { get; set; }
    }
}