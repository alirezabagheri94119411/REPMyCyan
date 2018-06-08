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
    //[Flags]
    public enum AccountGLEnum
    {
        [Description("ترازنامه")]
        BalanceSheet =1,
        [Description("سود و زیانی")]
        ProfitLoss =2,
        [Description("انتظامی")]
        Disciplinary =4,
      //  All=BalanceSheet | ProfitLoss | Disciplinary
           
    }
    /// <summary>
    /// جدول معرفی حساب گروه
    /// </summary>
    [Table("GLs", Schema = "Info")]
    public class GL : BaseEntity, IEditableObject
    {

        public GL()
        {
            TLs = new ObservableCollection<TL>();
            Status = true;
            AccountGLEnum = AccountGLEnum.BalanceSheet;
        }

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
        /// نوع حساب
        /// </summary>
  private AccountGLEnum _accountGLEnum;

        public AccountGLEnum AccountGLEnum
        {
            get { return _accountGLEnum; }
            set { SetProperty(ref _accountGLEnum, value);  }
        }

     
     
        /// <summary>
        /// کد حساب
        /// </summary>
        private long _gLCode;

        public long GLCode
        {
            get { return _gLCode; }
            set { SetProperty(ref _gLCode, value);  }
           
        }

        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value);   }
          
        }

        /// <summary>
        /// عنوان 2
        /// </summary>
        private string _title2;

        public string Title2
        {
            get { return _title2; }
            set { SetProperty(ref _title2, value);  }
          
        }

        /// <summary>
        /// وضعیت
        /// </summary>
        private bool _status;

        public bool Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value);  }
        }
       
        /// <summary>
        /// لیستی از حساب کل
        /// </summary>
        public virtual ICollection<TL> TLs { get;protected set; }
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
        //private long _gLCodeEmptyValue;
        //[NotMapped]
        //public long GLCodeEmptyValue
        //{
        //    get { return _gLCodeEmptyValue; }
        //    set { SetProperty(ref _gLCodeEmptyValue, value); }
        //}
        private long _gLCodeEmptyValue;
        [NotMapped]

        public long GLCodeEmptyValue
        {
            get { return _gLCodeEmptyValue; }
            set { SetProperty(ref _gLCodeEmptyValue, value); }
        }
        GLData backupData;
        internal struct GLData
        {
            internal long GLCode;
            internal string Title;
            internal string Title2;
            internal bool Status;
            internal AccountGLEnum AccountGLEnum;
        }
        void IEditableObject.BeginEdit()
        {
            backupData.GLCode = GLCode;
            backupData.Title = Title;
            backupData.Title2 = Title2;
            backupData.Status = Status;
            backupData.AccountGLEnum = AccountGLEnum;

        }
        void IEditableObject.EndEdit()
        {
            backupData = new GLData();
        }

        void IEditableObject.CancelEdit()
        {
            GLCode = backupData.GLCode;
            Title = backupData.Title;
            Title2 = backupData.Title2;
            Status = backupData.Status;
            AccountGLEnum = backupData.AccountGLEnum;
            
        }

    }

}
