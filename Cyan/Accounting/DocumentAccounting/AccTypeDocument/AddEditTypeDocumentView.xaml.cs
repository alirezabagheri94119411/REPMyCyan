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

namespace Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument
{
    /// <summary>
    /// Interaction logic for AddEditTypeDocumentView.xaml
    /// </summary>
    public partial class AddEditTypeDocumentView : UserControl
    {
        private AddEditTypeDocumentViewModel _viewModel;

        public AddEditTypeDocumentView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditTypeDocumentViewModel;
                _viewModel.Failed += OnFailed;
                _viewModel.Information += OnInformation;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
                _viewModel.Information -= OnInformation;

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
        private void OnInformation( string message)
        {
            var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

            var alert = new RadDesktopAlert
            {
                FlowDirection = FlowDirection.RightToLeft,
                Header = "اطلاعات",
                Content =message,
                ShowDuration = 2000,
            };
            manager.ShowAlert(alert);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            docTypeTextbox.Focus();
            docTypeTextbox.CaretIndex = 100;
          
        }

        private void docTypeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void friendlyNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void friendlyNameTextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
