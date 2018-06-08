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
    /// جدول اطلاعات کامل شرکت
    /// </summary>
   public class Company:BaseEntity
    {
        public Company()
        {
            RelatedPeople = new ObservableCollection<RelatedPerson>();
        }
        /// <summary>
        /// آی دی اطلاعات کامل شرکت
        /// </summary>
      
        private int _companyId;

        public int CompanyId
        {
            get { return _companyId; }
            set { SetProperty(ref _companyId, value); }
          
        }
        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }

        /// <summary>
        /// کد تفصیل
        /// </summary>
        private long _dCode;

        public long Dcode
        {
            get { return _dCode; }
            set { SetProperty(ref _dCode, value); }
        }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        private DateTime _dateRegistration = DateTime.Now;

        public DateTime DateRegistration
        {
            get { return _dateRegistration; }
            set { SetProperty(ref _dateRegistration, value); }
            
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


        //   public virtual Town Town { get; set; }
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
        /// آدرس
        /// </summary>
        private string _address;

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
           
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

        /// <summary>
        /// مانده حساب اول
        /// </summary>
        private long _firstAccountBalance;

        public long FirstAccountBalance
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
        private string _webSite;

        public string WebSite
        {
            get { return _webSite; }
            set { SetProperty(ref _webSite, value); }
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
