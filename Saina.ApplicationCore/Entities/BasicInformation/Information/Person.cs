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
    /// جدول اشخاص
    /// </summary>
   public class Person:BaseEntity
    {
        public Person()
        {
            RelatedPeople = new ObservableCollection<RelatedPerson>();
        }
        /// <summary>
        /// آی دی شخص
        /// </summary>
      
        private int _personId;

        public int PersonId
        {
            get { return _personId; }
            set { SetProperty(ref _personId, value); }
       
           
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
        /// کد تفصیل
        /// </summary>
        private long _dCode;

        public long Dcode
        {
            get { return _dCode; }
            set { SetProperty(ref _dCode, value); }
        }
        /// <summary>
        /// کد ملی
        /// </summary>
        private long _nationalCode;

        public long NationalCode
        {
            get { return _nationalCode; }
            set { SetProperty(ref _nationalCode, value); }
          
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
        //private bool _status;

        //public bool Status
        //{
        //    get { return _status; }
        //    set { SetProperty(ref _status, value); }
        //}

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
