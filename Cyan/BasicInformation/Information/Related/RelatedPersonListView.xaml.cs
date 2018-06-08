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

namespace Saina.WPF.BasicInformation.Information.Related
{
    /// <summary>
    /// Interaction logic for RelatedPersonListView.xaml
    /// </summary>
    public partial class RelatedPersonListView : UserControl
    {
        public RelatedPersonListView()
        {
            InitializeComponent();
        }
      
        private void RelatedPeopleAddEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditRelatedPersonWindow addEditRelatedPersonWindow = new AddEditRelatedPersonWindow() { };
            var RelatedPerson = ((Control)sender).DataContext as RelatedPerson;
            if (RelatedPerson == null)
                addEditRelatedPersonWindow = new AddEditRelatedPersonWindow() { };
            else
                addEditRelatedPersonWindow = new AddEditRelatedPersonWindow() { RelatedPerson = ((Control)sender).DataContext as RelatedPerson };

            addEditRelatedPersonWindow.OkClicked += () =>
            {
                ((RelatedPersonListViewModel)DataContext).RelatedPeople.Add(addEditRelatedPersonWindow.RelatedPerson);
            };
            addEditRelatedPersonWindow.Width = 1024;
            addEditRelatedPersonWindow.Height = 768;
            addEditRelatedPersonWindow.CanClose = true;
            addEditRelatedPersonWindow.Owner = Window.GetWindow(this);
            addEditRelatedPersonWindow.Owner = Window.GetWindow(this);
            addEditRelatedPersonWindow.Show();
        }
    }
}
