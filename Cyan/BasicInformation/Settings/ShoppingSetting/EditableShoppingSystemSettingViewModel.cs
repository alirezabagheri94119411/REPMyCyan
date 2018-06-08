using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Settings.ShoppingSetting
{
   public class EditableShoppingSystemSettingViewModel:ValidatableBindableBase
    {
        /// <summary>
        /// طول کد کالا
        /// </summary>
        private string _productCodeLenght;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string ProductCodeLenght
        {
            get { return _productCodeLenght; }
            set { SetProperty(ref _productCodeLenght, value); }
        }
        /// <summary>
        /// طول نوع کالا 
        /// </summary>
        private string _productTypeLenght;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string ProductTypeLenght
        {
            get { return _productTypeLenght; }
            set { SetProperty(ref _productTypeLenght, value); }
        }
        private string _productBrandLenght;
        /// <summary>
        /// طول برند کالا
        /// </summary>
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string ProductBrandLenght
        {
            get { return _productBrandLenght; }
            set { SetProperty(ref _productBrandLenght, value); }
        }
        private string _productModelLenght;
        /// <summary>
        /// طول مدل کالا
        /// </summary>
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string ProductModelLenght
        {
            get { return _productModelLenght; }
            set { SetProperty(ref _productModelLenght, value); }
        }
        private string _otherProductLenght;
        /// <summary>
        /// طول سایر کالا
        /// </summary>
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string OtherProductLenght
        {
            get { return _otherProductLenght; }
            set { SetProperty(ref _otherProductLenght, value); }
        }
     


        /// <summary>
        /// طول ایران کد
        /// </summary>
        private string _iranCodeProduct;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string IranCodeProduct
        {
            get { return _iranCodeProduct; }
            set { SetProperty(ref _iranCodeProduct, value); }
        }
        private string _numberLevel;
        /// <summary>
        /// طبقه کالا
        /// </summary>
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]
        public string NumberLevel
        {
            get { return _numberLevel; }
            set { SetProperty(ref _numberLevel, value); }
        }
        /// <summary>
        /// ارتباط کالا
        /// </summary>
        /// 
        private string _productCommunication;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]

        public string ProductCommunication
        {
            get { return _productCommunication; }
            set { SetProperty(ref _productCommunication, value); }
        }
        /// <summary>
        /// بارکد
        /// </summary>
        private string _Barcode;
        [Required(ErrorMessage = "عدد خود را وارد نمایید.")]

        public string Barcode
        {
            get { return _Barcode; }
            set { SetProperty(ref _Barcode, value); }
        }
        private string _Status;

        /// <summary>
        /// وضعیت
        /// </summary>
        public string StatusShopping
        {
            get { return _Status; }
            set { _Status = value; }
        }

    }
}
