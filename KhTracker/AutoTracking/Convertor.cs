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
                return 0.45;
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

    public class WeaponConverter : IValueConverter
    {
        private string CusPath = "pack://application:,,,/CustomImages/Other/";
        private string EnabledPath1 = "Images/Other/"; //sword
        private string EnabledPath2 = "Images/Other/"; //shield
        private string EnabledPath3 = "Images/Other/"; //staff
        private bool CusMode = Properties.Settings.Default.CustomIcons;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //get the correct path
            {
                if (CusMode)
                {
                    if (MainWindow.CustomSwordFound)
                        EnabledPath1 = CusPath;
                    if (MainWindow.CustomShieldFound)
                        EnabledPath2 = CusPath;
                    if (MainWindow.CustomStaffFound)
                        EnabledPath3 = CusPath;
                }
            }

            if ((string)value == "Sword")
            {
                return EnabledPath1 + "sword.png";
            }
            else if ((string)value == "Shield")
            {
                return EnabledPath2 + "shield.png";
            }
            else if ((string)value == "Staff")
            {
                return EnabledPath3 + "staff.png";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //get the correct path
            {
                if (CusMode)
                {
                    if (MainWindow.CustomSwordFound)
                        EnabledPath1 = CusPath;
                    if (MainWindow.CustomShieldFound)
                        EnabledPath2 = CusPath;
                    if (MainWindow.CustomStaffFound)
                        EnabledPath3 = CusPath;
                }
            }

            if ((string)value == EnabledPath1 + "sword.png")
            {
                return "Sword";
            }
            else if ((string)value == EnabledPath2 + "shield.png")
            {
                return "Shield";
            }
            else if ((string)value == EnabledPath3 + "staff.png")
            {
                return "Staff";
            }
            else
            {
                return "";
            }
        }
    }

}