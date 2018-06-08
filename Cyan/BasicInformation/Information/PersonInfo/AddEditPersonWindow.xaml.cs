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

namespace Saina.WPF.BasicInformation.Information.PersonInfo
{
    /// <summary>
    /// Interaction logic for AddEditPersonWindow.xaml
    /// </summary>
    public partial class AddEditPersonWindow : RadWindow, INotifyPropertyChanged
    {
        private PersonListViewModel personListViewModel;
        public AddEditPersonWindow()
        {
            InitializeComponent();
            Person = new Person();
            DataContext = this;
            Loaded += (s, e) =>
            {
                //  addEditBankAccountWindowViewModel = DataContext as AddEditBankAccountWindowViewModel;
                personListViewModel = DataContext as PersonListViewModel;
                //addEditBankAccountWindowViewModel.LoadBanks();
            //    personListViewModel.LoadBanks();


            };
        }
        private Person _sLStandardDescription;

        public Person Person
        {
            get { return _sLStandardDescription; }
            set
            {
                if (_sLStandardDescription == value) return;
                _sLStandardDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Person)));
            }
        }
        public event Action OkClicked;
        public event PropertyChangedEventHandler PropertyChanged;

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                OkClicked?.Invoke();
               personListViewModel.Add();
                personListViewModel.Save();
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
            Person = null;
            Close();
        }

    }
}



