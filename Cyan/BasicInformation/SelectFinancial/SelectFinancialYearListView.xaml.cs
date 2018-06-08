using Saina.ApplicationCore.Entities.BasicInformation;
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

namespace Saina.WPF.BasicInformation.SelectFinancial
{
    /// <summary>
    /// Interaction logic for SelectFinancialYearListView.xaml
    /// </summary>
    public partial class SelectFinancialYearListView : UserControl
    {
        public SelectFinancialYearListView()
        {
            InitializeComponent();
            this.DataContext = SmObjectFactory.Container.GetInstance<SelectFinancialYearListViewModel>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.DataContext = SmObjectFactory.Container.GetInstance<SelectFinancialYearListViewModel>();
            ((SelectFinancialYearListViewModel)DataContext).LoadSelectFinancialYears();
        }


        public FinancialYear SelectedFinancialYear
        {
            get { return (FinancialYear)GetValue(SelectedFinancialYearProperty); }
            set { SetValue(SelectedFinancialYearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFinancialYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFinancialYearProperty =
            DependencyProperty.Register("SelectedFinancialYear", typeof(FinancialYear), typeof(SelectFinancialYearListView), new PropertyMetadata(null));


    }
}
