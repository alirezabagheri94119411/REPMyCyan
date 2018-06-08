using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Saina.WPF.Commerce.ProductRacks
{
    class ProductRackListViewModel:BindableBase
    {
       // private ProductRackCollection items;

        public ProductRackListViewModel()
        {
         //   this.items = new ProductRackCollection(null);

            this.BeginLoadingItems();
        }

        //public ProductRackCollection Items
        //{
        //    get
        //    {
        //        return this.items;
        //    }
        //}
        private IEditableCollectionViewAddNewItem items;
        public IEditableCollectionViewAddNewItem Items
        {
            get
            {
                return items;
            }
            set
            {
                SetProperty(ref items, value);
            }
        }
        private void BeginLoadingItems()
        {
            // You can load the items from a web service
            this.ItemsLoaded();
        }

        private void ItemsLoaded()
        {
            using (var uow = new SainaDbContext())
            {
                Items = new QueryableCollectionView(uow.ProductRacks.ToList());
            }
            //ProductRack root = new ProductRack();
            //root.ProductRackTitle = "Personal Folders";
            //root.ImageUrl = "../../Images/ContextMenu/Outlook/1PersonalFolders.png";
            //root.IsExpanded = true;

            //this.Items.Add(root);
        }
    }
}
