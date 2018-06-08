using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Accounts.TLAccount;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddTLTreeItemWindow.xaml
    /// </summary>
    public partial class AddTLTreeItemWindow : RadWindow
    {
        public AddTLTreeItemWindow(AddTLTreeItemWindowViewModel addTLTreeItemWindowModel)
        {
            InitializeComponent();
          
            DataContext = addTLTreeItemWindowModel;

        }

      
        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            tLCodeTextbox.Focus();

            tLCodeTextbox.CaretIndex = 10;
            //gLCodeTextbox.CaretIndex = 10;

        }
        private void tLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

    }

}
