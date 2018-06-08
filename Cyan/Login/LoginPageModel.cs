using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Login
{
    public class LoginPageModel : ValidatableBindableBase
    {
        public LoginPageModel()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["SainaDbContext"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);
            ServerIp = builder.DataSource;
            DataBaseName = builder.InitialCatalog;
            ServerLoginName = builder.UserID;
            ServerPassword = builder.Password;
        }

      
        [Required(ErrorMessage = "لطفا نام کاربری را تکمیل نمائید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را تکمیل نمائید")]
        public string Password { get; set; }
        public string ServerIp { get; set; }
        public string DataBaseName { get; set; }
        public string ServerLoginName { get; set; }
        public string ServerPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
