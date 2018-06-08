using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    [Table("AccDocumentItems", Schema = "Acc")]
    public class AccDocumentItem : BaseEntity
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
        [ForeignKey("SLId")]
        public SL SL
        {
            get { return _sL; }
            set { SetProperty(ref _sL, value); }
        }
        /// <summary>
        /// آی دی d
        /// </summary>
        private int _sLId;
        [Required(ErrorMessage ="معین نمی تواند خالی باشد..")]
        public int SLId
        {
            get { return _sLId; }
            set {SetProperty(ref _sLId , value); }
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
        [ForeignKey("DL1")]
        public int? DL1Id
        {
            get { return _dL1Id; }
            set {SetProperty(ref _dL1Id , value); }
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
        [ForeignKey("DL2")]

        public int? DL2Id
        {
            get { return _dL2Id; }
            set {SetProperty(ref _dL2Id , value); }
        }
        /// <summary>
        /// آرتیکل 1
        /// </summary>
        private string _description1;

        public string Description1
        {
            get { return _description1; }
            set {SetProperty(ref _description1 , value); }
        }
        /// <summary>
        /// آرتیکل 2
        /// </summary>
        private string _description2;

        public string Description2
        {
            get { return _description2; }
            set {SetProperty(ref _description2 , value); }
        }
        /// <summary>
        /// بدهکار
        /// </summary>
        private double _debit;

        public double Debit
        {
            get { return _debit; }
            set { SetProperty(ref _debit , value );
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
            set {SetProperty(ref _currencyAmount , value); }
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
            set {SetProperty(ref _exchangeRate , value); }
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
            set {SetProperty(ref _currencyId , value); }
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
        private bool _HasExchange;

        public bool HasExchange
        {
            get { return _HasExchange; }
            set { SetProperty(ref _HasExchange, value); }
        }


        [NotMapped]
        public string TrackingDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_TrackingDate.Value)}/{pc.GetMonth(_TrackingDate.Value)}/{pc.GetDayOfMonth(_TrackingDate.Value)}");
                return result;
            }
        }
        /// <summary>
        /// شماره پیگیری
        /// </summary>
        private long _trackingNumber;

        public long TrackingNumber
        {
            get { return _trackingNumber; }
            set {SetProperty(ref _trackingNumber , value); }
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
            set {SetProperty(ref _accDocumentHeaderId , value); }
        }
        //[NotMapped]
        //public double Remain => Debit - Credit;
   
        public bool HasCurrency => SL?.Property.HasFlag(PropertyEnum.Exchange)==true;
    }
}
