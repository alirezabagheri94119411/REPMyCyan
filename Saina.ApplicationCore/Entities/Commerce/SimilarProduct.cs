using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول کالای مشابه
    /// </summary>
   public class SimilarProduct:BaseEntity
    {

        /// <summary>
        /// آی دی محصولات مشابه
        /// </summary>
        private int _SimilarProductId;
        public int SimilarProductId
        {
            get { return _SimilarProductId; }
            set { SetProperty(ref _SimilarProductId, value); }
        }


        /// <summary>
        /// کالا
        /// </summary>
        ///
        private Product _Product;
        public virtual Product Product
        {
            get { return _Product; }
            set { SetProperty(ref _Product, value); }
        }
        private int? _ProductId;
        public int? ProductId
        {
            get { return _ProductId; }
            set { SetProperty(ref _ProductId, value); }
        }

        /// <summary>
        /// نسبت مقداری
        /// </summary>
        private string _Ratio;
        public string Ratio
        {
            get { return _Ratio; }
            set { SetProperty(ref _Ratio, value); }
        }


        /// <summary>
        /// الویت
        /// </summary>
        private int _Order;
        public int Order
        {
            get { return _Order; }
            set { SetProperty(ref _Order, value); }
        }

      

      

    }
}
