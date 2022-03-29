using System;
using System.Globalization;

namespace BaseWpfCore
{
    /// <summary>
    /// Multiplies the input numerical value with the specified parameter value
    /// </summary>
    public class MultiplyConverter : BaseValueConverter<MultiplyConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Helper method for parsing the value
            static double GetDouble(object input) => Double.TryParse(input?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double output) ? output : throw new Exception($"Multiply converter failed parsing {input}");

            return GetDouble(value) * GetDouble(parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}