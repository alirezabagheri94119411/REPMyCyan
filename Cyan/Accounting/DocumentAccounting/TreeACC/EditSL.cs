using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
   public class EditSL : DataItem
    {
        private string _sLCode;
        public string SLCode
        {
            get { return _sLCode; }
            set { SetProperty(ref _sLCode, value); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private EditTL _TL;
        public EditTL TL
        {
            get { return _TL; }
            set { SetProperty(ref _TL, value); }
        }

    }
}
