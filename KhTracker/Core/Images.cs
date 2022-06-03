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
        public static bool CustomGreenNumbersFound = false;
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
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Yellow/QuestionMark.png", UriKind.Relative)));

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
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Blue/QuestionMark.png", UriKind.Relative)));

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
            data.GreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Kh2/Green/QuestionMark.png", UriKind.Relative)));

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
            data.OldSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Yellow/QuestionMark.png", UriKind.Relative)));

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
            data.OldBlueSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Blue/QuestionMark.png", UriKind.Relative)));

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
            data.OldGreenSingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/Old/Green/QuestionMark.png", UriKind.Relative)));
            #endregion

            #region Custom Numbers
            //Default are the KH2 numbers
            string GoodPath = "Images/Numbers/Kh2/";
            string GoodPathBlue = "Images/Numbers/Kh2/";
            string GoodPathGreen = "Images/Numbers/Kh2/";

            //Separete variable for if the user wanted to customize one set of numbers
            UriKind urikindvar = UriKind.Relative;
            UriKind urikindvarblue = UriKind.Relative;
            UriKind urikindvargreen = UriKind.Relative;

            //Fix paths if Old Numbers toggle is on
            if (OldNumOption.IsChecked)
            {
                GoodPath = "Images/Numbers/Old/";
                GoodPathBlue = "Images/Numbers/Old/";
                GoodPathGreen = "Images/Numbers/Old/";
            }

            //I don't want to set up the ability to replace just single images so all number images need to be included to customize them
            if (File.Exists("CustomImages/Numbers/Yellow/0.png") && File.Exists("CustomImages/Numbers/Yellow/1.png") && File.Exists("CustomImages/Numbers/Yellow/2.png") && File.Exists("CustomImages/Numbers/Yellow/3.png") &&
                File.Exists("CustomImages/Numbers/Yellow/4.png") && File.Exists("CustomImages/Numbers/Yellow/5.png") && File.Exists("CustomImages/Numbers/Yellow/6.png") && File.Exists("CustomImages/Numbers/Yellow/7.png") &&
                File.Exists("CustomImages/Numbers/Yellow/8.png") && File.Exists("CustomImages/Numbers/Yellow/9.png") && File.Exists("CustomImages/Numbers/Yellow/QuestionMark.png"))
            {
                GoodPath = "pack://application:,,,/CustomImages/Numbers/";
                urikindvar = UriKind.Absolute;
                CustomNumbersFound = true;
            }
            if (File.Exists("CustomImages/Numbers/Blue/0.png") && File.Exists("CustomImages/Numbers/Blue/1.png") && File.Exists("CustomImages/Numbers/Blue/2.png") && File.Exists("CustomImages/Numbers/Blue/3.png") &&
                File.Exists("CustomImages/Numbers/Blue/4.png") && File.Exists("CustomImages/Numbers/Blue/5.png") && File.Exists("CustomImages/Numbers/Blue/6.png") && File.Exists("CustomImages/Numbers/Blue/7.png") &&
                File.Exists("CustomImages/Numbers/Blue/8.png") && File.Exists("CustomImages/Numbers/Blue/9.png") && File.Exists("CustomImages/Numbers/Blue/QuestionMark.png"))
            {
                GoodPathBlue = "pack://application:,,,/CustomImages/Numbers/";
                urikindvarblue = UriKind.Absolute;
                CustomBlueNumbersFound = true;
            }
            if (File.Exists("CustomImages/Numbers/Green/0.png") && File.Exists("CustomImages/Numbers/Green/1.png") && File.Exists("CustomImages/Numbers/Green/2.png") && File.Exists("CustomImages/Numbers/Green/3.png") &&
                File.Exists("CustomImages/Numbers/Green/4.png") && File.Exists("CustomImages/Numbers/Green/5.png") && File.Exists("CustomImages/Numbers/Green/6.png") && File.Exists("CustomImages/Numbers/Green/7.png") &&
                File.Exists("CustomImages/Numbers/Green/8.png") && File.Exists("CustomImages/Numbers/Green/9.png") && File.Exists("CustomImages/Numbers/Green/QuestionMark.png"))
            {
                GoodPathGreen = "pack://application:,,,/CustomImages/Numbers/";
                urikindvargreen = UriKind.Absolute;
                CustomGreenNumbersFound = true;
            }

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
            data.CustomSingleNumbers.Add(new BitmapImage(new Uri(GoodPath + "Yellow/QuestionMark.png", urikindvar)));

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
            data.CustomBlueSingleNumbers.Add(new BitmapImage(new Uri(GoodPathBlue + "Blue/QuestionMark.png", urikindvarblue)));

            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/0.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/1.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/2.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/3.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/4.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/5.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/6.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/7.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/8.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/9.png", urikindvargreen)));
            data.CustomGreenSingleNumbers.Add(new BitmapImage(new Uri(GoodPathGreen + "Green/QuestionMark.png", urikindvargreen)));
            #endregion

            //i really hate how i did some of this

            //for autodetect (won't bother making this customizable for now)
            data.AD_Connect = new BitmapImage(new Uri("Images/connect.png", UriKind.Relative));
            data.AD_PC = new BitmapImage(new Uri("Images/PC.png", UriKind.Relative));
            data.AD_PCred = new BitmapImage(new Uri("Images/PCred.png", UriKind.Relative));
            data.AD_PS2 = new BitmapImage(new Uri("Images/ps2.png", UriKind.Relative));

            //check for custom stat and weapon icons (OLD)
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


            //set stuff for the vertical images
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

            //set stuff for the slash bar images
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

            //check for custom progression icons
            if (File.Exists("CustomImages/Progression/1k.png") && File.Exists("CustomImages/Progression/carpet.png") && File.Exists("CustomImages/Progression/screens.png") && File.Exists("CustomImages/Progression/kanga.png"))
                CustomProgFound = true;

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
            //defaults
            List<BitmapImage> SingleNum = data.SingleNumbers;
            List<BitmapImage> SingleBlueNum = data.BlueSingleNumbers;
            List<BitmapImage> SingleGreenNum = data.GreenSingleNumbers;

            //Get correct numbers
            {
                if (Properties.Settings.Default.OldNum)
                {
                    SingleNum = data.OldSingleNumbers;
                    SingleBlueNum = data.OldBlueSingleNumbers;
                    SingleGreenNum = data.OldGreenSingleNumbers;
                }

                if (Properties.Settings.Default.CustomIcons)
                {
                    if (CustomNumbersFound)
                    {
                        SingleNum = data.CustomSingleNumbers;
                    }
                    if (CustomBlueNumbersFound)
                    {
                        SingleBlueNum = data.CustomBlueSingleNumbers;
                    }
                    if (CustomGreenNumbersFound)
                    {
                        SingleGreenNum = data.CustomGreenSingleNumbers;
                    }
                }
            }

            //return correct number list
            switch(type)
            {
                case "B":
                case "SB":
                    return SingleBlueNum;
                case "G":
                case "SG":
                    return SingleGreenNum;
                case "Y":
                case "S":
                default:
                    return SingleNum;
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

                    if (File.Exists("CustomImages/Checks/AncestorSword.png"))
                    {
                        MulanWep.SetResourceReference(ContentProperty, "Cus-MulanWep");
                        broadcast.MulanWep.SetResourceReference(ContentProperty, "Cus-MulanWep");
                    }
                    if (File.Exists("CustomImages/Checks/BattlefieldsofWar.png"))
                    {
                        AuronWep.SetResourceReference(ContentProperty, "Cus-AuronWep");
                        broadcast.AuronWep.SetResourceReference(ContentProperty, "Cus-AuronWep");
                    }
                    if (File.Exists("CustomImages/Checks/BeastClaw.png"))
                    {
                        BeastWep.SetResourceReference(ContentProperty, "Cus-BeastWep");
                        broadcast.BeastWep.SetResourceReference(ContentProperty, "Cus-BeastWep");
                    }
                    if (File.Exists("CustomImages/Checks/BoneFist.png"))
                    {
                        JackWep.SetResourceReference(ContentProperty, "Cus-JackWep");
                        broadcast.JackWep.SetResourceReference(ContentProperty, "Cus-JackWep");
                    }
                    if (File.Exists("CustomImages/Checks/IceCream.png"))
                    {
                        IceCream.SetResourceReference(ContentProperty, "Cus-IceCream");
                        broadcast.IceCream.SetResourceReference(ContentProperty, "Cus-IceCream");
                    }
                    if (File.Exists("CustomImages/Checks/IdentityDisk.png"))
                    {
                        TronWep.SetResourceReference(ContentProperty, "Cus-TronWep");
                        broadcast.TronWep.SetResourceReference(ContentProperty, "Cus-TronWep");
                    }
                    if (File.Exists("CustomImages/Checks/Picture.png"))
                    {
                        Picture.SetResourceReference(ContentProperty, "Cus-Picture");
                        broadcast.Picture.SetResourceReference(ContentProperty, "Cus-Picture");
                    }
                    if (File.Exists("CustomImages/Checks/membership_card.png"))
                    {
                        MembershipCard.SetResourceReference(ContentProperty, "Cus-MembershipCard");
                        broadcast.MembershipCard.SetResourceReference(ContentProperty, "Cus-MembershipCard");
                    }
                    if (File.Exists("CustomImages/Checks/ProudFang.png"))
                    {
                        SimbaWep.SetResourceReference(ContentProperty, "Cus-SimbaWep");
                        broadcast.SimbaWep.SetResourceReference(ContentProperty, "Cus-SimbaWep");
                    }
                    if (File.Exists("CustomImages/Checks/Scimitar.png"))
                    {
                        AladdinWep.SetResourceReference(ContentProperty, "Cus-AladdinWep");
                        broadcast.AladdinWep.SetResourceReference(ContentProperty, "Cus-AladdinWep");
                    }
                    if (File.Exists("CustomImages/Checks/SkillCrossbones.png"))
                    {
                        SparrowWep.SetResourceReference(ContentProperty, "Cus-SparrowWep");
                        broadcast.SparrowWep.SetResourceReference(ContentProperty, "Cus-SparrowWep");
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

                    if (File.Exists("CustomImages/Checks/Ghost/AncestorSword.png"))
                    {
                        Ghost_MulanWep.SetResourceReference(ContentProperty, "Cus-G_MulanWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/BattlefieldsofWar.png"))
                    {
                        Ghost_AuronWep.SetResourceReference(ContentProperty, "Cus-G_AuronWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/BeastClaw.png"))
                    {
                        Ghost_BeastWep.SetResourceReference(ContentProperty, "Cus-G_BeastWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/BoneFist.png"))
                    {
                        Ghost_JackWep.SetResourceReference(ContentProperty, "Cus-G_JackWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/IceCream.png"))
                    {
                        Ghost_IceCream.SetResourceReference(ContentProperty, "Cus-G_IceCream");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/IdentityDisk.png"))
                    {
                        Ghost_TronWep.SetResourceReference(ContentProperty, "Cus-G_TronWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/Picture.png"))
                    {
                        Ghost_Picture.SetResourceReference(ContentProperty, "Cus-G_Picture");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/membership_card.png"))
                    {
                        Ghost_MembershipCard.SetResourceReference(ContentProperty, "Cus-G_MembershipCard");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/ProudFang.png"))
                    {
                        Ghost_SimbaWep.SetResourceReference(ContentProperty, "Cus-G_SimbaWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/Scimitar.png"))
                    {
                        Ghost_AladdinWep.SetResourceReference(ContentProperty, "Cus-G_AladdinWep");
                    }
                    if (File.Exists("CustomImages/Checks/Ghost/SkillCrossbones.png"))
                    {
                        Ghost_SparrowWep.SetResourceReference(ContentProperty, "Cus-G_SparrowWep");
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

                //visit locks
                if (File.Exists("CustomImages/Other/visitlock.png"))
                {
                    HollowBastionLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    OlympusColiseumLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    LandofDragonsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    PrideLandsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    HalloweenTownLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    SpaceParanoidsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    BeastsCastleLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    AgrabahLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    PortRoyalLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    TwilightTownLock_2.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));

                    broadcast.HollowBastionLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.OlympusColiseumLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.LandofDragonsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.PrideLandsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.HalloweenTownLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.SpaceParanoidsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.BeastsCastleLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.AgrabahLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.PortRoyalLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                    broadcast.TwilightTownLock_2.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlock.png", UriKind.Absolute));
                }
                if (File.Exists("CustomImages/Other/visitlocksilver.png"))
                {
                    TwilightTownLock_1.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlocksilver.png", UriKind.Absolute));
                    broadcast.TwilightTownLock_1.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlocksilver.png", UriKind.Absolute));
                }

                //world cross
                if (File.Exists("CustomImages/Other/crossworld.png"))
                {
                    SorasHeartCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    DriveFormsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    SimulatedTwilightTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    TwilightTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    HollowBastionCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    BeastsCastleCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    OlympusColiseumCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    AgrabahCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    LandofDragonsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    HundredAcreWoodCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    PrideLandsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    DisneyCastleCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    HalloweenTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    PortRoyalCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    TWTNWCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    SpaceParanoidsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    AtlanticaCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    PuzzSynthCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    GoACross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));

                    broadcast.SorasHeartCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.DriveFormsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.SimulatedTwilightTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.TwilightTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.HollowBastionCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.BeastsCastleCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.OlympusColiseumCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.AgrabahCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.LandofDragonsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.HundredAcreWoodCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.PrideLandsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.DisneyCastleCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.HalloweenTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.PortRoyalCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.TWTNWCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.SpaceParanoidsCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.AtlanticaCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                    broadcast.PuzzSynthCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/crossworld.png", UriKind.Absolute));
                }

                //DeathCounter counter skull
                if (File.Exists("CustomImages/Other/death.png"))
                {
                    Skull.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/death.png", UriKind.Absolute));
                    broadcast.Skull.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/death.png", UriKind.Absolute));
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


                    if (File.Exists("CustomImages/Broadcast/Checks/AncestorSword.png"))
                    {
                        broadcast.MulanWep.SetResourceReference(ContentProperty, "Cus-B_MulanWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/BattlefieldsofWar.png"))
                    {
                        broadcast.AuronWep.SetResourceReference(ContentProperty, "Cus-B_AuronWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/BeastClaw.png"))
                    {
                        broadcast.BeastWep.SetResourceReference(ContentProperty, "Cus-B_BeastWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/BoneFist.png"))
                    {
                        broadcast.JackWep.SetResourceReference(ContentProperty, "Cus-B_JackWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/IceCream.png"))
                    {
                        broadcast.IceCream.SetResourceReference(ContentProperty, "Cus-B_IceCream");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/IdentityDisk.png"))
                    {
                        broadcast.TronWep.SetResourceReference(ContentProperty, "Cus-B_TronWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/Picture.png"))
                    {
                        broadcast.Picture.SetResourceReference(ContentProperty, "Cus-B_Picture");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/membership_card.png"))
                    {
                        broadcast.MembershipCard.SetResourceReference(ContentProperty, "Cus-B_MembershipCard");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/ProudFang.png"))
                    {
                        broadcast.SimbaWep.SetResourceReference(ContentProperty, "Cus-B_SimbaWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/Scimitar.png"))
                    {
                        broadcast.AladdinWep.SetResourceReference(ContentProperty, "Cus-B_AladdinWep");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/SkillCrossbones.png"))
                    {
                        broadcast.SparrowWep.SetResourceReference(ContentProperty, "Cus-B_SparrowWep");
                    }

                }
            }
        }

        private void CustomWorldCheck()
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

                //puzzle/synth display
                if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth");
                }
                if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_S");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_S");
                }
                else if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_P");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_P");
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

                //puzzle/synth display
                if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth");
                }
                if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_S");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_S");
                }
                else if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_P");
                    broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_P");
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
                    if (File.Exists("CustomImages/Worlds/level.png"))
                    {
                        SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Worlds/disney_castle.png"))
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        if (File.Exists("CustomImages/Worlds/lingering_will.png"))
                        {
                            DisneyCastleLW.SetResourceReference(ContentProperty, "Cus-DisneyCastleLW");
                            broadcast.DisneyCastleLW.SetResourceReference(ContentProperty, "Cus-DisneyCastleLW");
                        }

                        if (File.Exists("CustomImages/Worlds/Level01.png") && SoraLevel01Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel01");
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel01");
                        }
                        if (File.Exists("CustomImages/Worlds/Level50.png") && SoraLevel50Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel50");
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel50");
                        }
                        if (File.Exists("CustomImages/Worlds/Level99.png") && SoraLevel99Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel99");
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel99");
                        }

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

                        //puzzle/synth display
                        if (File.Exists("CustomImages/Worlds/PuzzSynth.png") && PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth");
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth");
                        }
                        if (File.Exists("CustomImages/Worlds/Synth.png") && !PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_S");
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_S");
                        }
                        else if (File.Exists("CustomImages/Worlds/Puzzle.png") && PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_P");
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_P");
                        }

                    }
                }

                //Broadcast Window
                if (Directory.Exists("CustomImages/Broadcast/Worlds/"))
                {
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
                    if (File.Exists("CustomImages/Broadcast/Worlds/level.png"))
                    {
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-B_SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/disney_castle.png"))
                    {
                        broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        if (File.Exists("CustomImages/Broadcast/Worlds/lingering_will.png"))
                        {
                            broadcast.DisneyCastleLW.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleLW");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/Level01.png") && SoraLevel01Option.IsChecked)
                        {
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-B_SoraLevel01");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/Level50.png") && SoraLevel50Option.IsChecked)
                        {
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-B_SoraLevel50");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/Level99.png") && SoraLevel99Option.IsChecked)
                        {
                            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Cus-B_SoraLevel99");
                        }

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

                        if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum.png") && !OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum_cups.png") && OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusCupsImage");
                        }

                        //puzzle/synth display
                        if (File.Exists("CustomImages/Broadcast/Worlds/PuzzSynth.png") && PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                        {
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-B_PuzzSynth");
                        }
                        if (File.Exists("CustomImages/Broadcast/Worlds/Synth.png") && !PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                        {
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-B_PuzzSynth_S");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/Puzzle.png") && PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                        {
                            broadcast.PuzzSynth.SetResourceReference(ContentProperty, "Cus-B_PuzzSynth_P");
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
