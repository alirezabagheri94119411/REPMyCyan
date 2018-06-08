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
    /// تنظیمات سیستم دریافت و پرداخت
    /// </summary>
    public class SystemReceivePaymentSetting : BaseEntity
    {

        /// <summary>
        /// معین دریافت موجودیبانکها
        /// </summary>
        private string _sLReceiveStockBank;

        public string SLReceiveStockBank
        {
            get { return _sLReceiveStockBank; }
            set { _sLReceiveStockBank = value; }

        }

        /// <summary>
        /// معین پرداخت موجودی بانک
        /// </summary>
        private string _sLPaymentStockBank;

        public string SLPaymentStockBank
        {
            get { return _sLPaymentStockBank; }
            set { _sLPaymentStockBank = value; }

        }
        /// <summary>
        /// معین دریافت صندوق
        /// </summary>
        private string _sLReceiveCashDesk;

        public string SLReceiveCashDesk
        {
            get { return _sLReceiveCashDesk; }
            set { _sLReceiveCashDesk = value; }

        }

        /// <summary>
        /// معین پرداخت صندوق
        /// </summary>
        private string _sLPaymentCashDesk;

        public string SLPaymentCashDesk
        {
            get { return _sLPaymentCashDesk; }
            set { _sLPaymentCashDesk = value; }

        }

        /// <summary>
        ///معین دریافت کارت  خوان
        /// </summary>
        private string _sLReceiveCardReader;

        public string SLReceiveCardReader
        {
            get { return _sLReceiveCardReader; }
            set { _sLReceiveCardReader = value; }

        }

        /// <summary>
        /// معین پرداخت کارت خوان
        /// </summary>
        private string _sLPaymentCardReader;

        public string SLPaymentCardReader
        {
            get { return _sLPaymentCardReader; }
            set { _sLPaymentCardReader = value; }

        }

        /// <summary>
        /// معین دریافت اسناد نزد صندوق
        /// </summary>
        private string _sLReceiveDocumentFund;

        public string SLReceiveDocumentFund
        {
            get { return _sLReceiveDocumentFund; }
            set { _sLReceiveDocumentFund = value; }

        }

        /// <summary>
        /// معین پرداخت اسناد نزد صندوق
        /// </summary>
        private string _sLPaymentDocumentFund;

        public string SLPaymentDocumentFund
        {
            get { return _sLPaymentDocumentFund; }
            set { _sLPaymentDocumentFund = value; }

        }


        /// <summary>
        /// معین دریافت چکهای در جریان
        /// </summary>
        private string _sLReceiveCheckStream;

        public string SLReceiveCheckStream
        {
            get { return _sLReceiveCheckStream; }
            set { _sLReceiveCheckStream = value; }

        }

        /// <summary>
        /// معین پرداخت چکهای در جریان
        /// </summary>
        private string _sLPaymentCheckStream;

        public string SLPaymentCheckStream
        {
            get { return _sLPaymentCheckStream; }
            set { _sLPaymentCheckStream = value; }

        }

        /// <summary>
        ///معین دریافت  چکهای مدت دار
        /// </summary>
        private string _sLReceiveTimedCheck;

        public string SLReceiveTimedCheck
        {
            get { return _sLReceiveTimedCheck; }
            set { _sLReceiveTimedCheck = value; }

        }

        /// <summary>
        /// معین پرداخت  چکهای مدت دار
        /// </summary>
        private string _sLPaymentTimedCheck;

        public string SLPaymentTimedCheck
        {
            get { return _sLPaymentTimedCheck; }
            set { _sLPaymentTimedCheck = value; }

        }

        /// <summary>
        //معین دریافت /اسناد واخواست
        /// </summary>
        private string _sLReceiveWillingDocument;

        public string SLReceiveWillingDocument
        {
            get { return _sLReceiveWillingDocument; }
            set { _sLReceiveWillingDocument = value; }

        }

        /// <summary>
        /// معین پرداخت /اسناد واخواست
        /// </summary>
        private string _sLPaymentWillingDocument;

        public string SLPaymentWillingDocument
        {
            get { return _sLPaymentWillingDocument; }
            set { _sLPaymentWillingDocument = value; }

        }

        /// <summary>
        /// /معین دریافت طرف حساب انتظامی
        /// </summary>
        private string _sLReceiveLawOfficer;

        public string SLReceiveLawOfficer
        {
            get { return _sLReceiveLawOfficer; }
            set { _sLReceiveLawOfficer = value; }

        }

        /// <summary>
        /// معین پرداخت طرف حساب انتظامی
        /// </summary>
        private string _sLPaymentLawOfficer;

        public string SLPaymentLawOfficer
        {
            get { return _sLPaymentLawOfficer; }
            set { _sLPaymentLawOfficer = value; }

        }

        /// <summary>
        /// اسناد خودکار
        /// رسید دریافت
        /// </summary>
        private string _receipt;

        public string Receipt
        {
            get { return _receipt; }
            set { _receipt = value; }

        }

        /// <summary>
        /// اعلامیه پرداخت
        /// </summary>
        private string _PaymentNotice;

        public string PaymentNotice
        {
            get { return _PaymentNotice; }
            set { _PaymentNotice = value; }

        }

        /// <summary>
        /// چک دریافتی
        /// </summary>
        private string _checkReceived;

        public string CheckReceived
        {
            get { return _checkReceived; }
            set { _checkReceived = value; }

        }


        /// <summary>
        /// وصول چک
        /// </summary>
        private string _gettingCheck;

        public string GettingCheck
        {
            get { return _gettingCheck; }
            set { _gettingCheck = value; }

        }

        /// استرداد چک
        /// </summary>
        private string _checkExtra;

        public string CheckExtra
        {
            get { return _checkExtra; }
            set { _checkExtra = value; }

        }


    }
}
