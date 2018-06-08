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
using Saina.WPF.BasicInformation.Accounts.DLAccount;
using System.IO;
using Microsoft.Win32;

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
    /// <summary>
    /// Interaction logic for BankAccountListView.xaml
    /// </summary>
    public partial class BankAccountListView : UserControl
    {
        private BankAccountListViewModel _viewModel;
        private bool? _dialogResult;

        public BankAccountListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
               _viewModel = DataContext as BankAccountListViewModel;
                _viewModel.Deleting += OnDeleting;
                _viewModel.Deleted += OnDeleted;
                _viewModel.Failed += OnFailed;
                _viewModel.Error += OnError;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Deleting -= OnDeleting;
                _viewModel.Deleted -= OnDeleted;
                _viewModel.Failed -= OnFailed;
                _viewModel.Error -= OnError;
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
        private void BankAccountAddEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditBankAccountWindow addEditBankAccountWindow = new AddEditBankAccountWindow() {DataContext= DataContext };
            //var BankAccount = ((Control)sender).DataContext as BankAccount;
            //if (BankAccount == null)
            //    addEditBankAccountWindow = new AddEditBankAccountWindow() { };
            //else
            //    addEditBankAccountWindow = new A90ddEditBankAccountWindow() { BankAccount = ((Control)sender).DataContext as BankAccount };
            _viewModel.BankAccount = new BankAccount();
            addEditBankAccountWindow.OkClicked += () =>
            {
                ((BankAccountListViewModel)DataContext).BankAccounts.Add(_viewModel.BankAccount);
               

            };
            addEditBankAccountWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           addEditBankAccountWindow.Width = 768;
           addEditBankAccountWindow.Height = 500;
           addEditBankAccountWindow.CanClose = true;
           addEditBankAccountWindow.Owner = Window.GetWindow(this);
           addEditBankAccountWindow.Show();
         
        }

        private void relatedButton_Click(object sender, RoutedEventArgs e)
        {
            RelatedPersonListWindow relatedPersonListWindow = new RelatedPersonListWindow();
            relatedPersonListWindow.DataContext = this.DataContext;
            relatedPersonListWindow.ShowDialog();

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

        private void BankAccountEdit_Click(object sender, RoutedEventArgs e)
        {

                //addEditBankAccountWindow = new AddEditBankAccountWindow() { };
            AddEditBankAccountWindow addEditBankAccountWindow = new AddEditBankAccountWindow() { DataContext = DataContext };
            _viewModel.LoadBanks();
            _viewModel.BankAccount = ((Control)sender).DataContext as BankAccount;
              addEditBankAccountWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           addEditBankAccountWindow.Width = 768;
           addEditBankAccountWindow.Height = 500;
           addEditBankAccountWindow.CanClose = true;
           addEditBankAccountWindow.Owner = Window.GetWindow(this);
           addEditBankAccountWindow.Show();
        }
    }
}
