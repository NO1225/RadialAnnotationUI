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
    public class AMAndPMToButtonContentConverter : BaseValueConverter<AMAndPMToButtonContentConverter>
    {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            var timeofday = (AMPMEnum)value;

            switch (timeofday)
            {
                case AMPMEnum.AM:
                    return "Morning";
                case AMPMEnum.PM:
                    return "Afternoon";
                
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
