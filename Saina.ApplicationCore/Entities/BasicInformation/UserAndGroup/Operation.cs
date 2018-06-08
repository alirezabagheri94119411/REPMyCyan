using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    [Table("Operations", Schema = "Info")]

    public class Operation:BaseEntity
    {
        public Operation()
        {
            Accesses = new ObservableCollection<Access>();
        }
        private int _operationId;
        public int OperationId
        {
            get { return _operationId; }
            set { SetProperty(ref _operationId, value); }
        }
        private string _operationName;
        public string OperationName
        {
            get { return _operationName; }
            set { SetProperty(ref _operationName, value); }
        }
        private string _operationPersianName;
        public string OperationPersianName 
        {
            get { return _operationPersianName; }
            set { SetProperty(ref _operationPersianName, value); }
        }
        private short _displayIndex;
        public short DisplayIndex
        {
            get { return _displayIndex; }
            set { SetProperty(ref _displayIndex, value); }
        }

        private int _viewId;
        public int ViewId
        {
            get { return _viewId; }
            set { SetProperty(ref _viewId, value); }
        }
        private View _view;
        public View View
        {
            get { return _view; }
            set { SetProperty(ref _view, value); }
        }

        public virtual ICollection<Access> Accesses { get; set; }
        private bool _HasAccess;
        [NotMapped]
        public bool HasAccess
        {
            get { return _HasAccess; }
            set { SetProperty(ref _HasAccess, value); }
        }

    }
}
