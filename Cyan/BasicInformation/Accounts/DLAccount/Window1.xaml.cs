using Saina.Data.Context;
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

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource dLViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dLViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // dLViewSource.Source = [generic data source]
            using (var uow = new SainaDbContext())
            {
                dLViewSource.Source = uow.DLs.ToList();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, headerDataForm);
        }

        private void HeaderDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void HeaderDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {

        }

        private void HeaderDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        private void RadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

        }
    }
}
