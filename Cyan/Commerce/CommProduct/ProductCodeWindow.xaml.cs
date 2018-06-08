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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.Commerce.CommProduct
{
    /// <summary>
    /// Interaction logic for ProductCodeWindow.xaml
    /// </summary>
    public partial class ProductCodeWindow : RadWindow
    {
        private ProductListViewModel _viewModel;

        public ProductCodeWindow()
        {

            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as ProductListViewModel;

                //_viewModel.ProductBrandsDropDownOpenedCommand.Execute(null);
                //_viewModel.ProductModelsDropDownOpenedCommand.Execute(null);
                //_viewModel.ProductTypesDropDownOpenedCommand.Execute(null);
                //_viewModel.OtherProductsDropDownOpenedCommand.Execute(null);
                _viewModel.ProductDropDownOpenedCommand.Execute(null);
                _viewModel.Done += OnDone;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Done -= OnDone;
            };
        
}

        private void OnDone()
        {
            Close();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ApplyCommand.Execute(null);

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
