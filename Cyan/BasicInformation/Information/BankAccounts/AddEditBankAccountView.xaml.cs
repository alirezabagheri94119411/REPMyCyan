using Saina.WPF.BasicInformation.Information.Related;
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

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
    /// <summary>
    /// Interaction logic for AddEditBankAccountView.xaml
    /// </summary>
    public partial class AddEditBankAccountView : UserControl
    {
        private AddEditBankAccountViewModel _viewModel;

        public AddEditBankAccountView()
        {
            InitializeComponent();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as AddEditBankAccountViewModel;
                _viewModel.Failed += OnFailed;
               // _viewModel.RelatedPersonListViewModel.AddRelatedPersonRequested += RelatedPersonListViewModel_AddRelatedPersonRequested;

            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
            };

        }
        //private void RelatedPersonListViewModel_AddRelatedPersonRequested(int relatedPersonId)
        //{
        //    var addEditRelatedPersonWindow = new AddEditRelatedPersonWindow() { };

        //    addEditRelatedPersonWindow.OkClicked += () =>
        //    {
        //        _viewModel.RelatedPersonListViewModel.RelatedPeople.Add(addEditRelatedPersonWindow.RelatedPerson);
        //    };
        //    addEditRelatedPersonWindow.ShowDialog();
        //    //_viewModel.RelatedPersonListViewModel.RelatedPeople.Add(new ApplicationCore.Entities.BasicInformation.Accounts.RelatedPerson { RelatedPersonTitle = "Test" });
        //    //MessageBox.Show(relatedPersonId.ToString());
        //}

        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
        }
    }
}
