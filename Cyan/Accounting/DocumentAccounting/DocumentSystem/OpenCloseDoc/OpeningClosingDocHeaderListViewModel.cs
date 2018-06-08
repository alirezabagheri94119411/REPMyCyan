using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.OpenCloseDoc
{
    class OpeningClosingDocHeaderListViewModel:BindableBase
    {
        #region Constructors
        public OpeningClosingDocHeaderListViewModel(IAccDocumentHeadersService accDocumentHeadersService, IAppContextService appContextService, IOpeningClosingsService openingClosingsService)
        {
            _accDocumentHeadersService = accDocumentHeadersService;
            _appContextService = appContextService;
            _openingClosingsService = openingClosingsService;

            EditAccDocumentHeaderCommand = new RelayCommand<AccDocumentHeader>(OnEditAccDocumentHeader);
            ReturnCommand = new RelayCommand(OnReturnd);
            DeleteCommand = new RelayCommand<AccDocumentHeader>(OnDeleteAccDocumentHeader);
        }


        #endregion
        #region Fields
        private IOpeningClosingsService _openingClosingsService;

        private IAccDocumentHeadersService _accDocumentHeadersService;
        private List<AccDocumentHeader> _allAccDocumentHeaders;
        private IAppContextService _appContextService;

        #endregion
        #region Commands
        // public ICommand AddAccDocumentItemCommand { get; private set; }
        public ICommand ReturnCommand { get; private set; }

        public ICommand AddAccDocumentHeaderCommand { get; private set; }
        public ICommand EditAccDocumentHeaderCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<AccDocumentHeader> _accDocumentHeaders;
        public ObservableCollection<AccDocumentHeader> AccDocumentHeaders
        {
            get { return _accDocumentHeaders; }
            set { SetProperty(ref _accDocumentHeaders, value); }
        }
        private ObservableCollection<AccDocumentItem> _accDocumentItems;
        public ObservableCollection<AccDocumentItem> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set { SetProperty(ref _accDocumentItems, value); }
        }


        private bool _closing;
        public bool Closing
        {
            get { return _closing; }
            set { SetProperty(ref _closing, value); }
        }
        private bool _opening;
        public bool Opening
        {
            get { return _opening; }
            set { SetProperty(ref _opening, value); }
        }


        //  public event Action<AccDocumentHeader> AddAccDocumentHeaderRequested;
        public event Action<AccDocumentHeader> EditAccDocumentHeaderRequested;
        // public event Action<AccDocumentHeader> AddAccDocumentItemRequested;
        public event Action ReturnRequested;

        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<string> Error;

        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private void OnReturnd()
        {
            //   typeDocumentId = TypeDocumentId;
            ReturnRequested();
        }
        public async void LoadAccDocumentHeaders()
        {
            IsBusy = true;

            var dateDocument = _appContextService.CurrentFinancialYear;
            if (AccDocumentHeaders == null || !AccDocumentHeaders.Any())
            {
                _allAccDocumentHeaders = await _accDocumentHeadersService.GetOpenCloseHeadersAsync(dateDocument, 3,4);
                AccDocumentHeaders = new ObservableCollection<AccDocumentHeader>(_allAccDocumentHeaders);
            }
            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {
                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                _allAccDocumentHeaders = await _accDocumentHeadersService.GetOpenCloseHeadersAsync(dateDocument, 3,4);
                AccDocumentHeaders = new ObservableCollection<AccDocumentHeader>(_allAccDocumentHeaders);
            };
            IsBusy = false;
        }

        private void OnEditAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            EditAccDocumentHeaderRequested(accDocumentHeader);
        }
        private async void OnDeleteAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            if (Deleting())
            {
                try
                {
                    await _accDocumentHeadersService.DeleteAccDocumentHeaderAsync(accDocumentHeader.AccDocumentHeaderId);
                    AccDocumentHeaders.Remove(accDocumentHeader);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }

        #endregion
    }
}
