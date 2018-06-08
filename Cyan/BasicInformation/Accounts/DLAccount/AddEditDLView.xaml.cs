
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

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// Interaction logic for AddEditDLView.xaml
    /// </summary>
    public partial class AddEditDLView : UserControl
    {
        private AddEditDLViewModel _viewModel;

        public AddEditDLView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditDLViewModel;
                _viewModel.DL.PropertyChanged += (sender, earg) =>
                {
                    if (earg.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _viewModel.DL.MinLength;
                };
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

        private void RadAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dLCodeTextbox.IsEnabled = true;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dLCodeTextbox.Focus();
            dLCodeTextbox.CaretIndex = 20;
            //gLCodeTextbox.CaretIndex = 10;
           // tab.IsVisible = false;
        }

        private void dLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }


        //private void RadioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    buyerCheckBox.IsEnabled = false;
        //    personelCheckBox.IsEnabled = false;

        //    sellerCheckBox.IsEnabled = false;
        //    //if (buyerCheckBox.IsChecked == true || personelCheckBox.IsChecked == true || sellerCheckBox.IsChecked == true)
        //    //{
        //    //    personelCheckBox.IsChecked = false;
        //    //    sellerCheckBox.IsChecked = false;
        //    //    buyerCheckBox.IsChecked = false;
        //    //}
        //}

        //private void personCheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    buyerCheckBox.IsEnabled = true;
        //    personelCheckBox.IsEnabled = true;
        //    sellerCheckBox.IsEnabled = true;

        //}
    }
}
