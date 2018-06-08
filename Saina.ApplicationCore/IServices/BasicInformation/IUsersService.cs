using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    /// <summary>
    /// سرویس اطلاعات کاربران
    /// </summary>
    public interface IUsersService
    {
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersActiveAsync();
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
         int SaveChanges();
        /// <summary>
        /// یک کاربر را بر اساس اطلاعات لاگین او پیدا می‌کند
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <param name="password">کلمه عبور</param>
        /// <returns>کاربر احتمالی یافت شده</returns>
        User FindUser(string username, string password);

        /// <summary>
        /// یافتن یک کاربر بر اساس کلید اصلی او
        /// </summary>
        /// <param name="userId">شماره کاربر</param>
        /// <returns>کاربر احتمالی یافت شده</returns>
        Task<User> FindUserAsync(string username, string password);

        /// <summary>
        /// یک شیء کاربر را به زمینه ایی اف اضافه می‌کند
        /// </summary>
        /// <param name="data">وهله‌ای شیء کاربر</param>
        /// <returns>کاربر اضافه شده به زمینه</returns>
        //User AddUser(User data);

        /// <summary>
        /// جهت مقاصد انقیاد داده‌ها در دبلیو پی اف طراحی شده است
        /// لیستی از کاربران سیستم را باز می‌گرداند
        /// </summary>
        /// <param name="count">تعداد کاربر مد نظر</param>
        /// <returns>لیستی از کاربران</returns>
        List<User> GetUsersList(int count = 1000);
       List<Group> GetGroupsList(int count = 1000);
      //  User DeleteUser(User user);
       // User UpdateUser(User user);
        bool ContextHasChanges { get; }

    }
}
