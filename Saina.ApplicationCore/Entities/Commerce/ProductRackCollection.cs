using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public class ProductRackCollection: ObservableCollection<ProductRack>
    {

        private int _productRackCollectionId;

        public int ProductRackCollectionId
        {
            get { return _productRackCollectionId; }
            set { _productRackCollectionId = value; }
        }

        public ProductRackCollection(ProductRack owner)
        {
            this.Owner = owner;
        }
        protected override void SetItem(int index, ProductRack item)
        {
            item.Parent = this.Owner;

            base.SetItem(index, item);
        }

        protected override void ClearItems()
        {
            foreach (ProductRack item in this)
            {
                item.Parent = null;
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, ProductRack item)
        {
            item.Parent = this.Owner;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Parent = null;

            base.RemoveItem(index);
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private ProductRack _owner;
        public ProductRack Owner
        {
            get { return _owner; }
            set { _owner= value; }
        }

    }
}
