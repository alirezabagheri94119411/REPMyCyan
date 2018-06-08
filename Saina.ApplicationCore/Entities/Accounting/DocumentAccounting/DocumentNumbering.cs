using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    /// <summary>
    /// شماره گذاری اسناد
    /// </summary>
    [Table("DocumentNumberings", Schema = "Info")]
    public class DocumentNumbering:BaseEntity
    {
        /// <summary>
        /// آی دی شماره گذاری اسناد
        /// </summary>
       
        
        private int _documentNumberingId;

        public int DocumentNumberingId
        {
            get { return _documentNumberingId; }
            set { _documentNumberingId = value; }
        }

        /// <summary>
        /// موجودیت اسناد
        /// </summary>
        private AccountDocument _accountDocument;

        public virtual AccountDocument AccountDocument
        {
            get { return _accountDocument; }
            set { _accountDocument = value; }
        }
        /// <summary>
        /// آی دی سند
        /// </summary>
        private int? _accountDocumentId;

        public int? AccountDocumentId
        {
            get { return _accountDocumentId; }
            set { _accountDocumentId = value; }
        }

        /// <summary>
        /// روش شمار گذاری
        /// </summary>
        private CountingWay _countingWay;

        public virtual CountingWay CountingWay
        {
            get { return _countingWay; }
            set { _countingWay = value; }
        }
        private int? _countingWayId;

        public int? CountingWayId
        {
            get { return _countingWayId; }
            set { _countingWayId = value; }
        }

        /// <summary>
        /// شروع شماره
        /// </summary>
        private long _startNumber;

        public long StartNumber
        {
            get { return _startNumber; }
            set { _startNumber = value; }
        }

        /// <summary>
        /// پایان شماره
        /// </summary>
        private long _endNumber;

        public long EndNumber
        {
            get { return _endNumber; }
            set { _endNumber = value; }
        }

        /// <summary>
        /// روش 
        /// </summary>
        private Style _style;

        public virtual Style Style
        {
            get { return _style; }
            set { _style = value; }
        }

        private int? _styleId;

        public int? StyleId
        {
            get { return _styleId; }
            set { _styleId = value; }
        }



    }
}
