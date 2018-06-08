using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System.Data.Entity;
using Saina.Common.PersianToolkit;
using Saina.Common.Crypto;
using Saina.Data.Context;
using System.Windows;

namespace Saina.Data.Services.BasicInformation
{
    /// <summary>
    /// سرویس اطلاعات کاربران
    /// </summary>
    public class UsersService : IUsersService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public UsersService(SainaDbContext uow)
        {
            _uow = uow;
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        private string _username;
        #endregion
       
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async  Task<List<User>> GetUsersActiveAsync()
        {
            return await  _uow.Users.AsNoTracking().Where(x => x.IsActive == true).ToListAsync().ConfigureAwait(false);

        }
        public async  Task<List<User>> GetUsersAsync()
        {
            return await  _uow.Users.ToListAsync().ConfigureAwait(false);
        }
        public async  Task<User> GetUserAsync(int id)
        {
            return await  _uow.Users.AsNoTracking().FirstOrDefaultAsync(c => (c.UserId == id)).ConfigureAwait(false);
        }
        public async Task<User> AddUserAsync(User user)
        {
            user.Password = user.Password.SHA1Hash();
            _uow.Users.Add(user);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return user;
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            if (user.Password.Length != 40)
                user.Password = user.Password.SHA1Hash();
            _uow.Entry(user).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            //var cmd = $"EXEC User_Update @UserId = {user.UserId}, " +
            //    $"@FriendlyName = N'{user.FriendlyName}'," +
            //    $" @UserName = N'{user.UserName}'," +
            //    $" @Password = N'{user.Password}'," +
            //    $" @IsActive = {Convert.ToInt16(user.IsActive)}," +
            //    $" @GroupId = {user.GroupId}";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            return user;
        }
        public  int SaveChanges()
        {
          return   _uow.SaveChanges();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = _uow.Users.FirstOrDefault(c => c.UserId == userId);
            if (user != null)
            {
                _uow.Users.Remove(user);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// یک کاربر را بر اساس اطلاعات لاگین او پیدا می‌کند
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <param name="password">کلمه عبور</param>
        /// <returns>کاربر احتمالی یافت شده</returns>
        public User FindUser(string username, string password)
        {

            var hashedPassword = password;// password.SHA1Hash();
            if (password.Length < 40)
                hashedPassword = password.SHA1Hash();
            _username = username.ApplyCorrectYeKe();
            return _uow.Users.AsNoTracking().Include(x => x.DynamicPages).FirstOrDefault(x => x.UserName == _username && x.Password == hashedPassword);
        }

        /// <summary>
        /// یافتن یک کاربر بر اساس کلید اصلی او
        /// </summary>
        /// <param name="userId">شماره کاربر</param>
        /// <returns>کاربر احتمالی یافت شده</returns>
        public async Task<User> FindUserAsync(string username, string password)
        {
            _uow = new SainaDbContext();
            var hashedPassword = password;// password.SHA1Hash();
            if (password.Length < 40)
                hashedPassword = password.SHA1Hash();
            _username = username.ApplyCorrectYeKe();
            return await _uow.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == _username && x.Password == hashedPassword).ConfigureAwait(false);
        }

        /// <summary>
        /// جهت مقاصد انقیاد داده‌ها در دبلیو پی اف طراحی شده است
        /// لیستی از کاربران سیستم را باز می‌گرداند
        /// </summary>
        /// <param name="count">تعداد کاربر مد نظر</param>
        /// <returns>لیستی از کاربران</returns>
        public List<User> GetUsersList(int count = 1000)
        {
            return _uow.Users.OrderBy(x => x.FriendlyName).Take(count)
                      .ToList();

            // For Databinding with WPF.
            // Before calling this method you need to fill the context by using `Load()` method.
            //return _uow.Users.Local; 
        }

        public List<Group> GetGroupsList(int count = 1000)
        {
            return _uow.Groups.AsNoTracking().Include(x => x.Users).OrderBy(x => x.Name).Take(count)
                  .ToList();
        }

        #endregion


    }
}
