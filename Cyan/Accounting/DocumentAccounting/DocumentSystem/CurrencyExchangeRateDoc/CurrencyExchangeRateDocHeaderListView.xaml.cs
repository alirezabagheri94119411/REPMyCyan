using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CurrencyExchangeRateDoc
{
    /// <summary>
    /// Interaction logic for CurrencyExchangeRateDocheaderListView.xaml
    /// </summary>
    public partial class CurrencyExchangeRateDocHeaderListView : UserControl
    {
        //    private AccDocumentHeaderListViewModel _viewModel;
        //    private bool? _dialogResult;
        //    private SainaDbContext _uow;
        //    private CollectionViewSource accDocumentHeaderViewSource;
        //    private IEditableCollectionViewAddNewItem _virtualQueryableCollectionView;
        //    private bool isModified;

        //    public CurrencyExchangeRateDocHeaderListView()
        //    {
        //        InitializeComponent();
        //        Loaded += (s, e) =>
        //        {
        //            //_uow = SmObjectFactory.Container.GetInstance<SainaDbContext>();
        //            ////typeDocumentIdComboBox.ItemsSource = await _uow.TypeDocuments.ToListAsync();
        //            //_virtualQueryableCollectionView = new QueryableCollectionView(_uow.AccDocumentHeaders.ToList()); //new VirtualQueryableCollectionView(_uow.AccDocumentHeaders.OrderByDescending(x => x.AccDocumentHeaderId)) { LoadSize = 10 };
        //            //mainGrid.DataContext = _virtualQueryableCollectionView;

        //            _viewModel = DataContext as AccDocumentHeaderListViewModel;
        //            _viewModel.Loaded();
        //            BeginEdit();


        //            //AccDocumentHeader.Property
        //            headerDataForm.AddedNewItem += HeaderDataForm_AddedNewItem;
        //            var headers = headerDataForm.ItemsSource.Cast<AccDocumentHeader>();
        //            foreach (var header in headers)
        //            {
        //                header.PropertyChanged += AccDocumentHeader_PropertyChanged;
        //                var accDocumentItems = header.AccDocumentItems as ObservableCollection<AccDocumentItem>;
        //                accDocumentItems.CollectionChanged += AccDocumentItems_CollectionChanged;
        //                foreach (var item in header.AccDocumentItems)
        //                {
        //                    item.PropertyChanged += Item_PropertyChanged;
        //                }
        //            }
        //            headerDataForm.CurrentItemChanged += (se, ea) =>
        //            {
        //                CommitAndBeginEdit();
        //                RaiseCanExecuteChanged();
        //            };
        //        };
        //    }

        //    private void AccDocumentItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //    {
        //        isModified = true;
        //        RaiseCanExecuteChanged();
        //    }

        //    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //    {
        //        isModified = true;
        //        RaiseCanExecuteChanged();
        //    }

        //    private void BeginEdit()
        //    {
        //        if (AccDocumentHeader?.Status != StatusEnum.Permanent)
        //        {
        //            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
        //            beginEditCommand.Execute(null, this.headerDataForm);
        //        }
        //    }
        //    private void HeaderDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        //    {
        //        ((AccDocumentHeader)e.NewItem).PropertyChanged += AccDocumentHeader_PropertyChanged;
        //    }

        //    private void AccDocumentHeader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //    {
        //        if (e.PropertyName != "Status")
        //        {
        //            isModified = true;
        //        }
        //        else if (e.PropertyName == "Status")
        //        {
        //            AccDocumentItemsRadGridView.IsReadOnly = AccDocumentHeader.Status == StatusEnum.Permanent;
        //        }
        //        RaiseCanExecuteChanged();
        //    }
        //    private void OnError(string error)
        //    {
        //        DialogParameters parameters = new DialogParameters();
        //        parameters.OkButtonContent = "بستن";
        //        parameters.Header = "!اخطار";
        //        parameters.Content = error;
        //        RadWindow.Alert(parameters);
        //    }

        //    private void OnFailed(Exception ex)
        //    {
        //        DialogParameters parameters = new DialogParameters();
        //        parameters.OkButtonContent = "بستن";
        //        parameters.Header = "اخطار";
        //        parameters.Content = ex.Message;
        //        RadWindow.Alert(parameters);
        //    }

        //    private void OnDeleted()
        //    {
        //        DialogParameters parameters = new DialogParameters();
        //        parameters.OkButtonContent = "بستن";
        //        parameters.Header = "اطلاعات";
        //        parameters.Content = ".حذف با موفقیت انجام شد";
        //        RadWindow.Alert(parameters);
        //    }

        //    private async void SaveButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        var r = _uow.SaveChanges();
        //    }

        //    private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        //    {
        //        //var r = _uow.SaveChanges();
        //        _viewModel.Save();
        //    }

        //    private void RadDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        //    {
        //        _viewModel.AddAccDocumentHeader((AccDocumentHeader)e.NewItem);
        //    }

        //    private void RadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        //    {
        //        var r = _viewModel.DeleteDocumentHeader((AccDocumentHeader)e.DeletedItem);
        //    }

        //    private void accDocumentHeaderDataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        //    {
        //        headerDataForm.AddNewItem();
        //    }
        //    private void headerDataForm_DeletingItem(object sender, CancelEventArgs e)
        //    {

        //        DialogParameters parameters = new DialogParameters();
        //        parameters.OkButtonContent = "بله، مطمئنم";
        //        parameters.CancelButtonContent = "خیر";
        //        parameters.Header = "اخطار";
        //        parameters.Content = "آیا برای حذف  مطمئن هستید؟";
        //        parameters.Closed = OnClosed;
        //        RadWindow.Confirm(parameters);
        //        var r = _dialogResult == true;
        //    }
        //    private void OnClosed(object sender, WindowClosedEventArgs e)
        //    {
        //        _dialogResult = e.DialogResult;
        //    }
        //    private void DeleteRadPathButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (_dialogResult == false) return;
        //        headerDataForm.DeleteItem();
        //        OnDeleted();
        //        _viewModel.Save();
        //    }
        //    AccDocumentHeader AccDocumentHeader => headerDataForm.CurrentItem as AccDocumentHeader;
        //    private void accDocumentHeaderDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //    {
        //        detailRadTabItem.IsSelected = true;
        //    }

        //    private void draftButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (headerDataForm.ValidateItem())
        //        {
        //            AccDocumentHeader.Status = StatusEnum.Draft;
        //            CommitAndBeginEdit();
        //            RaiseCanExecuteChanged();
        //        }
        //    }
        //    private void temporaryButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        AccDocumentHeader.Status = StatusEnum.Temporary;
        //        CommitAndBeginEdit();
        //        RaiseCanExecuteChanged();
        //    }

        //    private void endButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        AccDocumentHeader.Status = StatusEnum.End;
        //        CommitAndBeginEdit();
        //        RaiseCanExecuteChanged();
        //    }

        //    private void backFromEndButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        AccDocumentHeader.Status = StatusEnum.Temporary;
        //        CommitAndBeginEdit();
        //        RaiseCanExecuteChanged();
        //    }

        //    private void permanentButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        AccDocumentHeader.Status = StatusEnum.Permanent;
        //        CommitAndBeginEdit();
        //        RaiseCanExecuteChanged();
        //    }
        //    private void RaiseCanExecuteChanged()
        //    {
        //        if (AccDocumentHeader != null)
        //        {
        //            draftButton.IsEnabled = AccDocumentHeader.Status != StatusEnum.Permanent && isModified;
        //            temporaryButton.IsEnabled =
        //                headerDataForm.ValidateItem()
        //                && AccDocumentHeader?.Difference == 0
        //                && AccDocumentHeader.Status != StatusEnum.End
        //                && AccDocumentHeader.Status != StatusEnum.Permanent
        //                && AccDocumentHeader.Status != StatusEnum.Temporary
        //                && (isModified || AccDocumentHeader.Status == StatusEnum.Temporary);
        //            endButton.IsEnabled =
        //                 headerDataForm.ValidateItem()
        //                && AccDocumentHeader?.Difference == 0
        //                && AccDocumentHeader.Status == StatusEnum.Temporary;
        //            backFromEndButton.IsEnabled = AccDocumentHeader.Status == StatusEnum.End;
        //            permanentButton.IsEnabled = AccDocumentHeader.Status == StatusEnum.End
        //                && AccDocumentHeader?.HasErrors == false
        //                && AccDocumentHeader?.Difference == 0;
        //            isModified = false;
        //        }
        //        AccDocumentItemsRadGridView.IsReadOnly = AccDocumentHeader?.Status == StatusEnum.Permanent;
        //    }

        //    private void CommitAndBeginEdit()
        //    {
        //        var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
        //        commitEditCommand.Execute(null, this.headerDataForm);
        //        BeginEdit();
        //    }

        //    private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        //    {
        //        if (detailRadTabItem.IsSelected)
        //        {
        //            BeginEdit();
        //            RaiseCanExecuteChanged();
        //        }

        //    }


        //}
        private CurrencyExchangeRateDocHeaderListViewModel _viewModel;
        private bool? _dialogResult;
        public CurrencyExchangeRateDocHeaderListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as CurrencyExchangeRateDocHeaderListViewModel;
                _viewModel.Deleting += OnDeleting;
                _viewModel.Deleted += OnDeleted;


            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Deleting -= OnDeleting;
                _viewModel.Deleted -= OnDeleted;


            };

        }
        private void OnDeleted()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اطلاعات";
            parameters.Content = ".حذف با موفقیت انجام شد";
            RadWindow.Alert(parameters);
        }

        private bool OnDeleting()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا برای حذف  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            return _dialogResult == true;
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
    }
}
