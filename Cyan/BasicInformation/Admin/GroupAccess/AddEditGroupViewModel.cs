using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Admin.GroupAccess
{
  /// <summary>
  /// افزودن و ویرایش گروه
  /// </summary>
    class AddEditGroupViewModel : BindableBase
    {
        private IGroupsService _groupsService;
        public AddEditGroupViewModel(IGroupsService groupsService)
        {
            _groupsService = groupsService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private EditableGroup _Group;
        public EditableGroup Group
        {
            get { return _Group; }
            set { SetProperty(ref _Group, value); }
        }

        private Group _editingGroup = null;

        public void SetGroup(Group group)
        {
            Group = Mapper.Map<Group, EditableGroup>(group);
            Group.ErrorsChanged += RaiseCanExecuteChanged;

        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public event Action<Exception> Failed;
        public event Action Done;


        private void OnCancel()
        {
            Done?.Invoke();
        }

        private async void OnSave()
        {
            var editingGroup = Mapper.Map<EditableGroup, Group>(Group);
            try
            {
                if (EditMode)
                await _groupsService.UpdateGroupAsync(editingGroup);
            else
                await _groupsService.AddGroupAsync(editingGroup);
            Done?.Invoke();
        }
         catch (Exception ex)
            {
                Failed(ex);
    }
            finally
            {
                Group = null;
            }
        }

       
        private bool CanSave()
        {
           return !Group.HasErrors;
        }

      
    }

}
