using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Common.Crypto;
using Saina.Data;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Admin.UserAccess
{
    /// <summary>
    /// افزودن و ویرایش کاربران
    /// </summary>
    class AddEditUserViewModel : BindableBase
    {
        private IUsersService _usersService;
        public AddEditUserViewModel(IUsersService usersService, IGroupsService groupsService)
        {
            _usersService = usersService;
            _groupsService = groupsService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            GroupsDropDownOpenedCommand = new RelayCommand(OnGroupsDropDownOpened, () => Groups != null && Groups.Any());
        }

        private async void OnGroupsDropDownOpened()
        {
            _allGroups = await _groupsService.GetGroupsAsync();
            Groups = new ObservableCollection<Group>(_allGroups);
        }

        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private EditableUser _User;
        public EditableUser User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }
        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set { SetProperty(ref _groups, value); }
        }
        private List<Group> _allGroups;
        private IGroupsService _groupsService;
        public async void LoadGroups()
        {
            if (Groups == null)
            {
                _allGroups = await _groupsService.GetGroupsAsync();
                Groups = new ObservableCollection<Group>(_allGroups);
            }
        }
        public override void UnLoaded()
        {
            Groups = null;
        }

        public void SetUser(User user)
        {
            //User = user;
            User = Mapper.Map<User, EditableUser>(user);
            User.ErrorsChanged += RaiseCanExecuteChanged;

        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done;
        public RelayCommand GroupsDropDownOpenedCommand { get; private set; }

        private void OnCancel()
        {
            User = null;
            Done?.Invoke();
        }
        public event Action<Exception> Failed;
        private async void OnSave()
        {
            var editingCustomer = Mapper.Map<EditableUser, User>(User);
            try
            {
                if (EditMode)
                    await _usersService.UpdateUserAsync(editingCustomer);
                else
                    await _usersService.AddUserAsync(editingCustomer);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                User = null;
            }
        }


        private bool CanSave()
        {
            return !User.HasErrors;
        }




    }

}
