using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Views.Login
{
    public class WelcomePageViewModel
    {
        public string Message { get; set; }//=> QueryData?.ToString(); // روش دريافت كوئري استرينگ ارسالي از صفحه‌ي قبل
    //    public override bool ViewModelContextHasChanges => false;
        public void Loaded()
        {

        }
    }
}
