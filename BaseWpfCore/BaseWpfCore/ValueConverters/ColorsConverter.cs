using System;
using System.Collections.Generic;
using System.Drawing;
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
                    return new SolidColorBrush(ColorTranslator.FromHtml("#000000").ToMediaColor());
                case BadgeColor.White:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#ffffff").ToMediaColor());
                case BadgeColor.Yellow:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#ffd000").ToMediaColor());
                case BadgeColor.Orange:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#ff8200").ToMediaColor());
                case BadgeColor.Blue:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#228cff").ToMediaColor());
                case BadgeColor.Pink:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#ff7b98").ToMediaColor());
                case BadgeColor.Red:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#ff4c00").ToMediaColor());         
                case BadgeColor.Green:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#042103").ToMediaColor());
                case BadgeColor.LimeGreen:
                    return new SolidColorBrush(ColorTranslator.FromHtml("#32CD32").ToMediaColor());
                case BadgeColor.Transperant:
                    return new SolidColorBrush(System.Windows.Media.Colors.Transparent);
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
