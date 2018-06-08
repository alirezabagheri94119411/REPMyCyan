 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// تعریف نرخ ارز
    /// </summary>
    [Table("ExchangeRates", Schema = "Info")]
    public class ExchangeRate : BaseEntity
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
               _exchangeRateId= value;
            }
        }

        /// <summary>
        /// اجباری
        /// ارز
        /// </summary>
        private Currency _currency;
       /// [ForeignKey("CurrencyId")]
        public virtual Currency Currency
        {
            get { return _currency; }
            set
            {
                _currency= value;
            }
        }
    
        private int? _currencyId;

        public int? CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        
        /// <summary>
        /// تاریخ موثر
        /// اجباری
        /// </summary>
        private DateTime _effectiveDate = DateTime.Now;

        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
            set { _effectiveDate = value; }
        }
        [NotMapped]
        public string EffectiveDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_effectiveDate)}/{pc.GetMonth(_effectiveDate)}/{pc.GetDayOfMonth(_effectiveDate)}");
                return result;
            }
        }
        /// <summary>
        /// نرخ برابری یک واحد
        /// </summary>
        private double _parityRate;

        public double ParityRate
        {
            get { return _parityRate; }
            set { _parityRate = value; }
        }


        /// <summary>
        /// توضیحات
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
