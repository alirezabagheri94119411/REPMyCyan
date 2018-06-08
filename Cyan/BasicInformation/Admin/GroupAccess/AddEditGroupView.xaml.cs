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

namespace Saina.WPF.BasicInformation.Admin.GroupAccess
{
    /// <summary>
    /// Interaction logic for AddEditGroupView.xaml
    /// </summary>
    public partial class AddEditGroupView : UserControl
    {
        public AddEditGroupView()
        {
            InitializeComponent();
        }

        private void groupNameTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
            NameTextBox.CaretIndex = 100;
        }
    }
}
