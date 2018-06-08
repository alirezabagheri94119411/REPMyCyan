using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
  
    public class DiffrenceFunction : AggregateFunction<AccDocumentItem, double>
    {
        public DiffrenceFunction()
        {
            this.AggregationExpression = items => StdDev(items);
        }

        private double StdDev(IEnumerable<AccDocumentItem> source)
        {
            var itemCount = source.Count();
            if (itemCount > 1)
            {
                var sumDebits = source.Sum(i => i.Debit);
                var sumCredits = source.Sum(i => i.Credit);
                var diffrence = Math.Abs(sumDebits - sumCredits);
                return diffrence;
            }

            return 0;
        }
    }
}
