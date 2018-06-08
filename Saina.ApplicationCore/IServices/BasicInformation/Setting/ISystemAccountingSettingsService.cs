using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Setting
{
    /// <summary>
    /// تنظیمات سیستم حسابداری
    /// </summary>
   public interface ISystemAccountingSettingsService
    {

       SystemAccountingSettingModel GetSystemAccountingSettingModel();
        bool SaveSystemAccountingSettingModel(SystemAccountingSettingModel systemAccountingSettingModel);
        Task<bool> HasTL(int tLId);
    }
}
