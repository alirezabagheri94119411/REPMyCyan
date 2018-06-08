using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Settings
{
    /// <summary>
    /// اسناد حسابداری اتوماتیک
    /// </summary>
    //public enum AutomaticAccountingDocuments
    // {
    //     رسیدازانبار,
    //         برگشتازرسید,
    //         هردو
    // }
    /// <summary>
    /// تنظیمات سیستم بازرگانی خرید
    /// </summary>
    public class ShoppingSystemSetting
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
    }
}