using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
   public class EditGL : DataItem
    {
        public EditGL()
        {
            _tLItems = new ObservableCollection<EditTL>();

        }
        private string _gLCode;
        public string GLCode
        {
            get { return _gLCode; }
            set { SetProperty(ref _gLCode, value); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<EditTL> _tLItems;
        public ObservableCollection<EditTL> TLItems
        {
            get { return _tLItems; }
           
        }

    }
}
