using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument
{
   public class EditableExchangeRate: ValidatableBindableBase
    {
        /// <summary>
        /// آی دی نرخ ارز
        /// </summary>

        private int _exchangeRateId;

        public int ExchangeRateId
        {
            get { return _exchangeRateId; }
            set
            {
                SetProperty(ref _exchangeRateId, value);
            }
        }

        /// <summary>
        ///
        /// اجباری
        /// ارز
        /// </summary>
       
        private int? _currencyId;
        [Required(ErrorMessage = "لطفا ارز را انتخاب نمایید.")]

        public int? CurrencyId
        {
            get { return _currencyId; }
            set
            {
                SetProperty(ref _currencyId, value);
            }
        }

        /// <summary>
        /// تاریخ موثر
        /// اجباری
        /// </summary>
        private DateTime _effectiveDate= DateTime.Now;
        [Required(ErrorMessage = "لطفا تاریخ موثر را وارد نمایید.")]

        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
            set
            {
                SetProperty(ref _effectiveDate, value);
            }
        }


        
        /// <summary>
        ///آی دی واحد مبادله
        /// </summary>
        //private int? _exchangeUnitId;
        //[Required(ErrorMessage = "لطفا واحد مبادله را انتخاب نمایید.")]

        //public int? ExchangeUnitId
        //{
        //    get { return _exchangeUnitId; }
        //    set
        //    {
        //        SetProperty(ref _exchangeUnitId, value);
        //    }
        //}


        /// <summary>
        /// نرخ برابری یک واحد
        /// </summary>
        private double _parityRate;
        [Required(ErrorMessage = "لطفا نرخ برابری را انتخاب نمایید.")]

        public double ParityRate
        {
            get { return _parityRate; }
            set
            {
                SetProperty(ref _parityRate, value);
            }
        }


        /// <summary>
        /// توضیحات
        /// </summary>
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

    }
}
