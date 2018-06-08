using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Financial
{
   public class EditableFinancialYear:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی سال مالی
        /// </summary>
        private int _financialYearId;

        public int FinancialYearId
        {
            get { return _financialYearId; }
            set
            {
                SetProperty(ref _financialYearId, value);
            }
        }
        private int _yearName;
        /// <summary>
        /// نام سال مالی
        /// </summary>
        [Required(ErrorMessage = "لطفا سال مالی را وارد نمایید.")]

        public int YearName
        {
            get { return _yearName; }
            set
            {
                SetProperty(ref _yearName, value);
            }
        }
        /// <summary>
        /// توضیحات
        /// </summary>
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }
        /// <summary>
        /// تاریخ شروع
        /// </summary>
        private DateTime _startDate;
        [Required(ErrorMessage = "لطفا تاریخ شروع را انتخاب نمایید.")]

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }
        /// <summary>
        /// تاریخ پایان
        /// </summary>
        private DateTime _endDate;
        [Required(ErrorMessage = "لطفا تاریخ پایان را انتخاب نمایید.")]

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }

        /// <summary>
        /// وضعیت
        /// </summary>
        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
            }
        }
        /// <summary>
        /// سال مالی انتخاب شده
        /// </summary>
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        private int _financialYearEmpyValue;

        public int FinancialYearEmpyValue
        {
            get { return _financialYearEmpyValue; }
            set { SetProperty(ref _financialYearEmpyValue, value); }
        }
    }
}
