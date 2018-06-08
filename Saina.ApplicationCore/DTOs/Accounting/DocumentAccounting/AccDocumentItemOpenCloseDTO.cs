using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
   public class AccDocumentItemOpenCloseDTO
    {
        /// <summary>
        /// کد معین
        /// </summary>
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

       
        /// <summary>
        /// جمع بدهکار
        /// </summary>
        public double SumDebit { get; set; }
        /// <summary>
        /// جمع بستانکار
        /// </summary>
        public double SumCredit { get; set; }
      
        /// <summary>
        /// جمع مبلغ ارز
        /// </summary>
        public double SumCurrencyAmount { get; set; }
        public int CurrencyId { get; set; }
    }
}
