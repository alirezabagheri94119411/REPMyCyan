using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public class StcTypeDocument:BaseEntity
    {
        public StcTypeDocument()
        {
            StcDocumentHeaders=new ObservableCollection<StcDocumentHeader>();
        }
        private int _StcTypeDocumentId;
        public int StcTypeDocumentId
        {
            get { return _StcTypeDocumentId; }
            set { SetProperty(ref _StcTypeDocumentId, value); }
        }
        private string _StcTypeDocumentTitle;
        public string StcTypeDocumentTitle
        {
            get { return _StcTypeDocumentTitle; }
            set { SetProperty(ref _StcTypeDocumentTitle, value); }
        }
        public virtual ICollection<StcDocumentHeader> StcDocumentHeaders { get; set; }

    }
}
