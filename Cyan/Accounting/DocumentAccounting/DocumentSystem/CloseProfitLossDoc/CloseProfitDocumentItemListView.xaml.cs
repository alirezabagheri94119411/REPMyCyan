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

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CloseProfitLossDoc
{
    /// <summary>
    /// Interaction logic for CloseProfitDocumentItemListView.xaml
    /// </summary>
    public partial class CloseProfitDocumentItemListView : UserControl
    {
        public CloseProfitDocumentItemListView()
        {
            InitializeComponent();
        }

        private void RadDatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var t = (Microsoft.Windows.Controls.DatePicker)e.Source;
            if (t.Text.Length == 10 && e.Text.Length > 0)
            {
                e.Handled = true;
            }

        }
    }
}
