using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Settings.RetailSetting
{
    /// <summary>
    /// سایز پیش فاکتور
    /// </summary>
   public class Invoice
    {
        /// <summary>
        /// آی دی پیش فاکتور
        /// </summary>
        private string _invoiceId;

        public string InvoiceId
        {
            get { return _invoiceId; }
            set { _invoiceId = value; }
        }
        /// <summary>
        /// عنوان پیش فاکتور
        /// </summary>
        private string _invoiceTitle;

        public string InvoiceTitle
        {
            get { return _invoiceTitle; }
            set { _invoiceTitle = value; }
        }


    }
}
