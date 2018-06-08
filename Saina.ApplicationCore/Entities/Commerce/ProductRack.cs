using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول قفسه کالا
    /// </summary>

    public class ProductRack:BaseEntity
    {
        public ProductRack()
        {
            Products = new ObservableCollection<Product>();
           _items = new ProductRackCollection(this);
        }
        private int _productRackId;
        /// <summary>
        /// کد قفسه کالا
        /// </summary>
        public int ProductRackId
        {
            get { return _productRackId; }
            set { SetProperty(ref _productRackId, value); }
        }
        private string _productRackTitle;
        /// <summary>
        /// عنوان قفسه کالا
        /// </summary>
        public string ProductRackTitle
        {
            get { return _productRackTitle; }
            set { SetProperty(ref _productRackTitle, value); }
        }
        private ProductRack _parent;
        public ProductRack Parent
        {
            get { return _parent; }
            set { SetProperty(ref _parent, value); }
        }
        private ProductRackCollection _items;
        public  ProductRackCollection Items
        {
            get { return this._items; }
           
        }

        private bool isExpanded;
        private string imageUrl;



        private string _imageUrl;
        [NotMapped]

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private bool _isExpanded;

        [NotMapped]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        public virtual ICollection<Product> Products { get; set; }
    }
}
