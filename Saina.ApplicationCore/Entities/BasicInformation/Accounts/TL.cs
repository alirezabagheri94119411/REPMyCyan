using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول حساب های کل 
    /// </summary>
    [Table("TLs", Schema = "Info")]
    public class TL : BaseEntity, IEditableObject
    {
        public TL()
        {
            SLs = new ObservableCollection<SL>();
            // TLDocumentItems = new HashSet<TLDocumentItem>();
            Status = true;
        }
        /// <summary>
        ///آی دی حساب کل
        /// </summary>

        /// <summary>
        /// آی دی حساب کل
        /// </summary>
        private int _tLId;

        public int TLId
        {
            get { return _tLId; }
            set
            {
                SetProperty(ref _tLId, value);
            }
        }

        /// <summary>
        /// نوع حساب
        /// </summary>
        private GL _GL;
        public virtual GL GL
        {
            get { return _GL; }
            set { SetProperty(ref _GL, value); }
        }

        //private GL _gL;

        //public GL GL
        //{
        //    get { return _gL; }
        //    set
        //    {
        //        _gL= value;
        //    }
        //}
        /// <summary>
        /// آی دی حساب گروه
        /// </summary>
        private int _gLId;

        public int GLId
        {
            get { return _gLId; }
            set { SetProperty(ref _gLId, value); }
        }

        /// <summary>
        /// کد حساب
        /// </summary>
        private long _tLCode;

        public long TLCode
        {
            get { return _tLCode; }
            set
            {
                SetProperty(ref _tLCode, value);
            }
        }

        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        /// <summary>
        /// عنوان 2
        /// </summary>
        private string _title2;

        public string Title2
        {
            get { return _title2; }
            set
            {
                SetProperty(ref _title2, value);
            }
        }

        /// <summary>
        /// وضعیت
        /// </summary>
        private bool _status;

        public bool Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        /// لیستی از گروه حساب معین
        /// </summary>
        public virtual ICollection<SL> SLs { get; protected set; }
        //  public virtual ICollection<TLDocumentItem> TLDocumentItems { get; protected set; }
        private bool isExpanded;
        private string imageUrl;



        private string _imageUrl;
        [NotMapped]

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private bool _isExpanded;

        [NotMapped]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }
        private string _regEx;
        [NotMapped]
        public string Regex
        {
            get { return _regEx; }
            set { SetProperty(ref _regEx, value); }
        }

        private int _minLength;
        [NotMapped]
        public int MinLength
        {
            get { return _minLength; }
            set { SetProperty(ref _minLength, value); }
        }
        private int _maxLength;
        [NotMapped]
        public int MaxLength
        {
            get { return _maxLength; }
            set { SetProperty(ref _maxLength, value); }
        }
        private long _tLCodeEmptyValue;
        [NotMapped]
        public long TLCodeEmptyValue
        {
            get { return _tLCodeEmptyValue; }
            set { SetProperty(ref _tLCodeEmptyValue, value); }
        }
        TLData backupData;
        internal struct TLData
        {
            internal long TLCode;
            internal int GLId;
            internal string Title;
            internal string Title2;
            internal bool Status;
        }
        void IEditableObject.BeginEdit()
        {
            backupData.TLCode = TLCode;
            backupData.GLId = GLId;
            backupData.Title = Title;
            backupData.Title2 = Title2;
            backupData.Status = Status;

        }
        void IEditableObject.EndEdit()
        {
            backupData = new TLData();
        }

        void IEditableObject.CancelEdit()
        {
            TLCode = backupData.TLCode;
            GLId = backupData.GLId;
            Title = backupData.Title;
           Title2 =backupData.Title2  ;
           Status= backupData.Status  ;
        }
    }
}
