using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
    /// <summary>
    /// جدول نوع بانک
    /// </summary>
    [Table("BankTypes", Schema = "Info")]
    public class BankType : BaseEntity
    {
        public BankType()
        {
            Banks = new ObservableCollection<Bank>();
        }
        /// <summary>
        /// آی دی نوع بانک
        /// </summary>
        private int _bankTypeId;

        public int BankTypeId
        {
            get { return _bankTypeId; }
            set { _bankTypeId = value; }
        }
        /// <summary>
        /// عنوان نوع بانک
        /// </summary>
        private string _bankTypeTitle;

        public string BankTypeTitle
        {
            get { return _bankTypeTitle; }
            set { _bankTypeTitle = value; }
        }
        public virtual ICollection<Bank> Banks { get; set; }
    }
}
