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
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Admin.GroupAccess
{
    /// <summary>
    /// لیست دسترسی گروه
    /// </summary>
    class GroupListViewModel : BindableBase
    {
        #region Constructors
        public GroupListViewModel(IGroupsService groupsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _groupsService = groupsService;
            AddGroupCommand = new RelayCommand(OnAddGroup);
            EditGroupCommand = new RelayCommand<Group>(OnEditGroup);
            DeleteCommand = new RelayCommand<Group>(OnDeleteGroup);
            SaveCommand = new RelayCommand(OnSave);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }

        #endregion
        #region Fields
        private IGroupsService _groupsService;
        private List<Group> _allGroups;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddGroupCommand { get; private set; }
        public ICommand EditGroupCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private AccessUtility _accessUtility;

        public event Action Saved;

        #endregion
        #region Properties
        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set { SetProperty(ref _groups, value); }
        }
        public event Action<Group> AddGroupRequested;
        public event Action<Group> EditGroupRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private void OnSave()
        {
            try
            {
                var c = _groupsService.SaveChanges();
                Saved?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }

        }
        public async void LoadGroups()
        {
            _allGroups = await _groupsService.GetGroupsAsync();
            Groups = new ObservableCollection<Group>(_allGroups);
        }
        private void OnAddGroup()
        {
            if (_accessUtility.HasAccess(21))
            {
                AddGroupRequested(new Group());
            }

        }
        private void OnEditGroup(Group group)
        {
            if (_accessUtility.HasAccess(22))
            {
                EditGroupRequested(group);
            }
        }
        private async void OnDeleteGroup(Group group)
        {
            if (_accessUtility.HasAccess(23))
            {
                if (Deleting())
                {
                    try
                    {
                        await _groupsService.DeleteGroupAsync(group.GroupId);
                        Groups.Remove(group);
                        Deleted();
                    }
                    catch (Exception ex)
                    {
                        Failed(ex);
                    }

                }
            }

        }
        #endregion

      


     

      

    }
}
