using Saina.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    public interface ICompanyInformationsService
    {
        CompanyInformationModel GetCompanyInformationModel();
        bool SaveCompanyInformationModel(CompanyInformationModel companyInformationModel);
    }

}
