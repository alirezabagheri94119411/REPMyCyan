using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول شرح استاندارد
    /// </summary>
    [Table("SLStandardDescriptions", Schema = "Info")]
    public class SLStandardDescription
    {
        /// <summary>
        /// آی دی شرح استاندارد
        /// </summary>
        private int _sLStandardDescriptionId;

        public int SLStandardDescriptionId
        {
            get { return _sLStandardDescriptionId; }
            set { _sLStandardDescriptionId = value; }
        }
        /// <summary>
        /// کد معین
        /// </summary>
        private int _sLStandardDescriptionCode;

        public int SLStandardDescriptionCode
        {
            get { return _sLStandardDescriptionCode; }
            set { _sLStandardDescriptionCode = value; }
        }

        /// <summary>
        /// عنوان شرح استاندارد
        /// </summary>
        private string _sLStandardDescriptiontTitle;

        public string SLStandardDescriptionTitle
        {
            get { return _sLStandardDescriptiontTitle; }
            set { _sLStandardDescriptiontTitle = value; }
        }
        private int _sLId;

        public int SLId
        {
            get { return _sLId; }
            set { _sLId = value; }
        }
        private SL _sL;

        public SL SL
        {
            get { return _sL; }
            set { _sL = value; }
        }


    }
}
