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

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
    /// <summary>
    /// Interaction logic for AddEditTLView.xaml
    /// </summary>
    public partial class AddEditTLView : UserControl
    {
        private AddEditTLViewModel _viewModel;

        public AddEditTLView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditTLViewModel;
                //_viewModel.TL.PropertyChanged += (sender, earg) =>
                //{
                //    if (earg.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _viewModel.TL.MinLength;
                //};
                _viewModel.TL.PropertyChanged += (sender, earg) =>
                {
                    if (earg.PropertyName == "MinLength")
                        textBoxInputRegExBehavior.MinLength = _viewModel.TL.MinLength;
                };
                _viewModel.Failed += OnFailed;
                _viewModel.Error += OnError;
                //_vm.Ed.PropertyChanged += (s, ea) =>
                //{
                //    if (ea.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _vm.Ed.MinLength;
                //};
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
            tLCodeTextbox.IsEnabled = true;
            tLCodeTextbox.Focus();
            tLCodeTextbox.CaretIndex = tLCodeTextbox.Text.Length;
        }

        private void tLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
