using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    /// <summary>
    /// جدول گروه
    /// </summary>
    [Table("Groups", Schema = "Info")]
    public class Group : BaseEntity
    {
        public Group()
        {
            DynamicPages = new ObservableCollection<UGDP>();
            Accesses = new ObservableCollection<Access>();
            Users = new ObservableCollection<User>();
        }

       
        /// <summary>
        /// آی دی گروه
        /// </summary>
        private int _groupId;
        [Key]
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId= value; }
        }
        /// <summary>
        /// نام گروه
        /// </summary>
        private string _name;
        [Required]
        public string Name
        {
            get { return _name; }
            set { _name= value; }
        }
     
        public virtual ICollection<User> Users { get; protected set; }
        private ICollection<UGDP> _dynamicPages;
        public virtual ICollection<UGDP> DynamicPages
        {
            get
            {
                return _dynamicPages;
            }
            set { _dynamicPages = value; }
        }
        public virtual ICollection<Access> Accesses { get; set; }

    }
}
