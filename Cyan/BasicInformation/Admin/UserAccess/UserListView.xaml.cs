using Microsoft.Win32;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
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

namespace Saina.WPF.BasicInformation.Admin.UserAccess
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        private AccessUtility _accessUtility;
        private UserListViewModel _viewModel;
        private bool? _dialogResult;

        public UserListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as UserListViewModel;
                radGridView.SelectedItem = null;
                _viewModel.Deleting += OnDeleting;
                _viewModel.Deleted += OnDeleted;
                _viewModel.Failed += OnFailed;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Deleting -= OnDeleting;
                _viewModel.Deleted -= OnDeleted;
                _viewModel.Failed -= OnFailed;
            };

        }
        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
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

        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(20))
            {
                permission p = new permission();
                ((DataViewModel)p.DataContext).SelectedUser = (User)((RadPathButton)sender).DataContext;
                p.Width = 1300;
                p.Height = 800;
                p.CanClose = true;
                p.Owner = Window.GetWindow(this);
                p.Show();
            }
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (radGridView.SelectedItems.Any())
                        data = radGridView.SelectedItems.Cast<User>()
                         .Select(user => new
                         {
                             UserName = user.UserName,
                             FriendlyName = user.FriendlyName,
                             GroupName = user.Group.Name,
                             Password = user.Password,
                         }).ToList();
                    else
                    {
                        data = _viewModel.Users.Cast<User>()
                              .Select(user => new
                              {
                                  UserName = user.UserName,
                                  FriendlyName = user.FriendlyName,
                                  GroupName = user.Group.Name,
                                  Password = user.Password,
                              }).ToList();

                    }
                    report.RegBusinessObject("UserReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\userReport.mrt");
                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (radGridView.SelectedItems.Any())
                    {
                        data = _viewModel.Users.Cast<User>()
                              .Select(user => new
                              {
                                  UserName = user.UserName,
                                  FriendlyName = user.FriendlyName,
                                  GroupName = user.Group.Name,
                                  Password = user.Password,
                              }).ToList();


                        report.RegBusinessObject("UserReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\userReport.mrt");
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
                var data = radGridView.SelectedItems.Cast<User>().ToList()
                       .Select(user => new
                       {
                           UserName = user.UserName,
                           FriendlyName = user.FriendlyName,
                           GroupName = user.Group.Name,
                           Password = user.Password,
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

                var path = $"{Environment.CurrentDirectory}\\Report\\userReport.mrt";
                report.RegBusinessObject("UserReport", data);
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
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (radGridView.SelectedItems.Any())
                        data = radGridView.SelectedItems.Cast<User>()
                         .Select(user => new
                         {
                             UserName = user.UserName,
                             FriendlyName = user.FriendlyName,
                             GroupName = user.Group.Name,
                             Password = user.Password,
                         }).ToList();
                    else
                    {
                        data = _viewModel.Users.Cast<User>()
                              .Select(user => new
                              {
                                  UserName = user.UserName,
                                  FriendlyName = user.FriendlyName,
                                  GroupName = user.Group.Name,
                                  Password = user.Password,
                              }).ToList();

                    }
                    report.RegBusinessObject("UserReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\userReport.mrt");
                    report.Print();
                }
            }
        }
        private void PrintReportAll()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (radGridView.SelectedItems.Any())
                    {
                        data = _viewModel.Users.Cast<User>()
                              .Select(user => new
                              {
                                  UserName = user.UserName,
                                  FriendlyName = user.FriendlyName,
                                  GroupName = user.Group.Name,
                                  Password = user.Password,
                              }).ToList();


                        report.RegBusinessObject("UserReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\userReport.mrt");
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
                var col0Visibility = radGridView.Columns[0].IsVisible;
                radGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    radGridView.Export(stream,
                     opt);
                }
                radGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }

    }
}
