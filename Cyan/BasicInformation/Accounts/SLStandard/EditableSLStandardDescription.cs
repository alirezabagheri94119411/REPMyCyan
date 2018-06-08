using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.SLStandard
{
   public class EditableSLStandardDescription:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی شرح استاندارد
        /// </summary>
        private int _sLStandardDescriptionId;

        public int SLStandardDescriptionId
        {
            get { return _sLStandardDescriptionId; }
            set { SetProperty(ref _sLStandardDescriptionId, value); }
        }
        /// <summary>
        /// کد معین
        /// </summary>
        private int _sLStandardDescriptionCode;

        public int SLStandardDescriptionCode
        {
            get { return _sLStandardDescriptionCode; }
            set { SetProperty(ref _sLStandardDescriptionCode, value); }
        }

        /// <summary>
        /// عنوان شرح استاندارد
        /// </summary>
        private string _sLStandardDescriptiontTitle;

        public string SLStandardDescriptionTitle
        {
            get { return _sLStandardDescriptiontTitle; }
            set { SetProperty(ref _sLStandardDescriptiontTitle, value); }
        }
        private int _sLid;

        public int SLId
        {
            get { return _sLid; }
            set { SetProperty(ref _sLid,value); }
        }

    }
}
