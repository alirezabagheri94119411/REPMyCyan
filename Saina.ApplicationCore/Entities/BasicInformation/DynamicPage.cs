using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation
{
    //[Flags]
    public enum Permission
    {
        Select = 1,
        Update = 2,
        Insert = 4,
        Delete = 8
    }

    public class DynamicPage : BaseEntity
    {
        public DynamicPage()
        {
            Children = new ObservableCollection<DynamicPage>();
            UsersAndGroups = new ObservableCollection<UGDP>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DynamicPageId { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }

        //public int? GroupId { get; set; }

        //public int? UserId { get; set; }

        //public virtual Group Group { get; set; }

        //public virtual User User { get; set; }
        public virtual ICollection<UGDP> UsersAndGroups { get; set; }
        public int? ParentId { get; set; }
        private DynamicPage _parent;

        [ForeignKey("ParentId")]
        public virtual DynamicPage Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
            }
        }

        [ForeignKey("ParentId")]
        public virtual ICollection<DynamicPage> Children { get; set; }
        public int Order { get; set; }
        public string IconPath { get; set; }

    }
}
