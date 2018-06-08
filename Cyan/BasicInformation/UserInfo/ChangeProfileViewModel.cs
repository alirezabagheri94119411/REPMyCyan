using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Common.Crypto;
using Saina.Data;
using Saina.Data.Context;
using Saina.WPF.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.UserInfo
{
    /// <summary>
    /// ویو تغییر اطلاعات کاربر
    /// </summary>
    public class ChangeProfileViewModel 
    {
        #region Constructors


        /// <summary>
        /// ویوو مدل تغییر اطلاعات کاربر جاری وارد شده به سیستم
        /// </summary>
        /// <param name="uow">وهله‌ای از زمینه و واحد کاری ایی اف</param>
        /// <param name="appContextService">اطلاعات سراسری برنامه در مورد کاربر جاری را فراهم می‌کند</param>
        /// <param name="usersService">سرویس اطلاعات کاربران</param>
        public ChangeProfileViewModel(SainaDbContext uow, IAppContextService appContextService, IUsersService usersService)
        {
            _uow = uow;
            _usersService = usersService;
            _appContextService = appContextService;

            ChangeProfileData = new LoginPageModel();
            DoSave = new RelayCommand(doSave, canDoSave);

           // ChangeProfileData.UserName = ;
           // ChangeProfileData.Password = _appContextService.CurrentUser.Password;
        }

        #endregion
        #region Fields
        readonly IAppContextService _appContextService;
        readonly SainaDbContext _uow;
        readonly IUsersService _usersService;
        #endregion
        #region Commands
        /// <summary>
        /// رخداد ذخیره سازی اطلاعات را دریافت می‌کند
        /// </summary>
        public RelayCommand DoSave { set; get; }

        /// <summary>
        /// اطلاعات کاربر جاری جهت نمایش در رابط کاربری
        /// </summary>
        public LoginPageModel ChangeProfileData { set; get; }

        /// <summary>
        /// فعال و غیرفعال کردن دکمه ذخیره سازی
        /// </summary>
        #endregion

        #region Methods

        private bool canDoSave()
        {
            return true;
          //  return ViewModelContextHasChanges;
        }

        /// <summary>
        /// ذخیره سازی اطلاعات تغییر کرده
        /// </summary>
        private  void doSave()
        {
            // نکته: از سرویس زمینه برنامه نمی‌توان برای به روز رسانی اطلاعات کاربر استفاده کرد
            // چون پس از لاگین، از کانتکست ایی اف جدا می‌شود

            //var user =await _usersService.FindUserAsync(_appContextService.CurrentUser.Id);
            //user.UserName = ChangeProfileData.UserName;
            //if (user.Password != ChangeProfileData.Password)
            //{
            //    user.Password = ChangeProfileData.Password.SHA1Hash();
            //}
            //_uow.SaveChanges(user.UserName);

            //updateAppContext(user);
        }

        /// <summary>
        /// به روز رسانی اطلاعات سراسری برنامه
        /// </summary>
        private void updateAppContext(User user)
        {
           //  = user.UserName;
          //  _appContextService.CurrentUser.Password = user.Password;
        }

        /// <summary>
        /// آیا در حین نمایش صفحه‌ای دیگر باید به کاربر پیغام داد که اطلاعات ذخیره نشده‌ای وجود دارد؟
        /// </summary>
        //public override bool ViewModelContextHasChanges
        //{
        //    get
        //    {
        //        var user = _appContextService.CurrentUser;
        //        if (user == null)
        //            return false;

        //        return (user.Password != ChangeProfileData.Password) ||
        //               (user.UserName != ChangeProfileData.UserName) ||
        //               _uow.ContextHasChanges;
        //    }
        //}
        #endregion


    }
}
