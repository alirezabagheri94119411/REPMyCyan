using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Common.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.DTOs;
using Saina.Data.Context;

namespace Saina.Data.Services.BasicInformation
{
    /// <summary>
    /// اطلاعات سراسری برنامه در مورد کاربر جاری را فراهم می‌کند
    /// It will be a `Singleton` instance, because it's a single instance/user application.
    /// </summary>
    public class AppContextService : IAppContextService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="usersService">سرویس اطلاعات کاربران</param>
        public AppContextService(IUsersService usersService
)
        {
            _usersService = usersService;
        }
        #endregion
        #region Fields
        readonly IUsersService _usersService;
        private ICompanyInformationsService _companyInformationsService;
        public SainaDbContext _uow;

        public CompanyInformationModel CompanyInformationModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Properties
        public static IDictionary<string, Permission> PagePermission { get; set; }
        /// <summary>
        /// آیا کاربر جاری اعتبار سنجی شده است؟
        /// </summary>
        public bool IsCurrentUserAuthenticated
        {
            get { return CurrentUser != null; }
        }
        /// <summary>
        /// آیا کاربر جاری وارد شده به سیستم دسترسی مدیریتی دارد؟
        /// </summary>
        public bool IsCurrentUserAdmin
        {
            get
            {
                return CurrentUser != null;
            }
        }
        /// <summary>
        /// اطلاعات کاربر جاری را باز می‌گرداند
        /// </summary>
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            private set { if (_currentUser == value) return; _currentUser = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUser))); }
        }

        private int _currentFinancialYear;

        public int CurrentFinancialYear
        {
            get { return _currentFinancialYear; }
            set { if (_currentFinancialYear == value) return; _currentFinancialYear = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFinancialYear))); }
        }

        /// <summary>
        /// کلیه نقش‌های کاربر وارد شده به سیستم را بر می‌گرداند
        /// </summary>
        public string[] CurrentUserRoles { get; private set; }

        /// <summary>
        /// کلیه نقش‌های تعریف شده در سیستم را باز می‌گرداند
        /// </summary>
        public string[] AllValidSystemRoles { get; private set; }
        //public FinancialYear SelectedFinancialYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private FinancialYear _SelectedFinancialYear;
        public FinancialYear SelectedFinancialYear
        {
            get { return _SelectedFinancialYear; }
            set { if (_SelectedFinancialYear == value) return; _SelectedFinancialYear = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedFinancialYear))); }
        }

        #endregion
        #region Methode
        /// <summary>
        /// جهت بررسی اطلاعات لاگین کاربر استفاده خواهد شد
        /// </summary>
        /// <param name="userName">نام کاربری وارد شده</param>
        /// <param name="password">کلمه عبور وارد شده</param>
        /// <returns>آیا عملیات موفقیت آمیز بوده یا خیر</returns>
        public async Task<bool> LoginCurrentUserAsync(string userName, string password)
        {
            CurrentUser = await _usersService.FindUserAsync(userName, password).ConfigureAwait(false);
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentUser)));
            if (CurrentUser == null || !CurrentUser.IsActive)
            {
                CurrentUser = null;
                return false;
            }
            SainaEnvironment.CurrentUserName = CurrentUser.UserName;
            // getRoles();
            //PagePermission = CurrentUser.DynamicPages.ToDictionary(x => x.DynamicPage.Name, y => y.Permission);
            PagePermission = CurrentUser.GetPagePermissionDictionary();
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(PagePermission)));

            return true;
        }
        /// <summary>
        /// اطلاعات لاگین کاربر را تخریب می‌کند و سبب خروج او از سیستم خواهد شد
        /// </summary>
        public void LogoutCurrentUserAsync()
        {
            CurrentUser = null;
            // CurrentUserRoles = null;
        }
        public void UpdateCurrentUser(List<User> usersList)
        {
            if (CurrentUser == null)
                return;

            var newUserInfo = usersList.FirstOrDefault(x => x.UserId == CurrentUser.UserId);
            if (newUserInfo == null)
                return;

            CurrentUser = newUserInfo;
            //  getRoles();
        }
        /// <summary>
        /// پس از به روز رسانی لیست کاربران شاید نیاز به به روز رسانی وضعیت کاربر جاری باشد
        /// </summary>
        /// <param name="usersList">لیست کاربران</param>
        public bool HasAccess(string typeName, Permission permission)
        {
            Permission p = 0;
            if (typeName != null && PagePermission != null)
            {
                PagePermission.TryGetValue(typeName, out p);
            }

            return p.HasFlag(permission);
        }

        #endregion
        public bool CanAccess(int UserRef, int ActionRef)
        {
            _uow = new SainaDbContext();
            return _uow.Accesses.First(x => x.UserId == UserRef && x.OperationId == ActionRef).HasAccess;
        }
    }
}
