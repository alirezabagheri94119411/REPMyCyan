using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Common.Toolkit;
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

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    /// <summary>
    /// Interaction logic for AddEditAccDocumentHeaderView.xaml
    /// </summary>
    public partial class AddEditAccDocumentHeaderView : UserControl
    {

        private AddEditAccDocumentHeaderViewModel _viewModel;

        public AddEditAccDocumentHeaderView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
                {
                    _viewModel = DataContext as AddEditAccDocumentHeaderViewModel;
                    _viewModel.Failed += OnFailed;
                    _viewModel.Error += OnError;
                    _viewModel.AccDocumentHeader.PropertyChanged += AccDocumentHeader_PropertyChanged;
                    _viewModel.AccDocumentHeader.ErrorsChanged += AccDocumentHeader_ErrorsChanged;
                };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
                _viewModel.Error -= OnError;

            };

        }

        private void AccDocumentHeader_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            if (_viewModel.AccDocumentHeader.HasErrors)
            {
               var errors = _viewModel.AccDocumentHeader.GetErrors().Where(x=>x.Key=="Status");
                foreach (var er in errors)
                {
                        MessageBox.Show(er.Value.ToString());
                }
            }
        }

        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
        }
        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void AccDocumentHeader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var editableAccDocumentHeader = sender as EditableAccDocumentHeader;
            if (e.PropertyName == "Status")
            {
                ////var flags = EnumHelper.GetFlags(editableAccDocumentHeader.Status);
                ////draftButton.IsEnabled = flags.Contains(StatusEnum.Draft);
                ////temporaryButton.IsEnabled = flags.Contains(StatusEnum.Temporary);
                ////endButton.IsEnabled = flags.Contains(StatusEnum.End);
                ////backFromEndButton.IsEnabled = flags.Contains(StatusEnum.BackFromEnd);
                ////permanentButton.IsEnabled = flags.Contains(StatusEnum.Permanent);
                //switch (editableAccDocumentHeader.Status)
                //{
                //    case StatusEnum.Draft:
                //        draftButton.IsEnabled = false;
                //        temporaryButton.IsEnabled = true;
                //        endButton.IsEnabled = false;
                //        backFromEndButton.IsEnabled = false;
                //        permanentButton.IsEnabled = false;
                //        break;
                //    case ApplicationCore.Entities.Accounting.DocumentAccounting.StatusEnum.Temporary:
                //        draftButton.IsEnabled = false;
                //        temporaryButton.IsEnabled = false;
                //        endButton.IsEnabled = true;
                //        backFromEndButton.IsEnabled = false;
                //        permanentButton.IsEnabled = false;
                //        break;
                //    case ApplicationCore.Entities.Accounting.DocumentAccounting.StatusEnum.End:
                //        draftButton.IsEnabled = false;
                //        temporaryButton.IsEnabled = false;
                //        endButton.IsEnabled = false;
                //        backFromEndButton.IsEnabled = true;
                //        permanentButton.IsEnabled = true;
                //        break;
                //    case ApplicationCore.Entities.Accounting.DocumentAccounting.StatusEnum.BackFromEnd:
                //        draftButton.IsEnabled = false;
                //        temporaryButton.IsEnabled = false;
                //        endButton.IsEnabled = true;
                //        backFromEndButton.IsEnabled = false;
                //        permanentButton.IsEnabled = true;
                //        break;
                //    case ApplicationCore.Entities.Accounting.DocumentAccounting.StatusEnum.Permanent:
                //        draftButton.IsEnabled = false;
                //        temporaryButton.IsEnabled = false;
                //        endButton.IsEnabled = false;
                //        backFromEndButton.IsEnabled = false;
                //        permanentButton.IsEnabled = false;
                //        break;
                //    default:
                //        break;
                //}
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // typeDocumentComboBox.SelectedIndex = 2;
        }
    }
}
