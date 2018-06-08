using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
   public class EditableDL: ValidatableBindableBase
    {
        /// <summary>
        /// آی دی حساب تفصیل
        /// </summary>
        private int _dLId;

        public int DLId
        {
            get { return _dLId; }
            set
            {
                SetProperty(ref _dLId, value);
            }
        }

        /// <summary>
        /// نوع حساب تفصیل
        /// </summary>
        private DLTypeEnum _dLType;
        [Required(ErrorMessage = "حساب تفصیل خود را انتخاب نمایید.")]

        public DLTypeEnum DLType
        {
            get { return _dLType; }
            set
            {
                SetProperty(ref _dLType, value);
                if (DLType==DLTypeEnum.Others || DLType==DLTypeEnum.Project|| DLType==DLTypeEnum.Cost||DLType==DLTypeEnum.BankAccount)
                {
                    DLAccountsNature = 0;
                }
            }
        }
       

        /// <summary>
        /// کد حساب تفصیل
        /// </summary>

        private int _dLCode;
        [Required(ErrorMessage = "حساب تفصیل خود را وارد نمایید.")]

        public int DLCode
        {
            get { return _dLCode; }
            set
            {
                SetProperty(ref _dLCode, value);
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

        /// <summary>
        /// ماهیت
        /// مشتری,پرسنل,شرکت
        /// </summary>
        private DLAccountsNatureEnum _dLAccountsNature;

        public DLAccountsNatureEnum DLAccountsNature
        {
            get { return _dLAccountsNature; }
            set
            {
                SetProperty(ref _dLAccountsNature, value);
               
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
            set { SetProperty(ref _maxLength, value); }
        }
        private string _regEx;

        public string Regex
        {
            get { return _regEx; }
            set { SetProperty(ref _regEx, value); }
        }
        private int _dLCodeEmpyValue;

        public int DLCodeEmpyValue
        {
            get { return _dLCodeEmpyValue; }
            set { SetProperty(ref _dLCodeEmpyValue, value); }
        }
     

    }
}
