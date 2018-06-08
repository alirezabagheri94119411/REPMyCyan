using Microsoft.Win32;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Telerik.Windows.Data;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    /// <summary>
    /// Interaction logic for DocumntsFlowView.xaml
    /// </summary>
    public partial class DocumntsFlowView : UserControl
    {
        private DocumntsFlowViewModel _viewModel;
        private IAppContextService _appContextService;

        public DocumntsFlowView()
        {
            InitializeComponent();
            _appContextService = SmObjectFactory.Container.GetInstance<IAppContextService>();

            Loaded += (s, e) =>
            {
                _viewModel = DataContext as DocumntsFlowViewModel;
                accDocumentItemsGridView.SelectedItem = null;
                var sender = _viewModel.AccDocumentItemDTO;
                var state = _viewModel.State;
                accDocumentItemsGridView.IsBusy = true;
                _viewModel.Loaded();
                accDocumentItemsGridView.IsBusy = false;
            };
        }

        private void accDocumentItemsGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (accDocumentItemsGridView.SelectedItem is AccDocumentItem1 accDocumentItem)
            {
                var date = accDocumentItem.DocumentDate;
             //   var x = accDocumentItem.AccDocumentHeader?.DailyNumber;
                PersianCalendar persianCalendar = new PersianCalendar();
                var DocDate = persianCalendar.GetYear(date);

                _viewModel.RaiseTestRequested(accDocumentItem.AccDocumentHeaderId, DocDate, true);
            }
        }
        #region Report

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
     
        private void ShowReport()
        {
            if (accDocumentItemsGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (accDocumentItemsGridView.SelectedItems.Any())
                        data = accDocumentItemsGridView.SelectedItems.Cast<AccDocumentItem1>()

                               .Select(accDocumentItemDTO => new
                               {
                                   accDocumentItemDTO.AccDocumentHeaderId,
                                   accDocumentItemDTO.AccDocumentItemId,
                                   accDocumentItemDTO.Credit,
                                   accDocumentItemDTO?.CurrencyAmount,
                                   accDocumentItemDTO?.CurrencyId,
                                   accDocumentItemDTO?.CurrencyTitle,
                                   accDocumentItemDTO.Debit,
                                   accDocumentItemDTO.Description1,
                                   accDocumentItemDTO.Description2,
                                   accDocumentItemDTO?.DLCode1,
                                   accDocumentItemDTO?.DLCode2,
                                   accDocumentItemDTO?.DLTitle1,
                                   accDocumentItemDTO?.DLTitle2,
                                   accDocumentItemDTO.DocumentDate,
                                   accDocumentItemDTO.DocumentDateString,
                                   accDocumentItemDTO.ExchangeRate,
                                   accDocumentItemDTO.Runningtotal,
                                   accDocumentItemDTO.SLCode,
                                   accDocumentItemDTO.SLTitle,
                                   accDocumentItemDTO.Status,
                                   accDocumentItemDTO?.TrackingDate,
                                   accDocumentItemDTO?.TrackingNumber

                               }).ToList();
                    else
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItemDTO>()
                            .Select(accDocumentItemDTO => new
                            {
                                Code = accDocumentItemDTO.Code,
                                OpeningSumCredit = accDocumentItemDTO.OpeningSumCredit,
                                OpeningSumDebit = accDocumentItemDTO.OpeningSumDebit,
                                RemainCredit = accDocumentItemDTO.RemainCredit,
                                RemainDebit = accDocumentItemDTO.RemainDebit,
                                SumCredit = accDocumentItemDTO.SumCredit,
                                SumDebit = accDocumentItemDTO.SumDebit,
                                Title = accDocumentItemDTO.Title
                            }).ToList();

                    }
                    report.RegBusinessObject("AccDocumentItemDTOReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemDTOReport.mrt");
                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (accDocumentItemsGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (accDocumentItemsGridView.SelectedItems.Any())
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItem1>()
                              .Select(accDocumentItemDTO => new
                              {
                                  accDocumentItemDTO.AccDocumentHeaderId,
                                  accDocumentItemDTO.AccDocumentItemId,
                                  accDocumentItemDTO.Credit,
                                  accDocumentItemDTO?.CurrencyAmount,
                                  accDocumentItemDTO?.CurrencyId,
                                  accDocumentItemDTO?.CurrencyTitle,
                                  accDocumentItemDTO.Debit,
                                  accDocumentItemDTO.Description1,
                                  accDocumentItemDTO.Description2,
                                  accDocumentItemDTO?.DLCode1,
                                  accDocumentItemDTO?.DLCode2,
                                  accDocumentItemDTO?.DLTitle1,
                                  accDocumentItemDTO?.DLTitle2,
                                  accDocumentItemDTO.DocumentDate,
                                  accDocumentItemDTO.DocumentDateString,
                                  accDocumentItemDTO.ExchangeRate,
                                  accDocumentItemDTO.Runningtotal,
                                  accDocumentItemDTO.SLCode,
                                  accDocumentItemDTO.SLTitle,
                                  accDocumentItemDTO.Status,
                                  accDocumentItemDTO?.TrackingDate,
                                  accDocumentItemDTO?.TrackingNumber

                              }).ToList();


                        report.RegBusinessObject("AccDocumentItemDTOReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemDTOReport.mrt");
                        report.Show();
                    }
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
                var data = _viewModel.AccDocumentItems.Cast<AccDocumentItem1>()
                            .Select(accDocumentItemDTO => new
                            {
                                accDocumentItemDTO.AccDocumentHeaderId,
                                accDocumentItemDTO.AccDocumentItemId,
                                accDocumentItemDTO.Credit,
                                accDocumentItemDTO?.CurrencyAmount,
                                accDocumentItemDTO?.CurrencyId,
                                accDocumentItemDTO?.CurrencyTitle,
                                accDocumentItemDTO.Debit,
                                accDocumentItemDTO.Description1,
                                accDocumentItemDTO.Description2,
                                accDocumentItemDTO?.DLCode1,
                                accDocumentItemDTO?.DLCode2,
                                accDocumentItemDTO?.DLTitle1,
                                accDocumentItemDTO?.DLTitle2,
                                accDocumentItemDTO.DocumentDate,
                                accDocumentItemDTO.DocumentDateString,
                                accDocumentItemDTO.ExchangeRate,
                                accDocumentItemDTO.Runningtotal,
                                accDocumentItemDTO.SLCode,
                                accDocumentItemDTO.SLTitle,
                                accDocumentItemDTO.Status,
                                accDocumentItemDTO?.TrackingDate,
                                accDocumentItemDTO?.TrackingNumber
                                
                                ,
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
                var path = $"{Environment.CurrentDirectory}\\Report\\accDocumentItemDTOReport.mrt";
                report.RegBusinessObject("AccDocumentItemDTOReport", data);
                // report.RegBusinessObject("GLReport1", Address);
                // report.RegData("xx", Address);
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
                var data = _viewModel.AccDocumentItems.Cast<AccDocumentItem1>()
                            .Select(accDocumentItemDTO => new
                            {
                                accDocumentItemDTO.AccDocumentHeaderId,
                                accDocumentItemDTO.AccDocumentItemId,
                                accDocumentItemDTO.Credit,
                                accDocumentItemDTO?.CurrencyAmount,
                                accDocumentItemDTO?.CurrencyId,
                                accDocumentItemDTO?.CurrencyTitle,
                                accDocumentItemDTO.Debit,
                                accDocumentItemDTO.Description1,
                                accDocumentItemDTO.Description2,
                                accDocumentItemDTO?.DLCode1,
                                accDocumentItemDTO?.DLCode2,
                                accDocumentItemDTO?.DLTitle1,
                                accDocumentItemDTO?.DLTitle2,
                                accDocumentItemDTO.DocumentDate,
                                accDocumentItemDTO.DocumentDateString,
                                accDocumentItemDTO.ExchangeRate,
                                accDocumentItemDTO.Runningtotal,
                                accDocumentItemDTO.SLCode,
                                accDocumentItemDTO.SLTitle,
                                accDocumentItemDTO.Status,
                                accDocumentItemDTO?.TrackingDate,
                                accDocumentItemDTO?.TrackingNumber
                            }).ToList();
                report.RegBusinessObject("AccDocumentItemDTOReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemDTOReport.mrt");
                report.Print();
            }
        }
        private void PrintReportAll()
        {
            if (accDocumentItemsGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (accDocumentItemsGridView.SelectedItems.Any())
                    {
                        data = _viewModel.AccDocumentItems.Cast<AccDocumentItem1>()
                              .Select(accDocumentItemDTO => new
                              {
                                  accDocumentItemDTO.AccDocumentHeaderId,
                                  accDocumentItemDTO.AccDocumentItemId,
                                  accDocumentItemDTO.Credit,
                                  accDocumentItemDTO?.CurrencyAmount,
                                  accDocumentItemDTO?.CurrencyId,
                                  accDocumentItemDTO?.CurrencyTitle,
                                  accDocumentItemDTO.Debit,
                                  accDocumentItemDTO.Description1,
                                  accDocumentItemDTO.Description2,
                                  accDocumentItemDTO?.DLCode1,
                                  accDocumentItemDTO?.DLCode2,
                                  accDocumentItemDTO?.DLTitle1,
                                  accDocumentItemDTO?.DLTitle2,
                                  accDocumentItemDTO.DocumentDate,
                                  accDocumentItemDTO.DocumentDateString,
                                  accDocumentItemDTO.ExchangeRate,
                                  accDocumentItemDTO.Runningtotal,
                                  accDocumentItemDTO.SLCode,
                                  accDocumentItemDTO.SLTitle,
                                  accDocumentItemDTO.Status,
                                  accDocumentItemDTO?.TrackingDate,
                                  accDocumentItemDTO?.TrackingNumber
                              }).ToList();


                        report.RegBusinessObject("AccDocumentItemDTOReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\accDocumentItemDTOReport.mrt");
                        report.Print();
                    }
                }
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
                var col0Visibility = accDocumentItemsGridView.Columns[0].IsVisible;
                accDocumentItemsGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    accDocumentItemsGridView.Export(stream,
                     opt);
                }
                accDocumentItemsGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
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


        #endregion
    }

}
