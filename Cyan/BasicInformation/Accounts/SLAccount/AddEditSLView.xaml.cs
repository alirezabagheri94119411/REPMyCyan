using Saina.WPF.BasicInformation.Accounts.SLStandard;
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

namespace Saina.WPF.BasicInformation.Accounts.SLAccount
{
    /// <summary>
    /// Interaction logic for AddEditSLView.xaml
    /// </summary>
    public partial class AddEditSLView : UserControl
    {
        private AddEditSLViewModel _viewModel;

        public AddEditSLView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditSLViewModel;
                _viewModel.SL.PropertyChanged += (sender, earg) =>
                {
                    if (earg.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _viewModel.SL.MinLength;
                };
                _viewModel.Failed += OnFailed;
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
        private void OnError(string error)
        {

            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sLCodeTextbox.IsEnabled = true;
            sLCodeTextbox.Focus();
            sLCodeTextbox.CaretIndex = sLCodeTextbox.Text.Length;
        }

        private void currencyCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (currencyCheckbox.IsChecked==false)
            {
                exchangeRateCheckbox.IsChecked = false;
            }
        }

        private void sLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
