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
    /// Interaction logic for EditBankAccountWindow.xaml
    /// </summary>
    public partial class EditBankAccountWindow : RadWindow
    {
        private DLListViewModel _ViewModel;
        string _Invoker;
        public EditBankAccountWindow(string invoker = "gridView")
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

            var errorMessage = "";
            var x = BankNameComboBox.Text;

            if (_ViewModel.DLss.CurrentItem is DL dL)
            {
                if (bankAccountTextBox.Text == "0" || bankAccountTextBox.Text == "")
                {
                    errorMessage += $"شماره حساب وارد نشده است {Environment.NewLine}";
                }
                if (branchTextBox.Text == "")
                {
                    errorMessage += $"نام شعبه وارد نشده است {Environment.NewLine}";
                }
                if (BankNameComboBox.Text == null)
                {
                    errorMessage += $"نام بانک را انتخاب نمایید {Environment.NewLine}";
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
                    // dL.Title = bankAccountTextBox.Text + " " + branchTextBox.Text;
                    dL.Title = bankAccountTextBox.Text + " " + BankNameComboBox.Text + " " + branchTextBox.Text;
                    dL.Branch = branchTextBox.Text;
                    dL.AccountNumber = Convert.ToInt32(bankAccountTextBox.Text);
                    //  var x = BankNameComboBox.SelectedItem;
                    //  var c= BankNameComboBox.valu
                    //dL.BankId =;
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
        private void bankAccountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void AddBankButton_Click(object sender, RoutedEventArgs e)
        {
            _ViewModel.RaiseRequested(BankCurrencyEnum.Bank);
        }
       public enum BankCurrencyEnum
        {
            Bank=1,
            Currency=2
        }

        private void AddCurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            _ViewModel.RaiseRequested(BankCurrencyEnum.Currency);

        }
    }
}
