using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs
{
    /// <summary>
    /// تنظیمات عمومی سیستم
    /// </summary>
    public class GeneralSystemSettingModel
    {
        /// <summary>
        /// معین پیش پرداخت عوارض
        /// </summary>
        private string _sLPrepaymentComplication;

        public string SLPrepaymentComplication
        {
            get { return _sLPrepaymentComplication; }
            set
            {
                _sLPrepaymentComplication = value;
            }
        }

        /// <summary>
        /// معین پیش پرداخت مالیات
        /// </summary>
        private string _sLPrepaymentTax;

        public string SLPrepaymentTax
        {
            get { return _sLPrepaymentTax; }
            set
            {
                _sLPrepaymentTax = value;
            }
        }


        /// <summary>
        /// معین عوارض پرداختی
        /// </summary>
        private string _sLPayment;

        public string SLPayment
        {
            get { return _sLPayment; }
            set
            {
                _sLPayment = value;
            }
        }

        /// <summary>
        /// معین مالیات پرداختی
        /// </summary>
        private string _sLGeneralTaxPaid;

        public string SLGeneralTaxPaid
        {
            get { return _sLGeneralTaxPaid; }
            set
            {
                _sLGeneralTaxPaid = value;
            }
        }


        /// <summary>
        /// نرخ مالیات
        /// </summary>
        private string _taxRate;

        public string TaxRate
        {
            get { return _taxRate; }
            set
            {
                _taxRate = value;
            }
        }


        /// <summary>
        /// نرخ عوارض
        /// </summary>
        private string _complicationRate;

        public string ComplicationRate
        {
            get { return _complicationRate; }
            set
            {
                _complicationRate = value;
            }
        }



    }
}


