using Saina.ApplicationCore.Entities;
using Saina.Common.PersianToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Saina.Common.EntityFrameworkToolkit;
using System.Threading.Tasks;
using System.Windows;
using Saina.ApplicationCore.IServices.BasicInformation;
using Z.EntityFramework.Plus;
using System.Threading;
using Saina.Common.Toolkit;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using EntityFramework.Functions;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Saina.Data.Context
{
    /// <summary>
    /// پیاده سازی الگوی واحد کار
    /// </summary>
    public class MyDbContextBase : DbContext
    {
        public MyDbContextBase()
        {
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
                   // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                   (context as MyDbContextBase).AuditEntries.AddRange(audit.Entries);
            //Database.SetInitializer<MyDbContextBase>(null);
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }


        /// <summary>
        /// بازگردانی تغییرات انجام شده در رکوردهای تحت نظر در حافظه به حالت اول
        /// </summary>
        public void RejectChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        ////private void auditFields(string auditFinancialYear)
        ////{
        ////    foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
        ////    {
        ////        switch (entry.State)
        ////        {
        ////            case EntityState.Added:
        ////                setCreatedOn(auditFinancialYear, entry);
        ////                setModifiedOn(auditFinancialYear, entry);
        ////                break;

        ////            case EntityState.Modified:
        ////                setModifiedOn(auditFinancialYear, entry);
        ////                break;
        ////        }
        ////    }
        ////}

        ////private static void setModifiedOn(string auditFinancialYear, DbEntityEntry<BaseEntity> entry)
        ////{
        ////    entry.Entity.ModifiedOn = DateTime.Now;
        ////    entry.Entity.ModifiedBy = auditFinancialYear;
        ////}

        ////private static void setCreatedOn(string auditFinancialYear, DbEntityEntry<BaseEntity> entry)
        ////{
        ////    entry.Entity.CreatedOn = DateTime.Now;
        ////    entry.Entity.CreatedBy = auditFinancialYear;
        ////    entry.Entity.CreatedOnPersian = PersianDate.CurrentSystemShamsiDate("/", true);
        ////}

        /// <summary>
        /// دسترسی به اعمال قابل انجام با یک موجودیت را فراهم می‌کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>        
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        /// <summary>
        /// ذخیره سازی کلیه تغییرات انجام شده در تمامی رکوردهای تحت نظر در حافظه
        /// </summary>
        /// <param name="userName">نام کاربر جاری</param>
        /// <param name="updateAuditFields">آیا فیلدهای ویرایش کننده اطلاعات نیز مقدار دهی شوند؟</param>
        /// <returns>تعداد رکوردهای تغییر کرده</returns>


        private bool hasValidationErrors()
        {
            var validationErrors = this.GetValidationErrors();
            if (validationErrors == null || !validationErrors.Any())
                return false;

            var results = new List<string>();
            foreach (var item in validationErrors)
            {
                results.Add(string.Join(Environment.NewLine, item.ValidationErrors.Select(x => x.ErrorMessage)));
            }

            //نمایش خطاهای اعتبار سنجی به کاربر
            //new SendMsg().ShowMsg(
            //    new AlertConfirmBoxModel
            //    {
            //        ErrorTitle = "لطفا خطاهای زیر را پیش از ثبت نهایی برطرف کنید:",
            //        Errors = results
            //    });
            MessageBox.Show(results.FirstOrDefault(), "خطای همزمانی لطفا خطاهای زیر را پیش از ثبت نهایی برطرف کنید");
            return true;
        }

        /// <summary>
        /// برای ذخیره سازی تعداد زیادی رکورد با هم کاربرد دارد. در غیراینصورت از آن استفاده نکنید
        /// </summary>
        public void DisableChangeTracking()
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        /// <summary>
        /// آیا در رکوردهای تحت نظر در حافظه تغییری حاصل شده است؟
        /// </summary>
        public bool ContextHasChanges
        {
            get
            {
                return this.ChangeTracker
                           .Entries()
                           .Any(x => x.State == EntityState.Added ||
                                     x.State == EntityState.Deleted ||
                                     x.State == EntityState.Modified);
            }
        }

        public override int SaveChanges()
        {
            var test = this.ChangeTracker
                           .Entries()
                           .Where(x => x.State == EntityState.Added ||
                                     x.State == EntityState.Deleted ||
                                     x.State == EntityState.Modified).ToList();
            //try
            //{
            if (this.Configuration.AutoDetectChangesEnabled)
                this.ApplyCorrectYeKe();
            if (hasValidationErrors())
                return 0;

            var audit = new Audit();
            audit.CreatedBy = SainaEnvironment.CurrentUserName;
            audit.PreSaveChanges(this);
            var rowAffecteds = base.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                base.SaveChanges();
            }

            return rowAffecteds;
            //}
          
            //catch (DbEntityValidationException validationException)
            //{
            //    var errors = new List<string>();
            //    foreach (var error in validationException.EntityValidationErrors)
            //    {
            //        var entry = error.Entry;
            //        errors.Add(entry.Entity.GetType().Name + ": " + entry.State);
            //        foreach (var err in error.ValidationErrors)
            //        {
            //            errors.Add(err.PropertyName + " " + err.ErrorMessage);
            //        }
            //    }

            //    MessageBox.Show(errors.FirstOrDefault(), "خطای اعتبار سنجی");

            //}
            //catch (DbUpdateConcurrencyException concurrencyException)
            //{
            //    var errors = "مقادیر در سمت بانک اطلاعاتی تغییر کرده‌اند. لطفا صفحه را ریفرش کنید" + ": "
            //        + concurrencyException.Entries.First().Entity.GetType().Name;

            //    MessageBox.Show(errors, "خطای همزمانی");
            //}

            //catch (DbUpdateException updateException)
            //{
            //    var errors = new List<string> { updateException.Message };
            //    if (updateException.InnerException != null)
            //        errors.Add(updateException.InnerException.Message);

            //    foreach (var entry in updateException.Entries)
            //    {
            //        errors.Add("Related Entity: " + entry.Entity);
            //    }

            //    MessageBox.Show(errors.FirstOrDefault(), "خطای همزمانی");

            //}

            return 0;
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

            try
            {
                if (Configuration.AutoDetectChangesEnabled)
                    this.ApplyCorrectYeKe();
                if (hasValidationErrors())
                    return 0;


                var audit = new Audit();
                audit.CreatedBy = SainaEnvironment.CurrentUserName;
                audit.PreSaveChanges(this);
                var rowAffecteds = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                audit.PostSaveChanges();

                if (audit.Configuration.AutoSavePreAction != null)
                {
                    audit.Configuration.AutoSavePreAction(this, audit);
                    await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }

                return rowAffecteds;
            }
            catch (DbEntityValidationException validationException)
            {
                var errors = new List<string>();
                foreach (var error in validationException.EntityValidationErrors)
                {
                    var entry = error.Entry;
                    errors.Add(entry.Entity.GetType().Name + ": " + entry.State);
                    foreach (var err in error.ValidationErrors)
                    {
                        errors.Add(err.PropertyName + " " + err.ErrorMessage);
                    }
                }
                throw;
                //MessageBox.Show(errors.FirstOrDefault(), "خطای اعتبار سنجی");
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                //var errors = "مقادیر در سمت بانک اطلاعاتی تغییر کرده‌اند. لطفا صفحه را ریفرش کنید" + ": "
                //    + concurrencyException.Entries.First().Entity.GetType().Name;

                //MessageBox.Show(errors, "خطای همزمانی");
                ResolveConcurrencyConflicts(concurrencyException);
                SaveWithConcurrencyResolution(this);
            }
            catch (DbUpdateException updateException)
            {
                //var errors = new List<string> { updateException.Message };
                //if (updateException.InnerException != null)
                //    errors.Add(updateException.InnerException.Message);

                //foreach (var entry in updateException.Entries)
                //{
                //    errors.Add("Related Entity: " + entry.Entity);
                //}

                //MessageBox.Show(errors.FirstOrDefault(), "خطای همزمانی");

            }
            return 0;
        }
        private static void SaveWithConcurrencyResolution(MyDbContextBase context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ResolveConcurrencyConflicts(ex);
                SaveWithConcurrencyResolution(context);
            }
        }
        private static void ResolveConcurrencyConflicts(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                Console.WriteLine(
                "Concurrency conflict found for {0}",
                entry.Entity.GetType());
                Console.WriteLine("\nYou are trying to save the following values:");
                PrintPropertyValues(entry.CurrentValues);
                Console.WriteLine("\nThe values before you started editing were:");
                PrintPropertyValues(entry.OriginalValues);
                var databaseValues = entry.GetDatabaseValues();
                Console.WriteLine("\nAnother user has saved the following values:");
                PrintPropertyValues(databaseValues);
                Console.Write(
                "[S]ave your values, [D]iscard you changes or [M]erge?");
                var action = Console.ReadKey().KeyChar.ToString().ToUpper();
                switch (action)
                {
                    case "S":
                        entry.OriginalValues.SetValues(databaseValues);
                        break;
                    case "D":
                        entry.Reload();
                        break;
                    case "M":
                        var mergedValues = MergeValues(
                        entry.OriginalValues,
                        entry.CurrentValues,
                        databaseValues);
                        entry.OriginalValues.SetValues(databaseValues);
                        entry.CurrentValues.SetValues(mergedValues);
                        break;
                    default:
                        throw new ArgumentException("Invalid option");
                }
            }
        }

        private static void PrintPropertyValues(DbPropertyValues currentValues)
        {

        }

        private static DbPropertyValues MergeValues(DbPropertyValues original, DbPropertyValues current, DbPropertyValues database)
        {
            var result = original.Clone();
            foreach (var propertyName in original.PropertyNames)
            {
                if (original[propertyName] is DbPropertyValues)
                {
                    var mergedComplexValues = MergeValues(
                    (DbPropertyValues)original[propertyName],
                    (DbPropertyValues)current[propertyName],
                    (DbPropertyValues)database[propertyName]);
                    ((DbPropertyValues)result[propertyName])
                    .SetValues(mergedComplexValues);
                }
                else
                {
                    if (!object.Equals(
                    current[propertyName],
                    original[propertyName]))
                    {
                        result[propertyName] = current[propertyName];
                    }
                    else if (!object.Equals(
                    database[propertyName],
                    original[propertyName]))
                    {
                        result[propertyName] = database[propertyName];
                    }
                }
            }
            return result;
        }
        private void PrintPropertyValues(DbPropertyValues values, IEnumerable<string> propertiesToPrint, int indent = 1)
        {
            foreach (var propertyName in propertiesToPrint)
            {
                var value = values[propertyName];
                if (value is DbPropertyValues)
                {
                    Console.WriteLine(
                    "{0}- Complex Property: {1}",
                    string.Empty.PadLeft(indent),
                    propertyName);
                    var complexPropertyValues = (DbPropertyValues)value;
                    PrintPropertyValues(
                    complexPropertyValues,
                    complexPropertyValues.PropertyNames,
                    indent + 1);
                }
                else
                {
                    Console.WriteLine(
                    "{0}- {1}: {2}",
                    string.Empty.PadLeft(indent),
                    propertyName,
                    values[propertyName]);
                }
            }
        }
        private IEnumerable<string> GetKeyPropertyNames(object entity)
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            return objectContext
            .ObjectStateManager
            .GetObjectStateEntry(entity)
            .EntityKey
            .EntityKeyValues
            .Select(k => k.Key);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  modelBuilder.Entity<User>().MapToStoredProcedures();
            //  modelBuilder.Entity<FinancialYear>().MapToStoredProcedures();
            //  modelBuilder.Entity<Group>().MapToStoredProcedures();
            //  modelBuilder.Entity<BankAccount>().MapToStoredProcedures();
            //  modelBuilder.Entity<Bank>().MapToStoredProcedures();
            //  modelBuilder.Entity<Company>().MapToStoredProcedures();
            //  modelBuilder.Entity<Person>().MapToStoredProcedures();
            ////  modelBuilder.Entity<AccountGroup>().MapToStoredProcedures();
            //  modelBuilder.Entity<GL>().MapToStoredProcedures();
            //  modelBuilder.Entity<TL>().MapToStoredProcedures();
            //  modelBuilder.Entity<SL>().MapToStoredProcedures();
            //  modelBuilder.Entity<DL>().MapToStoredProcedures();
            //  modelBuilder.Entity<SLStandardDescription>().MapToStoredProcedures();
            //  modelBuilder.Entity<Stock>().MapToStoredProcedures();
            //  modelBuilder.Entity<Product>().MapToStoredProcedures();
            //  modelBuilder.Entity<AccountDocument>().MapToStoredProcedures();
            //  modelBuilder.Entity<Currency>().MapToStoredProcedures();
            //  modelBuilder.Entity<DocumentNumbering>().MapToStoredProcedures();
            //  modelBuilder.Entity<ExchangeRate>().MapToStoredProcedures();
            //  modelBuilder.Entity<TypeDocument>().MapToStoredProcedures();
            //  modelBuilder.Entity<AccountDocument>().MapToStoredProcedures();
            //  modelBuilder.Entity<TreeAccount>().MapToStoredProcedures();
            //  modelBuilder.Entity<RelatedPerson>().MapToStoredProcedures();

            modelBuilder.Conventions.Add(new FunctionConvention<SainaDbContext>());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }

}