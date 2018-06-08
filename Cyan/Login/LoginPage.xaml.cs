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

namespace Saina.WPF.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl

    {

        public LoginPage()
        {
            InitializeComponent();
            //var vm= new LoginPageViewModel();
            //DataContext = vm;
            //vm.Logedin += Vm_Logedin;
        }

        //private void Vm_Logedin(string message)
        //{
        //    if (message=="OK")
        //    {
        //     var main= new MainWindow();
        //        main.Closed += Main_Closed;
        //        this.Hide();
        //        main.Show();

        //    }
        //}

        //private void Main_Closed(object sender, EventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}

    
    }
}
