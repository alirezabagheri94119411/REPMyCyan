using Microsoft.Win32;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Persistence;

namespace Saina.WPF.Commerce.WarehouseDocuments.IncomingDocuments
{
    public partial class IncomingDocumentListView : UserControl
    {
        public IncomingDocumentListView()
        {
            _appContextService = SmObjectFactory.Container.GetInstance<IAppContextService>();
            InitializeComponent();
            StcDocumentItemsRadGridView.KeyboardCommandProvider = new GridViewCustomKeyboardCommandProvider(StcDocumentItemsRadGridView);
            Loaded += StcDocumentHeaderListView_Loaded;
            Unloaded += (s, e) =>
            {
                _viewModel.UnLoaded();
            };

        }

        private void StcDocumentHeaderListView_Loaded(object sender, RoutedEventArgs e)
        {

            Loaded -= StcDocumentHeaderListView_Loaded;
            _viewModel = DataContext as IncomingDocumentListViewModel;
            _viewModel.Loaded();
            BeginEdit();
            if (StcDocumentHeaderDataForm?.ItemsSource != null)
            {
                var headers = StcDocumentHeaderDataForm.ItemsSource.Cast<StcDocumentHeader>();
                foreach (var header in headers)
                {
                    header.PropertyChanged += StcDocumentHeader_PropertyChanged;
                    //   var accDocumentItems = header.StcDocumentItems as ObservableCollection<StcDocumentItem>;
                    //accDocumentItems.CollectionChanged += StcDocumentItems_CollectionChanged;
                    //foreach (var item in header.StcDocumentItems)
                    //{
                    //    item.PropertyChanged += Item_PropertyChanged;
                    //}
                }
            }
            NavStateFalse();
            var addNewHeaderCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewHeaderCommand.Execute(null, StcDocumentHeaderDataForm);
            detailRadTabItem.IsSelected = true;
            StcDocumentItemsRadGridView.Focus();


        }

