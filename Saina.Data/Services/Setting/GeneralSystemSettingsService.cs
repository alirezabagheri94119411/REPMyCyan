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
   public class GeneralSystemSettingsService: IGeneralSystemSettingsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public GeneralSystemSettingsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _keyValues = _uow.Set<KeyValue>();
        }
        #endregion
        #region Fields
       // readonly IDbSet<GeneralSystemSetting> _companyInformations;
        readonly SainaDbContext _uow;
        private IDbSet<KeyValue> _keyValues;
        private IAppContextService _appContextService;
        #endregion
        #region Methode
        public GeneralSystemSettingModel GetGeneralSystemSettingModel()
        {
            var generalSystemSettingModel = new GeneralSystemSettingModel();
            var pr = new PropertyReflector();
            var dict = generalSystemSettingModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .ToDictionary(prop => prop.Name, prop => prop?.GetValue(generalSystemSettingModel, null)?.ToString() ?? "");
            var keyValues = _keyValues.Where(x => dict.Keys.Contains(x.Key)).ToList();
            if (keyValues.Any())
            {
                //keyValues.ForEach(x => x.Value = dict[x.Key]);
                foreach (var keyValue in keyValues)
                {
                    pr.SetValue(generalSystemSettingModel, keyValue.Key, keyValue.Value);
                }
            }
            return generalSystemSettingModel;
        }
        public bool SaveGeneralSystemSettingModel(GeneralSystemSettingModel generalSystemSettingModel)
        {
            var dict = generalSystemSettingModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .ToDictionary(prop => prop.Name, prop => prop?.GetValue(generalSystemSettingModel, null)?.ToString() ?? "");
            var keyValues = _keyValues.Where(x => dict.Keys.Contains(x.Key)).ToList();
            if (keyValues.Count==dict.Count)
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
