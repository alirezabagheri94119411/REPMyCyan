using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.WPF.BasicInformation.Information.BankInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Saina.WPF.BasicInformation.Information.BankInfo
{
    /// <summary>
    /// افزودن و ویرایش بانک
    /// </summary>
    public class AddEditBankViewModel : BindableBase
    {
        #region Constructors
        public AddEditBankViewModel(IBanksService banksService, IBankTypesService bankTypesService)
        {
            _banksService = banksService;
            _bankTypesService = bankTypesService;
             CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        #endregion
        #region Fields
        private IBanksService _banksService;
        private IBankTypesService _bankTypesService;
        private List<BankType> _allBankTypes;

        private Bank _editingBank = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableBank _Bank;
        public EditableBank Bank
        {
            get { return _Bank; }
            set { SetProperty(ref _Bank, value); }
        }
        private ObservableCollection<BankType> _bankTypes;

        public ObservableCollection<BankType> BankTypes
        {
            get { return _bankTypes; }
            set { SetProperty(ref _bankTypes, value); }
        }

        public event Action Done;
        public event Action<Exception> Failed;

        #endregion
        #region Methode
        public async void LoadBankTypes()
        {
          
            if (BankTypes == null)
            {
                _allBankTypes = await _bankTypesService.GetBankTypesAsync();
                BankTypes = new ObservableCollection<BankType>(_allBankTypes);
            }
        }
        public override void UnLoaded()
        {
            BankTypes = null;

        }
        public void SetBank(Bank bank)
        {
            Bank = Mapper.Map<Bank, EditableBank>(bank);
            Bank.ValidationDelegate += Bank_ValidationDelegate;

            Bank.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        private async void OnSave()
        {
            var editingBank = Mapper.Map<EditableBank, Bank>(Bank);
            try
            {
                if (EditMode)
                await _banksService.UpdateBankAsync(editingBank);
            else
                await _banksService.AddBankAsync(editingBank);
            Done?.Invoke();
        }
            catch (Exception ex)
            {
                Failed(ex);
    }
            finally
            {
                Bank = null;
            }
        }
       
        private bool CanSave()
        {
            return !Bank.HasErrors;
        }
        private async Task<List<string>> Bank_ValidationDelegate(object sender, string propertyName)
        {
            var bank = (EditableBank)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(Bank.BankName):

                    if (await _banksService.HasBankName1(bank.BankName))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;
                case nameof(Bank.BankName2):

                  
                default:
                    return null;
            }
        }
        #endregion












    }
}
