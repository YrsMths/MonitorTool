using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace VS_Presentation.ValueConverter
{
    public class IndexOddEvenToBackgroundBrush: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            if (index % 2 == 0)
            {
                return new SolidColorBrush(Color.FromRgb(248, 248, 248));
            }
            else
            {
                return new SolidColorBrush(Color.FromRgb(242, 242, 242));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
