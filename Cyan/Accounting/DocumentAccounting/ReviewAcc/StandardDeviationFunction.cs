using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    public class StandardDeviationFunction : AggregateFunction<AccDocumentItemDTO, double>
    {
        public StandardDeviationFunction()
        {
            this.AggregationExpression = items => StdDev(items);
        }

        private double StdDev(IEnumerable<AccDocumentItemDTO> source)
        {
            var itemCount = source.Count();
            if (itemCount > 1)
            {
                var sumDebits = source.Sum(i => i.SumDebit);
                var sumCredits = source.Sum(i => i.SumCredit);
                var diffrence = Math.Abs(sumDebits - sumCredits);
                return diffrence;
            }

            return 0;
        }
    }
}
