using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Accounts.DLAccount;
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

namespace Saina.WPF.BasicInformation.Information.Related
{
    /// <summary>
    /// Interaction logic for RelatedPersonListWindow.xaml
    /// </summary>
    ///   public partial class RelatedPersonListWindow : RadWindow
    public partial class RelatedPersonListWindow : RadWindow
    {

        private bool? _dialogResult;
    private DLListViewModel _viewModel;
   

    public RelatedPersonListWindow()
    {
        _viewModel = DataContext as DLListViewModel;

        InitializeComponent();
        Loaded += (s, e) =>
        {
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, relatedPersonDataForm);
        };
    }
    private void BeginEdit()
    {
        var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
        beginEditCommand.Execute(null, this.relatedPersonDataForm);
    }

    private void relatedPersonDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
    {
        //((RelatedPerson)e.NewItem).SystemDate = DateTime.Now.Date;
        //((RelatedPerson)e.NewItem).UserName = _appContextService.CurrentUser.UserName;

    }

    private void relatedPersonDataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
    {
        //relatedPersonDataForm.AddNewItem();

    }
    private void relatedPersonDataForm_DeletingItem(object sender, CancelEventArgs e)
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

    RelatedPerson RelatedPerson => relatedPersonDataForm.CurrentItem as RelatedPerson;
    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
        addNewCommand.Execute(null, this.relatedPersonDataForm);
    }
    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
        var cancelCommand = RadDataFormCommands.CancelEdit as RoutedUICommand;
        cancelCommand.Execute(null, this.relatedPersonDataForm);
    }
    private void deleteButton_Click(object sender, RoutedEventArgs e)
    {
        var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
        deleteCommand.Execute(null, this.relatedPersonDataForm);
    }

    private void saveButton_Click(object sender, RoutedEventArgs e)
    {
        CommitAndBeginEdit();
    }
    private void CommitAndBeginEdit()
    {
        var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
        commitEditCommand.Execute(null, this.relatedPersonDataForm);
        BeginEdit();
    }

        private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {

        }

        private void relatedPersonDataForm_DeletingItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

        }
    }

    }

