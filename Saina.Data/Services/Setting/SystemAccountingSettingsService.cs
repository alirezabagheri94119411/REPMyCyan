using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Settings;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Common.ReflectionToolkit;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Setting
{
  public class SystemAccountingSettingsService : ISystemAccountingSettingsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SystemAccountingSettingsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _keyValues = _uow.Set<KeyValue>();
        }
        #endregion
        #region Fields
        readonly IDbSet<SystemAccountingSetting> _companyInformations;
        readonly SainaDbContext _uow;
        private IDbSet<KeyValue> _keyValues;
        private IAppContextService _appContextService;
        #endregion
        #region Methode
        public SystemAccountingSettingModel GetSystemAccountingSettingModel()
        {
            var systemAccountingSettingModel = new SystemAccountingSettingModel();
            var pr = new PropertyReflector();
            var dict = systemAccountingSettingModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .ToDictionary(prop => prop.Name, prop => prop?.GetValue(systemAccountingSettingModel, null)?.ToString() ?? "");
            var keyValues = _keyValues.Where(x => dict.Keys.Contains(x.Key)).ToList();
            if (keyValues.Any())
            {
                //keyValues.ForEach(x => x.Value = dict[x.Key]);
                foreach (var keyValue in keyValues)
                {
                    pr.SetValue(systemAccountingSettingModel, keyValue.Key, keyValue.Value);
                }
            }
            return systemAccountingSettingModel;
        }
        public bool SaveSystemAccountingSettingModel(SystemAccountingSettingModel systemAccountingSettingModel)
        {
            var dict = systemAccountingSettingModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .ToDictionary(prop => prop.Name, prop => prop?.GetValue(systemAccountingSettingModel, null)?.ToString() ?? "");
            var keyValues = _keyValues.Where(x => dict.Keys.Contains(x.Key)).ToList();
            if (keyValues.Any())
            {
                keyValues.ForEach(x => x.Value = dict[x.Key]);
            }
            else
            {
                foreach (var item in dict)
                {
                    _keyValues.Add(new KeyValue { Key = item.Key, Value = item.Value });
                }
                //_uow.SaveChanges();
            }
            return _uow.SaveChanges() != 0;
        }
        public async Task<bool> HasTL(int tLId)
        {
            return await _uow.TLs.AnyAsync(x => x.TLId != tLId);
        }
        #endregion
    }
}
