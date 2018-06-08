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

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// Interaction logic for RelatedPersonListWindow.xaml
    /// </summary>
    public partial class RelatedPersonListWindow : RadWindow
    {
        public RelatedPersonListWindow()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                relatedPersonDataForm.AddNewItem();
            };
        }
    }
}
