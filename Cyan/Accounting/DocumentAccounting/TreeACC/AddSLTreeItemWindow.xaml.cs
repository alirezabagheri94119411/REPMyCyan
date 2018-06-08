using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Accounts.SLAccount;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    /// <summary>
    /// Interaction logic for AddSLTreeItemWindow.xaml
    /// </summary>
    public partial class AddSLTreeItemWindow : RadWindow
    {
        public AddSLTreeItemWindow(AddSLTreeItemWindowViewModel addSLTreeItemWindowModel)
        {
            InitializeComponent();
            
            DataContext = addSLTreeItemWindowModel as AddSLTreeItemWindowViewModel;
            //Loaded += (s, ea) =>
            //{
            //    DataContext = DataContext as AddEditSLViewModel;
            //    //DataContext.SL.PropertyChanged += (sender, earg) =>
            //    //{
            //    //    if (earg.PropertyName == "MinLength") textBoxInputRegExBehavior.MinLength = _viewModel.SL.MinLength;
            //    //};
               
            //};
            //Unloaded += (s, ea) =>
            //{
                
            //};

        }



        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            sLCodeTextbox.Focus();

            sLCodeTextbox.CaretIndex = 10;
            //gLCodeTextbox.CaretIndex = 10;

        }
        private void sLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

    }
}
