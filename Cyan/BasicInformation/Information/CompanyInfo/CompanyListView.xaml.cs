using System;
using System.Collections.Generic;
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
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System.IO;
using Microsoft.Win32;

namespace Saina.WPF.BasicInformation.Information.CompanyInfo
{
    /// <summary>
    /// Interaction logic for CompanyListView.xaml
    /// </summary>
    public partial class CompanyListView : UserControl
    {
        private CompanyListViewModel _viewModel;
        private bool? _dialogResult;

        public CompanyListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as CompanyListViewModel;
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

       
        private void CompanyAddEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditCompanyWindow addEditCompanyWindow = new AddEditCompanyWindow() {DataContext=DataContext };
            _viewModel.Company = new Company();
            //var Company = ((Control)sender).DataContext as Company;
            //if (Company == null)
            //    addEditCompanyWindow = new AddEditCompanyWindow() { };
            //else
            //    addEditCompanyWindow = new AddEditCompanyWindow() { Company = ((Control)sender).DataContext as Company };

            addEditCompanyWindow.OkClicked += () =>
            {
                ((CompanyListViewModel)DataContext).Companies.Add(addEditCompanyWindow.Company);
            };
            addEditCompanyWindow.Width = 1024;
            addEditCompanyWindow.Height = 768;
            addEditCompanyWindow.CanClose = true;
            addEditCompanyWindow.Owner = Window.GetWindow(this);
            addEditCompanyWindow.Owner = Window.GetWindow(this);
            addEditCompanyWindow.Show();
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowReport()
        {

        }

        private void designButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DesignReport()
        {

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintReport()
        {

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

        private void CompanyEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditCompanyWindow addEditCompanyWindow = new AddEditCompanyWindow() { DataContext = DataContext };
            _viewModel.LoadCompanies();
            _viewModel.Company = ((Control)sender).DataContext as Company;
            addEditCompanyWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addEditCompanyWindow.Width = 768;
            addEditCompanyWindow.Height = 500;
            addEditCompanyWindow.CanClose = true;
            addEditCompanyWindow.Owner = Window.GetWindow(this);
            addEditCompanyWindow.Show();
        }
    }
}
