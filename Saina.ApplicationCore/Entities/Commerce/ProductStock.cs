using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول واسطه بین کالا و انبار
    /// </summary>
   public class ProductStock:BaseEntity
    {
       
        public int ProductStockId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

    }
}
