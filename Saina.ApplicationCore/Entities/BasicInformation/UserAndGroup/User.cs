using MoreLinq;
using Saina.ApplicationCore.Entities.Commerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    [Table("Users", Schema = "Info")]

    public class User : BaseEntity
    {
        public User()
        {
            DynamicPages = new ObservableCollection<UGDP>();
            Stocks = new ObservableCollection<Stock>();
            Accesses = new ObservableCollection<Access>();
            IsActive = true;
        }
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _friendlyName;
        [Required(ErrorMessage = "نام مستعار خالی است.")]
        [StringLength(maximumLength: 450, MinimumLength = 3, ErrorMessage = "نام مستعار باید بین سه تا 450 کاراکتر باشد.")]
      

        public string FriendlyName
        {
            get { return _friendlyName; }
            set { _friendlyName = value; }
        }

       
 private string _userName;
        [Required(ErrorMessage = "نام کاربری خالی است.")]
        [StringLength(maximumLength: 450, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین سه تا 450 کاراکتر باشد.")]
       

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

      
  private string _password;
        [Required(ErrorMessage = "کلمه عبور خالی است")]
        [StringLength(maximumLength: 50, ErrorMessage = "حداکثر طول کلمه عبور 50 کاراکتر است")]
      

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private bool _hasFullAccess;
        public bool HasFullAccess
        {
            get { return _hasFullAccess; }
            set { SetProperty(ref _hasFullAccess, value); }
        }



        private ICollection<UGDP> _dynamicPages;
        public virtual ICollection<UGDP> DynamicPages
        {
            get
            {
                if (Group?.DynamicPages != null) return Group.DynamicPages;
                return _dynamicPages;
            }
             set { _dynamicPages = value; }
        }
        ////public virtual ICollection<UGDP> DynamicPages { get; set; }
        public virtual Group Group { get; set; }

        private int? _groupId;

        public int? GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        public bool HasAccess(string typeName)
        {
            if (Group != null)
                return Group.DynamicPages.Select(x => x.DynamicPage.Name).Contains(typeName);
            return DynamicPages.Select(x => x.DynamicPage.Name).Contains(typeName);
        }

        public IDictionary<string, Permission> GetPagePermissionDictionary()
        {
            return DynamicPages.DistinctBy(x=>x.DynamicPage.Name).ToDictionary(x => x.DynamicPage.Name, y => y.UserRights);
        }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }

    }

}
