using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Saina.ApplicationCore.DTOs.Accounting.ReviewAcc
{
    public class AccDocumentHeaderFilter:PropertyObservable
    {
        public AccDocumentHeaderFilter()
        {
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _FromNumber = 1; 
             _ToNumber = 100;
            TLDocumentType = TLDocumentTypeEnum.All;
            AccountGLEnum = AccountGLEnum.BalanceSheet | AccountGLEnum.ProfitLoss | AccountGLEnum.Disciplinary;
        }
        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { SetProperty(ref _FromDate, value); }
        }
        [NotMapped]
        public string FromDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_FromDate)}/{pc.GetMonth(_FromDate)}/{pc.GetDayOfMonth(_FromDate)}");
                return result;
            }
        }
        private FinancialYear _FromFinancialYear;
        public FinancialYear FromFinancialYear
        {
            get { return _FromFinancialYear; }
            set { SetProperty(ref _FromFinancialYear, value); if (value != null) FromDate = value.StartDate; }
        }
        private FinancialYear _ToFinancialYear;
        public FinancialYear ToFinancialYear
        {
            get { return _ToFinancialYear; }
            set { SetProperty(ref _ToFinancialYear, value); if (value != null) ToDate = value.EndDate; }
        }


        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { SetProperty(ref _ToDate, value); }
        }
        [NotMapped]
        public string ToDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_ToDate)}/{pc.GetMonth(_ToDate)}/{pc.GetDayOfMonth(_ToDate)}");
                return result;
            }
        }
        private long _FromNumber;
        public long FromNumber
        {
            get { return _FromNumber; }
            set { SetProperty(ref _FromNumber, value); }
        }

        private long _ToNumber;
        public long ToNumber
        {
            get { return _ToNumber; }
            set { SetProperty(ref _ToNumber, value); }
        }

        private bool _hasOpening;
        public bool HasOpening
        {
            get { return _hasOpening; }
            set { SetProperty(ref _hasOpening, value); }
        }
        private bool _HasCurrency;
        public bool HasCurrency
        {
            get { return _HasCurrency; }
            set { SetProperty(ref _HasCurrency, value); }
        }

        private long _GLCode;
        public long Code
        {
            get { return _GLCode; }
            set { SetProperty(ref _GLCode, value); }
        }
        private long _TLCode;
        public long TLCode
        {
            get { return _TLCode; }
            set { SetProperty(ref _TLCode, value); }
        }
        private long _SLCode;
        public long SLCode
        {
            get { return _SLCode; }
            set { SetProperty(ref _SLCode, value); }
        }
        private long _DL1Code;
        public long DL1Code
        {
            get { return _DL1Code; }
            set { SetProperty(ref _DL1Code, value); }
        }
        private long _DL2Code;
        public long DL2Code
        {
            get { return _DL2Code; }
            set { SetProperty(ref _DL2Code, value); }
        }
        private long _TrackingId;
        public long TrackingId
        {
            get { return _TrackingId; }
            set { SetProperty(ref _TrackingId, value); }
        }
        private long _CurrencyId;
        public long CurrencyId
        {
            get { return _CurrencyId; }
            set { SetProperty(ref _CurrencyId, value); }
        }
        private string _CurrentMethodName;
        public string CurrentMethodName
        {
            get { return _CurrentMethodName; }
            set { SetProperty(ref _CurrentMethodName, value); }
        }

        public int? Id { get; set; }
        public long TrackingNumber { get; set; }

        /// <summary>
        /// نوع حساب
        /// </summary>
        private AccountGLEnum _accountGLEnum;

        public AccountGLEnum AccountGLEnum
        {
            get { return _accountGLEnum; }
            set { SetProperty(ref _accountGLEnum, value); }
        }

        private TLDocumentTypeEnum _TLDocumentType;
        public TLDocumentTypeEnum TLDocumentType
        {
            get { return _TLDocumentType; }
            set { SetProperty(ref _TLDocumentType, value); }
        }

    }
}
