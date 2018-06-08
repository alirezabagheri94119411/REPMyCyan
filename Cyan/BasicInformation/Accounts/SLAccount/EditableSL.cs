using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.SLAccount
{
    public class EditableSL : ValidatableBindableBase
    {
        /// <summary>
        /// آی دی حساب معین
        /// </summary>
        private int _sLId;

        public int SLId
        {
            get { return _sLId; }
            set
            {
                SetProperty(ref _sLId, value);
            }
        }

        /// <summary>
        /// آی دی گروه کل
        /// </summary>
        /// 
        private int? _tLId;
        [Required(ErrorMessage = "حساب کل خود را انتخاب نمایید.")]
        public int? TLId
        {
            get { return _tLId; }
            set
            {
                SetProperty(ref _tLId, value);
            }
        }

        /// <summary>
        /// کد حساب
        /// </summary>
        private long _sLCode;
        //[Range(0,5,ErrorMessage ="")]
        //[Required(ErrorMessage ="")]
        //[Compare("Title",ErrorMessage ="")]
        [Required(ErrorMessage = "کد حساب خود را وارد نمایید.")]
        public long SLCode
        {
            get { return _sLCode; }
            set
            {
                SetProperty(ref _sLCode, value);
            }
        }

        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;
        //[RegularExpression(@"^[0-9]+$",
        // ErrorMessage = "فقط عدد وارد شود")]
        [Required(ErrorMessage = "عنوان خود را وارد نمایید.")]
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        /// <summary>
        /// عنوان 2
        /// </summary>
        private string _title2;
   //     [RegularExpression(@"^[0-5]{2}$",
   //ErrorMessage = "یک عدد دو رقمی کوچکتر از 55")]
     
        public string Title2
        {
            get { return _title2; }
            set
            {
                SetProperty(ref _title2, value);
            }
        }

        /// <summary>
        /// وضعیت
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
        /// <summary>
        /// ویژگی ها
        /// </summary>
        private PropertyEnum _property;

        [Required(ErrorMessage = "ویژگی های خود را وارد نمایید.")]

       

        public PropertyEnum Property
        {
            get { return _property; }
            set
            {
                SetProperty(ref _property, value);
             
            }
        }
        /// <summary>
        /// ماهیت حساب
        /// </summary>
        private AccountsNatureEnum _accountsNatureEnum;

        public  AccountsNatureEnum AccountsNatureEnum
        {
            get { return _accountsNatureEnum; }

            set { _accountsNatureEnum = value; }
        }
        //private int?  _accountsNatureId;

        //public int? AccountsNatureId
        //{
        //    get { return _accountsNatureId; }
        //    set
        //    {
        //        SetProperty(ref _accountsNatureId, value);
        //    }
        //}

        /// <summary>
        /// تفصیل 1
        /// </summary>
        private DLTypeEnum _dLType1;

        public DLTypeEnum DLType1
        {
            get { return _dLType1; }
            set { SetProperty(ref _dLType1,value); }
        }
        /// <summary>
        /// تفصیل 2
        /// </summary>

        private DLTypeEnum _dLType2;


        public DLTypeEnum DLType2
        {
            get { return _dLType2; }
            set { SetProperty(ref _dLType2, value); }
        }

        private string _regEx;

        public string Regex
        {
            get { return _regEx; }
            set { SetProperty(ref _regEx, value); }
        }

        private int _minLength;

        public int MinLength
        {
            get { return _minLength; }
            set { SetProperty(ref _minLength, value); }
        }
        private int _maxLength;

        public int MaxLength
        {
            get { return _maxLength; }
            set { SetProperty(ref _maxLength, value); }
        }
        private long _sLCodeEmptyValue;

        public long SLCodeEmptyValue
        {
            get { return _sLCodeEmptyValue; }
            set { SetProperty(ref _sLCodeEmptyValue,value); }
        }


    }
}

