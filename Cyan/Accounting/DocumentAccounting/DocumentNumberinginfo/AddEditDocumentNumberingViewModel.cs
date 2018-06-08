using AutoMapper;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentNumberinginfo
{
    /// <summary>
    /// افزودن و ویرایش شماره گذاری اسناد
    /// </summary>
    class AddEditDocumentNumberingViewModel : BindableBase
    {
        #region Constructors
        public AddEditDocumentNumberingViewModel(IDocumentNumberingsService documentNumberingsService, IAppContextService appContextService, IAccDocumentHeadersService accDocumentHeadersService, IAccountDocumentsService accountDocumentsService, IStylesService stylesService, ICountingWaysService countingWaysService)
        {
            _documentNumberingsService = documentNumberingsService;
            _accDocumentHeadersService = accDocumentHeadersService;
            _appContextService = appContextService;
            _accountDocumentsService = accountDocumentsService;
            _stylesService = stylesService;
            _countingWaysService = countingWaysService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            AccountDocumentsDropDownOpenedCommand = new RelayCommand(OnAccountDocumentsDropDownOpened, () => AccountDocuments != null && AccountDocuments.Any());
        }
        #endregion
        #region Fields
        private IDocumentNumberingsService _documentNumberingsService;
        private List<AccountDocument> _allAccountDocuments;
        private IAccDocumentHeadersService _accDocumentHeadersService;
        private List<AccDocumentHeaderDTO> _allAccDocumentHeaders;
        private List<Style> _allStyles;
        private List<CountingWay> _allCountingWays;
        private IAppContextService _appContextService;


        private IAccountDocumentsService _accountDocumentsService;
        private IStylesService _stylesService;
        private ICountingWaysService _countingWaysService;
        //   private DocumentNumbering _editingDocumentNumbering = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand AccountDocumentsDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableDocumentNumbering _DocumentNumbering;
        public EditableDocumentNumbering DocumentNumbering
        {
            get { return _DocumentNumbering; }
            set { SetProperty(ref _DocumentNumbering, value); }
        }
        private ObservableCollection<AccountDocument> _currencies;
        public ObservableCollection<AccountDocument> AccountDocuments
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        private ObservableCollection<Style> _styles;

        public ObservableCollection<Style> Styles
        {
            get { return _styles; }
            set { SetProperty(ref _styles, value); }
        }
        private ObservableCollection<CountingWay> _countingWays;

        public ObservableCollection<CountingWay> CountingWays
        {
            get { return _countingWays; }
            set { SetProperty(ref _countingWays, value); }
        }
        private ObservableCollection<AccDocumentHeaderDTO> _accDocumentHeaders;

        public ObservableCollection<AccDocumentHeaderDTO> AccDocumentHeaders
        {
            get { return _accDocumentHeaders; }
            set { SetProperty(ref _accDocumentHeaders, value); }
        }


        public event Action Done;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private async void OnAccountDocumentsDropDownOpened()
        {
            _allAccountDocuments = await _accountDocumentsService.GetAccountDocumentsAsync();
            AccountDocuments = new ObservableCollection<AccountDocument>(_allAccountDocuments);
        }
        public async void LoadAccountDocuments()
        {
            _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear);
            AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(_allAccDocumentHeaders);
            if (EditMode)
            {
                if (AccDocumentHeaders?.Any() == true)
                {
                    DocumentNumbering.IsReadOnly = false;
                }
                else
                {
                    DocumentNumbering.IsReadOnly = true;
                    if (Styles == null)
                    {
                        _allStyles = await _stylesService.GetStylesAsync();
                        Styles = new ObservableCollection<Style>(_allStyles);

                    }
                }
                _appContextService.PropertyChanged += async (sender, eventArgs) =>
                {
                    _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccDocumentHeadersAsync(_appContextService.CurrentFinancialYear);
                    AccDocumentHeaders = new ObservableCollection<AccDocumentHeaderDTO>(_allAccDocumentHeaders);
                    if (AccDocumentHeaders?.Any() == true)
                    {
                        DocumentNumbering.IsReadOnly = false;
                    }
                    else
                    {
                        DocumentNumbering.IsReadOnly = true;
                        if (Styles == null)
                        {
                            _allStyles = await _stylesService.GetStylesAsync();
                            Styles = new ObservableCollection<Style>(_allStyles);

                        }
                    }
                };
            }

            else
            {

                DocumentNumbering.IsReadOnly = true;
                if (AccountDocuments == null)
                {
                    _allAccountDocuments = await _accountDocumentsService.GetAccountDocumentsAsync();
                    AccountDocuments = new ObservableCollection<AccountDocument>(_allAccountDocuments);
                }

                if (CountingWays == null)
                {

                    _allCountingWays = await _countingWaysService.GetCountingWaysAsync();
                    CountingWays = new ObservableCollection<CountingWay>(_allCountingWays);

                }
                if (Styles == null)
                {
                    _allStyles = await _stylesService.GetStylesAsync();
                    Styles = new ObservableCollection<Style>(_allStyles);

                }
            }
        }
        public void SetDocumentNumbering(DocumentNumbering documentNumbering)
        {
            DocumentNumbering = Mapper.Map<DocumentNumbering, EditableDocumentNumbering>(documentNumbering);
            DocumentNumbering.ValidationDelegate += DocumentNumbering_ValidationDelegate;

            DocumentNumbering.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        private async void OnSave()
        {
            var editingDocumentNumbering = Mapper.Map<EditableDocumentNumbering, DocumentNumbering>(DocumentNumbering);
            try
            {
                if (EditMode)
                    await _documentNumberingsService.UpdateDocumentNumberingAsync(editingDocumentNumbering);
                else
                    await _documentNumberingsService.AddDocumentNumberingAsync(editingDocumentNumbering);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                DocumentNumbering = null;
            }
        }

        private bool CanSave()
        {
            return !DocumentNumbering.HasErrors;
        }
        private async Task<List<string>> DocumentNumbering_ValidationDelegate(object sender, string propertyName)
        {



            var documentNumbering = (EditableDocumentNumbering)sender;

            List<string> errors = new List<string>();

            var start = documentNumbering.StartNumber;
            var end = documentNumbering.EndNumber;

            switch (propertyName)
            {
                case nameof(EditableDocumentNumbering.AccountDocumentId):

                    if (await _documentNumberingsService.HasTitle(documentNumbering.AccountDocumentId))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;
                case nameof(EditableDocumentNumbering.StartNumber):

                    if (start == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (start >= end)
                    {
                        errors.Add("شماره شروع نباید از شماره پایان بزرگتر باشد");
                    }


                    break;
                case nameof(EditableDocumentNumbering.EndNumber):

                    if (end == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (start >= end)
                    {
                        errors.Add("شماره پایان نباید از شماره شروع کوچکتر باشد");
                    }
                    //   documentNumbering.ValidateProperty(nameof(EditableDocumentNumbering.EndNumber), documentNumbering.EndNumber);
                    documentNumbering.ValidateProperty(nameof(EditableDocumentNumbering.StartNumber), documentNumbering.StartNumber);



                    break;

                default:
                    return null;
            }
            return errors;
        }
        #endregion
    }
}
