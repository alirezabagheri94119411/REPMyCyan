using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Common.Config;
using Saina.Common.Crypto;
using Saina.WPF.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace Saina.WPF.Login
{
    public class LoginPageViewModel : BindableBase
    {
        /// <summary>
        /// اطلاعات صفحه لاگین جهت نمایش و دریافت
        /// </summary>
        public LoginPageModel LoginPageData { set; get; }

        /// <summary>
        /// رخداد کلیک بر روی دکمه ورود به سیستم را دریافت می‌کند
        /// </summary>
        public RelayCommand DoLogin { set; get; }
        public RelayCommand DropDownOpenedCommand { get; private set; }

        readonly IAppContextService _appContextService;
        readonly IConfigSetGet _configSetGet;

        /// <summary>
        /// ویوو مدل صفحه لاگین برنامه
        /// </summary>
        /// <param name="appContextService">اطلاعات سراسری برنامه در مورد کاربر جاری را فراهم می‌کند</param>
        /// <param name="configSetGet">دسترسی به اطلاعات فایل کانفیگ برنامه</param>
        public LoginPageViewModel()
        {
            _appContextService = SmObjectFactory.Container.GetInstance<IAppContextService>();
            _configSetGet = SmObjectFactory.Container.GetInstance<IConfigSetGet>();

            LoginPageData = new LoginPageModel { UserName = _configSetGet.GetConfigData("LastLoginName"), Password = _configSetGet.GetConfigData("Password"), RememberMe = _configSetGet.GetConfigData("RememberMe") == "true" };
            if (string.IsNullOrWhiteSpace(LoginPageData.Password))
            {

            }
            DoLogin = new RelayCommand(doLogin, canDoLogin);
            DropDownOpenedCommand = new RelayCommand(onDropDownOpened);
            initUserFromConfig();
        }

        private void onDropDownOpened()
        {
            if (TestConnection(LoginPageData.ServerIp, LoginPageData.DataBaseName, LoginPageData.ServerLoginName, LoginPageData.ServerPassword))
            {
                DataBaseList = new ObservableCollection<string>(GetDatabaseList().SelectMany(x => x.Values).ToList());
            }
            else
            {
                DataBaseList = new ObservableCollection<string>();
                LoginPageData.DataBaseName = "";
            }
        }

        /// <summary>
        /// نام کاربر وارد شده به سیستم در فایل کانفیگ درج خواهد شد
        /// </summary>
        private void initUserFromConfig()
        {
            var username = _configSetGet.GetConfigData("LastLoginName");
            if (!string.IsNullOrWhiteSpace(username))
            {
                LoginPageData.UserName = username; //update view
            }
        }

        /// <summary>
        /// فعال و غیرفعال سازی دکمه لاگین
        /// </summary>
        /// <returns></returns>
        bool canDoLogin()
        {
            // return true;

            return !string.IsNullOrWhiteSpace(LoginPageData.UserName) &&
                   !string.IsNullOrWhiteSpace(LoginPageData.Password) &&
                   !string.IsNullOrWhiteSpace(LoginPageData.DataBaseName)&&
                   !string.IsNullOrWhiteSpace(LoginPageData.ServerPassword);
        }
        //public ObservableCollection<string> DataBaseList { get; set; }
        private ObservableCollection<string> _dataBaseList;

        //public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> DataBaseList
        {
            get { return _dataBaseList; }
            set
            {
                SetProperty(ref _dataBaseList, value);
            }
        }

        private static bool TestConnection(string ServerIp, string DataBaseName, string LoginName, string Password)
        {
            try
            {
                if (DataBaseName == null) DataBaseName = "";
                System.Data.SqlClient.SqlConnectionStringBuilder sqlConnectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
                //scsb.UserID = LoginName;
                //scsb.Password = Password;
                //scsb.InitialCatalog = DataBaseName;
                //scsb.DataSource = ServerIp;

                sqlConnectionStringBuilder.InitialCatalog = DataBaseName;
                sqlConnectionStringBuilder.DataSource = ServerIp;
               

                sqlConnectionStringBuilder.IntegratedSecurity = false;
                sqlConnectionStringBuilder.UserID = LoginName;
                sqlConnectionStringBuilder.Password = Password;




                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                conn.Open();
                conn.Close();
                return true;
            }
            catch
            {
                //SainaMessage.Message.ShowMessage("خطا", ("Info_MSG_25").Translate(), SainaMessage.MessageState.Error, SainaMessage.MessageShow.Ok);
                return false;
            }

        }

        private ObservableCollection<Dictionary<string, string>> GetDatabaseList()
        {
            ObservableCollection<Dictionary<string, string>> list = new ObservableCollection<Dictionary<string, string>>();
            var correctConnection = TestConnection(LoginPageData.ServerIp, LoginPageData.DataBaseName, LoginPageData.ServerLoginName, LoginPageData.ServerPassword);
            try
            {

                // Open connection to the database
                string conString = $"server={LoginPageData.ServerIp};uid={LoginPageData.ServerLoginName};pwd={LoginPageData.ServerPassword}; database={""}";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlConnection con2 = new SqlConnection(conString);
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases WHERE CASE WHEN state_desc = 'ONLINE' THEN OBJECT_ID(QUOTENAME(name) + '.[dbo].[KeyValues]', 'U') END IS NOT NULL", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {

                            while (dr.Read())
                            {
                                con2.Open();

                                using (SqlCommand cmd2 = new SqlCommand($"SELECT [Key],Value from {dr[0].ToString()}.dbo.KeyValues where [Key]='StoreName'", con2))
                                {
                                    using (IDataReader dr2 = cmd2.ExecuteReader())
                                    {
                                        while (dr2.Read())
                                        {
                                            list.Add(new Dictionary<string, string>() { { dr2[0].ToString(), dr2[1].ToString() } });
                                        }
                                    }
                                }
                                con2.Close();
                                //list.Add(dr[0].ToString()); 
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                return new ObservableCollection<Dictionary<string, string>>();

            }
            if (!list.Any())
                list.Add(new Dictionary<string, string>() { { "SainaCyan", "SainaCyan" } });
            return list;

        }
        /// <summary>
        /// سعی در ورود به سیستم
        /// </summary>
       async void doLogin()
        {
            IsBusy = true;
            var result =await _appContextService.LoginCurrentUserAsync(LoginPageData.UserName, LoginPageData.Password);
            // آیا کاربر اعتبارسنجی شده است؟
            if (result)
            {
                if (LoginPageData.RememberMe)
                {
                    if (_configSetGet.GetConfigData("LastLoginName") == "")
                    {
                        // ثبت نام کاربری او در فایل کانفیگ برنامه
                        _configSetGet.SetConfigData("LastLoginName", LoginPageData.UserName);
                        _configSetGet.SetConfigData("Password", LoginPageData.Password.SHA1Hash());
                        _configSetGet.SetConfigData("RememberMe", "true");
                    }
                }
                else
                {
                    _configSetGet.SetConfigData("LastLoginName", "");
                    _configSetGet.SetConfigData("Password", "");
                    _configSetGet.SetConfigData("RememberMe", "");

                }
                Logedin("OK");
                // هدایت به صفحه خوش آمد گویی
                // هدایت به صفحه خوش آمد گویی به همراه ارسال كوئري استرينگ
                //  Redirect.ToWelcomePage(queryStringData: "... Hello World ...");

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["SainaDbContext"].ConnectionString = $@"Data Source={LoginPageData.ServerIp};Initial Catalog={LoginPageData.DataBaseName};Persist Security Info=True;User ID={LoginPageData.ServerLoginName};Password={LoginPageData.ServerPassword}";
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            else
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";

                parameters.Header = "اخطار";
                parameters.Content = "!!لطفا مجددا سعی نمائید";

                RadWindow.Alert(parameters);
                // نمایش خطایی به کاربر در صورت عدم ورود اطلاعات صحیح یا معتبر
               // MessageBox.Show("لطفا مجددا سعی نمائید","خطا",MessageBoxButton.OK);
                //new SendMsg().ShowMsg(new AlertConfirmBoxModel
                //{
                //    ErrorTitle = "خطا",
                //    Errors = new List<string> { "لطفا مجددا سعی نمائید." },
                //    ShowCancel = Visibility.Collapsed,
                //    ShowConfirm = Visibility.Visible
                //});
            }
            IsBusy = false;
        }
        public event Action<string> Logedin;
        public void Loaded()
        {

        }

        /// <summary>
        /// آیا در حین نمایش صفحه‌ای دیگر باید به کاربر پیغام داد که اطلاعات ذخیره نشده‌ای وجود دارد؟
        /// </summary>
        //public override bool ViewModelContextHasChanges
        //{
        //    get { return false; }
        //}

    }

}
