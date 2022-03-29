using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BaseWpfCore
{
    /// <summary>
    /// multiply the input numirical value with the choosen parameter
    /// </summary>
    public class BackGroundColorConverter : BaseValueConverter<BackGroundColorConverter>
    {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            var color = (BadgeColor)value;

            switch (color)
            {
                case BadgeColor.Black:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                case BadgeColor.White:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                case BadgeColor.Yellow:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffd000"));
                case BadgeColor.Orange:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff8200"));
                case BadgeColor.Blue:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#228cff"));
                case BadgeColor.Pink:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff7b98"));
                case BadgeColor.Red:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4c00"));         
                case BadgeColor.Green:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#042103"));
                case BadgeColor.LimeGreen:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32CD32"));
                case BadgeColor.Transperant:
                    return new SolidColorBrush(Colors.Transparent);
                default:
                    break;
            }
            return null;


        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
