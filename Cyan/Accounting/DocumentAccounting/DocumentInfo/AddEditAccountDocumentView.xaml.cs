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

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentInfo
{
    /// <summary>
    /// Interaction logic for AddEditDocumentView.xaml
    /// </summary>
    public partial class AddEditAccountDocumentView : UserControl
    {
        private AddEditAccountDocumentViewModel _viewModel;

        public AddEditAccountDocumentView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditAccountDocumentViewModel;
                
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
            docTitleTextBox.Focus();
            docTitleTextBox.CaretIndex = 100;

        }

        private void docTitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
