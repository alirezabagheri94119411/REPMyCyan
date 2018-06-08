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
    /// جدول روش
    /// </summary>
    [Table("Styles", Schema = "Info")]
    public class Style:BaseEntity
    {
        public Style()
        {
            DocumentNumberings = new ObservableCollection<DocumentNumbering>();
        }
        /// <summary>
        /// آی دی روش 
        /// </summary>
        private int _styleId;

        public int StyleId
        {
            get { return _styleId; }
            set { _styleId = value; }
        }
        /// <summary>
        /// عنوان روش
        /// </summary>
        private string _styleTitle;

        public string StyleTitle
        {
            get { return _styleTitle; }
            set { _styleTitle = value; }
        }

        /// <summary>
        /// لیستی از شماره گذاری اسناد
        /// </summary>
        public virtual ICollection<DocumentNumbering> DocumentNumberings { get; set; }
    }
}
