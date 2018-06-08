using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Commerce
{
   public class ProductsService: IProductsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public ProductsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;

        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;

        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async  Task<List<Product>> GetProductsAsync()
        {
            return await  _uow.Set<Product>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<Product> GetProductAsync(int id)
        {
            return  await  _uow.Set<Product>().FirstOrDefaultAsync(c => (c.ProductId == id)).ConfigureAwait(false);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            _uow.Products.Add(product);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return product;
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {

            //      var cmd = $"EXEC Product_Update @ProductId = {product.ProductId}," +
            //$" @ProductTitle = N'{product.ProductTitle}'";
            //      await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(product).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return product;
        }
        public async Task DeleteProductAsync(int productId)
        {
            var product = _uow.Set<Product>().FirstOrDefault(c => c.ProductId == productId);
            if (product != null)
            {
                _uow.Set<Product>().Remove(product);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
