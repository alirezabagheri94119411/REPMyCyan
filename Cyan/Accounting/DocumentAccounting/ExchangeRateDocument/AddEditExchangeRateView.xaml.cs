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

namespace Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument
{
    /// <summary>
    /// Interaction logic for AddEditExchangeRateView.xaml
    /// </summary>
    public partial class AddEditExchangeRateView : UserControl
    {
        private AddEditExchangeRateViewModel _viewModel;

        public AddEditExchangeRateView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditExchangeRateViewModel;
                _viewModel.Failed += OnFailed;
            };
            Unloaded += (s, ea) =>
            {
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
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            exchangeComboBox.Focus();
        }

        private void exchangeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();

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
