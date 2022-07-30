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

    //public class LevelConverter : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/";
    //    private string CurrentPath = "Images/Numbers/Kh2/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value == 2)
    //        {
    //            return CurrentPath + "Yellow/2.png";
    //        }
    //        else if ((int)value == 3)
    //        {
    //            return CurrentPath + "Yellow/3.png";
    //        }
    //        else if ((int)value == 4)
    //        {
    //            return CurrentPath + "Yellow/4.png";
    //        }
    //        else if ((int)value == 5)
    //        {
    //            return CurrentPath + "Yellow/5.png";
    //        }
    //        else if ((int)value == 6)
    //        {
    //            return CurrentPath + "Yellow/6.png";
    //        }
    //        else if ((int)value == 7)
    //        {
    //            return CurrentPath + "Yellow/7.png";
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((string)value == CurrentPath + "Yellow/2.png")
    //        {
    //            return 2;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/3.png")
    //        {
    //            return 3;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/4.png")
    //        {
    //            return 4;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/5.png")
    //        {
    //            return 5;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/6.png")
    //        {
    //            return 6;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/7.png")
    //        {
    //            return 7;
    //        }
    //        else
    //        {
    //            return 1;
    //        }
    //    }
    //}

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

    //public class GrowthAbilityConverter : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/";
    //    private string CurrentPath = "Images/Numbers/Kh2/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value == 1)
    //        {
    //            return "";
    //        }
    //        else if ((int)value == 2)
    //        {
    //            return CurrentPath + "Yellow/2.png";
    //        }
    //        else if ((int)value == 3)
    //        {
    //            return CurrentPath + "Yellow/3.png";
    //        }
    //        else if ((int)value == 4)
    //        {
    //            return CurrentPath + "Yellow/4.png";
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((string)value == "")
    //        {
    //            return 1;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/2.png")
    //        {
    //            return 2;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/3.png")
    //        {
    //            return 3;
    //        }
    //        else if ((string)value == CurrentPath + "Yellow/4.png")
    //        {
    //            return 4;
    //        }
    //        else
    //        {
    //            return 0;
    //        }
    //    }
    //}

    //public class NumberConverter : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/Yellow/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/Yellow/";
    //    private string CurrentPath = "Images/Numbers/Kh2/Yellow/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value >= 0)
    //        {
    //            if ((int)value > 99)
    //                value = 99;
    //
    //            return CurrentPath + (value).ToString() + ".png";
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if ((string)value != null)
    //        {
    //            string val = (string)value;
    //            val = val.Substring(val.LastIndexOf('/'));
    //            return int.Parse(val.Substring(0, val.IndexOf('.')));
    //        }
    //        else
    //        {
    //            return 1;
    //        }
    //    }
    //}

    //public class NumberConverter100 : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/Yellow/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/Yellow/";
    //    private string CurrentPath = "Images/Numbers/Kh2/Yellow/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value >= 100)
    //        {
    //            int num = (int)value;
    //            //split number into separate digits
    //            List<int> listOfInts = new List<int>();
    //            while (num > 0)
    //            {
    //                listOfInts.Add(num % 10);
    //                num /= 10;
    //            }
    //
    //            value = listOfInts[2];
    //
    //            return CurrentPath + value.ToString() + ".png";
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if ((string)value != null)
    //        {
    //            string val = (string)value;
    //            val = val.Substring(val.LastIndexOf('/'));
    //            return int.Parse(val.Substring(0, val.IndexOf('.')));
    //        }
    //        else
    //        {
    //            return 1;
    //        }
    //    }
    //}
    //
    //public class NumberConverter010 : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/Yellow/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/Yellow/";
    //    private string CurrentPath = "Images/Numbers/Kh2/Yellow/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value >= 10)
    //        {
    //            int num = (int)value;
    //            //split number into separate digits
    //            List<int> listOfInts = new List<int>();
    //            while (num > 0)
    //            {
    //                listOfInts.Add(num % 10);
    //                num /= 10;
    //            }
    //
    //            value = listOfInts[1];
    //
    //            return CurrentPath + value.ToString() + ".png";
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if ((string)value != null)
    //        {
    //            string val = (string)value;
    //            val = val.Substring(val.LastIndexOf('/'));
    //            return int.Parse(val.Substring(0, val.IndexOf('.')));
    //        }
    //        else
    //        {
    //            return 1;
    //        }
    //    }
    //}
    //
    //public class NumberConverter001 : IValueConverter
    //{
    //    //Find current number image setting
    //    private string OldPath = "Images/Numbers/Old/Yellow/";
    //    private string CusPath = "pack://application:,,,/CustomImages/Numbers/Yellow/";
    //    private string CurrentPath = "Images/Numbers/Kh2/Yellow/"; //Default
    //    private bool OldNums = Properties.Settings.Default.OldNum;
    //    private bool CusNums = Properties.Settings.Default.CustomIcons;
    //
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        {
    //            if (OldNums)
    //                CurrentPath = OldPath;
    //            if (CusNums && MainWindow.CustomNumbersFound)
    //                CurrentPath = CusPath;
    //        }
    //
    //        if ((int)value >= 0)
    //        {
    //            int num = (int)value;
    //            //split number into separate digits
    //            List<int> listOfInts = new List<int>();
    //
    //            if (num > 0)
    //            {
    //                while (num > 0)
    //                {
    //                    listOfInts.Add(num % 10);
    //                    num /= 10;
    //                }
    //                value = listOfInts[0];
    //            }
    //            else
    //                value = 0;
    //
    //            return CurrentPath + value.ToString() + ".png";
    //        }
    //        else
    //        {
    //            return CurrentPath + "QuestionMark.png";
    //        }
    //    }
    //
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if ((string)value != null)
    //        {
    //            string val = (string)value;
    //            val = val.Substring(val.LastIndexOf('/'));
    //            return int.Parse(val.Substring(0, val.IndexOf('.')));
    //        }
    //        else
    //        {
    //            return 1;
    //        }
    //    }
    //}


}