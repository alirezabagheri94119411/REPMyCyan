using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
  
    /// <summary>
    /// اطلاعات بانک
    /// </summary>
    [Table("Banks", Schema = "Info")]
    public class Bank:BaseEntity
    {
        public Bank()
        {
            BankAccounts = new ObservableCollection<BankAccount>();
        }
       
 
        /// <summary>
        /// آی دی بانک
        /// </summary>
        private int _bankId;

        public int BankId
        {
            get { return _bankId; }
            set
            {
               _bankId= value;
            }
        }
        private BankType _bankType;

        public virtual BankType BankType
        {
            get { return _bankType; }
            set { _bankType = value; }
        }


        /// <summary>
        /// نام بانک
        /// </summary>
        private string _bankName;

        public string BankName
        {
            get { return _bankName; }
            set
            {
               _bankName= value;
            }
        }

        /// <summary>
        /// نام بانک 2
        /// </summary>
        ///  private string _bankName;
        private string _bankName2;
        public string BankName2
        {
            get { return _bankName2; }
            set
            {
               _bankName2= value;
            }
        }

        /// <summary>
        /// نوع بانک
        /// </summary>
        private int? _bankTypeId;

        public int? BankTypeId
        {
            get { return _bankTypeId; }
            set
            {
                _bankTypeId = value;
            }
        }
       
        /// <summary>
        /// لیستی از حساب های بانکی
        /// </summary>
        public virtual ICollection<BankAccount> BankAccounts { get; protected set; }
    }
}
