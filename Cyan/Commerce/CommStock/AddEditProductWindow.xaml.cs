using System.Windows;
using Telerik.Windows.Controls;

namespace Saina.WPF.Commerce.CommStock
{
    /// <summary>
    /// Interaction logic for AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : RadWindow
    {
        private AddEditProductWindowViewModel _viewModel;

        public AddEditProductWindow()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditProductWindowViewModel;
            };
        }

        private void _click(object sender, RoutedEventArgs e)
        {
            this.Close();

            foreach (var item in RadGridView1.SelectedItems)
            {
                _viewModel.Products.AddNewItem(item);
            }
        }
    }
}
