using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace KhTracker
{
    public class HideZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return " ";
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == " ")
            {
                return 0;
            }
            else
            {
                return value;
            }
        }
    }

    public class ObtainedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return 1;
            }
            else
            {
                return 0.25;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class LevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 2)
            {
                return "Images/two.png";
            }
            else if ((int)value == 3)
            {
                return "Images/three.png";
            }
            else if ((int)value == 4)
            {
                return "Images/four.png";
            }
            else if ((int)value == 5)
            {
                return "Images/five.png";
            }
            else if ((int)value == 6)
            {
                return "Images/six.png";
            }
            else if ((int)value == 7)
            {
                return "Images/seven.png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}