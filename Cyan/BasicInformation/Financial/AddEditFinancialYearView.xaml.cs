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

namespace Saina.WPF.BasicInformation.Financial
{
    /// <summary>
    /// Interaction logic for AddEditFinancialYearView.xaml
    /// </summary>
    public partial class AddEditFinancialYearView : UserControl
    {
        private AddEditFinancialYearViewModel _viewModel;

        public AddEditFinancialYearView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditFinancialYearViewModel;
                _viewModel.Failed += OnFailed;
                _viewModel.Error += OnError;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
                _viewModel.Error -= OnError;

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
        private void OnError(string ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex;
            RadWindow.Alert(parameters);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            yearNameTextBox.Focus();
            yearNameTextBox.CaretIndex = 100;

        }

        private void yearNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void yearNameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            yearNameTextBox.SelectAll();
        }
    }
}
