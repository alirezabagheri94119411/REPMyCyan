using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Settings
{
    /// <summary>
    /// جدول خارجی معین
    /// </summary>
   public class SLSetting
    {
        /// <summary>
        /// آی دی خارجی معین
        /// </summary>
            public string SLId { get; internal set; }
        /// <summary>
        /// عنوان خارجی معین
        /// </summary>
            public string Title { get; internal set; }
    }
}
