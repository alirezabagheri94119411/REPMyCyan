using Microsoft.Win32;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
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
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;
using Telerik.Windows.Persistence.Storage;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    /// <summary>
    /// Interaction logic for ReviewAccountListView.xaml
    /// </summary>
    public partial class ReviewAccountListView : UserControl
    {
        private ReviewAccountListViewModel _viewModel;
        private IsolatedStorageProvider isolatedStorageProvider;

        System.IO.Stream stream;
        public ReviewAccountListView()
        {
           // isolatedStorageProvider = new IsolatedStorageProvider();
            InitializeComponent();
            //ServiceProvider.RegisterPersistenceProvider<ICustomPropertyProvider>(typeof(RadGridView), new GridViewCustomPropertyProvider());
            //this.EnsureLoadState();
            Loaded += (s, e) =>
            {
                _viewModel = DataContext as ReviewAccountListViewModel;
                reviewRadGridView.SelectedItem = null;

               // isolatedStorageProvider.LoadFromStorage();
            };

            Unloaded += (s, e) =>
            {
            //    isolatedStorageProvider.SaveToStorage();
            };
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            //GLGroupedRadRadioButton.IsChecked = true;
            _viewModel.ApplyFilterCommand.Execute(null);
        }
        private void RadGridView10_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //DocumntsFlowWindow documntsFlowWindow = new DocumntsFlowWindow(reviewRadGridView.SelectedItem as AccDocumentItemDTO, (DataContext as ReviewAccountListViewModel)?.AccDocumentHeaderFilter?.CurrentMethodName);
            //documntsFlowWindow.Width = 1024;
            //documntsFlowWindow.Height = 500;
            //documntsFlowWindow.CanClose = true;
            //documntsFlowWindow.TestRequested += (headerId) =>
            //{
            //    _viewModel.RaiseTestRequested(headerId);
            //};
            //documntsFlowWindow.Owner = Window.GetWindow(this);
            //documntsFlowWindow.Show();

            //
            if (reviewRadGridView.SelectedItem is AccDocumentItemDTO accDocumentItem)
            {
                _viewModel.Raisedetail(accDocumentItem, (DataContext as ReviewAccountListViewModel)?.AccDocumentHeaderFilter?.CurrentMethodName);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //startTextBox.Focus();
            //startTextBox.CaretIndex = 1000;
        }

        private void startTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (reviewRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (reviewRadGridView.SelectedItems.Any())
                        data = reviewRadGridView.SelectedItems.Cast<AccDocumentItemDTO>().Select(i =>
                         new
                         {
                             Code = i.Code,
                             OpeningSumDebit = i.OpeningSumDebit,
                             OpeningSumCredit = i.OpeningSumCredit,
                             RemainDebit = i.RemainDebit,
                             RemainCredit = i.RemainCredit,
                             SumCredit = i.SumCredit,
                             SumDebit = i.SumDebit,
                             Title = i.Title,
                             FromDate= _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
                             ToDate= _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

                         }
                          ).ToList();
                    else
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemDTO>()
                           .Select(i =>
            new
            {
                Code = i.Code,
                OpeningSumDebit = i.OpeningSumDebit,
                OpeningSumCredit = i.OpeningSumCredit,
                RemainDebit = i.RemainDebit,
                RemainCredit = i.RemainCredit,
                SumCredit = i.SumCredit,
                SumDebit = i.SumDebit,
                Title = i.Title,
                FromDate = _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
                ToDate = _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

            }
).ToList();
                    }
                    report.RegBusinessObject("ReviewReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\reviewReport.mrt");
                    report.Show();
                }
            }
        }


        private void designButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }

        private void DesignReport()
        {
            StiReport report = new StiReport();
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
            var data = reviewRadGridView.SelectedItems.Cast<AccDocumentItemDTO>().Select(i =>
          new
          {
              Code = i.Code,
              OpeningSumDebit = i.OpeningSumDebit,
              OpeningSumCredit = i.OpeningSumCredit,
              RemainDebit = i.RemainDebit,
              RemainCredit = i.RemainCredit,
              SumCredit = i.SumCredit,
              SumDebit = i.SumDebit,
              Title = i.Title,
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
              FromDate = _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
              ToDate = _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

          }
           ).ToList();
            var path = $"{Environment.CurrentDirectory}\\Report\\reviewReport.mrt";
            report.RegBusinessObject("ReviewReport", data);
            if (!File.Exists(path))
                File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
            report.Load(path);
            report.Design();

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }

        private void PrintReport()
        {
            StiReport report = new StiReport();
            var data = reviewRadGridView.SelectedItems.Cast<AccDocumentItemDTO>().Select(i =>
            new
            {
                Code = i.Code,

                OpeningSumDebit = i.OpeningSumDebit,
                OpeningSumCredit = i.OpeningSumCredit,
                RemainDebit = i.RemainDebit,
                RemainCredit = i.RemainCredit,
                SumCredit = i.SumCredit,
                SumDebit = i.SumDebit,
                Title = i.Title,
                FromDate = _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
                ToDate = _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

            }
             ).ToList();
            report.RegBusinessObject("ReviewReport", data);
            report.Load($"{Environment.CurrentDirectory}\\Report\\reviewReport.mrt");
            report.Print();
        }

        private void reviewRadGridView_DataLoaded(object sender, EventArgs e)
        {
            reviewRadGridView.Columns[1].IsVisible = ((TextBlock)reviewRadGridView.Columns[1].Header).Text.ToString() != "ارز";
            //reviewRadGridView.Columns[2].IsVisible = ((TextBlock)reviewRadGridView.Columns[1].Header).Text.ToString() != "شماره پیگیری";
            if (((TextBlock)reviewRadGridView.Columns[1].Header).Text.ToString() != "شماره پیگیری")
                reviewRadGridView.Columns[2].Header = "عنوان";
            else
                reviewRadGridView.Columns[2].Header = "تاریخ پیگیری";



            //  reviewRadGridView.Columns[3].IsVisible = ((TextBlock)reviewRadGridView.Columns[3].Header).Text.ToString() != "تاریخ پیگیری";

            //reviewRadGridView.Columns[1].IsVisible =reviewRadGridView.Columns[1].Header.ToString() != "ارز";
            //reviewRadGridView.Columns[2].IsVisible = reviewRadGridView.Columns[1].Header.ToString() != "شماره پیگیری";

        }

        private bool CanLoad()
        {
            return this.stream != null && this.stream.Length > 0;
        }

        private void EnsureLoadState()
        {
            undoRadRadioButton.IsEnabled = CanLoad();
            //this.buttonLoad.IsEnabled = this.CanLoad();
        }

        private void GLGroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.GLGroupedCommand.CanExecute(null))
            {
                _viewModel.GLGroupedCommand.Execute(null);
                //OnSave();
            }
        }

        private void TLGroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.TLGroupedCommand.CanExecute(null))
            {
                _viewModel.TLGroupedCommand.Execute(null);
                //OnSave();
            }
        }

        private void SLGroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SLGroupedCommand.CanExecute(null))
                _viewModel.SLGroupedCommand.Execute(null);
        }

        private void DL1GroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DL1GroupedCommand.CanExecute(null))
                _viewModel.DL1GroupedCommand.Execute(null);
        }

        private void DL2GroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DL2GroupedCommand.CanExecute(null))
                _viewModel.DL2GroupedCommand.Execute(null);
        }

        private void CurrencyGroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrencyGroupedCommand.CanExecute(null))
                _viewModel.CurrencyGroupedCommand.Execute(null);
        }

        private void TrackingGroupedRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.TrackingGroupedCommand.CanExecute(null))
                _viewModel.TrackingGroupedCommand.Execute(null);
        }

        private void OnSave()
        {
            PersistenceManager manager = new PersistenceManager();
            this.stream = manager.Save(this.reviewRadGridView);
            this.EnsureLoadState();
        }

        private void OnLoad()
        {
            this.stream.Position = 0L;
            PersistenceManager manager = new PersistenceManager();
            manager.Load(this.reviewRadGridView, this.stream);
            this.EnsureLoadState();
        }

        private void UndoRadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            //if (_viewModel.UndoCommand.CanExecute(null))
            //    _viewModel.UndoCommand.Execute(null);
            //OnLoad();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
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
                var col0Visibility = reviewRadGridView.Columns[0].IsVisible;
                reviewRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    reviewRadGridView.Export(stream,
                     opt);
                }
                reviewRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        private void PrintReportAll()
        {
            if (reviewRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (reviewRadGridView.SelectedItems.Any())

                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemDTO>()
                           .Select(i =>
            new
            {
                Code = i.Code,
                OpeningSumDebit = i.OpeningSumDebit,
                OpeningSumCredit = i.OpeningSumCredit,
                RemainDebit = i.RemainDebit,
                RemainCredit = i.RemainCredit,
                SumCredit = i.SumCredit,
                SumDebit = i.SumDebit,
                Title = i.Title,
                FromDate = _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
                ToDate = _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

            }).ToList();

                        report.RegBusinessObject("ReviewReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\reviewReport.mrt");
                        report.Print();
                    }
                }
            }
        }
        private void ShowReportAll()
        {
            if (reviewRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (reviewRadGridView.SelectedItems.Any())

                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemDTO>()
                           .Select(i =>
            new
            {
                Code = i.Code,
                OpeningSumDebit = i.OpeningSumDebit,
                OpeningSumCredit = i.OpeningSumCredit,
                RemainDebit = i.RemainDebit,
                RemainCredit = i.RemainCredit,
                SumCredit = i.SumCredit,
                SumDebit = i.SumDebit,
                Title = i.Title,
                FromDate = _viewModel.AccDocumentHeaderFilter.FromDate.ToShortDateString(),
                ToDate = _viewModel.AccDocumentHeaderFilter.ToDate.ToShortDateString(),

            }).ToList();
                  
                    report.RegBusinessObject("ReviewReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\reviewReport.mrt");
                    report.Show();
                }
                }
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

        private void dateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var t = (Microsoft.Windows.Controls.DatePicker)e.Source;
            if (t.Text.Length == 10 && e.Text.Length > 0)
            {
                e.Handled = true;
            }
        }
    }
}
