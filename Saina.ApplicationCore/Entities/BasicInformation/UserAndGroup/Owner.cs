using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    [Table("Owners", Schema = "Info")]
    public class Owner:BaseEntity
    {
        public Owner()
        {
            Views = new ObservableCollection<View>();
        }
        private int _ownerId;
        public int OwnerId
        {
            get { return _ownerId; }
            set { SetProperty(ref _ownerId, value); }
        }
        private string _ownerName;
        public string OwnerName
        {
            get { return _ownerName; }
            set { SetProperty(ref _ownerName, value); }
        }
        private string _ownerTitle;
        public string OwnerTitle
        {
            get { return _ownerTitle; }
            set { SetProperty(ref _ownerTitle, value); }
        }
        private short _displayIndex;
        public short DisplayIndex
        {
            get { return _displayIndex; }
            set { SetProperty(ref _displayIndex, value); }
        }

        public virtual ICollection<View> Views { get; set; }


    }
}
