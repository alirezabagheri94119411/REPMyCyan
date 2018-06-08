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

namespace Saina.WPF.BasicInformation.Settings.ShoppingSetting
{
    /// <summary>
    /// Interaction logic for ShoppingSystemSettingView.xaml
    /// </summary>
    public partial class ShoppingSystemSettingView : UserControl
    {
       
        private ShoppingSystemSettingViewModel _viewModel;
        private bool? _dialogResult;
        public ShoppingSystemSettingView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as ShoppingSystemSettingViewModel;
                _viewModel.Error += OnError;
            };
            Unloaded += (s, ea) =>
            {

                _viewModel.Error -= OnError;
            };

        }
        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اطلاعات";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }

        private void gLLength_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
