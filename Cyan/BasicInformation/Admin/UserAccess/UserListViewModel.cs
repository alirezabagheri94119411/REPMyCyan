using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Behaviors;
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
    /// لیست دسترسی کاربران
    /// </summary>
    class UserListViewModel : BindableBase
    {
        #region Constructors
        public UserListViewModel(IUsersService usersService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _usersService = usersService;
            AddUserCommand = new RelayCommand(OnAddUser);
            EditUserCommand = new RelayCommand<User>(OnEditUser);
            DeleteCommand = new RelayCommand<User>(OnDeleteUser);
            SaveCommand = new RelayCommand(OnSave);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IUsersService _usersService;
        private List<User> _allUsers;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private AccessUtility _accessUtility;

        public event Action Saved;
        #endregion
        #region Properties
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
       
        public event Action<User> AddUserRequested;
        public event Action<User> EditUserRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
      
        public async void LoadUsers()
        {
            //_allUsers = await _usersService.GetUsersAsync();
            //Users = new ObservableCollection<User>(_allUsers);

            _allUsers = await _usersService.GetUsersAsync();
            Users = new ObservableCollection<User>(_allUsers);

        }
        private void OnAddUser()
        {
            if (_accessUtility.HasAccess(17))
            {
                AddUserRequested(new User());
            }
        }
        private void OnEditUser(User user)
        {
            if (_accessUtility.HasAccess(18))
            {
                EditUserRequested(user);
            }
        }

        private async void OnDeleteUser(User user)
        {
            if (_accessUtility.HasAccess(19))
            {
                if (Deleting())
                {
                    try
                    {
                        await _usersService.DeleteUserAsync(user.UserId);
                        Users.Remove(user);
                        Deleted();
                    }
                    catch (Exception ex)
                    {
                        Failed(ex);
                    }

                }
            }

        }
        private  void OnSave()
        {
            try
            {
                var c= _usersService.SaveChanges();
                Saved?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }

        }

        #endregion

    }
}
  
