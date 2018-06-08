using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Settings
{
    /// <summary>
    /// تنظیمات سیستم خرده فروشی
    /// </summary>
   public class SystemSettingRetail:BaseEntity
    {
        /// <summary>
        /// انتخاب انبار از لیست انبار
        /// </summary>
        private string _selectWarehouse;

        public string SelectWarehouse
        {
            get { return _selectWarehouse; }
            set { _selectWarehouse = value; }
        }


        /// <summary>
        /// تعداد ارقام روند در فاکتور خرده فروشی
        /// </summary>
        private string _numberDigit;

        public string NumberDigit
        {
            get { return _numberDigit; }
            set { _numberDigit = value; }
        }

        /// <summary>
        /// گزارش پیش فرض  فاکتور و برگشت از فاکتور
        /// </summary>
        private string _invoice;

        public string Invoice
        {
            get { return _invoice; }
            set { _invoice = value; }
        }

        /// <summary>
        /// ثبت فاکتور و برگشت و تسویه به روز
        /// </summary>
        private string _factorRegistration;

        public string FactorRegistration
        {
            get { return _factorRegistration; }
            set { _factorRegistration = value; }
        }

        /// <summary>
        /// تعداد نسخه چاپ
        /// </summary>
        private string _numberPrint;

        public string NumberPrint
        {
            get { return _numberPrint; }
            set { _numberPrint = value; }
        }

        /// <summary>
        /// ترمینال کارت خوان 
        /// </summary>
        private string _terminalCardReader;

        public string TerminalCardReader
        {
            get { return _terminalCardReader; }
            set { _terminalCardReader = value; }
        }

        /// <summary>
        /// محل پیش فرض موس در فاکتور
        /// </summary>
        private string _defaultMouseLocation;

        public string DefaultMouseLocation
        {
            get { return _defaultMouseLocation; }
            set { _defaultMouseLocation = value; }
        }

    }
}
