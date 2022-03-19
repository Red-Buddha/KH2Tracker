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
            #region Numbers
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

            //Old numbers
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

            //i really hate how i did some of this

            //check for custom stat and weapon icons
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
            var SingleNum = data.SingleNumbers;
            var SingleBlueNum = data.BlueSingleNumbers;

            //Get correct numbers
            {
                if (OldMode)
                {
                    NormalNum = data.OldNumbers;
                    BlueNum = data.OldBlueNumbers;
                    SingleNum = data.OldSingleNumbers;
                    SingleBlueNum = data.OldBlueSingleNumbers;
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
                }
            }

            //return correct number list
            if (type == "Y")
            {
                return NormalNum;
            }
            else if (type == "B")
            {
                return BlueNum;
            }
            else if (type == "S")
            {
                return SingleNum;
            }
            else if (type == "SB")
            {
                return SingleBlueNum;
            }
            else
                return NormalNum;
        }
    }
}
