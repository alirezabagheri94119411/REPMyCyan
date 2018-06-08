using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
    public class AccDocumentItemDTO1
    {
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int? TypeDocumentId { get; set; }
        public long SLCode { get; set; }
        public string SLTitle { get; set; }
        public long TLCode { get; set; }
        public string TLTitle { get; set; }
        public long GLCode { get; set; }
        public string GLTitle { get; set; }
        public string CurrencyTitle  { get; set; }
        public long TrackingNumber { get; set; }
        public long? DL1Code { get; set; }
        public string DL1Title { get; set; }
        public long? DL2Code { get; set; }
        public String DL2Title { get; set; }
        public long CurrencyId { get; set; }
    }
}
