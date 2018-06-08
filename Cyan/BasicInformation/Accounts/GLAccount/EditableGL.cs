using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.GLAccount
{
  
   public class EditableGL:ValidatableBindableBase
    {
        private AccountGLEnum _accountGLEnum;

        public AccountGLEnum AccountGLEnum
        {
            get { return _accountGLEnum; }
            set { SetProperty(ref _accountGLEnum, value); }
        }

        /// <summary>
        /// آی دی حساب گروه
        /// </summary>

        private int _gLId;

        public int GLId
        {
            get { return _gLId; }
            set
            {
                SetProperty(ref _gLId, value);
            }
        }


        /// <summary>
        /// آی دی گروه حساب
        /// </summary>

        //private int? _accountGroupId;
        ////[Required(ErrorMessage = "گروه حساب خود را انتخاب نمایید.")]

        //public int? AccountGroupId
        //{
        //    get { return _accountGroupId; }
        //    set { SetProperty(ref _accountGroupId, value); }

        //}

        /// <summary>
        /// کد حساب
        /// </summary>
        private long _gLCode;
        //[Required(ErrorMessage = "کد حساب خود را وارد نمایید.")]

        public long GLCode
        {
            get { return _gLCode; }
            set
            {
                SetProperty(ref _gLCode, value);
            }
        }
        //private string _accountGroupTitle;

        //public string AccountGroupTitle
        //{
        //    get { return _accountGroupTitle; }
        //    set
        //    {
        //        _accountGroupTitle = value;


        //    }
        //}
        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;
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
            set { _maxLength = value; }
        }
        private string _regEx;

        public string Regex
        {
            get { return _regEx; }
            set { SetProperty(ref _regEx, value); }
        }
        private long _gLCodeEmptyValue;

        public long GLCodeEmptyValue
        {
            get { return _gLCodeEmptyValue; }
            set { SetProperty(ref _gLCodeEmptyValue, value); }
        }
    }
}
