using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول مدل کالا
    /// </summary>
   public class ProductModel:BaseEntity
    {
        private int _productModelId;
        /// <summary>
        /// آی دی مدل کالا
        /// </summary>
        public int ProductModelId
        {
            get { return _productModelId; }
            set { SetProperty(ref _productModelId, value); }
        }
        private int _productModelCode;
        public int ProductModelCode
        {
            get { return _productModelCode; }
            set { SetProperty(ref _productModelCode, value); }
        }

        private string _productModelTitle;
        /// <summary>
        /// عنوان مدل کالا
        /// </summary>
        public string ProductModelTitle
        {
            get { return _productModelTitle; }
            set { SetProperty(ref _productModelTitle, value); }
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
