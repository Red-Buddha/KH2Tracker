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
                return "Images/Numbers/2.png";
            }
            else if ((int)value == 3)
            {
                return "Images/Numbers/3.png";
            }
            else if ((int)value == 4)
            {
                return "Images/Numbers/4.png";
            }
            else if ((int)value == 5)
            {
                return "Images/Numbers/5.png";
            }
            else if ((int)value == 6)
            {
                return "Images/Numbers/6.png";
            }
            else if ((int)value == 7)
            {
                return "Images/Numbers/7.png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Images/Numbers/2.png")
            {
                return 2;
            }
            else if ((string)value == "Images/Numbers/3.png")
            {
                return 3;
            }
            else if ((string)value == "Images/Numbers/4.png")
            {
                return 4;
            }
            else if ((string)value == "Images/Numbers/5.png")
            {
                return 5;
            }
            else if ((string)value == "Images/Numbers/6.png")
            {
                return 6;
            }
            else if ((string)value == "Images/Numbers/7.png")
            {
                return 7;
            }
            else
            {
                return 1;
            }
        }
    }

    public class WeaponConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Sword")
            {
                return "Images/Simple/sword.png";
            }
            else if ((string)value == "Shield")
            {
                return "Images/Simple/shield.png";
            }
            else if ((string)value == "Staff")
            {
                return "Images/Simple/staff.png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Images/Simple/sword.png")
            {
                return "Sword";
            }
            else if ((string)value == "Images/Simple/shield.png")
            {
                return "Shield";
            }
            else if ((string)value == "Images/Simple/staff.png")
            {
                return "Staff";
            }
            else
            {
                return "";
            }
        }
    }

    public class GrowthAbilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
            {
                return "";
            }
            else if ((int)value == 2)
            {
                return "Images/Numbers/2.png";
            }
            else if ((int)value == 3)
            {
                return "Images/Numbers/3.png";
            }
            else if ((int)value == 4)
            {
                return "Images/Numbers/4.png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "")
            {
                return 1;
            }
            else if ((string)value == "Images/Numbers/2.png")
            {
                return 2;
            }
            else if ((string)value == "Images/Numbers/3.png")
            {
                return 3;
            }
            else if ((string)value == "Images/Numbers/4.png")
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
    }

    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value >= 0)
            {
                if ((int)value > 99)
                    value = 99;

                return "Images/Numbers/" + (value).ToString() + ".png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value != null)
            {
                string val = (string)value;
                val = val.Substring(val.LastIndexOf('/'));
                return int.Parse(val.Substring(0, val.IndexOf('.')));
            }
            else
            {
                return 1;
            }
        }
    }
}