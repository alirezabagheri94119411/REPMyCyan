using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public class StcDocumentItem:BaseEntity
    {
        private int _StcDocumentItemId;
        public int StcDocumentItemId
        {
            get { return _StcDocumentItemId; }
            set { SetProperty(ref _StcDocumentItemId, value); }
        }
        private Stock _Stock;
        public Stock Stock
        {
            get { return _Stock; }
            set { SetProperty(ref _Stock, value); }
        }
        private int _StockId;
        public int StockId
        {
            get { return _StockId; }
            set { SetProperty(ref _StockId, value); }
        }
        private BaseDocumentEnum _BaseDocumentEnum;
        /// <summary>
        /// سند مبنا
        /// </summary>
        public BaseDocumentEnum BaseDocumentEnum
        {
            get { return _BaseDocumentEnum; }
            set { SetProperty(ref _BaseDocumentEnum, value); }
        }
        private Product _Product;
        public Product Product
        {
            get { return _Product; }
            set { SetProperty(ref _Product, value); }
        }
        private int _ProductId;
        public int ProductId
        {
            get { return _ProductId; }
            set { SetProperty(ref _ProductId, value); }
        }
        private long _Barcode;
        public long Barcode
        {
            get { return _Barcode; }
            set { SetProperty(ref _Barcode, value); }
        }


    }
}
