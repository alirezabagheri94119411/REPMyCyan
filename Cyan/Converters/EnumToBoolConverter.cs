using System;
using System.Globalization;
using System.Windows.Data;

namespace Saina.WPF.Converters
{
    public class EnumToBoolConverter:IValueConverter
    {
        private int val;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intParam = (int)parameter;
            val = (int)value;

            return ((intParam & val) != 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)//bool to enum
        {
            val ^= (int)parameter;
            return Enum.Parse(targetType, val.ToString());
        }
    }

    public class SingleEnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
