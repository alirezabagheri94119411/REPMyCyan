using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Saina.Data.Context;
using Telerik.Windows.Data;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System.ComponentModel;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SainaDbContext _uow;
        private CollectionViewSource accDocumentHeaderViewSource;
        private IEditableCollectionViewAddNewItem _virtualQueryableCollectionView;

        public Window1()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _uow = SmObjectFactory.Container.GetInstance<Saina.Data.Context.SainaDbContext>();
            typeDocumentIdComboBox.ItemsSource = await _uow.TypeDocuments.ToListAsync();
            _virtualQueryableCollectionView =new QueryableCollectionView( _uow.AccDocumentHeaders.ToList()); //new VirtualQueryableCollectionView(_uow.AccDocumentHeaders.OrderByDescending(x => x.AccDocumentHeaderId)) { LoadSize = 10 };
            mainGrid.DataContext = _virtualQueryableCollectionView;

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ////await _uow.SaveChangesAsync();
            //_virtualQueryableCollectionView.AddNew(new AccDocumentHeader() {TypeDocumentId=1 });
            //_virtualQueryableCollectionView.VirtualItemCount++;
            //AccDocumentHeaderRadGridView.Rebind();
            ////_virtualQueryableCollectionView.MoveCurrentToNext();
            //_uow.AccDocumentHeaders.Add(new AccDocumentHeader { TypeDocumentId = 1 });
            var r = _uow.SaveChanges();

        }

        private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            var r=_uow.SaveChanges();
        }

        private void RadDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            _uow.AccDocumentHeaders.Add((AccDocumentHeader)e.NewItem);
        }

        private void RadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            _uow.AccDocumentHeaders.Remove((AccDocumentHeader)e.DeletedItem);
            var r = _uow.SaveChanges();


        }

        private void accDocumentHeaderDataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            //_uow.AccDocumentHeaders.Add((AccDocumentHeader)e.NewObject);
            AccDocumentAccDocumentHeaderDataForm.AddNewItem();
        }

        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < AccDocumentHeaderRadGridView.SelectedItems.Count; i++)
            //{
            //_uow.AccDocumentHeaders.Remove((AccDocumentHeader)AccDocumentHeaderRadGridView.SelectedItems[i]);

            //}

            // _uow.AccDocumentHeaders.Remove((AccDocumentHeader)AccDocumentHeaderRadGridView.SelectedItem);
           
            AccDocumentAccDocumentHeaderDataForm.DeleteItem();
            var r = _uow.SaveChanges();
        }
    }
}
