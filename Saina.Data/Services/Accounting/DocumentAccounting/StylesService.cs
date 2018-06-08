using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
   public class StylesService: IStylesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public StylesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<Style>> GetStylesAsync()
        {
            return await _uow.Set<Style>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<Style> GetStyleAsync(int id)
        {
            return await  _uow.Set<Style>().FirstOrDefaultAsync(c => (c.StyleId == id)).ConfigureAwait(false);
        }
        public async Task<Style> AddStyleAsync(Style style)
        {
            _uow.Styles.Add(style);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return style;
        }
        public async Task<Style> UpdateStyleAsync(Style style)
        {

            //var cmd = $"EXEC Style_Update @StyleId = {style.StyleId}," +
            //   $" @StyleTitle = N'{style.StyleTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(style).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return style;
        }
        public async Task DeleteStyleAsync(int styleId)
        {
            var style = _uow.Set<Style>().FirstOrDefault(c => c.StyleId == styleId);
            if (style != null)
            {
                _uow.Set<Style>().Remove(style);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
