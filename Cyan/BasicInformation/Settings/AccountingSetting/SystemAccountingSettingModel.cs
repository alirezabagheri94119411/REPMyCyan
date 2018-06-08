using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Settings.AccountingSetting
{
    /// <summary>
    /// سیستم تنظیمات حسابداری
    /// </summary>
    public class EditableSystemAccountingSettingModel : ValidatableBindableBase
    {
        /// <summary>
        /// طول حساب گروه
        /// </summary>
        private string _gLLength;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string GLLength
        {
            get { return _gLLength; }
            set { SetProperty(ref _gLLength, value); }
        }


        /// <summary>
        /// طول حساب کل
        /// </summary>
        private string _tLLength;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]

        public string TLLength
        {
            get { return _tLLength; }
            set { SetProperty(ref _tLLength, value); }
        }


        /// <summary>
        /// طول حساب معین
        /// </summary>
        private string _sLLength;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]

        public string SLLength
        {
            get { return _sLLength; }
            set { SetProperty(ref _sLLength, value); }
        }


        /// <summary>
        /// طول حساب تفصیل
        /// </summary>
        private string _dLLength;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]

        public string DLLength
        {
            get { return _dLLength; }
            set { SetProperty(ref _dLLength, value); }
        }
        private bool _gLActive;

        public bool GLActive
        {
            get { return _gLActive; }
            set { SetProperty(ref _gLActive, value); }
        }
        private bool _tLActive;

        public bool TLActive
        {
            get { return _tLActive; }
            set { SetProperty(ref _tLActive, value); }
        }
        private bool _sLActive;

        public bool SLActive
        {
            get { return _sLActive; }
            set { SetProperty(ref _sLActive, value); }
        }
        private bool _dLActive;

        public bool DLActive
        {
            get { return _dLActive; }
            set { SetProperty(ref _dLActive, value); ; }
        }
        private string _StatusAcc;
        /// <summary>
        /// وضعیت
        /// </summary>
        public string StatusAcc
        {
            get { return _StatusAcc; }
            set { SetProperty(ref _StatusAcc, value); }
        }

    }
}
