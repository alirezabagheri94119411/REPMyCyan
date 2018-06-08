using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Settings.RetailSetting
{
    /// <summary>
    /// محل پیش فرض ماوس
    /// </summary>
   public class DefaultMouseLocation
    {
        /// <summary>
        /// آی دی محل پیش فرض ماوس
        /// </summary>
        private string _defaultMouseLocationId;

        public string DefaultMouseLocationId
        {
            get { return _defaultMouseLocationId; }
            set { _defaultMouseLocationId = value; }
        }
        /// <summary>
        /// عنوان محل پیش فرض ماوس
        /// </summary>
        private string _defaultMouseLocationTitle;

        public string DefaultMouseLocationTitle
        {
            get { return _defaultMouseLocationTitle; }
            set { _defaultMouseLocationTitle = value; }
        }




    }
}
