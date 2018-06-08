using Saina.ApplicationCore.Entities.BasicInformation.Information;
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
    ///
    /// <summary>
    ///افزودن افراد مرتبط
    /// </summary>
    public partial class AddEditRelatedPersonWindow : RadWindow, INotifyPropertyChanged
    {
        public AddEditRelatedPersonWindow()
        {
            InitializeComponent();
            RelatedPerson = new RelatedPerson();
            DataContext = this;
        }
        private RelatedPerson _relatedPerson;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelatedPerson RelatedPerson
        {
            get { return _relatedPerson; }
            set
            {
                if (_relatedPerson == value) return;
                _relatedPerson = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RelatedPerson)));
            }
        }
        public event Action OkClicked;
     

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            OkClicked?.Invoke();
            Close();
        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
            RelatedPerson = null;
            Close();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
            NameTextBox.CaretIndex = 100;

        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
