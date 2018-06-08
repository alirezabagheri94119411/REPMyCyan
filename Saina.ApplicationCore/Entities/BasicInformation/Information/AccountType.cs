using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{/// <summary>
/// جدول نوع حساب
/// </summary>
    [Table("AccountTypes", Schema = "Info")]
    public class AccountType:BaseEntity
    {
        public AccountType()
        {
            BankAccounts = new ObservableCollection<BankAccount>();
        }
        /// <summary>
        /// آی دی نوع حساب
        /// </summary>
        private int _accountTypeId;

        public int AccountTypeId
        {
            get { return _accountTypeId; }
            set { _accountTypeId = value; }
        }
        /// <summary>
        /// عنوان نوع حساب
        /// </summary>
        private string _accountTypeTitle;

        public string AccountTypeTitle
        {
            get { return _accountTypeTitle; }
            set { _accountTypeTitle = value; }
        }
        /// <summary>
        /// لیستی از حساب های بانکی
        /// </summary>
        public virtual ICollection<BankAccount> BankAccounts { get; protected set; }
    }
}
