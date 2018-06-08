using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.Commerce.ProductRacks
{
    /// <summary>
    /// Interaction logic for ProductRackListView.xaml
    /// </summary>
    public partial class ProductRackListView : UserControl
    {
        private ProductRackListViewModel _vm;

        public ProductRackListView()
        {
            InitializeComponent();
            _vm = new ProductRackListViewModel();
            DataContext = _vm;
        }
        private RadTreeViewItem ClickedTreeViewItem
        {
            get
            {
                return this.ContextMenu.GetClickedElement<RadTreeViewItem>();
            }
        }

        private void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedTreeViewItem != null)
            {
                ProductRack dataItem = this.ClickedTreeViewItem.DataContext as ProductRack;

                // We will disable the "New Sibling" and "Delete" context menu 
                // items if the clicked treeview item is a root item
                bool isRootItem = dataItem.Parent == null;

                (this.ContextMenu.Items[1] as RadMenuItem).IsEnabled = !isRootItem; // New Sibling
                (this.ContextMenu.Items[4] as RadMenuItem).IsEnabled = !isRootItem; // Delete
            }
        }

        private void ContextMenuClick(object sender, RoutedEventArgs e)
        {
            if (this.ClickedTreeViewItem == null) return;

            ProductRack item = this.ClickedTreeViewItem.DataContext as ProductRack;

            if (item == null) return;

            string header = (e.OriginalSource as RadMenuItem).Header as string;
            switch (header)
            {
                case "New Child":
                    ChildListWindow childListWindow = SmObjectFactory.Container.GetInstance<ChildListWindow>();

                    var tree = childListWindow.DataContext as ChildListWindowViewModel;

                    childListWindow.ShowDialog();
                    ProductRack newChild = new ProductRack();
                    newChild.ProductRackTitle = childListWindow.titleTextbox.Text;
                    using (var uow = new SainaDbContext())
                    {
                        // uow.ProductRacks.Include(Parent).Add(Parent(newChild));
                        item.Items.Add(newChild);

                        uow.ProductRacks.Add(item);
                            uow.SaveChanges();
                        item.IsExpanded = true; // Ensure that the new child is visible
                      
                    }
           
                  //  item.Items.Add(newChild)
               
                    break;
                case "New Sibling":
                     childListWindow = SmObjectFactory.Container.GetInstance<ChildListWindow>();
                    ProductRack newSibling = new ProductRack();
                    newSibling.ProductRackTitle = childListWindow.titleTextbox.Text;

                    item.Parent.Items.Add(newSibling);
                    break;
                case "Delete":
                    using (var uow = new SainaDbContext())
                    {
                        item.Parent.Items.Remove(item);
                    //    uow.ProductRacks.Remove(item);
                        uow.SaveChanges();
                    }

                    break;
                case "Edit":
                    using (var uow = new SainaDbContext())
                    {
                        this.ClickedTreeViewItem.IsInEditMode = true;
                        uow.SaveChanges();
                    }
                    break;
                case "Select":
                    this.ClickedTreeViewItem.IsSelected = true;
                    break;
            }
        }
    }
}
//tree.SaveClicked += (p) =>
//{

//    p.ProductRackTitle = item.ProductRackTitle;
//    p.ImageUrl = "../../Resources/cian.png";

//    item.IsExpanded = true; // Ensure that the new child is visible
//    using (var uow = new SainaDbContext())
//    {
//        uow.ProductRacks.Add(p);
//        uow.SaveChanges();
//        childListWindow.Close();
//    }
//};