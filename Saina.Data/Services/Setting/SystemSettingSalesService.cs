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
  public  class SystemSettingSalesService: ISystemSettingSalesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SystemSettingSalesService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _keyValues = _uow.Set<KeyValue>();
        }
        #endregion
        #region Fields
       // readonly IDbSet<SystemSettingSale> _companyInformations;
        readonly SainaDbContext _uow;
        private IDbSet<KeyValue> _keyValues;
        private IAppContextService _appContextService;
        #endregion
        #region Methode
        public SystemSettingSaleModel GetSystemSettingSaleModel()
        {
            var systemSettingSaleModel = new SystemSettingSaleModel();
            var pr = new PropertyReflector();
            var dict = systemSettingSaleModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .ToDictionary(prop => prop.Name, prop => prop?.GetValue(systemSettingSaleModel, null)?.ToString() ?? "");
            var keyValues = _keyValues.Where(x => dict.Keys.Contains(x.Key)).ToList();
            if (keyValues.Any())
            {
                //keyValues.ForEach(x => x.Value = dict[x.Key]);
                foreach (var keyValue in keyValues)
                {
                    pr.SetValue(systemSettingSaleModel, keyValue.Key, keyValue.Value);
                }
            }
            return systemSettingSaleModel;
        }
        public bool SaveSystemSettingSaleModel(SystemSettingSaleModel systemSettingSaleModel)
        {
            var dict = systemSettingSaleModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .ToDictionary(prop => prop.Name, prop => prop?.GetValue(systemSettingSaleModel, null)?.ToString() ?? "");
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
        #endregion
    }
}
