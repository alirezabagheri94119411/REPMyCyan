using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.CurrencyDocument
{
    /// <summary>
    /// افزودن و ویرایش ارز
    /// </summary>
    class AddEditCurrencyViewModel:BindableBase
    {
        #region Constructors
        public AddEditCurrencyViewModel(ICurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
          
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        #endregion
        #region Fields
        private ICurrenciesService _currenciesService;
      
        private Currency _editingCurrency = null;
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
        private EditableCurrency _Currency;
        public EditableCurrency Currency
        {
            get { return _Currency; }
            set { SetProperty(ref _Currency, value); }
        }
        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode
      
        public void SetCurrency(Currency currency)
        {
            Currency = Mapper.Map<Currency, EditableCurrency>(currency);
            Currency.ValidationDelegate += Currency_ValidationDelegate;

            Currency.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingCurrency = Mapper.Map<EditableCurrency, Currency>(Currency);
            try
            {
                if (EditMode)
                await _currenciesService.UpdateCurrencyAsync(editingCurrency);
            else
                await _currenciesService.AddCurrencyAsync(editingCurrency);
            Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed.Invoke(ex);
            }
            finally
            {
                Currency = null;
            }
        }
        
        private bool CanSave()
        {
            return !Currency.HasErrors;
        }
        private async Task<List<string>> Currency_ValidationDelegate(object sender, string propertyName)
        {
            var currency = (EditableCurrency)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(Currency.CurrencyTitle):

                    if (await _currenciesService.HasEnglishName(currency.CurrencyTitle))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;
                case nameof(Currency.CurrencyTitle2):

                    if (await _currenciesService.HasFarsiName(currency.CurrencyTitle2))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;

                default:
                    return null;
            }
        }
        #endregion

    }
}
