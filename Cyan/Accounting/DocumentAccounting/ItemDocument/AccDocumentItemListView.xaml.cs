using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Saina.WPF.Accounting.DocumentAccounting.ItemDocument
{
    /// <summary>
    /// Interaction logic for AccDocumentItemListView.xaml
    /// </summary>
    public partial class AccDocumentItemListView : UserControl
    {
        private AccDocumentItemListViewModel _viewModel;

        public AccDocumentItemListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as ItemDocument.AccDocumentItemListViewModel;
                //((GridViewComboBoxColumn)this.RadGridView1.Columns["Description1"]).ItemsSource = _viewModel;
            };
            //var culture = new CultureInfo("en-US");
            //var dtfInfo = new DateTimeFormatInfo
            //{
            //    ShortDatePattern = "MM-dd-yyyy",
            //    ShortTimePattern = "HH:mm",
            //    DateSeparator = "-"
            //};
            //culture.DateTimeFormat = dtfInfo;
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void RadGridView1_CurrentCellChanged(object sender, Telerik.Windows.Controls.GridViewCurrentCellChangedEventArgs e)
        {
         
        }

        private void RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            //if (_viewModel.SLsDropDownOpenedCommand.CanExecute(null))
            //    _viewModel.SLsDropDownOpenedCommand.Execute(null);
        }

        private void RadGridView1_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            ////here you may need to perform a check which column is being edited to be sure it is the one with the combobox
            //// this may be done by checking the e.Cell.Column  
            //var x = e.EditingElement as RadComboBox;
            //string newlyTypedText = "";
            //if (x != null)
            //    newlyTypedText = x.Text;
            ////here add the newlyTypedText item to the ComboBoxColumn.ItemsSource collection
            //var qq = ((GridViewComboBoxColumn)this.RadGridView1.Columns["SLStandardDescriptionTitle"]).ItemsSource as HashSet<SLStandardDescription>;
            //SLStandardDescription vv = new SLStandardDescription() ;
            //if (!string.IsNullOrWhiteSpace(newlyTypedText) && !qq.Any(s => s.SLStandardDescriptionTitle == newlyTypedText))
            //{
            //     vv =new SLStandardDescription { SLStandardDescriptionTitle = newlyTypedText };
            //    qq.Add(vv);
            //}
            //if (x != null)
            //    x.SelectedItem = vv;
        }

        private void RadGridView1_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            var employee = new EditableAccDocumentItem();
            employee.AccDocumentHeaderId = _viewModel.AccDocumentHeader.AccDocumentHeaderId;
            employee.Description1 = "dddddddddddddd";
            e.NewObject = employee;
        }

        private void RadGridView1_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            //store the items to be deleted
            //_viewModel.ItemsToBeDeleted.AddRange(e.Items);

            ////cancel the event so the item is not deleted
            ////and wait for the user confirmation
            //e.Cancel = true;
            ////open the Confirm dialog
            //RadWindow.Confirm("Are you sure?", this.OnRadWindowClosed);

        }
        //public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        //{
        //    List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();

        //    if (key == Key.Down)
        //    {

        //        //commandsToExecute.Clear();
        //        //commandsToExecute.Add(RadGridViewCommands.CommitEdit);
        //        //commandsToExecute.Add(RadGridViewCommands.MoveNext);
        //        //commandsToExecute.Add(RadGridViewCommands.BeginEdit);
        //        commandsToExecute.Add(RadGridViewCommands.BeginInsert);
        //    }

        //    return commandsToExecute;
        //}
        //BeginInsert - corresponds to Insert key.

        //CancelCellEdit - Esc key

        //CancelRowEdit - Esc key after the cell edit is already committed

        //CollapseHierarchyItem, ExpandHierarchyItem - Space key - expands/ collapses a particular item in the hierarchy if the focus is not the hierarchy button

        //CommitCellEdit - Tab or Enter keys

        //CommitEdit -
        //Tab or Enter keys

        //Copy, Paste - Ctrl + C, Ctrl + V

        //Delete - Del key

        //MoveBottom, MoveTop - Bottom, Top keys

        //MoveUp, MoveDown - Up, Down keys

        //MoveFirst, MoveLast - First, Last keys

        //MoveLeft, MoveRight - Left, Right keys

        //MoveNext, MovePrevious - Next, Previous keys

        //MoveHome - Home key

        //Select All - Ctrl + A
    }
}
