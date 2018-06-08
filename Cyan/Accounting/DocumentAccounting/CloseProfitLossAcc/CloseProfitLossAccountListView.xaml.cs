using Microsoft.Win32;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.Data.Context;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
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

namespace Saina.WPF.Accounting.DocumentAccounting.CloseProfitLossAcc
{
    /// <summary>
    /// Interaction logic for CloseAccountListView.xaml
    /// </summary>
    public partial class CloseProfitLossAccountListView : UserControl
    {
        private AccessUtility _accessUtility;
        private CloseProfitLossAccountListViewModel _viewModel;
        private IsolatedStorageProvider isolatedStorageProvider;

        public CloseProfitLossAccountListView()
        {
            isolatedStorageProvider = new IsolatedStorageProvider();

            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as CloseProfitLossAccountListViewModel;
                RadGridView1.SelectedItem = null;
                _viewModel.Error += OnError;
                _viewModel.Information += OnInformation;

                isolatedStorageProvider.LoadFromStorage();
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Error -= OnError;
                _viewModel.Information -= OnInformation;

                isolatedStorageProvider.SaveToStorage();
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
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
           ((ICommand) _viewModel.TransferCommand).Execute(RadGridView1.SelectedItems);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ((ICommand)_viewModel.ReturnCommand).Execute(RadGridView2.SelectedItems);

        }

        private void AllTransferButton_Click(object sender, RoutedEventArgs e)
        {
            ((ICommand)_viewModel.AllTransferCommand).Execute(RadGridView1.SelectedItems);

        }

        private void AllReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ((ICommand)_viewModel.AllReturnCommand).Execute(RadGridView2.SelectedItems);

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AccHeaderDateTextbox.Focus();
           

        }

        private void descriptTextBox_GotFocus(object sender, RoutedEventArgs e)
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
                        data = RadGridView1.SelectedItems.Cast<AccDocumentItemOpenCloseDTO>()
                         .Select(accDocumentItemOpenCloseDTO => new
                         {
                             accDocumentItemOpenCloseDTO.CurrencyId,
                             accDocumentItemOpenCloseDTO.DL1Code,
                             accDocumentItemOpenCloseDTO.DL2Code,
                             accDocumentItemOpenCloseDTO.DLTitle1,
                             accDocumentItemOpenCloseDTO.DLTitle2,
                             accDocumentItemOpenCloseDTO.SLCode,
                             accDocumentItemOpenCloseDTO.SLTitle,
                             accDocumentItemOpenCloseDTO.SumCredit,
                             accDocumentItemOpenCloseDTO.SumCurrencyAmount,
                             accDocumentItemOpenCloseDTO.SumDebit,
                         }).ToList();
                    else
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemOpenCloseDTO>()
                               .Select(accDocumentItemOpenCloseDTO => new
                               {
                                   accDocumentItemOpenCloseDTO.CurrencyId,
                                   accDocumentItemOpenCloseDTO.DL1Code,
                                   accDocumentItemOpenCloseDTO.DL2Code,
                                   accDocumentItemOpenCloseDTO.DLTitle1,
                                   accDocumentItemOpenCloseDTO.DLTitle2,
                                   accDocumentItemOpenCloseDTO.SLCode,
                                   accDocumentItemOpenCloseDTO.SLTitle,
                                   accDocumentItemOpenCloseDTO.SumCredit,
                                   accDocumentItemOpenCloseDTO.SumCurrencyAmount,
                                   accDocumentItemOpenCloseDTO.SumDebit,
                               }).ToList();

                    }
                    report.RegBusinessObject("AccDocumentItemOpenCloseDTOReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemOpenCloseDTOReport.mrt");
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
                var data = RadGridView1.SelectedItems.Cast<AccDocumentItemOpenCloseDTO>()
                       .Select(accDocumentItemOpenCloseDTO => new
                       {
                           accDocumentItemOpenCloseDTO.CurrencyId,
                           accDocumentItemOpenCloseDTO.DL1Code,
                           accDocumentItemOpenCloseDTO.DL2Code,
                           accDocumentItemOpenCloseDTO.DLTitle1,
                           accDocumentItemOpenCloseDTO.DLTitle2,
                           accDocumentItemOpenCloseDTO.SLCode,
                           accDocumentItemOpenCloseDTO.SLTitle,
                           accDocumentItemOpenCloseDTO.SumCredit,
                           accDocumentItemOpenCloseDTO.SumCurrencyAmount,
                           accDocumentItemOpenCloseDTO.SumDebit,
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
                       });

                var path = $"{Environment.CurrentDirectory}\\Report\\accDocumentItemOpenCloseDTOReport.mrt";
                report.RegBusinessObject("AccDocumentItemOpenCloseDTOReport", data);
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
                var data = RadGridView1.SelectedItems.Cast<AccDocumentItemOpenCloseDTO>()
                       .Select(accDocumentItemOpenCloseDTO => new
                       {
                           accDocumentItemOpenCloseDTO.CurrencyId,
                           accDocumentItemOpenCloseDTO.DL1Code,
                           accDocumentItemOpenCloseDTO.DL2Code,
                           accDocumentItemOpenCloseDTO.DLTitle1,
                           accDocumentItemOpenCloseDTO.DLTitle2,
                           accDocumentItemOpenCloseDTO.SLCode,
                           accDocumentItemOpenCloseDTO.SLTitle,
                           accDocumentItemOpenCloseDTO.SumCredit,
                           accDocumentItemOpenCloseDTO.SumCurrencyAmount,
                           accDocumentItemOpenCloseDTO.SumDebit,
                       });

                report.RegBusinessObject("AccDocumentItemOpenCloseDTOReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemOpenCloseDTOReport.mrt");
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
        private void AccButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(67))
            {
                _viewModel.RaiseTestRequested(TypeEnum.Profits);
                 }
        }
    }
}
