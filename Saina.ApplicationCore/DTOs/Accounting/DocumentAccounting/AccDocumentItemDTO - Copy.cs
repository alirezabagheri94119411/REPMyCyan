using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
  public  class AccDocumentItemDTO
    {
        public Int64 AccRow { get; set; }
        public int Id { get; set; }
        public long? Code { get; set; }
        public double OpeningSumDebit { get; set; }
        public double OpeningSumCredit { get; set; }

        public double SumDebit { get; set; }
        public double SumCredit { get; set; }

        public double RemainDebit
        {
            get
            {
                double remain = (OpeningSumDebit+SumDebit) - (OpeningSumCredit+SumCredit);
                return remain > 0 ? remain : 0;
            }
        }

        public double RemainCredit
        {
            get
            {
                double remain = (OpeningSumDebit+SumDebit) - (OpeningSumCredit + SumCredit);
                return remain < 0 ? -remain : 0;
            }
        }

    }
}
