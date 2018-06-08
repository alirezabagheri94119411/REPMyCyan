using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    public class InputValidationRule : ValidationRule
    {
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            string input = value?.ToString();

            bool rt = input != null && !string.IsNullOrWhiteSpace(input); //Regex.IsMatch(input, @"(^(0?)(\.\d{2}))|(^([1-9]\d*)(\.\d{2})$)");
            if (!rt)
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
