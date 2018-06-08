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
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saina.WPF.BasicInformation.Admin.UserAccess
{
    /// <summary>
    /// Interaction logic for permission.xaml
    /// </summary>
    public partial class permission : RadWindow
    {

        private DataViewModel _vm;

        public permission()
        {
            InitializeComponent();
            _vm = new DataViewModel();
            DataContext = _vm;
            Loaded += (s, ea) =>
            {
                // _vm.ItemsLoaded();
            };

        }



        private RadTreeViewItem ClickedTreeViewItem
        {
            get
            {
                return ContextMenu.GetClickedElement<RadTreeViewItem>();
            }
        }
        private void ContextMenuClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string tag = (e.OriginalSource as RadMenuItem).Tag as string;
            List<int> operationIds = new List<int>();
            if (ClickedTreeViewItem.DataContext is Owner)
            {
                var owner = ClickedTreeViewItem.DataContext as Owner;
                operationIds = owner.Views.SelectMany(x => x.Operations).Select(x => x.OperationId).ToList();
            }
            else if (ClickedTreeViewItem.DataContext is View)
            {
                var window = ClickedTreeViewItem.DataContext as View;
                operationIds = window.Operations.Select(x => x.OperationId).ToList();
            }
            else if (ClickedTreeViewItem.DataContext is Operation)
            {
                var operation = ClickedTreeViewItem.DataContext as Operation;
                //////if (tag == "allow")
                //////{
                ////    _vm.ApplyPermission(new List<int> { action.Id}, tag == "allow");
                //////}
                //////else if (tag == "deny")
                //////{
                //////}
                operationIds = new List<int> { operation.OperationId };
            }

            _vm.ApplyPermission(operationIds, tag == "allow");
            MessageBox.Show("دسترسی ها اعمال شد");
        }

        private void OperationRadToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var operationId = ((Operation)((RadToggleButton)sender).DataContext).OperationId;
            _vm.ApplyPermission(new List<int> {operationId }, true,false);
        }

        private void OperationRadToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var operationId = ((Operation)((RadToggleButton)sender).DataContext).OperationId;
            _vm.ApplyPermission(new List<int> { operationId }, false, false);

        }
    }
    public class DataViewModel
    {
        public DataViewModel()
        {
            items = new ObservableCollection<Owner>();
            ItemsLoaded();
        }

        private ObservableCollection<Owner> items;

        public ObservableCollection<Owner> Items
        {
            get
            {
                return items;
            }
        }
        private User _SelectedUser;
        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value; if (_SelectedUser == null) return;
                using (var uow = new SainaDbContext())
                {

                    uow.Owners.Include(x => x.Views.Select(y => y.Operations)).Load();
                    items = uow.Owners.Local;
                    items.SelectMany(x => x.Views).SelectMany(x => x.Operations).ToList().ForEach(x => x.HasAccess = SelectedUser.Accesses.First(y => y.OperationId == x.OperationId).HasAccess);
                }
            }
        }

        public void ItemsLoaded()
        {
            //using (var uow = new SainaDbContext())
            //{
            //    uow.Owners.Include(x => x.Views.Select(y => y.Operations)).Load();
            //    items = uow.Owners.Local;
            //}
        }

        public void ApplyPermission(List<int> actionIds, bool hasAccess, bool isMenu = true)
        {
            using (var uow = new SainaDbContext())
            {
                uow.Accesses.Where(x => actionIds.Contains(x.OperationId ?? 0)).ToList().ForEach(x => x.HasAccess = hasAccess);
                if (isMenu)
                    items.SelectMany(x => x.Views).SelectMany(x => x.Operations).Where(x => actionIds.Contains(x.OperationId)).ToList().ForEach(x => x.HasAccess = hasAccess);
                uow.SaveChanges();

            }
        }
    }
    public class DataItem : INotifyPropertyChanged
    {
        private bool isExpanded;
        private string imageUrl;

        public event PropertyChangedEventHandler PropertyChanged;
        [NotMapped]
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                if (imageUrl != value)
                {
                    imageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }
        [NotMapped]
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

