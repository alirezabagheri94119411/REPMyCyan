using Saina.Common.Toolkit;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Saina.WPF.Converters
{
    public class EnumToDescription :  IValueConverter
    {
        public EnumToDescription()
        {

        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string description = EnumHelper.GetEnumDescription((Enum)value);
            return description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
