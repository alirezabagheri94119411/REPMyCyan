using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    [Table("AccountsNatures", Schema = "Info")]
    public class AccountsNature:BaseEntity
    {
        //public AccountsNature()
        //{
        //    SLs = new HashSet<SL>();
        //}
        /// <summary>
        /// آی دی ماهیت
        /// </summary>
        private int _accountsNatureId;

        public int AccountsNatureId
        {
            get { return _accountsNatureId; }
            set
            {
                _accountsNatureId = value;
            }

            //  public int AccountsNatureId { get; set; }

        }
        /// <summary>
        /// عنوان ماهیت
        /// </summary>
        private string _accountsNatureTitle;

        public string AccountsNatureTitle
        {
            get { return _accountsNatureTitle; }
            set { _accountsNatureTitle = value; }
        }

     //   public virtual ICollection<SL> SLs { get; protected set; }

    }
}
