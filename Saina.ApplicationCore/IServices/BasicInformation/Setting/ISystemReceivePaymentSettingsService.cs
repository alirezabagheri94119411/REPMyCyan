using Saina.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Setting
{
   public interface ISystemReceivePaymentSettingsService
    {
        SystemReceivePaymentSettingModel GetSystemReceivePaymentSettingModel();
        bool SaveSystemReceivePaymentSettingModel(SystemReceivePaymentSettingModel systemReceivePaymentSettingModel);
    }
}
