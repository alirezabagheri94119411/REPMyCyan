using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup
{
    [Table("Accesses", Schema = "Info")]
    public class Access:BaseEntity
    {
        public Access()
        {
            
        }
        private int _accessId;
        public int AccessId
        {
            get { return _accessId; }
            set { SetProperty(ref _accessId, value); }
        }
        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private int? _userId;
        public int? UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        private Group _group;
        public Group Group
        {
            get { return _group; }
            set { SetProperty(ref _group, value); }
        }

        private int? _groupId;
        public int? GroupId
        {
            get { return _groupId; }
            set { SetProperty(ref _groupId, value); }
        }
        private Operation _operation;
        public virtual Operation Operation
        {
            get { return _operation; }
            set { SetProperty(ref _operation, value); }
        }
        private int? _operationId;
        public int? OperationId
        {
            get { return _operationId; }
            set { SetProperty(ref _operationId, value); }
        }
        private bool _hasAccess;
        public bool HasAccess
        {
            get { return _hasAccess; }
            set { SetProperty(ref _hasAccess, value); }
        }
       



    }
}
