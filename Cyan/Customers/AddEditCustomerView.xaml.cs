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

namespace Saina.WPF.Customers
{
    /// <summary>
    /// Interaction logic for AddEditCustomerView.xaml
    /// </summary>
    public partial class AddEditCustomerView : UserControl
    {
        private AddEditCustomerViewModel _viewModel;

        public AddEditCustomerView()
        {
            InitializeComponent();
            //Loaded += (s, ea) =>
            //{
            //    _viewModel = DataContext as AddEditCustomerViewModel;
            //    _viewModel.
            //};
            ParentName = "CustomerListView";
        }

        public string ParentName { get; private set; }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
