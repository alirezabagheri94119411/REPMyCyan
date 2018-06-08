using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.PersonInfo
{
   public class EditablePerson:ValidatableBindableBase
    {
        private int _personId;

        public int PersonId
        {
            get { return _personId; }
            set
            {
                SetProperty(ref _personId, value);
            }
        }
        /// <summary>
        /// کد تفصیل
        /// </summary>
        private long? _dCode;

        public long? Dcode
        {
            get { return _dCode; }
            set { SetProperty(ref _dCode, value); }
        }

        /// <summary>
        /// کد ملی
        /// </summary>
        private long _nationalCode;
        [Required(ErrorMessage = "لطفا کد ملی را وارد نمایید.")]

        public long NationalCode
        {
            get { return _nationalCode; }
            set
            {
                SetProperty(ref _nationalCode, value);
            }
        }


        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        private long _certificateNumber;
        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد نمایید.")]

        public long CertificateNumber
        {
            get { return _certificateNumber; }
            set
            {
                SetProperty(ref _certificateNumber, value);
            }
        }


        /// <summary>
        /// تاریخ تولد
        /// </summary>
        private DateTime _birthDate;
        [Required(ErrorMessage = "لطفا تاریخ تولد را وارد نمایید.")]

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                SetProperty(ref _birthDate, value);
            }
        }


        /// <summary>
        /// نام پدر
        /// </summary>
        private string _fatherName;
        [Required(ErrorMessage = "لطفا نام پدر را وارد نمایید.")]

        public string FatherName
        {
            get { return _fatherName; }
            set
            {
                SetProperty(ref _fatherName, value);
            }
        }

        /// <summary>
        /// تعداد فرزندان
        /// </summary>
        private int _numberOfChildren;

        public int NumberOfChildren
        {
            get { return _numberOfChildren; }
            set
            {
                SetProperty(ref _numberOfChildren, value);
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
        /// جنسیت
        /// </summary>
        private string _sexuality;
        [Required(ErrorMessage = "لطفاکد ملی را انتخاب نمایید.")]

        public string Sexuality
        {
            get { return _sexuality; }
            set
            {
                SetProperty(ref _sexuality, value);
            }
        }

        /// <summary>
        /// محل تولد
        /// </summary>
        private string _birthPlace;
        [Required(ErrorMessage = "لطفا محل تولد را وارد نمایید.")]

        public string BirthPlace
        {
            get { return _birthPlace; }
            set
            {
                SetProperty(ref _birthPlace, value);
            }
        }

        /// <summary>
        /// محل صدور شناسنامه
        /// </summary>
        private string _certificatePlace;
        [Required(ErrorMessage = "لطفا محل صدور شناسنامه را وارد نمایید.")]

        public string CertificatePlace
        {
            get { return _certificatePlace; }
            set
            {
                SetProperty(ref _certificatePlace, value);
            }
        }

        /// <summary>
        /// سریال شناسنامه
        /// </summary>
        private string _certificateSeries;
        [Required(ErrorMessage = "لطفا سریال شناسنامه وارد نمایید.")]

        public string CertificateSeries
        {
            get { return _certificateSeries; }
            set
            {
                SetProperty(ref _certificateSeries, value);
            }
        }

        /// <summary>
        /// آدرس
        /// </summary>
        private string address;
        [Required(ErrorMessage = "لطفا آدرس را وارد نمایید.")]

        public string Address
        {
            get { return address; }
            set
            {
                SetProperty(ref address, value);
            }
        }

        /// <summary>
        /// آدرس2
        /// </summary>
        private string address2;

        public string Address2
        {
            get { return address2; }
            set
            {
                SetProperty(ref address2, value);
            }
        }
        /// <summary>
        /// آدرس وب سایت
        /// </summary>
        private string _webSite;
        [EmailAddress(ErrorMessage = "لطفا آدرس ایمیل را وارد نمایید")]

        public string WebSite
        {
            get { return _webSite; }
            set
            {
                SetProperty(ref _webSite, value);
            }
        }

        /// <summary>
        /// مانده حساب اول
        /// </summary>
        private double _firstAccountBalance;

        public double FirstAccountBalance
        {
            get { return _firstAccountBalance; }
            set
            {
                SetProperty(ref _firstAccountBalance, value);
            }
        }

        /// <summary>
        /// فکس
        /// </summary>
        private long _fax;

        public long Fax
        {
            get { return _fax; }
            set
            {
                SetProperty(ref _fax, value);
            }
        }

        /// <summary>
        /// لوگو
        /// </summary>
        private string _logo;

        public string Logo
        {
            get { return _logo; }
            set
            {
                SetProperty(ref _logo, value);
            }
        }
    }
}
