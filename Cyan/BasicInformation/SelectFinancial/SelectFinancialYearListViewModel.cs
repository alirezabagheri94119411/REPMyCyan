using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.BasicInformation.Financial;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.SelectFinancial
{
    class SelectFinancialYearListViewModel : BindableBase
    {

        private ISelectFinancialYearsService _selectFinancialYearsService;
        private IAppContextService _appContextService;

        public SelectFinancialYearListViewModel(ISelectFinancialYearsService selectFinancialYearsService,IAppContextService appContextService)
        {
            _selectFinancialYearsService = selectFinancialYearsService;
            _appContextService = appContextService;
            SaveCommand = new RelayCommand(OnSave, CanSave);
            FinancialYearsDropDownOpenedCommand = new RelayCommand(OnFinancialYearsDropDownOpened);


        }
        public ICommand FinancialYearsDropDownOpenedCommand { get; private set; }
        private List<FinancialYear> _allFinancialYears;


        private ObservableCollection<FinancialYear> _FinancialYears;
        public ObservableCollection<FinancialYear> FinancialYears
        {
            get { return _FinancialYears; }
            set { SetProperty(ref _FinancialYears, value); }
        }
        private EditableFinancialYear _FinancialYear;
        public EditableFinancialYear FinancialYear
        {
            get { return _FinancialYear; }
            set { SetProperty(ref _FinancialYear, value); }
        }
        //private DateTime? _dateNow;

        //public DateTime? DateNow
        //{
        //    get { return _dateNow; }
        //    set { SetProperty(ref _dateNow, value); }
        //}
        public string DateNowString
        {
            get
            {
                var _dateNow = DateTime.Now;
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_dateNow)}/{pc.GetMonth(_dateNow)}/{pc.GetDayOfMonth(_dateNow)}");
                return result;
            }
        }
        private FinancialYear _selected;

        public FinancialYear Selected
        {
            get { return _selected; }
            set
            {
                if (value != null && value != _selected)
                {
                    SetProperty(ref _selected,value);
                    _selectFinancialYearsService.UpdateFinancialYearAsync(_selected);
                    _appContextService.CurrentFinancialYear = value.YearName;
                    _appContextService.SelectedFinancialYear = value;
                }
            }
        }
        private string _computerName;

        public string ComputerName
        {
            get { return _computerName; }
            set { SetProperty(ref _computerName, value); }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        private async void OnFinancialYearsDropDownOpened()
        {
            _allFinancialYears = await _selectFinancialYearsService.GetFinancialYearsActiveAsync();
            FinancialYears = new ObservableCollection<FinancialYear>(_allFinancialYears);
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private List<FinancialYear> _allSelectFinancialYears;

        public async void LoadSelectFinancialYears()
        {
            _allSelectFinancialYears = await _selectFinancialYearsService.GetFinancialYearsActiveAsync();
            FinancialYears = new ObservableCollection<FinancialYear>(_allSelectFinancialYears);
            //DateNow = DateTime.Now;
             ComputerName = System.Environment.MachineName;
          //  UserName = _appContextService.CurrentUser.FriendlyName;
            Selected = FinancialYears.FirstOrDefault(x => x.Selected);
        }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done;
        private void OnCancel()
        {
            Done?.Invoke();
        }
        public event Action<Exception> Failed;

        private async void OnSave()
        {
            var editingFinancialYear = Mapper.Map<EditableFinancialYear, FinancialYear>(FinancialYear);

            await _selectFinancialYearsService.UpdateFinancialYearAsync(editingFinancialYear);
        }


        private bool CanSave()
        {
            return !FinancialYear.HasErrors;
        }




    }
}
