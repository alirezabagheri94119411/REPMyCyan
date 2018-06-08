using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using Saina.Common.Enums;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    public enum CloumnStatusEnum
    {
        DL2=1,
        Discription2=2,
        Currency=4,
        ExchangeRate=8,
        Amount=16,
        Tracking=32,
        TrackingDate=64
    }
   public class EditableAccDocumentHeader:ValidatableBindableBase,IStateObject
    {
        public EditableAccDocumentHeader()
        {
            IsReadOnly = Status == StatusEnum.Permanent;
            State = ObjectState.Added;
        }
        /// <summary>
        /// آی دی هدر سند حسابداری
        /// </summary>
        private int _accDocumentHeaderId;

        public int AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { SetProperty(ref _accDocumentHeaderId, value); }
        }
        /// <summary>
        /// شماره سند
        /// </summary>
        private long _documentNumber;
        [Required(ErrorMessage = "لطفا شماره سند را وارد نمایید.")]

        public long DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value); }
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
        [Required(ErrorMessage = "لطفا شماره  ثابت سیستم را وارد نمایید.")]

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
            get { return _documentDate; }
            set { SetProperty(ref _documentDate, value); }

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
        [Required(ErrorMessage = "لطفا شماره سند دستی را وارد نمایید.")]

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
        //    set { SetProperty(ref _documentHistory, value); }
        //}

        ///// <summary>
        ///// وضعیت
        ///// </summary>
        private StatusEnum _status;

        public StatusEnum Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        /// <summary>
        /// نوع سند
        /// </summary>
        private int? _typeDocumentId;
     //   [Required(ErrorMessage = "لطفا شماره سند دستی را وارد نمایید.")]

        public int? TypeDocumentId
        {
            get { return _typeDocumentId; }
            set { SetProperty(ref _typeDocumentId, value);
              
            }
        }

        /// <summary>
        /// مکاتبات
        /// </summary>
        private string _attachment;

        public string Attachment
        {
            get { return _attachment; }
            set { SetProperty(ref _attachment, value); }
        }
        /// <summary>
        /// مبلغ سند
        /// </summary>
        private double _AmountDocument;
        public double AmountDocument
        {
            get { return _AmountDocument; }
            set { SetProperty(ref _AmountDocument, value); }
        }

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }

        private double _sumDebit;

        public double SumDebit
        {
            get { return _sumDebit; }
            set { SetProperty(ref _sumDebit, value); Difference = Math.Abs(SumDebit - SumCredit); }

        }
        private double _sumCredit;

        public double SumCredit
        {
            get { return _sumCredit; }
            set { SetProperty(ref _sumCredit, value); Difference = Math.Abs(SumDebit - SumCredit); }

        }
        private double _difference;

        public double Difference
        {
            get { return _difference; }
            set { SetProperty(ref _difference, value);if(AccDocumentHeaderId!=0)
                    State = ObjectState.Modified; }

        }
        private CloumnStatusEnum _cloumnStatus;
        public CloumnStatusEnum CloumnStatus
        {
            get { return _cloumnStatus; }
            set { SetProperty(ref _cloumnStatus, value); }
        }
            //public  ICollection<EditableAccDocumentItem> AccDocumentItems { get; set; }
        public ObjectState State { get; set; }
    }
}
