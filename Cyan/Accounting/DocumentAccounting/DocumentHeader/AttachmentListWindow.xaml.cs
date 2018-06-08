using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Telerik.Windows.Controls.GridView;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    /// <summary>
    /// Interaction logic for AttachmentListWindow.xaml
    /// </summary>
    public partial class AttachmentListWindow : RadWindow
    {

        private bool? _dialogResult;
        private AccDocumentHeaderListViewModel _viewModel;
        private IAppContextService _appContextService;

        public AttachmentListWindow()
        {
            _viewModel = DataContext as AccDocumentHeaderListViewModel;
            _appContextService = SmObjectFactory.Container.GetInstance<IAppContextService>();

            InitializeComponent();
            Loaded += (s, e) =>
            {
                var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                addNewCommand.Execute(null, attachmentDataForm);
            };
        }
        private void BeginEdit()
        {
            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, this.attachmentDataForm);
        }

        private void attachmentDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            ((Attachment)e.NewItem).SystemDate = DateTime.Now.Date;
            ((Attachment)e.NewItem).UserName = _appContextService.CurrentUser.UserName;

        }

        private void attachmentDataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            //attachmentDataForm.AddNewItem();

        }
        private void attachmentDataForm_DeletingItem(object sender, CancelEventArgs e)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا برای حذف  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            var r = _dialogResult == true;
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
       
        Attachment Attachment => attachmentDataForm.CurrentItem as Attachment;
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, this.attachmentDataForm);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var cancelCommand = RadDataFormCommands.CancelEdit as RoutedUICommand;
            cancelCommand.Execute(null, this.attachmentDataForm);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, this.attachmentDataForm);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            CommitAndBeginEdit();
        }
        private void CommitAndBeginEdit()
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            commitEditCommand.Execute(null, this.attachmentDataForm);
            BeginEdit();
        }
    }
}
