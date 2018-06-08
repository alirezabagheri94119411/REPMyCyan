using Saina.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Setting
{
    /// <summary>
    /// تنظیمات سیستم فروش
    /// </summary>
   public interface ISystemSettingSalesService
    {
        SystemSettingSaleModel GetSystemSettingSaleModel();
        bool SaveSystemSettingSaleModel(SystemSettingSaleModel systemSettingSaleModel);
    }
}
