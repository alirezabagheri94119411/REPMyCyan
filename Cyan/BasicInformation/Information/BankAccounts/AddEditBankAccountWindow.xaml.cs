using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
    /// <summary>
    /// Interaction logic for AddEditBankAccountWindow.xaml
    /// </summary>
    public partial class AddEditBankAccountWindow : RadWindow, INotifyPropertyChanged
    {
       // private AddEditBankAccountWindowViewModel addEditBankAccountWindowViewModel;
        private BankAccountListViewModel _viewModel;
        public AddEditBankAccountWindow()
        {
            CultureInfo culutreInfo = new CultureInfo("fa-IR");
            culutreInfo.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culutreInfo;
            //برای نمایش اعداد به صورت فارسی
            // Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentUICulture = culutreInfo;
            InitializeComponent();
           // BankAccount = new BankAccount();
          //  DataContext = addEditBankAccountWindowViewModel;
            //    this.AccDocumentItemsRadGridView.KeyboardCommandProvider = new CustomKeyboardCommandProvider(this.AccDocumentItemsRadGridView);

            Loaded += (s, e) =>
            {
                //  addEditBankAccountWindowViewModel = DataContext as AddEditBankAccountWindowViewModel;
                _viewModel = DataContext as BankAccountListViewModel;
                //addEditBankAccountWindowViewModel.LoadBanks();
            };



        }


       // private BankAccount _bankAccount;
        private object _dialogResult;

        //public BankAccount BankAccount
        //{
        //    get { return _bankAccount; }
        //    set
        //    {
        //        if (_bankAccount == value) return;
        //        _bankAccount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankAccount)));
        //    }
        //}


        public event Action OkClicked;
        public event PropertyChangedEventHandler PropertyChanged;

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                OkClicked?.Invoke();
                _viewModel.Add();
                _viewModel.Save();
                Close();
            }
        }

        private bool Validate()
        {
            var result = true;
            var errorMessage = "";
            //if (bankAccountListViewModel.BankAccount.Bank==null)
            //{
            //    errorMessage += $"حساب بانکی خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.AccountNumber == 0)
            //{
            //    errorMessage += $"شماره حساب خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.Branch == null)
            //{
            //    errorMessage += $"شعبه خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.ShabaNumber == null)
            //{
            //    errorMessage += $" شماره شبا خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.AccountTypeId == null)
            //{
            //    errorMessage += $"نوع حساب خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.CardNumber == 0)
            //{
            //    errorMessage += $"شماره کارت خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.PoseId == null)
            //{
            //    errorMessage += $"شماره پوز خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.OpeningDate == null)
            //{
            //    errorMessage += $"تاریخ افتتاح خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.PostalCode == 0)
            //{
            //    errorMessage += $"کد پستی خالی می باشد {Environment.NewLine}";
            //}
            //if (bankAccountListViewModel.BankAccount.Addrress == null)
            //{
            //    errorMessage += $"آدرس خالی می باشد {Environment.NewLine}";
            //}
            
            if (errorMessage.Length > 0)
            {

                result = false;
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "!اخطار";
                parameters.Content = errorMessage;
                RadWindow.Alert(parameters);
            }
            return result;
        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
           // bankAccountListViewModel.BankAccount = null;
            Close();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BankNameComboBox.Focus();
            // yearNameTextBox.CaretIndex = 100;

        }

        private void bankAccountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }

        private void BankNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var xx = BankNameComboBox.SelectedValue;
            var xx2 = BankNameComboBox.SelectedItem;

        }
    }
}
