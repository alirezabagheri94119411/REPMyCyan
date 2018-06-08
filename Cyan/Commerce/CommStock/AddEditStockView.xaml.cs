using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Saina.WPF.Commerce.CommStock
{
    /// <summary>
    /// Interaction logic for AddEditStockView.xaml
    /// </summary>
    public partial class AddEditStockView : UserControl
    {
        public AddEditStockView()
        {
            InitializeComponent(); 
        }

        private void productButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditProductWindow addEditProductWindow = new AddEditProductWindow();
            addEditProductWindow.DataContext = this.DataContext;
            //attachmentListWindow.ShowDialog();
            addEditProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addEditProductWindow.Width = 1024;
            addEditProductWindow.Height = 768;
            addEditProductWindow.CanClose = true;
            addEditProductWindow.Owner = Window.GetWindow(this);
            addEditProductWindow.Show();
            //addEditProductWindow.ShowDialog();
        }
        // Rasouli
        //private void RadPathButton_Click(object sender, RoutedEventArgs e)
        //{
        //    addEditStockViewModel = new AddEditStockViewModel(stocksService, productsService, usersService, sLsService);

        //    addEditStockViewModel.Products = new ObservableCollection<Product>();

        //    foreach (var item in radComboBox.SelectedItems)
        //    {
        //        addEditStockViewModel.Products.Add((Product)item);
        //    }

        //    addEditStockViewModel.OnSave();
        //}
        //private void radComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var viewModel = DataContext as AddEditStockViewModel;

        //    if (viewModel != null)
        //    {
        //        viewModel.Stock.Products.Clear();

        //        radComboBox.SelectedItems
        //            .Cast<Product>()
        //            .ToList()
        //            .ForEach(p => viewModel.Stock.Products.Add(p));
        //    }
        //}
    }
}
