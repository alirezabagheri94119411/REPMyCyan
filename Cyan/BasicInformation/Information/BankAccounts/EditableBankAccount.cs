using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
   public class EditableBankAccount:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی حساب بانک
        /// </summary>
     
        private int _bankAccountId;

        public int BankAccountId
        {
            get { return _bankAccountId; }
            set
            {
                SetProperty(ref _bankAccountId, value);
            }
        }
        /// <summary>
        /// کد تفصیل
        /// </summary>
        private int? _dCode;

        public int? Dcode
        {
            get { return _dCode; }
            set { SetProperty(ref _dCode, value); }
        }

        /// <summary>
        /// آی دی بانک
        /// </summary>
        private int? _bankId;
        [Required(ErrorMessage = "لطفا نام بانک را انتخاب نمایید.")]

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
        [Required(ErrorMessage = "لطفا شماره حساب را وارد نمایید.")]

        public long AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                SetProperty(ref _accountNumber, value);
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
                SetProperty(ref _branch, value);
            }
        }


        /// <summary>
        /// نوع حساب
        /// </summary>
        private int? _accountTypeId;
        [Required(ErrorMessage = "لطفا نوع حساب را وارد نمایید.")]

        public int? AccountTypeId
        {
            get { return _accountTypeId; }
            set
            {
                SetProperty(ref _accountTypeId, value);
            }
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
                SetProperty(ref _shabaNumber, value);
            }
        }


       

      
        /// <summary>
        /// آدرس
        /// </summary>
        private string _addrress;
        [Required(ErrorMessage = "لطفا آدرس راوارد نمایید.")]

        public string Addrress
        {
            get { return _addrress; }
            set
            {
                SetProperty(ref _addrress, value);
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
                SetProperty(ref _postalCode, value);
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
                SetProperty(ref _cardNumber, value);
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
                SetProperty(ref _poseId, value);
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
                SetProperty(ref _webSite, value);
            }
        }
      

        /// <summary>
        /// تاریخ افتتاح
        /// </summary>
        private DateTime? _openingDate;

        public DateTime? OpeningDate
        {
            get { return _openingDate; }
            set
            {
                SetProperty(ref _openingDate, value);
            }
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
                SetProperty(ref _firstReservePeriod, value);
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
                SetProperty(ref _inventoryReservation, value);
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
                SetProperty(ref _currencyId, value);
            }
        }


        /// <summary>
        /// نرخ ارز
        /// </summary>
        private bool _exchangeRate;
        [Required(ErrorMessage = "لطفا نوع ارز را انتخاب نمایید.")]

        public bool ExchangeRate
        {
            get { return _exchangeRate; }
            set
            {
                SetProperty(ref _exchangeRate, value);
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
                SetProperty(ref _status, value);
            }
        }
        private Bank _Bank;
        public Bank Bank
        {
            get { return _Bank; }
            set { SetProperty(ref _Bank, value); }
        }
    }
}
