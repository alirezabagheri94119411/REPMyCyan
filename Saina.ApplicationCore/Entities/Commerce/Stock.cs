using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// /انبار
    /// </summary>
    public class Stock : BaseEntity
    {
        public Stock()
        {
            Products = new ObservableCollection<Product>();
        }

        /// <summary>
        /// آی دی انبار
        /// </summary>
        private int _stockId;
        public int StockId
        {
            get { return _stockId; }
            set { _stockId = value; }
        }

        /// <summary>
        /// کد انبار
        /// </summary>
        private string _stockCode;
        public string StockCode
        {
            get { return _stockCode; }
            set { _stockCode = value; }
        }
        
        /// <summary>
        /// عنوان انبار
        /// </summary>
        private string _stockTitle1;
        public string StockTitle1
        {
            get { return _stockTitle1; }
            set { _stockTitle1 = value; }
        }

        /// <summary>
        /// 2عنوان انبار
        /// </summary>
        private string _stockTitle2;
        public string StockTitle2
        {
            get { return _stockTitle2; }
            set { _stockTitle2 = value; }
        }

        /// <summary>
        /// مسئول انبار
        /// </summary>
        private User _user;
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }

        /// <summary>
        /// مسئول انبارآی دی
        /// </summary>
        private int? _userId;
        public int? UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// تلفن
        /// </summary>
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        /// <summary>
        /// آدرس
        /// </summary>
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        
        /// <summary>
        /// حساب معین
        /// </summary>
        private SL _sL;
        public virtual SL SL
        {
            get { return _sL; }
            set { _sL = value; }
        }

        /// <summary>
        /// آی دی حساب معین
        /// </summary>
        private int _sLId;
        public int SLId
        {
            get { return _sLId; }
            set { _sLId = value; }
        }

        private bool _IsSelected;
        [NotMapped]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { SetProperty(ref _IsSelected, value); }
        }

        /// <summary>
        /// لیست کالا
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
