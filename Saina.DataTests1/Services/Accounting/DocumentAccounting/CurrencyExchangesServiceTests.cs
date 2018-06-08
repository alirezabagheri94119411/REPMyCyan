using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saina.Data.Services.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting.Tests
{
    [TestClass()]
    public class CurrencyExchangesServiceTests
    {
        [TestMethod()]
        public  void GetGroupedAccDocumentItemsAsyncTest()
        {
            ////// CurrencyExchangesService currencyExchangesService = new CurrencyExchangesService(new Context.SainaDbContext(), null);
            //////// var lastDate= currencyExchangesService.GetLastDateDocAsync(1396).GetAwaiter().GetResult();
            //////var result=  currencyExchangesService.GetGroupedAccDocumentItemsAsync(new DateTime(2017,3,24),DateTime.Now).GetAwaiter().GetResult();
            ////// Assert.AreEqual(1, result.Count

            var ctx = new Context.SainaDbContext();
            var q= $@"SELECT tl.TLId, tl.TLCode,tl.Title TLTitle,SUM(di.Debit) SumDebit,SUM(di.Credit) SumCredit,DATEPART(month, h.DocumentDate) AS DocumentDateMonth FROM Acc.AccDocumentItems di
 INNER JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
 INNER JOIN Info.SLs sl ON sl.SLId = di.SLId
 INNER JOIN Info.TLs tl ON tl.TLId = sl.TLId
 WHERE h.DocumentDate BETWEEN '2000-1-1' AND '2019-11-01'
GROUP BY tl.TLId, tl.TLCode,tl.Title,DATEPART(month, h.DocumentDate)";
            var r1=ctx.Database.SqlQuery<TestClass>(q).ToList();
            var r2 = r1.GroupBy(x => x.DocumentDateMonth);
            var count = r2.Count();
            foreach (var item in r2)
            {
                var key=item.Key;
                var h = new ApplicationCore.Entities.Accounting.DocumentAccounting.AccDocumentHeader { };
                foreach (var i in item)
                {
                    h.AccDocumentItems.Add(new ApplicationCore.Entities.Accounting.DocumentAccounting.AccDocumentItem {Debit=i.SumDebit });
                }
                ctx.AccDocumentHeaders.Add(h);


                var value = item;
            }
        }
    }

    public class TestClass
    {
        public int TLId { get; set; }
        public int TLCode { get; set; }
        public string Title { get; set; }
        public double SumDebit { get; set; }
        public double SumdCredit { get; set; }
        public int DocumentDateMonth { get; set; }
    }
}