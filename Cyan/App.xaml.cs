using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.Common.Toolkit;
using Saina.Data.Context;
using Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyDocument;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.DocumentInfo;
using Saina.WPF.Accounting.DocumentAccounting.DocumentNumberinginfo;
using Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo;
using Saina.WPF.Accounting.DocumentAccounting.TreeACC;
using Saina.WPF.BasicInformation.Accounts.DLAccount;
using Saina.WPF.BasicInformation.Accounts.GLAccount;
using Saina.WPF.BasicInformation.Accounts.SLAccount;
using Saina.WPF.BasicInformation.Accounts.TLAccount;
using Saina.WPF.BasicInformation.Admin;
using Saina.WPF.BasicInformation.Admin.GroupAccess;
using Saina.WPF.BasicInformation.Admin.UserAccess;
using Saina.WPF.BasicInformation.Financial;
using Saina.WPF.BasicInformation.Information.BankAccounts;
using Saina.WPF.BasicInformation.Information.BankInfo;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
using Saina.WPF.BasicInformation.Information.PersonInfo;
//using Saina.WPF.BasicInformation.Information.PersonInfo;
using Saina.WPF.BasicInformation.Information.Related;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.WPF.BasicInformation.Settings.ShoppingSetting;
using Saina.WPF.Commerce.CommProduct;
using Saina.WPF.Commerce.CommStock;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace Saina.WPF
{
    public partial class App
    {
        /// <summary>
        /// آغاز کننده برنامه
        /// </summary>
        public App()
        {
            checkSingleInstanceApplication();
            wireUpEvents();
          InitializePersianCulture();
            //StartDb.InitDb();
            // InitializePersianCulture();
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = PersianDateExtensionMethods.GetPersianCulture();
            // setCultureInfo();
            createMaps();

        }

        private void createMaps()
        {
            Mapper.Initialize(
                cfg =>
                {

                    //cfg.CreateMap<DynamicPage, DynamicPage>().MaxDepth(1);
                    //cfg.CreateMap<Customer, Customers.SimpleEditableCustomer>().ReverseMap();
                    cfg.CreateMap<User, EditableUser>().ReverseMap();
                    cfg.CreateMap<Group, EditableGroup>().ReverseMap();
                    cfg.CreateMap<FinancialYear, EditableFinancialYear>().ReverseMap();
                    //    cfg.CreateMap<GL, EditableGL>().ReverseMap();
                    //    cfg.CreateMap<TL, EditableTL>().ReverseMap();
                    //    cfg.CreateMap<SL, EditableSL>().ReverseMap();
                    cfg.CreateMap<DL, EditableDL>().ReverseMap();
                    cfg.CreateMap<Bank, EditableBank>().ReverseMap();
                    cfg.CreateMap<BankAccount, EditableBankAccount>().ReverseMap();
                    cfg.CreateMap<Currency, EditableCurrency>().ReverseMap();
                    cfg.CreateMap<ExchangeRate, EditableExchangeRate>().ReverseMap();
                    cfg.CreateMap<Person, EditablePerson>().ReverseMap();
                    cfg.CreateMap<Company, EditableCompany>().ReverseMap();
                    cfg.CreateMap<DocumentNumbering, EditableDocumentNumbering>().ReverseMap();
                    cfg.CreateMap<AccountDocument, EditableAccountDocument>().ReverseMap();
                    // cfg.CreateMap<SLStandardDescription, EditableSLStandardDescription>().ReverseMap();
                    cfg.CreateMap<Stock, EditableStock>().ReverseMap();
                    cfg.CreateMap<Product, EditableProduct>().ReverseMap();
                    cfg.CreateMap<TypeDocument, EditableTypeDocument>().ReverseMap();
                    cfg.CreateMap<RelatedPerson, EditableRelatedPerson>().ReverseMap();
                    cfg.CreateMap<EditableSystemAccountingSettingModel, SystemAccountingSettingModel>().ReverseMap();
                    cfg.CreateMap<AccDocumentHeader, EditableAccDocumentHeader>().ReverseMap().MaxDepth(1);
                    cfg.CreateMap<AccDocumentItem, EditableAccDocumentItem>().ReverseMap().MaxDepth(1);
                    cfg.CreateMap<TLDocumentHeader, EditableTLDocumentHeader>().ReverseMap().MaxDepth(1);
                    // cfg.CreateMap<TreeAccount, EditableTreeAccount>().ReverseMap();
                    cfg.CreateMap<TreeAccount, EditableTLDocumentItem>().ReverseMap();
                    cfg.CreateMap<EditableShoppingSystemSettingViewModel, ShoppingSystemSettingModel>().ReverseMap();

                }
            );

        }

        public static void InitializePersianCulture()
        {
            setCultureInfo("fa-ir", new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                              new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" },
                              new[]
                                  {
                                      "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی",
                                      "بهمن", "اسفند", ""
                                  },
                              new[]
                                  {
                                      "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی",
                                      "بهمن", "اسفند", ""
                                  }, "ق.ظ. ", "ب.ظ. ", "yyyy/MM/dd", new PersianCalendar());
        }

        public static void setCultureInfo(string culture, string[] abbreviatedDayNames, string[] dayNames,
                                          string[] abbreviatedMonthNames, string[] monthNames, string amDesignator,
                                          string pmDesignator, string shortDatePattern, Calendar calendar)
        {
            var calture = new CultureInfo(culture);
            var info = calture.DateTimeFormat;
            info.AbbreviatedDayNames = abbreviatedDayNames;
            info.DayNames = dayNames;
            info.AbbreviatedMonthNames = abbreviatedMonthNames;
            info.MonthNames = monthNames;
            info.AMDesignator = amDesignator;
            info.PMDesignator = pmDesignator;
            info.ShortDatePattern = shortDatePattern;
            info.FirstDayOfWeek = DayOfWeek.Saturday;
            var cal = calendar;
            var type = typeof(DateTimeFormatInfo);
            var fieldInfo = type.GetField("calendar", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
                fieldInfo.SetValue(info, cal);
            var field = typeof(CultureInfo).GetField("calendar", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                field.SetValue(calture, cal);
            Thread.CurrentThread.CurrentCulture = calture;
            Thread.CurrentThread.CurrentUICulture = calture;
            CultureInfo.CurrentCulture.DateTimeFormat = info;
            CultureInfo.CurrentUICulture.DateTimeFormat = info;
        }


        private static void setCultureInfo1()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-IR");
            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            //            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            CultureInfo culutreInfo = new CultureInfo("fa-IR");
            culutreInfo.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culutreInfo;
            //برای نمایش اعداد به صورت فارسی
            // Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentUICulture = culutreInfo;

            FrameworkElement.LanguageProperty.OverrideMetadata(
typeof(FrameworkElement),
new FrameworkPropertyMetadata(
XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

        }
      
        private void wireUpEvents()
        {
            this.Exit += appExit;
            this.Deactivated += appDeactivated;
            this.Startup += appStartup;
            this.DispatcherUnhandledException += appDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += currentDomainUnhandledException;
        }

        private void checkSingleInstanceApplication()
        {
            var process = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (process.Length > 1)
            {
                MessageBox.Show("نسخ دیگری از برنامه در حال اجراست", "برنامه حسابداری ساینا", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Shutdown();
            }
        }

        static void appDeactivated(object sender, EventArgs e)
        {
            Memory.ReEvaluateWorkingSet();
        }

        static void appStartup(object sender, StartupEventArgs e)
        {
            reducingCpuConsumptionForAnimations();
        }

        static void reducingCpuConsumptionForAnimations()
        {
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
                 typeof(Timeline),
                 new FrameworkPropertyMetadata { DefaultValue = 20 }
                 );
        }

        static void currentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // این نوع خطاها عموما موارد مدیریت نشده در تردها هستند که بهتر است برنامه اینجا تمام شود
            var exception = (Exception)e.ExceptionObject;
            MessageBox.Show(exception.Message, "خطای مدیریت نشده");
        }

        static void appExit(object sender, ExitEventArgs e)
        {
        }

        private static void appDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "خطای عمومی:");
            e.Handled = true;
        }
    }
    public static class PersianDateExtensionMethods
    {
        private static CultureInfo _Culture;
        public static CultureInfo GetPersianCulture()
        {
            if (_Culture == null)
            {
                _Culture = new CultureInfo("fa-IR");
                DateTimeFormatInfo formatInfo = _Culture.DateTimeFormat;
                formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
                var monthNames = new[]
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
                formatInfo.AbbreviatedMonthNames =
                    formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
                formatInfo.AMDesignator = "ق.ظ";
                formatInfo.PMDesignator = "ب.ظ";
                formatInfo.ShortDatePattern = "yyyy/MM/dd";
                formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
                formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
                System.Globalization.Calendar cal = new PersianCalendar();

                FieldInfo fieldInfo = _Culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                    fieldInfo.SetValue(_Culture, cal);

                FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(formatInfo, cal);

                _Culture.NumberFormat.NumberDecimalSeparator = "/";
                _Culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
                _Culture.NumberFormat.NumberNegativePattern = 0;
            }
            return _Culture;
        }

        public static string ToPeString(this DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToString(format, GetPersianCulture());
        }
    }
}
