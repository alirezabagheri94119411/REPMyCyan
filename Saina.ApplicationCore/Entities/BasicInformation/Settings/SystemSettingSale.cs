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
    /// تنظیمات سیستم فروش
    /// </summary>
    public class SystemSettingSale:BaseEntity
    {
        /// <summary>
        /// معین حساب دریافت/
        /// </summary>
        private string _sLReceiveAccount;

        public string SLReceiveAccount
        {
            get { return _sLReceiveAccount; }
            set { _sLReceiveAccount = value; }
        }

        /// <summary>
        /// تخفیفات
        /// </summary>
        private string _discount;

        public string Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        /// <summary>
        /// معین حساب فروش
        /// </summary>
        private string _sLSalesAccount;

        public string SLSalesAccount
        {
            get { return _sLSalesAccount; }
            set { _sLSalesAccount = value; }
        }

        /// <summary>
        /// تخفیف خدمات
        /// </summary>
        private string _sLDiscountService;

        public string SLDiscountService
        {
            get { return _sLDiscountService; }
            set { _sLDiscountService = value; }
        }

        /// <summary>
        /// خدمات
        /// </summary>
        private string _sLService;

        public string SLService
        {
            get { return _sLService; }
            set { _sLService = value; }
        }


        /// <summary>
        /// برگشت از فروش
        /// </summary>
        private string _sLReturnSale;

        public string SLReturnSale
        {
            get { return _sLReturnSale; }
            set { _sLReturnSale = value; }
        }


        /// <summary>
        /// تخفیف
        /// </summary>
        private string _sLDiscount;

        public string SLDiscount
        {
            get { return _sLDiscount; }
            set { _sLDiscount = value; }
        }

        /// <summary>
        /// اضافات
        /// </summary>
        private string _sLAddition;

        public string SLAddition
        {
            get { return _sLAddition; }
            set { _sLAddition = value; }
        }

        /// <summary>
        /// معین حساب پورسانت(پرداختی)
        /// </summary>
        private string _sLCommissionPaid;

        public string SLCommissionPaid
        {
            get { return _sLCommissionPaid; }
            set { _sLCommissionPaid = value; }
        }


        /// <summary>
        /// معین حساب پورسانت هزینه)
        /// </summary>
        private string _sLCommissionCost;

        public string SLCommissionCost
        {
            get { return _sLCommissionCost; }
            set { _sLCommissionCost = value; }
        }



        /// <summary>
        /// تنظیمات فاکتور و برگشت از فاکتور برای حواله خروج وورود از انبار
        /// </summary>
        private string _invoiceSetting;

        public string InvoiceSetting
        {
            get { return _invoiceSetting; }
            set { _invoiceSetting = value; }
        }

       
    }
}
