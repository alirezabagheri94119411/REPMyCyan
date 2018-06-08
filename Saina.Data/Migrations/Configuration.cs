namespace Saina.Data.Migrations
{
    using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
    using Saina.ApplicationCore.Entities.BasicInformation;
    using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
    using Saina.ApplicationCore.Entities.BasicInformation.Information;
    using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
    using Saina.ApplicationCore.Entities.Commerce;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Saina.Data.Context.SainaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Saina.Data.Context.SainaDbContext context)
        {
            //context.StcTypeDocuments.AddOrUpdate(x => x.StcTypeDocumentTitle,
            //  new StcTypeDocument { StcTypeDocumentId = 1,StcTypeDocumentTitle = "خرید داخلی" },
            //  new StcTypeDocument { StcTypeDocumentId = 2,StcTypeDocumentTitle = "خرید وارداتی" },
            //  new StcTypeDocument { StcTypeDocumentId = 3,StcTypeDocumentTitle = "تولید" },
            //  new StcTypeDocument { StcTypeDocumentId = 4,StcTypeDocumentTitle = "سایر" },
            //  new StcTypeDocument { StcTypeDocumentId = 5, StcTypeDocumentTitle = "موجودی اول دوره" }

            //  );

            //ماهیت تفصیل
            context.DLAccountsNatures.AddOrUpdate(x => x.DLAccountsNatureTitle,
              new DLAccountsNature { DLAccountsNatureId = 1, DLAccountsNatureTitle = "خریدار" },
              new DLAccountsNature { DLAccountsNatureId = 2, DLAccountsNatureTitle = "مشتری" },
              new DLAccountsNature { DLAccountsNatureId = 4, DLAccountsNatureTitle = "پرسنل" }

              );
            ////ویژگی
            context.Properties.AddOrUpdate(x => x.PropertyTitle,
            new Property { PropertyId = 1, PropertyTitle = "کنترل ماهیت طی دوره" },
                new Property { PropertyId = 2, PropertyTitle = "ارزی" },
                new Property { PropertyId = 3, PropertyTitle = "پیگیری" },
                new Property { PropertyId = 4, PropertyTitle = "تفصیلی1" },
                new Property { PropertyId = 5, PropertyTitle = "تفصیلی2" },
                new Property { PropertyId = 6, PropertyTitle = "تسعیر" }
                );
            //ماهیت
            context.AccountsNatures.AddOrUpdate(x => x.AccountsNatureTitle,
            new AccountsNature { AccountsNatureId = 1, AccountsNatureTitle = "بدهکار" },
                new AccountsNature { AccountsNatureId = 2, AccountsNatureTitle = "بستانکار" },
                new AccountsNature { AccountsNatureId = 3, AccountsNatureTitle = "مهم نیست" }
            );

            //نوع بانک
            context.BankTypes.AddOrUpdate(x => x.BankTypeTitle,
            new BankType { BankTypeId = 1, BankTypeTitle = "دولتی" },
                new BankType { BankTypeId = 2, BankTypeTitle = "خصوصی" },
                new BankType { BankTypeId = 3, BankTypeTitle = "موسسه" }
                );
            //نوع حساب
            context.AccountTypes.AddOrUpdate(x => x.AccountTypeTitle,
           new AccountType { AccountTypeId = 1, AccountTypeTitle = "جاری" },
                new AccountType { AccountTypeId = 2, AccountTypeTitle = "پس انداز کوتاه مدت" },
                new AccountType { AccountTypeId = 3, AccountTypeTitle = "پس انداز بلند مدت" });
            //روش
            context.Styles.AddOrUpdate(x => x.StyleTitle,
                   new Style { StyleId = 1, StyleTitle = "سال مالی" },
                new Style { StyleId = 2, StyleTitle = "بدون فیلتر" });
            //روش شماره گذاری
            context.CountingWays.AddOrUpdate(x => x.CountingWayTitle,
         new CountingWay { CountingWayId = 1, CountingWayTitle = "دستی" },
              new CountingWay { CountingWayId = 2, CountingWayTitle = "خودکار" });

            //حساب عامل
            context.SelectAgents.AddOrUpdate(x => x.SelectAgentTitle,
              new SelectAgent { SelectAgentTitle = "حساب عامل 1" });

            //بانک
            //context.Banks.AddOrUpdate(x => x.BankName,
            // new Bank { BankId = 1, BankName = "صادرات" });

            //حساب بانکی
            // context.BankAccounts.AddOrUpdate(x => x.PoseId,
            //new BankAccount { PoseId = "002305" });
            //اسناد
            context.AccountDocuments.AddOrUpdate(x => x.AccountDocumentTitle,
              new AccountDocument { AccountDocumentTitle = "سند حسابداری ", AccountDocumentId = 1 });
            //نوع سند
            context.TypeDocuments.AddOrUpdate(x => x.TypeDocumentTitle,
             new TypeDocument { TypeDocumentId = 1, TypeDocumentTitle = "سود و زیانی" },
             new TypeDocument { TypeDocumentId = 2, TypeDocumentTitle = "عمومی" },
             new TypeDocument { TypeDocumentId = 3, TypeDocumentTitle = "افتتاحیه" },
             new TypeDocument { TypeDocumentId = 4, TypeDocumentTitle = "اختتامیه" },
             new TypeDocument { TypeDocumentId = 5, TypeDocumentTitle = "تعدیل ماهیت" },
             new TypeDocument { TypeDocumentId = 6, TypeDocumentTitle = "تسعیر ارز" });
            //انبار
            //context.Stocks.AddOrUpdate(x => x.StockTitle1,
            //  new Stock { StockTitle1 = "انبار 1" });
            //کالا
            //context.Products.AddOrUpdate(x => x.ProductTitle,
            //  new Product { ProductTitle = "کالا 1" });
            ////سال مالی
            context.FinancialYears.AddOrUpdate(x => x.YearName,

              new FinancialYear { FinancialYearId = 1, YearName = 1396, StartDate = new DateTime(2017, 3, 21), EndDate = new DateTime(2018, 3, 20) },
              new FinancialYear { FinancialYearId = 2, YearName = 1397, StartDate = new DateTime(2018, 3, 21), EndDate = new DateTime(2019, 3, 20) }
              );
            //شماره گذاری اسناد
            context.DocumentNumberings.AddOrUpdate(x => x.DocumentNumberingId,
             new DocumentNumbering { DocumentNumberingId = 1, AccountDocumentId = 1, CountingWayId = 1, StyleId = 1, StartNumber = 10, EndNumber = 100 }

             );

            context.SaveChanges();

            if (!context.Users.Any())
            {

                context.Database.ExecuteSqlCommand(
@"


                              INSERT [dbo].[DynamicPages] ([DynamicPageId], [Name], [Title], [ParentId], [Order], [IconPath]) VALUES (1, N'BasicInformatins', N'اطلاعات پایه', NULL, 0, N'Resources/Icon/etelaatpayeh.png')
 SET IDENTITY_INSERT [info].[Groups] ON 

                                INSERT [info].[Groups] ([GroupId], [Name]) VALUES (1, N'Group1')
                                INSERT [info].[Groups] ([GroupId], [Name]) VALUES (2, N'Group2')
                                SET IDENTITY_INSERT [info].[Groups] OFF
                                SET IDENTITY_INSERT [info].[Users] ON 
INSERT INTO [info].[Users]([UserId],[FriendlyName],[UserName],[Password],[IsActive],[HasFullAccess],[GroupId])
VALUES (1, N'Admin', N'Admin', N'7C4A8D09CA3762AF61E59520943DC26494F8941B', 1, 1,1)
                                SET IDENTITY_INSERT [info].[Users] OFF
                                SET IDENTITY_INSERT [dbo].[UGDPs] ON 

                                INSERT [dbo].[UGDPs] ([UGDPId], [UserId], [GroupId], [DynamicPageId], [UserRights]) VALUES (1, 1, 1, 1, 0)
      ");

            }
            context.DynamicPages.AddOrUpdate(x => x.Name,
               new DynamicPage { DynamicPageId = 2, Name = "Settings", Title = "تنظیمات ", ParentId = 1, Order = 0, IconPath = "Resources/Icon/tanzimat.png" },
               new DynamicPage { DynamicPageId = 3, Name = "SystemAccountingSettings", Title = "تنظیمات سیستم حسابداری", ParentId = 2, Order = 0, IconPath = "Resources/Icon/tanzimat_hesabdari.png" },
               new DynamicPage { DynamicPageId = 4, Name = "SystemAccountingSettingView", Title = "تنظیمات سیستم حسابداری", ParentId = 2, Order = 0, IconPath = "Resources/Icon/tanzimat_hesabdari.png" },
               new DynamicPage { DynamicPageId = 5, Name = "CompanyInformations", Title = "تنظیمات اطلاعات شرکت", ParentId = 2, Order = 0, IconPath = "Resources/Icon/tanzimat_ettelaat.png" },
               new DynamicPage { DynamicPageId = 6, Name = "CompanyInformationView", Title = "تنظیمات اطلاعات شرکت", ParentId = 2, Order = 0, IconPath = "Resources/Icon/tanzimat_ettelaat.png" },
               //new DynamicPage { DynamicPageId = 7, Name = "GeneralSystemSettings", Title = "تنظیمات سیستم عمومی", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 8, Name = "GeneralSystemSettingView", Title = "تنظیمات سیستم عمومی", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 9, Name = "SystemReceivePaymentSettings", Title = "تنظیمات سیستم دریافت و پرداخت", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 10, Name = "SystemReceivePaymentSettingView", Title = "تنظیمات سیستم دریافت و پرداخت", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 11, Name = "SystemSettingRetails", Title = "تنظیمات سیستم خرده فروشی", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 12, Name = "SystemSettingRetailView", Title = "تنظیمات سیستم خرده فروشی", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 13, Name = "SalarySystemSettings", Title = "تنظیمات سیستم حقوق و دستمزد", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 14, Name = "SalarySystemSettingView", Title = "تنظیمات سیستم حقوق و دستمزد", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 15, Name = "SystemSettingSales", Title = "تنظیمات سیستم فروش", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 16, Name = "SystemSettingSaleView", Title = "تنظیمات سیستم فروش", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 17, Name = "ShoppingSystemSettings", Title = "تنظیمات سیستم بازرگانی خرید", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 18, Name = "ShoppingSystemSettingView", Title = "تنظیمات سیستم بازرگانی خرید", ParentId = 2, Order = 0, IconPath = "Resources/Icon/cian.png" },

               new DynamicPage { DynamicPageId = 19, Name = "Accounts", Title = "حساب ها", ParentId = 1, Order = 0, IconPath = "Resources/Icon/hesabha.png" },
               new DynamicPage { DynamicPageId = 20, Name = "GLs", Title = "حساب گروه", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_gorooh.png" },
               new DynamicPage { DynamicPageId = 21, Name = "GLListView", Title = "فهرست حساب های گروه", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_gorooh.png" },
               new DynamicPage { DynamicPageId = 22, Name = "AddEditGLView", Title = "افزودن /ویرایش حساب گروه", ParentId = 21, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_gorooh.png" },
               new DynamicPage { DynamicPageId = 23, Name = "TLs", Title = "حساب کل", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_kol.png" },
               new DynamicPage { DynamicPageId = 24, Name = "TLListView", Title = "فهرست حساب های کل", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_kol.png" },
               new DynamicPage { DynamicPageId = 25, Name = "AddEditTLView", Title = "افزودن /ویرایش حساب کل", ParentId = 24, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_kol.png" },
               new DynamicPage { DynamicPageId = 26, Name = "SLs", Title = "حساب معین", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_moayan.png" },
               new DynamicPage { DynamicPageId = 27, Name = "SLListView", Title = "فهرست حساب های معین", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_moayan.png" },
               new DynamicPage { DynamicPageId = 28, Name = "AddEditSLView", Title = "افزودن /ویرایش حساب معین", ParentId = 27, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_moayan.png" },
               new DynamicPage { DynamicPageId = 29, Name = "DLs", Title = "حساب تفصیل", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_tafsil.png" },
               new DynamicPage { DynamicPageId = 30, Name = "DLListView", Title = "فهرست حساب های تفصیل", ParentId = 19, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_tafsil.png" },
               new DynamicPage { DynamicPageId = 31, Name = "AddEditDLView", Title = "افزودن /ویرایش حساب تفصیل", ParentId = 30, Order = 0, IconPath = "Resources/Icon/fehrest_hesabha_tafsil.png" },

               new DynamicPage { DynamicPageId = 32, Name = "Access", Title = "دسترسی ها", ParentId = 1, Order = 0, IconPath = "Resources/Icon/dastresiha.png" },
               new DynamicPage { DynamicPageId = 33, Name = "Users", Title = "دسترسی کاربران", ParentId = 32, Order = 0, IconPath = "Resources/Icon/fehrest_karbaran.png" },
               new DynamicPage { DynamicPageId = 34, Name = "UserListView", Title = "فهرست کاربران", ParentId = 32, Order = 0, IconPath = "Resources/Icon/fehrest_karbaran.png" },
               new DynamicPage { DynamicPageId = 35, Name = "AddEditUserView", Title = "افزودن /ویرایش کاربران", ParentId = 34, Order = 0, IconPath = "Resources/Icon/fehrest_karbaran.png" },
               new DynamicPage { DynamicPageId = 36, Name = "Groups", Title = "دسترسی گروه", ParentId = 32, Order = 0, IconPath = "Resources/Icon/fehrest_goroohha.png" },
               new DynamicPage { DynamicPageId = 37, Name = "GroupListView", Title = "فهرست گروه ها", ParentId = 32, Order = 0, IconPath = "Resources/Icon/fehrest_goroohha.png" },
               new DynamicPage { DynamicPageId = 38, Name = "AddEditGroupView", Title = "افزودن /ویرایش گروه", ParentId = 37, Order = 0, IconPath = "Resources/Icon/fehrest_goroohha.png" },

               new DynamicPage { DynamicPageId = 39, Name = "BankCurrency", Title = " بانک و ارز", ParentId = 1, Order = 0, IconPath = "Resources/Icon/bank.png" },
               new DynamicPage { DynamicPageId = 40, Name = "Banks", Title = "بانک ها", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_bank.png" },
               new DynamicPage { DynamicPageId = 41, Name = "BankListView", Title = "فهرست بانک ها", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_bank.png" },
               new DynamicPage { DynamicPageId = 42, Name = "AddEditBankView", Title = "افزودن /ویرایش بانک", ParentId = 41, Order = 0, IconPath = "Resources/Icon/fehrest_bank.png" },
               new DynamicPage { DynamicPageId = 43, Name = "Currencies", Title = "ارز", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_arz.png" },
               new DynamicPage { DynamicPageId = 44, Name = "CurrencyListView", Title = "فهرست ارز", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_arz.png" },
               new DynamicPage { DynamicPageId = 45, Name = "AddEditCurrencyView", Title = "افزودن /ویرایش ارز", ParentId = 44, Order = 0, IconPath = "Resources/Icon/fehrest_arz.png" },
               new DynamicPage { DynamicPageId = 46, Name = "ExchangeRates", Title = "نرخ ارز", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_nerkharz.png" },
               new DynamicPage { DynamicPageId = 47, Name = "ExchangeRateListView", Title = "فهرست نرخ ارز", ParentId = 39, Order = 0, IconPath = "Resources/Icon/fehrest_nerkharz.png" },
               new DynamicPage { DynamicPageId = 48, Name = "AddEditExchangeRateView", Title = "افزودن /ویرایش نرخ ارز", ParentId = 47, Order = 0, IconPath = "Resources/Icon/fehrest_nerkharz.png" },

               // new DynamicPage { DynamicPageId = 49, Name = "DocumntsFlowView", Title = "گردش مرور", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               //new DynamicPage { DynamicPageId = 50, Name = "BankAccounts", Title = "حساب بانکی", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 51, Name = "BankAccountListView", Title = "فهرست حساب های بانکی", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 52, Name = "AddEditBankAccountView", Title = "افزودن /ویرایش حساب بانکی", ParentId = 51, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 53, Name = "Companies", Title = "شرکت ها", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 54, Name = "CompanyListView", Title = "فهرست اطلاعات شرکت", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 55, Name = "AddEditCompanyView", Title = "افزودن /ویرایش اطلاعات شرکت", ParentId = 54, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 56, Name = "People", Title = "پرسنل", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 57, Name = "PersonListView", Title = "فهرست پرسنل", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" },
               // new DynamicPage { DynamicPageId = 58, Name = "AddEditPersonView", Title = "افزودن /ویرایش پرسنل", ParentId = 57, Order = 0, IconPath = "Resources/Icon/cian.png" },

               new DynamicPage { DynamicPageId = 59, Name = "Documents", Title = "اسناد", ParentId = 1, Order = 0, IconPath = "Resources/Icon/asnad.png" },
               new DynamicPage { DynamicPageId = 60, Name = "AccountDocuments", Title = "اسناد حسابداری", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_asnad.png" },
               new DynamicPage { DynamicPageId = 61, Name = "AccountDocumentListView", Title = "فهرست اسناد", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_asnad.png" },
               new DynamicPage { DynamicPageId = 62, Name = "AddEditAccountDocumentView", Title = "افزودن /ویرایش سند", ParentId = 61, Order = 0, IconPath = "Resources/Icon/fehrest_asnad.png" },
               new DynamicPage { DynamicPageId = 63, Name = "DocumentNumberings", Title = "شماره گذاری اسناد", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_shomarehgozari_asnad.png" },
               new DynamicPage { DynamicPageId = 64, Name = "DocumentNumberingListView", Title = "فهرست شماره گذاری اسناد", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_shomarehgozari_asnad.png" },
               new DynamicPage { DynamicPageId = 65, Name = "AddEditDocumentNumberingView", Title = "افزودن /ویرایش شماره گذاری اسناد", ParentId = 64, Order = 0, IconPath = "Resources/Icon/fehrest_shomarehgozari_asnad.png" },
               new DynamicPage { DynamicPageId = 66, Name = "FinancialYears", Title = " سال مالی", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_sal_mali.png" },
               new DynamicPage { DynamicPageId = 67, Name = "FinancialYearListView", Title = "فهرست سال مالی", ParentId = 59, Order = 0, IconPath = "Resources/Icon/fehrest_sal_mali.png" },
               new DynamicPage { DynamicPageId = 68, Name = "AddEditFinancialYearView", Title = "افزودن /ویرایش سال مالی", ParentId = 66, Order = 0, IconPath = "Resources/Icon/fehrest_sal_mali.png" },
               new DynamicPage { DynamicPageId = 69, Name = "About", Title = "درباره ما", ParentId = 1, Order = 0, IconPath = "Resources/Icon/cian.png" },


              new DynamicPage { DynamicPageId = 77, Name = "Accounting", Title = "حسابداری", ParentId = null, Order = 0, IconPath = "Resources/Icon/hesabdari.png" },
              new DynamicPage { DynamicPageId = 78, Name = "TypeDocuments", Title = "اسناد حسابداری", ParentId = 77, Order = 0, IconPath = "Resources/Icon/sanad.png" },

              new DynamicPage { DynamicPageId = 79, Name = "TypeDocumentListView", Title = "انواع سند حسابداری", ParentId = 77, Order = 0, IconPath = "Resources/Icon/anvaahesab.png" },
              new DynamicPage { DynamicPageId = 80, Name = "AddEditTypeDocumentView", Title = "افزودن/ویرایش  سند حسابداری", ParentId = 79, Order = 0, IconPath = "Resources/Icon/anvaahesab.png" },
              //new DynamicPage { DynamicPageId = 58, Name = "AddEditTypeDocumentView", Title = "درخت حساب ها", ParentId = 77, Order = 0, IconPath = "Resources/Icon/cian.png" },
              new DynamicPage { DynamicPageId = 81, Name = "AccDocumentHeaders", Title = "صدور اسناد حسابداری", ParentId = 77, Order = 0, IconPath = "Resources/Icon/sodoorsanad.png" },
              new DynamicPage { DynamicPageId = 82, Name = "AccDocumentHeaderListView", Title = "صدور سند حسابداری", ParentId = 77, Order = 0, IconPath = "Resources/Icon/sodoorsanad.png" },
              new DynamicPage { DynamicPageId = 83, Name = "AddEditAccDocumentHeaderView", Title = "افزودن و ویرایش هدر سند حسابداری", ParentId = 82, Order = 0, IconPath = "Resources/Icon/sodoorsanad.png" },

              new DynamicPage { DynamicPageId = 84, Name = "ReviewAccounts", Title = "مرور حساب ها", ParentId = 77, Order = 0, IconPath = "Resources/Icon/moroor.png" },
              new DynamicPage { DynamicPageId = 85, Name = "ReviewAccountListView", Title = "مرور حساب ها", ParentId = 77, Order = 0, IconPath = "Resources/Icon/moroor.png" },
              new DynamicPage { DynamicPageId = 86, Name = "ConvertDocuments", Title = " تبدیل اسناد", ParentId = 77, Order = 0, IconPath = "Resources/Icon/tabdilasnad.png" },
              new DynamicPage { DynamicPageId = 87, Name = "ConvertDocumentListView", Title = " تبدیل اسناد", ParentId = 77, Order = 0, IconPath = "Resources/Icon/tabdilasnad.png" },

              new DynamicPage { DynamicPageId = 88, Name = "CurrencyExchanges", Title = "تسعیر ارز", ParentId = 77, Order = 0, IconPath = "Resources/Icon/taseer.png" },
              new DynamicPage { DynamicPageId = 89, Name = "CurrencyExchangeListView", Title = "تسعیر ارز", ParentId = 77, Order = 0, IconPath = "Resources/Icon/taseer.png" },

              new DynamicPage { DynamicPageId = 90, Name = "TLDocuments", Title = "هدر سند کل", ParentId = 77, Order = 0, IconPath = "Resources/Icon/sanadKol.png" },
              new DynamicPage { DynamicPageId = 91, Name = "TLDocumentHeaderListView", Title = " سند کل", ParentId = 77, Order = 0, IconPath = "Resources/Icon/sanadKol.png" },
              new DynamicPage { DynamicPageId = 92, Name = "TLDocumentItemListView", Title = "آیتم سند کل", ParentId = 91, Order = 0, IconPath = "Resources/Icon/sanadKol.png" },

              new DynamicPage { DynamicPageId = 93, Name = "CloseAccounts", Title = "بستن حساب های سود و زیانی", ParentId = 77, Order = 0, IconPath = "Resources/Icon/bastanhesab.png" },
              new DynamicPage { DynamicPageId = 94, Name = "CloseProfitLossAccountListView", Title = "بستن حساب ها سود و زیانی", ParentId = 77, Order = 0, IconPath = "Resources/Icon/bastanhesab.png" },

              new DynamicPage { DynamicPageId = 95, Name = "OpeningClosings", Title = "اختتامیه و افتتاحیه ", ParentId = 77, Order = 0, IconPath = "Resources/Icon/ekhtetamiye.png" },
              new DynamicPage { DynamicPageId = 96, Name = "OpeningClosingListView", Title = "اختتامیه و افتتاحیه", ParentId = 77, Order = 0, IconPath = "Resources/Icon/ekhtetamiye.png" },
                 new DynamicPage { DynamicPageId = 97, Name = "TreeAccounts", Title = "درختواره حساب ها", ParentId = 77, Order = 0, IconPath = "Resources/Icon/derakhtvareh.png" },
              new DynamicPage { DynamicPageId = 98, Name = "TreeAccountListView", Title = "درختواره حساب ها", ParentId = 77, Order = 0, IconPath = "Resources/Icon/derakhtvareh.png" }
              //new DynamicPage { DynamicPageId = 99, Name = "Warehousing", Title = "انبارداری ", ParentId = null, Order = 0, IconPath = "Resources/Icon/cian.png" }

              //new DynamicPage { DynamicPageId = 100, Name = "Stocks", Title = "انبار", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 101, Name = "StockListView", Title = "فهرست انبار", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 102, Name = "AddEditStockView", Title = "افزودن/ویرایش انبار", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 103, Name = "products", Title = "کالا", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 104, Name = "ProductListView", Title = "فهرست کالا", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 105, Name = "AddEditProductView", Title = "افزودن/ویرایش کالا", ParentId = 104, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 106, Name = "RelatedProducts", Title = "ارتباط با کالا", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 107, Name = "ProductModelListView", Title = "فهرست مدل کالا", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 108, Name = "ProductTypeListView", Title = "فهرست نوع کالا", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 109, Name = "ProductBrandListView", Title = "فهرست برند کالا", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 110, Name = "OtherProductListView", Title = "فهرست سایر کالا", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 111, Name = "MeasurementUnitListView", Title = "فهرست واحد سنجش", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 112, Name = "ProductRackListView", Title = "فهرست قفسه کالا", ParentId = 106, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 113, Name = "WarehouseDocuments", Title = "اسناد انبار", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 114, Name = "IncomingDocumentListView", Title = "اسناد ورودی", ParentId = 113, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 115, Name = "OutputDocumentListView", Title = "اسناد خروجی", ParentId = 113, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 116, Name = "ReturnInputListView", Title = "اسناد برگشت ورودی", ParentId = 113, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 117, Name = "ReturnOutputListView", Title = "اسناد برگشت خروجی", ParentId = 113, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 118, Name = "TransferWarehouseListView", Title = "اسناد انتقال بین انبار", ParentId = 113, Order = 0, IconPath = "Resources/Icon/cian.png" },
              //new DynamicPage { DynamicPageId = 119, Name = "TestView", Title = "", ParentId = 99, Order = 0, IconPath = "Resources/Icon/cian.png" }

              );
            context.UGDPs.AddOrUpdate(x => x.DynamicPageId,
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 1, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 2, UserRights = 0 },
                 new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 3, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 4, UserRights = 0 },
                 new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 5, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 6, UserRights = 0 },
                // new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 7, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 8, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 9, UserRights = 0 },
                // new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 10, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 11, UserRights = 0 },
                // new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 12, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 13, UserRights = 0 },
                // new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 14, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 15, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 16, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 17, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 18, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 19, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 20, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 21, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 22, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 23, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 24, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 25, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 26, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 27, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 28, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 29, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 30, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 31, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 32, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 33, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 34, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 35, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 36, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 37, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 38, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 39, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 40, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 41, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 42, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 43, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 44, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 45, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 46, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 47, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 48, UserRights = 0 },
                // new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 49, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 50, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 51, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 52, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 53, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 54, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 55, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 56, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 57, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 58, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 59, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 60, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 61, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 62, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 63, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 64, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 65, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 66, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 67, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 68, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 69, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 70, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 71, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 72, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 73, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 74, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 75, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 76, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 77, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 78, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 79, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 80, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 81, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 82, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 83, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 84, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 85, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 86, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 87, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 88, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 89, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 90, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 91, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 92, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 93, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 94, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 95, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 96, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 97, UserRights = 0 },
                new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 98, UserRights = 0 }
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 99, UserRights = 0 }
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 100, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 101, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 102, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 103, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 104, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 105, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 106, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 107, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 108, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 109, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 110, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 111, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 112, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 113, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 114, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 115, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 116, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 117, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 118, UserRights = 0 },
                //new UGDP { UserId = 1, GroupId = 1, DynamicPageId = 119, UserRights = 0 }
                );
            context.Owners.AddOrUpdate(x => x.OwnerName,
              new Owner { OwnerName = "Info", OwnerTitle = "اطلاعات پایه", DisplayIndex = 1 },
              new Owner { OwnerName = "Acc", OwnerTitle = "حسابداری", DisplayIndex = 2 }
              );
            context.Views.AddOrUpdate(x => x.ViewName,
            new View { ViewName = "SystemAccountingSettingView", ViewPersianName = "تنظیمات سیستم حسابداری", DisplayIndex = 1, OwnerId = 1 },
            new View { ViewName = "CompanyInformationView", ViewPersianName = "تنظیمات اطلاعات شرکت", DisplayIndex = 2, OwnerId = 1 },
            new View { ViewName = "GLListView", ViewPersianName = "حساب گروه", DisplayIndex = 3, OwnerId = 1 },
            new View { ViewName = "TLListView", ViewPersianName = "حساب کل", DisplayIndex = 4, OwnerId = 1 },
            new View { ViewName = "SLListView", ViewPersianName = "حساب معین", DisplayIndex = 5, OwnerId = 1 },
            new View { ViewName = "DLListView", ViewPersianName = "حساب تفصیل", DisplayIndex = 6, OwnerId = 1 },
            new View { ViewName = "UserListView", ViewPersianName = " لیست کاربران", DisplayIndex = 7, OwnerId = 1 },
            new View { ViewName = "AddEditUserView", ViewPersianName = " افزودن و ویرایش کاربران", DisplayIndex = 8, OwnerId = 1 },
            new View { ViewName = "GroupListView", ViewPersianName = " لیست گروه", DisplayIndex = 9, OwnerId = 1 },
            new View { ViewName = "AddEditGroupView", ViewPersianName = "افزودن و ویرایش گروه", DisplayIndex = 10, OwnerId = 1 },
            new View { ViewName = "BankListView", ViewPersianName = "لیست بانک", DisplayIndex = 11, OwnerId = 1 },
            new View { ViewName = "AddEditBankView", ViewPersianName = "افزودن و ویرایش بانک", DisplayIndex = 12, OwnerId = 1 },
            new View { ViewName = "CurrencyListView", ViewPersianName = "لیست ارز", DisplayIndex = 13, OwnerId = 1 },
            new View { ViewName = "AddEditCurrencyView", ViewPersianName = "افزودن و ویرایش لیست ارز", DisplayIndex = 14, OwnerId = 1 },
            new View { ViewName = "ExchangeRateListView", ViewPersianName = "لیست نرخ ارز", DisplayIndex = 15, OwnerId = 1 },
            new View { ViewName = "AddEditExchangeRateView", ViewPersianName = "افزودن و ویرایش نرخ ارز", DisplayIndex = 16, OwnerId = 1 },
            new View { ViewName = "AccountDocumentListView", ViewPersianName = "لیست اسناد شماره گذاری", DisplayIndex = 17, OwnerId = 1 },
            new View { ViewName = "AddEditAccountDocumentView", ViewPersianName = "افزودن و ویرایش اسناد شماره گذاری", DisplayIndex = 18, OwnerId = 1 },
            new View { ViewName = "DocumentNumberingListView", ViewPersianName = "لیست شماره گذاری اسناد", DisplayIndex = 19, OwnerId = 1 },
            new View { ViewName = "AddEditDocumentNumberingView", ViewPersianName = "افزودن و ویرایش شماره گذاری اسناد", DisplayIndex = 20, OwnerId = 1 },
            new View { ViewName = "FinancialYearListView", ViewPersianName = "لیست سال مالی", DisplayIndex = 21, OwnerId = 1 },
            new View { ViewName = "AddEditFinancialYearView", ViewPersianName = "افزودن و ویراش سال مالی", DisplayIndex = 22, OwnerId = 1 },
            new View { ViewName = "TypeDocumentListView", ViewPersianName = "لیست انواع سند حسابداری", DisplayIndex = 23, OwnerId = 2 },
            new View { ViewName = "AddEditTypeDocumentView", ViewPersianName = "افزودن و ویرایش انواع اسناد حسابداری", DisplayIndex = 24, OwnerId = 2 },
            new View { ViewName = "AccDocumentHeaderListView", ViewPersianName = "صدور سند حسابداری", DisplayIndex = 25, OwnerId = 2 },
            new View { ViewName = "ReviewAccountListView", ViewPersianName = "مرور حساب ها", DisplayIndex = 26, OwnerId = 2 },
            new View { ViewName = "ConvertDocumentListView", ViewPersianName = "تبدیل اسناد", DisplayIndex = 27, OwnerId = 2 },
            new View { ViewName = "CurrencyExchangeListView", ViewPersianName = "تسعیر ارز", DisplayIndex = 28, OwnerId = 2 },
            new View { ViewName = "TLDocumentHeaderListView", ViewPersianName = "هدر سند کل", DisplayIndex = 29, OwnerId = 2 },
            new View { ViewName = "TLDocumentItemListView", ViewPersianName = "آیتم سند کل", DisplayIndex = 30, OwnerId = 2 },
            new View { ViewName = "CloseProfitLossAccountListView", ViewPersianName = "سود و زیانی", DisplayIndex = 31, OwnerId = 2 },
            new View { ViewName = "OpeningClosingListView", ViewPersianName = "افتتاحیه و اختتامیه", DisplayIndex = 32, OwnerId = 2 },
            new View { ViewName = "TreeAccountListView", ViewPersianName = "درخت حساب ها", DisplayIndex = 33, OwnerId = 2 }

            );
            context.Operations.AddOrUpdate(x => x.DisplayIndex,
            new Operation { OperationName = "Save", OperationPersianName = "ذخیره", DisplayIndex = 1, ViewId = 1 },
            new Operation { OperationName = "Save", OperationPersianName = "ذخیره", DisplayIndex = 2, ViewId = 2 },
            new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 3, ViewId = 3 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 4, ViewId = 3 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 5, ViewId = 3 },
             new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 6, ViewId = 4 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 7, ViewId = 4 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 8, ViewId = 4 },

             new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 9, ViewId = 5 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 10, ViewId = 5 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 11, ViewId = 5 },

           new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 12, ViewId = 6 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 13, ViewId = 6 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 14, ViewId = 6 },
            new Operation { OperationName = "Information", OperationPersianName = "اطلاعات بیشتر", DisplayIndex = 15, ViewId = 6 },
            new Operation { OperationName = "Related", OperationPersianName = "افراد مرتبط", DisplayIndex = 16, ViewId = 6 },

             new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 17, ViewId = 7 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 18, ViewId = 7 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 19, ViewId = 7 },
            new Operation { OperationName = "Access", OperationPersianName = "دسترسی", DisplayIndex = 20, ViewId = 7 },

                new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 21, ViewId = 9 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 22, ViewId = 9 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 23, ViewId = 9 },
            new Operation { OperationName = "Access", OperationPersianName = "دسترسی", DisplayIndex = 24, ViewId = 9 },

           new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 25, ViewId = 11 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 26, ViewId = 11 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 27, ViewId = 11 },

              new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 28, ViewId = 13 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 29, ViewId = 13 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 30, ViewId = 13 },

                new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 31, ViewId = 15 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 32, ViewId = 15 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 33, ViewId = 15 },

             new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 34, ViewId = 17 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 35, ViewId = 17 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 36, ViewId = 17 },

                  new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 37, ViewId = 19 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 38, ViewId = 19 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 39, ViewId = 19 },

                  new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 40, ViewId = 21 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 41, ViewId = 21 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 42, ViewId = 21 },

                  new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 43, ViewId = 23 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 44, ViewId = 23 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 45, ViewId = 23 },

            new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 46, ViewId = 25 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 47, ViewId = 25 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 48, ViewId = 25 },
            new Operation { OperationName = "Cancel", OperationPersianName = "انصراف", DisplayIndex = 49, ViewId = 25 },
            new Operation { OperationName = "DraftButton", OperationPersianName = "پیش نویس", DisplayIndex = 50, ViewId = 25 },
            new Operation { OperationName = "TemporaryButton", OperationPersianName = "موقت", DisplayIndex = 51, ViewId = 25 },
            new Operation { OperationName = "EndButton", OperationPersianName = "خاتمه", DisplayIndex = 52, ViewId = 25 },
            new Operation { OperationName = "BackFromEndButton", OperationPersianName = "برگشت از خاتمه", DisplayIndex = 53, ViewId = 25 },
            new Operation { OperationName = "PermanentButton", OperationPersianName = "دائم", DisplayIndex = 54, ViewId = 25 },
            new Operation { OperationName = "History", OperationPersianName = "تاریخچه", DisplayIndex = 55, ViewId = 25 },

            new Operation { OperationName = "Base", OperationPersianName = "سند مبنا", DisplayIndex = 56, ViewId = 25 },
            new Operation { OperationName = "Attachment", OperationPersianName = "مکاتبات", DisplayIndex = 57, ViewId = 25 },
            new Operation { OperationName = "Numbering", OperationPersianName = "شماره گذاری", DisplayIndex = 58, ViewId = 25 },

            new Operation { OperationName = "Temporary", OperationPersianName = "خاتمه", DisplayIndex = 59, ViewId = 27 },
            new Operation { OperationName = "Permanent", OperationPersianName = "دائم", DisplayIndex = 60, ViewId = 27 },

            new Operation { OperationName = "Export", OperationPersianName = "صدور سند", DisplayIndex = 61, ViewId = 28 },
            new Operation { OperationName = "DisplayAcc", OperationPersianName = "نمایش سند", DisplayIndex = 62, ViewId = 28 },
            new Operation { OperationName = "DisplayItems", OperationPersianName = "نمایش اقلام", DisplayIndex = 63, ViewId = 28 },

            new Operation { OperationName = "Export", OperationPersianName = "صدور", DisplayIndex = 64, ViewId = 29 },
            new Operation { OperationName = "DisplayItems", OperationPersianName = "نمایش اقلام", DisplayIndex = 65, ViewId = 29 },
             new Operation { OperationName = "Delete", OperationPersianName = "حذف سند", DisplayIndex = 66, ViewId = 29 },
            new Operation { OperationName = "DisplayAcc", OperationPersianName = "نمایش سند", DisplayIndex = 67, ViewId = 31 },
             new Operation { OperationName = "Export", OperationPersianName = "صدور سند", DisplayIndex = 68, ViewId = 32 },
            new Operation { OperationName = "DisplayAcc", OperationPersianName = "نمایش سند", DisplayIndex = 69, ViewId = 32 },
             new Operation { OperationName = "Add", OperationPersianName = "افزودن", DisplayIndex = 70, ViewId = 33 },
            new Operation { OperationName = "Edit", OperationPersianName = "ویرایش", DisplayIndex = 71, ViewId = 33 },
            new Operation { OperationName = "Delete", OperationPersianName = "حذف", DisplayIndex = 72, ViewId = 33 }
            );
            context.Accesses.AddOrUpdate(x => x.OperationId,
new Access { UserId = 1, GroupId = 1, OperationId = 1, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 2, HasAccess = true },

new Access { UserId = 1, GroupId = 1, OperationId = 3, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 4, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 5, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 6, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 7, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 8, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 9, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 10, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 11, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 12, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 13, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 14, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 15, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 16, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 17, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 18, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 19, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 20, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 21, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 22, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 23, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 24, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 25, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 26, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 27, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 28, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 29, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 30, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 31, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 32, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 33, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 34, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 35, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 36, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 37, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 38, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 39, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 40, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 41, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 42, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 43, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 44, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 45, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 46, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 47, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 48, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 49, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 50, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 51, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 52, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 53, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 54, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 55, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 56, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 57, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 58, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 59, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 60, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 61, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 62, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 63, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 64, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 65, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 66, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 67, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 68, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 69, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 70, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 71, HasAccess = true },
new Access { UserId = 1, GroupId = 1, OperationId = 72, HasAccess = true }
);
            context.SaveChanges();
            base.Seed(context);

        }
    }
}
