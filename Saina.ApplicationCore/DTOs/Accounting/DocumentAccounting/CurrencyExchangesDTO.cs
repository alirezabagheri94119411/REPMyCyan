using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
    public class CurrencyExchangesDTO
    {
        public int? CurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
        public double ParityRate { get; set; }
    }

}
