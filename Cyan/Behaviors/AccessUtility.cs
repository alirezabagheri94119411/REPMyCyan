using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Saina.WPF.Behaviors
{
    class AccessUtility
    {
        private IAppContextService _appContextService;
        private SainaDbContext _uow;

        public AccessUtility(IAppContextService appContextService)
        {
            _appContextService = appContextService;

        }
        public bool HasAccess(int ActionRef,string msg="شما به این عملیات دسترسی ندارید")
        {
            var userId = _appContextService.CurrentUser.UserId;

            var access = this.HasAccess(userId, ActionRef);
            if (!access)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "اخطار";
                parameters.Content = msg;
                RadWindow.Alert(parameters); ;
            }
            return access;
        }
        private bool HasAccess(int UserRef, int ActionRef)
        {
            _uow = new SainaDbContext();
            return _uow.Accesses.First(x => x.UserId == UserRef && x.OperationId == ActionRef).HasAccess;
        }

    }
}
