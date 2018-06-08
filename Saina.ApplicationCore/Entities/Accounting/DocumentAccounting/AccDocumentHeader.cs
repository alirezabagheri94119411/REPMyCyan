using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    [Flags]
    public enum StatusEnum
    {
        [Description("پیش نویس")]//Temporary(if Balanced)
        Draft = 0,
        [Description("موقت")]//Draft(if changed) | End
        Temporary = 1,
        [Description("خاتمه")]//Draft(if changed) | BackFromEnd(if not Draft)
        End = 2,
        [Description("دائم")]
        Permanent = 4,

    }
 
    /// <summary>
    /// جدول هدر سند حسابداری
    /// </summary>
    [Table("AccDocumentHeaders", Schema = "Acc")]
    public class AccDocumentHeader : BaseEntity
    {
       
        public AccDocumentHeader()
        {
            AccDocumentItems = new ObservableCollection<AccDocumentItem>();
            Attachments = new ObservableCollection<Attachment>();
        }
        /// <summary>
        /// آی دی هدر سند حسابداری
        /// </summary>
        private int _accDocumentHeaderId;
        
        public int AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { _accDocumentHeaderId = value; }
        }
        
        /// <summary>
        /// شماره سند
        /// </summary>
        private long _documentNumber;
        [Required(ErrorMessage ="شماره سند نباید خالی باشد")]
        public long DocumentNumber
        {
            get { return _documentNumber; }
            set
            {
                //if (value != _documentNumber)
                //{

                //    ValidationContext validationContext = new ValidationContext(this, null, null);
                //    validationContext.MemberName = "DocumentNumber";
                //    Validator.ValidateProperty(value, validationContext);
                //    this._documentNumber = value;
                //    this.OnPropertyChanged("DocumentNumber");
                //}
                SetProperty(ref _documentNumber , value);
            }
        } /// <summary>
          /// شماره روزانه
          /// </summary>
        private long _dailyNumber;

        public long DailyNumber
        {
            get { return _dailyNumber; }
            set { SetProperty(ref _dailyNumber, value); }
        }

        /// <summary>
        /// شماره ثابت سیستم
        /// </summary>
        private long _systemFixNumber;

        public long SystemFixNumber
        {
            get { return _systemFixNumber; }
            set { SetProperty(ref _systemFixNumber, value); }
        }
        /// <summary>
        /// تاریخ سند
        /// </summary>
        private DateTime _documentDate = DateTime.Now;

        public DateTime DocumentDate
        {
            get {
                return _documentDate;
            }
            set { SetProperty(ref _documentDate, value); }

        }

        [NotMapped]
        public string DocumentDateString
        {
            get {
                PersianCalendar pc = new PersianCalendar();
                var getMonth= string.Format($"{pc.GetMonth(_documentDate)}");
                var getDay = string.Format($"{pc.GetDayOfMonth(_documentDate)}");
                if (Convert.ToInt16(getMonth)<10)
                {
                    getMonth = "0" + getMonth;
                }
                if (Convert.ToInt16(getDay) < 10)
                {
                    getDay = "0" + getDay;
                }
                string result = string.Format($"{pc.GetYear(_documentDate)}/{getMonth}/{getDay}");
               // string result = string.Format($"{pc.GetYear(_documentDate)}/{pc.GetMonth(_documentDate)}/{pc.GetDayOfMonth(_documentDate)}");
                return result; }
        }

        /// <summary>
        /// نام سیستم
        /// </summary>
        private string _systemName;

        public string SystemName
        {
            get { return _systemName; }
            set { SetProperty(ref _systemName, value); }
        }/// <summary>
         /// صادر کننده
         /// </summary>
        private string _exporter;

        public string Exporter
        {
            get { return _exporter; }
            set { SetProperty(ref _exporter, value); }
        }
        /// <summary>
        /// تایید کننده
        /// </summary>
        private string _seconder;

        public string Seconder
        {
            get { return _seconder; }
            set { SetProperty(ref _seconder, value); }
        }
        /// <summary>
        /// شرح هدر سند
        /// </summary>
        private string _headerDescription;

        public string HeaderDescription
        {
            get { return _headerDescription; }
            set { SetProperty(ref _headerDescription, value); }
        }


        /// <summary>
        /// شماره سند دستی
        /// </summary>
        private long _manualDocumentNumber;

        public long ManualDocumentNumber
        {
            get { return _manualDocumentNumber; }
            set { SetProperty(ref _manualDocumentNumber, value); }
        }
        private string _baseDocument;

        public string BaseDocument
        {
            get { return _baseDocument; }
            set { SetProperty(ref _baseDocument, value); }
        }
        /// <summary>
        /// تاریخچه سند
        /// </summary>
        //private string _documentHistory;

        //public string DocumentHistory
        //{
        //    get { return _documentHistory; }
        //    set { _documentHistory = value; }
        //}

        /// <summary>
        /// وضعیت
        /// </summary>
        private StatusEnum _status;

        public StatusEnum Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }

        }
        /// <summary>
        /// نوع سند
        /// </summary>
        private TypeDocument _typeDocument;

        public virtual TypeDocument TypeDocument
        {
            get { return _typeDocument; }
            set { SetProperty(ref _typeDocument, value); }
        }
        private int? _typeDocumentId;

        public int? TypeDocumentId
        {
            get { return _typeDocumentId; }
            set { SetProperty(ref _typeDocumentId, value); }
        }

        /// <summary>
        /// مبلغ سند
        /// </summary>
        private double _amountDocument;

        public double AmountDocument
        {
            get { return _amountDocument; }
            set { SetProperty(ref _amountDocument, value); }
        }

        /// <summary>
        /// لیستی از آیتم های سند حسابداری
        /// </summary>
        private ObservableCollection<AccDocumentItem> _AccDocumentItems;
        public ObservableCollection<AccDocumentItem> AccDocumentItems
        {
            get { return _AccDocumentItems; }
            set { SetProperty(ref _AccDocumentItems, value); }
        }

        public virtual ICollection<Attachment> Attachments { get; set; }
        [NotMapped]
        public double SumDebit => AccDocumentItems.Sum(x => x.Debit);
        [NotMapped]
        public double SumCredit => AccDocumentItems.Sum(x => x.Credit);
        [NotMapped]
        public double Difference => Math.Abs(SumDebit - SumCredit);

        [NotMapped]
        public bool IsReadOnly { get; set; }
    }
  
 
}

