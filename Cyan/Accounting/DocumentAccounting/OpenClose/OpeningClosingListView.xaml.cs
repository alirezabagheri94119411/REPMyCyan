using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
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

namespace Saina.WPF.Accounting.DocumentAccounting.OpenClose
{
    /// <summary>
    /// Interaction logic for OpeningClosingListView.xaml
    /// </summary>
    public partial class OpeningClosingListView : UserControl
    {
        private OpeningClosingListViewModel _viewModel;
       // private bool? _dialogResult;
        public OpeningClosingListView()
        {
            InitializeComponent();
          //  InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as OpeningClosingListViewModel;
                _viewModel.Error += OnError;
                _viewModel.Information += OnInformation;

            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Error -= OnError;
                _viewModel.Information -= OnInformation;

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
        private void OnInformation(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اطلاعات";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }

        //step3
        private void AccButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RaiseTestRequested(TypeEnum.Opening);
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
