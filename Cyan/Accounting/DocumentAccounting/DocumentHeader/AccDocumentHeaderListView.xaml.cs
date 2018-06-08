using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Stimulsoft.Report;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Data.Entity;
using Telerik.Windows.Controls.GridView;
using System.Collections;
using System.Collections.Generic;
using Saina.Data.Services.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation;
using System.Windows.Media;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System.Windows.Data;
using Telerik.Windows.Persistence;
using System.IO;
using Telerik.Windows.Controls.Filtering.Editors;
using Microsoft.Win32;
using System.Reflection;
using System.Globalization;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System.Threading;
using Telerik.Windows.Persistence.Storage;
using Saina.Common.Config;
using Saina.WPF.Behaviors;
using System.Text.RegularExpressions;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    public partial class AccDocumentHeaderListView : UserControl
    {
        private PersistenceManager persistenceManager;
        private string lastSerializadProvider;
        private IsolatedStorageProvider isolatedStorageProvider;
        private AccDocumentHeader clipboardAccDocumentHeader;
        private ObservableCollection<AccDocumentItem> clipboardAccDocumentItems;
        //  public ObservableCollection<EditableAccItem> Clipboard { get; set; }
        public AccDocumentHeaderListView()
        {
            persistenceManager = new PersistenceManager();
            // isolatedStorageProvider = new IsolatedStorageProvider();

            _appContextService = SmObjectFactory.Container.GetInstance<IAppContextService>();
            _currencyExchangesService = SmObjectFactory.Container.GetInstance<ICurrencyExchangesService>();
            InitializeComponent();
            //var addNewItemCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            //addNewItemCommand.Execute(null, AccDocumentItemsRadGridView);
            //AccDocumentItemsRadGridView.KeyboardCommandProvider = new GridViewCustomKeyboardCommandProvider(AccDocumentItemsRadGridView);
            //AccDocumentHeaderDataForm.PreviewKeyDown += AccDocumentHeaderDataForm_PreviewKeyDown;
            Loaded += AccDocumentHeaderListView_Loaded;

            Loaded += (s, e) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _configSetGet = SmObjectFactory.Container.GetInstance<IConfigSetGet>();

                //  AccDocumentHeaderListViewModel = new AccDocumentHeaderListViewModel { UserName = _configSetGet.GetConfigData("LastLoginName"), Password = _configSetGet.GetConfigData("Password"), RememberMe = _configSetGet.GetConfigData("RememberMe") == "true" };
                AccDocumentItemsRadGridView.Columns[0].IsVisible = (_configSetGet.GetConfigData("AccDocumentItemId")).ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[1].IsVisible = _configSetGet.GetConfigData("AccDocumentItemSLId").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[2].IsVisible = _configSetGet.GetConfigData("AccDocumentItemDL1").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[3].IsVisible = _configSetGet.GetConfigData("AccDocumentItemDL2").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[4].IsVisible = _configSetGet.GetConfigData("AccDocumentItemDescription1").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[5].IsVisible = _configSetGet.GetConfigData("AccDocumentItemDescription2").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[6].IsVisible = _configSetGet.GetConfigData("AccDocumentItemDebit").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[7].IsVisible = _configSetGet.GetConfigData("AccDocumentItemCredit").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[8].IsVisible = _configSetGet.GetConfigData("AccDocumentItemCurrencyAmount").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[9].IsVisible = _configSetGet.GetConfigData("AccDocumentItemExchangeRate").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[10].IsVisible = _configSetGet.GetConfigData("AccDocumentItemTrackingDate").ToLower() == "true";
                AccDocumentItemsRadGridView.Columns[11].IsVisible = _configSetGet.GetConfigData("AccDocumentItemTrackingNumber").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[0].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersDocumentNumber").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[1].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersSystemFixNumber").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[2].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersDailyNumber").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[3].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersManualDocumentNumber").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[4].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersDocumentDate").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[5].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersSystemName").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[6].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersExporter").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[7].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersSeconder").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[8].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersHeaderDescription").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[9].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersAmountDocument").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[10].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersTypeDocument").ToLower() == "true";
                AccDocumentHeaderRadGridView.Columns[11].IsVisible = _configSetGet.GetConfigData("AccDocumentHeadersStatus").ToLower() == "true";

            };

            Unloaded += (s, e) =>
             {
                 _configSetGet.SetConfigData("AccDocumentItemId", (AccDocumentItemsRadGridView.Columns[0].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemSLId", (AccDocumentItemsRadGridView.Columns[1].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemDL1", (AccDocumentItemsRadGridView.Columns[2].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemDL2", (AccDocumentItemsRadGridView.Columns[3].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemDescription1", (AccDocumentItemsRadGridView.Columns[4].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemDescription2", (AccDocumentItemsRadGridView.Columns[5].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemDebit", (AccDocumentItemsRadGridView.Columns[6].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemCredit", (AccDocumentItemsRadGridView.Columns[7].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemCurrencyAmount", (AccDocumentItemsRadGridView.Columns[8].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemExchangeRate", (AccDocumentItemsRadGridView.Columns[9].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemTrackingDate", (AccDocumentItemsRadGridView.Columns[10].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentItemTrackingNumber", (AccDocumentItemsRadGridView.Columns[11].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersDocumentNumber", (AccDocumentHeaderRadGridView.Columns[0].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersSystemFixNumber", (AccDocumentHeaderRadGridView.Columns[1].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersDailyNumber", (AccDocumentHeaderRadGridView.Columns[2].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersManualDocumentNumber", (AccDocumentHeaderRadGridView.Columns[3].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersDocumentDate", (AccDocumentHeaderRadGridView.Columns[4].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersSystemName", (AccDocumentHeaderRadGridView.Columns[5].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersExporter", (AccDocumentHeaderRadGridView.Columns[6].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersSeconder", (AccDocumentHeaderRadGridView.Columns[7].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersHeaderDescription", (AccDocumentHeaderRadGridView.Columns[8].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersAmountDocument", (AccDocumentHeaderRadGridView.Columns[9].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersTypeDocument", (AccDocumentHeaderRadGridView.Columns[10].IsVisible).ToString());
                 _configSetGet.SetConfigData("AccDocumentHeadersStatus", (AccDocumentHeaderRadGridView.Columns[11].IsVisible).ToString());

                 // PersistenceManager managerAccDocumentItemTrackingDate"  = new PersistenceManager();
                 //  stream = manager.Save(ColAccDocumentItemTrackingNumberumnsSetting);
                 //LocalDataSourceSerializer provider = new LocalDataSourceSerializer();
                 //this.lastSerializadProvider = provider.Serialize(this.pivotGrid.DataProvider);
                 //this.EnsureLoadState();
                 _viewModel.UnLoaded();
                 //listBox=this.ColumnsSetting.sa

                 //isolatedStorageProvider.SaveToStorage();
                 //Stream stream = persistenceManager.Save(ColumnsSttings);

                 //MemoryStream memoryStream = new MemoryStream();

                 //stream.CopyTo(memoryStream);

                 //File.WriteAllBytes(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\SaveSetting\\AccSettings.dat", memoryStream.ToArray());
             };
        }

        private void AccDocumentHeaderDataForm_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if(e.Key== Key.Enter)
            //{
            //    var date = AccDocumentHeaderDataForm.ChildrenOfType<DataFormDataField>().FirstOrDefault(x => x.Name == "documentDate");
            //    date.Focus();
            //    e.Handled = true;
            //}
        }
        private void AccDocumentHeaderListView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= AccDocumentHeaderListView_Loaded;
            _viewModel = DataContext as AccDocumentHeaderListViewModel;
            _AccDocumentHeaderDataFormKeyboardCommandProvider = new AccDocumentHeaderDataFormKeyboardCommandProvider(_viewModel);
            AccDocumentHeaderDataForm.CommandProvider = _AccDocumentHeaderDataFormKeyboardCommandProvider;

            _AccDocumentHeaderDataFormKeyboardCommandProvider.Reject += () => { _viewModel.Reject(); };
            //if (this.stream != null && this.stream.Length > 0)
            //{
            //    stream.Position = 0L;
            //    PersistenceManager manager = new PersistenceManager();
            //    manager.Load(ColumnsSetting, stream);
            //}
            _viewModel.Loaded();
            //var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            //addNewCommand.Execute(null, AccDocumentHeaderDataForm);
            //BeginEdit();
            if (AccDocumentHeaderDataForm?.ItemsSource != null)
            {
                var headers = AccDocumentHeaderDataForm.ItemsSource.Cast<AccDocumentHeader>();
                foreach (var header in headers)
                {
                    header.PropertyChanged += AccDocumentHeader_PropertyChanged;
                    var accDocumentItems = header.AccDocumentItems as ObservableCollection<AccDocumentItem>;
                    accDocumentItems.CollectionChanged += AccDocumentItems_CollectionChanged;
                    foreach (var item in header.AccDocumentItems)
                    {
                        item.PropertyChanged += Item_PropertyChanged;
                    }
                }
            }
            NavStateFalse();
            //if (_viewModel.HeaderId == 0)
            //    AccDocumentHeaderDataForm.AddNewItem();
            //AccDocumentItemsRadGridView.SelectedItem = null;
            //detailRadTabItem.IsSelected = true;
            if (_viewModel.HeaderId == 0)
            {
                listRadTabItem.IsSelected = true;
            }
            else
            {
                if (_viewModel.HasFlow == true)
                {
                    _viewModel.LoadedFlow();
                    detailRadTabItem.IsSelected = true;
                    AccDocumentItemsRadGridView.IsReadOnly = true;
                }
                else
                {
                    _viewModel.Loaded();
                    detailRadTabItem.IsSelected = true;
                    AccDocumentItemsRadGridView.IsReadOnly = true;
                }


            }
            if (_viewModel.TypeEnum != 0)
            {
                AccDocumentItemsRadGridView.IsReadOnly = true;
                AccDocumentHeaderDataForm.CancelEdit();
                draftButton.Visibility = Visibility.Hidden;
                // draftButton.Visibility= "Hidden";
                //  draftButton.IsEnabled = false;
            }
            AccDocumentItemsRadGridView.Focus();

        }
        private void xx(object sender, ExecutedRoutedEventArgs args)
        {
            MessageBox.Show("OnRoutedCommand2Eexuted");
        }

        #region Fields

        private AccDocumentHeaderListViewModel _viewModel;
        private bool? _dialogResult;
        private bool? _dialogResult1;
        private bool isModified;
        private IAppContextService _appContextService;
        private ICurrencyExchangesService _currencyExchangesService;
        private Stream stream;
        AccDocumentHeader AccDocumentHeader => AccDocumentHeaderDataForm.CurrentItem as AccDocumentHeader;
        AccDocumentItem AccDocumentItem => AccDocumentItemsRadGridView.SelectedCells as AccDocumentItem;
        private GridViewRow ClickedRow
        {
            get
            {
                return GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }

        public AccDocumentHeader AccDocumentHeader1 { get; private set; }

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
                var col0Visibility = AccDocumentItemsRadGridView.Columns[0].IsVisible;
                AccDocumentItemsRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    AccDocumentItemsRadGridView.Export(stream,
                     opt);
                }
                AccDocumentItemsRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            StiReport report = new StiReport();
            var headerIds = AccDocumentHeaderRadGridView.SelectedItems.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
            using (var uow = new SainaDbContext())
            {
                var data = uow.AccDocumentItems.Include(x => x.AccDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL.TL).Where(x => headerIds.Contains(x.AccDocumentHeaderId))
                     .ToList()
                    .Select(accDocumentItem => new
                    {
                        HeaderDescription = accDocumentItem.AccDocumentHeader.HeaderDescription,
                        ManualDocumentNumber = accDocumentItem.AccDocumentHeader.ManualDocumentNumber,
                        Seconder = accDocumentItem.AccDocumentHeader.Seconder,
                        Status = accDocumentItem.AccDocumentHeader.Status,
                        SumDebit = accDocumentItem.AccDocumentHeader.SumDebit,
                        SystemFixNumber = accDocumentItem.AccDocumentHeader.SystemFixNumber,
                        SystemName = accDocumentItem.AccDocumentHeader.SystemName,
                        TypeDocumentTitle = accDocumentItem.AccDocumentHeader.TypeDocument?.TypeDocumentTitle,
                        DocumentNumber = accDocumentItem.AccDocumentHeader.DocumentNumber,
                        DocumentDate = accDocumentItem.AccDocumentHeader.DocumentDate.ToShortDateString(),
                        DailyNumber = accDocumentItem.AccDocumentHeader.DailyNumber,
                        ItemDebit = accDocumentItem.Debit,
                        Credit = accDocumentItem.Credit,
                        CurrencyTitle = accDocumentItem.Currency?.CurrencyTitle,
                        CurrencyAmount = accDocumentItem.CurrencyAmount,
                        Description1 = accDocumentItem.Description1,
                        Description2 = accDocumentItem.Description2,
                        DLCode1 = accDocumentItem.DL1?.DLCode,
                        DLCode2 = accDocumentItem.DL2?.DLCode,
                        DLTitle1 = accDocumentItem.DL1?.Title,
                        DLTitle2 = accDocumentItem.DL2?.Title,
                        SLTitle = accDocumentItem.SL.Title,
                        SLCode = accDocumentItem.SL.SLCode,
                        TLCode = accDocumentItem.SL.TL.TLCode,
                        TLTitle = accDocumentItem.SL.TL.Title,
                        TLTitle2 = accDocumentItem.SL.TL.Title2,
                        TrackingDate = accDocumentItem.TrackingDate,
                        TrackingNumber = accDocumentItem.TrackingNumber,
                        DescriptionItem1 = accDocumentItem.Description1,
                        DescriptionItem2 = accDocumentItem.Description2,
                    })
                   ;
                report.RegBusinessObject("AccHeaderItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accHeaderItemReport.mrt");
                report.Show();
            }
        }
        private void DesignButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }
        private void DesignReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var AddressCompany = _viewModel.CompanyInformationModel.Address;
                var CityCompany = _viewModel.CompanyInformationModel.City;
                var EconomicStoreCodeCompany = _viewModel.CompanyInformationModel.EconomicStoreCode;
                var FaxCompany = _viewModel.CompanyInformationModel.Fax;
                var LogoCompany = _viewModel.CompanyInformationModel.Logo;
                var ManagerNameCompany = _viewModel.CompanyInformationModel.ManagerName;
                var MobileCompany = _viewModel.CompanyInformationModel.Mobile;
                var PhoneCompany = _viewModel.CompanyInformationModel.Phone;
                var Phone2Company = _viewModel.CompanyInformationModel.Phone2;
                var PostalCodeCompany = _viewModel.CompanyInformationModel.PostalCode;
                var ProvinceCompany = _viewModel.CompanyInformationModel.Province;
                var StoreNameCompany = _viewModel.CompanyInformationModel.StoreName;
                var StoreCodeCompany = _viewModel.CompanyInformationModel.StoreCode;
                var TownCompany = _viewModel.CompanyInformationModel.Town;
                var WebSiteCompany = _viewModel.CompanyInformationModel.WebSite;
                var headerIds = AccDocumentHeaderRadGridView.SelectedItems.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
                var data = uow.AccDocumentItems.Include(x => x.AccDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL.TL).Where(x => headerIds.Contains(x.AccDocumentHeaderId))
                     .ToList()
                    .Select(accDocumentItem => new
                    {
                        HeaderDescription = accDocumentItem.AccDocumentHeader.HeaderDescription,
                        ManualDocumentNumber = accDocumentItem.AccDocumentHeader.ManualDocumentNumber,
                        Seconder = accDocumentItem.AccDocumentHeader.Seconder,
                        Status = accDocumentItem.AccDocumentHeader.Status,
                        SumDebit = accDocumentItem.AccDocumentHeader.SumDebit,
                        SystemFixNumber = accDocumentItem.AccDocumentHeader.SystemFixNumber,
                        SystemName = accDocumentItem.AccDocumentHeader.SystemName,
                        TypeDocumentTitle = accDocumentItem.AccDocumentHeader.TypeDocument?.TypeDocumentTitle,
                        DocumentNumber = accDocumentItem.AccDocumentHeader.DocumentNumber,
                        DocumentDate = accDocumentItem.AccDocumentHeader.DocumentDate.ToShortDateString(),
                        DailyNumber = accDocumentItem.AccDocumentHeader.DailyNumber,
                        ItemDebit = accDocumentItem.Debit,
                        Credit = accDocumentItem.Credit,
                        CurrencyTitle = accDocumentItem.Currency?.CurrencyTitle,
                        CurrencyAmount = accDocumentItem.CurrencyAmount,
                        Description1 = accDocumentItem.Description1,
                        Description2 = accDocumentItem.Description2,
                        DLCode1 = accDocumentItem.DL1?.DLCode,
                        DLCode2 = accDocumentItem.DL2?.DLCode,
                        DLTitle1 = accDocumentItem.DL1?.Title,
                        DLTitle2 = accDocumentItem.DL2?.Title,
                        SLTitle = accDocumentItem.SL.Title,
                        SLCode = accDocumentItem.SL.SLCode,
                        TLCode = accDocumentItem.SL.TL.TLCode,
                        TLTitle = accDocumentItem.SL.TL.Title,
                        TLTitle2 = accDocumentItem.SL.TL.Title2,
                        TrackingDate = accDocumentItem.TrackingDate,
                        TrackingNumber = accDocumentItem.TrackingNumber,
                        DescriptionItem1 = accDocumentItem.Description1,
                        DescriptionItem2 = accDocumentItem.Description2,
                        AddressCompany = AddressCompany,
                        CityCompany = CityCompany,
                        EconomicStoreCode = EconomicStoreCodeCompany,
                        FaxCompany = FaxCompany,
                        LogoCompany = LogoCompany,
                        ManagerNameCompany = ManagerNameCompany,
                        MobileCompany = MobileCompany,
                        PhoneCompany = PhoneCompany,
                        Phone2Company = Phone2Company,
                        PostalCodeCompany = PostalCodeCompany,
                        ProvinceCompany = ProvinceCompany,
                        StoreNameCompany = StoreNameCompany,
                        StoreCodeCompany = StoreCodeCompany,
                        TownCompany = TownCompany,
                        WebSiteCompany = WebSiteCompany,
                    })
                   ;

                var path = $"{Environment.CurrentDirectory}\\Report\\accHeaderItemReport.mrt";
                report.RegBusinessObject("AccHeaderItemReport", data);
                if (!File.Exists(path))
                    File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
                report.Load(path);
                report.Design();
            }
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var headerIds = AccDocumentHeaderRadGridView.SelectedItems.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
                var data = uow.AccDocumentItems.Include(x => x.AccDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL.TL).Where(x => headerIds.Contains(x.AccDocumentHeaderId))
                                     .ToList()
                                    .Select(accDocumentItem => new
                                    {
                                        HeaderDescription = accDocumentItem.AccDocumentHeader.HeaderDescription,
                                        ManualDocumentNumber = accDocumentItem.AccDocumentHeader.ManualDocumentNumber,
                                        Seconder = accDocumentItem.AccDocumentHeader.Seconder,
                                        Status = accDocumentItem.AccDocumentHeader.Status,
                                        SumDebit = accDocumentItem.AccDocumentHeader.SumDebit,
                                        SystemFixNumber = accDocumentItem.AccDocumentHeader.SystemFixNumber,
                                        SystemName = accDocumentItem.AccDocumentHeader.SystemName,
                                        TypeDocumentTitle = accDocumentItem.AccDocumentHeader.TypeDocument?.TypeDocumentTitle,
                                        DocumentNumber = accDocumentItem.AccDocumentHeader.DocumentNumber,
                                        DocumentDate = accDocumentItem.AccDocumentHeader.DocumentDate.ToShortDateString(),
                                        DailyNumber = accDocumentItem.AccDocumentHeader.DailyNumber,
                                        ItemDebit = accDocumentItem.Debit,
                                        Credit = accDocumentItem.Credit,
                                        CurrencyTitle = accDocumentItem.Currency?.CurrencyTitle,
                                        CurrencyAmount = accDocumentItem.CurrencyAmount,
                                        Description1 = accDocumentItem.Description1,
                                        Description2 = accDocumentItem.Description2,
                                        DLCode1 = accDocumentItem.DL1?.DLCode,
                                        DLCode2 = accDocumentItem.DL2?.DLCode,
                                        DLTitle1 = accDocumentItem.DL1?.Title,
                                        DLTitle2 = accDocumentItem.DL2?.Title,
                                        SLTitle = accDocumentItem.SL.Title,
                                        SLCode = accDocumentItem.SL.SLCode,
                                        TLCode = accDocumentItem.SL.TL.TLCode,
                                        TLTitle = accDocumentItem.SL.TL.Title,
                                        TLTitle2 = accDocumentItem.SL.TL.Title2,
                                        TrackingDate = accDocumentItem.TrackingDate,
                                        TrackingNumber = accDocumentItem.TrackingNumber,
                                        DescriptionItem1 = accDocumentItem.Description1,
                                        DescriptionItem2 = accDocumentItem.Description2,
                                    })
                                   ;
                report.RegBusinessObject("AccHeaderItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accHeaderItemReport.mrt");
                report.Print();
            }
        }
        private void AccDocumentItemsRadGridViewContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string tag = (e.OriginalSource as RadMenuItem).Tag as string;
            if (clipboardAccDocumentItems == null)
            {
                Paste.IsEnabled = false;
            }
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
                    var index = AccDocumentItemsRadGridView.Items.IndexOf(AccDocumentItemsRadGridView.SelectedItem);
                    if (index > -1)
                        ((ObservableCollection<AccDocumentItem>)AccDocumentItemsRadGridView.ItemsSource).Insert(index, new AccDocumentItem());
                    break;
                case "edit":
                    AccDocumentItemsRadGridView.BeginEdit();
                    break;
                case "delete":
                    AccDocumentItemsRadGridView.Items.Remove(AccDocumentItemsRadGridView.SelectedItem);
                    break;
                case "copyHeader":
                    Paste.IsEnabled = true;
                    PasteHeader.IsEnabled = true;

                    // PasteHeader.IsEnabled = true;
                    clipboardAccDocumentHeader = new AccDocumentHeader();
                    var accDocumentHeaders = AccDocumentHeaderRadGridView.SelectedItems.Cast<AccDocumentHeader>().SelectMany(x => x.AccDocumentItems);
                    clipboardAccDocumentItems = new ObservableCollection<AccDocumentItem>();
                    foreach (var item in accDocumentHeaders)
                    {
                        clipboardAccDocumentItems.Add(new AccDocumentItem()
                        {
                            Debit = item.Debit,
                            Description1 = item.Description1,
                            Credit = item.Credit,
                            Description2 = item.Description2,
                            DL1 = item.DL1,
                            DL2 = item.DL2,
                            DL1Id = item.DL1Id,
                            DL2Id = item.DL2Id,
                            SL = item.SL,
                            SLId = item.SLId,
                            CurrencyAmount = item.CurrencyAmount,
                            Currency = item.Currency,
                            CurrencyId = item.CurrencyId,
                            Handled = item.Handled,
                            TrackingNumber = item.TrackingNumber,
                            TrackingDate = item.TrackingDate
                        });
                    }

                    break;
                case "pasteHeader":
                    PasteHeader.IsEnabled = false;
                    var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                    addNewCommand.Execute(null, AccDocumentHeaderDataForm);
                    if (clipboardAccDocumentItems.Count > 0)
                    {
                        Paste.IsEnabled = true;
                        for (int i = 0; i < clipboardAccDocumentItems.Count(); i++)
                        {
                            int pasteIndex = AccDocumentItemsRadGridView.Items.Count - 1;
                            if (pasteIndex == -1)
                            {
                                pasteIndex = 0;
                            }
                            var x = (ObservableCollection<AccDocumentItem>)AccDocumentItemsRadGridView.ItemsSource;
                            x.Insert(pasteIndex, clipboardAccDocumentItems[i]);
                        }
                        AccDocumentItemsRadGridView.Items.Refresh();
                    }

                    break;
                case "copy":
                    Paste.IsEnabled = true;
                    // clipboardAccDocumentItems = new ObservableCollection<AccDocumentItem>();
                    //   Clipboard = new ObservableCollection<EditableAccItem>();
                    //var items= AccDocumentItemsRadGridView.SelectedItems;
                    // AccDocumentItem items1 = (AccDocumentItemsRadGridView.SelectedItems).Cast<AccDocumentItem>();

                    // for (int i = 0; i < (items.Count); i++)
                    // {
                    //     var SL = _viewModel.GetSLById(items[i].SLId);

                    //     if (items[i].SLId == 0)
                    //         AccDocumentItemsRadGridView.Items.Remove(items[i]);
                    // }
                    // Clipboard=
                    clipboardAccDocumentItems = GetAccDocumnetItems(AccDocumentItemsRadGridView.SelectedItems);

                    break;
                case "paste":

                    Paste.IsEnabled = false;
                    if (clipboardAccDocumentItems.Count > 0)
                    {
                        Paste.IsEnabled = true;
                        for (int i = 0; i < clipboardAccDocumentItems.Count(); i++)
                        {
                            int pasteIndex = AccDocumentItemsRadGridView.Items.Count - 1;
                            if (pasteIndex == -1)
                            {
                                pasteIndex = 0;
                            }
                            var x = (ObservableCollection<AccDocumentItem>)AccDocumentItemsRadGridView.ItemsSource;
                            x.Insert(pasteIndex, clipboardAccDocumentItems[i]);
                        }
                        // AccDocumentItemsRadGridView.Items.Refresh();
                        clipboardAccDocumentItems = new ObservableCollection<AccDocumentItem>();
                    }

                    break;
                default:
                    break;
            }
        }

        #endregion
        #region Navigation
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            AccDocumentHeaderDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            AccDocumentHeaderDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            AccDocumentHeaderDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            AccDocumentHeaderDataForm.MoveCurrentToPrevious();
        }
        #endregion

        #region AccDocumentHeaderDataForm

        #region AccDocumentHeaderDataFormCommands

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(46))
            {
                detailRadTabItem.IsSelected = true;
                draftButton.Visibility = Visibility.Visible;

                StatusEnum status;
                if (_viewModel.AccDocumentHeaders.CurrentItem is AccDocumentHeader AccDocumentHeader)
                {
                    var items = AccDocumentHeader.AccDocumentItems.ToList();

                    for (int i = 0; i < items.Count; i++)
                    {

                        if (items[i].SLId == 0)
                            AccDocumentItemsRadGridView.Items.Remove(items[i]);
                    }
                    status = AccDocumentHeader.Status;
                    var x = AccDocumentHeader.ManualDocumentNumber;
                }

                NavStateFalse();

                var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                addNewCommand.Execute(null, AccDocumentHeaderDataForm);
            }
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
            if (_accessUtility.HasAccess(49))
            {
                OnCanceling();
                if (_dialogResult == true)
                {
                    if (AccDocumentHeaderDataForm != null)
                    {
                        NavStateTrue();
                        AccDocumentHeaderDataForm.CancelEdit();
                        //  AccDocumentHeaderDataForm.BeginEdit();
                        _viewModel.Reject();
                    }
                }
            }
            ////var cancelCommand = RadDataFormCommands.CancelEdit as RoutedUICommand;
            ////cancelCommand.Execute(null, AccDocumentHeaderDataForm);
            //MessageBoxResult result = MessageBox.Show("آیا مطمئن هستید؟", "اخطار", MessageBoxButton.OKCancel);
            //if (result == MessageBoxResult.OK)
            //{
            //    if (AccDocumentHeaderDataForm != null)
            //    {
            //        NavStateTrue();

            //        AccDocumentHeaderDataForm.CancelEdit();
            //        AccDocumentHeaderDataForm.BeginEdit();
            //    }
            //}
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(48))
            {//  NavStateFalse();
             //if (_viewModel.AccDocumentHeaders.CurrentItem is AccDocumentHeader accDocumentHeader)
             //{

                var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
                deleteCommand.Execute(null, AccDocumentHeaderDataForm);
            }
        }
        private void Deleting()
        {

        }
        private void DraftButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(50))
            {
                if (ValidateDocumetHeader())
                {
                    var message = "";
                    var r = _viewModel.HasExport(AccDocumentHeader);
                    if (r != 0)
                    {

                        if (r == 1)
                        {
                            message = "سود و زیانی";
                        }
                        else if (r == 3)
                        {
                            message = "افتتاحیه";
                        }
                        else if (r == 4)
                        {
                            message = "اختتامیه";
                        }
                        else if (r == 5)
                        {
                            message = "سند کل";
                        }
                        else if (r == 6)
                        {
                            message = "تسعیر ارز";
                        }
                        DialogParameters parameters = new DialogParameters();
                        parameters.OkButtonContent = "بستن";
                        parameters.Header = "اخطار";
                        parameters.Content = "امکان تغییر وضعیت پیش نویس  سند به علت صدور سند" + message + "وجود ندارد";
                        // parameters.Closed = OnClosed;
                        RadWindow.Alert(parameters);
                    }
                    else
                    {

                        AccDocumentHeader.Status = StatusEnum.Draft;
                        CommitAndBeginEdit();
                        RaiseCanExecuteChanged();
                        var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                        var alert = new RadDesktopAlert
                        {
                            FlowDirection = FlowDirection.RightToLeft,
                            Header = "اطلاعات",
                            Content = ".پیش نویس انجام شد",
                            ShowDuration = 1200,
                        };

                        manager.ShowAlert(alert);
                        GETSL();
                    }
                }
            }
        }

        private void GETSL()
        {
            var xxx = (AccDocumentHeader)_viewModel.AccDocumentHeaders.CurrentItem;
            if (xxx != null)
            {
                foreach (var item in xxx.AccDocumentItems)
                {
                    item.SL = _viewModel.GetSLById(item.SLId);
                    if (item.SL.DLType1 != 0)
                        item.DL1 = _viewModel.GetDLById(item.DL1Id.Value);
                    if (item.SL.DLType2 != 0)
                        item.DL2 = _viewModel.GetDLById(item.DL2Id.Value);
                    if (item.CurrencyId != 0 && item.CurrencyId != null)
                        item.Currency = _viewModel.GetCurrById(item.CurrencyId.Value);


                }

            }
        }

        private void TemporaryButton1_Click(object sender, RoutedEventArgs e)
        {
            var status = AccDocumentHeaderDataForm.ValidateItem()
                  && AccDocumentHeader?.Difference == 0
                  && AccDocumentHeader.Status != StatusEnum.End
                  && AccDocumentHeader.Status != StatusEnum.Permanent
                  && (AccDocumentHeader.Status != StatusEnum.Temporary
                  || (isModified && AccDocumentHeader.Status == StatusEnum.Temporary));
            //  var status = RaiseCanExecuteChanged(StatusEnum.Temporary);

            if (status == true)
            {
                if (ValidateDocumetHeader())
                {
                    AccDocumentHeader.Status = StatusEnum.Temporary;
                    CommitAndBeginEdit();
                    RaiseCanExecuteChanged();
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".ثبت موقت انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);

                }
                GETSL();

            }
            else
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "اخطار";
                parameters.Content = "امکان ثبت موقت وجود ندارد";
                RadWindow.Alert(parameters);
            }

        }
        private void TemporaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(51))
            {
                if (ValidateDocumetHeader())
                {
                    AccDocumentHeader.Status = StatusEnum.Temporary;
                    CommitAndBeginEdit();
                    RaiseCanExecuteChanged();
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".ثبت موقت انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                    GETSL();

                }
            }
        }
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(52))
            {
                if (ValidateDocumetHeader())
                {
                    AccDocumentHeader.Status = StatusEnum.End;
                    AccDocumentHeader.Seconder = _appContextService.CurrentUser.FriendlyName;
                    CommitAndBeginEdit();
                    RaiseCanExecuteChanged();
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".خاتمه انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                    GETSL();
                }
                //var status= RaiseCanExecuteChanged(StatusEnum.End);
                //if (status == true)
                //{
                //    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                //    var alert = new RadDesktopAlert
                //    {
                //        FlowDirection = FlowDirection.RightToLeft,
                //        Header = "اطلاعات",
                //        Content = ".خاتمه انجام شد",
                //        ShowDuration = 1200,
                //    };
                //    manager.ShowAlert(alert);
                //}
                //else
                //{
                //    DialogParameters parameters = new DialogParameters();
                //    parameters.OkButtonContent = "بستن";
                //    parameters.Header = "اخطار";
                //    parameters.Content = "امکان ثبت خاتمه وجود ندارد";
                //    RadWindow.Alert(parameters);
                //}

            }
        }
        private void BackFromEndButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(53))
            {
                if (ValidateDocumetHeader())
                {
                    AccDocumentHeader.Status = StatusEnum.Temporary;
                    CommitAndBeginEdit();
                    isModified = true;
                    RaiseCanExecuteChanged();
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ". برگشت از خاتمه انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                    GETSL();
                }
                //var status= RaiseCanExecuteChanged(StatusEnum.Temporary);
                //if (status == true)
                //{
                //    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                //    var alert = new RadDesktopAlert
                //    {
                //        FlowDirection = FlowDirection.RightToLeft,
                //        Header = "اطلاعات",
                //        Content = ". برگشت از خاتمه انجام شد",
                //        ShowDuration = 1200,
                //    };
                //    manager.ShowAlert(alert);
                //}
                //else
                //{
                //    DialogParameters parameters = new DialogParameters();
                //    parameters.OkButtonContent = "بستن";
                //    parameters.Header = "اخطار";
                //    parameters.Content = "امکان ثبت برگشت از خاتمه وجود ندارد";
                //    RadWindow.Alert(parameters);
                //}

            }
        }
        private void PermanentButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDocumetHeader())
            {
                AccDocumentHeader.Status = StatusEnum.Permanent;
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".صند قطعی شد",
                    ShowDuration = 1200,
                };
                manager.ShowAlert(alert);
                GETSL();

                //var  status= RaiseCanExecuteChanged(StatusEnum.Permanent);
                //if (status == true)
                //{
                //    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                //    var alert = new RadDesktopAlert
                //    {
                //        FlowDirection = FlowDirection.RightToLeft,
                //        Header = "اطلاعات",
                //        Content = ".صند قطعی شد",
                //        ShowDuration = 1200,
                //    };
                //    manager.ShowAlert(alert);
                //}
                //else
                //{
                //    DialogParameters parameters = new DialogParameters();
                //    parameters.OkButtonContent = "بستن";
                //    parameters.Header = "اخطار";
                //    parameters.Content = "امکان ثبت قطعی کردن سند وجود ندارد";
                //    RadWindow.Alert(parameters);
                //}

            }
        }
        public void NavStateFalse()
        {
            // addButton.IsEnabled = false;
            //  saveButton.IsEnabled = true;
            // deleteButton.IsEnabled = false;
            // cancelButton.IsEnabled = true;
            //editButton.IsEnabled = false;
            //firstButton.IsEnabled = false;
            //nextButton.IsEnabled = false;
            //lastButton.IsEnabled = false;
            //previousButton.IsEnabled = false;
        }
        public void NavStateTrue()
        {
            //  addButton.IsEnabled = true;
            // saveButton.IsEnabled = false;
            // deleteButton.IsEnabled = true;
            // cancelButton.IsEnabled = false;
            // editButton.IsEnabled = true;
            //firstButton.IsEnabled = true;
            //nextButton.IsEnabled = true;
            //lastButton.IsEnabled = true;
            //previousButton.IsEnabled = true;
        }
        private bool ValidateDocumetHeader()
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            var result = true;
            var items = AccDocumentHeader.AccDocumentItems.ToList();
            var errorMessage = "";
            bool resultAcc = false;
            foreach (var item in items)
            {
                if (item.CurrencyId != null)
                {
                    resultAcc = true;
                }
            }
            _viewModel.DocumentDate = DateTime.Now;

            var year = _appContextService.CurrentFinancialYear;

            var date = AccDocumentHeaderDataForm.ChildrenOfType<Microsoft.Windows.Controls.DatePicker>().FirstOrDefault(x => x.Name == "dateDoc");
            // string formatted = date.Text.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var c = date.Text;
            var DocDate = persianCalendar.GetYear(date.DisplayDate);
            var last = _viewModel.LastPermanent();
            var lastexchange = _viewModel.LastExchange();
            //  var lastDate = last.Value.ToString("{yy-MM-dd}");
            //  var s=  String.Format("{0:yy-MM-dd}", xx);

            if (DocDate != year)
            {
                errorMessage += $"تاریخ سند مطابق با سال مالی نمی باشد {Environment.NewLine}";
            }
            if (date.Text == "")
            {
                errorMessage += $"تاریخ سند خالی می باشد {Environment.NewLine}";
            }
            if (last != null)
            {
                if ((DateTime.Parse(c)) <= last)
                {
                    errorMessage += $"در بین اسناد دائم نمی توانید سند صادر کنید {Environment.NewLine}";
                }
            }
            if (lastexchange != null)
            {
                if (((DateTime.Parse(c)) <= lastexchange) && resultAcc == true)
                {
                    errorMessage += $"در بین اسناد تسعیر نمی توانید سند صادر کنید {Environment.NewLine}";
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                var SL = _viewModel.GetSLById(items[i].SLId);

                if (items[i].SLId == 0)
                    AccDocumentItemsRadGridView.Items.Remove(items[i]);
                else
                {
                    if (SL.DLType1 != 0 && items[i].DL1Id == null)
                        errorMessage += $"ردیف {i + 1} تفصیل اول نمی تواند خالی باشد {Environment.NewLine}";
                    if (SL.DLType2 != 0 && items[i].DL2Id == null)
                        errorMessage += $"ردیف {i + 1} تفصیل دوم نمی تواند خالی باشد {Environment.NewLine}";

                    if (items[i].Description1 == "" || items[i].Description1 == null)
                    {
                        errorMessage += $"ردیف {i + 1} شرح آرتیکل نمی تواند خالی باشد {Environment.NewLine}";

                    }
                    if (items[i].Debit == 0 && items[i].Credit == 0)
                    {
                        errorMessage += $"ردیف {i + 1} بدهکار یا بستانکار را مقدار دهی کنید {Environment.NewLine}";

                    }
                }
            }

            AccDocumentHeaderDataForm.ValidationSummary.Errors.Clear();
            using (var uow = new SainaDbContext())
            {
                if (AccDocumentHeader.AccDocumentHeaderId==0)
                {

               HasDocumentNumber=   uow.AccDocumentHeaders.Any(x => x.DocumentNumber == AccDocumentHeader.DocumentNumber);
               HasManualDocumentNumber =   uow.AccDocumentHeaders.Any(x => x.ManualDocumentNumber == AccDocumentHeader.ManualDocumentNumber);
                }
                else
                {
                    HasDocumentNumber = uow.AccDocumentHeaders.Any(x => x.DocumentNumber == AccDocumentHeader.DocumentNumber && x.AccDocumentHeaderId!=AccDocumentHeader.AccDocumentHeaderId);
                    HasManualDocumentNumber = uow.AccDocumentHeaders.Any(x => x.ManualDocumentNumber == AccDocumentHeader.ManualDocumentNumber && x.AccDocumentHeaderId != AccDocumentHeader.AccDocumentHeaderId
                    );
                }
            }
            if (HasDocumentNumber==true)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "شماره سند",
                    ErrorContent = "شماره سند نباید تکراری باشد"
                });
            }
            if (HasManualDocumentNumber == true)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "شماره سند",
                    ErrorContent = "شماره دستی نباید تکراری باشد"
                });
            }
            if (AccDocumentHeader.DocumentNumber == 0)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "شماره سند",
                    ErrorContent = "شماره سند نباید خالی باشد"
                });

                result = false;
            }
            if (AccDocumentHeader.SystemFixNumber == 0)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "شماره ثابت سند",
                    ErrorContent = "شماره ثابت سند نباید خالی باشد"
                });

                result = false;
            }
            if (AccDocumentHeader.ManualDocumentNumber == 0)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "شماره دستی سند",
                    ErrorContent = "شماره دستی سند نباید خالی باشد"
                });
                result = false;
            }
            if (AccDocumentHeader.TypeDocumentId == 0)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "نوع سند",
                    ErrorContent = "نوع سند نباید خالی باشد"
                });
                result = false;
            }
            if (AccDocumentHeader.DocumentDate == null)
            {
                AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
                {
                    SourceFieldDisplayName = "تاریخ سند",
                    ErrorContent = "تاریخ سند نباید خالی باشد"
                });
                result = false;
            }
            items = AccDocumentHeader.AccDocumentItems.ToList();

            if (items.Count == 0)
            {
                result = false;
                errorMessage += $" هیچ سندی برای ثبت وجود ندارد {Environment.NewLine}";
            }
            if (errorMessage.Length > 0)
            {
                result = false;

                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "!اخطار";
                parameters.Content = errorMessage;
                RadWindow.Alert(parameters);
            }

            return result;
        }
        private void RaiseCanExecuteChanged()
        {
            if (AccDocumentHeader != null)
            {
                draftButton.IsEnabled = AccDocumentHeader.Status == StatusEnum.Temporary ||
                (AccDocumentHeader.Status != StatusEnum.Permanent && isModified);
                temporaryButton.IsEnabled =
                    AccDocumentHeaderDataForm.ValidateItem()
                    && AccDocumentHeader?.Difference == 0
                    && AccDocumentHeader.Status != StatusEnum.End
                    && AccDocumentHeader.Status != StatusEnum.Permanent
                    && (AccDocumentHeader.Status != StatusEnum.Temporary
                    || (isModified && AccDocumentHeader.Status == StatusEnum.Temporary));
                endButton.IsEnabled =
                     AccDocumentHeaderDataForm.ValidateItem()
                    && AccDocumentHeader?.Difference == 0
                    && AccDocumentHeader.Status == StatusEnum.Temporary;
                backFromEndButton.IsEnabled = AccDocumentHeader.Status == StatusEnum.End;
                //permanentButton.IsEnabled = AccDocumentHeader.Status == StatusEnum.End
                //&& AccDocumentHeader?.HasErrors == false
                //&& AccDocumentHeader?.Difference == 0;

                isModified = false;
            }

            AccDocumentItemsRadGridView.IsReadOnly = AccDocumentHeader?.Status == StatusEnum.Permanent;
        }
        private bool RaiseCanExecuteChanged1(StatusEnum status)
        {
            var enable = false;
            var Status = status;
            AccDocumentItemsRadGridView.IsReadOnly = AccDocumentHeader?.Status == StatusEnum.Permanent;

            var draftButton = AccDocumentHeader.Status == StatusEnum.Temporary ||
             (AccDocumentHeader.Status != StatusEnum.Permanent && isModified);
            var temporaryButton =
                  AccDocumentHeaderDataForm.ValidateItem()
                  && AccDocumentHeader?.Difference == 0
                  && AccDocumentHeader.Status != StatusEnum.End
                  && AccDocumentHeader.Status != StatusEnum.Permanent
                  && (AccDocumentHeader.Status != StatusEnum.Temporary
                  || (isModified && AccDocumentHeader.Status == StatusEnum.Temporary));
            var endButton =
                  AccDocumentHeaderDataForm.ValidateItem()
                 && AccDocumentHeader?.Difference == 0
                 && AccDocumentHeader.Status == StatusEnum.Temporary;
            var backFromEndButton = AccDocumentHeader.Status == StatusEnum.End;
            var permanentButton = AccDocumentHeader.Status == StatusEnum.End
                   && AccDocumentHeader?.HasErrors == false
                   && AccDocumentHeader?.Difference == 0;

            isModified = false;
            if (status == StatusEnum.Draft && draftButton == true)
            {
                enable = true;
            }
            else if (status == StatusEnum.Temporary && temporaryButton == true)
            {
                enable = true;
            }
            else if (status == StatusEnum.End && endButton == true)
            {
                enable = true;
            }
            else if (status == StatusEnum.Permanent && permanentButton == true)
            {
                enable = true;
            }

            return enable;
        }
        private void CommitAndBeginEdit()
        {
            AccDocumentHeaderRadGridView.CommitEdit();
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (_viewModel != null)
            {
                commitEditCommand.Execute(null, AccDocumentHeaderDataForm);
                _viewModel.Save();
                BeginEdit();
            }
        }

        #endregion

        #region AccDocumentHeaderDataFormEventHandlers

        private void AccDocumentHeaderDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

            //DialogParameters parameters = new DialogParameters();
            //parameters.OkButtonContent = "بله";
            //parameters.CancelButtonContent = "خیر";
            //parameters.Header = "اخطار";
            //parameters.Content = "آیا مطمئن هستید که می خواهید بدون ذخیره تغیرات، به سند بعدی بروید؟";
            //parameters.Closed = OnClosed;
            //RadWindow.Confirm(parameters);
            //if (_dialogResult==true)
            //{
            //    _viewModel.Reject();
            //}
            //else
            //{

            //}
            // var acc = AccDocumentHeader.Status;
            RaiseCanExecuteChanged();
        }
        private void AccDocumentHeaderDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            
               PersianCalendar persianCalendar = new PersianCalendar();
            _viewModel.DocumentDate = DateTime.Now;
            var newItem = ((AccDocumentHeader)e.NewItem);
            ((AccDocumentHeader)e.NewItem).PropertyChanged += AccDocumentHeader_PropertyChanged;
            ((ObservableCollection<AccDocumentItem>)newItem.AccDocumentItems).CollectionChanged += AccDocumentItems_CollectionChanged;
            newItem.TypeDocumentId = 2;//عمومی
            newItem.SystemName = System.Environment.MachineName;
            newItem.Exporter = _appContextService.CurrentUser.FriendlyName;
            var year = _appContextService.CurrentFinancialYear;
            var startYear = _currencyExchangesService.GetStartFinancialYear1(year);

            var DocDate = persianCalendar.GetYear(_viewModel.DocumentDate.Value);

            //if (DocDate != year)
            //{
            //    newItem.DocumentDate = startYear;
            //}
            using (var uow = new SainaDbContext())
            {
                var docDate = uow.Database.SqlQuery<DateTime?>($"SELECT MAX(DocumentDate) FROM Acc.AccDocumentHeaders WHERE[dbo].[ShamsiToMiladi] (DocumentDate, 'Saal') ={_appContextService.CurrentFinancialYear}").FirstOrDefault();
                if (docDate != null)
                {
                    newItem.DocumentDate = docDate.Value;
                }
                else if (docDate == null)
                {
                    if (DocDate != year)
                    {
                        newItem.DocumentDate = startYear;
                    }
                    else
                    {
                        newItem.DocumentDate = DateTime.Now;
                    }
                }

                var getDocumentNumbering = uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefault(x => x.AccountDocumentId == 1);
                var EndNumber = getDocumentNumbering.EndNumber;
                var zz = $"SELECT MAX(DocumentNumber)+1 FROM Acc.AccDocumentHeaders WHERE[dbo].[ShamsiToMiladi] (DocumentDate, 'Saal') ={_appContextService.CurrentFinancialYear}";
                var lastAccDocumentHeaderCode = uow.Database.SqlQuery<long?>($"SELECT MAX(DocumentNumber)+1 FROM Acc.AccDocumentHeaders WHERE[dbo].[ShamsiToMiladi] (DocumentDate, 'Saal') ={_appContextService.CurrentFinancialYear}").FirstOrDefault();

                lastAccDocumentHeaderCode = lastAccDocumentHeaderCode != null ? lastAccDocumentHeaderCode : 0;

                if (lastAccDocumentHeaderCode > EndNumber)
                {
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "اخطار";
                    parameters.Content = ".شماره گذاری اسناد به پایان رسیده، لطفا شماره گذاری اسناد را بررسی نمایید";
                    RadWindow.Alert(parameters);
                    return;
                }
                else
                {
                    long startNumber = getDocumentNumbering.StartNumber;
                    lastAccDocumentHeaderCode = lastAccDocumentHeaderCode == 0 ? startNumber : lastAccDocumentHeaderCode++;
                    int? accountDocumentId = getDocumentNumbering.AccountDocumentId;
                   style = getDocumentNumbering.StyleId;
                  countingWayId = getDocumentNumbering.CountingWayId;
                    var lastDailyNumberCode = uow.Database.SqlQuery<long?>($"select MAX(DailyNumber)+1 from Acc.AccDocumentHeaders where YEAR(DocumentDate)={ newItem.DocumentDate.Year} and MONTH(DocumentDate)={ newItem.DocumentDate.Month} and DAY(DocumentDate)={ newItem.DocumentDate.Day}").FirstOrDefault();
                    if (lastDailyNumberCode == null)
                    {
                        lastDailyNumberCode = 1;
                    }
                    newItem.DailyNumber = lastDailyNumberCode.Value;

                    string stringLastAccDocumentHeaderCode = lastAccDocumentHeaderCode.ToString();
                    string stringlastDailyNumberCode = lastDailyNumberCode.ToString();
                    string lastAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                    string lastDayAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                    if (lastAccDocumentHeaderCode <= EndNumber)
                    {
                        newItem.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");
                        if (style == 1 && countingWayId == 1)
                        {
                            newItem.IsReadOnly = false;
                            newItem.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                        else if (style == 1 && countingWayId == 2)
                        {
                            newItem.IsReadOnly = true;
                            newItem.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                        else if (style == 2 && countingWayId == 1)
                        {
                            newItem.IsReadOnly = false;
                            newItem.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                        else if (style == 2 && countingWayId == 2)
                        {
                            newItem.IsReadOnly = true;
                            newItem.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                            newItem.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                        }
                    }
                }
            }
            _viewModel.AddAccDocumentHeader((AccDocumentHeader)e.NewItem);
        }
        private void AccDocumentHeaderDataForm_DeletingItem(object sender, CancelEventArgs e)
        {

            AccDocumentHeader1 = AccDocumentHeader;
            var r = _viewModel.HasAcc(AccDocumentHeader.DocumentDate, AccDocumentHeader.TypeDocument, AccDocumentHeader.Status);
            var message = "";
            if (r == 2)
            {
                var status = AccDocumentHeader.Status;
                if (status == StatusEnum.End)
                {
                    message = "خاتمه";
                }
                if (status == StatusEnum.Permanent)
                {
                    message = "دائم";
                }
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "اخطار";
                parameters.Content = "امکان حذف سند به علت " + message + "وجود ندارد";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else
            {

                if (r == 0)
                {

                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بله، مطمئنم";
                    parameters.CancelButtonContent = "خیر";
                    parameters.Header = "اخطار";
                    parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                    parameters.Closed = OnClosed;
                    RadWindow.Confirm(parameters);
                    e.Cancel = _dialogResult == false;
                    // _dialogResult == true;
                }
                else
                {

                    if (r == 1)
                    {
                        message = "سود و زیانی";
                    }
                    else if (r == 3)
                    {
                        message = "افتتاحیه";
                    }
                    else if (r == 4)
                    {
                        message = "اختتامیه";
                    }
                    else if (r == 5)
                    {
                        message = "سند کل";
                    }
                    else if (r == 6)
                    {
                        message = "تسعیر ارز";
                    }
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "اخطار";
                    parameters.Content = "گردش دارد.امکان حذف وجود ندارد" + message + "این سند در سند";

                    // parameters.Closed = OnClosed;
                    RadWindow.Alert(parameters);
                    e.Cancel = true;
                }

            }
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void AccDocumentHeaderDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            if (_dialogResult == true)
            {
                _viewModel.DeleteDocumentHeader((AccDocumentHeader1));
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".حذف با موفقیت انجام شد",
                    ShowDuration = 12000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void AccDocumentHeaderDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                _viewModel.Save();
        }
        private void AccDocumentHeaderDataForm_ValidatingItem(object sender, CancelEventArgs e)
        {
            //grid.SetValue(Grid.ToolTipProperty, new Binding()
            //{
            //    Source = textBox1,
            //    Path = new PropertyPath("(Validation.Errors).CurrentItem.ErrorContent")
            //});
            //if (AccDocumentHeaderDataForm?.ValidationSummary != null)
            //{
            //    AccDocumentHeaderDataForm.ValidationSummary.Errors.Clear();
            //    AccDocumentHeaderDataForm.ValidationSummary.Errors.Add(new Telerik.Windows.Controls.Data.ErrorInfo
            //    {
            //        SourceFieldDisplayName = "شماره سند",
            //        ErrorContent = "Test Error"
            //    });
            //    e.Cancel = false;
            //}

        }

        #endregion

        #endregion

        #region AccDocumentHeaderRadGridView

        private void AccDocumentHeader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Status")
            {
                isModified = true;
            }
            else if (e.PropertyName == "Status")
            {
                AccDocumentItemsRadGridView.IsReadOnly = AccDocumentHeader.Status == StatusEnum.Permanent;
            }
            AccDocumentHeaderDataForm.BeginEdit();
            RaiseCanExecuteChanged();
        }
        private void AccDocumentHeaderRadGridView_FieldFilterEditorCreated(object sender, EditorCreatedEventArgs e)
        {
            //get the StringFilterEditor in your RadGridView
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }
        private void AccDocumentHeaderRadGridView_FilterOperatorsLoading(object sender, FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void AccDocumentHeaderRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AccDocumentHeader.TypeDocumentId == 2)
            {
                detailRadTabItem.IsSelected = true;
                AccDocumentItemsRadGridView.IsReadOnly = true;
            }
            else
            {
                detailRadTabItem.IsSelected = true;

                AccDocumentItemsRadGridView.IsReadOnly = true;
                AccDocumentHeaderDataForm.CancelEdit();
                draftButton.Visibility = Visibility.Hidden;
            }
            detailRadTabItem.IsSelected = true;
            AccDocumentItemsRadGridView.IsReadOnly = true;
            //BeginEdit();

        }
        private void BeginEdit()
        {
            if (AccDocumentHeader?.Status != StatusEnum.Permanent && AccDocumentHeader?.Status != StatusEnum.End)
            {
                AccDocumentHeaderDataForm.BeginEdit();
                AccDocumentItemsRadGridView.IsReadOnly = false;
            }
            else
            {
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".این سند در وضعیت دائم یا خاتمه می باشد و امکان ویرایش ندارد",
                    ShowDuration = 1200,
                };
                manager.ShowAlert(alert);
            }
        }

        #endregion

        #region AccDocumentItems

        private void AccDocumentItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            isModified = true;
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                ((AccDocumentItem)e.NewItems[0]).PropertyChanged += Item_PropertyChanged;
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
        private void AccDocumentItemsRadGridView_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            var cc= ((RadGridView)sender).DataContext;
            if (cc  is AccDocumentItem a)
            {

            }
            AccDocumentItemsRadGridView.SelectedItem = ((RadGridView)sender).DataContext;
            var senderacc = AccDocumentItemsRadGridView.SelectedItem;
            //فقط خواندنی کردن سلول ها
            if (AccDocumentItemsRadGridView.SelectedItem is AccDocumentItem accDocumentItem)
            {
                if (e.Cell.Column.UniqueName != "SL" && accDocumentItem.SLId == 0)
                {
                    e.Cancel = true;
                }
                else if (e.Cell.Column.UniqueName == "DL1")
                {
                    if (accDocumentItem.SL.DLType1 == 0)
                    {
                        accDocumentItem.DL1 = null;
                        //e.Cancel = true;
                        e.Cell.Column.IsEnabled = false;
                    }
                    else
                    {
                        e.Cell.Column.IsEnabled = true;
                    }
                }
                else if (e.Cell.Column.UniqueName == "DL2")
                {
                    e.Cancel = accDocumentItem.SL.DLType2 == 0;
                }
                else if (e.Cell.Column.UniqueName == "CurrencyTitle")
                {
                    e.Cancel = !(accDocumentItem.HasCurrency && _viewModel.Currencies.Any());
                    if (e.Cancel)
                    {
                        e.Cell.Column.IsEnabled = false;
                    }
                }
                else if (e.Cell.Column.UniqueName == "ExchangeRate" || e.Cell.Column.UniqueName == "CurrencyAmount")
                {
                    if (accDocumentItem.CurrencyId == 0)
                    {
                        e.Cancel = true;
                        e.Cell.Column.IsEnabled = false;
                    }
                }
                AccDocumentItemsRadGridView.SelectedItem = ((RadGridView)sender).DataContext;

            }
        }
        private void AccDocumentItemsRadGridView_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
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
                    if (_viewModel.AccDocumentItem != null)
                    {
                        if (e.Cell.Column.UniqueName == "description1")
                            _viewModel.AccDocumentItem.Description1 = textEntered;
                        else if (e.Cell.Column.UniqueName == "description2")
                            _viewModel.AccDocumentItem.Description2 = textEntered;
                    }
                }
            }



        }
        private void AccDocumentItemsRadGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                var tempDebit = ((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Debit;
                ((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Debit = ((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Credit;
                ((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Credit = tempDebit;
                // (((AccDocumentItem)parentGrid.CurrentItem).Debit, ((AccDocumentItem)parentGrid.CurrentItem).Debit) = (((AccDocumentItem)parentGrid.CurrentItem).Debit, ((AccDocumentItem)parentGrid.CurrentItem).Debit);
                if (((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Debit != 0)
                    ((AccDocumentItem)AccDocumentItemsRadGridView.SelectedItem).Credit = 0;

            }
        }
        private void AccDocumentItemsRadGridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            //e.ErrorMessage = "ggfgff";
            //e.IsValid = false;

            //  var ee = ((RadComboBox)e.EditingElement).SelectedValue==null;
        }
        private void AccDocumentItemsRadGridView_CellValidated(object sender, GridViewCellValidatedEventArgs e)
        {
        }
        private void AccDocumentItemsRadGridView_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {

            var gridView = e.OwnerGridViewItemsControl;
            AccDocumentItemsRadGridView.ScrollIntoViewAsync(AccDocumentItemsRadGridView.Items[0], //the row
                                 AccDocumentItemsRadGridView.Columns[01], //the column
                                 new Action<FrameworkElement>((f) =>
                                 {
                                     if (f is GridViewRow row)
                                     {
                                         row.IsSelected = true; // the callback method
                                         //row.CommitEdit();
                                     }
                                 }));
        }

        #endregion

        #region Other

        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            //if (detailRadTabItem.IsSelected && AccDocumentHeaderRadGridView.SelectedItem == null && _viewModel.HeaderId == 0)
            //{
            //    var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            //    addNewCommand.Execute(null, AccDocumentHeaderDataForm);
            //    //  BeginEdit();
            //    // RaiseCanExecuteChanged();
            //}
            //else 

            if (detailRadTabItem.IsSelected && AccDocumentHeaderRadGridView.SelectedItem != null)
            {

                if (AccDocumentHeader.TypeDocumentId == 2)
                {
                    detailRadTabItem.IsSelected = true;
                    AccDocumentItemsRadGridView.IsReadOnly = true;
                    draftButton.Visibility = Visibility.Visible;

                }
                else
                {
                    detailRadTabItem.IsSelected = true;

                    AccDocumentItemsRadGridView.IsReadOnly = true;
                    AccDocumentHeaderDataForm.CancelEdit();
                    draftButton.Visibility = Visibility.Hidden;
                }
                detailRadTabItem.IsSelected = true;
                AccDocumentItemsRadGridView.IsReadOnly = true;
            }
        }
        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(57))
            {
                AttachmentListWindow attachmentListWindow = new AttachmentListWindow();
                attachmentListWindow.DataContext = DataContext;

                attachmentListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                attachmentListWindow.Width = 1024;
                attachmentListWindow.Height = 768;
                attachmentListWindow.CanClose = true;
                attachmentListWindow.Owner = Window.GetWindow(this);
                attachmentListWindow.Owner = Window.GetWindow(this);
                attachmentListWindow.Show();
                attachmentListWindow.Show();
            }
            //  attachmentListWindow.ShowDialog();
        }
        private void DocumentHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            //IEnumerable<int> headerIds;
            //if (AccDocumentHeaderRadGridView.SelectedItems != null)
            //    headerIds = AccDocumentHeaderRadGridView.SelectedItems.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
            if (_accessUtility.HasAccess(55))
            {
                if (AccDocumentHeaderDataForm.CurrentItem is AccDocumentHeader accDocumentHeader && accDocumentHeader.AccDocumentHeaderId != 0)
                {
                    DocumentHistoryListWindow documentHistoryListWindow = new DocumentHistoryListWindow(accDocumentHeader.AccDocumentHeaderId);
                    documentHistoryListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    documentHistoryListWindow.Width = 1024;
                    documentHistoryListWindow.Height = 768;
                    documentHistoryListWindow.CanClose = true;
                    documentHistoryListWindow.Owner = Window.GetWindow(this);
                    documentHistoryListWindow.Show();
                }
            }

        }
        private void DL1RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.AccDocumentItem = ((RadComboBox)sender).DataContext as AccDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);

            itemsViewOriginal.Filter = ((x) =>
            {

                return x != null && _viewModel.AccDocumentItem.SL != null ? _viewModel.AccDocumentItem.SL.DLType1.HasFlag(((DL)x).DLType) : false;
            });
            itemsViewOriginal.Refresh();
        }
        private void DL2RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.AccDocumentItem = ((RadComboBox)sender).DataContext as AccDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);
            itemsViewOriginal.Filter = ((x) =>
            {
                return x != null && _viewModel.AccDocumentItem.SL != null ? _viewModel.AccDocumentItem.SL.DLType2.HasFlag(((DL)x).DLType) : false;
            });
            itemsViewOriginal.Refresh();
        }
        private void Description_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.AccDocumentItem = ((RadComboBox)sender).DataContext as AccDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);
            itemsViewOriginal.Filter = ((x) =>
            {
                return x != null && ((SLStandardDescription)x).SLId == _viewModel.AccDocumentItem.SLId;
            });
            itemsViewOriginal.Refresh();
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
            PersistenceManager manager = new PersistenceManager();
            stream = manager.Save(AccDocumentItemsRadGridView);
            //this.EnsureLoadState();
        }
        private void unload_Click(object sender, RoutedEventArgs e)
        {
            stream.Position = 0L;
            PersistenceManager manager = new PersistenceManager();
            manager.Load(AccDocumentItemsRadGridView, stream);
        }

        #endregion

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (!this.AccDocumentItemsRadGridView.Items.IsEditingItem)
            //    return;
            //var lastcolumnIndex = AccDocumentItemsRadGridView.Columns.Count - 1;
            //var isLastColumn = AccDocumentItemsRadGridView.CurrentCell?.Column == AccDocumentItemsRadGridView.Columns[lastcolumnIndex];
            //var lastRowIndex = AccDocumentItemsRadGridView.Items.Count - 1;
            //var isLastRow = AccDocumentItemsRadGridView.Items.IndexOf(AccDocumentItemsRadGridView.SelectedItem) == lastRowIndex;
            //var isFirstIndex = AccDocumentItemsRadGridView.CurrentCell.Column.Name == "SL" || AccDocumentItemsRadGridView.CurrentCell.Column.Name == "RowNumber"; //this.radGridView1.CurrentCell.ColumnInfo.Name
            //if (AccDocumentItemsRadGridView.CurrentCell != null)
            //{

            //    TextBox textBox = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<TextBox>().FirstOrDefault();
            //    RadComboBox radComboBox = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<RadComboBox>().FirstOrDefault();
            //    RadDatePicker radDatePicker = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<RadDatePicker>().FirstOrDefault();
            //    TextBlock textblock = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<TextBlock>().FirstOrDefault();

            //    var isReadOnly = AccDocumentItemsRadGridView.CurrentCell.Column.IsReadOnly;

            //    if (e.Key == Key.Tab)
            //    {
            //        if (radComboBox != null)
            //            radComboBox.IsDropDownOpen = false;
            //        e.Handled = true;
            //        RadGridViewCommands.MoveNext.Execute(null);
            //        RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //        e.Handled = true;
            //    }
            //    else if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers != ModifierKeys.Control)
            //    {
            //        if (isLastColumn && isLastRow)
            //        {
            //            AccDocumentItemsRadGridView.BeginInsert();
            //            AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
            //        }
            //        else if (AccDocumentItemsRadGridView.Items.IsAddingNew || AccDocumentItemsRadGridView.Items.IsEditingItem)
            //        {
            //            RadGridViewCommands.CommitEdit.Execute(null);
            //        }
            //        else
            //        {
            //            RadGridViewCommands.MoveNext.Execute(null);
            //            RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //            RadGridViewCommands.BeginEdit.Execute(null);
            //        }
            //        e.Handled = true;
            //    }
            //    else if (e.Key == Key.Up)
            //    {
            //        if (textBox != null && textBox.Name != "PART_EditableTextBox" || radComboBox?.ItemsSource == null || (radComboBox.ItemsSource.Cast<object>()).Count() < 2 || radComboBox?.IsDropDownOpen != true)
            //        {
            //            RadGridViewCommands.MoveUp.Execute(null);
            //            RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //            RadGridViewCommands.BeginEdit.Execute(null);

            //            e.Handled = true;
            //        }
            //    }

            //    else if (e.Key == Key.Right)
            //    {
            //        if (!isFirstIndex && (textblock != null || textBox?.CaretIndex == 0))
            //        {
            //            RadGridViewCommands.MovePrevious.Execute(null);
            //            RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //            RadGridViewCommands.BeginEdit.Execute(null);
            //            e.Handled = true;
            //        }
            //    }

            //    else if (e.Key == Key.Down)
            //    {

            //        if (textBox != null && textBox.Name != "PART_EditableTextBox" || radComboBox?.ItemsSource == null || (radComboBox.ItemsSource.Cast<object>()).Count() < 2 || radComboBox?.IsDropDownOpen != true)
            //        {
            //            if (isLastRow)
            //            {
            //                RadGridViewCommands.CommitEdit.Execute(null);
            //                RadGridViewCommands.BeginInsert.Execute(null);

            //                AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
            //                RadGridViewCommands.CommitEdit.Execute(null);
            //                RadGridViewCommands.BeginEdit.Execute(null);
            //            }
            //            else
            //            {
            //                RadGridViewCommands.MoveDown.Execute(null);
            //                RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //                RadGridViewCommands.BeginEdit.Execute(null);
            //                e.Handled = true;
            //            }
            //        }
            //    }
            //    else if ((isLastColumn && isLastRow) || ((textblock != null || textBox != null) && e.Key == Key.Left))
            //    {
            //        if ((isLastColumn && isLastRow) || (textblock != null || (textBox != null && textBox.CaretIndex == textBox.Text.Length)))
            //        {
            //            if (isLastColumn && isLastRow)
            //            {
            //                RadGridViewCommands.CommitEdit.Execute(null);
            //                RadGridViewCommands.BeginInsert.Execute(null);

            //                AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
            //                RadGridViewCommands.CommitEdit.Execute(null);
            //                RadGridViewCommands.BeginEdit.Execute(null);
            //            }
            //            else
            //            {
            //                RadGridViewCommands.MoveNext.Execute(null);
            //                RadGridViewCommands.SelectCurrentUnit.Execute(null);
            //                RadGridViewCommands.BeginEdit.Execute(null);
            //            }

            //            e.Handled = true;
            //        }
            //    }
            //    else if (radComboBox != null || radDatePicker != null)
            //    {
            //        if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            //        {
            //            AccDocumentItemsRadGridView.BeginEdit();
            //            if (radComboBox != null)
            //                radComboBox.IsDropDownOpen = true;
            //            else if (radDatePicker != null)
            //                radDatePicker.IsDropDownOpen = true;
            //            e.Handled = true;
            //        }
            //    }
            //}

        }
        private Telerik.Windows.Controls.GridViewColumn GetColumn(int DisplayIndex, bool IsMovingToNext)
        {
            foreach (var col in AccDocumentItemsRadGridView.Columns)
            {
                if (col.DisplayIndex == DisplayIndex && col.IsVisible == true)
                    return col;
                else if (col.DisplayIndex == DisplayIndex && col.IsVisible == false)
                {
                    if (IsMovingToNext)
                        return GetColumn(DisplayIndex + 1, true);
                    else
                        return GetColumn(DisplayIndex - 1, false);
                }
            }

            return null;
        }
        private void HandleKeyDown(KeyEventArgs e)
        {


        }
        private void AccDocumentItemsRadGridView_Loaded(object sender, RoutedEventArgs e)
        {
            AccDocumentItemsRadGridView.BeginInsert();
            //AccDocumentItemsRadGridView.CommitEdit();
            AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
            AccDocumentItemsRadGridView.BeginEdit(null);
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.TypeEnum = 0;
            _viewModel.UnLoaded();
        }
        private AccDocumentHeaderDataFormKeyboardCommandProvider _AccDocumentHeaderDataFormKeyboardCommandProvider;
        private AccessUtility _accessUtility;
        private IConfigSetGet _configSetGet;
        private int? style;
        private int? countingWayId;

        public AccDocumentHeaderListViewModel AccDocumentHeaderListViewModel { get; }
        public bool IsReadOnly { get; private set; }
        public bool HasDocumentNumber { get; private set; }
        public bool HasManualDocumentNumber { get; private set; }

        private void AccDocumentItemsRadGridView_CurrentCellChanged(object sender, GridViewCurrentCellChangedEventArgs e)
        {
        }
        private void AccDocumentItemsRadGridView_CopyingCellClipboardContent(object sender, GridViewCellClipboardEventArgs e)
        {
            GridViewComboBoxColumn comboBox = (e.Cell.Column as GridViewComboBoxColumn);
            if (comboBox != null)
            {
                foreach (object item in comboBox.ItemsSource)
                {

                    PropertyInfo pi = Utility.GetProperty(item.GetType(), comboBox.DisplayMemberPath);

                    object itemValue = Utility.GetPropertyValue(pi, item);

                    if (itemValue.ToString() == e.Value.ToString())
                    {
                        PropertyInfo valueProperty = Utility.GetProperty(item.GetType(), comboBox.SelectedValueMemberPath);

                        object valuePropertyValue = Utility.GetPropertyValue(valueProperty, item);

                        e.Value = valuePropertyValue;
                    }
                }
            }
        }
        private void AccDocumentItemsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void AccDocumentItemsRadGridView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
        private void AccDocumentItemsRadGridView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (AccDocumentItemsRadGridView.CurrentCell != null)
            {
                var lastcolumnIndex = AccDocumentItemsRadGridView.Columns.Count - 1;
                var isLastColumn = AccDocumentItemsRadGridView.CurrentCell?.Column == AccDocumentItemsRadGridView.Columns[lastcolumnIndex];
                var lastRowIndex = AccDocumentItemsRadGridView.Items.Count - 1;
                var isLastRow = AccDocumentItemsRadGridView.Items.IndexOf(AccDocumentItemsRadGridView.SelectedItem) == lastRowIndex;
                var isFirstIndex = AccDocumentItemsRadGridView.CurrentCell.Column.Name == "SL" || AccDocumentItemsRadGridView.CurrentCell.Column.Name == "RowNumber"; //this.radGridView1.CurrentCell.ColumnInfo.Name



                TextBox textBox = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<TextBox>().FirstOrDefault();
                RadComboBox radComboBox = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<RadComboBox>().FirstOrDefault();
                RadDatePicker radDatePicker = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<RadDatePicker>().FirstOrDefault();
                TextBlock textblock = AccDocumentItemsRadGridView.CurrentCell.ChildrenOfType<TextBlock>().FirstOrDefault();

                var isReadOnly = AccDocumentItemsRadGridView.CurrentCell.Column.IsReadOnly;

                if (e.Key == Key.Tab)
                {
                    if (radComboBox != null)
                        radComboBox.IsDropDownOpen = false;
                    e.Handled = true;
                    RadGridViewCommands.MoveNext.Execute(null);
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers != ModifierKeys.Control)
                {
                    if (isLastColumn && isLastRow)
                    {
                        AccDocumentItemsRadGridView.BeginInsert();
                        AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
                    }
                    else if (AccDocumentItemsRadGridView.Items.IsAddingNew || AccDocumentItemsRadGridView.Items.IsEditingItem)
                    {
                        RadGridViewCommands.CommitEdit.Execute(null);
                    }
                    else
                    {
                        RadGridViewCommands.MoveNext.Execute(null);
                        RadGridViewCommands.SelectCurrentUnit.Execute(null);
                        RadGridViewCommands.BeginEdit.Execute(null);
                    }
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    if (textBox != null && textBox.Name != "PART_EditableTextBox" || radComboBox?.ItemsSource == null || (radComboBox.ItemsSource.Cast<object>()).Count() < 2 || radComboBox?.IsDropDownOpen != true)
                    {
                        RadGridViewCommands.MoveUp.Execute(null);
                        RadGridViewCommands.SelectCurrentUnit.Execute(null);
                        RadGridViewCommands.BeginEdit.Execute(null);

                        e.Handled = true;
                    }
                }

                else if (e.Key == Key.Right)
                {
                    if (!isFirstIndex && (textblock != null || textBox?.CaretIndex == 0))
                    {
                        RadGridViewCommands.MovePrevious.Execute(null);
                        RadGridViewCommands.SelectCurrentUnit.Execute(null);
                        RadGridViewCommands.BeginEdit.Execute(null);
                        e.Handled = true;
                    }
                }

                else if (e.Key == Key.Down)
                {

                    if (textBox != null && textBox.Name != "PART_EditableTextBox" || radComboBox?.ItemsSource == null || (radComboBox.ItemsSource.Cast<object>()).Count() < 1 || radComboBox?.IsDropDownOpen != true)
                    {
                        if (isLastRow)
                        {
                            RadGridViewCommands.CommitEdit.Execute(null);
                            RadGridViewCommands.BeginInsert.Execute(null);

                            AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
                            RadGridViewCommands.CommitEdit.Execute(null);
                            RadGridViewCommands.BeginEdit.Execute(null);
                        }
                        else
                        {
                            RadGridViewCommands.MoveDown.Execute(null);
                            RadGridViewCommands.SelectCurrentUnit.Execute(null);
                            RadGridViewCommands.BeginEdit.Execute(null);
                            e.Handled = true;
                        }
                    }

                }
                else if ((isLastColumn && isLastRow) || ((textblock != null || textBox != null) && e.Key == Key.Left))
                {
                    if ((isLastColumn && isLastRow) || (textblock != null || (textBox != null && textBox.CaretIndex == textBox.Text.Length)))
                    {
                        if (isLastColumn && isLastRow)
                        {
                            RadGridViewCommands.CommitEdit.Execute(null);
                            RadGridViewCommands.BeginInsert.Execute(null);

                            AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[0];
                            RadGridViewCommands.CommitEdit.Execute(null);
                            RadGridViewCommands.BeginEdit.Execute(null);
                        }
                        else
                        {
                            RadGridViewCommands.MoveNext.Execute(null);
                            RadGridViewCommands.SelectCurrentUnit.Execute(null);
                            RadGridViewCommands.BeginEdit.Execute(null);
                        }

                        e.Handled = true;
                    }
                }
                else if (radComboBox != null || radDatePicker != null)
                {
                    if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                    {
                        AccDocumentItemsRadGridView.BeginEdit();
                        if (radComboBox != null)
                            radComboBox.IsDropDownOpen = true;
                        else if (radDatePicker != null)
                            radDatePicker.IsDropDownOpen = true;
                        e.Handled = true;
                    }
                }
            }

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(47))
            {
                using (var uow = new SainaDbContext())
                {
                    var getDocumentNumbering = uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefault(x => x.AccountDocumentId == 1);
                    style = getDocumentNumbering.StyleId;
                    countingWayId = getDocumentNumbering.CountingWayId;
                }
                if (style == 1 && countingWayId == 1)
                {
                    IsReadOnly = false;

                }
                else if (style == 1 && countingWayId == 2)
                {
                    IsReadOnly = true;

                }
                else if (style == 2 && countingWayId == 1)
                {
                    IsReadOnly = false;

                }
                else if (style == 2 && countingWayId == 2)
                {
                    IsReadOnly = true;

                }
                BeginEdit();
            }
        }
        private void DataFormDataField_Loaded(object sender, RoutedEventArgs e)
        {
            // (sender as FrameworkElement).Focus();
            var dataForm = AccDocumentHeaderDataForm;
            var fields = dataForm.ChildrenOfType<DataFormDataField>();
            var field = fields.FirstOrDefault(x => x.Name == "documentNumberDataFormDataField");
            if (field != null)
            {
                var textBox = field.FindChildByType<TextBox>();
                textBox.SelectAll();
                field.Focus();
                textBox.Focus();
            }
        }
        private void AddSLButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RaiseSLRequested(SLDLEnum.SL);

        }
        private void AddDLButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RaiseSLRequested(SLDLEnum.DL);

        }

        public enum SLDLEnum
        {
            SL = 1,
            DL = 2
        }

        private void AccDocumentHeaderDataForm_LostFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadWatermarkTextBox radWatermarkTextBox)
                if (radWatermarkTextBox.Name == "WatermarkTextBox")
                {
                    if (AccDocumentItemsRadGridView.ItemsSource == null || ((ObservableCollection<AccDocumentItem>)AccDocumentItemsRadGridView.ItemsSource).Count == 0)
                        AccDocumentItemsRadGridView.BeginInsert();
                    AccDocumentItemsRadGridView.Focus();
                    AccDocumentItemsRadGridView.CurrentColumn = AccDocumentItemsRadGridView.Columns[1];
                    RadGridViewCommands.SelectCurrentUnit.Execute(null);
                }
        }
        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(58))
            {
                if (_viewModel.ContextHasChanges && detailRadTabItem.IsSelected == false && AccDocumentHeaderRadGridView.SelectedItem != null)
                {
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بله";
                    parameters.CancelButtonContent = "خیر";
                    parameters.Header = "اخطار";
                    parameters.Content = "آیا مطمئن هستید که می خواهید بدون ذخیره تغیرات، به آیتم بعدی بروید؟";
                    parameters.Closed = OnClosed;
                    RadWindow.Confirm(parameters);
                    if (_dialogResult == true)
                    {
                        _viewModel.Reject();
                        AccDocumentHeaderDataForm.CancelEdit();
                        _viewModel.DocumentNumbering();
                    }

                    else
                    {
                        var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                        var alert = new RadDesktopAlert
                        {
                            FlowDirection = FlowDirection.RightToLeft,
                            Header = "اطلاعات",
                            Content = ".لطفا قبل از شماره گذاری ، تغییرات را ذخیره نمایید",
                            ShowDuration = 1200,
                        };
                        manager.ShowAlert(alert);
                    }

                    // e.Cancel = _dialogResult == (MessageBox.Show("xxxx", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes);

                }
                else
                {
                    _viewModel.DocumentNumbering();

                }
            }
        }
        private void AccDocumentHeaderRadGridView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            var xxx = (AccDocumentHeader)_viewModel.AccDocumentHeaders.CurrentItem;
            if (xxx != null)
            {
                foreach (var item in xxx.AccDocumentItems)
                {
                    item.SL = _viewModel.GetSLById(item.SLId);
                    if (item.SL.DLType1 != 0)
                        item.DL1 = _viewModel.GetDLById(item.DL1Id.Value);
                    if (item.SL.DLType2 != 0)
                        item.DL2 = _viewModel.GetDLById(item.DL2Id.Value);
                    if (item.CurrencyId != 0 && item.CurrencyId != null)
                        item.Currency = _viewModel.GetCurrById(item.CurrencyId.Value);
                }

            }
        }
        private void AccDocumentHeaderDataForm_Loaded(object sender, RoutedEventArgs e)
        {
            //CultureInfo culutreInfo = new CultureInfo("fa-IR");
            //culutreInfo.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            //culutreInfo.DateTimeFormat.Calendar = new PersianCalendar();
            ////    var datePicker = AccDocumentHeaderDataForm.ChildrenOfType<RadDatePicker>().FirstOrDefault();
            //Thread.CurrentThread.CurrentUICulture = culutreInfo;
            //this.Culture = culutreInfo;
        }
        private void AccDocumentHeaderRadGridView_SelectedCellsChanging(object sender, GridViewSelectedCellsChangingEventArgs e)
        {

        }
        private void AccDocumentHeaderRadGridView_SelectionChanging(object sender, SelectionChangingEventArgs e)
        {
            //if (_viewModel.ContextHasChanges && AccDocumentHeaderRadGridView.IsLoaded)
            //{

            //        //DialogParameters parameters = new DialogParameters();
            //        //parameters.OkButtonContent = "بله";
            //        //parameters.CancelButtonContent = "خیر";
            //        //parameters.Header = "اخطار";
            //        //parameters.Content = "آیا مطمئن هستید که می خواهید بدون ذخیره تغیرات، به آیتم بعدی بروید؟";
            //        //parameters.Closed = OnClosed;
            //        //RadWindow.Confirm(parameters);
            if (_viewModel.ContextHasChanges && detailRadTabItem.IsSelected == false && AccDocumentHeaderRadGridView.SelectedItem != null)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بله";
                parameters.CancelButtonContent = "خیر";
                parameters.Header = "اخطار";
                parameters.Content = "آیا مطمئن هستید که می خواهید بدون ذخیره تغیرات، به آیتم بعدی بروید؟";
                parameters.Closed = OnClosed;
                RadWindow.Confirm(parameters);
                e.Cancel = _dialogResult == true;
                if (_dialogResult == true)
                {
                    _viewModel.Reject();
                    AccDocumentHeaderDataForm.CancelEdit();
                }

                else
                {
                    e.Cancel = true;
                    ;
                }

                // e.Cancel = _dialogResult == (MessageBox.Show("xxxx", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes);

            }

            //}
        }
        private void SLStandardDescriptionsRadComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AccDocumentItemsRadGridView.SelectedItem is AccDocumentItem accDocumentItem)
                ((RadComboBox)sender).Text = accDocumentItem.Description1;

        }
        private void SLStandardDescriptionsRadComboBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AccDocumentItemsRadGridView.SelectedItem is AccDocumentItem accDocumentItem)
                ((RadComboBox)sender).Text = accDocumentItem.Description2;

        }
        private void DataFormDateField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void PrintReportAll()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var headerIds = ((ICollectionView)(AccDocumentHeaderRadGridView.ItemsSource)).SourceCollection.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
                var data = uow.AccDocumentItems.Include(x => x.AccDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL.TL).Where(x => headerIds.Contains(x.AccDocumentHeaderId))
                                     .ToList()
                                    .Select(accDocumentItem => new
                                    {
                                        HeaderDescription = accDocumentItem.AccDocumentHeader.HeaderDescription,
                                        ManualDocumentNumber = accDocumentItem.AccDocumentHeader.ManualDocumentNumber,
                                        Seconder = accDocumentItem.AccDocumentHeader.Seconder,
                                        Status = accDocumentItem.AccDocumentHeader.Status,
                                        SumDebit = accDocumentItem.AccDocumentHeader.SumDebit,
                                        SystemFixNumber = accDocumentItem.AccDocumentHeader.SystemFixNumber,
                                        SystemName = accDocumentItem.AccDocumentHeader.SystemName,
                                        TypeDocumentTitle = accDocumentItem.AccDocumentHeader.TypeDocument?.TypeDocumentTitle,
                                        DocumentNumber = accDocumentItem.AccDocumentHeader.DocumentNumber,
                                        DocumentDate = accDocumentItem.AccDocumentHeader.DocumentDate.ToShortDateString(),
                                        DailyNumber = accDocumentItem.AccDocumentHeader.DailyNumber,
                                        ItemDebit = accDocumentItem.Debit,
                                        Credit = accDocumentItem.Credit,
                                        CurrencyTitle = accDocumentItem.Currency?.CurrencyTitle,
                                        CurrencyAmount = accDocumentItem.CurrencyAmount,
                                        Description1 = accDocumentItem.Description1,
                                        Description2 = accDocumentItem.Description2,
                                        DLCode1 = accDocumentItem.DL1?.DLCode,
                                        DLCode2 = accDocumentItem.DL2?.DLCode,
                                        DLTitle1 = accDocumentItem.DL1?.Title,
                                        DLTitle2 = accDocumentItem.DL2?.Title,
                                        SLTitle = accDocumentItem.SL.Title,
                                        SLCode = accDocumentItem.SL.SLCode,
                                        TLCode = accDocumentItem.SL.TL.TLCode,
                                        TLTitle = accDocumentItem.SL.TL.Title,
                                        TLTitle2 = accDocumentItem.SL.TL.Title2,
                                        TrackingDate = accDocumentItem.TrackingDate,
                                        TrackingNumber = accDocumentItem.TrackingNumber,
                                        DescriptionItem1 = accDocumentItem.Description1,
                                        DescriptionItem2 = accDocumentItem.Description2,
                                    })
                                   ;
                report.RegBusinessObject("AccHeaderItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accHeaderItemReport.mrt");
                report.Print();
            }
        }
        private void ShowReportAll()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var headerIds = ((ICollectionView)(AccDocumentHeaderRadGridView.ItemsSource)).SourceCollection.Cast<AccDocumentHeader>().Select(x => x.AccDocumentHeaderId);
                var data = uow.AccDocumentItems.Include(x => x.AccDocumentHeader).Include(x => x.SL).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL.TL).Where(x => headerIds.Contains(x.AccDocumentHeaderId))
                                     .ToList()
                                    .Select(accDocumentItem => new
                                    {
                                        HeaderDescription = accDocumentItem.AccDocumentHeader.HeaderDescription,
                                        ManualDocumentNumber = accDocumentItem.AccDocumentHeader.ManualDocumentNumber,
                                        Seconder = accDocumentItem.AccDocumentHeader.Seconder,
                                        Status = accDocumentItem.AccDocumentHeader.Status,
                                        SumDebit = accDocumentItem.AccDocumentHeader.SumDebit,
                                        SystemFixNumber = accDocumentItem.AccDocumentHeader.SystemFixNumber,
                                        SystemName = accDocumentItem.AccDocumentHeader.SystemName,
                                        TypeDocumentTitle = accDocumentItem.AccDocumentHeader.TypeDocument?.TypeDocumentTitle,
                                        DocumentNumber = accDocumentItem.AccDocumentHeader.DocumentNumber,
                                        DocumentDate = accDocumentItem.AccDocumentHeader.DocumentDate.ToShortDateString(),
                                        DailyNumber = accDocumentItem.AccDocumentHeader.DailyNumber,
                                        ItemDebit = accDocumentItem.Debit,
                                        Credit = accDocumentItem.Credit,
                                        CurrencyTitle = accDocumentItem.Currency?.CurrencyTitle,
                                        CurrencyAmount = accDocumentItem.CurrencyAmount,
                                        Description1 = accDocumentItem.Description1,
                                        Description2 = accDocumentItem.Description2,
                                        DLCode1 = accDocumentItem.DL1?.DLCode,
                                        DLCode2 = accDocumentItem.DL2?.DLCode,
                                        DLTitle1 = accDocumentItem.DL1?.Title,
                                        DLTitle2 = accDocumentItem.DL2?.Title,
                                        SLTitle = accDocumentItem.SL.Title,
                                        SLCode = accDocumentItem.SL.SLCode,
                                        TLCode = accDocumentItem.SL.TL.TLCode,
                                        TLTitle = accDocumentItem.SL.TL.Title,
                                        TLTitle2 = accDocumentItem.SL.TL.Title2,
                                        TrackingDate = accDocumentItem.TrackingDate,
                                        TrackingNumber = accDocumentItem.TrackingNumber,
                                        DescriptionItem1 = accDocumentItem.Description1,
                                        DescriptionItem2 = accDocumentItem.Description2,
                                    })
                                   ;
                report.RegBusinessObject("AccHeaderItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accHeaderItemReport.mrt");
                report.Show();
            }
        }
        private void ListBoxItemReport_Selected(object sender, RoutedEventArgs e)
        {
            ShowReport();

            ReportMenu.IsOpen = false;
        }
        private void ListBoxItemReportALL_Selected(object sender, RoutedEventArgs e)
        {
            ShowReportAll();
            //reportList1.IsSelected = false;
            //reportList2.IsSelected = false;
            ReportMenu.IsOpen = false;

        }
        private void ListBoxItemPrintALL_Selected(object sender, RoutedEventArgs e)
        {
            PrintReportAll();
            PrintMenu.IsOpen = false;
        }
        private void ListBoxItemPrint_Selected(object sender, RoutedEventArgs e)
        {
            PrintReport();
            PrintMenu.IsOpen = false;

        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadRefresh();
            _viewModel.TypeEnum = 0;
        }
        private ObservableCollection<AccDocumentItem> GetAccDocumnetItems(ObservableCollection<object> selectedAccDocumnetItems)
        {
            ObservableCollection<AccDocumentItem> result = new ObservableCollection<AccDocumentItem>();

            foreach (AccDocumentItem item in selectedAccDocumnetItems)
            {
                if (item.SLId != 0)
                {
                    result.Add(new AccDocumentItem()
                    {
                        Debit = item.Debit,
                        Description1 = item.Description1,
                        Credit = item.Credit,
                        Description2 = item.Description2,
                        DL1 = item.DL1,
                        DL2 = item.DL2,
                        DL1Id = item.DL1Id,
                        DL2Id = item.DL2Id,
                        SL = item.SL,
                        SLId = item.SLId,
                        CurrencyAmount = item.CurrencyAmount,
                        Currency = item.Currency,
                        CurrencyId = item.CurrencyId,
                        Handled = item.Handled,
                        TrackingNumber = item.TrackingNumber,
                        TrackingDate = item.TrackingDate
                    });
                }
            }

            return result;
        }
        //private ObservableCollection<EditableAccItem> GetAccDocumnetItems1(ObservableCollection<object> selectedAccDocumnetItems)
        //{
        //    ObservableCollection<EditableAccItem> result = new ObservableCollection<EditableAccItem>();

        //    foreach (AccDocumentItem item in selectedAccDocumnetItems)
        //    {
        //        if (item.SLId != 0)
        //        {
        //            result.Add(new EditableAccItem()
        //            {
        //                Debit = item.Debit,
        //                Description1 = item.Description1,
        //                Credit = item.Credit,
        //                Description2 = item.Description2,
        //                DL1Id = item.DL1,
        //                DL2Id = item.DL2,
        //                DL1Id = item.DL1Id,
        //                DL2Id = item.DL2Id,
        //                SL = item.SL,
        //                SLId = item.SLId,
        //                CurrencyAmount = item.CurrencyAmount,
        //                Currency = item.Currency,
        //                CurrencyId = item.CurrencyId,
        //                Handled = item.Handled,
        //                TrackingNumber = item.TrackingNumber,
        //                TrackingDate = item.TrackingDate
        //            });
        //        }
        //    }

        //    return result;
        //}



        private void Currency_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (AccDocumentItemsRadGridView.SelectedItem is AccDocumentItem accDocumentItem)
            //    ((RadComboBox)sender).Text = accDocumentItem.Currency.CurrencyTitle;
        }

        private void Currency_DropDownOpened(object sender, EventArgs e)
        {
          
            _viewModel.AccDocumentItem = ((RadComboBox)sender).DataContext as AccDocumentItem;
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(((RadComboBox)sender).ItemsSource);
            
                itemsViewOriginal.Filter = ((x) =>
                          {
                             
                              return x!= null && _viewModel.AccDocumentItem.SL != null ? _viewModel.AccDocumentItem.SL.Property==PropertyEnum.Consistency : false;
                          });
           
            itemsViewOriginal.Refresh();
        }

        private void EndItemsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(52))
            {// ObservableCollection<AccDocumentHeader> accDocumentHeaders = new ObservableCollection<AccDocumentHeader>();

                ObservableCollection<object> accDocumentHeaders = AccDocumentHeaderRadGridView.SelectedItems;
                var count = 0;
                var list = AccDocumentHeaderRadGridView.SelectedItems;
                foreach (AccDocumentHeader item in accDocumentHeaders)
                {
                    if (item.Status == StatusEnum.Temporary)
                    {
                        item.Status = StatusEnum.End;
                        item.Seconder = _appContextService.CurrentUser.FriendlyName;
                        count++;
                    }
                }
                CommitAndBeginEdit();
                RaiseCanExecuteChanged();
                if (count > 0)
                {
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".خاتمه انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                }
                else
                {

                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = "سند پیش نویس نمی تواند خاتمه  یابد.",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                }
            }
        }
        public static class Utility
        {
            public static PropertyInfo GetProperty(Type objectType, string propertyName)
            {
                return objectType.GetProperty(propertyName);
            }
            public static object GetPropertyValue(PropertyInfo property, object objectToQuery)
            {
                if (property == null || objectToQuery == null)
                    return null;

                return property.GetValue(objectToQuery, null);
            }
        }

        private void BackEndItemsButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (_accessUtility.HasAccess(53))
            {
                var count = 0;
                ObservableCollection<object> accDocumentHeaders = AccDocumentHeaderRadGridView.SelectedItems;
                var list = AccDocumentHeaderRadGridView.SelectedItems;
                foreach (AccDocumentHeader item in accDocumentHeaders)
                {
                    if (item.Status == StatusEnum.End)
                    {
                        item.Status = StatusEnum.Temporary;
                        item.Seconder = _appContextService.CurrentUser.FriendlyName;
                        count++;
                    }
                }
                // AccDocumentHeader.Status = StatusEnum.Temporary;
                CommitAndBeginEdit();
                isModified = true;
                RaiseCanExecuteChanged();
                if (count > 0)
                {
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".برگشت خاتمه انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                }
                else
                {

                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = "برای برگشت باید سند حتما خاتمه باشد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                }
            }
        }
        private bool OnDeleteItem()
        {
            AccDocumentHeader1 = AccDocumentHeader;
            var r = _viewModel.HasAcc(AccDocumentHeader.DocumentDate, AccDocumentHeader.TypeDocument, AccDocumentHeader.Status);
            var message = "";
            if (r == 2)
            {
                var status = AccDocumentHeader.Status;
                if (status == StatusEnum.End)
                {
                    message = "خاتمه";
                }
                if (status == StatusEnum.Permanent)
                {
                    message = "دائم";
                }
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "اخطار";
                parameters.Content = "امکان حذف سند به علت " + message + "وجود ندارد";

                RadWindow.Alert(parameters);
                return false;
            }
            else
            {

                if (r == 0)
                {

                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بله، مطمئنم";
                    parameters.CancelButtonContent = "خیر";
                    parameters.Header = "اخطار";
                    parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                    parameters.Closed = OnClosed;
                    RadWindow.Confirm(parameters);
                    return _dialogResult == true;
                    // _dialogResult == true;
                }
                else
                {

                    if (r == 1)
                    {
                        message = "سود و زیانی";
                    }
                    else if (r == 3)
                    {
                        message = "افتتاحیه";
                    }
                    else if (r == 4)
                    {
                        message = "اختتامیه";
                    }
                    else if (r == 5)
                    {
                        message = "سند کل";
                    }
                    else if (r == 6)
                    {
                        message = "تسعیر ارز";
                    }
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "اخطار";
                    parameters.Content = "امکان حذف سند به علت صدور سند" + message + "وجود ندارد";
                    // parameters.Closed = OnClosed;
                    RadWindow.Alert(parameters);
                    return false;
                }

            }

        }


        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

            var hasdel = OnDeleteItem();
            AccDocumentItemsRadGridView.SelectedItem = ((Button)sender).DataContext;

            if (AccDocumentItemsRadGridView.CurrentItem is AccDocumentItem accDocumentItem)
            {
                if (hasdel == true)
                {
                    _viewModel.DeleteItem(accDocumentItem);
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = "حذف با موفقیت انجام شد",
                        ShowDuration = 1200,
                    };
                    manager.ShowAlert(alert);
                    //  _viewModel.Load(AccDocumentHeader);
                    if (_viewModel.HasHeaderItem(AccDocumentHeader) == false)
                    {
                        _viewModel.DeleteDocumentHeader(AccDocumentHeader);
                    }
                }
            }
        }

        private void AccDocumentItemsRadGridViewContextMenu_Opening(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            //if (clipboardAccDocumentItems.Count == 0)
            //{
            //    Paste.IsEnabled = false;
            //}
        }

        private void ReportMenu_Click(object sender, RoutedEventArgs e)
        {
            reportList.SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(56))
            {
            }
            }

      
        //private void dateDoc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{

        //    if (e.Text == "/")
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        Regex regex = new Regex("[^0-9]+");
        //        e.Handled = regex.IsMatch(e.Text);
        //    }
        //}
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
}
