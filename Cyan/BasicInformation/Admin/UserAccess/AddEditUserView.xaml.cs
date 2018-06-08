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

namespace Saina.WPF.BasicInformation.Admin.UserAccess
{
    /// <summary>
    /// Interaction logic for AddEditUserView.xaml
    /// </summary>
    public partial class AddEditUserView : UserControl
    {
        private AddEditUserViewModel _viewModel;

        public AddEditUserView()
        {
            InitializeComponent();
        
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditUserViewModel;
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

        private void friendlyNameTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            friendlyNameTextBox.Focus();
            friendlyNameTextBox.CaretIndex = 100;
        }
    }
}
