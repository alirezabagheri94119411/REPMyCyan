using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public class ProductBrand:BaseEntity
    {
        private int _productBrandId;
        /// <summary>
        /// آی دی برند کالا
        /// </summary>
        public int ProductBrandId
        {
            get { return _productBrandId; }
            set { SetProperty(ref _productBrandId, value); }
        }
        private int _productBrandCode;
        public int ProductBrandCode
        {
            get { return _productBrandCode; }
            set { SetProperty(ref _productBrandCode, value); }
        }

        private string _productBrandTitle;
        /// <summary>
        /// عنوان نوع کالا
        /// </summary>
        public string ProductBrandTitle
        {
            get { return _productBrandTitle; }
            set { SetProperty(ref _productBrandTitle, value); }
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
