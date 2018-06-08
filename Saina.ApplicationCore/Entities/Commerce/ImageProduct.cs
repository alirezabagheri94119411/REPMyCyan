using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public class ImageProduct: BaseEntity
    {
   
       
        private int _imageProductId;
 /// <summary>
        /// آی دی تصویر کالا
        /// </summary>
        public int ImageProductId
        {
            get { return _imageProductId; }
            set { _imageProductId = value; }
        }
        private string _picture;
        /// <summary>
        /// تصویر کالا
        /// </summary>
        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        /// <summary>
        /// ارتباط با کالا
        /// </summary>
        public virtual Product Product { get; set; }
       
    }
}
