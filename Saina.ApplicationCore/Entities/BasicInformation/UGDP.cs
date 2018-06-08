using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saina.ApplicationCore.Entities.BasicInformation
{
    public class UGDP : BaseEntity
    {
        private int _uGDPId;

        public int UGDPId
        {
            get { return _uGDPId; }
            set { _uGDPId = value; }
        }

        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private int _groupId;

        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        private Group _group;

        public Group Group
        {
            get { return _group; }
            set { _group = value; }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }



        private int _dynamicPageId;

        public int DynamicPageId
        {
            get { return _dynamicPageId; }
            set { _dynamicPageId = value; }
        }



        public virtual DynamicPage DynamicPage { get; set; }
        public Permission UserRights { get; set; }


    }
}