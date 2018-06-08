using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Commerce.CommProduct
{
   public class EditableProduct:ValidatableBindableBase 
    {
        /// <summary>
        /// آی دی کالا
        /// </summary>
        private int _productId;

        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }
        /// <summary>
        /// عنوان کالا
        /// </summary>
        private string _productTitle;

        public string ProductTitle
        {
            get { return _productTitle; }
            set { SetProperty(ref _productTitle, value); }
        }
    }
}
