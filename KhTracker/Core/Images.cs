using System;
using System.Collections.Generic;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
//using Microsoft.Win32;
//using System.Drawing;
//using System.Windows.Documents;
//using System.Runtime.InteropServices;
//using System.ComponentModel;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        //dumb stuff to help figure out what to do about custom images
        public static bool CustomNumbersFound = false;
        public static bool CustomBlueNumbersFound = false;
        public static bool CustomSwordFound = false;
        public static bool CustomStaffFound = false;
        public static bool CustomShieldFound = false;
        public static bool CustomLevelFound = false;
        public static bool CustomStrengthFound = false;
        public static bool CustomMagicFound = false;
        public static bool CustomDefenseFound = false;
        public static bool CustomProgFound = false;
        public static bool CustomBarYFound = false;
        public static bool CustomBarBFound = false;
        public static bool CustomVBarWFound = false;
        public static bool CustomVBarYFound = false;

        //handle adding all custom images and such
        public void InitImages()
        {
            #region KH2 Yellow Numbers
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_QuestionMark.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_0.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_1.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_2.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_3.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_4.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_5.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_6.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_7.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_8.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/_9.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/10.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/11.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/12.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/13.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/14.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/15.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/16.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/17.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/18.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/19.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/20.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/21.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/22.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/23.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/24.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/25.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/26.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/27.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/28.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/29.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/30.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/31.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/32.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/33.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/34.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/35.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/36.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/37.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/38.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/39.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/40.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/41.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/42.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/43.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/44.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/45.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/46.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/47.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/48.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/49.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/50.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/51.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/52.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/53.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/54.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/55.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/56.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/57.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/58.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/59.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/60.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/61.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/62.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/63.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/64.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/65.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/66.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/67.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/68.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/69.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/70.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/71.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/72.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/73.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/74.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/75.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/76.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/77.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/78.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/79.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/80.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/81.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/82.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/83.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/84.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/85.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/86.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/87.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/88.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/89.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/90.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/91.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/92.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/93.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/94.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/95.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/96.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/97.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/98.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/99.png", UriKind.Relative)));

            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/QuestionMark.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/0.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/1.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/2.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/3.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/4.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/5.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/6.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/7.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/8.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/9.png", UriKind.Relative)));
            #endregion

            #region KH2 Blue Numbers
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_QuestionMark.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_0.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_1.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_2.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_3.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_4.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_5.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_6.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_7.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_8.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/_9.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/10.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/11.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/12.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/13.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/14.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/15.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/16.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/17.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/18.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/19.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/20.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/21.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/22.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/23.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/24.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/25.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/26.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/27.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/28.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/29.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/30.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/31.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/32.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/33.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/34.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/35.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/36.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/37.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/38.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/39.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/40.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/41.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/42.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/43.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/44.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/45.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/46.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/47.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/48.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/49.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/50.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/51.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/52.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/53.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/54.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/55.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/56.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/57.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/58.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/59.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/60.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/61.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/62.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/63.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/64.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/65.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/66.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/67.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/68.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/69.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/70.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/71.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/72.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/73.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/74.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/75.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/76.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/77.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/78.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/79.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/80.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/81.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/82.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/83.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/84.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/85.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/86.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/87.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/88.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/89.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/90.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/91.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/92.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/93.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/94.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/95.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/96.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/97.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/98.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/99.png", UriKind.Relative)));

            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/QuestionMark.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/0.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/1.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/2.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/3.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/4.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/5.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/6.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/7.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/8.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/9.png", UriKind.Relative)));
            #endregion

            #region KH2 Green Numbers
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_QuestionMark.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_0.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_1.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_2.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_3.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_4.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_5.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_6.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_7.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_8.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/_9.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/10.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/11.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/12.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/13.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/14.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/15.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/16.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/17.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/18.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/19.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/20.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/21.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/22.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/23.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/24.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/25.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/26.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/27.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/28.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/29.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/30.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/31.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/32.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/33.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/34.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/35.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/36.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/37.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/38.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/39.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/40.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/41.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/42.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/43.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/44.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/45.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/46.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/47.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/48.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/49.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/50.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/51.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/52.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/53.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/54.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/55.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/56.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/57.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/58.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/59.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/60.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/61.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/62.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/63.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/64.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/65.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/66.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/67.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/68.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/69.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/70.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/71.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/72.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/73.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/74.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/75.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/76.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/77.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/78.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/79.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/80.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/81.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/82.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/83.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/84.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/85.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/86.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/87.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/88.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/89.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/90.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/91.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/92.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/93.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/94.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/95.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/96.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/97.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/98.png", UriKind.Relative)));
            data.GreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/99.png", UriKind.Relative)));

            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/QuestionMark.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/0.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/1.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/2.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/3.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/4.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/5.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/6.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/7.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/8.png", UriKind.Relative)));
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/9.png", UriKind.Relative)));
            #endregion

            #region Old Yellow
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_QuestionMark.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_0.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_1.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_2.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_3.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_4.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_5.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_6.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_7.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_8.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/_9.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/10.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/11.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/12.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/13.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/14.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/15.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/16.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/17.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/18.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/19.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/20.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/21.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/22.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/23.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/24.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/25.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/26.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/27.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/28.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/29.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/30.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/31.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/32.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/33.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/34.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/35.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/36.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/37.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/38.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/39.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/40.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/41.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/42.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/43.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/44.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/45.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/46.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/47.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/48.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/49.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/50.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/51.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/52.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/53.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/54.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/55.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/56.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/57.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/58.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/59.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/60.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/61.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/62.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/63.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/64.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/65.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/66.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/67.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/68.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/69.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/70.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/71.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/72.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/73.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/74.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/75.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/76.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/77.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/78.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/79.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/80.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/81.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/82.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/83.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/84.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/85.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/86.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/87.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/88.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/89.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/90.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/91.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/92.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/93.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/94.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/95.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/96.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/97.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/98.png", UriKind.Relative)));
            data.OldNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/99.png", UriKind.Relative)));

            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/QuestionMark.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/0.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/1.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/2.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/3.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/4.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/5.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/6.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/7.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/8.png", UriKind.Relative)));
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/9.png", UriKind.Relative)));
            #endregion

            #region Old Blue
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_QuestionMark.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_0.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_1.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_2.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_3.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_4.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_5.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_6.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_7.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_8.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/_9.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/10.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/11.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/12.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/13.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/14.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/15.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/16.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/17.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/18.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/19.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/20.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/21.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/22.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/23.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/24.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/25.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/26.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/27.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/28.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/29.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/30.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/31.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/32.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/33.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/34.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/35.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/36.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/37.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/38.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/39.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/40.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/41.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/42.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/43.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/44.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/45.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/46.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/47.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/48.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/49.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/50.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/51.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/52.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/53.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/54.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/55.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/56.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/57.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/58.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/59.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/60.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/61.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/62.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/63.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/64.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/65.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/66.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/67.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/68.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/69.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/70.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/71.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/72.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/73.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/74.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/75.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/76.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/77.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/78.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/79.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/80.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/81.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/82.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/83.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/84.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/85.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/86.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/87.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/88.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/89.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/90.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/91.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/92.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/93.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/94.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/95.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/96.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/97.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/98.png", UriKind.Relative)));
            data.OldBlueNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/99.png", UriKind.Relative)));

            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/QuestionMark.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/0.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/1.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/2.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/3.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/4.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/5.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/6.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/7.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/8.png", UriKind.Relative)));
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/9.png", UriKind.Relative)));
            #endregion

            #region Old Green
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_QuestionMark.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_0.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_1.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_2.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_3.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_4.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_5.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_6.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_7.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_8.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/_9.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/10.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/11.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/12.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/13.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/14.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/15.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/16.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/17.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/18.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/19.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/20.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/21.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/22.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/23.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/24.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/25.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/26.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/27.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/28.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/29.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/30.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/31.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/32.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/33.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/34.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/35.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/36.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/37.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/38.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/39.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/40.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/41.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/42.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/43.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/44.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/45.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/46.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/47.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/48.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/49.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/50.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/51.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/52.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/53.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/54.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/55.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/56.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/57.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/58.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/59.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/60.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/61.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/62.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/63.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/64.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/65.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/66.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/67.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/68.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/69.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/70.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/71.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/72.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/73.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/74.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/75.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/76.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/77.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/78.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/79.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/80.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/81.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/82.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/83.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/84.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/85.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/86.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/87.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/88.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/89.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/90.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/91.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/92.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/93.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/94.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/95.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/96.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/97.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/98.png", UriKind.Relative)));
            data.OldGreenNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/99.png", UriKind.Relative)));

            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/QuestionMark.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/0.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/1.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/2.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/3.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/4.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/5.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/6.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/7.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/8.png", UriKind.Relative)));
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/9.png", UriKind.Relative)));
            #endregion

            #region Custom
            //Custom numbers
            //Default are the KH2 numbers
            string OldPath = "Images/Numbers/Old/";
            string GoodPath = "Images/Numbers/Kh2/";
            string GoodPathBlue = "Images/Numbers/Kh2/";
            //Separete variable for if the user wanted to customize one set of numbers
            var urikindvar = UriKind.Relative;
            var urikindvarblue = UriKind.Relative;
            //Fix paths if Old Numbers toggle is on
            if (OldNumOption.IsChecked == true)
            {
                GoodPath = OldPath;
                GoodPathBlue = OldPath;
            }
            //very lazy method here. i don't feel like setting it up to check if each and every number image exists cause there are literally over 200
            //so i just check 4 images in each folder to decide if custom numbers should be used.
            if (File.Exists("CustomImages/Numbers/Yellow/_5.png") && File.Exists("CustomImages/Numbers/Yellow/48.png") && File.Exists("CustomImages/Numbers/Yellow/_QuestionMark.png") && File.Exists("CustomImages/Numbers/Yellow/QuestionMark.png"))
            {
                GoodPath = "pack://application:,,,/CustomImages/Numbers/";
                urikindvar = UriKind.Absolute;
                CustomNumbersFound = true;
            }
            if (File.Exists("CustomImages/Numbers/Blue/_5.png") && File.Exists("CustomImages/Numbers/Blue/48.png") && File.Exists("CustomImages/Numbers/Blue/_QuestionMark.png") && File.Exists("CustomImages/Numbers/Blue/QuestionMark.png"))
            {
                GoodPathBlue = "pack://application:,,,/CustomImages/Numbers/";
                urikindvarblue = UriKind.Absolute;
                CustomBlueNumbersFound = true;
            }

            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_QuestionMark.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_0.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_1.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_2.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_3.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_4.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_5.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_6.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_7.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_8.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/_9.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/10.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/11.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/12.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/13.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/14.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/15.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/16.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/17.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/18.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/19.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/20.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/21.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/22.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/23.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/24.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/25.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/26.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/27.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/28.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/29.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/30.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/31.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/32.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/33.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/34.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/35.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/36.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/37.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/38.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/39.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/40.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/41.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/42.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/43.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/44.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/45.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/46.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/47.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/48.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/49.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/50.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/51.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/52.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/53.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/54.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/55.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/56.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/57.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/58.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/59.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/60.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/61.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/62.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/63.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/64.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/65.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/66.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/67.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/68.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/69.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/70.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/71.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/72.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/73.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/74.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/75.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/76.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/77.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/78.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/79.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/80.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/81.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/82.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/83.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/84.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/85.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/86.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/87.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/88.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/89.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/90.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/91.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/92.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/93.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/94.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/95.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/96.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/97.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/98.png", urikindvar)));
            data.CustomNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/99.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/QuestionMark.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/0.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/1.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/2.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/3.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/4.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/5.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/6.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/7.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/8.png", urikindvar)));
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/9.png", urikindvar)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_QuestionMark.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_0.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_1.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_2.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_3.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_4.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_5.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_6.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_7.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_8.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/_9.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/10.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/11.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/12.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/13.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/14.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/15.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/16.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/17.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/18.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/19.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/20.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/21.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/22.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/23.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/24.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/25.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/26.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/27.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/28.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/29.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/30.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/31.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/32.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/33.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/34.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/35.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/36.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/37.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/38.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/39.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/40.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/41.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/42.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/43.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/44.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/45.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/46.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/47.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/48.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/49.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/50.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/51.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/52.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/53.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/54.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/55.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/56.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/57.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/58.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/59.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/60.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/61.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/62.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/63.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/64.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/65.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/66.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/67.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/68.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/69.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/70.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/71.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/72.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/73.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/74.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/75.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/76.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/77.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/78.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/79.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/80.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/81.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/82.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/83.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/84.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/85.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/86.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/87.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/88.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/89.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/90.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/91.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/92.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/93.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/94.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/95.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/96.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/97.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/98.png", urikindvarblue)));
            data.CustomBlueNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/99.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/QuestionMark.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/0.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/1.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/2.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/3.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/4.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/5.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/6.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/7.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/8.png", urikindvarblue)));
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/9.png", urikindvarblue)));
            #endregion


            //temp. fix later after number refatcoring
            data.CustomGreenNumbers = data.GreenNumbers;
            data.CustomGreenSingleNumbers = data.GreenSingleNumbers;

            //i really hate how i did some of this

            //for autodetect
            data.AD_Connect = new BitmapImage(new Uri("Images/connect.png", UriKind.Relative));
            data.AD_PC = new BitmapImage(new Uri("Images/PC.png", UriKind.Relative));
            data.AD_PCred = new BitmapImage(new Uri("Images/PCred.png", UriKind.Relative));
            data.AD_PS2 = new BitmapImage(new Uri("Images/ps2.png", UriKind.Relative));

            //check for custom stat and weapon icons (OLD)
            {
                if (File.Exists("CustomImages/Other/sword.png"))
                    CustomSwordFound = true;
                if (File.Exists("CustomImages/Other/staff.png"))
                    CustomStaffFound = true;
                if (File.Exists("CustomImages/Other/shield.png"))
                    CustomShieldFound = true;

                if (File.Exists("CustomImages/Other/level.png"))
                    CustomLevelFound = true;
                if (File.Exists("CustomImages/Other/strength.png"))
                    CustomStrengthFound = true;
                if (File.Exists("CustomImages/Other/magic.png"))
                    CustomMagicFound = true;
                if (File.Exists("CustomImages/Other/defence.png"))
                    CustomDefenseFound = true;
            }

            //set stuff for the vertical images
            {
                data.VerticalBarW = new BitmapImage(new Uri("Images/VerticalBarWhite.png", UriKind.Relative));
                data.VerticalBarY = new BitmapImage(new Uri("Images/VerticalBar.png", UriKind.Relative));

                if (File.Exists("CustomImages/VerticalBarWhite.png"))
                {
                    data.CustomVerticalBarW = new BitmapImage(new Uri("pack://application:,,,/CustomImages/VerticalBarWhite.png", UriKind.Absolute));
                    CustomVBarWFound = true;
                }
                else
                    data.CustomVerticalBarW = data.VerticalBarW;

                if (File.Exists("CustomImages/VerticalBar.png"))
                {
                    data.CustomVerticalBarY = new BitmapImage(new Uri("pack://application:,,,/CustomImages/VerticalBar.png", UriKind.Absolute));
                    CustomVBarYFound = true;
                }
                else
                    data.CustomVerticalBarY = data.VerticalBarY;
            }

            //set stuff for the slash bar images
            {
                data.SlashBarB = new BitmapImage(new Uri("Images/Numbers/BarBlue.png", UriKind.Relative));
                data.SlashBarY = new BitmapImage(new Uri("Images/Numbers/Bar.png", UriKind.Relative));

                if (File.Exists("CustomImages/Numbers/BarBlue.png"))
                {
                    data.CustomSlashBarB = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Numbers/BarBlue.png", UriKind.Absolute));
                    CustomBarBFound = true;
                }
                else
                    data.CustomSlashBarB = data.SlashBarB;

                if (File.Exists("CustomImages/Numbers/Bar.png"))
                {
                    data.CustomSlashBarY = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Numbers/Bar.png", UriKind.Absolute));
                    CustomBarYFound = true;
                }
                else
                    data.CustomSlashBarY = data.SlashBarY;
            }

            //check for custom progression icons
            {
                if (File.Exists("CustomImages/Progression/1k.png") && File.Exists("CustomImages/Progression/carpet.png") && File.Exists("CustomImages/Progression/screens.png") && File.Exists("CustomImages/Progression/kanga.png"))
                    CustomProgFound = true;
            }
        }

        //dumb window backgound stuff
        private void MainBG_DefToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MainDefOption.IsChecked == false)
            {
                MainDefOption.IsChecked = true;
                return;
            }

            MainImg1Option.IsChecked = false;
            MainImg2Option.IsChecked = false;
            MainImg3Option.IsChecked = false;

            Properties.Settings.Default.MainBG = 0;

            if (MainDefOption.IsChecked)
            {
                Background = Application.Current.Resources["BG-Default"] as SolidColorBrush;
            }
        }

        private void MainBG_Img1Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MainImg1Option.IsChecked == false)
            {
                MainImg1Option.IsChecked = true;
                return;
            }

            MainDefOption.IsChecked = false;
            MainImg2Option.IsChecked = false;
            MainImg3Option.IsChecked = false;

            Properties.Settings.Default.MainBG = 1;

            if (MainImg1Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    Background = Application.Current.Resources["BG-Image1"] as ImageBrush;
                else
                    Background = Application.Current.Resources["BG-ImageDef1"] as ImageBrush;
            }
        }

        private void MainBG_Img2Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MainImg2Option.IsChecked == false)
            {
                MainImg2Option.IsChecked = true;
                return;
            }

            MainDefOption.IsChecked = false;
            MainImg1Option.IsChecked = false;
            MainImg3Option.IsChecked = false;

            Properties.Settings.Default.MainBG = 2;

            if (MainImg2Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    Background = Application.Current.Resources["BG-Image2"] as ImageBrush;
                else
                    Background = Application.Current.Resources["BG-ImageDef2"] as ImageBrush;
            }
        }

        private void MainBG_Img3Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MainImg3Option.IsChecked == false)
            {
                MainImg3Option.IsChecked = true;
                return;
            }

            MainDefOption.IsChecked = false;
            MainImg1Option.IsChecked = false;
            MainImg2Option.IsChecked = false;

            Properties.Settings.Default.MainBG = 3;

            if (MainImg3Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    Background = Application.Current.Resources["BG-Image3"] as ImageBrush;
                else
                    Background = Application.Current.Resources["BG-ImageDef3"] as ImageBrush;
            }
        }

        private void BroadcastBG_DefToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (BroadcastDefOption.IsChecked == false)
            {
                BroadcastDefOption.IsChecked = true;
                return;
            }

            BroadcastImg1Option.IsChecked = false;
            BroadcastImg2Option.IsChecked = false;
            BroadcastImg3Option.IsChecked = false;

            Properties.Settings.Default.BroadcastBG = 0;

            if (BroadcastDefOption.IsChecked)
            {
                broadcast.Background = Application.Current.Resources["BG-Default"] as SolidColorBrush;
            }
        }

        private void BroadcastBG_Img1Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (BroadcastImg1Option.IsChecked == false)
            {
                BroadcastImg1Option.IsChecked = true;
                return;
            }

            BroadcastDefOption.IsChecked = false;
            BroadcastImg2Option.IsChecked = false;
            BroadcastImg3Option.IsChecked = false;

            Properties.Settings.Default.BroadcastBG = 1;

            if (BroadcastImg1Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    broadcast.Background = Application.Current.Resources["BG-BImage1"] as ImageBrush;
                else
                    broadcast.Background = Application.Current.Resources["BG-BImageDef1"] as ImageBrush;
            }
        }

        private void BroadcastBG_Img2Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (BroadcastImg2Option.IsChecked == false)
            {
                BroadcastImg2Option.IsChecked = true;
                return;
            }

            BroadcastDefOption.IsChecked = false;
            BroadcastImg1Option.IsChecked = false;
            BroadcastImg3Option.IsChecked = false;

            Properties.Settings.Default.BroadcastBG = 2;

            if (BroadcastImg2Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    broadcast.Background = Application.Current.Resources["BG-BImage2"] as ImageBrush;
                else
                    broadcast.Background = Application.Current.Resources["BG-BImageDef2"] as ImageBrush;
            }
        }

        private void BroadcastBG_Img3Toggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (BroadcastImg3Option.IsChecked == false)
            {
                BroadcastImg3Option.IsChecked = true;
                return;
            }

            BroadcastDefOption.IsChecked = false;
            BroadcastImg1Option.IsChecked = false;
            BroadcastImg2Option.IsChecked = false;

            Properties.Settings.Default.BroadcastBG = 3;

            if (BroadcastImg3Option.IsChecked)
            {
                if (File.Exists("CustomImages/BG.png"))
                    broadcast.Background = Application.Current.Resources["BG-BImage3"] as ImageBrush;
                else
                    broadcast.Background = Application.Current.Resources["BG-BImageDef3"] as ImageBrush;
            }
        }

        //stuff to call the right number image
        public List<BitmapImage> GetDataNumber(string type)
        {
            bool OldMode = Properties.Settings.Default.OldNum;
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            var NormalNum = data.Numbers;
            var BlueNum = data.BlueNumbers;
            var GreenNum = data.GreenNumbers;
            var SingleNum = data.SingleNumbers;
            var SingleBlueNum = data.BlueSingleNumbers;
            var SingleGreenNum = data.GreenSingleNumbers;

            //Get correct numbers
            {
                if (OldMode)
                {
                    NormalNum = data.OldNumbers;
                    BlueNum = data.OldBlueNumbers;
                    GreenNum = data.OldGreenNumbers;
                    SingleNum = data.OldSingleNumbers;
                    SingleBlueNum = data.OldBlueSingleNumbers;
                    SingleGreenNum = data.OldGreenSingleNumbers;
                }

                if (CustomMode)
                {
                    if (CustomNumbersFound)
                    {
                        NormalNum = data.CustomNumbers;
                        SingleNum = data.CustomSingleNumbers;
                    }
                    if (CustomBlueNumbersFound)
                    {
                        BlueNum = data.CustomBlueNumbers;
                        SingleBlueNum = data.CustomBlueSingleNumbers;
                    }
                    //if (CustomGreenNumbersFound)
                    //{
                    //    GreenNum = data.CustomGreenNumbers;
                    //    SingleGreenNum = data.CustomGreenSingleNumbers;
                    //}
                }
            }

            //return correct number list
            switch(type)
            {
                case "Y":
                    return NormalNum;
                case "B":
                    return BlueNum;
                case "S":
                    return SingleNum;
                case "SB":
                    return SingleBlueNum;
                case "G":
                    return GreenNum;
                case "SG":
                    return SingleGreenNum;
                default:
                    return NormalNum;
            }
        }

        //get cutom images for toggles. i'll recode this to be better later i swear
        private void CustomChecksCheck()
        {
            if (CustomFolderOption.IsChecked)
            {
                //check if folders exists then start checking if each file exists in it
                if (Directory.Exists("CustomImages/Checks/"))
                {
                    //Main Window
                    if (File.Exists("CustomImages/Checks/ansem_report01.png"))
                        Report1.SetResourceReference(ContentProperty, "Cus-AnsemReport01");
                    if (File.Exists("CustomImages/Checks/ansem_report02.png"))
                        Report2.SetResourceReference(ContentProperty, "Cus-AnsemReport02");
                    if (File.Exists("CustomImages/Checks/ansem_report03.png"))
                        Report3.SetResourceReference(ContentProperty, "Cus-AnsemReport03");
                    if (File.Exists("CustomImages/Checks/ansem_report04.png"))
                        Report4.SetResourceReference(ContentProperty, "Cus-AnsemReport04");
                    if (File.Exists("CustomImages/Checks/ansem_report05.png"))
                        Report5.SetResourceReference(ContentProperty, "Cus-AnsemReport05");
                    if (File.Exists("CustomImages/Checks/ansem_report06.png"))
                        Report6.SetResourceReference(ContentProperty, "Cus-AnsemReport06");
                    if (File.Exists("CustomImages/Checks/ansem_report07.png"))
                        Report7.SetResourceReference(ContentProperty, "Cus-AnsemReport07");
                    if (File.Exists("CustomImages/Checks/ansem_report08.png"))
                        Report8.SetResourceReference(ContentProperty, "Cus-AnsemReport08");
                    if (File.Exists("CustomImages/Checks/ansem_report09.png"))
                        Report9.SetResourceReference(ContentProperty, "Cus-AnsemReport09");
                    if (File.Exists("CustomImages/Checks/ansem_report10.png"))
                        Report10.SetResourceReference(ContentProperty, "Cus-AnsemReport10");
                    if (File.Exists("CustomImages/Checks/ansem_report11.png"))
                        Report11.SetResourceReference(ContentProperty, "Cus-AnsemReport11");
                    if (File.Exists("CustomImages/Checks/ansem_report12.png"))
                        Report12.SetResourceReference(ContentProperty, "Cus-AnsemReport12");
                    if (File.Exists("CustomImages/Checks/ansem_report13.png"))
                        Report13.SetResourceReference(ContentProperty, "Cus-AnsemReport13");

                    if (File.Exists("CustomImages/Checks/jump.png"))
                    {
                        HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                        broadcast.HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                    }
                    if (File.Exists("CustomImages/Checks/quick.png"))
                    {
                        QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                        broadcast.QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                    }
                    if (File.Exists("CustomImages/Checks/dodge.png"))
                    {
                        DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                        broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                    }
                    if (File.Exists("CustomImages/Checks/aerial.png"))
                    {
                        AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                        broadcast.AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                    }
                    if (File.Exists("CustomImages/Checks/glide.png"))
                    {
                        Glide.SetResourceReference(ContentProperty, "Cus-Glide");
                        broadcast.Glide.SetResourceReference(ContentProperty, "Cus-Glide");
                    }

                    if (File.Exists("CustomImages/Checks/fire.png"))
                    {
                        Fire1.SetResourceReference(ContentProperty, "Cus-Fire");
                        Fire2.SetResourceReference(ContentProperty, "Cus-Fire");
                        Fire3.SetResourceReference(ContentProperty, "Cus-Fire");
                        broadcast.Fire.SetResourceReference(ContentProperty, "Cus-Fire");
                    }
                    if (File.Exists("CustomImages/Checks/blizzard.png"))
                    {
                        Blizzard1.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        Blizzard2.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        Blizzard3.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        broadcast.Blizzard.SetResourceReference(ContentProperty, "Cus-Blizzard");
                    }
                    if (File.Exists("CustomImages/Checks/thunder.png"))
                    {
                        Thunder1.SetResourceReference(ContentProperty, "Cus-Thunder");
                        Thunder2.SetResourceReference(ContentProperty, "Cus-Thunder");
                        Thunder3.SetResourceReference(ContentProperty, "Cus-Thunder");
                        broadcast.Thunder.SetResourceReference(ContentProperty, "Cus-Thunder");
                    }
                    if (File.Exists("CustomImages/Checks/cure.png"))
                    {
                        Cure1.SetResourceReference(ContentProperty, "Cus-Cure");
                        Cure2.SetResourceReference(ContentProperty, "Cus-Cure");
                        Cure3.SetResourceReference(ContentProperty, "Cus-Cure");
                        broadcast.Cure.SetResourceReference(ContentProperty, "Cus-Cure");
                    }
                    if (File.Exists("CustomImages/Checks/reflect.png"))
                    {
                        Reflect1.SetResourceReference(ContentProperty, "Cus-Reflect");
                        Reflect2.SetResourceReference(ContentProperty, "Cus-Reflect");
                        Reflect3.SetResourceReference(ContentProperty, "Cus-Reflect");
                        broadcast.Reflect.SetResourceReference(ContentProperty, "Cus-Reflect");
                    }
                    if (File.Exists("CustomImages/Checks/magnet.png"))
                    {
                        Magnet1.SetResourceReference(ContentProperty, "Cus-Magnet");
                        Magnet2.SetResourceReference(ContentProperty, "Cus-Magnet");
                        Magnet3.SetResourceReference(ContentProperty, "Cus-Magnet");
                        broadcast.Magnet.SetResourceReference(ContentProperty, "Cus-Magnet");
                    }

                    if (File.Exists("CustomImages/Checks/torn_pages.png"))
                    {
                        TornPage1.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage2.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage3.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage4.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage5.SetResourceReference(ContentProperty, "Cus-TornPage");
                    }

                    if (File.Exists("CustomImages/Checks/valor.png"))
                    {
                        Valor.SetResourceReference(ContentProperty, "Cus-Valor");
                        ValorM.SetResourceReference(ContentProperty, "Cus-Valor");
                        broadcast.Valor.SetResourceReference(ContentProperty, "Cus-Valor");
                    }
                    if (File.Exists("CustomImages/Checks/wisdom.png"))
                    {
                        Wisdom.SetResourceReference(ContentProperty, "Cus-Wisdom");
                        WisdomM.SetResourceReference(ContentProperty, "Cus-Wisdom");
                        broadcast.Wisdom.SetResourceReference(ContentProperty, "Cus-Wisdom");
                    }
                    if (File.Exists("CustomImages/Checks/limit.png"))
                    {
                        Limit.SetResourceReference(ContentProperty, "Cus-Limit");
                        LimitM.SetResourceReference(ContentProperty, "Cus-Limit");
                        broadcast.Limit.SetResourceReference(ContentProperty, "Cus-Limit");
                    }
                    if (File.Exists("CustomImages/Checks/master.png"))
                    {
                        Master.SetResourceReference(ContentProperty, "Cus-Master");
                        MasterM.SetResourceReference(ContentProperty, "Cus-Master");
                        broadcast.Master.SetResourceReference(ContentProperty, "Cus-Master");
                    }
                    if (File.Exists("CustomImages/Checks/final.png"))
                    {
                        Final.SetResourceReference(ContentProperty, "Cus-Final");
                        FinalM.SetResourceReference(ContentProperty, "Cus-Final");
                        broadcast.Final.SetResourceReference(ContentProperty, "Cus-Final");
                    }

                    if (File.Exists("CustomImages/Checks/genie.png"))
                    {
                        Lamp.SetResourceReference(ContentProperty, "Cus-Genie");
                        broadcast.Lamp.SetResourceReference(ContentProperty, "Cus-Genie");
                    }
                    if (File.Exists("CustomImages/Checks/stitch.png"))
                    {
                        Ukulele.SetResourceReference(ContentProperty, "Cus-Stitch");
                        broadcast.Ukulele.SetResourceReference(ContentProperty, "Cus-Stitch");
                    }
                    if (File.Exists("CustomImages/Checks/chicken_little.png"))
                    {
                        Baseball.SetResourceReference(ContentProperty, "Cus-ChickenLittle");
                        broadcast.Baseball.SetResourceReference(ContentProperty, "Cus-ChickenLittle");
                    }
                    if (File.Exists("CustomImages/Checks/peter_pan.png"))
                    {
                        Feather.SetResourceReference(ContentProperty, "Cus-PeterPan");
                        broadcast.Feather.SetResourceReference(ContentProperty, "Cus-PeterPan");
                    }

                    if (File.Exists("CustomImages/Checks/proof_of_nonexistence.png"))
                    {
                        Nonexistence.SetResourceReference(ContentProperty, "Cus-ProofOfNon");
                        broadcast.Nonexistence.SetResourceReference(ContentProperty, "Cus-ProofOfNon");
                    }
                    if (File.Exists("CustomImages/Checks/proof_of_connection.png"))
                    {
                        Connection.SetResourceReference(ContentProperty, "Cus-ProofOfCon");
                        broadcast.Connection.SetResourceReference(ContentProperty, "Cus-ProofOfCon");
                    }
                    if (File.Exists("CustomImages/Checks/proof_of_tranquility.png"))
                    {
                        Peace.SetResourceReference(ContentProperty, "Cus-ProofOfPea");
                        broadcast.Peace.SetResourceReference(ContentProperty, "Cus-ProofOfPea");
                    }
                    if (File.Exists("CustomImages/Checks/promise_charm.png"))
                    {
                        PromiseCharm.SetResourceReference(ContentProperty, "Cus-PromiseCharm");
                        broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Cus-PromiseCharm");
                    }
                    if (File.Exists("CustomImages/Checks/once_more.png"))
                    {
                        OnceMore.SetResourceReference(ContentProperty, "Cus-OnceMore");
                        broadcast.OnceMore.SetResourceReference(ContentProperty, "Cus-OnceMore");
                    }
                    if (File.Exists("CustomImages/Checks/second_chance.png"))
                    {
                        SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");
                        broadcast.SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");
                    }
                }

                //for ghost items
                if (Directory.Exists("CustomImages/Checks/Ghost/"))
                {
                    //Main Window
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report01.png"))
                        Ghost_Report1.SetResourceReference(ContentProperty, "Cus-G_AnsemReport01");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report02.png"))
                        Ghost_Report2.SetResourceReference(ContentProperty, "Cus-G_AnsemReport02");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report03.png"))
                        Ghost_Report3.SetResourceReference(ContentProperty, "Cus-G_AnsemReport03");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report04.png"))
                        Ghost_Report4.SetResourceReference(ContentProperty, "Cus-G_AnsemReport04");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report05.png"))
                        Ghost_Report5.SetResourceReference(ContentProperty, "Cus-G_AnsemReport05");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report06.png"))
                        Ghost_Report6.SetResourceReference(ContentProperty, "Cus-G_AnsemReport06");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report07.png"))
                        Ghost_Report7.SetResourceReference(ContentProperty, "Cus-G_AnsemReport07");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report08.png"))
                        Ghost_Report8.SetResourceReference(ContentProperty, "Cus-G_AnsemReport08");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report09.png"))
                        Ghost_Report9.SetResourceReference(ContentProperty, "Cus-G_AnsemReport09");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report10.png"))
                        Ghost_Report10.SetResourceReference(ContentProperty, "Cus-G_AnsemReport10");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report11.png"))
                        Ghost_Report11.SetResourceReference(ContentProperty, "Cus-G_AnsemReport11");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report12.png"))
                        Ghost_Report12.SetResourceReference(ContentProperty, "Cus-G_AnsemReport12");
                    if (File.Exists("CustomImages/Checks/Ghost/ansem_report13.png"))
                        Ghost_Report13.SetResourceReference(ContentProperty, "Cus-G_AnsemReport13");

                    if (File.Exists("CustomImages/Checks/Ghost/fire.png"))
                    {
                        Ghost_Fire1.SetResourceReference(ContentProperty, "Cus-G_Fire");
                        Ghost_Fire2.SetResourceReference(ContentProperty, "Cus-G_Fire");
                        Ghost_Fire3.SetResourceReference(ContentProperty, "Cus-G_Fire");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/blizzard.png"))
                    {
                        Ghost_Blizzard1.SetResourceReference(ContentProperty, "Cus-G_Blizzard");
                        Ghost_Blizzard2.SetResourceReference(ContentProperty, "Cus-G_Blizzard");
                        Ghost_Blizzard3.SetResourceReference(ContentProperty, "Cus-G_Blizzard");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/thunder.png"))
                    {
                        Ghost_Thunder1.SetResourceReference(ContentProperty, "Cus-G_Thunder");
                        Ghost_Thunder2.SetResourceReference(ContentProperty, "Cus-G_Thunder");
                        Ghost_Thunder3.SetResourceReference(ContentProperty, "Cus-G_Thunder");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/cure.png"))
                    {
                        Ghost_Cure1.SetResourceReference(ContentProperty, "Cus-G_Cure");
                        Ghost_Cure2.SetResourceReference(ContentProperty, "Cus-G_Cure");
                        Ghost_Cure3.SetResourceReference(ContentProperty, "Cus-G_Cure");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/reflect.png"))
                    {
                        Ghost_Reflect1.SetResourceReference(ContentProperty, "Cus-G_Reflect");
                        Ghost_Reflect2.SetResourceReference(ContentProperty, "Cus-G_Reflect");
                        Ghost_Reflect3.SetResourceReference(ContentProperty, "Cus-G_Reflect");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/magnet.png"))
                    {
                        Ghost_Magnet1.SetResourceReference(ContentProperty, "Cus-G_Magnet");
                        Ghost_Magnet2.SetResourceReference(ContentProperty, "Cus-G_Magnet");
                        Ghost_Magnet3.SetResourceReference(ContentProperty, "Cus-G_Magnet");
                    }

                    if (File.Exists("CustomImages/Checks/Ghost/torn_pages.png"))
                    {
                        Ghost_TornPage1.SetResourceReference(ContentProperty, "Cus-G_TornPage");
                        Ghost_TornPage2.SetResourceReference(ContentProperty, "Cus-G_TornPage");
                        Ghost_TornPage3.SetResourceReference(ContentProperty, "Cus-G_TornPage");
                        Ghost_TornPage4.SetResourceReference(ContentProperty, "Cus-G_TornPage");
                        Ghost_TornPage5.SetResourceReference(ContentProperty, "Cus-G_TornPage");
                    }

                    if (File.Exists("CustomImages/Checks/Ghost/valor.png"))
                    {
                        Ghost_Valor.SetResourceReference(ContentProperty, "Cus-G_Valor");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/wisdom.png"))
                    {
                        Ghost_Wisdom.SetResourceReference(ContentProperty, "Cus-G_Wisdom");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/limit.png"))
                    {
                        Ghost_Limit.SetResourceReference(ContentProperty, "Cus-G_Limit");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/master.png"))
                    {
                        Ghost_Master.SetResourceReference(ContentProperty, "Cus-G_Master");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/final.png"))
                    {
                        Ghost_Final.SetResourceReference(ContentProperty, "Cus-G_Final");
                    }

                    if (File.Exists("CustomImages/Checks/Ghost/genie.png"))
                    {
                        Ghost_Lamp.SetResourceReference(ContentProperty, "Cus-G_Genie");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/stitch.png"))
                    {
                        Ghost_Ukulele.SetResourceReference(ContentProperty, "Cus-G_Stitch");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/chicken_little.png"))
                    {
                        Ghost_Baseball.SetResourceReference(ContentProperty, "Cus-G_ChickenLittle");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/peter_pan.png"))
                    {
                        Ghost_Feather.SetResourceReference(ContentProperty, "Cus-G_PeterPan");
                    }

                    if (File.Exists("CustomImages/Checks/Ghost/proof_of_nonexistence.png"))
                    {
                        Ghost_Nonexistence.SetResourceReference(ContentProperty, "Cus-G_ProofOfNon");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/proof_of_connection.png"))
                    {
                        Ghost_Connection.SetResourceReference(ContentProperty, "Cus-G_ProofOfCon");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/proof_of_tranquility.png"))
                    {
                        Ghost_Peace.SetResourceReference(ContentProperty, "Cus-G_ProofOfPea");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/promise_charm.png"))
                    {
                        Ghost_PromiseCharm.SetResourceReference(ContentProperty, "Cus-G_PromiseCharm");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/once_more.png"))
                    {
                        Ghost_OnceMore.SetResourceReference(ContentProperty, "Cus-G_OnceMore");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/second_chance.png"))
                    {
                        Ghost_SecondChance.SetResourceReference(ContentProperty, "Cus-G_SecondChance");
                    }
                }

                if (CustomLevelFound)
                {
                    LevelIcon.SetResourceReference(ContentProperty, "Cus-LevelIcon");
                    broadcast.LevelIcon.SetResourceReference(ContentProperty, "Cus-LevelIcon");
                }

                if (CustomStrengthFound)
                {
                    StrengthIcon.SetResourceReference(ContentProperty, "Cus-StrengthIcon");
                    broadcast.StrengthIcon.SetResourceReference(ContentProperty, "Cus-StrengthIcon");
                }

                if (CustomMagicFound)
                {
                    MagicIcon.SetResourceReference(ContentProperty, "Cus-MagicIcon");
                    broadcast.MagicIcon.SetResourceReference(ContentProperty, "Cus-MagicIcon");
                }

                if (CustomDefenseFound)
                {
                    DefenseIcon.SetResourceReference(ContentProperty, "Cus-DefenseIcon");
                    broadcast.DefenseIcon.SetResourceReference(ContentProperty, "Cus-DefenseIcon");
                }

                //broadcast window specific
                if (File.Exists("CustomImages/Broadcast/Other/ansem_report.png"))
                {
                    broadcast.Report.SetResourceReference(ContentProperty, "Cus-B_AnsemReport");
                }
                if (File.Exists("CustomImages/Broadcast/Other/torn_pages.png"))
                {
                    broadcast.TornPage.SetResourceReference(ContentProperty, "Cus-B_TornPage");
                }
                if (File.Exists("CustomImages/Broadcast/Other/chest.png"))
                {
                    broadcast.Chest.SetResourceReference(ContentProperty, "Cus-B_Chest");
                }
                if (Directory.Exists("CustomImages/Broadcast/Checks/"))
                {
                    if (File.Exists("CustomImages/Broadcast/Checks/jump.png"))
                    {
                        broadcast.HighJump.SetResourceReference(ContentProperty, "Cus-B_HighJump");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/quick.png"))
                    {
                        broadcast.QuickRun.SetResourceReference(ContentProperty, "Cus-B_QuickRun");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/dodge.png"))
                    {
                        broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Cus-B_DodgeRoll");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/aerial.png"))
                    {
                        broadcast.AerialDodge.SetResourceReference(ContentProperty, "Cus-B_AerialDodge");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/glide.png"))
                    {
                        broadcast.Glide.SetResourceReference(ContentProperty, "Cus-B_Glide");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/fire.png"))
                    {
                        broadcast.Fire.SetResourceReference(ContentProperty, "Cus-B_Fire");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/blizzard.png"))
                    {
                        broadcast.Blizzard.SetResourceReference(ContentProperty, "Cus-B_Blizzard");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/thunder.png"))
                    {
                        broadcast.Thunder.SetResourceReference(ContentProperty, "Cus-B_Thunder");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/cure.png"))
                    {
                        broadcast.Cure.SetResourceReference(ContentProperty, "Cus-B_Cure");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/reflect.png"))
                    {
                        broadcast.Reflect.SetResourceReference(ContentProperty, "Cus-B_Reflect");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/magnet.png"))
                    {
                        broadcast.Magnet.SetResourceReference(ContentProperty, "Cus-B_Magnet");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/valor.png"))
                    {
                        broadcast.Valor.SetResourceReference(ContentProperty, "Cus-B_Valor");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/wisdom.png"))
                    {
                        broadcast.Wisdom.SetResourceReference(ContentProperty, "Cus-B_Wisdom");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/limit.png"))
                    {
                        broadcast.Limit.SetResourceReference(ContentProperty, "Cus-B_Limit");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/master.png"))
                    {
                        broadcast.Master.SetResourceReference(ContentProperty, "Cus-B_Master");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/final.png"))
                    {
                        broadcast.Final.SetResourceReference(ContentProperty, "Cus-B_Final");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/genie.png"))
                    {
                        broadcast.Lamp.SetResourceReference(ContentProperty, "Cus-B_Genie");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/stitch.png"))
                    {
                        broadcast.Ukulele.SetResourceReference(ContentProperty, "Cus-B_Stitch");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/chicken_little.png"))
                    {
                        broadcast.Baseball.SetResourceReference(ContentProperty, "Cus-B_ChickenLittle");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/peter_pan.png"))
                    {
                        broadcast.Feather.SetResourceReference(ContentProperty, "Cus-B_PeterPan");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_nonexistence.png"))
                    {
                        broadcast.Nonexistence.SetResourceReference(ContentProperty, "Cus-B_ProofOfNon");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_connection.png"))
                    {
                        broadcast.Connection.SetResourceReference(ContentProperty, "Cus-B_ProofOfCon");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_tranquility.png"))
                    {
                        broadcast.Peace.SetResourceReference(ContentProperty, "Cus-B_ProofOfPea");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/promise_charm.png"))
                    {
                        broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Cus-B_PromiseCharm");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/once_more.png"))
                    {
                        broadcast.OnceMore.SetResourceReference(ContentProperty, "Cus-B_OnceMore");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/second_chance.png"))
                    {
                        broadcast.SecondChance.SetResourceReference(ContentProperty, "Cus-B_SecondChance");
                    }
                }
            }
        }

        private void CustomWorldCheck()
        {
            {
                if (MinWorldOption.IsChecked)
                {
                    if (CavernOption.IsChecked)
                    {
                        HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionCorImage");
                        broadcast.HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionCorImage");
                    }
                    else
                    {
                        HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                        broadcast.HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                    }

                    if (TimelessOption.IsChecked)
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleTrImage");
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleTrImage");
                    }
                    else
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleImage");
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleImage");
                    }

                    if (OCCupsOption.IsChecked)
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusCupsImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusCupsImage");
                    }
                    else
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                    }
                }
                if (OldWorldOption.IsChecked)
                {
                    if (CavernOption.IsChecked)
                    {
                        HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionCorImage");
                        broadcast.HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionCorImage");
                    }
                    else
                    {
                        HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                        broadcast.HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                    }

                    if (TimelessOption.IsChecked)
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleTrImage");
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleTrImage");
                    }
                    else
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleImage");
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleImage");
                    }

                    if (OCCupsOption.IsChecked)
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusCupsImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusCupsImage");
                    }
                    else
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                    }
                }
            }

            if (CustomFolderOption.IsChecked)
            {
                //Main Window
                //We set both main and broadcast window worlds to the same image here
                //this makes it so that the broadcast window uses the same custom images that the
                //main window uses unless the specific broadacst window world folder is found
                if (Directory.Exists("CustomImages/Worlds/"))
                {
                    if (File.Exists("CustomImages/Worlds/level.png"))
                    {
                        SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Worlds/simulated_twilight_town.png"))
                    {
                        SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-SimulatedImage");
                        broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-SimulatedImage");
                    }
                    if (File.Exists("CustomImages/Worlds/land_of_dragons.png"))
                    {
                        LandofDragons.SetResourceReference(ContentProperty, "Cus-LandofDragonsImage");
                        broadcast.LandofDragons.SetResourceReference(ContentProperty, "Cus-LandofDragonsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/pride_land.png"))
                    {
                        PrideLands.SetResourceReference(ContentProperty, "Cus-PrideLandsImage");
                        broadcast.PrideLands.SetResourceReference(ContentProperty, "Cus-PrideLandsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/halloween_town.png"))
                    {
                        HalloweenTown.SetResourceReference(ContentProperty, "Cus-HalloweenTownImage");
                        broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Cus-HalloweenTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/space_paranoids.png"))
                    {
                        SpaceParanoids.SetResourceReference(ContentProperty, "Cus-SpaceParanoidsImage");
                        broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Cus-SpaceParanoidsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/drive_form.png"))
                    {
                        DriveForms.SetResourceReference(ContentProperty, "Cus-DriveFormsImage");
                        broadcast.DriveForms.SetResourceReference(ContentProperty, "Cus-DriveFormsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/twilight_town.png"))
                    {
                        TwilightTown.SetResourceReference(ContentProperty, "Cus-TwilightTownImage");
                        broadcast.TwilightTown.SetResourceReference(ContentProperty, "Cus-TwilightTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/beast's_castle.png"))
                    {
                        BeastsCastle.SetResourceReference(ContentProperty, "Cus-BeastCastleImage");
                        broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Cus-BeastCastleImage");
                    }
                    if (File.Exists("CustomImages/Worlds/agrabah.png"))
                    {
                        Agrabah.SetResourceReference(ContentProperty, "Cus-AgrabahImage");
                        broadcast.Agrabah.SetResourceReference(ContentProperty, "Cus-AgrabahImage");
                    }
                    if (File.Exists("CustomImages/Worlds/100_acre_wood.png"))
                    {
                        HundredAcreWood.SetResourceReference(ContentProperty, "Cus-HundredAcreImage");
                        broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Cus-HundredAcreImage");
                    }
                    if (File.Exists("CustomImages/Worlds/port_royal.png"))
                    {
                        PortRoyal.SetResourceReference(ContentProperty, "Cus-PortRoyalImage");
                        broadcast.PortRoyal.SetResourceReference(ContentProperty, "Cus-PortRoyalImage");
                    }
                    if (File.Exists("CustomImages/Worlds/the_world_that_never_was.png"))
                    {
                        TWTNW.SetResourceReference(ContentProperty, "Cus-TWTNWImage");
                        broadcast.TWTNW.SetResourceReference(ContentProperty, "Cus-TWTNWImage");
                    }
                    if (File.Exists("CustomImages/Worlds/atlantica.png"))
                    {
                        Atlantica.SetResourceReference(ContentProperty, "Cus-AtlanticaImage");
                        broadcast.Atlantica.SetResourceReference(ContentProperty, "Cus-AtlanticaImage");
                    }
                    if (File.Exists("CustomImages/Worlds/replica_data.png"))
                    {
                        GoA.SetResourceReference(ContentProperty, "Cus-GardenofAssemblageImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        //basically we only want to set the custom images if they exist and if the toggle is on/off
                        //otherwise they just use the defaults that were defined in the beggining of this function
                        if (File.Exists("CustomImages/Worlds/hollow_bastion.png") && !CavernOption.IsChecked)
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/hollow_bastion_cor.png") && CavernOption.IsChecked)
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionCorImage");
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionCorImage");
                        }

                        if (File.Exists("CustomImages/Worlds/disney_castle.png") && !TimelessOption.IsChecked)
                        {
                            DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/disney_castle_tr.png") && TimelessOption.IsChecked)
                        {
                            DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleTrImage");
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleTrImage");
                        }

                        if (File.Exists("CustomImages/Worlds/olympus_coliseum.png") && !OCCupsOption.IsChecked)
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/olympus_coliseum_cups.png") && OCCupsOption.IsChecked)
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusCupsImage");
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusCupsImage");
                        }
                    }

                }

                //Broadcast Window
                if (Directory.Exists("CustomImages/Broadcast/Worlds/"))
                {
                    if (File.Exists("CustomImages/Broadcast/Worlds/level.png"))
                    {
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-B_SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/simulated_twilight_town.png"))
                    {
                        broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-B_SimulatedImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/land_of_dragons.png"))
                    {
                        broadcast.LandofDragons.SetResourceReference(ContentProperty, "Cus-B_LandofDragonsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/pride_land.png"))
                    {
                        broadcast.PrideLands.SetResourceReference(ContentProperty, "Cus-B_PrideLandsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/halloween_town.png"))
                    {
                        broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Cus-B_HalloweenTownImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/space_paranoids.png"))
                    {
                        broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Cus-B_SpaceParanoidsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/drive_form.png"))
                    {
                        broadcast.DriveForms.SetResourceReference(ContentProperty, "Cus-B_DriveFormsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/twilight_town.png"))
                    {
                        broadcast.TwilightTown.SetResourceReference(ContentProperty, "Cus-B_TwilightTownImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/beast's_castle.png"))
                    {
                        broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Cus-B_BeastCastleImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/agrabah.png"))
                    {
                        broadcast.Agrabah.SetResourceReference(ContentProperty, "Cus-B_AgrabahImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/100_acre_wood.png"))
                    {
                        broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Cus-B_HundredAcreImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/port_royal.png"))
                    {
                        broadcast.PortRoyal.SetResourceReference(ContentProperty, "Cus-B_PortRoyalImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/the_world_that_never_was.png"))
                    {
                        broadcast.TWTNW.SetResourceReference(ContentProperty, "Cus-B_TWTNWImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/atlantica.png"))
                    {
                        broadcast.Atlantica.SetResourceReference(ContentProperty, "Cus-B_AtlanticaImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        //basically we only want to set the custom images if they exist and if the toggle is on/off
                        //otherwise they just use the defaults that were defined in the beggining of this function
                        if (File.Exists("CustomImages/Broadcast/Worlds/hollow_bastion.png") && !CavernOption.IsChecked)
                        {
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-B_HollowBastionImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/hollow_bastion_cor.png") && CavernOption.IsChecked)
                        {
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-B_HollowBastionCorImage");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/disney_castle.png") && !TimelessOption.IsChecked)
                        {
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/disney_castle_tr.png") && TimelessOption.IsChecked)
                        {
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleTrImage");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum.png") && !OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum_cups.png") && OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusCupsImage");
                        }
                    }

                }
            }
        }

        private void ReportNumCheck()
        {
            //since the numbers are a part of the actual report image, we need this if the user changes the
            //number toggle or the check toggle so that the reports have the correct image

            if (MinCheckOption.IsChecked && !CustomFolderOption.IsChecked)
            {
                if (OldNumOption.IsChecked)
                {
                    Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01_old");
                    Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02_old");
                    Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03_old");
                    Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04_old");
                    Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05_old");
                    Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06_old");
                    Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07_old");
                    Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08_old");
                    Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09_old");
                    Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10_old");
                    Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11_old");
                    Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12_old");
                    Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13_old");

                    Ghost_Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01_old");
                    Ghost_Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02_old");
                    Ghost_Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03_old");
                    Ghost_Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04_old");
                    Ghost_Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05_old");
                    Ghost_Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06_old");
                    Ghost_Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07_old");
                    Ghost_Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08_old");
                    Ghost_Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09_old");
                    Ghost_Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10_old");
                    Ghost_Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11_old");
                    Ghost_Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12_old");
                    Ghost_Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13_old");
                }
                else
                {
                    Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01");
                    Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02");
                    Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03");
                    Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04");
                    Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05");
                    Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06");
                    Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07");
                    Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08");
                    Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09");
                    Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10");
                    Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11");
                    Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12");
                    Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13");

                    Ghost_Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01");
                    Ghost_Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02");
                    Ghost_Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03");
                    Ghost_Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04");
                    Ghost_Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05");
                    Ghost_Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06");
                    Ghost_Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07");
                    Ghost_Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08");
                    Ghost_Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09");
                    Ghost_Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10");
                    Ghost_Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11");
                    Ghost_Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12");
                    Ghost_Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13");
                }
            }

            if (OldCheckOption.IsChecked && !CustomFolderOption.IsChecked)
            {

                if (OldNumOption.IsChecked)
                {
                    Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01_old");
                    Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02_old");
                    Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03_old");
                    Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04_old");
                    Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05_old");
                    Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06_old");
                    Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07_old");
                    Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08_old");
                    Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09_old");
                    Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10_old");
                    Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11_old");
                    Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12_old");
                    Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13_old");

                    Ghost_Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01_old");
                    Ghost_Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02_old");
                    Ghost_Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03_old");
                    Ghost_Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04_old");
                    Ghost_Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05_old");
                    Ghost_Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06_old");
                    Ghost_Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07_old");
                    Ghost_Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08_old");
                    Ghost_Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09_old");
                    Ghost_Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10_old");
                    Ghost_Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11_old");
                    Ghost_Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12_old");
                    Ghost_Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13_old");
                }
                else
                {
                    Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01");
                    Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02");
                    Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03");
                    Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04");
                    Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05");
                    Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06");
                    Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07");
                    Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08");
                    Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09");
                    Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10");
                    Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11");
                    Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12");
                    Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13");

                    Ghost_Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01");
                    Ghost_Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02");
                    Ghost_Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03");
                    Ghost_Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04");
                    Ghost_Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05");
                    Ghost_Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06");
                    Ghost_Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07");
                    Ghost_Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08");
                    Ghost_Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09");
                    Ghost_Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10");
                    Ghost_Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11");
                    Ghost_Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12");
                    Ghost_Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13");
                }
            }
        }
    }
}
