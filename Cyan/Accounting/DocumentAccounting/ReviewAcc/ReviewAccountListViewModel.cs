using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.DTOs.Accounting.ReviewAcc;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Common.Toolkit;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    class ReviewAccountListViewModel : BindableBase
    {
        #region Constructors
        public ReviewAccountListViewModel(IReviewAccountsService reviewAccountsService,
            ISelectFinancialYearsService selectFinancialYearsService,IAppContextService appContextService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _reviewAccountsService = reviewAccountsService;
            _appContextService = appContextService;
            AccDocumentHeaderFilter = new AccDocumentHeaderFilter
            {
            };
            _selectFinancialYearsService = selectFinancialYearsService;

            GLGroupedCommand = new RelayCommand(OnGLGrouped);
            TLGroupedCommand = new RelayCommand(OnTLGrouped);
            SLGroupedCommand = new RelayCommand(OnSLGrouped);
            DL1GroupedCommand = new RelayCommand(OnDL1Grouped);
            DL2GroupedCommand = new RelayCommand(OnDL2Grouped);
            FromYearsDropDownOpenedCommand = new RelayCommand(OnFromYearsDropDownOpened);
            ToYearsDropDownOpenedCommand = new RelayCommand(OnToYearsDropDownOpened);

            CurrencyGroupedCommand = new RelayCommand(OnCurrencyGrouped);
            TrackingGroupedCommand = new RelayCommand(OnTrackingGrouped);

            GLDetailedCommand = new RelayCommand<string>(OnGLDetailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedGL); });
            TLDetailedCommand = new RelayCommand<string>(OnTLDetailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedTL); });
            SLDetailedCommand = new RelayCommand<string>(OnSLDetailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedSL); });
            DL1DetailedCommand = new RelayCommand<string>(OnDL1Detailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedDL1); });
            DL2DetailedCommand = new RelayCommand<string>(OnDL2Detailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedDL2); });
            CurrencyDetailedCommand = new RelayCommand<string>(OnCurrencySLDetailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedCurrency); });
            TrackingDetailedCommand = new RelayCommand<string>(OnTrackingDetailed, (s) => { return GroupStatus.HasFlag(GroupStatusEnum.DetailedTracking); });
            UndoCommand = new RelayCommand(Undo, () => { return GroupStatus.HasFlag(GroupStatusEnum.Undo); });

            ApplyFilterCommand = new RelayCommand(OnApplyFilter, () => AccDocumentHeaderFilter != null);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;

        internal void RaiseTestRequested(int headerId,int date,bool hasFlow)
        {
            TestRequested?.Invoke(headerId,date, hasFlow);
        }
        internal void Raisedetail(AccDocumentItemDTO accDocumentItemDTO ,string s)
        {
            DetailRequested?.Invoke(accDocumentItemDTO,s);
        }
        private void Undo()
        {
            //var t=EnumHelper.GetFlagNumbers((int)(GroupStatus& ~GroupStatusEnum.Undo));
            //GroupStatus &=~(GroupStatusEnum)(t.Max());
            if (dataStack.Any())
            {
                //var c = commandStack.Pop();
                var d = dataStack.Pop();
                AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(d.AccDocumentItemDTOs);
                ColumnName = d.ColumnName;
                //AccDocumentHeaderFilter.CurrentMethodName = d.CurrentMethodName;
                GroupStatus = d.GroupStatus;
                Path = d.Path;
                SelectedAccDocumentItem = d.SelectedAccDocumentItem;
                //if (c != null && c.CanExecute(null))
                //c.Execute("back");
            }

        }

        private async void OnApplyFilter()
        {
            //await _reviewAccountsService.ApplyFilterOld();
            _reviewAccountsService.AccDocumentHeaderFilter = AccDocumentHeaderFilter;
            //OnGLGrouped();
            //            (AccDocumentHeaderFilter.CurrentMethodName == "OnGLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnGLDetailed") && x.SL.TL.GLId == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnTLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTLDetailed") && x.SL.TLId == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnSLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnSLDetailed") && x.SLId == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Detailed") && x.DL1Id == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Detailed") && x.DL2Id == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyDetailed") && x.CurrencyId == AccDocumentHeaderFilter.Id ||
            //(AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingDetailed") && x.TrackingNumber == AccDocumentHeaderFilter.TrackingNumber

            switch (AccDocumentHeaderFilter.CurrentMethodName)
            {
                default:
                    break;
            }
        }

        #endregion
        #region Fields
        private IReviewAccountsService _reviewAccountsService;
        private IAppContextService _appContextService;
        private List<AccDocumentItemDTO> _allAccDocumentItems;
        private List<FinancialYear> _allFinancialYears;

        private List<SL> _allSLs;
        private string _selectedCommand;
        //   private ReviewAccount _editingReviewAccount = null;
        #endregion
        #region Commands
        public ICommand FromYearsDropDownOpenedCommand { get; }
        public ICommand ToYearsDropDownOpenedCommand { get; }
        public ICommand GLGroupedCommand { get; private set; }
        public ICommand TLGroupedCommand { get; private set; }
        public ICommand SLGroupedCommand { get; private set; }
        public ICommand DL1GroupedCommand { get; private set; }
        public ICommand DL2GroupedCommand { get; private set; }
        public ICommand CurrencyGroupedCommand { get; private set; }
        public ICommand TrackingGroupedCommand { get; private set; }
        public ICommand GLDetailedCommand { get; private set; }
        public ICommand TLDetailedCommand { get; private set; }
        public ICommand SLDetailedCommand { get; private set; }
        public ICommand DL1DetailedCommand { get; private set; }
        public ICommand DL2DetailedCommand { get; private set; }
        public ICommand CurrencyDetailedCommand { get; private set; }
        public ICommand TrackingDetailedCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand ApplyFilterCommand { get; private set; }

        private AccessUtility _accessUtility;
        Stack<ICommand> commandStack = new Stack<ICommand>();
        Stack<Data> dataStack = new Stack<Data>();
        public event Action<AccDocumentItemDTO> AddAccDocumentItemRequested;
        public event Action Done;
        public event Action<int,int,bool> TestRequested;
        public  Action<AccDocumentItemDTO,string> DetailRequested;

        #endregion
        #region Properties

        private AccDocumentItemListViewModel _accDocumentItemListViewModel;

        public AccDocumentItemListViewModel AccDocumentItemListViewModel
        {
            get { return _accDocumentItemListViewModel; }
            set { SetProperty(ref _accDocumentItemListViewModel, value); }
        }
        private ObservableCollection<AccDocumentItemDTO> _accDocumentItems;
        public ObservableCollection<AccDocumentItemDTO> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set
            {
                SetProperty(ref _accDocumentItems, value);
                SelectedAccDocumentItem = value.FirstOrDefault();
            }
        }
        private AccDocumentItemDTO _SelectedAccDocumentItem;
        public AccDocumentItemDTO SelectedAccDocumentItem
        {
            get { return _SelectedAccDocumentItem; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _SelectedAccDocumentItem, value);
                    AccDocumentHeaderFilter.Id = SelectedAccDocumentItem.Id;
                    AccDocumentHeaderFilter.Code = SelectedAccDocumentItem.Code;
                }
            }
        }
        private ObservableCollection<FinancialYear> _FinancialYears;
        public ObservableCollection<FinancialYear> FinancialYears
        {
            get { return _FinancialYears; }
            set { SetProperty(ref _FinancialYears, value); }
        }

        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set
            {
                SetProperty(ref _columnName, value);
                if (AccDocumentHeaderFilter?.CurrentMethodName != null && AccDocumentHeaderFilter.CurrentMethodName.EndsWith("Grouped")) commandStack.Clear();
            }
        }
        private AccDocumentHeaderFilter _AccDocumentHeaderFilter;
        public AccDocumentHeaderFilter AccDocumentHeaderFilter
        {
            get { return _AccDocumentHeaderFilter; }
            set { SetProperty(ref _AccDocumentHeaderFilter, value); _reviewAccountsService.AccDocumentHeaderFilter = value; }
        }

        private ISelectFinancialYearsService _selectFinancialYearsService;
        private GroupStatusEnum _GroupStatus;
        public GroupStatusEnum GroupStatus
        {
            get { return _GroupStatus; }
            set
            {
                SetProperty(ref _GroupStatus, value);
                if (((int)GroupStatus) > 1)
                    _GroupStatus |= GroupStatusEnum.Undo;
                RaiseCanExecuteChanged();
            }
        }
        private string _Path;
        public string Path
        {
            get { return _Path; }
            set { SetProperty(ref _Path, value);if (_Path == "") dataStack = new Stack<Data>(); }
        }
        private bool _IsBusyGridView;
        public bool IsBusyGridView
        {
            get { return _IsBusyGridView; }
            set { SetProperty(ref _IsBusyGridView, value); }
        }

        #endregion
        #region Methode
        public async void LoadAccDocumentItems()
        {
            //await _reviewAccountsService.ApplyFilterOld();
            ((ICommand)GLGroupedCommand).Execute(null);
            GroupStatus = GroupStatusEnum.All & ~GroupStatusEnum.DetailedGL;
            commandStack.Push(GLDetailedCommand);
            FinancialYears =new ObservableCollection<FinancialYear>( await _selectFinancialYearsService.GetFinancialYearsActiveAsync());
            //AccDocumentHeaderFilter.FromDate = _appContextService.SelectedFinancialYear.StartDate;
            //AccDocumentHeaderFilter.ToDate = _appContextService.SelectedFinancialYear.EndDate;

            AccDocumentHeaderFilter.
   FromDate = _selectFinancialYearsService.GetFirstFinancialYear() ?? DateTime.Now;
            AccDocumentHeaderFilter.
             ToDate = _selectFinancialYearsService.GetLastFinancialYear() ?? DateTime.Now;

            OnGLGrouped();
        }

        private async void OnFromYearsDropDownOpened()
        {
            //_allFinancialYears = await _selectFinancialYearsService.GetFinancialYearsActiveAsync();
            //FinancialYears = new ObservableCollection<FinancialYear>(_allFinancialYears);
        }
        private async void OnToYearsDropDownOpened()
        {
            //_allFinancialYears = await _reviewAccountsService.GetFinancialYearAsync(1395);
            //FinancialYears = new ObservableCollection<FinancialYear>(_allFinancialYears);
        }
        private async void OnGLGrouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "کد گروه";
            AccDocumentHeaderFilter.CurrentMethodName = "OnGLGrouped";
            GroupStatus = GroupStatusEnum.All & ~(GroupStatusEnum.DetailedGL);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedGLAsync();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }
        private async void OnTLGrouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "کد کل";
            AccDocumentHeaderFilter.CurrentMethodName = "OnTLGrouped";
            GroupStatus = GroupStatusEnum.All & ~(GroupStatusEnum.DetailedTL);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedTLAsync();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }
        private async void OnSLGrouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "کد معین";
            AccDocumentHeaderFilter.CurrentMethodName = "OnSLGrouped";
            GroupStatus = GroupStatusEnum.All;
            GroupStatus &= ~(GroupStatusEnum.DetailedSL);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedSLAsync();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }
        private async void OnDL1Grouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "کد تفصیل اول";
            AccDocumentHeaderFilter.CurrentMethodName = "OnDL1Grouped";
            GroupStatus = GroupStatusEnum.All;
            GroupStatus &= ~(GroupStatusEnum.DetailedDL1);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedDL1Async();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }
        private async void OnDL2Grouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "کد تفصیل دوم";
            AccDocumentHeaderFilter.CurrentMethodName = "OnDL2Grouped";
            GroupStatus = GroupStatusEnum.All;
            GroupStatus &= ~(GroupStatusEnum.DetailedDL2);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedDL2Async();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }

        private async void OnCurrencyGrouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "ارز";
            AccDocumentHeaderFilter.CurrentMethodName = "OnCurrencyGrouped";
            AccDocumentHeaderFilter.HasCurrency = true;

            GroupStatus = GroupStatusEnum.All;
            GroupStatus &= ~(GroupStatusEnum.DetailedCurrency);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedCurrencyAsync();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }
        private async void OnTrackingGrouped()
        {
            IsBusyGridView = true;
            Path = "";
            ColumnName = "شماره پیگیری";
            AccDocumentHeaderFilter.CurrentMethodName = "OnTrackingGrouped";
            GroupStatus = GroupStatusEnum.All;
            GroupStatus &= ~(GroupStatusEnum.DetailedTracking);
            _allAccDocumentItems = await _reviewAccountsService.GetGroupedTrackingAsync();
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            IsBusyGridView = false;
        }

        private async void OnGLDetailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "کد گروه";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedGLAsync();
            AccDocumentHeaderFilter.CurrentMethodName = "OnGLDetailed";
            SetGroupStatus(GroupStatusEnum.DetailedGL);
            //GroupStatus = GroupStatusEnum.All;
            //GroupStatus &= ~(GroupStatusEnum.DetailedGL);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            commandStack.Push(GLDetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}";
               // Path = $"{sl.TL?.GL?.GLCode}";
            }
            IsBusyGridView = false;
        }
        void SetGroupStatus(GroupStatusEnum groupStatusEnum)
        {
            IsBusyGridView = true;
            var t1 = EnumHelper.GetFlagNumbers((int)GroupStatusEnum.All);
            var s = (t1.Where(x => x > (int)groupStatusEnum).Sum());
            var s2 = EnumHelper.GetFlagNumbers(s).Cast<GroupStatusEnum>();
            GroupStatus = 0;
            foreach (var item in s2)
            {
                GroupStatus |= item;
            }
            //GroupStatus = (GroupStatusEnum)(s);
            IsBusyGridView = false;
        }
        private async void OnTLDetailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "کد کل";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedTLAsync();
            AccDocumentHeaderFilter.CurrentMethodName = "OnTLDetailed";
            SetGroupStatus(GroupStatusEnum.DetailedTL);
            //GroupStatus &= ~(GroupStatusEnum.DetailedTL);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(TLDetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}";
              //  Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}";
            }
            IsBusyGridView = false;
        }
        private async void OnSLDetailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "کد معین";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedSLAsync();
            AccDocumentHeaderFilter.CurrentMethodName = "OnSLDetailed";
            SetGroupStatus(GroupStatusEnum.DetailedSL);
            //GroupStatus &= ~(GroupStatusEnum.DetailedSL);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(SLDetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}-->{sl?.Title}";
               // Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}-->{sl?.SLCode}";
            }
            IsBusyGridView = false;
        }
        private async void OnDL1Detailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "کد تفصیل اول";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedDL1Async();
            AccDocumentHeaderFilter.CurrentMethodName = "OnDL1Detailed";
            SetGroupStatus(GroupStatusEnum.DetailedDL1);
            //GroupStatus &= ~(GroupStatusEnum.DetailedDL1);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(DL1DetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}-->{sl?.Title}-->{_allAccDocumentItems.FirstOrDefault()?.Title}";

                //Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}-->{sl?.SLCode}-->{_allAccDocumentItems.FirstOrDefault()?.Code}";
            }
            IsBusyGridView = false;
        }
        private async void OnDL2Detailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "کد تفصیل دوم";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedDL2Async();
            AccDocumentHeaderFilter.CurrentMethodName = "OnDL2Detailed";
            SetGroupStatus(GroupStatusEnum.DetailedDL2);
            ////GroupStatus &= ~(GroupStatusEnum.DetailedDL2);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(DL2DetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}-->{sl?.Title}-->{_allAccDocumentItems.FirstOrDefault()?.Title}";

                //  Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}-->{sl?.SLCode}-->{_allAccDocumentItems.FirstOrDefault()?.Code}";
            }
            IsBusyGridView = false;
        }

        private async void OnCurrencySLDetailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "ارز";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedCurrencyAsync();
            AccDocumentHeaderFilter.CurrentMethodName = "OnCurrencySLDetailed";
            AccDocumentHeaderFilter.HasCurrency = true;
            SetGroupStatus(GroupStatusEnum.DetailedCurrency);
            //GroupStatus &= ~(GroupStatusEnum.DetailedCurrency);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(CurrencyDetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}-->{sl?.Title}-->{_allAccDocumentItems.FirstOrDefault()?.Title}";

                // Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}-->{sl?.SLCode}-->{_allAccDocumentItems.FirstOrDefault()?.Code}";
            }
            IsBusyGridView = false;
        }
        private async void OnTrackingDetailed(string sender)
        {
            if (sender != "back")
                dataStack.Push(new Data { ColumnName = ColumnName, AccDocumentItemDTOs = AccDocumentItems.ToList(), SelectedAccDocumentItem = AutoMapper.Mapper.Map<AccDocumentItemDTO, AccDocumentItemDTO>(SelectedAccDocumentItem), CurrentMethodName = "OnGLDetailed", GroupStatus = GroupStatus, Path = Path });
            IsBusyGridView = true;
            ColumnName = "شماره پیگیری";
            _allAccDocumentItems = await _reviewAccountsService.GetDetailedTrackingAsync();
            AccDocumentHeaderFilter.CurrentMethodName = "OnTrackingDetailed";
            SetGroupStatus(GroupStatusEnum.DetailedTracking);
            //GroupStatus &= ~(GroupStatusEnum.DetailedTracking);
            AccDocumentItems = new ObservableCollection<AccDocumentItemDTO>(_allAccDocumentItems);
            if (sender != "back")
                commandStack.Push(TrackingDetailedCommand);
            var sl = _reviewAccountsService.GetSL();
            if (sl != null)
            {
                //Path = $"{sl.TL?.GL?.GLCode}-->{sl.TL?.TLCode}-->{sl?.SLCode}-->{_allAccDocumentItems.FirstOrDefault()?.Code}";
                Path = $"{sl.TL?.GL?.Title}-->{sl.TL?.Title}-->{sl?.Title}-->{_allAccDocumentItems.FirstOrDefault()?.Title}";
            }
            IsBusyGridView = false;
        }
        private void RaiseCanExecuteChanged()
        {
            ((RelayCommand<string>)GLDetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)TLDetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)SLDetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)DL1DetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)DL2DetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)CurrencyDetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand<string>)TrackingDetailedCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UndoCommand).RaiseCanExecuteChanged();
        }
        public void SetReviewAccount()
        {
            AccDocumentItemListViewModel.AccDocumentItems = new ObservableCollection<EditableAccDocumentItem>();
        }
        #endregion
    }
    class Data
    {
        public List<AccDocumentItemDTO> AccDocumentItemDTOs { get; set; }
        public string ColumnName { get; set; }
        public string CurrentMethodName { get; set; }
        public string Path { get; set; }
        public GroupStatusEnum GroupStatus { get; set; }
        public AccDocumentItemDTO SelectedAccDocumentItem { get; set; }
    }
}
