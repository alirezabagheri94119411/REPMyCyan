using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول نوع کالا
    /// </summary>
   public class ProductType:BaseEntity
    {
        private int _productTypeId;
        /// <summary>
        /// آی دی نوع کالا
        /// </summary>
        public int ProductTypeId
        {
            get { return _productTypeId; }
            set { SetProperty(ref _productTypeId, value); }
        }
        private int _productTypeCode;
        public int ProductTypeCode
        {
            get { return _productTypeCode; }
            set { SetProperty(ref _productTypeCode, value); }
        }

        private string _productTypeTitle;
        /// <summary>
        /// عنوان نوع کالا
        /// </summary>
        public string ProductTypeTitle
        {
            get { return _productTypeTitle; }
            set { SetProperty(ref _productTypeTitle, value); }
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
