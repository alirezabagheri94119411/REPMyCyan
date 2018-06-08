using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo
{
    public class TLDocumentHeaderListViewModel : BindableBase
    {
        #region Constructors
        public TLDocumentHeaderListViewModel(ITLDocumentsService tLDocumentsService,
            IAccDocumentItemsService accDocumentItemsService, IOpeningClosingsService openingClosingsService,
            IAppContextService appContextService, ICurrencyExchangesService currencyExchangesService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _tLDocumentsService = tLDocumentsService;
            _openingClosingsService = openingClosingsService;
            _appContextService = appContextService;
            _accDocumentItemsService = accDocumentItemsService;
            _currencyExchangesService = currencyExchangesService;
            CancelCommand = new RelayCommand(OnCancel);
            //   SaveCommand = new RelayCommand(OnSave, CanSave);
            ViewCommand = new RelayCommand<TLDocumentHeader>(OnView);
            ExportDocumentCommand = new RelayCommand(OnExportDocument, CanExportDocument);
            DeleteCommand = new RelayCommand<TLDocumentHeader>(OnDeleteTLDocumentHeader);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }



        private bool CanExportDocument()
        {
            return !Close;
        }
        #endregion
        #region Fields
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;


        private ITLDocumentsService _tLDocumentsService;
        private IOpeningClosingsService _openingClosingsService;
        private IAppContextService _appContextService;
        private IAccDocumentItemsService _accDocumentItemsService;
        private ICurrencyExchangesService _currencyExchangesService;


        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        //   public RelayCommand SaveCommand { get; private set; }
        public ICommand ViewCommand { get; private set; }
        public ICommand ExportDocumentCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;

        //  public ICommand TypeDocumentsDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        private int _numberDoc;

        public bool Close { get; private set; }

        public int NumberDoc
        {
            get { return _numberDoc; }
            set { SetProperty(ref _numberDoc, value); }
        }
        private int _fromNumberDocFrom;
        public int FromNumber
        {
            get { return _fromNumberDocFrom; }
            set { SetProperty(ref _fromNumberDocFrom, value); }
        }

        private DateTime? _dateDoc;
        public DateTime? DateDoc
        {
            get { return _dateDoc; }
            set
            {
                SetProperty(ref _dateDoc, value);
                if (_dateDoc != null)
                {

                    PersianCalendar pc = new PersianCalendar();
                    DateDocString = string.Format($"{pc.GetYear(_dateDoc.Value)}/{pc.GetMonth(_dateDoc.Value)}/{pc.GetDayOfMonth(_dateDoc.Value)}");
                }
                else
                {
                    DateDocString = "";
                }
            }
        }

        //public string DateDocString
        //{
        //    get
        //    {
        //        PersianCalendar pc = new PersianCalendar();
        //        string result = string.Format($"{pc.GetYear(_dateDoc.Value)}/{pc.GetMonth(_dateDoc.Value)}/{pc.GetDayOfMonth(_dateDoc.Value)}");
        //        return result;
        //    }

        //}
        private string _DateDocString;
        public string DateDocString
        {
            get { return _DateDocString; }
            set { SetProperty(ref _DateDocString, value); }
        }

        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get { return _fromDate; }
            set { SetProperty(ref _fromDate, value); }
        }

        private long _toNumber;
        public long ToNumber
        {
            get { return _toNumber; }
            set { SetProperty(ref _toNumber, value); }
        }
        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get { return _toDate; }
            set { SetProperty(ref _toDate, value); }
        }

        private ObservableCollection<TLDocumentHeader> _tLDocumentHeaders;
        public ObservableCollection<TLDocumentHeader> TLDocumentHeaders
        {
            get { return _tLDocumentHeaders; }
            set { SetProperty(ref _tLDocumentHeaders, value); }
        }
        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }
        private EditableTLDocumentHeader _tLDocumentHeader;
        public EditableTLDocumentHeader TLDocumentHeader
        {
            get { return _tLDocumentHeader; }
            set { SetProperty(ref _tLDocumentHeader, value); }
        }
        private DateTime _tLDocumentDate;
        public DateTime TLDocumentDate
        {
            get { return _tLDocumentDate; }
            set { SetProperty(ref _tLDocumentDate, value); }
        }
        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        private bool _HasACC;
        public bool HasACC
        {
            get { return _HasACC; }
            set { SetProperty(ref _HasACC, value); }
        }
        private bool _HasTL;
        public bool HasTL
        {
            get { return _HasTL; }
            set { SetProperty(ref _HasTL, value); }
        }

        public event Action<TLDocumentHeader> ViewTLDocumentItemRequested;
        public event Action<string> Error;
        public event Action<string> Information;
        public event Action Done;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private void OnView(TLDocumentHeader tLDocumentHeader)
        {
            if (_accessUtility.HasAccess(65))
            {
                ViewTLDocumentItemRequested(tLDocumentHeader);
            }
        }
        //private void RaiseCanExecuteChanged(object sender, EventArgs e)
        //{
        //    SaveCommand.RaiseCanExecuteChanged();
        //}
        private void OnCancel()
        {
            Done?.Invoke();
        }
        public void SetTLDocumentHeader(TLDocumentHeader tLDocumentHeader)
        {
            TLDocumentHeader = Mapper.Map<TLDocumentHeader, EditableTLDocumentHeader>(tLDocumentHeader);

        }
        private bool _IsDaily;
        public bool IsDaily
        {
            get { return _IsDaily; }
            set { SetProperty(ref _IsDaily, value); }
        }
        private bool _IsMonthly;
        private SainaDbContext _uow;

        public bool IsMonthly
        {
            get { return _IsMonthly; }
            set { SetProperty(ref _IsMonthly, value); }
        }


        public async void LoadTLDocumentHeaders()
        {
            IsBusy = true;

            TLDocumentHeader = new EditableTLDocumentHeader();
            var dateDocument = _appContextService.CurrentFinancialYear;
            HasTL = _tLDocumentsService.HasTLDocumentItem(dateDocument);
            HasACC = _accDocumentItemsService.HasAccDocumentItemDate(dateDocument);
            if (HasTL == true && HasACC == true)
            {
                ToDate = _tLDocumentsService.LastDate(dateDocument);
                //  ToNumber= _tLDocumentsService.LastNumber(dateDocument);
            }
            else if (HasTL == false && HasACC == true)
            {
                ToDate = _accDocumentItemsService.Firstdate(dateDocument);
                //ToNumber= _accDocumentItemsService.FirstNumber(dateDocument)+1;
            }
            else if (HasTL == false && HasACC == false)
            {
                ToDate = _currencyExchangesService.GetEndFinancialYear1(dateDocument);

            }
            HasType(dateDocument);
            // LastTLDoc(dateDocument);
            Close = await _openingClosingsService.GetCloseAccAsync(dateDocument);
            if (Close == false)
            {
                IsEnable = true;
            }
            else
            {
                IsEnable = false;
            }

            await LastTLDoc(dateDocument);
            TLDocumentHeaders = new ObservableCollection<TLDocumentHeader>(_tLDocumentsService.GetTLDocumentHeadersAsync(dateDocument).Result);


            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {
                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                HasTL = _tLDocumentsService.HasTLDocumentItem(dateDocument);
                HasACC = _accDocumentItemsService.HasAccDocumentItemDate(dateDocument);
                if (HasTL == true && HasACC == true)
                {
                    ToDate = _tLDocumentsService.LastDate(dateDocument);
                    //  ToNumber= _tLDocumentsService.LastNumber(dateDocument);
                }
                else if (HasTL == false && HasACC == true)
                {
                    ToDate = _accDocumentItemsService.Firstdate(dateDocument);
                    //ToNumber= _accDocumentItemsService.FirstNumber(dateDocument)+1;
                }
                else if (HasTL == false && HasACC == false)
                {
                    ToDate = _currencyExchangesService.GetEndFinancialYear1(dateDocument);

                }
                HasType(dateDocument);
                // LastTLDoc(dateDocument);
                Close = await _openingClosingsService.GetCloseAccAsync(dateDocument);
                if (Close == false)
                {
                    IsEnable = true;
                }
                else
                {
                    IsEnable = false;
                }

                LastTLDocSync(dateDocument);
                TLDocumentHeaders = new ObservableCollection<TLDocumentHeader>(_tLDocumentsService.GetTLDocumentHeadersAsync(dateDocument).Result);


            };
            IsBusy = false;

        }

        private async Task LastTLDoc(int dateDocument)
        {
            NumberDoc = await _tLDocumentsService.GetLastNumberDocAsync(dateDocument);
            DateDoc = await _tLDocumentsService.GetLastDateDocAsync(dateDocument);
        }
        private void LastTLDocSync(int dateDocument)
        {
            NumberDoc = _tLDocumentsService.LastNumber(dateDocument);
            DateDoc = _tLDocumentsService.GetLastDateDoc(dateDocument);
        }
        //private void LastTLDoc(int dateDocument)
        //{

        //}

        private void HasType(int dateDocument)
        {
            var hasType = _tLDocumentsService.HasType(dateDocument);
            if (hasType == Convert.ToInt16(TLDocumentExportEnum.Daily))
            {
                IsDaily = true;
                IsMonthly = false;
            }
            else if (hasType == Convert.ToInt16(TLDocumentExportEnum.Monthly))
            {
                IsDaily = false;
                IsMonthly = true;
            }
            else
            {
                IsDaily = true;
                IsMonthly = true;
            }
        }

        private async void OnExportDocument()
        {
            if (_accessUtility.HasAccess(64))
            {
                long resultNumber = 1;
            var date = _appContextService.CurrentFinancialYear;
            var resultDate = await _currencyExchangesService.GetStartFinancialYear(date);
            Close = await _openingClosingsService.GetCloseAccAsync(date);
                if (Close == false)
                {
                    IsEnable = true;
                    PersianCalendar persianCalendar = new PersianCalendar();
                    HasTL = _tLDocumentsService.HasTLDocumentItem(date);
                    HasACC = _accDocumentItemsService.HasAccDocumentItemDate(date);
                    if (HasACC == true)
                    {
                        if (HasTL == true)
                        {
                            resultNumber = _tLDocumentsService.LastNumber(date);
                            resultDate = _tLDocumentsService.LastDate(date);
                        }
                        else
                        {
                            resultNumber = 1;
                            //  resultNumber = _accDocumentItemsService.FirstNumber(date); 
                            resultDate = _accDocumentItemsService.Firstdate(date);
                        }
                        var tLDocumentExportEnum = TLDocumentHeader.TLDocumentExport;
                        var tLDocumentTypeEnum = TLDocumentHeader.TLDocumentType;
                        if (tLDocumentExportEnum == 0)
                        {
                            Error("نوع صدور انتخاب نشده است");
                        }
                        else
                        {

                            if (ToDate == null)
                            {

                                Error(". تاریخ را وارد نمایید");
                            }
                            else if (ToDate != null)
                            {

                                if (resultDate > ToDate)
                                {
                                    Error("تاریخ را درست وارد نمایید");
                                }
                                else
                                {
                                    if (resultDate < ToDate)
                                    {
                                        var getHeader = _tLDocumentsService.GetAccDocumentHeaders(resultDate, ToDate.Value);
                                        if (getHeader == true)
                                        {
                                            Information?.Invoke("در بین اسناد وضعیت پیش نویس وجود دارد");

                                        }
                                        else
                                        {

                                            var result = _tLDocumentsService.GetExportDocumentDate(date, resultNumber, resultDate.Date, ToDate.Value.Date, tLDocumentExportEnum, tLDocumentTypeEnum);
                                            if (result != null)
                                            {


                                                if (Convert.ToInt64(result) > 0)
                                                {
                                                    Information?.Invoke("ثبت با موفقیت انجام شد");
                                                }
                                                else
                                                {
                                                    Information?.Invoke("سندی برای ثبت وجود ندارد");

                                                }
                                            }
                                        }

                                    }
                                    else if (resultDate > ToDate)
                                    {
                                        Information?.Invoke("تاریخ وارد شده از آخرین تاریخ سند کل کوچکتر است");

                                    }
                                    else if (resultDate == ToDate)
                                    {
                                        Information?.Invoke("تاریخ وارد شده با آخرین تاریخ سند برابر است");

                                    }
                                    HasType(date);
                                    await LastTLDoc(date);
                                    TLDocumentHeaders = new ObservableCollection<TLDocumentHeader>(_tLDocumentsService.GetTLDocumentHeadersAsync(_appContextService.CurrentFinancialYear).Result);

                                }
                            }
                        }
                    }
                    else
                    {
                        Information?.Invoke("در این سال سندی زده نشده است.");
                        IsEnable = false;
                    }
                }
            }
        }
        private async void OnDeleteTLDocumentHeader(TLDocumentHeader tLDocumentHeader)
        {
            if (_accessUtility.HasAccess(66))
            {
                if (Deleting())
            {
                //try
                //{

                _tLDocumentsService.DeleteTLDocumentHeaderAsync(tLDocumentHeader.TLDocumentHeaderId);
                TLDocumentHeaders.Remove(tLDocumentHeader);
                Deleted();
                HasType(_appContextService.CurrentFinancialYear);
                await LastTLDoc(_appContextService.CurrentFinancialYear);
                //}
                //catch (Exception ex)
                //{
                //    Failed(ex);
                //}

            }
               }
        }
        public void DeleteTLDocumentHeader(ObservableCollection<object> tLDocumentHeaders)
        {
           _uow = new SainaDbContext();
            foreach (TLDocumentHeader item in tLDocumentHeaders)
            {
            var q = $"delete Acc.TLDocumentItems where TLDocumentHeaderId={item.TLDocumentHeaderId} delete Acc.TLDocumentHeaders where TLDocumentHeaderId={item.TLDocumentHeaderId}";
             _uow.Database.ExecuteSqlCommand(q);
            }
          

        }

    }


    #endregion
}
//}
