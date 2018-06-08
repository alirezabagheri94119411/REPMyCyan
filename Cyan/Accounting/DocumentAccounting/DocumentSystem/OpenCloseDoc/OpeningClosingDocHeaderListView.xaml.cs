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

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.OpenCloseDoc
{
    /// <summary>
    /// Interaction logic for OpeningClosingDocHeaderListView.xaml
    /// </summary>
    public partial class OpeningClosingDocHeaderListView : UserControl
    {
        private OpeningClosingDocHeaderListViewModel _viewModel;
        private bool? _dialogResult;
        public OpeningClosingDocHeaderListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as OpeningClosingDocHeaderListViewModel;
                _viewModel.Deleting += OnDeleting;
                _viewModel.Deleted += OnDeleted;


            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Deleting -= OnDeleting;
                _viewModel.Deleted -= OnDeleted;


            };

        }
        private void OnDeleted()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اطلاعات";
            parameters.Content = ".حذف با موفقیت انجام شد";
            RadWindow.Alert(parameters);
        }

        private bool OnDeleting()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا برای حذف  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            return _dialogResult == true;
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
    }
}
