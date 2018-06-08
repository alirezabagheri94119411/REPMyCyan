using Microsoft.Win32;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Saina.WPF.Behaviors;
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
using Telerik.Windows.Persistence.Storage;

namespace Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo
{
    /// <summary>
    /// Interaction logic for CurrencyExchangeListView.xaml
    /// </summary>
    public partial class CurrencyExchangeListView : UserControl
    {
        private AccessUtility _accessUtility;
        private CurrencyExchangeListViewModel _viewModel;
        private IsolatedStorageProvider isolatedStorageProvider;

        public CurrencyExchangeListView()
        {
          //  isolatedStorageProvider = new IsolatedStorageProvider();

            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as CurrencyExchangeListViewModel;
                RadGridView1.SelectedItem = null;
                _viewModel.Error += OnError;
                _viewModel.Information += OnInformation;

             //   isolatedStorageProvider.LoadFromStorage();
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Error -= OnError;
                _viewModel.Information -= OnInformation;

              //  isolatedStorageProvider.SaveToStorage();
            };
        }
        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void OnInformation(string information)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اطلاعات";
            parameters.Content = information;
            RadWindow.Alert(parameters);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentDateTextBox.Focus();
        }

        private void descriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (RadGridView1.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (RadGridView1.SelectedItems.Any())
                        data = RadGridView1.SelectedItems.Cast<AccDocumentItemGroupedDTO>()
                        .Select(accDocumentItemGroupedDTO => new
                        {
                            accDocumentItemGroupedDTO.CurrencyTitle,
                            accDocumentItemGroupedDTO.DL1Code,
                            accDocumentItemGroupedDTO.DL2Code,
                            accDocumentItemGroupedDTO.DLTitle1,
                            accDocumentItemGroupedDTO.DLTitle2,
                            accDocumentItemGroupedDTO.DocumentDate,
                            accDocumentItemGroupedDTO.ParityRate,
                            accDocumentItemGroupedDTO.SLCode,
                            accDocumentItemGroupedDTO.SLTitle,
                            accDocumentItemGroupedDTO.SumCredit,
                            accDocumentItemGroupedDTO.SumCurrencyAmount,
                            accDocumentItemGroupedDTO.SumDebit,
                            accDocumentItemGroupedDTO.TrackingNumber,
                        }).ToList();
                    else
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemGroupedDTO>()
                             .Select(accDocumentItemGroupedDTO => new
                             {
                                 accDocumentItemGroupedDTO.CurrencyTitle,
                                 accDocumentItemGroupedDTO.DL1Code,
                                 accDocumentItemGroupedDTO.DL2Code,
                                 accDocumentItemGroupedDTO.DLTitle1,
                                 accDocumentItemGroupedDTO.DLTitle2,
                                 accDocumentItemGroupedDTO.DocumentDate,
                                 accDocumentItemGroupedDTO.ParityRate,
                                 accDocumentItemGroupedDTO.SLCode,
                                 accDocumentItemGroupedDTO.SLTitle,
                                 accDocumentItemGroupedDTO.SumCredit,
                                 accDocumentItemGroupedDTO.SumCurrencyAmount,
                                 accDocumentItemGroupedDTO.SumDebit,
                                 accDocumentItemGroupedDTO.TrackingNumber,
                             }).ToList();

                    }
                    report.RegBusinessObject("CurrencyExchangeReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt");
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
                var data = RadGridView1.SelectedItems.Cast<AccDocumentItemGroupedDTO>()
                         .Select(accDocumentItemGroupedDTO => new
                         {
                             accDocumentItemGroupedDTO.CurrencyTitle,
                             accDocumentItemGroupedDTO.DL1Code,
                             accDocumentItemGroupedDTO.DL2Code,
                             accDocumentItemGroupedDTO.DLTitle1,
                             accDocumentItemGroupedDTO.DLTitle2,
                             accDocumentItemGroupedDTO.DocumentDate,
                             accDocumentItemGroupedDTO.ParityRate,
                             accDocumentItemGroupedDTO.SLCode,
                             accDocumentItemGroupedDTO.SLTitle,
                             accDocumentItemGroupedDTO.SumCredit,
                             accDocumentItemGroupedDTO.SumCurrencyAmount,
                             accDocumentItemGroupedDTO.SumDebit,
                             accDocumentItemGroupedDTO.TrackingNumber,
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
                         }).ToList();
                var path = $"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt";
                // var path = $"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt";
                report.RegBusinessObject("CurrencyExchangeReport", data);
                if (!File.Exists(path))
                    File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
           report.Load(path);
                report.Design();
            }
        }
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = RadGridView1.SelectedItems.Cast<AccDocumentItemGroupedDTO>()
                       .Select(accDocumentItemGroupedDTO => new
                       {
                           accDocumentItemGroupedDTO.CurrencyTitle,
                           accDocumentItemGroupedDTO.DL1Code,
                           accDocumentItemGroupedDTO.DL2Code,
                           accDocumentItemGroupedDTO.DLTitle1,
                           accDocumentItemGroupedDTO.DLTitle2,
                           accDocumentItemGroupedDTO.DocumentDate,
                           accDocumentItemGroupedDTO.ParityRate,
                           accDocumentItemGroupedDTO.SLCode,
                           accDocumentItemGroupedDTO.SLTitle,
                           accDocumentItemGroupedDTO.SumCredit,
                           accDocumentItemGroupedDTO.SumCurrencyAmount,
                           accDocumentItemGroupedDTO.SumDebit,
                           accDocumentItemGroupedDTO.TrackingNumber,
                                                 }).ToList();
                report.RegBusinessObject("CurrencyExchangeReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt");
                report.Print();
            }
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
                var col0Visibility = RadGridView1.Columns[0].IsVisible;
                RadGridView1.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    RadGridView1.Export(stream,
                     opt);
                }
                RadGridView1.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }

       
      //step3
        private void CurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(62))
            {
                _viewModel.RaiseTestRequested(TypeEnum.Litter);
            }
        }
        private void ShowReportAll()
        {
            if (RadGridView1.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (RadGridView1.SelectedItems.Any())
                       
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemGroupedDTO>()
                             .Select(accDocumentItemGroupedDTO => new
                             {
                                 accDocumentItemGroupedDTO.CurrencyTitle,
                                 accDocumentItemGroupedDTO.DL1Code,
                                 accDocumentItemGroupedDTO.DL2Code,
                                 accDocumentItemGroupedDTO.DLTitle1,
                                 accDocumentItemGroupedDTO.DLTitle2,
                                 accDocumentItemGroupedDTO.DocumentDate,
                                 accDocumentItemGroupedDTO.ParityRate,
                                 accDocumentItemGroupedDTO.SLCode,
                                 accDocumentItemGroupedDTO.SLTitle,
                                 accDocumentItemGroupedDTO.SumCredit,
                                 accDocumentItemGroupedDTO.SumCurrencyAmount,
                                 accDocumentItemGroupedDTO.SumDebit,
                                 accDocumentItemGroupedDTO.TrackingNumber,
                             }).ToList();

                   
                    report.RegBusinessObject("CurrencyExchangeReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt");
                    report.Show();
                }
                }
            }
        }
        private void PrintReportAll()
        {
            if (RadGridView1.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (RadGridView1.SelectedItems.Any())

                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemGroupedDTO>()
                             .Select(accDocumentItemGroupedDTO => new
                             {
                                 accDocumentItemGroupedDTO.CurrencyTitle,
                                 accDocumentItemGroupedDTO.DL1Code,
                                 accDocumentItemGroupedDTO.DL2Code,
                                 accDocumentItemGroupedDTO.DLTitle1,
                                 accDocumentItemGroupedDTO.DLTitle2,
                                 accDocumentItemGroupedDTO.DocumentDate,
                                 accDocumentItemGroupedDTO.ParityRate,
                                 accDocumentItemGroupedDTO.SLCode,
                                 accDocumentItemGroupedDTO.SLTitle,
                                 accDocumentItemGroupedDTO.SumCredit,
                                 accDocumentItemGroupedDTO.SumCurrencyAmount,
                                 accDocumentItemGroupedDTO.SumDebit,
                                 accDocumentItemGroupedDTO.TrackingNumber,
                             }).ToList();


                        report.RegBusinessObject("CurrencyExchangeReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\currencyExchangeReport.mrt");
                        report.Print();
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

        private void DocumentDateTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            var t = (Microsoft.Windows.Controls.DatePicker)e.Source;
            if (t.Text.Length == 10 && e.Text.Length > 0)
            {
                e.Handled = true;
            }
           
        }
    }
    //step4
    public enum TypeEnum
    {
        mynull=0,
        Litter=6,
        Profits=1,
        Opening=3,
         Closing=4
    }
}
