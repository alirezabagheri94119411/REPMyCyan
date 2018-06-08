using Saina.ApplicationCore.Entities.Commerce;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Commerce.CommStock
{
   public class EditableStock:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی انبار
        /// </summary>
        private int _stockId;
        public int StockId
        {
            get { return _stockId; }
            set { SetProperty(ref _stockId, value); }
        }

        /// <summary>
        /// کد انبار
        /// </summary>
        private string _stockCode;
        [Required(ErrorMessage = "لطفا کد انبار را انتخاب نمایید.")]
        public string StockCode
        {
            get { return _stockCode; }
            set { SetProperty(ref _stockCode, value); }
        }

        /// <summary>
        /// عنوان انبار
        /// </summary>
        private string _stockTitle1;
        [Required(ErrorMessage = "لطفاعنوان انبار را وارد نمایید.")]
        public string StockTitle1
        {
            get { return _stockTitle1; }
            set { SetProperty(ref _stockTitle1, value); }
        }

        /// <summary>
        /// 2عنوان انبار
        /// </summary>
        private string _stockTitle2;
        public string StockTitle2
        {
            get { return _stockTitle2; }
            set { SetProperty(ref _stockTitle2, value); }
        }

        /// <summary>
        /// آی دی مسئول انبار
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
            set { SetProperty(ref _phone, value); }
        }

        /// <summary>
        /// آدرس
        /// </summary>
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        /// <summary>
        /// آی دی حساب معین
        /// </summary>
        private int _sLId;
        [Required(ErrorMessage = "لطفا حساب معین خود را انتخاب نمایید.")]
        public int SLId
        {
            get { return _sLId; }
            set { SetProperty(ref _sLId, value); }
        }

        /// <summary>
        /// لیست کالا
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
