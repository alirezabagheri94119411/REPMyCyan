﻿using Saina.ApplicationCore.Entities.BasicInformation.Information;
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

namespace Saina.WPF.BasicInformation.Information.CompanyInfo
{
    /// <summary>
    /// Interaction logic for AddEditCompanyWindow.xaml
    /// </summary>
    public partial class AddEditCompanyWindow : RadWindow, INotifyPropertyChanged
    {
        private CompanyListViewModel companyListViewModel;
        
        public AddEditCompanyWindow()
        {

            InitializeComponent();
            Company = new Company();
            DataContext = this;
            Loaded += (s, e) =>
            {
                //  addEditBankAccountWindowViewModel = DataContext as AddEditBankAccountWindowViewModel;
                companyListViewModel = DataContext as CompanyListViewModel;
                //addEditBankAccountWindowViewModel.LoadBanks();
                //    personListViewModel.LoadBanks();


            };
        }
        private Company _company;

        public Company Company
        {
            get { return _company; }
            set
            {
                if (_company == value) return;
                _company = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Company)));
            }
        }


        public event Action OkClicked;
        public event PropertyChangedEventHandler PropertyChanged;

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                OkClicked?.Invoke();
                companyListViewModel.Add();
                companyListViewModel.Save();
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
           // Company = null;
            Close();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dateTextBox.Focus();
        }

        private void companyNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
