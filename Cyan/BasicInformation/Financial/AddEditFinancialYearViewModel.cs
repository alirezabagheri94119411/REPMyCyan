using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Financial
{
    /// <summary>
    /// افزودن و ویرایش سال مالی
    /// </summary>
    class AddEditFinancialYearViewModel : BindableBase
    {
        #region Constructors
        public AddEditFinancialYearViewModel(IFinancialYearsService financialYearsService, IAppContextService appContextService)
        {
            _financialYearsService = financialYearsService;
            _appContextService = appContextService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        #endregion
        #region Fields
        private IFinancialYearsService _financialYearsService;
        private IAppContextService _appContextService;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done;

        public event Action<Exception> Failed;
        public event Action<string> Error;
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private EditableFinancialYear _FinancialYear;
        public EditableFinancialYear FinancialYear
        {
            get { return _FinancialYear; }
            set { SetProperty(ref _FinancialYear, value); }
        }
        #endregion
        #region Methode
        public void SetFinancialYear(FinancialYear financialYear)
        {
            FinancialYear = Mapper.Map<FinancialYear, EditableFinancialYear>(financialYear);
            FinancialYear.ValidationDelegate += FinancialYear_ValidationDelegate;

            FinancialYear.ErrorsChanged += RaiseCanExecuteChanged;

        }
        public async void LoadFinancialYears()
        {
            if (!EditMode)
            {
                var getFinancialYear = await _financialYearsService.GetFinancialYearsAsync();

                string s = "0," + getFinancialYear.ToString();
                var lastFinancialYear = _financialYearsService.GetLastFinancialYear() + 1;
                var stringLastFinancialYear = lastFinancialYear.ToString();
                var lastFinancialYearsCode = stringLastFinancialYear.ToString();
                FinancialYear.YearName = int.Parse($"{lastFinancialYearsCode}");

                FinancialYear.FinancialYearEmpyValue = lastFinancialYear;

            }
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
            PersianCalendar persianCalendar = new PersianCalendar();
            var startDate = persianCalendar.GetYear(FinancialYear.StartDate);
            var endDate = persianCalendar.GetYear(FinancialYear.EndDate);
            var year = FinancialYear.YearName;
            if (year!=0)
            {

                if (startDate != year || endDate!=year)
                {
                    Error?.Invoke("تاریخ شروع  یا پایان با  نام سال مالی منطبق نیست");

                }
                else  if (startDate == year && endDate == year)
                {
                    if (FinancialYear.StartDate > FinancialYear.EndDate)
                    {
                        Error?.Invoke("تاریخ شروع نباید از تاریخ پایان بزرگتر باشد");

                    }
                    else
                    {
                        var editingFinancialYear = Mapper.Map<EditableFinancialYear, FinancialYear>(FinancialYear);
                        try
                        {
                            if (EditMode)
                                await _financialYearsService.UpdateFinancialYearAsync(editingFinancialYear);
                            else
                                await _financialYearsService.AddFinancialYearAsync(editingFinancialYear);
                            Done?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Failed(ex);
                        }
                        finally
                        {
                            FinancialYear = null;
                        }
                    }
                }
                
                }
            else
            {
                Error?.Invoke("سال مالی را وارد نمایید");
            }
        }


        private bool CanSave()
        {
            return !FinancialYear.HasErrors;
        }
        private async Task<List<string>> FinancialYear_ValidationDelegate(object sender, string propertyName)
        {
            var financialYears = (EditableFinancialYear)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(FinancialYear.YearName):

                    if (await _financialYearsService.HasFinancialYear(financialYears.YearName))
                        errors.Add("سال مالی نباید تکراری باشد");
                    return errors;
                //case nameof(FinancialYear.StartDate):

                //    if (await _financialYearsService.HasduplicateStart(financialYears.StartDate))
                //        errors.Add("تاریخ شروع نباید تکراری باشد");
                //    return errors;
                //case nameof(FinancialYear.EndDate):

                //    if (await _financialYearsService.HasduplicateEnd(financialYears.EndDate))
                //        errors.Add("تاریخ پایان نباید تکراری باشد");
                //    return errors;
                default:
                    return null;
            }
        }
        #endregion




        // private FinancialYear _editingFinancialYear = null;






    }
}



