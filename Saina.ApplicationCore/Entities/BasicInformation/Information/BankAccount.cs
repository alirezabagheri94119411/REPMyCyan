using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{

    /// <summary>
    /// اطلاعات حساب بانکی
    /// </summary>
   public class BankAccount:BaseEntity
    {
        public BankAccount()
        {
            RelatedPeople = new ObservableCollection<RelatedPerson>();
            Status = true;
        }
        /// <summary>
        /// آی دی حساب بانک
        /// </summary>
     
        private int _bankAccountId;

        public int BankAccountId
        {
            get { return _bankAccountId; }
            set
            {
                _bankAccountId= value;
            }
        }
        /// <summary>
        /// کد تفصیل
        /// </summary>
        private int _dCode;

        public int Dcode
        {
            get { return  _dCode; }
            set {  _dCode = value; }
        }

        /// <summary>
        /// نام بانک
        /// </summary>

        private Bank _bank;

        public virtual Bank Bank
        {
            get { return _bank; }
            set
            {
                _bank= value;
            }
        }
        /// <summary>
        /// آی دی بانک
        /// </summary>
        private int? _bankId;

        public int? BankId
        {
            get { return _bankId; }
            set
            {
                SetProperty(ref _bankId, value);
            }
        }
      

        /// <summary>
        /// شماره حساب
        /// </summary>
        private long _accountNumber;

        public long AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                _accountNumber= value;
            }
        }

        /// <summary>
        /// شعبه
        /// </summary>
        private string _branch;

        public string Branch
        {
            get { return _branch; }
            set
            {
                _branch= value;
            }
        }


        /// <summary>
        /// نوع حساب
        /// </summary>
        private AccountType _accountType;

        public virtual AccountType AccountType
        {
            get { return _accountType; }
            set
            {
                _accountType= value;
            }
        }
        private int? _accountTypeId;

        public int? AccountTypeId
        {
            get { return _accountTypeId; }
            set { _accountTypeId = value; }
        }


        /// <summary>
        /// شماره شبا
        /// </summary>
        private string _shabaNumber;

        public string ShabaNumber
        {
            get { return _shabaNumber; }
            set
            {
                _shabaNumber= value;
            }
        }


    

        /// <summary>
        /// آدرس
        /// </summary>
        private string _addrress;

        public string Addrress
        {
            get { return _addrress; }
            set
            {
                _addrress= value;
            }
        }


        /// <summary>
        /// کد پستی
        /// </summary>
        private long _postalCode;

        public long PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode= value;
            }
        }

        /// <summary>
        /// شماره کارت
        /// </summary>
        private long _cardNumber;

        public long CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber= value;
            }
        }


        /// <summary>
        /// شماره شناسه پوز
        /// </summary>
        private string _poseId;

        public string PoseId
        {
            get { return _poseId; }
            set
            {
                _poseId= value;
            }
        }

        /// <summary>
        /// آدرس وب سایت
        /// </summary
        private string _webSite;

        public string WebSite
        {
            get { return _webSite; }
            set
            {
                _webSite= value;
            }
        }


        /// <summary>
        /// تاریخ افتتاح
        /// </summary>
        //private DateTime _openingDate;

        //public DateTime OpeningDate
        //{
        //    get { return _openingDate; }
        //    set
        //    {
        //        _openingDate= value;
        //    }
        //}
        private DateTime? _OpeningDate;
        public DateTime? OpeningDate
        {
            get { return _OpeningDate; }
            set { SetProperty(ref _OpeningDate, value); }
        }

        /// <summary>
        /// مانده اول دوره
        /// </summary>
        private double _firstReservePeriod;

        public double FirstReservePeriod
        {
            get { return _firstReservePeriod; }
            set
            {
                _firstReservePeriod= value;
            }
        }

        /// <summary>
        /// رزرو موجودی
        /// </summary>
        private double _inventoryReservation;

        public double InventoryReservation
        {
            get { return _inventoryReservation; }
            set
            {
                _inventoryReservation= value;
            }
        }


        /// <summary>
        /// نوع ارز
        /// </summary>

        private Currency _currencyType;

        public virtual Currency CurrencyType
        {
            get { return _currencyType; }
            set
            {
                _currencyType= value;
            }
        }
        /// <summary>
        /// آی دی نوع ارز
        /// </summary>
        private int? _currencyId;

        public int? CurrencyId
        {
            get { return _currencyId; }
            set
            {
                _currencyId= value;
            }
        }


        /// <summary>
        /// نرخ ارز
        /// </summary>
        private bool _exchangeRate;

        public bool ExchangeRate
        {
            get { return _exchangeRate; }
            set
            {
                _exchangeRate= value;
            }
        }


        /// <summary>
        /// وضعیت فعال یا غیر فعال
        /// </summary>
        private bool _status;

        public bool Status
        {
            get { return _status; }
            set
            {
                _status= value;
            }
        }

        public virtual ICollection<RelatedPerson> RelatedPeople { get; set; }




        //public virtual DL DL { get; set; }
        //private int _DLId;
        //public int DLId
        //{
        //    get { return _DLId; }
        //    set { SetProperty(ref _DLId, value); }
        //}

    }
}
