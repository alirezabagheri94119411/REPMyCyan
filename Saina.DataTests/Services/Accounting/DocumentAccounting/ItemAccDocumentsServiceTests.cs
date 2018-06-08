using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Saina.Data.Services.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting.Tests
{
    [TestClass()]
    public class ItemAccDocumentsServiceTests
    {
        [TestMethod()]
        public void GetAllItemAccDocument1Test()

        { 
            var dls = new List<DL> {
                new DL{DLId=1,DLType= DLTypeEnum.Others,DLCode=10000000,Title="D1",Status=true },
                new DL{DLId=2,DLType= DLTypeEnum.People,DLCode=10000001,Title="D2",Status=true },
                new DL{DLId=3,DLType= DLTypeEnum.Company,DLCode=10000002,Title="D3",Status=true },
                new DL{DLId=4,DLType= DLTypeEnum.Others,DLCode=10000003,Title="D4",Status=true },
            };
            var gls = new List<GL>
            {
                new GL{GLId=1,Title="G1",AccountGLEnum= AccountGLEnum.ProfitLoss,GLCode=10},
                new GL{GLId=2,Title="G2",AccountGLEnum= AccountGLEnum.ProfitLoss,GLCode=11},
            };
            var tls = new List<TL>
            {
                new TL{TLId=1,Title="T1",GLId=1,GL=gls[0],TLCode=1001},
                new TL{TLId=2,Title="T2",GLId=2,GL=gls[1],TLCode=1101},
                new TL{TLId=3,Title="T3",GLId=1,GL=gls[0],TLCode=1102},
                new TL{TLId=4,Title="T4",GLId=2,GL=gls[1],TLCode=1103},
            };
            var sls = new List<SL>
            {
                new SL{SLId=1,Title="S1",TLId=1,TL=tls[0],SLCode=100101,DLType1= DLTypeEnum.Others | DLTypeEnum.Company,DLType2=DLTypeEnum.Company},
                new SL{SLId=2,Title="S2",TLId=1,TL=tls[0],SLCode=100102,DLType1=  DLTypeEnum.Company,DLType2=DLTypeEnum.Others},
                new SL{SLId=3,Title="S3",TLId=2,TL=tls[1],SLCode=110101,DLType1=  DLTypeEnum.People | DLTypeEnum.Others,DLType2=DLTypeEnum.Others},
                new SL{SLId=4,Title="S4",TLId=2,TL=tls[1],SLCode=110102,DLType1=  DLTypeEnum.Company,DLType2=DLTypeEnum.Others | DLTypeEnum.People},
                new SL{SLId=5,Title="S5",TLId=3,TL=tls[2],SLCode=100101,DLType1= DLTypeEnum.Others | DLTypeEnum.Company,DLType2=DLTypeEnum.Company},
                new SL{SLId=6,Title="S6",TLId=3,TL=tls[2],SLCode=100102,DLType1=  DLTypeEnum.Company,DLType2=DLTypeEnum.Others},
                new SL{SLId=7,Title="S7",TLId=4,TL=tls[3],SLCode=110101,DLType1=  DLTypeEnum.People | DLTypeEnum.Others,DLType2=DLTypeEnum.Others},
                new SL{SLId=8,Title="S8",TLId=4,TL=tls[3],SLCode=110102,DLType1=  DLTypeEnum.Company,DLType2=DLTypeEnum.Others | DLTypeEnum.People},

            };
            var data = new List<ItemAccDocument>
            {
                new ItemAccDocument{ItemAccDocumentId=1,AccRow=1,SLCodeId=1,SLCode=sls[0],DL1Id=2,DL2Id=null,Debit=1,Credit=0,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=2,AccRow=1,SLCodeId=1,SLCode=sls[0],DL1Id=null,DL2Id=2,Debit=0,Credit=1,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=3,AccRow=2,SLCodeId=2,SLCode=sls[1],DL1Id=null,DL2Id=1,Debit=2,Credit=0,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=4,AccRow=2,SLCodeId=2,SLCode=sls[1],DL1Id=2,DL2Id=null,Debit=0,Credit=2,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=5,AccRow=3,SLCodeId=3,SLCode=sls[2],DL1Id=3,DL2Id=null,Debit=4,Credit=0,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=6,AccRow=4,SLCodeId=4,SLCode=sls[3 ],DL1Id=null,DL2Id=3,Debit=0,Credit=4,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=7,AccRow=5,SLCodeId=5,SLCode=sls[4 ],DL1Id=null,DL2Id=2,Debit=0,Credit=6,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=8,AccRow=6,SLCodeId=5,SLCode=sls[4 ],DL1Id=1,DL2Id=null,Debit=0,Credit=1,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=9,AccRow=7,SLCodeId=6,SLCode=sls[5 ],DL1Id=2,DL2Id=null,Debit=0,Credit=8,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=10,AccRow=8,SLCodeId=7,SLCode=sls[6 ],DL1Id=1,DL2Id=null,Debit=0,Credit=7,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=11,AccRow=9,SLCodeId=8,SLCode=sls[7 ],DL1Id=1,DL2Id=null,Debit=0,Credit=1,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
                new ItemAccDocument{ItemAccDocumentId=12,AccRow=10,SLCodeId=8,SLCode=sls[7 ],DL1Id=null,DL2Id=3,Debit=0,Credit=2,AmountCurrency=5000,ExchangeRateId=1,CurrencyId=1,TrackingDate=DateTime.Now,TrackingNumber=0,AccDocumentHeaderId=2},
            };

            DbSet<ItemAccDocument> mockSet = GetQueryableMockDbSet(data);

            var mockContext = new Mock<SainaDbContext>();
            mockContext.Setup(c => c.ItemAccDocuments).Returns(mockSet);

            var service = new ItemAccDocumentsService(mockContext.Object,null);
            var allItemAccDocuments = service.GetAllItemAccDocument1();

            Assert.AreEqual(2, allItemAccDocuments.Count);
            //Assert.AreEqual(1, allItemAccDocuments[0].SLCodeId);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}