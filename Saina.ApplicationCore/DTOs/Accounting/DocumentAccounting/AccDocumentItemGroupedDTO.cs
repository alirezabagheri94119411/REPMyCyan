using System.Collections.Generic;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Globalization;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting

{
    public class AccDocumentItemGroupedDTO
    {
        /// <summary>
        /// کد معین
        /// </summary>
        public int AccDocumentItemId { get; set; }
        //public int? SLCode { get; set; }
        public int SLId { get; set; }
        public long SLCode { get; set; }
        public string SLTitle { get; set; }
        /// <summary>
        /// کد تفصیل1
        /// </summary>
        public int? DL1Id { get; set; }
        public int? DL1Code { get; set; }
        /// <summary>
        /// کد تفصیل2
        /// </summary>
        public int? DL2Code { get; set; }
        public int? DL2Id { get; set; }
        public string DLTitle1 { get; set; }
        public string DLTitle2 { get; set; }

        public int? CurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
        public double? ParityRate { get; set; }
        /// <summary>
        /// جمع بدهکار
        /// </summary>
        public double SumDebit { get; set; }
        /// <summary>
        /// جمع بستانکار
        /// </summary>
        public double SumCredit { get; set; }
        public int TypeDocumentId { get; set; }
        // public DateTime DocumentDate { get; set; }
        private DateTime _DocumentDate;
       // public int CurrencyId { get; set; }
        public DateTime DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }

       
        public string DocumentDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_DocumentDate)}/{pc.GetMonth(_DocumentDate)}/{pc.GetDayOfMonth(_DocumentDate)}");
                return result;
            }
        }
        /// <summary>
        /// شماره پیگیری
        /// </summary>
        public long TrackingNumber { get; set; }
        /// <summary>
        /// آخرین ارز
        /// </summary>
      //  public double LastCurrency { get; set; }
       /// <summary>
       /// جمع مبلغ ارز
       /// </summary>
        public double SumCurrencyAmount { get; set; }
       public bool HasExchange { get; set; }
                                           // public double SumDebitCurrencies { get; set; }

        //public double MyProperty
        //{
        //    get { var t= LastCurrency*SumCurrencyAmount;
        //        var r = SumDebit - SumCredit;
        //        var rr = r - t;
        //        return rr;
        //    }
        //}
        //public double ExSumCredit
        //{
        //    get
        //    {
        //        SumDebitCurrencies = di.Where(x => x.credit == 0).Sum(x => x.currency);
        //        var sumCreditCurrencies = di.Where(x => x.debit == 0).Sum(x => x.currency);
        //        var remain = sumDebitCurrencies - sumCreditCurrencies;
        //        return MyProperty < 0 ? MyProperty : 0;
        //    }
        //}
        //public double ExSumDebit
        //{
        //    get
        //    {
        //        return MyProperty > 0 ? MyProperty : 0;
        //    }
        //}

    }
}