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
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Telerik.Windows.Controls;
using Saina.WPF.BasicInformation.Information.Related;

namespace Saina.WPF.BasicInformation.Information.PersonInfo
{
    /// <summary>
    /// Interaction logic for PersonListView.xaml
    /// </summary>
    public partial class PersonListView : UserControl
    {
        private PersonListViewModel _viewModel;
        private bool? _dialogResult;

        public PersonListView()
        {
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as PersonListViewModel;
                _viewModel.Deleting += OnDeleting;
                _viewModel.Deleted += OnDeleted;
                _viewModel.Failed += OnFailed;
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Deleting -= OnDeleting;
                _viewModel.Deleted -= OnDeleted;
                _viewModel.Failed -= OnFailed;
            };

        }
        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
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
        private void PersonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPersonWindow addEditPersonindow = new AddEditPersonWindow() { DataContext = DataContext };
            _viewModel.Person = new Person();
          //  var Person = ((Control)sender).DataContext as  Person;
            //if (Person == null)
            //    addEditPersonindow = new AddEditPersonWindow() { };
            //else
            //    addEditPersonindow = new AddEditPersonWindow() { Person = ((Control)sender).DataContext as Person };

            addEditPersonindow.OkClicked += () =>
            {
                ((PersonListViewModel)DataContext).People.Add(addEditPersonindow.Person);
            };
            addEditPersonindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addEditPersonindow.Width = 768;
            addEditPersonindow.Height = 500;
            addEditPersonindow.CanClose = true;
            addEditPersonindow.Owner = Window.GetWindow(this);
            addEditPersonindow.Show();
            // addEditPersonindow.ShowDialog();
        }

        private void AddRelated_Click(object sender, RoutedEventArgs e)
        {
            RelatedPersonListWindow relatedPersonListWindow = new RelatedPersonListWindow();
            relatedPersonListWindow.Width = 1024;
            relatedPersonListWindow.Height = 768;
            relatedPersonListWindow.CanClose = true;
            relatedPersonListWindow.Owner = Window.GetWindow(this);
            relatedPersonListWindow.Owner = Window.GetWindow(this);
            relatedPersonListWindow.Show();
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowReport()
        {

        }

        private void designButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DesignReport()
        {

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintReport()
        {

        }

        private void PersonEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPersonWindow addEditPersonindow = new AddEditPersonWindow() { DataContext = DataContext };
            _viewModel.Person = ((Control)sender).DataContext as Person;// _viewModel.Person = new Person();
            addEditPersonindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addEditPersonindow.Width = 768;
            addEditPersonindow.Height = 500;
            addEditPersonindow.CanClose = true;
            addEditPersonindow.Owner = Window.GetWindow(this);
            addEditPersonindow.Show();
        }
    }
}
