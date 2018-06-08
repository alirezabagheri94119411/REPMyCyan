using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CloseProfitLossDoc
{
    class EditCloseProfitLossDocViewModel:BindableBase
    {
        #region Constructors
        public EditCloseProfitLossDocViewModel(IAccDocumentHeadersService accDocumentHeadersService, IAppContextService appContextService, Saina.Data.Context.SainaDbContext uow)
        {
            _accDocumentHeadersService = accDocumentHeadersService;
            _appContextService = appContextService;
            _uow = uow;
            CancelCommand = new RelayCommand(OnCancel);
            //  SaveCommand = new RelayCommand(OnSave, CanSave);
            ChangeStatusCommand = new RelayCommand<StatusEnum>(OnStatusChanged);
            AccDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<AccDocumentItemListViewModel>();
            TypeDocumentsDropDownOpenedCommand = new RelayCommand(OnTypeDocumentsDropDownOpened);
        }


        #endregion
        #region Fields
        private IAccDocumentHeadersService _accDocumentHeadersService;
        private List<TypeDocument> _allTypeDocuments;
        private IAppContextService _appContextService;
        private SainaDbContext _uow;

        //   private AccDocumentHeader _editingAccDocumentHeader = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public ICommand ChangeStatusCommand { get; private set; }

        public ICommand TypeDocumentsDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private AccDocumentItemListViewModel _accDocumentItemListViewModel;

        public AccDocumentItemListViewModel AccDocumentItemListViewModel
        {
            get { return _accDocumentItemListViewModel; }
            set { SetProperty(ref _accDocumentItemListViewModel, value); _accDocumentItemListViewModel.AccDocumentItemsCollectionChanged += _accDocumentItemListViewModel_AccDocumentItemsCollectionChanged; _accDocumentItemListViewModel.EditableAccDocumentItemChanged += _accDocumentItemListViewModel_EditableAccDocumentItemChanged; }
        }


        private EditableAccDocumentHeader _accDocumentHeader;
        public EditableAccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { SetProperty(ref _accDocumentHeader, value); AccDocumentItemListViewModel.AccDocumentHeader = value; }
        }

        private ObservableCollection<TypeDocument> _typeDocuments;

        public ObservableCollection<TypeDocument> TypeDocuments
        {
            get { return _typeDocuments; }
            set { SetProperty(ref _typeDocuments, value); }
        }
        private TypeDocument _typeDocument;
        public TypeDocument TypeDocument
        {
            get { return _typeDocument; }
            set { SetProperty(ref _typeDocument, value); }
        }
        private bool _checkType;
        public bool CheckType
        {
            get { return _checkType; }
            set { SetProperty(ref _checkType, value); }
        }


        public long EndNumber { get; set; }
        public event Action<Exception> Failed;
        public event Action<string> Error;

        public event Action Done;
        #endregion
        #region Methode

        private async void OnTypeDocumentsDropDownOpened()
        {
            _allTypeDocuments = await _accDocumentHeadersService.GetTypeDocumentsAsync();
            TypeDocuments = new ObservableCollection<TypeDocument>(_allTypeDocuments);
        }
        public void SetAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            if (EditMode == false)
                AccDocumentItemListViewModel.AccDocumentItems = new ObservableCollection<EditableAccDocumentItem>();

            AccDocumentHeader = Mapper.Map<AccDocumentHeader, EditableAccDocumentHeader>(accDocumentHeader);
            AccDocumentHeader.PropertyChanging += AccDocumentHeader_PropertyChanging;
            AccDocumentHeader.PropertyChanged += AccDocumentHeader_PropertyChanged;
            AccDocumentHeader.ValidationDelegate += AccDocumentHeader_ValidationDelegate;

            AccDocumentHeader.ErrorsChanged += RaiseCanExecuteChanged;
        }


        private void AccDocumentHeader_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            var v = (EditableAccDocumentHeader)sender;

            //v.Handled = true;
        }

        private void AccDocumentHeader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!AccDocumentHeader.HasErrors)
            {
                if (e.PropertyName != "Status")
                {
                    AccDocumentHeader.Status |= StatusEnum.Draft;
                    if (AccDocumentHeader.Difference == 0 && AccDocumentItemListViewModel.AccDocumentItems.Count > 1)
                        AccDocumentHeader.Status |= StatusEnum.Temporary;
                }
            }

        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            //SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        public async void LoadAccDocumentHeaders()
        {

            var getDocumentNumbering = await _accDocumentHeadersService.GetDocumentNumberingAsync();


            if (!EditMode)
            {
                //      var x = StatusEnum.Temporary ^ StatusEnum.Draft;
                //  _accDocumentHeader.Status = 0;

                EndNumber = getDocumentNumbering.EndNumber;
                var lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(_appContextService.CurrentFinancialYear);
                if (lastAccDocumentHeaderCode > EndNumber)
                {
                    Error("شماره گذاری اسناد به پایان رسیده،لطفا شماره گذاری اسناد را بررسی نمایید");
                }
                else
                {
                    var startNumber = getDocumentNumbering.StartNumber;

                    if (lastAccDocumentHeaderCode == 0)
                    {
                        lastAccDocumentHeaderCode = startNumber;
                    }
                    else
                    {
                        lastAccDocumentHeaderCode++;
                    }

                    var accountDocumentId = getDocumentNumbering.AccountDocumentId;
                    var style = getDocumentNumbering.StyleId;
                    var countingWayId = getDocumentNumbering.CountingWayId;

                    var lastDailyNumberCode = await _accDocumentHeadersService.GetLastDailyNumberCode();
                    var stringLastAccDocumentHeaderCode = lastAccDocumentHeaderCode.ToString();
                    var stringlastDailyNumberCode = lastDailyNumberCode.ToString();
                    var lastAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                    var lastDayAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                    if (lastAccDocumentHeaderCode <= EndNumber)
                    {
                        AccDocumentHeader.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");


                        if (style == 1 && countingWayId == 1)
                        {
                            AccDocumentHeader.IsReadOnly = false;
                            AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                        else if (style == 1 && countingWayId == 2)
                        {
                            AccDocumentHeader.IsReadOnly = true;
                            AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                        else if (style == 2 && countingWayId == 1)
                        {
                            AccDocumentHeader.IsReadOnly = false;

                            AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");

                        }
                        else if (style == 2 && countingWayId == 2)
                        {
                            AccDocumentHeader.IsReadOnly = true;

                            AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");

                        }
                    }
                }


            }
        }
        private async void OnStatusChanged(StatusEnum statusEnum)
        {
            var oldStatus = AccDocumentHeader.Status;
            //if (AccDocumentHeader.HasErrors == false)
            //{
            //    SaveChanges(AccDocumentHeader);
            //    if (statusEnum.HasFlag(StatusEnum.Draft))//(statusEnum & StatusEnum.Draft)== StatusEnum.Draft
            //    {
            //        var s = ((int)AccDocumentHeader.Status - (int)StatusEnum.Draft);
            //        if (s <= 0)
            //            AccDocumentHeader.Status = StatusEnum.None;
            //        else
            //            AccDocumentHeader.Status &= (StatusEnum)s;
            //    }
            //    else if (statusEnum.HasFlag(StatusEnum.Temporary))
            //    {
            //        if (AccDocumentHeader.Difference == 0)
            //            AccDocumentHeader.Status = StatusEnum.End;
            //        //  Failed?.Invoke(new Exception("سند تراز نیست"));
            //    }
            //    else if (statusEnum.HasFlag(StatusEnum.End))
            //    {
            //        if (AccDocumentHeader.Difference == 0)
            //            AccDocumentHeader.Status = StatusEnum.BackFromEnd | StatusEnum.Permanent;

            //    }
            //    else if (statusEnum.HasFlag(StatusEnum.BackFromEnd))
            //    {
            //        AccDocumentHeader.Status = StatusEnum.End;
            //    }
            //    else if (statusEnum.HasFlag(StatusEnum.Permanent))
            //    {
            //        if (AccDocumentHeader.Difference == 0)
            //            AccDocumentHeader.Status = StatusEnum.None;
            //        AccDocumentItemListViewModel.CanAdd = false;
            //    }
            //    ///مبلغ سند باید به صورت دستی زده شود
            //    ///این عدد جمع بدهکار یا بستکار می شود.
            //    //SaveChanges(AccDocumentHeader);

            //    //if (AccDocumentHeader.HasErrors) AccDocumentHeader.Status = oldStatus;
            //    //var editingAccDocumentHeader = new AccDocumentHeader() { HeaderDescription="dsfsdfsd"}; //Mapper.Map<EditableAccDocumentHeader, AccDocumentHeader>(AccDocumentHeader);
            //    //Mapper.Map(AccDocumentItemListViewModel.AccDocumentItems.ToList(), editingAccDocumentHeader.AccDocumentItems);
            //    //string ComputerName = System.Environment.MachineName;
            //    //try
            //    //{
            //    //    if (EditMode)
            //    //    {
            //    //        editingAccDocumentHeader.Seconder = _appContextService.CurrentUser.FriendlyName;
            //    //        editingAccDocumentHeader.SystemName = ComputerName;

            //    //        if (editingAccDocumentHeader.Status == StatusEnum.Permanent)
            //    //        {
            //    //            editingAccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
            //    //        }
            //    //        await _accDocumentHeadersService.UpdateAccDocumentHeaderAsync(editingAccDocumentHeader);
            //    //    }
            //    //    else
            //    //    {
            //    //        editingAccDocumentHeader.Seconder = _appContextService.CurrentUser.FriendlyName;
            //    //        editingAccDocumentHeader.SystemName = ComputerName;
            //    //        //  editingAccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
            //    //        await _accDocumentHeadersService.AddAccDocumentHeaderAsync(editingAccDocumentHeader);
            //    //    }
            //    //    Done?.Invoke();
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Failed(ex);
            //    //}
            //    //finally
            //    //{
            //    //    AccDocumentHeader = null;
            //    //}
            //}
            //private async void OnSave()
            //{

            //    var editingAccDocumentHeader = Mapper.Map<EditableAccDocumentHeader, AccDocumentHeader>(AccDocumentHeader);
            //    Mapper.Map( AccDocumentItemListViewModel.AccDocumentItems.ToList(), editingAccDocumentHeader.AccDocumentItems);
            //    string ComputerName = System.Environment.MachineName;
            //    try
            //    {
            //        if (EditMode)
            //        {
            //            editingAccDocumentHeader.Seconder = _appContextService.CurrentUser.FriendlyName;
            //            editingAccDocumentHeader.SystemName = ComputerName;

            //            if (editingAccDocumentHeader.Status== StatusEnum.Permanent)
            //            {
            //              editingAccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
            //            }
            //            await _accDocumentHeadersService.UpdateAccDocumentHeaderAsync(editingAccDocumentHeader);
            //        }
            //        else
            //        {
            //            editingAccDocumentHeader.Seconder = _appContextService.CurrentUser.FriendlyName;
            //            editingAccDocumentHeader.SystemName = ComputerName;
            //          //  editingAccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
            //            await _accDocumentHeadersService.AddAccDocumentHeaderAsync(editingAccDocumentHeader);
            //        }
            //        Done?.Invoke();
            //    }
            //    catch (Exception ex)
            //    {
            //        Failed(ex);
            //    }
            //    finally
            //    {
            //        AccDocumentHeader = null;
            //    }
            //}
        }







        public void SaveChanges(EditableAccDocumentHeader selectedHeader)
        {
            var existingParent = _uow.AccDocumentHeaders
.Where(p => p.AccDocumentHeaderId == selectedHeader.AccDocumentHeaderId)
.Include(p => p.AccDocumentItems)
.SingleOrDefault();

            if (existingParent != null)
            {
                // Update parent
                _uow.Entry(existingParent).CurrentValues.SetValues(selectedHeader);

                // Delete AccDocumentItems
                //foreach (var existingChild in existingParent.AccDocumentItems.ToList())
                //{
                //    if (!selectedHeader.AccDocumentItems.Any(c => c.AccDocumentItemId == existingChild.AccDocumentItemId))
                //        _uow.AccDocumentItems.Remove(existingChild);
                //}


                // Update and Insert AccDocumentItems
                //foreach (var childModel in selectedHeader.AccDocumentItems)
                //{
                //    var existingChild = existingParent.AccDocumentItems
                //        .SingleOrDefault(c => c.AccDocumentItemId == childModel.AccDocumentItemId);

                //    if (existingChild != null)
                //        Update child
                //        _uow.Entry(existingChild).CurrentValues.SetValues(childModel);
                //    else
                //    {
                //        Insert child
                //        var newChild = new AccDocumentItem
                //        {
                //            SLId = childModel.SLId,
                //            DL1Id = childModel.DL1Id,
                //            DL2Id = childModel.DL2Id
                //            ,
                //            Debit = childModel.Debit,
                //            Credit = childModel.Credit,
                //            Description1 = childModel.Description1,
                //            Description2 = childModel.Description2
                //        };// AutoMapper.Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(childModel);// new AccDocumentItem
                //        {
                //            //Data = childModel.Data,
                //            ////...
                //        };
                //        existingParent.AccDocumentItems.Add(newChild);
                //    }
                //}

                _uow.SaveChanges();
            }

        }







        private bool CanSave()
        {
            return !AccDocumentHeader.HasErrors;
        }
        private async Task<List<string>> AccDocumentHeader_ValidationDelegate(object sender, string propertyName)
        {

            var accDocumentHeader = (EditableAccDocumentHeader)sender;

            List<string> errors = new List<string>();

            var documentNumber = AccDocumentHeader.DocumentNumber;
            var manualDocumentNumber = AccDocumentHeader.ManualDocumentNumber;
            var systemFixNumber = AccDocumentHeader.SystemFixNumber;

            switch (propertyName)
            {

                case nameof(EditableAccDocumentHeader.DocumentNumber):

                    if (documentNumber == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (documentNumber >= EndNumber)
                    {
                        errors.Add("شماره سند نباید از شماره پایان شماره گذاری بزرگتر باشد");
                    }
                    break;
                case nameof(EditableAccDocumentHeader.SystemFixNumber):

                    if (documentNumber == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (systemFixNumber >= EndNumber)
                    {
                        errors.Add("شماره ثابت سیستم نباید از شماره پایان شماره گذاری بزرگتر باشد");
                    }
                    break;
                case nameof(EditableAccDocumentHeader.ManualDocumentNumber):

                    if (documentNumber == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (manualDocumentNumber >= EndNumber)
                    {
                        errors.Add("شماره دستی نباید از شماره پایان شماره گذاری بزرگتر باشد");
                    }
                    break;
                //case nameof(EditableAccDocumentHeader.Status):
                //    switch (accDocumentHeader.Status)
                //    {
                //        case StatusEnum.Draft:

                //            break;
                //        case StatusEnum.Temporary:
                //            if (accDocumentHeader.Difference != 0)
                //                errors.Add("StatusEnum.Temporary");
                //            break;
                //        case StatusEnum.End:
                //            if (accDocumentHeader.Difference != 0)
                //                errors.Add("سند تراز نیست");

                //            break;
                //        case StatusEnum.BackFromEnd:
                //            break;
                //        case StatusEnum.Permanent:
                //            //if (accDocumentHeader.Difference != 0)
                //            //    errors.Add("سند تراز نیست");
                //            break;
                //        default:
                //            break;
                //    }
                //break;
                default:
                    return null;
            }
            return errors;
        }
        private void _accDocumentItemListViewModel_EditableAccDocumentItemChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var d = sender as Saina.WPF.Accounting.DocumentAccounting.ItemDocument.EditableAccDocumentItem;

            AccDocumentHeader.Status |= StatusEnum.Draft;
            if (!d.HasErrors)
            {
                if (AccDocumentHeader.Difference == 0 && AccDocumentItemListViewModel?.AccDocumentItems?.Count > 1)
                    AccDocumentHeader.Status |= StatusEnum.Temporary;
            }
        }

        private void _accDocumentItemListViewModel_AccDocumentItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var d = sender as IEnumerable<Saina.WPF.Accounting.DocumentAccounting.ItemDocument.EditableAccDocumentItem>;
            AccDocumentHeader.Status |= StatusEnum.Draft;
            if (AccDocumentHeader.Difference == 0 && d.Count() > 1)
                AccDocumentHeader.Status |= StatusEnum.Temporary;
            else
            {
                AccDocumentHeader.Status = (StatusEnum)((int)AccDocumentHeader.Status) - ((int)StatusEnum.Temporary);
            }

            if (AccDocumentHeader.Status < 0)
                AccDocumentHeader.Status = StatusEnum.Draft;
            if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                foreach (var item in d)
                {
                    item.AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId;
                }

        }
        #endregion
    }
}
