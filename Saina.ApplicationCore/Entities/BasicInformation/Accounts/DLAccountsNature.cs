using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول ماهیت حساب
    /// </summary>
    [Table("DLAccountsNatures", Schema = "Info")]
    public class DLAccountsNature
    {
        /// <summary>
        /// آی دی ماهیت حساب
        /// </summary>
        private int _dLAccountsNatureId;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DLAccountsNatureId
        {
            get { return _dLAccountsNatureId; }
            set { _dLAccountsNatureId = value; }
        }
        /// <summary>
        /// عنوان ماهیت حساب
        /// </summary>
        private string _dLAccountsNatureTitle;

        public string DLAccountsNatureTitle
        {
            get { return _dLAccountsNatureTitle; }
            set { _dLAccountsNatureTitle = value; }
        }

    }
}
