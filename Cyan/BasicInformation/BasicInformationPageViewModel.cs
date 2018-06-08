using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Views.BasicInformation
{
    public class BasicInformationPageViewModel 
    {
        public BasicInformationPageViewModel()
        {
            DoNavigate = new RelayCommand<string>(Navigate);
        }

        public RelayCommand<string> DoNavigate { set; get; }
        private void Navigate(string url)
        {
           // Redirect.To(url);
        }
      //  public override bool ViewModelContextHasChanges => false;
    }
}
