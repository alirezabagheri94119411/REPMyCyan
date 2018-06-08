using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.BasicInformation.Settings;
using Saina.ApplicationCore.Entities.Commerce;
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
    public enum AccountsNatureEnum
    {
        //BalanceSheet,
        //ProfitLoss,
        //Disciplinary
        [Description("بدهکار")]
        Debt = 1,
        [Description("بستانکار")]
        Cred = 2,
        [Description("مهم نیست")]
        None = 4
    }
    public enum PropertyEnum
    {
        [Description("تعدادی")]
        Count = 1,
        [Description("ارز")]
        Exchange = 2,
        [Description("پیگیری")]
        Consistency = 4,
        [Description("تسعیر")]
        Litter = 8
    }
    /// <summary>
    /// جدول حساب معین
    /// </summary>
    [Table("SLs", Schema = "Info")]
    public class SL : BaseEntity, IValidatableObject, IEditableObject
    {

        public SL()
        {
            Stocks = new ObservableCollection<Stock>();
            SLStandardDescriptions = new ObservableCollection<SLStandardDescription>();
            SLs = new ObservableCollection<AccDocumentItem>();
            StcDocumentHeaders = new ObservableCollection<StcDocumentHeader>();
            Status = true;
            AccountsNatureEnum = AccountsNatureEnum.None;

        }

        /// <summary>
        /// آی دی حساب معین
        /// </summary>
        private int _sLId;

        public int SLId
        {
            get { return _sLId; }
            set
            {
                SetProperty(ref _sLId, value);
            }
        }

        /// <summary>
        /// نوع حساب
        /// </summary>
        private TL _tL;

        public virtual TL TL
        {
            get { return _tL; }
            set
            {
                SetProperty(ref _tL, value);
            }
        }
        /// <summary>
        /// آی دی گروه کل
        /// </summary>
        private int _tLId;

        public int TLId
        {
            get { return _tLId; }
            set { SetProperty(ref _tLId, value); }
        }

        /// <summary>
        /// کد حساب
        /// </summary>
        private long _sLCode;

        public long SLCode
        {
            get { return _sLCode; }
            set
            {
                SetProperty(ref _sLCode, value);
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
        /// <summary>
        /// ویژگی ها
        /// </summary>
        private PropertyEnum _property;

        public PropertyEnum Property
        {
            get { return _property; }
            set
            {
                SetProperty(ref _property, value);
            }
        }

        /// <summary>
        /// ماهیت حساب
        /// </summary>
        private AccountsNatureEnum _accountsNatureEnum;

        public AccountsNatureEnum AccountsNatureEnum
        {
            get { return _accountsNatureEnum; }

            set { SetProperty(ref _accountsNatureEnum, value); }
        }
        //private int? _accountsNatureId;

        //public int? AccountsNatureId
        //{
        //    get { return _accountsNatureId; }
        //    set { _accountsNatureId = value; }
        //}

        /// <summary>
        /// تفصیل 1
        /// </summary>
        private DLTypeEnum _dLType1;

        public DLTypeEnum DLType1
        {
            get { return _dLType1; }
            set { SetProperty(ref _dLType1, value); }
        }
        /// <summary>
        /// تفصیل 2
        /// </summary>

        private DLTypeEnum _dLType2;


        public DLTypeEnum DLType2
        {
            get { return _dLType2; }
            set { SetProperty(ref _dLType2, value); }
        }

        /// <summary>
        /// لیستی از انبار
        /// </summary>
        public virtual ICollection<Stock> Stocks { get; set; }
        /// <summary>
        /// لیستی از شرح استاندارد
        /// </summary>
        public virtual ICollection<SLStandardDescription> SLStandardDescriptions { get; set; }

        [InverseProperty("SL")]
        public virtual ICollection<AccDocumentItem> SLs { get; set; }

        public virtual ICollection<StcDocumentHeader> StcDocumentHeaders { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == "Ali")
                yield return (new ValidationResult("عنوان نباید علی باشد...."));
        }
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

        private long _sLCodeEmptyValue;
        [NotMapped]
        public long SLCodeEmptyValue
        {
            get { return _sLCodeEmptyValue; }
            set { SetProperty(ref _sLCodeEmptyValue, value); }
        }
        SLData backupData;
        internal struct SLData
        {
            internal long SLCode;
            internal int TLId;
            internal string Title;
            internal string Title2;
            internal bool Status;
            internal PropertyEnum Property;
            internal AccountsNatureEnum AccountsNatureEnum;
            internal DLTypeEnum DLType1;
            internal DLTypeEnum DLType2;
        }
        void IEditableObject.BeginEdit()
        {
            backupData.SLCode = SLCode;
            backupData.TLId = TLId;
            backupData.Title = Title;
            backupData.Title2 = Title2;
            backupData.Status = Status;
            backupData.Property = Property;
            backupData.AccountsNatureEnum = AccountsNatureEnum;
            backupData.DLType1 = DLType1;
            backupData.DLType2 = DLType2;

        }
        void IEditableObject.EndEdit()
        {
            backupData = new SLData();
        }

        void IEditableObject.CancelEdit()
        {
            SLCode = backupData.SLCode;
            TLId = backupData.TLId;
            Title = backupData.Title;
            Title2 = backupData.Title2;
            Status = backupData.Status;
           Property= backupData.Property  ;
           AccountsNatureEnum= backupData.AccountsNatureEnum ;
            DLType1= backupData.DLType1  ;
            DLType2= backupData.DLType2  ;
        }

    }
}
