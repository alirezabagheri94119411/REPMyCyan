using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AddEditPersonWindow.xaml
    /// </summary>
    public partial class AddEditPersonWindow : RadWindow
    {
        public AddEditPersonWindow()
        {
            InitializeComponent();
        }

        public event Action OkClicked;

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            OkClicked?.Invoke();
        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddEditPerosnDataForm_DeletingItem(object sender, CancelEventArgs e)
        {

        }

        private void AddEditPerosnDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {

        }
    }
}



