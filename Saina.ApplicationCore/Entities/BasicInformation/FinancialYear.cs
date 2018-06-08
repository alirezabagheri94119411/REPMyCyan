using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation
{
    /// <summary>
    /// تعریف فیلدهای سال مالی
    /// </summary>
    [Table("FinancialYears", Schema = "Info")]
    public class FinancialYear : BaseEntity
    {
        public FinancialYear()
        {
            IsActive = true;
        }
        /// <summary>
        /// آی دی سال مالی
        /// </summary>
        private int _financialYearId;

        public int FinancialYearId
        {
            get { return _financialYearId; }
            set
            {
               _financialYearId= value;
            }
        }
        private int _yearName;
        /// <summary>
        /// نام سال مالی
        /// </summary>

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
        private DateTime _startDate = DateTime.Now;

        public DateTime StartDate 
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }
        [NotMapped]
        public string StartDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_startDate)}/{pc.GetMonth(_startDate)}/{pc.GetDayOfMonth(_startDate)}");
                return result;
            }
        }
        /// <summary>
        /// تاریخ پایان
        /// </summary>
        private DateTime _endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }
        [NotMapped]
        public string EndDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_endDate)}/{pc.GetMonth(_endDate)}/{pc.GetDayOfMonth(_endDate)}");
                return result;
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
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

    }
}
