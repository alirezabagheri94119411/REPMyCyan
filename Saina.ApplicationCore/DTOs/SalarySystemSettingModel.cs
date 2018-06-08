using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs
{
    /// <summary>
    /// تنظیمات حقوق و دستمزد
    /// </summary>
  public class SalarySystemSettingModel
    {

        /// <summary>
        /// سقف بیمه روزانه
        /// </summary>
        private string _dailyInsuranceCeiling;

        public string DailyInsuranceCeiling
        {
            get { return _dailyInsuranceCeiling; }
            set
            {
                _dailyInsuranceCeiling = value;
            }
        }

        /// <summary>
        /// نرخ بیمه سهم کارمند
        /// </summary>
        private string _employeeContributionInsuranceRate;

        public string EmployeeContributionInsuranceRate
        {
            get { return _employeeContributionInsuranceRate; }
            set
            {
                _employeeContributionInsuranceRate = value;
            }
        }

        /// <summary>
        /// نرخ بیمه سهم کارفرما
        /// </summary>
        private string _employerSharePremiumRate;

        public string EmployerSharePremiumRate
        {
            get { return _employerSharePremiumRate; }
            set
            {
                _employerSharePremiumRate = value;
            }
        }

        /// <summary>
        /// نرخ بیمه بیکاری
        /// </summary>
        private string _unemploymentInsuranceRates;

        public string UnemploymentInsuranceRates
        {
            get { return _unemploymentInsuranceRates; }
            set
            {
                _unemploymentInsuranceRates = value;
            }
        }

        /// <summary>
        /// نرخ بیمه سخت و زیان اور
        /// </summary>
        private string _hardshipInsuranceRates;

        public string HardshipInsuranceRates
        {
            get { return _hardshipInsuranceRates; }
            set
            {
                _hardshipInsuranceRates = value;
            }
        }

        /// <summary>
        /// حساب معین هرینه بیمه
        /// </summary>
        private string _sLInsuranceCost;

        public string SLInsuranceCost
        {
            get { return _sLInsuranceCost; }
            set
            {
                _sLInsuranceCost = value;
            }
        }
        private string _agentInsuranceCost;

        public string AgentInsuranceCost
        {
            get { return _agentInsuranceCost; }
            set
            {
                _agentInsuranceCost = value;
            }

        }
        /// <summary>
        /// حساب معین بیمه پرداختی
        /// </summary>
        private string _sLPaidInsurance;

        public string SLPaidInsurance
        {
            get { return _sLPaidInsurance; }
            set
            {
                _sLPaidInsurance = value;
            }
        }
        private string _agentPaidInsurance;

        public string AgentPaidInsurance
        {
            get { return _agentPaidInsurance; }
            set
            {
                _agentPaidInsurance = value;
            }
        }

        /// <summary>
        /// ضریب معافیت بیمه مالیات
        /// </summary>
        private string _taxExemptionCoefficient;

        public string TaxExemptionCoefficient
        {
            get { return _taxExemptionCoefficient; }
            set
            {
                _taxExemptionCoefficient = value;
            }
        }

        /// <summary>
        /// ضریب معافیت بیمه تکمیلی
        /// </summary>
        private string _supplementaryInsuranceDeductibleFactor;

        public string SupplementaryInsuranceDeductibleFactor
        {
            get { return _supplementaryInsuranceDeductibleFactor; }
            set
            {
                _supplementaryInsuranceDeductibleFactor = value;
            }
        }


        /// <summary>
        /// حساب معین مالیات پرداختی
        /// </summary>
        private string _sLTaxPaid;

        public string SLTaxPaid
        {
            get { return _sLTaxPaid; }
            set
            {
                _sLTaxPaid = value;
            }
        }

        private string _agentTaxPaid;

        public string AgentTaxPaid
        {
            get { return _agentTaxPaid; }
            set
            {
                _agentTaxPaid = value;
            }
        }



        /// <summary>
        /// سقف مرخصی ماهانه
        /// </summary>
        private string _monthlyLeaveLimit;

        public string MonthlyLeaveLimit
        {
            get { return _monthlyLeaveLimit; }
            set
            {
                _monthlyLeaveLimit = value;
            }
        }

        /// <summary>
        /// سقف مرخصی قابل انتقال
        /// </summary>
        private string _transferableLeaveCeiling;

        public string TransferableLeaveCeiling
        {
            get { return _transferableLeaveCeiling; }
            set
            {
                _transferableLeaveCeiling = value;
            }
        }

        /// <summary>
        /// حساب معین ذخیره مرخصی
        /// </summary>
        private string _sLStoreLeave;

        public string SLStoreLeave
        {
            get { return _sLStoreLeave; }
            set
            {
                _sLStoreLeave = value;
            }
        }
        private string _agentStoreLeave;

        public string AgentStoreLeave
        {
            get { return _agentStoreLeave; }
            set
            {
                _agentStoreLeave = value;
            }
        }
        /// <summary>
        /// نحساب معین ذخیره مرخصی
        /// </summary>
        private string _sLNoStoreLeave;

        public string SLNoStoreLeave
        {
            get { return _sLNoStoreLeave; }
            set
            {
                _sLNoStoreLeave = value;
            }
        }

        /// <summary>
        /// تعداد روزهای سنوات
        /// </summary>
        private string _numberDaysAge;

        public string NumberDaysAge
        {
            get { return _numberDaysAge; }
            set
            {
                _numberDaysAge = value;
            }
        }

        /// <summary>
        /// حساب معین هزینه سنوات
        /// </summary>
        private string _sLYearCost;

        public string SLYearCost
        {
            get { return _sLYearCost; }
            set
            {
                _sLYearCost = value;
            }
        }

        /// <summary>
        /// انتخاب عامل هزینه ی سنوات
        /// </summary>
        private string _agentYearCost;

        public string AgentYearCost
        {
            get { return _agentYearCost; }
            set
            {
                _agentYearCost = value;
            }
        }

        /// <summary>
        /// حساب معین حقوق 
        /// </summary>
        private string _sLSalary;

        public string SLSalary
        {
            get { return _sLSalary; }
            set
            {
                _sLSalary = value;
            }
        }


        /// <summary>
        /// انتخاب عامل حقوق
        /// </summary>
        private string _agentSalary;

        public string AgentSalary
        {
            get { return _agentSalary; }
            set
            {
                _agentSalary = value;
            }
        }

        /// <summary>
        /// حساب معین جاری کارکنان
        /// </summary>

        private string _sLCurrentEmployees;

        public string SLCurrentEmployees
        {
            get { return _sLCurrentEmployees; }
            set
            {
                _sLCurrentEmployees = value;
            }
        }

        /// <summary>
        /// انتخاب عامل جاری کارکنان
        /// </summary>
        private string _agentCurrentEmployees;

        public string AgentCurrentEmployees
        {
            get { return _agentCurrentEmployees; }
            set
            {
                _agentCurrentEmployees = value;
            }
        }

        /// <summary>
        /// حساب معین وام
        /// </summary>
        private string _sLLoan;

        public string SLLoan
        {
            get { return _sLLoan; }
            set
            {
                _sLLoan = value;
            }
        }

        /// <summary>
        /// انتخاب عامل وام
        /// </summary>
        private string _agentLoan;

        public string AgentLoan
        {
            get { return _agentLoan; }
            set
            {
                _agentLoan = value;
            }
        }

        /// <summary>
        /// حساب معین ذخیره سنوات
        /// </summary>
        private string _sLSaveYear;

        public string SLSaveYear
        {
            get { return _sLSaveYear; }
            set
            {
                _sLSaveYear = value;
            }
        }

        /// <summary>
        /// انتخاب عامل ذخیره سنوات
        /// </summary>
        private string _agentSaveYear;

        public string AgentSaveYear
        {
            get { return _agentSaveYear; }
            set
            {
                _agentSaveYear = value;
            }
        }

        /// <summary>
        /// سقف عیدی
        /// </summary>
        private string _eidCeiling;

        public string EidCeiling
        {
            get { return _eidCeiling; }
            set
            {
                _eidCeiling = value;
            }
        }

        /// <summary>
        /// حساب معین هزینه عیدی
        /// </summary>
        private string _sLEidCost;

        public string SLEidCost
        {
            get { return _sLEidCost; }
            set
            {
                _sLEidCost = value;
            }
        }

        /// <summary>
        /// انتخاب عامل هزینه عیدی
        /// </summary>
        private string _agenEidCost;

        public string AgenEidCost
        {
            get { return _agenEidCost; }
            set
            {
                _agenEidCost = value;
            }
        }

        /// <summary>
        /// ضریب عیدی
        /// </summary>
        private string _eidFactor;

        public string EidFactor
        {
            get { return _eidFactor; }
            set
            {
                _eidFactor = value;
            }
        }

        /// <summary>
        /// معافیت عیدی
        /// </summary>
        private string _eidbreak;

        public string Eidbreak
        {
            get { return _eidbreak; }
            set
            {
                _eidbreak = value;
            }
        }


    }
}
