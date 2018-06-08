using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول نوع تفصیل
    /// </summary>
    [Table("DLTypes", Schema = "Info")]
    public class DLType
    {
        /// <summary>
        /// آی دی نوع تفصیل
        /// </summary>
        private int _dLTypeId;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DLTypeId
        {
            get { return _dLTypeId; }
            set { _dLTypeId = value; }
        }
        /// <summary>
        /// عنوان نوع تفصیل
        /// </summary>
        private string _dLTypeTitle;

        public string DLTypeTitle
        {
            get { return _dLTypeTitle; }
            set { _dLTypeTitle = value; }
        }

    }
}
