using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
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

namespace Saina.WPF.BasicInformation.Accounts.SLStandard
{
    /// <summary>
    /// Interaction logic for AddEditStandardDescriptionWindow.xaml
    /// </summary>
    public partial class AddEditStandardDescriptionWindow : RadWindow, INotifyPropertyChanged
    {
        public AddEditStandardDescriptionWindow()
        {
            InitializeComponent();
            SLStandardDescription = new SLStandardDescription();
            DataContext = this;
        }
        private SLStandardDescription _sLStandardDescription;

        public SLStandardDescription SLStandardDescription
        {
            get { return _sLStandardDescription; }
            set { if (_sLStandardDescription == value) return;
                _sLStandardDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SLStandardDescription)));
            }
        }


        public event Action OkClicked;
        public event PropertyChangedEventHandler PropertyChanged;

        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {
            OkClicked?.Invoke();
            Close();
        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
            SLStandardDescription = null;
            Close();
        }
    }
}