        private void xx(object sender, ExecutedRoutedEventArgs args)
        {
            MessageBox.Show("OnRoutedCommand2Eexuted");
        }
        #region Fields
        private IncomingDocumentListViewModel _viewModel;
        private bool? _dialogResult;
        private bool isModified;
        private IAppContextService _appContextService;
        private Stream stream;
        StcDocumentHeader StcDocumentHeader => StcDocumentHeaderDataForm.CurrentItem as StcDocumentHeader;
        private GridViewRow ClickedRow
        {
            get
            {
                return GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }
        #endregion
        #region Report
        void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (.{0})|.{0}|All files (.)|.", extension, "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                var col0Visibility = StcDocumentItemsRadGridView.Columns[0].IsVisible;
                StcDocumentItemsRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    StcDocumentItemsRadGridView.Export(stream,
                     opt);
                }
                StcDocumentItemsRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            StiReport report = new StiReport();
            var headerIds = StcDocumentHeaderRadGridView.SelectedItems.Cast<StcDocumentHeader>().Select(x => x.StcDocumentHeaderId);
            //using (var uow = new SainaDbContext())
            //{
            //    var data = uow.StcDocumentItems.Include(x => x.StcDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Where(x => headerIds.Contains(x.StcDocumentHeaderId))
            //         .ToList()
            //        .Select(accDocumentItem => new
            //        {
            //            HeaderDescription = accDocumentItem.StcDocumentHeader.HeaderDescription,
            //            ManualDocumentNumber = accDocumentItem.StcDocumentHeader.ManualDocumentNumber,
            //            Seconder = accDocumentItem.StcDocumentHeader.Seconder,
            //            Status = accDocumentItem.StcDocumentHeader.Status,
            //            SumDebit = accDocumentItem.StcDocumentHeader.SumDebit,
            //            SystemFixNumber = accDocumentItem.StcDocumentHeader.SystemFixNumber,
            //            SystemName = accDocumentItem.StcDocumentHeader.SystemName,
            //            TypeDocumentTitle = accDocumentItem.StcDocumentHeader.TypeDocument.TypeDocumentTitle,
            //            DocumentNumber = accDocumentItem.StcDocumentHeader.DocumentNumber,
            //            DocumentDate = accDocumentItem.StcDocumentHeader.DocumentDate,
            //            DailyNumber = accDocumentItem.StcDocumentHeader.DailyNumber,
            //            ItemDebit = accDocumentItem.Debit,
            //            Credit = accDocumentItem.Credit,
            //            CurrencyTitle = accDocumentItem.Currency.CurrencyTitle,
            //            CurrencyAmount = accDocumentItem.CurrencyAmount,
            //            Description1 = accDocumentItem.Description1,
            //            Description2 = accDocumentItem.Description2,
            //            DLCode1 = accDocumentItem.DL1.DLCode,
            //            DLCode2 = accDocumentItem.DL2.DLCode,
            //            DLTitle1 = accDocumentItem.DL1.Title,
            //            DLTitle2 = accDocumentItem.DL2.Title,
            //            SLTitle = accDocumentItem.SL.Title,
            //            TrackingDate = accDocumentItem.TrackingDate,
            //            TrackingNumber = accDocumentItem.TrackingNumber,
            //            DescriptionItem1 = accDocumentItem.Description1,
            //            DescriptionItem2 = accDocumentItem.Description2,
            //        })
            //       ;
            //    report.RegBusinessObject("AccHeaderItemReport", data);
            //    report.Load($"{Environment.CurrentDirectory}\\report.mrt");
            //    report.Show();
            //}
        }
        private void DesignButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }
        private void DesignReport()
        {
            StiReport report = new StiReport();
            //using (var uow = new SainaDbContext())
            //{
            //    var headerIds = StcDocumentHeaderRadGridView.SelectedItems.Cast<StcDocumentHeader>().Select(x => x.StcDocumentHeaderId);
            //    var data = uow.StcDocumentItems.Include(x => x.StcDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Where(x => headerIds.Contains(x.StcDocumentHeaderId))
            //         .ToList()
            //        .Select(accDocumentItem => new
            //        {
            //            HeaderDescription = accDocumentItem.StcDocumentHeader.HeaderDescription,
            //            ManualDocumentNumber = accDocumentItem.StcDocumentHeader.ManualDocumentNumber,
            //            Seconder = accDocumentItem.StcDocumentHeader.Seconder,
            //            Status = accDocumentItem.StcDocumentHeader.Status,
            //            SumDebit = accDocumentItem.StcDocumentHeader.SumDebit,
            //            SystemFixNumber = accDocumentItem.StcDocumentHeader.SystemFixNumber,
            //            SystemName = accDocumentItem.StcDocumentHeader.SystemName,
            //            TypeDocumentTitle = accDocumentItem.StcDocumentHeader.TypeDocument.TypeDocumentTitle,
            //            DocumentNumber = accDocumentItem.StcDocumentHeader.DocumentNumber,
            //            DocumentDate = accDocumentItem.StcDocumentHeader.DocumentDate,
            //            DailyNumber = accDocumentItem.StcDocumentHeader.DailyNumber,
            //            ItemDebit = accDocumentItem.Debit,
            //            Credit = accDocumentItem.Credit,
            //            CurrencyTitle = accDocumentItem.Currency.CurrencyTitle,
            //            CurrencyAmount = accDocumentItem.CurrencyAmount,
            //            Description1 = accDocumentItem.Description1,
            //            Description2 = accDocumentItem.Description2,
            //            DLCode1 = accDocumentItem.DL1.DLCode,
            //            DLCode2 = accDocumentItem.DL2.DLCode,
            //            DLTitle1 = accDocumentItem.DL1.Title,
            //            DLTitle2 = accDocumentItem.DL2.Title,
            //            SLTitle = accDocumentItem.SL.Title,
            //            TrackingDate = accDocumentItem.TrackingDate,
            //            TrackingNumber = accDocumentItem.TrackingNumber,
            //            DescriptionItem1 = accDocumentItem.Description1,
            //            DescriptionItem2 = accDocumentItem.Description2,
            //        })
            //       ;

            //    report.RegBusinessObject("AccHeaderItemReport", data);
            //    report.Design();
            //}
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            StiReport report = new StiReport();
            //using (var uow = new SainaDbContext())
            //{
            //    var headerIds = StcDocumentHeaderRadGridView.SelectedItems.Cast<StcDocumentHeader>().Select(x => x.StcDocumentHeaderId);
            //    var data = uow.StcDocumentItems.Include(x => x.StcDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Where(x => headerIds.Contains(x.StcDocumentHeaderId))
            //                         .ToList()
            //                        .Select(accDocumentItem => new
            //                        {
            //                            HeaderDescription = accDocumentItem.StcDocumentHeader.HeaderDescription,
            //                            ManualDocumentNumber = accDocumentItem.StcDocumentHeader.ManualDocumentNumber,
            //                            Seconder = accDocumentItem.StcDocumentHeader.Seconder,
            //                            Status = accDocumentItem.StcDocumentHeader.Status,
            //                            SumDebit = accDocumentItem.StcDocumentHeader.SumDebit,
            //                            SystemFixNumber = accDocumentItem.StcDocumentHeader.SystemFixNumber,
            //                            SystemName = accDocumentItem.StcDocumentHeader.SystemName,
            //                            TypeDocumentTitle = accDocumentItem.StcDocumentHeader.TypeDocument.TypeDocumentTitle,
            //                            DocumentNumber = accDocumentItem.StcDocumentHeader.DocumentNumber,
            //                            DocumentDate = accDocumentItem.StcDocumentHeader.DocumentDate,
            //                            DailyNumber = accDocumentItem.StcDocumentHeader.DailyNumber,
            //                            ItemDebit = accDocumentItem.Debit,
            //                            Credit = accDocumentItem.Credit,
            //                            CurrencyTitle = accDocumentItem.Currency.CurrencyTitle,
            //                            CurrencyAmount = accDocumentItem.CurrencyAmount,
            //                            Description1 = accDocumentItem.Description1,
            //                            Description2 = accDocumentItem.Description2,
            //                            DLCode1 = accDocumentItem.DL1.DLCode,
            //                            DLCode2 = accDocumentItem.DL2.DLCode,
            //                            DLTitle1 = accDocumentItem.DL1.Title,
            //                            DLTitle2 = accDocumentItem.DL2.Title,
            //                            SLTitle = accDocumentItem.SL.Title,
            //                            TrackingDate = accDocumentItem.TrackingDate,
            //                            TrackingNumber = accDocumentItem.TrackingNumber,
            //                            DescriptionItem1 = accDocumentItem.Description1,
            //                            DescriptionItem2 = accDocumentItem.Description2,
            //                        })
            //                       ;
            //    report.RegBusinessObject("AccHeaderItemReport", data);
            //    report.Load($"{Environment.CurrentDirectory}\\report.mrt");
            //    report.Print();
            //}
        }
        private void StcDocumentItemsRadGridViewContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string tag = (e.OriginalSource as RadMenuItem).Tag as string;
            switch (tag)
            {
                case "show":
                    ShowReport();
                    break;
                case "design":
                    DesignReport();
                    break;
                case "print":
                    PrintReport();
                    break;
                case "add":
                    var index = StcDocumentItemsRadGridView.Items.IndexOf(StcDocumentItemsRadGridView.SelectedItem);
                    if (index > -1)
                        ((ObservableCollection<StcDocumentItem>)StcDocumentItemsRadGridView.ItemsSource).Insert(index, new StcDocumentItem());
                    break;
                case "edit":
                    StcDocumentItemsRadGridView.BeginEdit();
                    break;
                case "delete":
                    StcDocumentItemsRadGridView.Items.Remove(StcDocumentItemsRadGridView.SelectedItem);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region StcDocumentHeaderDataForm
        #region StcDocumentHeaderDataFormCommands
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavStateFalse();

            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, StcDocumentHeaderDataForm);
        }
        private bool OnCanceling()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            return _dialogResult == true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            OnCanceling();
            if (_dialogResult == true)
            {
                if (StcDocumentHeaderDataForm != null)
                {
                    NavStateTrue();

                    StcDocumentHeaderDataForm.CancelEdit();
                    StcDocumentHeaderDataForm.BeginEdit();
                }
            }

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            NavStateFalse();

            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, StcDocumentHeaderDataForm);
        }
        private void DraftButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
                //  StcDocumentHeader.Status = StatusEnum.Draft;
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".پیش نویس انجام شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void TemporaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
                // StcDocumentHeader.Status = StatusEnum.Temporary;
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".ثبت موقت انجام شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
             //   StcDocumentHeader.Status = StatusEnum.End;
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".خاتمه انجام شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void BackFromEndButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
              //  StcDocumentHeader.Status = StatusEnum.Temporary;
                CommitAndBeginEdit();
                isModified = true;
                RaiseCanExecuteChanged();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ". برگشت از خاتمه انجام شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void PermanentButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
                //  StcDocumentHeader.Status = StatusEnum.Permanent;
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".صند قطعی شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }
        }
        public void NavStateFalse()
        {
            addButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = true;

        }
        public void NavStateTrue()
        {
            addButton.IsEnabled = true;
            deleteButton.IsEnabled = true;
            cancelButton.IsEnabled = false;
        }
        private bool ValidateDocumetHeader()
        {
            var result = true;
            //var items = StcDocumentHeader.StcDocumentItems.ToList();
            //var errorMessage = "";
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (items[i].SLId == 0)
            //        StcDocumentItemsRadGridView.Items.Remove(items[i]);
            //    else
            //    {
            //        if (items[i].SL?.DLType1 != 0 && items[i].DL1Id == null)
            //            errorMessage += $"ردیف {i + 1} تفصیل اول نمی تواند خالی باشد {Environment.NewLine}";
            //        if (items[i].SL?.DLType2 != 0 && items[i].DL2Id == null)
            //            errorMessage += $"ردیف {i + 1} تفصیل دوم نمی تواند خالی باشد {Environment.NewLine}";

            //        //if (items[i].Description1 == "" || items[i].Description1 == null)
            //        //{
            //        //    errorMessage += $"ردیف {i + 1} شرح آرتیکل نمی تواند خالی باشد {Environment.NewLine}";

            //        //}
            //        if (items[i].Debit == 0 && items[i].Credit == 0)
            //        {
            //            errorMessage += $"ردیف {i + 1} بدهکار یا بستانکار را مقدار دهی کنید {Environment.NewLine}";

            //        }
            //    }

            //}
            //StcDocumentHeaderDataForm.ValidationSummary.Errors.Clear();
            //if (StcDocumentHeader.DocumentNumber == 0)
            //{
            //    StcDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "شماره سند",
            //        ErrorContent = "شماره سند نباید خالی باشد"
            //    });
            //    result = false;
            //}
            //if (StcDocumentHeader.SystemFixNumber == 0)
            //{
            //    StcDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "شماره ثابتسند",
            //        ErrorContent = "شماره ثابت سند نباید خالی باشد"
            //    });
            //    result = false;
            //}
            //if (StcDocumentHeader.ManualDocumentNumber == 0)
            //{
            //    StcDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "شماره دستی سند",
            //        ErrorContent = "شماره دستی سند نباید خالی باشد"
            //    });
            //    result = false;
            //}
            //if (StcDocumentHeader.TypeDocumentId == 0)
            //{
            //    StcDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "نوع سند",
            //        ErrorContent = "نوع سند نباید خالی باشد"
            //    });
            //    result = false;
            //}
            //if (StcDocumentHeader.DocumentDate == null)
            //{
            //    StcDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "تاریخ سند",
            //        ErrorContent = "تاریخ سند نباید خالی باشد"
            //    });
            //    result = false;
            //}
            //items = StcDocumentHeader.StcDocumentItems.ToList();

            //if (items.Count == 0)
            //{
            //    result = false;
            //    errorMessage += $" هیچ سندی برای ثبت وجود ندارد {Environment.NewLine}";
            //}
            //if (errorMessage.Length > 0)
            //{
            //    result = false;

            //    DialogParameters parameters = new DialogParameters();
            //    parameters.OkButtonContent = "بستن";
            //    parameters.Header = "!اخطار";
            //    parameters.Content = errorMessage;
            //    RadWindow.Alert(parameters);
            //}

            return result;
        }
        private void RaiseCanExecuteChanged()
        {
            //if (StcDocumentHeader != null)
            //{
            //    draftButton.IsEnabled = StcDocumentHeader.Status == StatusEnum.Temporary ||
            //    (StcDocumentHeader.Status != StatusEnum.Permanent && isModified);
            //    temporaryButton.IsEnabled =
            //        StcDocumentHeaderDataForm.ValidateItem()
            //        && StcDocumentHeader?.Difference == 0
            //        && StcDocumentHeader.Status != StatusEnum.End
            //        && StcDocumentHeader.Status != StatusEnum.Permanent
            //        && (StcDocumentHeader.Status != StatusEnum.Temporary
            //        || (isModified && StcDocumentHeader.Status == StatusEnum.Temporary));
            //    endButton.IsEnabled =
            //         StcDocumentHeaderDataForm.ValidateItem()
            //        && StcDocumentHeader?.Difference == 0
            //        && StcDocumentHeader.Status == StatusEnum.Temporary;
            //    backFromEndButton.IsEnabled = StcDocumentHeader.Status == StatusEnum.End;
            //    permanentButton.IsEnabled = StcDocumentHeader.Status == StatusEnum.End
            //        && StcDocumentHeader?.HasErrors == false
            //        && StcDocumentHeader?.Difference == 0;
            //    isModified = false;
            //}
            //StcDocumentItemsRadGridView.IsReadOnly = StcDocumentHeader?.Status == StatusEnum.Permanent;
        }
        private void CommitAndBeginEdit()
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (_viewModel != null)
            {
                _viewModel.Save();
                commitEditCommand.Execute(null, StcDocumentHeaderDataForm);
                BeginEdit();
            }

        }
        #endregion
        #region StcDocumentHeaderDataFormEventHandlers
        private void StcDocumentHeaderDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

            //DialogParameters parameters = new DialogParameters();
            //parameters.OkButtonContent = "بله";
            //parameters.CancelButtonContent = "خیر";
            //parameters.Header = "اخطار";
            //parameters.Content = "آیا می خواهید تغییرات ذخیره شود؟";
            //parameters.Closed = OnClosed;
            //RadWindow.Confirm(parameters);
            //if (StcDocumentHeaderDataForm. _dialogResult == false)
            //{
            //    CommitAndBeginEdit();
            //}
            //else
            //{
            //    var cancelEditCommand = RadDataFormCommands.CancelEdit as RoutedUICommand;
            //    cancelEditCommand.Execute(null, StcDocumentHeaderDataForm);
            //}

            RaiseCanExecuteChanged();
        }
        private void StcDocumentHeaderDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            //var newItem = ((StcDocumentHeader)e.NewItem);
            //((StcDocumentHeader)e.NewItem).PropertyChanged += StcDocumentHeader_PropertyChanged;
            //((ObservableCollection<StcDocumentItem>)newItem.StcDocumentItems).CollectionChanged += StcDocumentItems_CollectionChanged;
            //newItem.TypeDocumentId = 2;//عمومی
            //using (var uow = new SainaDbContext())
            //{
            //    var getDocumentNumbering = uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefault(x => x.AccountDocumentId == 1);
            //    var EndNumber = getDocumentNumbering.EndNumber;
            //    var lastStcDocumentHeaderCode = uow.Database.SqlQuery<long?>($"SELECT MAX(DocumentNumber)+1 FROM dbo.StcDocumentHeaders WHERE[dbo].[ShamsiToMiladi] (DocumentDate, 'Saal') ={_appContextService.CurrentFinancialYear}").FirstOrDefault();

            //    lastStcDocumentHeaderCode = lastStcDocumentHeaderCode != null ? lastStcDocumentHeaderCode : 0;

            //    if (lastStcDocumentHeaderCode > EndNumber)
            //    {
            //        DialogParameters parameters = new DialogParameters();
            //        parameters.OkButtonContent = "بستن";
            //        parameters.Header = "اخطار";
            //        parameters.Content = ".شماره گذاری اسناد به پایان رسیده، لطفا شماره گذاری اسناد را بررسی نمایید";
            //        RadWindow.Alert(parameters);
            //        return;
            //    }
            //    else
            //    {
            //        long startNumber = getDocumentNumbering.StartNumber;
            //        lastStcDocumentHeaderCode = lastStcDocumentHeaderCode == 0 ? startNumber : lastStcDocumentHeaderCode++;
            //        int? accountDocumentId = getDocumentNumbering.AccountDocumentId;
            //        int? style = getDocumentNumbering.StyleId;
            //        int? countingWayId = getDocumentNumbering.CountingWayId;
            //        long lastDailyNumberCode = uow.StcDocumentHeaders.Where(x =>
            //        x.DocumentDate.Day == DateTime.Now.Day
            //         && x.DocumentDate.Month == DateTime.Now.Month
            //         && x.DocumentDate.Year == DateTime.Now.Year).Select(x => x.DailyNumber).DefaultIfEmpty(1).Max();
            //        string stringLastStcDocumentHeaderCode = lastStcDocumentHeaderCode.ToString();
            //        string stringlastDailyNumberCode = lastDailyNumberCode.ToString();
            //        string lastStcDocumentHeadersCode = stringLastStcDocumentHeaderCode;
            //        string lastDayStcDocumentHeadersCode = stringLastStcDocumentHeaderCode;
            //        if (lastStcDocumentHeaderCode <= EndNumber)
            //        {
            //            newItem.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");
            //            if (style == 1 && countingWayId == 1)
            //            {
            //                newItem.IsReadOnly = false;
            //                newItem.DocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.ManualDocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.SystemFixNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //            }
            //            else if (style == 1 && countingWayId == 2)
            //            {
            //                newItem.IsReadOnly = true;
            //                newItem.DocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.ManualDocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.SystemFixNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //            }
            //            else if (style == 2 && countingWayId == 1)
            //            {
            //                newItem.IsReadOnly = false;
            //                newItem.DocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.ManualDocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.SystemFixNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //            }
            //            else if (style == 2 && countingWayId == 2)
            //            {
            //                newItem.IsReadOnly = true;
            //                newItem.DocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.ManualDocumentNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //                newItem.SystemFixNumber = int.Parse($"{lastStcDocumentHeadersCode}");
            //            }
            //        }
            //    }
            //}
            //_viewModel.AddStcDocumentHeader((StcDocumentHeader)e.NewItem);
        }
        private void StcDocumentHeaderDataForm_DeletingItem(object sender, CancelEventArgs e)
        {

            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا برای حذف  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            e.Cancel = _dialogResult == false;
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void StcDocumentHeaderDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            var r = _viewModel.DeleteDocumentHeader((StcDocumentHeader)e.DeletedItem);
            if (r > 0)
            {
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".حذف با موفقیت انجام شد",
                    ShowDuration = 5000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void StcDocumentHeaderDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                _viewModel.Save();
        }
        private void StcDocumentHeaderDataForm_ValidatingItem(object sender, CancelEventArgs e)
        {


        }

        #endregion
        #endregion
        #region StcDocumentHeaderRadGridView
        private void StcDocumentHeader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Status")
            {
                isModified = true;
            }
            else if (e.PropertyName == "Status")
            {
                //    StcDocumentItemsRadGridView.IsReadOnly = StcDocumentHeader.Status == StatusEnum.Permanent;
            }
            StcDocumentHeaderDataForm.BeginEdit();
            RaiseCanExecuteChanged();
        }
        private void StcDocumentHeaderRadGridView_FieldFilterEditorCreated(object sender, EditorCreatedEventArgs e)
        {
            //get the StringFilterEditor in your RadGridView
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }
        private void StcDocumentHeaderRadGridView_FilterOperatorsLoading(object sender, FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void StcDocumentHeaderRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }
        private void BeginEdit()
        {
            //if (StcDocumentHeader?.Status != StatusEnum.Permanent)
            //{

            //    var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            //    beginEditCommand.Execute(null, StcDocumentHeaderDataForm);
            //}
        }
        #endregion
        #region StcDocumentItems
        private void StcDocumentItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            isModified = true;
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                ((StcDocumentItem)e.NewItems[0]).PropertyChanged += Item_PropertyChanged;
            }
            RaiseCanExecuteChanged();
        }
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            isModified = true;
            if (e.PropertyName == "SLId")
            {
                //_viewModel.DLs1DropDownOpenedCommand.Execute(sender);
                //_viewModel.DLs2DropDownOpenedCommand.Execute(sender);
            }
            RaiseCanExecuteChanged();
        }
        private void StcDocumentItemsRadGridView_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "description1" || e.Cell.Column.UniqueName == "description2")
            {
                RadComboBox combo = e.Cell.ChildrenOfType<RadComboBox>().FirstOrDefault();
                if (combo != null)
                {
                    List<SLStandardDescription> comboItems = combo.ItemsSource as List<SLStandardDescription>;

                    string textEntered = e.Cell.ChildrenOfType<RadComboBox>().First().Text;

                    bool result = comboItems.Contains(comboItems.Where(x => x.SLStandardDescriptionTitle == textEntered).FirstOrDefault());
                    if (!result)
                    {
                        comboItems.Add(new SLStandardDescription { SLStandardDescriptionTitle = textEntered });
                        combo.SelectedItem = new SLStandardDescription { SLStandardDescriptionTitle = textEntered };
                    }
                    if (_viewModel.StcDocumentItem != null)
                    {
                        //if (e.Cell.Column.UniqueName == "description1")
                        //    _viewModel.StcDocumentItem.Description1 = textEntered;
                        //else if (e.Cell.Column.UniqueName == "description2")
                        //    _viewModel.StcDocumentItem.Description2 = textEntered;
                    }
                }
            }
        }

        private void StcDocumentItemsRadGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F3)
            //{
            //    var tempDebit = ((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Debit;
            //    ((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Debit = ((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Credit;
            //    ((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Credit = tempDebit;
            //    // (((StcDocumentItem)parentGrid.CurrentItem).Debit, ((StcDocumentItem)parentGrid.CurrentItem).Debit) = (((StcDocumentItem)parentGrid.CurrentItem).Debit, ((StcDocumentItem)parentGrid.CurrentItem).Debit);
            //    if (((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Debit != 0)
            //        ((StcDocumentItem)StcDocumentItemsRadGridView.SelectedItem).Credit = 0;

            //}
        }
        private void StcDocumentItemsRadGridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {

        }

        private void StcDocumentItemsRadGridView_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            var gridView = e.OwnerGridViewItemsControl;
            StcDocumentItemsRadGridView.ScrollIntoViewAsync(StcDocumentItemsRadGridView.Items[StcDocumentItemsRadGridView.Items.Count - 1], //the row
                                 StcDocumentItemsRadGridView.Columns[StcDocumentItemsRadGridView.Columns.Count - 1], //the column
                                 new Action<FrameworkElement>((f) =>
                                 {
                                     (f as GridViewRow).IsSelected = true; // the callback method
                                 }));
        }
        #endregion
        #region Other
        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (detailRadTabItem.IsSelected && StcDocumentHeaderRadGridView.SelectedItem != null)
            {
                BeginEdit();
                RaiseCanExecuteChanged();
            }
        }
        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            //AttachmentListWindow attachmentListWindow = new AttachmentListWindow();
            //attachmentListWindow.DataContext = DataContext;

            //attachmentListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //attachmentListWindow.Width = 1024;
            //attachmentListWindow.Height = 768;
            //attachmentListWindow.CanClose = true;
            //attachmentListWindow.ShowDialog();
        }
        private void DocumentHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            //DocumentHistoryListWindow documentHistoryListWindow = new DocumentHistoryListWindow();
            //documentHistoryListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //documentHistoryListWindow.Width = 1024;
            //documentHistoryListWindow.Height = 768;
            //documentHistoryListWindow.CanClose = true;
            //documentHistoryListWindow.ShowDialog();
        }
        private void DL1RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.StcDocumentItem = ((RadComboBox)sender).DataContext as StcDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);

            //itemsViewOriginal.Filter = ((x) =>
            //{

            ////    return x != null && _viewModel.StcDocumentItem.SL != null ? _viewModel.StcDocumentItem.SL.DLType1.HasFlag(((DL)x).DLType) : false;
            //});
            //itemsViewOriginal.Refresh();
        }
        private void DL2RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.StcDocumentItem = ((RadComboBox)sender).DataContext as StcDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);
            //itemsViewOriginal.Filter = ((x) =>
            //{
            //  //  return x != null && _viewModel.StcDocumentItem.SL != null ? _viewModel.StcDocumentItem.SL.DLType2.HasFlag(((DL)x).DLType) : false;
            //});
            itemsViewOriginal.Refresh();
        }
        private void Description_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.StcDocumentItem = ((RadComboBox)sender).DataContext as StcDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);
            //itemsViewOriginal.Filter = ((x) =>
            //{
            //    return x != null && ((SLStandardDescription)x).SLId == _viewModel.StcDocumentItem.SLId;
            //});
            //itemsViewOriginal.Refresh();
        }
        private void RadComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //var comboBox = sender as RadComboBox;
            //if (!comboBox.IsDropDownOpen && (e.Key == Key.Up || e.Key == Key.Down))
            //{
            //    e.Handled = true;
            //    return;
            //}
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void load_Click(object sender, RoutedEventArgs e)
        {
            //PersistenceManager manager = new PersistenceManager();
            //stream = manager.Save(StcDocumentItemsRadGridView);
            //this.EnsureLoadState();
        }
        private void unload_Click(object sender, RoutedEventArgs e)
        {
            stream.Position = 0L;
            PersistenceManager manager = new PersistenceManager();
            manager.Load(StcDocumentItemsRadGridView, stream);
        }
        #endregion

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!this.StcDocumentItemsRadGridView.Items.IsEditingItem)
                return;

            this.HandleKeyDown(e);
            //this.HandleKeyUp(e);
            //this.HandleKeyLeft(e);
            //this.HandleKeyRight(e);
        }

        private void HandleKeyDown(KeyEventArgs e)
        {
            var editBox = StcDocumentItemsRadGridView.CurrentCell.ChildrenOfType<TextBox>().FirstOrDefault();

            if (e.Key == Key.Up)
            {
                if (editBox != null && editBox.Name != "PART_EditableTextBox")
                {
                    RadGridViewCommands.MoveUp.Execute(null);
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                    RadGridViewCommands.BeginEdit.Execute(null);

                    e.Handled = true;
                }
            }
            if (e.Key == Key.Right)
            {
                if (editBox.CaretIndex == editBox.Text.Length)
                {
                    RadGridViewCommands.MoveRight.Execute(null);
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                    RadGridViewCommands.BeginEdit.Execute(null);

                    e.Handled = true;
                }
            }

            if (e.Key == Key.Down)
            {
                if (editBox != null && editBox.Name != "PART_EditableTextBox")
                {
                    RadGridViewCommands.MoveDown.Execute(null);
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                    RadGridViewCommands.BeginEdit.Execute(null);
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Left)
            {
                if (editBox?.CaretIndex == 0)
                {
                    RadGridViewCommands.MoveLeft.Execute(null);
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                    RadGridViewCommands.BeginEdit.Execute(null);

                    e.Handled = true;
                }
            }

        }

        private void StcDocumentItemsRadGridView_Loaded(object sender, RoutedEventArgs e)
        {
            StcDocumentItemsRadGridView.BeginInsert();
            StcDocumentItemsRadGridView.CurrentColumn = StcDocumentItemsRadGridView.Columns[0];

        }
    }
}
    public static class Command
    {
        public static RoutedCommand Add = new RoutedCommand();
        public static RoutedCommand Delete = new RoutedCommand();
        public static RoutedCommand Draft = new RoutedCommand();
        public static RoutedCommand Cancel = new RoutedCommand();
        public static RoutedCommand Temporary = new RoutedCommand();
        public static RoutedCommand End = new RoutedCommand();
        public static RoutedCommand BackFromEnd = new RoutedCommand();
        public static RoutedCommand Permanent = new RoutedCommand();
        public static RoutedCommand Show = new RoutedCommand();
        public static RoutedCommand Design = new RoutedCommand();
        public static RoutedCommand Print = new RoutedCommand();

    }
    /// <summary>
    /// Interaction logic for IncomingDocumentListview.xaml
    /// </summary>
  
