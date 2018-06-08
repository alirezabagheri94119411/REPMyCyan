using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF
{
    public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        protected Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            else
                return null;
        }
        public Dictionary<string, List<string>> GetErrors()
        {
            return _errors;
        }
        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        protected override void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            //if (!_errors.Keys.Contains(propertyName))
            var oldValue = member;
            base.SetProperty<T>(ref member, val, propertyName);
            ValidateProperty(propertyName, val);
            //if (_errors.Keys.Contains(propertyName))
            //{
            //    base.SetProperty<T>(ref member, oldValue, propertyName);
            //    //ValidateProperty(propertyName, val);
            //    _errors.Remove(propertyName);
            //}
        }

        public async void ValidateProperty<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, results);

            List<string> bussinessErrors = null;
            if (ValidationDelegate != null)
                bussinessErrors = await ValidationDelegate(this, propertyName);
            if (bussinessErrors != null)
                if (_errors.ContainsKey(propertyName))
                    _errors[propertyName].AddRange(bussinessErrors);
                else
                    _errors.Add(propertyName, bussinessErrors);

            if (results.Any())
            {
                _errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else if (bussinessErrors == null || bussinessErrors != null && !bussinessErrors.Any())
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public event Func<object, string, Task<List<string>>> ValidationDelegate;
    }
}
