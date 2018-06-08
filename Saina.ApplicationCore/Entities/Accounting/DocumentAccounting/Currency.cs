using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// جدول تعریف ارز
    /// </summary>
    [Table("Currencies", Schema = "Info")]
    public class Currency  : BaseEntity
    {
        public Currency()
        {
            BankAccounts = new ObservableCollection<BankAccount>();
            ExchangeRates = new ObservableCollection<ExchangeRate>();
          //  CurrencyTitles = new ObservableCollection<ExchangeRate>();
            CurrencyNames = new ObservableCollection<AccDocumentItem>();
        }
        /// <summary>
        /// آی دی ارز
        /// </summary>
        
        /// <summary>
        /// آی دی ارز
        /// </summary>
        private int _currencyId;

        public int CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
          
        }

        /// <summary>
        /// عنوان ارز به انگلیسی
        /// اجباری
        /// </summary>
        private string _currencyTitle;

        public string CurrencyTitle
        {
            get { return _currencyTitle; }
            set { _currencyTitle = value; }
           
        }

        /// <summary>
        /// عنوان ارز به فارسی
        /// اجباری
        /// </summary>
        private string _currencyTitle2;

        public string CurrencyTitle2
        {
            get { return _currencyTitle2; }
            set { _currencyTitle2 = value; }
          
        }

        /// <summary>
        /// واحد مبادله
        /// عدد 1
        /// </summary>
        private double _exchangeUnit;

        public double ExchangeUnit
        {
            get { return _exchangeUnit; }
            set { _exchangeUnit = value; }
          
        }

        /// <summary>
        /// واحد اعشار 
        /// 4 رقم
        /// </summary>
        private int _numberDecimal;

        public int NumberDecimal
        {
            get { return _numberDecimal; }
            set { _numberDecimal = value; }
           
        }

        /// <summary>
        /// عنوان اعشار فارسی
        /// اجباری
        /// </summary>
        private string _titleDecimal;

        public string TitleDecimal
        {
            get { return _titleDecimal; }
            set { _titleDecimal = value; }
           
        }

        /// <summary>
        ///
        /// اجباری عنوان اعشار انگلیسی
        /// </summary>
        private string _titleDecimal2;

        public string TitleDecimal2
        {
            get { return _titleDecimal2; }
            set { _titleDecimal2 = value; }
           
        }
       


        /// <summary>
        /// لیستی از نرخ ارز
        /// </summary>
      //  [InverseProperty("ExchangeUnit")]
      //  public virtual ICollection<ExchangeRate> ExchangeUnits { get; protected set; }
       // [InverseProperty("CurrencyTitle")]
        public virtual ICollection<ExchangeRate> ExchangeRates { get; protected set; }
        public virtual ICollection<BankAccount> BankAccounts { get; protected set; }
        public virtual ICollection<AccDocumentItem> CurrencyNames { get; protected set; }
    }
}
