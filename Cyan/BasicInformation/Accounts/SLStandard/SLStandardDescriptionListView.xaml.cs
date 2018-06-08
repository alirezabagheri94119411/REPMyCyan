using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
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

namespace Saina.WPF.BasicInformation.Accounts.SLStandard
{
    /// <summary>
    /// Interaction logic for SLStandardDescriptionListView.xaml
    /// </summary>
    public partial class SLStandardDescriptionListView : UserControl
    {
        public SLStandardDescriptionListView()
        {
            InitializeComponent();
        }

        private void SLStandardAddEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditStandardDescriptionWindow addEditStandardDescriptionWindow = new AddEditStandardDescriptionWindow() { };
            var SLStandardDescription = ((Control)sender).DataContext as SLStandardDescription;
            if (SLStandardDescription == null)
                addEditStandardDescriptionWindow = new AddEditStandardDescriptionWindow() { };
            else
                addEditStandardDescriptionWindow = new AddEditStandardDescriptionWindow() { SLStandardDescription = ((Control)sender).DataContext as SLStandardDescription };

            addEditStandardDescriptionWindow.OkClicked += () =>
            {
                ((SLStandardDescriptionListViewModel)DataContext).SLStandardDescriptions.Add(addEditStandardDescriptionWindow.SLStandardDescription);
            };
            addEditStandardDescriptionWindow.Owner =  Application.Current.MainWindow;
            addEditStandardDescriptionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addEditStandardDescriptionWindow.Width = 600;
            addEditStandardDescriptionWindow.Height = 200     ;
            addEditStandardDescriptionWindow.CanClose = true;
            addEditStandardDescriptionWindow.ShowDialog();
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowReport()
        {

        }

        private void designButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DesignReport()
        {

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintReport()
        {

        }
    }
}
                    