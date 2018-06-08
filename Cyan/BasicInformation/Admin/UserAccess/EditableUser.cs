using System.ComponentModel.DataAnnotations;

namespace Saina.WPF.BasicInformation.Admin.UserAccess
{
    internal class EditableUser:ValidatableBindableBase
    {
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
            set { SetProperty(ref _userName,value); }
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
        private int? _groupId;

        public int? GroupId
        {
            get { return _groupId; }
            set { SetProperty(ref _groupId, value); }
        }
    }
}