using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.ConvertDoc
{
    class ConvertDocumentListViewModel : BindableBase
    {
        #region Constructors
        public ConvertDocumentListViewModel(IConvertDocumentsService convertDocumentsService,
            IOpeningClosingsService openingClosingsService, IAppContextService appContextService, ISystemAccountingSettingsService systemAccountingSettingsService, IAccDocumentHeadersService accDocumentHeadersService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _systemAccountingSettingsService = systemAccountingSettingsService;
            AccountingSetting = _systemAccountingSettingsService.GetSystemAccountingSettingModel();
            _accDocumentHeadersService = accDocumentHeadersService;
            _openingClosingsService = openingClosingsService;
            _convertDocumentsService = convertDocumentsService;
            _appContextService = appContextService;
            PermanentConvertCommand = new RelayCommand(OnPermanentConvertDocument);
            TemporaryConvertCommand = new RelayCommand(OnTemporaryConvertDocument);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
            // AddAccDocumentItemCommand = new RelayCommand<AccDocumentItem>(OnAddAccDocumentItem);
        }
        #endregion
        #region Fields
        private IConvertDocumentsService _convertDocumentsService;
        private List<AccDocumentHeader> _allAccDocumentHeaders;
        private IAppContextService _appContextService;
        private ISystemAccountingSettingsService _systemAccountingSettingsService;

        public SystemAccountingSettingModel AccountingSetting { get; }

        private IAccDocumentHeadersService _accDocumentHeadersService;
        private IOpeningClosingsService _openingClosingsService;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        //   private ConvertDocument _editingConvertDocument = null;
        #endregion
        #region Commands
        public ICommand PermanentConvertCommand { get; private set; }
        public ICommand TemporaryConvertCommand { get; private set; }

        private AccessUtility _accessUtility;

        //  public ICommand AddAccDocumentItemCommand { get; private set; }
        //public event Action<AccDocumentItem> AddAccDocumentItemRequested;
        public event Action Done;

        #endregion
        #region Properties

        private ObservableCollection<AccDocumentHeaderDTO> _accDocumentHeaders;
        public ObservableCollection<AccDocumentHeaderDTO> AccDocumentHeaders
        {
            get { return _accDocumentHeaders; }
            set { SetProperty(ref _accDocumentHeaders, value); }
        }
        private long _numberPermanent;

        public bool Close { get; private set; }
        public bool IsEnable { get; private set; }

        public long NumberPermanent
        {
            get { return _numberPermanent; }
            set { SetProperty(ref _numberPermanent, value); }
        }
        private DateTime? _datePermanent;
        public DateTime? DatePermanent
        {
            get { return _datePermanent; }
            set
            {
                SetProperty(ref _datePermanent, value);
                if (_datePermanent != null)
                {

                    PersianCalendar pc = new PersianCalendar();
                    DatePermanentString = string.Format($"{pc.GetYear(_datePermanent.Value)}/{pc.GetMonth(_datePermanent.Value)}/{pc.GetDayOfMonth(_datePermanent.Value)}");
                }
                else
                {
                    DatePermanentString = "";
                }

            }
        }
        private int _toNumber;
        public int ToNumber
        {
            get { return _toNumber; }
            set { SetProperty(ref _toNumber, value); SetProperty(ref _toDate, null); }
        }
        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get { return _toDate; }
            set { SetProperty(ref _toDate, value); SetProperty(ref _toNumber, 0); }
        }
        public event Action<string> Error;
        public event Action<string> Information;
        private EditableTypeDocument _typeDocument;
        public EditableTypeDocument TypeDocument
        {
            get { return _typeDocument; }
            set { SetProperty(ref _typeDocument, value); }
        }
        private string _DatePermanentString;
        public string DatePermanentString
        {
            get { return _DatePermanentString; }
            set { SetProperty(ref _DatePermanentString, value); }
        }

        #endregion
        #region Methode
        //public void Get()
        //{
        //    var Get = _systemAccountingSettingsService.GetSystemAccountingSettingModel();
        //    StatusConvert = Get.StatusAcc;
        //    if (StatusConvert != "")
        //    {
        //        if (StatusConvert == "Date")
        //        {
        //            EnableDate = true;
        //            EnableNumber = false;
        //        }
        //        else if (StatusConvert == "Number")
        //        {
        //            EnableDate = false;
        //            EnableNumber = true;
        //        }
        //    }
        //    else
        //    {
        //        EnableDate = true;
        //        EnableNumber = true;
        //    }
        //}
        public async void LoadAccDocumentItems()
        {
            IsBusy = true;
            SystemAccounting = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            ToDate = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            var dateConvert = persianCalendar.GetYear(ToDate.Value);
            var dateDocument = _appContextService.CurrentFinancialYear;
            if (dateConvert!= dateDocument)
            {
                ToDate = _convertDocumentsService.GetEndFinancialYear1(dateDocument);
            }
            //  Get();
            Close = await _openingClosingsService.GetCloseAccAsync(dateDocument);

            if (Close == false)
            {
                IsBusy = true;
                IsEnable = true;
                await GetLast(dateDocument);
                AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(dateDocument));
                IsBusy = false;
            }
            else
            {
                IsEnable = false;
            }
            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {

                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                Close =  _openingClosingsService.GetCloseAcc(dateDocument);

                if (Close == false)
                {
                    IsBusy = true;
                    IsEnable = true;
                    await GetLast(dateDocument);

                    //NumberPermanent = await _convertDocumentsService.GetLastNumberPermanentAsync(dateDocument);
                    //DatePermanent = await _convertDocumentsService.GetLastDatePermanentAsync(dateDocument);
                    AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>( _accDocumentHeadersService.GetAccDocumentHeadersAsync(dateDocument).Result);
                    IsBusy = false;
                }
                else
                {
                    IsEnable = false;
                }

            };


        }

        private async Task GetLast(int dateDocument)
        {
            NumberPermanent = await _convertDocumentsService.GetLastNumberPermanentAsync(dateDocument);
            DatePermanent = await _convertDocumentsService.GetLastDatePermanentAsync(dateDocument);
        }

        public EditableSystemAccountingSettingModel SystemAccountingSettingModel { get; set; }
        private string _StatusConvert;

        public EditableSystemAccountingSettingModel SystemAccounting { get; private set; }
        public SystemAccountingSettingModel EditableSystemAccountingSettingModel { get; private set; }

        public string StatusConvert
        {
            get { return _StatusConvert; }
            set
            {
                SetProperty(ref _StatusConvert, value);

            }
        }

        private bool _EnableDate;

        public bool EnableDate
        {
            get { return _EnableDate; }
            set
            {
                SetProperty(ref _EnableDate, value);

            }
        }
        private bool _EnableNumber;

        public bool EnableNumber
        {
            get { return _EnableNumber; }
            set
            {
                SetProperty(ref _EnableNumber, value);

            }

        }
        public EditableSystemAccountingSettingModel SystemAccounting2 { get; set; }
        private async void OnPermanentConvertDocument()
        {
            if (_accessUtility.HasAccess(60))
            {
                var dateDocument = _appContextService.CurrentFinancialYear;
                var username = _appContextService.CurrentUser.FriendlyName;
                //if (StatusConvert != "")
                //{
                //    if (StatusConvert == "Date")
                //    {
                if (ToDate != null)
                {
                    var x = await _convertDocumentsService.CanUpdate(dateDocument, ToDate.Value.Date);
                    if (x == false)
                    {
                        await _convertDocumentsService.UpdateAccToDateAsync(dateDocument, StatusEnum.End, ToDate.Value.Date, StatusEnum.Permanent, username);


                        AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
                        await GetLast(dateDocument);
                        // Get();
                        Information("تبدیل اسناد دائم با موفقیت انجام شد");
                    }
                    else
                    {
                        Information("در بین اسناد وضعیت پیش نویس  یا موقت وجود دارد، امکان تبدیل اسناد وجود ندارد");

                    }
                }
                else
                {
                    Error(".لطفا تاریخ  خود را وارد نمایید");

                }
            }
            //}
            //else if (StatusConvert == "Number")
            //{
            //    if (ToNumber != 0)
            //    {
            //        var x = await _convertDocumentsService.UpdateAccToNumberAsync(dateDocument, StatusEnum.End, ToNumber, StatusEnum.Permanent);
            //        AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
            //        Get();

            //        Information("تبدیل اسناد دائم با موفقیت انجام شد");

            //    }
            //    else
            //    {
            //        Error(".لطفا تاریخ  خود را وارد نمایید");

            //    }
            //}
            //}
            //else
            //{
            // if (ToDate != null && ToNumber != 0)
            // {
            //     Error(".لطفا تنها تاریخ یا تنهاعدد خود را وارد نمایید");

            // }
            //else if (ToDate != null)
            // {
            //     await _convertDocumentsService.UpdateAccToDateAsync(dateDocument, StatusEnum.End, ToDate.Value.Date, StatusEnum.Permanent);
            //     AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
            //     Assign("Date");
            //     Get();
            //     Information("تبدیل اسناد دائم با موفقیت انجام شد");

            // }
            // else if (ToNumber != 0)
            // {
            //     await _convertDocumentsService.UpdateAccToNumberAsync(dateDocument, StatusEnum.End, ToNumber, StatusEnum.Permanent);
            //     AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
            //     Assign("Number");
            //     Get();
            //     Information("تبدیل اسناد دائم با موفقیت انجام شد");


            // }
            //    else
            //    {
            //        Error(".لطفا تاریخ یا عدد خود را وارد نمایید");

            //    }
            //}


        }
        private async void OnTemporaryConvertDocument()
        {
            if (_accessUtility.HasAccess(59))
            {
                var username = _appContextService.CurrentUser.FriendlyName;

                var dateDocument = _appContextService.CurrentFinancialYear;
                //if (StatusConvert != "")
                //{

                //    if (StatusConvert == "Date")
                //    {
                if (ToDate != null)
                {
                    await _convertDocumentsService.UpdateAccToDateAsync(dateDocument, StatusEnum.Permanent, ToDate.Value.Date, StatusEnum.End, username);
                    AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
                    // Get();

                    Information("تبدیل اسناد خاتمه با موفقیت انجام شد");

                }
                else
                {
                    Error(".لطفا تاریخ  خود را وارد نمایید");

                }
            }
            //    }
            //    else if (StatusConvert == "Number")
            //    {
            //        if (ToNumber != 0)
            //        {
            //            var x = await _convertDocumentsService.UpdateAccToNumberAsync(dateDocument, StatusEnum.Permanent, ToNumber, StatusEnum.End);
            //            AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));

            //            Get();
            //            Information("تبدیل اسناد خاتمه با موفقیت انجام شد");

            //        }
            //        else
            //        {
            //            Error(".لطفا تاریخ  خود را وارد نمایید");

            //        }
            //    }
            //}
            //else
            //{
            //    if (ToDate != null && ToNumber != 0)
            //    {
            //        Error(".لطفا تنها تاریخ یا تنهاعدد خود را وارد نمایید");

            //    }
            //    else if (ToDate != null)
            //    {
            //        await _convertDocumentsService.UpdateAccToDateAsync(dateDocument, StatusEnum.Permanent, ToDate.Value.Date, StatusEnum.End);
            //        AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
            //        Assign("Date");
            //        Get();
            //        Information("تبدیل اسناد خاتمه با موفقیت انجام شد");

            //    }
            //    else if (ToNumber != 0)
            //    {
            //        await _convertDocumentsService.UpdateAccToNumberAsync(dateDocument, StatusEnum.Permanent, ToNumber, StatusEnum.End);
            //        AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear));
            //        Assign("Number");
            //        Get();
            //        Information("تبدیل اسناد خاتمه با موفقیت انجام شد");


            //    }
            //    else
            //    {
            //        Error(".لطفا تاریخ یا عدد خود را وارد نمایید");

            //    }
            //}



        }
        public void Assign(string type)
        {
            SystemAccounting.StatusAcc = type;
            EditableSystemAccountingSettingModel = AutoMapper.Mapper.Map<EditableSystemAccountingSettingModel, SystemAccountingSettingModel>(SystemAccounting);

            _systemAccountingSettingsService.SaveSystemAccountingSettingModel(EditableSystemAccountingSettingModel);
            var AccountingSetting = _systemAccountingSettingsService.GetSystemAccountingSettingModel();
            StatusConvert = AccountingSetting.StatusAcc;
        }


        #endregion
    }
}
