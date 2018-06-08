using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs
{
    /// <summary>
    /// تنظیمات سیستم بازرگانی
    /// </summary>
    public class ShoppingSystemSettingModel
    {
        /// <summary>
        /// معین حساب های پرداختنی
        /// </summary>
        private string _sLPayableAccount;

        public string SLPayableAccount
        {
            get { return _sLPayableAccount; }
            set { _sLPayableAccount = value; }

        }


        /// <summary>
        /// معین تخفیفات نقدی خرید
        /// </summary>
        private string _sLCashbackShopping;

        public string SLCashbackShopping
        {
            get { return _sLCashbackShopping; }
            set { _sLCashbackShopping = value; }

        }

        /// <summary>
        /// معین هزینه ی حمل
        /// </summary>
        private string _sLTransportationCost;

        public string SLTransportationCost
        {
            get { return _sLTransportationCost; }
            set { _sLTransportationCost = value; }

        }


        /// <summary>
        /// معیین برگشت رسید حمل
        /// </summary>
        private string _sLReturnShippingArrival;

        public string SLReturnShippingArrival
        {
            get { return _sLReturnShippingArrival; }
            set { _sLReturnShippingArrival = value; }

        }

        /// <summary>
        /// سایر حساب های دریافتنی
        /// </summary>
        private string _otherAccountsReceivable;

        public string OtherAccountsReceivable
        {
            get { return _otherAccountsReceivable; }
            set { _otherAccountsReceivable = value; }

        }


        /// <summary>
        /// بهای تمام شده
        /// </summary>
        private string _costing;

        public string Costing
        {
            get { return _costing; }
            set { _costing = value; }

        }


        /// <summary>
        /// موجودی منفی کنترل شود
        /// </summary>
        private string _negativeInventoryControl;

        public string NegativeInventoryControl
        {
            get { return _negativeInventoryControl; }
            set { _negativeInventoryControl = value; }

        }


        /// <summary>
        /// اسناد حسابداری اتوماتیک
        /// </summary>

        private string _automaticAccountingDocument;

        public string AutomaticAccountingDocument
        {
            get { return _automaticAccountingDocument; }
            set { _automaticAccountingDocument = value; }
        }
        /// <summary>
        /// طول کد کالا
        /// </summary>
        private string _productCodeLenght;
        public string ProductCodeLenght
        {
            get { return _productCodeLenght; }
            set { _productCodeLenght= value; }
        }
        /// <summary>
        /// طول نوع کالا 
        /// </summary>
        private string  _productTypeLenght;

        public string ProductTypeLenght
        {
            get { return _productTypeLenght; }
            set { _productTypeLenght = value; }
        }
        private string _productBrandLenght;
        /// <summary>
        /// طول برند کالا
        /// </summary>
        public string ProductBrandLenght
        {
            get { return _productBrandLenght; }
            set { _productBrandLenght = value; }
        }
        private string _productModelLenght;
        /// <summary>
        /// طول مدل کالا
        /// </summary>
        public string ProductModelLenght
        {
            get { return _productModelLenght; }
            set { _productModelLenght = value; }
        }
        private string _otherProductLenght;
        /// <summary>
        /// طول سایر کالا
        /// </summary>
        public string OtherProductLenght
        {
            get { return _otherProductLenght; }
            set { _otherProductLenght = value; }
        }



        /// <summary>
        /// طول ایران کد
        /// </summary>
        private string _iranCodeProduct;
        public string IranCodeProduct
        {
            get { return _iranCodeProduct; }
            set { _iranCodeProduct= value; }
        }
        /// <summary>
        /// ارتباط کالا
        /// </summary>
        private string _productCommunication;
        public string ProductCommunication
        {
            get { return _productCommunication; }
            set {  _productCommunication =value; }
        }
        /// <summary>
        /// ارتباط اسناد
        /// </summary>
        private string _documentCommunication;
        public string DocumentCommunication
        {
            get { return _documentCommunication; }
            set {  _documentCommunication= value; }
        }
        private string _numberLevel;
        /// <summary>
        /// طبقه کالا
        /// </summary>
        public string NumberLevel
        {
            get { return _numberLevel; }
            set {  _numberLevel= value; }
        }
        /// <summary>
        /// بارکد
        /// </summary>

        private string _Barcode;

        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
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
