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

namespace Saina.WPF.BasicInformation.Accounts.GLAccount
{
    /// <summary>
    /// Interaction logic for AddEditGLView.xaml
    /// </summary>
    public partial class AddEditGLView : UserControl
    {
        private AddEditGLViewModel _viewModel;

        public AddEditGLView()
        {
            InitializeComponent();
          
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditGLViewModel;
                _viewModel.GL.PropertyChanged += (sender, earg) =>
                {
                    if (earg.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _viewModel.GL.MinLength;
                };
                _viewModel.Failed += OnFailed;
               // _viewModel.Error += OnError;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
               // _viewModel.Error -= OnError;

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            gLCodeTextbox.Focus();
            
              gLCodeTextbox.CaretIndex =10;
            //gLCodeTextbox.CaretIndex = 10;

        }

        private void gLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }



        //private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    gLCodeTextbox.IsEnabled = true;
        //}
    }
}
