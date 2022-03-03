using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace oe_guiws4_2.ColorConverters
{
    public class BackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string herotypes = value.ToString();
            if (herotypes == "GOOD")
            {
                return Brushes.LightGreen;
            }
            if (herotypes == "EVIL")
            {
                return Brushes.Red;
            }
            return Brushes.LightYellow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
