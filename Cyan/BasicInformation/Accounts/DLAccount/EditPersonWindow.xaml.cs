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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// Interaction logic for EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : RadWindow
    {
        private DLListViewModel _ViewModel;
        string _Invoker;
        public EditPersonWindow(string invoker = "gridView")
        {
            _Invoker = invoker;

            InitializeComponent();
            Loaded += (s, e) =>
            {
                _ViewModel = DataContext as DLListViewModel;
                if (_ViewModel.DLss.CurrentItem is DL dL)
                {
                    _ViewModel.DL = dL;
                }
            };
        }

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            var name = nameTextbox.Text;
            var errorMessage = "";
            _ViewModel.DL.Name = nameTextbox.Text;
            if (_ViewModel.DLss.CurrentItem is DL dL)
            {
                if (nameTextbox.Text == "")
                {
                    errorMessage += $"نام شخص وارد نشده است {Environment.NewLine}";
                }
                if (familyTextBox.Text == "")
                {
                    errorMessage += $"نام خانوادگی شخص وارد نشده است {Environment.NewLine}";
                }
                if (errorMessage.Length > 0)
                {

                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "!اخطار";
                    parameters.Content = errorMessage;
                    RadWindow.Alert(parameters);
                }
                else
                {

                    dL.Title = nameTextbox.Text + " " + familyTextBox.Text;
                    dL.Name = nameTextbox.Text;
                    dL.Family = familyTextBox.Text;
                    _ViewModel.Active = false;
                    Close();

                }
            }

        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
            if (_ViewModel.DLss.CurrentItem is DL dL)
            {
                if (_Invoker == "radioButton")
                    dL.DLType = DLTypeEnum.Others;
                Close();
            }

        }
    }
}
