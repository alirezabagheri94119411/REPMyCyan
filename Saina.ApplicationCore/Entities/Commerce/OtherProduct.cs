using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول سایر کالا
    /// </summary>
   public class OtherProduct:BaseEntity
    {
        private int _otherProductId;
        /// <summary>
        /// آی دی سایر کالا
        /// </summary>
        public int OtherProductId
        {
            get { return _otherProductId; }
            set { SetProperty(ref _otherProductId, value); }
        }
        private int _otherProductCode;
        public int OtherProductCode
        {
            get { return _otherProductCode; }
            set { SetProperty(ref _otherProductCode, value); }
        }

        private string _otherProductTitle;
        /// <summary>
        /// عنوان سایر کالا
        /// </summary>
        public string OtherProductTitle
        {
            get { return _otherProductTitle; }
            set { SetProperty(ref _otherProductTitle, value); }
        }
        private bool _requiredCode;
        /// <summary>
        /// اجباری در کد
        /// </summary>
        public bool RequiredCode
        {
            get { return _requiredCode; }
            set { SetProperty(ref _requiredCode, value); }
        }
        private bool _requiredDocument;
        /// <summary>
        /// اجباری در اسناد
        /// </summary>
        public bool RequiredDocument
        {
            get { return _requiredDocument; }
            set { SetProperty(ref _requiredDocument, value); }
        }



    }
}
