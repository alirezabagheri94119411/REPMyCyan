using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.Commerce;
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

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    public enum DLTypeEnum
    {
        [Description("حساب بانکی")]
        BankAccount = 1,
        [Description("اشخاص")]
        People = 2,
        [Description("شرکت")]
        Company = 4,
        [Description("مرکز هزینه")]
        Cost = 8,
        [Description("پروژه")]
        Project = 16,
        [Description("سایر")]
        Others = 32,
    }
   public enum DLAccountsNatureEnum
    {
        Seller = 1,
        Buyer = 2,
        Personel=4
    }
    /// <summary>
    /// 
    /// جدول حساب تفصیل
    /// </summary>
    [Table("DLs", Schema = "Info")]

    public class DL:BaseEntity
    {
        public DL()
        {
            //Companies = new ObservableCollection<Company>();
            //BankAccounts = new ObservableCollection<BankAccount>();
            //People = new ObservableCollection<Person>();
            AccDocumentItems1 = new ObservableCollection<AccDocumentItem>();
            AccDocumentItems2 = new ObservableCollection<AccDocumentItem>();
            StcDocumentHeaders1 = new ObservableCollection<StcDocumentHeader>();
            StcDocumentHeaders2 = new ObservableCollection<StcDocumentHeader>();
            RelatedPeople = new ObservableCollection<RelatedPerson>();
            Status = true;
            DLType = DLTypeEnum.Others;
           // DLType = Others;
        }
        /// <summary>
        /// آی دی حساب تفصیل
        /// </summary>
        private int _dLId;

        public int DLId
        {
            get { return _dLId; }
            set
            {
                _dLId= value;
            }
        }

        /// <summary>
        /// نوع حساب تفصیل
        /// </summary>
        private DLTypeEnum _DLType;
        public DLTypeEnum DLType
        {
            get { return _DLType; }
            set { SetProperty(ref _DLType, value); }
        }

        /// <summary>
        /// کد حساب تفصیل
        /// </summary>

        private int _dLCode;

        public int DLCode
        {
            get { return _dLCode; }
            set { SetProperty(ref _dLCode, value); }

        }

        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }

        }

        /// <summary>
        /// عنوان 2
        /// </summary>
        private string _title2;

        public string Title2
        {
            get { return _title2; }
            set { SetProperty(ref _title2, value); }

        }

        /// <summary>
        /// وضعیت
        /// </summary>
        private bool _status;

        public bool Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }

        }

        /// <summary>
        /// ماهیت
        /// مشتری,پرسنل,شرکت
        /// </summary>
        private DLAccountsNatureEnum _dLAccountsNature;

        public DLAccountsNatureEnum DLAccountsNature
        {
            get { return _dLAccountsNature; }
            set { SetProperty(ref _dLAccountsNature, value); }

        }
        //اشخاص
       
        
        /// <summary>
        /// کد ملی
        /// </summary>
        private long _nationalCode;

        public long NationalCode
        {
            get { return _nationalCode; }
            set { SetProperty(ref _nationalCode, value); }

        }
        private string _name;
        /// <summary>
        /// نام شخص
        /// </summary>

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _family;
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Family
        {
            get { return _family; }
            set { SetProperty(ref _family, value); }
        }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        private long _certificateNumber;

        public long CertificateNumber
        {
            get { return _certificateNumber; }
            set { SetProperty(ref _certificateNumber, value); }

        }


        /// <summary>
        /// تاریخ تولد
        /// </summary>
        private DateTime _birthDate = DateTime.Now;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }

        }
        [NotMapped]
        public string BirthDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_birthDate)}/{pc.GetMonth(_birthDate)}/{pc.GetDayOfMonth(_birthDate)}");
                return result;
            }
        }

        /// <summary>
        /// نام پدر
        /// </summary>
        private string _fatherName;

        public string FatherName
        {
            get { return _fatherName; }
            set { SetProperty(ref _fatherName, value); }

        }

        /// <summary>
        /// تعداد فرزندان
        /// </summary>
        private int _numberOfChildren;

        public int NumberOfChildren
        {
            get { return _numberOfChildren; }
            set { SetProperty(ref _numberOfChildren, value); }

        }


        /// <summary>
        /// کد پستی
        /// </summary>
        private long _postalCode;

        public long PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }

        }

        /// <summary>
        /// جنسیت
        /// </summary>
        private string _sexuality;

        public string Sexuality
        {
            get { return _sexuality; }
            set { SetProperty(ref _sexuality, value); }

        }

        /// <summary>
        /// محل تولد
        /// </summary>
        private string _birthPlace;

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { SetProperty(ref _birthPlace, value); }

        }

        /// <summary>
        /// محل صدور شناسنامه
        /// </summary>
        private string _certificatePlace;

        public string CertificatePlace
        {
            get { return _certificatePlace; }
            set { SetProperty(ref _certificatePlace, value); }

        }

        /// <summary>
        /// سریال شناسنامه
        /// </summary>
        private string _certificateSeries;

        public string CertificateSeries
        {
            get { return _certificateSeries; }
            set { SetProperty(ref _certificateSeries, value); }

        }

        /// <summary>
        /// آدرس
        /// </summary>
        private string address;

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }

        }

        /// <summary>
        /// آدرس2
        /// </summary>
        private string address2;

        public string Address2
        {
            get { return address2; }
            set { SetProperty(ref address2, value); }

        }
        /// <summary>
        /// آدرس وب سایت
        /// </summary>
        private string _webSite;

        public string WebSite
        {
            get { return _webSite; }
            set { SetProperty(ref _webSite, value); }

        }

        /// <summary>
        /// مانده حساب اول
        /// </summary>
        private double _firstAccountBalance;

        public double FirstAccountBalance
        {
            get { return _firstAccountBalance; }
            set { SetProperty(ref _firstAccountBalance, value); }

        }

        /// <summary>
        /// فکس
        /// </summary>
        private long _fax;

        public long Fax
        {
            get { return _fax; }
            set { SetProperty(ref _fax, value); }

        }

        /// <summary>
        /// لوگو
        /// </summary>
        private string _logo;

        public string Logo
        {
            get { return _logo; }
            set { SetProperty(ref _logo, value); }

        }

        //شرکت
     
      
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        private DateTime _dateRegistration = DateTime.Now;

        public DateTime DateRegistration
        {
            get { return _dateRegistration; }
            set { SetProperty(ref _dateRegistration, value); }

        }
        [NotMapped]
        public string DateRegistrationString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_dateRegistration)}/{pc.GetMonth(_dateRegistration)}/{pc.GetDayOfMonth(_dateRegistration)}");
                return result;
            }
        }
        /// <summary>
        /// شماره ثبت
        /// </summary>
        private string _registrationNumber;

        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { SetProperty(ref _registrationNumber, value); }

        }


        /// <summary>
        /// شماره اقتصادی
        /// </summary>
        private string _economicalNumber;

        public string EconomicalNumber
        {
            get { return _economicalNumber; }
            set { SetProperty(ref _economicalNumber, value); }

        }


        /// <summary>
        /// نام استان
        /// </summary>
        // public virtual City City { get; set; }
        private string _city;

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }

        }

        /// <summary>
        /// نام شهر
        /// </summary>
        //   public virtual Province Province { get; set; }
        private string _province;

        public string Province
        {
            get { return _province; }
            set { SetProperty(ref _province, value); }

        }


        /// <summary>
        /// نام شهرستان
        /// </summary>
        private string _town;

        public string Town
        {
            get { return _town; }
            set { SetProperty(ref _town, value); }

        }


        /// <summary>
        /// تلفن مدیر عامل 
        /// </summary>
        private string _phoneManager;

        public string PhoneManager
        {
            get { return _phoneManager; }
            set { SetProperty(ref _phoneManager, value); }

        }

        /// <summary>
        /// نام مدیرعامل یا واسط
        /// </summary>
        private string _managerName;

        public string ManagerName
        {
            get { return _managerName; }
            set { SetProperty(ref _managerName, value); }

        }


        /// <summary>
        /// تلفن1
        /// </summary>
        private string _phone1;

        public string Phone1
        {
            get { return _phone1; }
            set { SetProperty(ref _phone1, value); }

        }


        /// <summary>
        /// تلفن2
        /// </summary>
        private string _phone2;

        public string Phone2
        {
            get { return _phone2; }
            set { SetProperty(ref _phone2, value); }

        }
        /// <summary>
        /// تلفن3
        /// </summary>
        private string _phone3;

        public string Phone3
        {
            get { return _phone3; }
            set { SetProperty(ref _phone3, value); }

        }


        /// <summary>
        /// نام لاتین
        /// </summary>
        private string _latinName;

        public string LatinName
        {
            get { return _latinName; }
            set { SetProperty(ref _latinName, value); }

        }

      
        //حساب بانکی
        /// <summary>
        /// نام بانک
        /// </summary>

        private Bank _bank;

        public virtual Bank Bank
        {
            get { return _bank; }
            set
            {
                _bank = value;
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
                _accountNumber = value;
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
                _branch = value;
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
                _accountType = value;
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
                _shabaNumber = value;
            }
        }




        /// <summary>
        /// آدرس
        /// </summary>
        //private string _addrress;

        //public string Addrress
        //{
        //    get { return _addrress; }
        //    set
        //    {
        //        _addrress = value;
        //    }
        //}


       
        /// <summary>
        /// شماره کارت
        /// </summary>
        private long _cardNumber;

        public long CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
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
                _poseId = value;
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
        private DateTime? _OpeningDate=DateTime.Now;
        public DateTime? OpeningDate
        {
            get { return _OpeningDate; }
            set { SetProperty(ref _OpeningDate, value); }
        }
        [NotMapped]
        public string OpeningDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                if (_OpeningDate==null)
                {
                    _OpeningDate = DateTime.Now;
                }
                string result = string.Format($"{pc.GetYear(_OpeningDate.Value)}/{pc.GetMonth(_OpeningDate.Value)}/{pc.GetDayOfMonth(_OpeningDate.Value)}");
                return result;
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
                _firstReservePeriod = value;
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
                _inventoryReservation = value;
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
                _currencyType = value;
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
                _currencyId = value;
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
                _exchangeRate = value;
            }
        }


        public virtual ICollection<RelatedPerson> RelatedPeople { get; set; }

        [InverseProperty("DL1")]
        public virtual ICollection<AccDocumentItem> AccDocumentItems1 { get; set; }

        [InverseProperty("DL2")]
        public virtual ICollection<AccDocumentItem> AccDocumentItems2 { get; set; }
        [InverseProperty("DL1")]
        public virtual ICollection<StcDocumentHeader> StcDocumentHeaders1 { get; set; }

        [InverseProperty("DL2")]
        public virtual ICollection<StcDocumentHeader> StcDocumentHeaders2 { get; set; }

    }
}
