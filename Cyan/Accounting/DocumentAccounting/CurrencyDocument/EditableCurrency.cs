using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.CurrencyDocument
{
   public class EditableCurrency: ValidatableBindableBase
    {
        /// <summary>
        /// آی دی ارز
        /// </summary>
        private int _currencyId;

        public int CurrencyId
        {
            get { return _currencyId; }
            set
            {
                SetProperty(ref _currencyId, value);
            }
        }

        /// <summary>
        /// عنوان ارز به انگلیسی
        /// اجباری
        /// </summary>
        private string _currencyTitle;
        [Required(ErrorMessage = "لطفا عنوان ارز انگلیسی را وارد نمایید.")]

        public string CurrencyTitle
        {
            get { return _currencyTitle; }
            set
            {
                SetProperty(ref _currencyTitle, value);
            }
        }

        /// <summary>
        /// عنوان ارز به فارسی
        /// اجباری
        /// </summary>
        private string _currencyTitle2;
        [Required(ErrorMessage = "لطفا عنوان ارز فارسی را وارد نمایید.")]

        public string CurrencyTitle2
        {
            get { return _currencyTitle2; }
            set
            {
                SetProperty(ref _currencyTitle2, value);
            }
        }

        /// <summary>
        /// واحد مبادله
        /// عدد 1
        /// </summary>
        private double _exchangeUnit;
        [Required(ErrorMessage = "لطفا واحد مبادله را وارد نمایید.")]
        public double ExchangeUnit
        {
            get { return _exchangeUnit; }
            set
            {
                SetProperty(ref _exchangeUnit, value);
            }
        }

        /// <summary>
        /// واحد اعشار 
        /// 4 رقم
        /// </summary>
        private int _numberDecimal;
        [Required(ErrorMessage = "لطفا واحد اعشار را وارد نمایید.")]

        public int NumberDecimal
        {
            get { return _numberDecimal; }
            set
            {
                SetProperty(ref _numberDecimal, value);
            }
        }

        /// <summary>
        /// عنوان اعشار فارسی
        /// اجباری
        /// </summary>
        private string _titleDecimal;
        [Required(ErrorMessage = "لطفا عنوان اعشار فارسی را وارد نمایید.")]

        public string TitleDecimal
        {
            get { return _titleDecimal; }
            set
            {
                SetProperty(ref _titleDecimal, value);
            }
        }

        /// <summary>
        ///
        /// اجباری عنوان اعشار انگلیسی
        /// </summary>
        private string _titleDecimal2;
        [Required(ErrorMessage = "لطفا عنوان اعشار انگلیسی را وارد نمایید.")]

        public string TitleDecimal2
        {
            get { return _titleDecimal2; }
            set
            {
                SetProperty(ref _titleDecimal2, value);
            }
        }

    }
}
