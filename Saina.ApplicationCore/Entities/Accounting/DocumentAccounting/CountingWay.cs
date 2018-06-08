using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// جدول روش شماره گذاری
    /// </summary>
    [Table("CountingWays", Schema = "Info")]
    public class CountingWay:BaseEntity
    {
        public CountingWay()
        {
            DocumentNumberings = new ObservableCollection<DocumentNumbering>();
        }
        /// <summary>
        /// آی دی روش شماره گذاری
        /// </summary>
        private int _countingWayId;

        public int CountingWayId
        {
            get { return _countingWayId; }
            set { _countingWayId = value; }
        }
        /// <summary>
        /// عنوان روش شماره گذاری
        /// </summary>
        private string _countingWayTitle;

        public string CountingWayTitle
        {
            get { return _countingWayTitle; }
            set { _countingWayTitle = value; }
        }

        /// <summary>
        /// لیستی از شماره گذاری اسناد
        /// </summary>
        public virtual ICollection<DocumentNumbering> DocumentNumberings { get; set; }
    }
}
