using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Z.EntityFramework.Plus;
using System.Threading.Tasks;
using System.Threading;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using EntityFramework.Functions;
using System.Data.Entity.Core.Objects;

namespace Saina.Data.Context
{
    public class SainaDbContext : MyDbContextBase
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product22> Product22s { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemOption> OrderItemOptions { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<User> Users { set; get; }
        public DbSet<DynamicPage> DynamicPages { set; get; }
        public DbSet<Group> Groups { set; get; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<KeyValue> KeyValues { set; get; }
        //public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<GL> GLs { get; set; }
        public DbSet<TL> TLs { get; set; }
        public DbSet<SL> SLs { get; set; }
        public DbSet<DL> DLs { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Bank> Banks { get; set; }
   
        
        public DbSet<Person> People { get; set; }
     
       public DbSet<RelatedPerson> RelatedPeople { get; set; }
  
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AccountDocument> AccountDocuments { get; set; }
        public DbSet<DocumentNumbering> DocumentNumberings { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SelectAgent> SelectAgents { get; set; }
        public DbSet<UGDP> UGDPs { get; set; }
        public DbSet<SLStandardDescription> SLStandardDescriptions { get; set; }
        
        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<AccDocumentHeader> AccDocumentHeaders { get; set; }
        public DbSet<AccDocumentItem> AccDocumentItems { get; set; }
        public DbSet<DLType> DLTypes { get; set; }
        public DbSet<DLAccountsNature> DLAccountsNatures { get; set; }
        //public DbSet<TreeAccount> TreeAccounts { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<AccountsNature> AccountsNatures { get; set; }
    //    public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<BankType> BankTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<CountingWay> CountingWays { get; set; }
        public DbSet<TLDocumentHeader> TLDocumentHeaders { get; set; }
        public DbSet<TLDocumentItem> TLDocumentItems { get; set; }
        public DbSet<SimilarProduct> SimilarProducts { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
      //  public DbSet<InventoryControl> InventoryControls { get; set; }
        public DbSet<OtherProduct> OtherProducts { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductRack> ProductRacks { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
       // public DbSet<ProductRack> ProductRacks { get; set; }
        public DbSet<ProductRackCollection> ProductRackCollections { get; set; }
        public DbSet<StcDocumentHeader> StcDocumentHeaders { get; set; }
        public DbSet<StcTypeDocument> StcTypeDocuments { get; set; }
        
        [Function(FunctionType.ComposableScalarValuedFunction, nameof(ShamsiToMiladi), Schema = "dbo")]
        [return: Parameter(DbType = "nvarchar")]
        public string ShamsiToMiladi(
            [Parameter(DbType = "datetime")] DateTime intDate,
            [Parameter(DbType = "nvarchar")] string format) =>
                Function.CallNotSupported<string>();

    }
}
