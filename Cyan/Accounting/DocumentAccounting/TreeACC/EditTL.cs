using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
   public class EditTL:DataItem
    {
        public EditTL()
        {
            _sLItems = new ObservableCollection<EditSL>();
        }
        private string _tLCode;
        public string TLCode
        {
            get { return _tLCode; }
            set { SetProperty(ref _tLCode, value); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private ObservableCollection<EditSL> _sLItems;
        public ObservableCollection<EditSL> SLItems
        {
            get { return _sLItems; }
           
        }

        private EditGL _GL;
        public EditGL GL
        {
            get { return _GL; }
            set { SetProperty(ref _GL, value); }
        }

       
    }
}
