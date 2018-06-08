using Saina.ApplicationCore.Entities.Commerce;
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
using System.Windows.Shapes;

namespace Saina.WPF.Commerce.ProductRacks
{
    /// <summary>
    /// Interaction logic for ChildListWindow.xaml
    /// </summary>
    public partial class ChildListWindow : Window
    {
        public ChildListWindow()
        {
            InitializeComponent();
            DataItem = new ProductRack();
            DataContext = this;
        }
        public ProductRack DataItem { get; set; }
    }
}
