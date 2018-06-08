using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    /// <summary>
    /// اطلاعات سراسری برنامه در مورد کاربر جاری را فراهم می‌کند
    /// </summary>
    public interface IAppContextService:INotifyPropertyChanged
    {
        bool CanAccess(int UserRef, int ActionRef);
        
        /// <summary>
        /// جهت بررسی اطلاعات لاگین کاربر استفاده خواهد شد
        /// </summary>
        /// <param name="userName">نام کاربری وارد شده</param>
        /// <param name="password">کلمه عبور وارد شده</param>
        /// <returns>آیا عملیات موفقیت آمیز بوده یا خیر</returns>
        /// 
        Task<bool> LoginCurrentUserAsync(string userName, string password);

        /// <summary>
        /// اطلاعات لاگین کاربر را تخریب می‌کند و سبب خروج او از سیستم خواهد شد
        /// </summary>
        void LogoutCurrentUserAsync();

        /// <summary>
        /// آیا کاربر جاری اعتبار سنجی شده است؟
        /// </summary>
        bool IsCurrentUserAuthenticated { get; }

        /// <summary>
        /// اطلاعات کاربر جاری را باز می‌گرداند
        /// </summary>
            User CurrentUser { get; }
        //اطلاعات شرکت فعلی
        //   CompanyInformation CurrentCompany { get; }


            /// <summary>
            /// آیا کاربر جاری وارد شده به سیستم دسترسی مدیریتی دارد؟
            /// </summary>
        bool IsCurrentUserAdmin { get; }

        /// <summary>
        /// کلیه نقش‌های کاربر وارد شده به سیستم را بر می‌گرداند
        /// </summary>
        string[] CurrentUserRoles { get; }

        /// <summary>
        /// کلیه نقش‌های تعریف شده در سیستم را باز می‌گرداند
        /// </summary>
        string[] AllValidSystemRoles { get; }

        /// <summary>
        /// آیا کاربر جاری نقش‌های مشخص شده را دارا است؟
        /// </summary>
        /// <param name="requiredRoles">لیست یک سری نقش برای بررسی</param>
        /// <returns>آیا کاربر جاری نقش‌های مشخص شده را دارا است؟</returns>
//Task<bool> IsCurrentUserInRoles(string[] requiredRoles);

        /// <summary>
        /// آیا کاربر جاری می‌تواند بر اساس نقش‌هایی که دارد صفحه درخواستی را مشاهده کند؟
        /// </summary>
        /// <param name="attributeInstance">وهله‌ای از ویژگی مشخص کننده سطوح دسترسی مورد نیاز یک صفحه</param>
        /// <returns>آیا کاربر جاری نقش‌های مشخص شده را دارا است؟</returns>
        //bool CanCurrentUserNavigateTo(PageAuthorizationAttribute attributeInstance, string typeName = "");

        /// <summary>
        /// پس از به روز رسانی لیست کاربران شاید نیاز به به روز رسانی وضعیت کاربر جاری باشد
        /// </summary>
        /// <param name="usersList">لیست کاربران</param>
       // void UpdateCurrentUserList(List<FinancialYear> usersList);
        void UpdateCurrentUser(List<User> usersList);

        //IDictionary<string, Permission> PagePermission { get; set; }

        bool HasAccess(string typeName, Permission permission);
        //void UpdateCurrentUser(ObservableCollection<FinancialYear> users);
        int CurrentFinancialYear { get; set; }
        FinancialYear SelectedFinancialYear { get; set; }
    }
}
