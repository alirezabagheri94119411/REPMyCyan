using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs
{
    /// <summary>
    /// سیستم تنظیمات حسابداری
    /// </summary>
    public class SystemAccountingSettingModel
    {
        /// <summary>
        /// طول حساب گروه
        /// </summary>
        private string _gLLength;

        public string GLLength
        {
            get { return _gLLength; }
            set { _gLLength = value; }
        }


        /// <summary>
        /// طول حساب کل
        /// </summary>
        private string _tLLength;

        public string TLLength
        {
            get { return _tLLength; }
            set { _tLLength = value; }
        }


        /// <summary>
        /// طول حساب معین
        /// </summary>
        private string _sLLength;

        public string SLLength
        {
            get { return _sLLength; }
            set { _sLLength = value; }
        }


        /// <summary>
        /// طول حساب تفصیل
        /// </summary>
        private string _dLLength;

        public string DLLength
        {
            get { return _dLLength; }
            set { _dLLength = value; }
        }
        private string _StatusAcc;

        public string StatusAcc
        {
            get { return _StatusAcc; }
            set { _StatusAcc = value; }
        }

    }
    }
