using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
//using Telerik.Windows.Data;
using Saina.Data.Context;
using System.Data.Entity;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System.Collections.ObjectModel;
using Telerik.Windows.Data;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.DTOs;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    public class DocumntsFlowViewModel : BindableBase
    {
        public DocumntsFlowViewModel(IAppContextService appContextService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _appContextService = appContextService;

        }
        public CompanyInformationModel CompanyInformationModel { get; set; }

        private IAppContextService _appContextService;
        private ICompanyInformationsService _companyInformationsService;

        public AccDocumentItemDTO AccDocumentItemDTO { get;  set; }
        public string State { get;  set; }
      
        private ObservableCollection<AccDocumentItem1> _AccDocumentItems;
        //private ObservableCollection<AccDocumentItem1> AccDocumentItems1;

        public ObservableCollection<AccDocumentItem1> AccDocumentItems
        {
            get { return _AccDocumentItems; }
            set { SetProperty(ref _AccDocumentItems, value); }
        }

        public int DLCode2 { get; private set; }

        public event Action<int,int,bool> TestRequested;

        public void RaiseTestRequested(int accDocumentHeaderId, int date,bool hasflow)
        {
            TestRequested?.Invoke(accDocumentHeaderId,date, hasflow);
        }
        public void Loaded()
        {
            var _uow = SmObjectFactory.Container.GetInstance<SainaDbContext>();
            
            switch (State)
            {
                case "OnGLGrouped":
                case "OnGLDetailed":

                    double currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                    .Where(i => i.SL.TL.GL.GLId == AccDocumentItemDTO.Id 
                                  && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                    .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                    .ToList()
                                    .Select(i =>
                                    {
                                        currentTotal += i.Debit - i.Credit;
                                        return new AccDocumentItem1
                                        {
                                            DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                            AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate =i.AccDocumentHeader.DocumentDate,
                                            SLCode = i.SL.SLCode,
                                            SLTitle = i.SL?.Title,
                                            DLCode1 = i.DL1?.DLCode,
                                            DLTitle1 = i.DL1?.Title,
                                            DLTitle2 = i.DL2?.Title,
                                            DLCode2 = i.DL2?.DLCode,
                                            Description1 = i.Description1,
                                            Description2 = i.Description2,
                                            Debit = i.Debit,
                                            Credit = i.Credit,
                                            CurrencyTitle = i.Currency?.CurrencyTitle,
                                            ExchangeRate = i.ExchangeRate,
                                            CurrencyAmount = i.CurrencyAmount,
                                            TrackingNumber = i.TrackingNumber,
                                            TrackingDate = i.TrackingDate,
                                            Runningtotal = currentTotal

                                        };
                                    }
            ).ToList()
                                   );
                    
                    break;
                case "OnTLGrouped":
                case "OnTLDetailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i => i.SL.TLId == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                         DocumentDate = i.AccDocumentHeader.DocumentDate,

                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
              
                    break;
                case "OnSLGrouped":
                case "OnSLDetailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i => i.SLId == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate=i.AccDocumentHeader.DocumentDate,
                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
                    
                    break;
                case "OnDL1Grouped":
                case "OnDL1Detailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i => i.SL.SLCode == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate=i.AccDocumentHeader.DocumentDate,
                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
                 
                    break;
                case "OnDL2Grouped":
                case "OnDL2Detailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i => i.SL.SLCode == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate=i.AccDocumentHeader.DocumentDate,
                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
                    
                    break;
                case "OnCurrencyGrouped":
                case "OnCurrencyDetailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i => i.SL.SLCode == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x => x.AccDocumentHeader.DocumentNumber)//.OrderBy(x => x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate=i.AccDocumentHeader.DocumentDate,
                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
                   
                    break;
                case "OnTrackingGrouped":
                case "OnTrackingDetailed":
                    currentTotal = 0;
                    AccDocumentItems = new ObservableCollection<AccDocumentItem1>(_uow.AccDocumentItems.Include(x => x.SL)
                                 .Where(i =>i.SL.SLCode == AccDocumentItemDTO.Id && i.AccDocumentHeader.Status != ((int)(StatusEnum.Draft)))
                                 .OrderBy(x =>x.AccDocumentHeader.DocumentNumber)//.OrderBy(x=>x.AccDocumentHeader.DocumentDate)
                                 .ToList()
                                 .Select(i =>
                                 {
                                     currentTotal += i.Debit - i.Credit;
                                     return new AccDocumentItem1
                                     {
                                         DocumentNumber = i.AccDocumentHeader.DocumentNumber,
                                         AccDocumentHeaderId = i.AccDocumentHeaderId,
                                            DocumentDate=i.AccDocumentHeader.DocumentDate,
                                         SLCode = i.SL.SLCode,
                                         SLTitle = i.SL?.Title,
                                         DLCode1 = i.DL1?.DLCode,
                                         DLTitle1 = i.DL1?.Title,
                                         DLTitle2 = i.DL2?.Title,
                                         DLCode2 = i.DL2?.DLCode,
                                         Description1 = i.Description1,
                                         Description2 = i.Description2,
                                         Debit = i.Debit,
                                         Credit = i.Credit,
                                         CurrencyTitle = i.Currency?.CurrencyTitle,
                                         ExchangeRate = i.ExchangeRate,
                                         CurrencyAmount = i.CurrencyAmount,
                                         TrackingNumber = i.TrackingNumber,
                                         TrackingDate = i.TrackingDate,
                                         Runningtotal = currentTotal

                                     };
                                 }
         ).ToList()
                                );
                 
                    break;
                default:
                    break;

            }
        }

    }
}
