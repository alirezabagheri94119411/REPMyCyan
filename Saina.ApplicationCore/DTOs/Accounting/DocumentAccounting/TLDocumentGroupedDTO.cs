using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
  public  class TLDocumentGroupedDTO
    {
        /// <summary>
        /// کد کل
        /// </summary>
        //public int? SLCode { get; set; }
        public int? TLId { get; set; }
        public long TLCode { get; set; }
      
        public string Title { get; set; }
        /// <summary>
        /// جمع بدهکار
        /// </summary>
        public double SumDebit { get; set; }
        /// <summary>
        /// جمع بستانکار
        /// </summary>
        public double SumCredit { get; set; }
        
    }
}
