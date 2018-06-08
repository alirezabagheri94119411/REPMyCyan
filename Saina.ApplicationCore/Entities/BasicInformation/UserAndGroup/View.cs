using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    [Table("Views", Schema = "Info")]

    public class View:BaseEntity
    {
        public View()
        {
            Operations = new ObservableCollection<Operation>();
        }
        private int _viewId;
        public int ViewId
        {
            get { return _viewId; }
            set { SetProperty(ref _viewId, value); }
        }
        private string _viewName;
        public string ViewName
        {
            get { return _viewName; }
            set { SetProperty(ref _viewName, value); }
        }
        private string _viewPersianName;
        public string ViewPersianName
        {
            get { return _viewPersianName; }
            set { SetProperty(ref _viewPersianName, value); }
        }
        private short _displayIndex;
        public short DisplayIndex
        {
            get { return _displayIndex; }
            set { SetProperty(ref _displayIndex, value); }
        }

        private int _ownerId;
        public int OwnerId
        {
            get { return _ownerId; }
            set { SetProperty(ref _ownerId, value); }
        }
        private Owner _owner;
        public Owner Owner
        {
            get { return _owner; }
            set { SetProperty(ref _owner, value); }
        }
        public virtual ICollection<Operation> Operations { get; set; }

    }
}
