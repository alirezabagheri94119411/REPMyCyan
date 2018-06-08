using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Settings
{
    /// <summary>
    /// تنظیمات سیستم حسابداری
    /// </summary>
   public class SystemAccountingSetting:BaseEntity
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


    }
}
