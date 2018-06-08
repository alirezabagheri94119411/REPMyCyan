using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
   public class EditableTL:ValidatableBindableBase
    {
        private int _tLId;

        public int TLId
        {
            get { return _tLId; }
            set
            {
                SetProperty(ref _tLId, value);
            }
        }

       
        /// <summary>
        /// آی دی حساب گروه
        /// </summary>
        private int? _gLId;
        [Required(ErrorMessage = "حساب گروه خود را انتخاب نمایید.")]

        public int? GLId
        {
            get { return _gLId; }
            set
            {
                SetProperty(ref _gLId, value);
            }
        }

        /// <summary>
        /// کد حساب
        /// </summary>
        private long _tLCode;
        [Required(ErrorMessage = "کد حساب خود را وارد نمایید.")]

        public long TLCode
        {
            get { return _tLCode; }
            set
            {
                SetProperty(ref _tLCode, value);
            }
        }

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
        private long _tLCodeEmptyValue;
        public long TLCodeEmptyValue
        {
            get { return _tLCodeEmptyValue; }
            set { SetProperty(ref _tLCodeEmptyValue, value); }
        }


    }
}
