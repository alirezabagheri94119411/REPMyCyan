using Microsoft.Win32;
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
using System.Data.Entity;
using Saina.WPF.Behaviors;

namespace Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo
{
    /// <summary>
    /// Interaction logic for AddEditTLDocumentView.xaml
    /// </summary>
    public partial class TLDocumentItemListView : UserControl
    {
        private AccessUtility _accessUtility;
        private TLDocumentItemListViewModel _viewModel;

        public TLDocumentItemListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as TLDocumentItemListViewModel;
                RadGridView1.SelectedItem = null;
            };
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            StiReport report = new StiReport();
            var headerIds = RadGridView1.SelectedItems.Cast<TLDocumentHeader>().Select(z => z.TLDocumentHeaderId);

            using (var uow = new SainaDbContext())
            {
                var data = uow.TLDocumentItems.Include(x => x.TLDocumentHeader).Where(x => headerIds.Contains(x.TLDocumentHeaderId))
                    .ToList()
                         //  var data = RadGridView1.SelectedItems.Cast<TLDocumentHeader>()
                         .Select(tLDocumentHeader => new
                         {
                             TLDocumentExport = tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Daily ? "روزانه" : tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Monthly ? "ماهانه" : "",
                             tLDocumentHeader.TLDocumentHeader.TLDocumentHeaderDate,
                             tLDocumentHeader.TLDocumentHeader.TLDocumentNumber,
                             TLDocumentType = tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Closing ? "اختتامیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Litter ? "تسعیر" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Opening ? "افتتاحیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Profit ? "سود و زیانی" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.None ? "هیچ کدام" : "",
                             tLDocumentHeader.TLDocumentItemCode,
                             tLDocumentHeader.TLDocumentItemDate,
                             tLDocumentHeader.TLId,
                             tLDocumentHeader.TLTitle,
                             tLDocumentHeader.Credit,
                             tLDocumentHeader.Debit
                         });
                report.RegBusinessObject("TLDocumentItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\tLDocumentItemReport.mrt");
                report.Show();
            }
        }
        //private void ShowReport()
        //{
        //    if (RadGridView1.ItemsSource != null)
        //    {
        //        var headerIds = RadGridView1.SelectedItems.Cast<TLDocumentHeader>().Select(z => z.TLDocumentHeaderId);

        //        StiReport report = new StiReport();
        //        using (var uow = new SainaDbContext())
        //        {
        //            dynamic data;
        //            if (RadGridView1.SelectedItems.Any())
        //                data = uow.TLDocumentItems.Include(x => x.TLDocumentHeader).Where(x => headerIds.Contains(x.TLDocumentHeaderId))
        //            .ToList()
        //                 //  var data = RadGridView1.SelectedItems.Cast<TLDocumentHeader>()
        //                 .Select(tLDocumentHeader => new
        //                 {
        //                     TLDocumentExport = tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Daily ? "روزانه" : tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Monthly ? "ماهانه" : "",
        //                     tLDocumentHeader.TLDocumentHeader.TLDocumentHeaderDate,
        //                     tLDocumentHeader.TLDocumentHeader.TLDocumentNumber,
        //                     TLDocumentType = tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Closing ? "اختتامیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Litter ? "تسعیر" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Opening ? "افتتاحیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Profit ? "سود و زیانی" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.None ? "هیچ کدام" : "",
        //                     tLDocumentHeader.TLDocumentItemCode,
        //                     tLDocumentHeader.TLDocumentItemDate,
        //                     tLDocumentHeader.TLId,
        //                     tLDocumentHeader.TLTitle,
        //                     tLDocumentHeader.Credit,
        //                     tLDocumentHeader.Debit
        //                 }).ToList();
        //            //else
        //            //{
        //            //    data = _viewModel.TLDocumentItems.Cast<GL>()
        //            //       .Select(tLDocumentHeader => new
        //            //       {
        //            //           TLDocumentExport = tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Daily ? "روزانه" : tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Monthly ? "ماهانه" : "",
        //            //           tLDocumentHeader.TLDocumentHeader.TLDocumentHeaderDate,
        //            //           tLDocumentHeader.TLDocumentHeader.TLDocumentNumber,
        //            //           TLDocumentType = tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Closing ? "اختتامیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Litter ? "تسعیر" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Opening ? "افتتاحیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Profit ? "سود و زیانی" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.None ? "هیچ کدام" : "",
        //            //           tLDocumentHeader.TLDocumentItemCode,
        //            //           tLDocumentHeader.TLDocumentItemDate,
        //            //           tLDocumentHeader.TLId,
        //            //           tLDocumentHeader.TLTitle,
        //            //           tLDocumentHeader.Credit,
        //            //           tLDocumentHeader.Debit
        //            //       }).ToList();
        //            //}
        //            report.RegBusinessObject("TLDocumentItemReport", data);
        //            report.Load($"{Environment.CurrentDirectory}\\Report\\tLDocumentItemReport.mrt");
        //            report.Show();
        //        }
        //    }
        //}
      
        private void designButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }
        private void DesignReport()
        {
            StiReport report = new StiReport();
            var headerIds = RadGridView1.SelectedItems.Cast<TLDocumentHeader>().Select(z => z.TLDocumentHeaderId);

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
                var data = uow.TLDocumentItems.Include(x => x.TLDocumentHeader).Where(x => headerIds.Contains(x.TLDocumentHeaderId))
                     .ToList()
                          //  var data = RadGridView1.SelectedItems.Cast<TLDocumentHeader>()
                          .Select(tLDocumentHeader => new
                          {
                              TLDocumentExport = tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Daily ? "روزانه" : tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Monthly ? "ماهانه" : "",
                              tLDocumentHeader.TLDocumentHeader.TLDocumentHeaderDate,
                              tLDocumentHeader.TLDocumentHeader.TLDocumentNumber,
                              TLDocumentType = tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Closing ? "اختتامیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Litter ? "تسعیر" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Opening ? "افتتاحیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Profit ? "سود و زیانی" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.None ? "هیچ کدام" : "",
                              tLDocumentHeader.TLDocumentItemCode,
                              tLDocumentHeader.TLDocumentItemDate,
                              tLDocumentHeader.TLId,
                              tLDocumentHeader.TLTitle,
                              tLDocumentHeader.Credit,
                              tLDocumentHeader.Debit,
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
                var path = $"{Environment.CurrentDirectory}\\Report\\tLDocumentItemReport.mrt";
                report.RegBusinessObject("TLDocumentItemReport", data);
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
            var headerIds = RadGridView1.SelectedItems.Cast<TLDocumentHeader>().Select(z => z.TLDocumentHeaderId);

            using (var uow = new SainaDbContext())
            {
                var data = uow.TLDocumentItems.Include(x => x.TLDocumentHeader).Where(x => headerIds.Contains(x.TLDocumentHeaderId))
                      .ToList()
                           //  var data = RadGridView1.SelectedItems.Cast<TLDocumentHeader>()
                           .Select(tLDocumentHeader => new
                           {
                               TLDocumentExport = tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Daily ? "روزانه" : tLDocumentHeader.TLDocumentHeader.TLDocumentExport == TLDocumentExportEnum.Monthly ? "ماهانه" : "",
                               tLDocumentHeader.TLDocumentHeader.TLDocumentHeaderDate,
                               tLDocumentHeader.TLDocumentHeader.TLDocumentNumber,
                               TLDocumentType = tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Closing ? "اختتامیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Litter ? "تسعیر" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Opening ? "افتتاحیه" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.Profit ? "سود و زیانی" : tLDocumentHeader.TLDocumentHeader.TLDocumentType == TLDocumentTypeEnum.None ? "هیچ کدام" : "",
                               tLDocumentHeader.TLDocumentItemCode,
                               tLDocumentHeader.TLDocumentItemDate,
                               tLDocumentHeader.TLId,
                               tLDocumentHeader.TLTitle,
                               tLDocumentHeader.Credit,
                               tLDocumentHeader.Debit
                           });
                report.RegBusinessObject("TLDocumentItemReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\tLDocumentItemReport.mrt");
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
    }
}
