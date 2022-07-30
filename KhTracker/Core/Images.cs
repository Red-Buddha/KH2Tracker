using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        //dumb stuff to help figure out what to do about custom images
        public static bool CustomSwordFound = false;
        public static bool CustomStaffFound = false;
        public static bool CustomShieldFound = false;
        public static bool CustomLevelFound = false;
        public static bool CustomStrengthFound = false;
        public static bool CustomMagicFound = false;
        public static bool CustomDefenseFound = false;
        public static bool CustomProgFound = false;

        //handle adding all custom images and such
        public void InitImages()
        {
            //i really hate how i did some of this

            //for autodetect (won't bother making this customizable for now)
            data.AD_Connect = new BitmapImage(new Uri("Images/connect.png", UriKind.Relative));
            data.AD_PC = new BitmapImage(new Uri("Images/PC.png", UriKind.Relative));
            data.AD_PCred = new BitmapImage(new Uri("Images/PCred.png", UriKind.Relative));
            data.AD_PS2 = new BitmapImage(new Uri("Images/ps2.png", UriKind.Relative));

            //check for custom stat and weapon icons
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

            //check for custom progression icons. if these 4 are there then assume all progression icons are there
            //no reason for these 4 specific ones, i just picked 4 completely unrelated ones at random.
            if (File.Exists("CustomImages/Progression/1k.png") && File.Exists("CustomImages/Progression/carpet.png") && 
                File.Exists("CustomImages/Progression/screens.png") && File.Exists("CustomImages/Progression/kanga.png"))
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

        //get cutom images for toggles. i'll recode this to be better later i swear
        private void CustomChecksCheck()
        {
            //TODO: this needs to go in init or somethin 
            Dictionary<Item, Tuple<string, string>> CusItemCheck = new Dictionary<Item, Tuple<string, string>>
            {
                {Report1, new Tuple<string, string>("CustomImages/Checks/ansem_report01.png", "Cus-AnsemReport01")},
                {Report2, new Tuple<string, string>("CustomImages/Checks/ansem_report02.png", "Cus-AnsemReport02")},
                {Report3, new Tuple<string, string>("CustomImages/Checks/ansem_report03.png", "Cus-AnsemReport03")},
                {Report4, new Tuple<string, string>("CustomImages/Checks/ansem_report04.png", "Cus-AnsemReport04")},
                {Report5, new Tuple<string, string>("CustomImages/Checks/ansem_report05.png", "Cus-AnsemReport05")},
                {Report6, new Tuple<string, string>("CustomImages/Checks/ansem_report06.png", "Cus-AnsemReport06")},
                {Report7, new Tuple<string, string>("CustomImages/Checks/ansem_report07.png", "Cus-AnsemReport07")},
                {Report8, new Tuple<string, string>("CustomImages/Checks/ansem_report08.png", "Cus-AnsemReport08")},
                {Report9, new Tuple<string, string>("CustomImages/Checks/ansem_report09.png", "Cus-AnsemReport09")},
                {Report10, new Tuple<string, string>("CustomImages/Checks/ansem_report10.png", "Cus-AnsemReport10")},
                {Report11, new Tuple<string, string>("CustomImages/Checks/ansem_report11.png", "Cus-AnsemReport11")},
                {Report12, new Tuple<string, string>("CustomImages/Checks/ansem_report12.png", "Cus-AnsemReport12")},
                {Report13, new Tuple<string, string>("CustomImages/Checks/ansem_report13.png", "Cus-AnsemReport13")},
                {Fire1, new Tuple<string, string>("CustomImages/Checks/fire.png", "Cus-Fire")},
                {Fire2, new Tuple<string, string>("CustomImages/Checks/fire.png", "Cus-Fire")},
                {Fire3, new Tuple<string, string>("CustomImages/Checks/fire.png", "Cus-Fire")},
                {Blizzard1, new Tuple<string, string>("CustomImages/Checks/blizzard.png", "Cus-Blizzard")},
                {Blizzard2, new Tuple<string, string>("CustomImages/Checks/blizzard.png", "Cus-Blizzard")},
                {Blizzard3, new Tuple<string, string>("CustomImages/Checks/blizzard.png", "Cus-Blizzard")},
                {Thunder1, new Tuple<string, string>("CustomImages/Checks/thunder.png", "Cus-Thunder")},
                {Thunder2, new Tuple<string, string>("CustomImages/Checks/thunder.png", "Cus-Thunder")},
                {Thunder3, new Tuple<string, string>("CustomImages/Checks/thunder.png", "Cus-Thunder")},
                {Cure1, new Tuple<string, string>("CustomImages/Checks/cure.png", "Cus-Cure")},
                {Cure2, new Tuple<string, string>("CustomImages/Checks/cure.png", "Cus-Cure")},
                {Cure3, new Tuple<string, string>("CustomImages/Checks/cure.png", "Cus-Cure")},
                {Reflect1, new Tuple<string, string>("CustomImages/Checks/reflect.png", "Cus-Reflect")},
                {Reflect2, new Tuple<string, string>("CustomImages/Checks/reflect.png", "Cus-Reflect")},
                {Reflect3, new Tuple<string, string>("CustomImages/Checks/reflect.png", "Cus-Reflect")},
                {Magnet1, new Tuple<string, string>("CustomImages/Checks/magnet.png", "Cus-Magnet")},
                {Magnet2, new Tuple<string, string>("CustomImages/Checks/magnet.png", "Cus-Magnet")},
                {Magnet3, new Tuple<string, string>("CustomImages/Checks/magnet.png", "Cus-Magnet")},
                {TornPage1, new Tuple<string, string>("CustomImages/Checks/torn_pages.png", "Cus-TornPage")},
                {TornPage2, new Tuple<string, string>("CustomImages/Checks/torn_pages.png", "Cus-TornPage")},
                {TornPage3, new Tuple<string, string>("CustomImages/Checks/torn_pages.png", "Cus-TornPage")},
                {TornPage4, new Tuple<string, string>("CustomImages/Checks/torn_pages.png", "Cus-TornPage")},
                {TornPage5, new Tuple<string, string>("CustomImages/Checks/torn_pages.png", "Cus-TornPage")},
                {Valor, new Tuple<string, string>("CustomImages/Checks/valor.png", "Cus-Valor")},
                {Wisdom, new Tuple<string, string>("CustomImages/Checks/wisdom.png", "Cus-Wisdom")},
                {Limit, new Tuple<string, string>("CustomImages/Checks/limit.png", "Cus-Limit")},
                {Master, new Tuple<string, string>("CustomImages/Checks/master.png", "Cus-Master")},
                {Final, new Tuple<string, string>("CustomImages/Checks/final.png", "Cus-Final")},
                {Lamp, new Tuple<string, string>("CustomImages/Checks/genie.png", "Cus-Genie")},
                {Ukulele, new Tuple<string, string>("CustomImages/Checks/stitch.png", "Cus-Stitch")},
                {Baseball, new Tuple<string, string>("CustomImages/Checks/chicken_little.png", "Cus-ChickenLittle")},
                {Feather, new Tuple<string, string>("CustomImages/Checks/peter_pan.png", "Cus-PeterPan")},
                {Nonexistence, new Tuple<string, string>("CustomImages/Checks/proof_of_nonexistence.png", "Cus-ProofOfNon")},
                {Connection, new Tuple<string, string>("CustomImages/Checks/proof_of_connection.png", "Cus-ProofOfCon")},
                {Peace, new Tuple<string, string>("CustomImages/Checks/proof_of_tranquility.png", "Cus-ProofOfPea")},
                {PromiseCharm, new Tuple<string, string>("CustomImages/Checks/promise_charm.png", "Cus-PromiseCharm")},
                {OnceMore, new Tuple<string, string>("CustomImages/Checks/once_more.png", "Cus-OnceMore")},
                {SecondChance, new Tuple<string, string>("CustomImages/Checks/second_chance.png", "Cus-SecondChance")},
                {MulanWep, new Tuple<string, string>("CustomImages/Checks/AncestorSword.png", "Cus-MulanWep")},
                {AuronWep, new Tuple<string, string>("CustomImages/Checks/BattlefieldsofWar.png", "Cus-AuronWep")},
                {BeastWep, new Tuple<string, string>("CustomImages/Checks/BeastClaw.png", "Cus-BeastWep")},
                {JackWep, new Tuple<string, string>("CustomImages/Checks/BoneFist.png", "Cus-JackWep")},
                {IceCream, new Tuple<string, string>("CustomImages/Checks/IceCream.png", "Cus-IceCream")},
                {TronWep, new Tuple<string, string>("CustomImages/Checks/IdentityDisk.png", "Cus-TronWep")},
                {Picture, new Tuple<string, string>("CustomImages/Checks/Picture.png", "Cus-Picture")},
                {MembershipCard, new Tuple<string, string>("CustomImages/Checks/membership_card.png", "Cus-MembershipCard")},
                {SimbaWep, new Tuple<string, string>("CustomImages/Checks/ProudFang.png", "Cus-SimbaWep")},
                {AladdinWep, new Tuple<string, string>("CustomImages/Checks/Scimitar.png", "Cus-AladdinWep")},
                {SparrowWep, new Tuple<string, string>("CustomImages/Checks/SkillCrossbones.png", "Cus-SparrowWep")},
                {HadesCup, new Tuple<string, string>("CustomImages/Checks/hades_cup.png", "Cus-HadesCup")},
                {OlympusStone, new Tuple<string, string>("CustomImages/Checks/olympus_stone.png", "Cus-OlympusStone")},
                {UnknownDisk, new Tuple<string, string>("CustomImages/Checks/UnknownDisk.png", "Cus-UnknownDisk")},
                {Anti, new Tuple<string, string>("CustomImages/Checks/anti.png", "Cus-Anti")},
                {Ghost_Report1, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report01.png", "Cus-G_AnsemReport01")},
                {Ghost_Report2, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report02.png", "Cus-G_AnsemReport02")},
                {Ghost_Report3, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report03.png", "Cus-G_AnsemReport03")},
                {Ghost_Report4, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report04.png", "Cus-G_AnsemReport04")},
                {Ghost_Report5, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report05.png", "Cus-G_AnsemReport05")},
                {Ghost_Report6, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report06.png", "Cus-G_AnsemReport06")},
                {Ghost_Report7, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report07.png", "Cus-G_AnsemReport07")},
                {Ghost_Report8, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report08.png", "Cus-G_AnsemReport08")},
                {Ghost_Report9, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report09.png", "Cus-G_AnsemReport09")},
                {Ghost_Report10, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report10.png", "Cus-G_AnsemReport10")},
                {Ghost_Report11, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report11.png", "Cus-G_AnsemReport11")},
                {Ghost_Report12, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report12.png", "Cus-G_AnsemReport12")},
                {Ghost_Report13, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report13.png", "Cus-G_AnsemReport13")},
                {Ghost_Fire1, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire")},
                {Ghost_Fire2, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire")},
                {Ghost_Fire3, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire")},
                {Ghost_Blizzard1, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard")},
                {Ghost_Blizzard2, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard")},
                {Ghost_Blizzard3, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard")},
                {Ghost_Thunder1, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder")},
                {Ghost_Thunder2, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder")},
                {Ghost_Thunder3, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder")},
                {Ghost_Cure1, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure")},
                {Ghost_Cure2, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure")},
                {Ghost_Cure3, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure")},
                {Ghost_Reflect1, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect")},
                {Ghost_Reflect2, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect")},
                {Ghost_Reflect3, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect")},
                {Ghost_Magnet1, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet")},
                {Ghost_Magnet2, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet")},
                {Ghost_Magnet3, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet")},
                {Ghost_TornPage1, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage")},
                {Ghost_TornPage2, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage")},
                {Ghost_TornPage3, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage")},
                {Ghost_TornPage4, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage")},
                {Ghost_TornPage5, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage")},
                {Ghost_Valor, new Tuple<string, string>("CustomImages/Checks/Ghost/valor.png", "Cus-G_Valor")},
                {Ghost_Wisdom, new Tuple<string, string>("CustomImages/Checks/Ghost/wisdom.png", "Cus-G_Wisdom")},
                {Ghost_Limit, new Tuple<string, string>("CustomImages/Checks/Ghost/limit.png", "Cus-G_Limit")},
                {Ghost_Master, new Tuple<string, string>("CustomImages/Checks/Ghost/master.png", "Cus-G_Master")},
                {Ghost_Final, new Tuple<string, string>("CustomImages/Checks/Ghost/final.png", "Cus-G_Final")},
                {Ghost_Lamp, new Tuple<string, string>("CustomImages/Checks/Ghost/genie.png", "Cus-G_Genie")},
                {Ghost_Ukulele, new Tuple<string, string>("CustomImages/Checks/Ghost/stitch.png", "Cus-G_Stitch")},
                {Ghost_Baseball, new Tuple<string, string>("CustomImages/Checks/Ghost/chicken_little.png", "Cus-G_ChickenLittle")},
                {Ghost_Feather, new Tuple<string, string>("CustomImages/Checks/Ghost/peter_pan.png", "Cus-G_PeterPan")},
                {Ghost_Nonexistence, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_nonexistence.png", "Cus-G_ProofOfNon")},
                {Ghost_Connection, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_connection.png", "Cus-G_ProofOfCon")},
                {Ghost_Peace, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_tranquility.png", "Cus-G_ProofOfPea")},
                {Ghost_PromiseCharm, new Tuple<string, string>("CustomImages/Checks/Ghost/promise_charm.png", "Cus-G_PromiseCharm")},
                {Ghost_OnceMore, new Tuple<string, string>("CustomImages/Checks/Ghost/once_more.png", "Cus-G_OnceMore")},
                {Ghost_SecondChance, new Tuple<string, string>("CustomImages/Checks/Ghost/second_chance.png", "Cus-G_SecondChance")},
                {Ghost_MulanWep, new Tuple<string, string>("CustomImages/Checks/Ghost/AncestorSword.png", "Cus-G_MulanWep")},
                {Ghost_AuronWep, new Tuple<string, string>("CustomImages/Checks/Ghost/BattlefieldsofWar.png", "Cus-G_AuronWep")},
                {Ghost_BeastWep, new Tuple<string, string>("CustomImages/Checks/Ghost/BeastClaw.png", "Cus-G_BeastWep")},
                {Ghost_JackWep, new Tuple<string, string>("CustomImages/Checks/Ghost/BoneFist.png", "Cus-G_JackWep")},
                {Ghost_IceCream, new Tuple<string, string>("CustomImages/Checks/Ghost/IceCream.png", "Cus-G_IceCream")},
                {Ghost_TronWep, new Tuple<string, string>("CustomImages/Checks/Ghost/IdentityDisk.png", "Cus-G_TronWep")},
                {Ghost_Picture, new Tuple<string, string>("CustomImages/Checks/Ghost/Picture.png", "Cus-G_Picture")},
                {Ghost_MembershipCard, new Tuple<string, string>("CustomImages/Checks/Ghost/membership_card.png", "Cus-G_MembershipCard")},
                {Ghost_SimbaWep, new Tuple<string, string>("CustomImages/Checks/Ghost/ProudFang.png", "Cus-G_SimbaWep")},
                {Ghost_AladdinWep, new Tuple<string, string>("CustomImages/Checks/Ghost/Scimitar.png", "Cus-G_AladdinWep")},
                {Ghost_SparrowWep, new Tuple<string, string>("CustomImages/Checks/Ghost/SkillCrossbones.png", "Cus-G_SparrowWep")},
                {Ghost_HadesCup, new Tuple<string, string>("CustomImages/Checks/Ghost/hades_cup.png", "Cus-G_HadesCup")},
                {Ghost_OlympusStone, new Tuple<string, string>("CustomImages/Checks/Ghost/olympus_stone.png", "Cus-G_OlympusStone")},
                {Ghost_UnknownDisk, new Tuple<string, string>("CustomImages/Checks/Ghost/UnknownDisk.png", "Cus-G_UnknownDisk")},
                {Ghost_Anti, new Tuple<string, string>("CustomImages/Checks/Ghost/anti.png", "Cus-G_Anti")}
            };

            string[] files = Directory.GetFiles("CustomImages/", "*.png", SearchOption.AllDirectories);
            //check if i actually need this lowercase edit
            files = files.Select(s => s.ToLowerInvariant()).ToArray();

            //if list isn't empty then compare against dictionary to determine what icons to replace
            if (files.Length > 0)
            {
                foreach (var item in CusItemCheck)
                {
                    if (files.Contains(item.Value.Item1.ToLower()))
                    {
                        item.Key.SetResourceReference(ContentProperty, item.Value.Item2);
                    }
                }
            }



            if (CustomFolderOption.IsChecked)
            {
                //check if folders exists then start checking if each file exists in it
                if (Directory.Exists("CustomImages/Checks/"))
                {
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

                    if (File.Exists("CustomImages/Checks/valor.png"))
                    {
                        ValorM.SetResourceReference(ContentProperty, "Cus-Valor");
                        broadcast.Valor.SetResourceReference(ContentProperty, "Cus-Valor");
                    }
                    if (File.Exists("CustomImages/Checks/wisdom.png"))
                    {
                        WisdomM.SetResourceReference(ContentProperty, "Cus-Wisdom");
                        broadcast.Wisdom.SetResourceReference(ContentProperty, "Cus-Wisdom");
                    }
                    if (File.Exists("CustomImages/Checks/limit.png"))
                    {
                        LimitM.SetResourceReference(ContentProperty, "Cus-Limit");
                        broadcast.Limit.SetResourceReference(ContentProperty, "Cus-Limit");
                    }
                    if (File.Exists("CustomImages/Checks/master.png"))
                    {
                        MasterM.SetResourceReference(ContentProperty, "Cus-Master");
                        broadcast.Master.SetResourceReference(ContentProperty, "Cus-Master");
                    }
                    if (File.Exists("CustomImages/Checks/final.png"))
                    {
                        FinalM.SetResourceReference(ContentProperty, "Cus-Final");
                        broadcast.Final.SetResourceReference(ContentProperty, "Cus-Final");
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

                    if (File.Exists("CustomImages/Broadcast/Checks/hades_cup.png"))
                    {
                        broadcast.HadesCup.SetResourceReference(ContentProperty, "Cus-B_HadesCup");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/olympus_stone.png"))
                    {
                        broadcast.OlympusStone.SetResourceReference(ContentProperty, "Cus-B_OlympusStone");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/UnknownDisk.png"))
                    {
                        broadcast.UnknownDisk.SetResourceReference(ContentProperty, "Cus-B_UnknownDisk");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/anti.png"))
                    {
                        broadcast.Anti.SetResourceReference(ContentProperty, "Cus-B_Anti");
                    }
                }
            }
        }

        private void CustomWorldCheck()
        {
            if (MinWorldOption.IsChecked)
            {

                HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                broadcast.HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                
                OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                

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
 
                HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                broadcast.HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");

                OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                

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
                            //DisneyCastleLW.SetResourceReference(ContentProperty, "Cus-DisneyCastleLW");
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
                        if (File.Exists("CustomImages/Worlds/hollow_bastion.png"))
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                        }

                        if (File.Exists("CustomImages/Worlds/olympus_coliseum.png"))
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
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
                        if (File.Exists("CustomImages/Broadcast/Worlds/hollow_bastion.png"))
                        {
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-B_HollowBastionImage");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum.png"))
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusImage");
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

        ///TODO: edit all to void and use it to set up the image chages here instead of toggles.cs
        public void SetItemImage()
        {
            LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
            StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
            MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
            DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");

            string type = "";
            if (MinCheckOption.IsChecked)
            {
                type = "Min-";
            }
            if (OldCheckOption.IsChecked)
            {
                type = "Old-";
            }

            // Item icons
            foreach (var item in data.Items.Keys)
            {
                data.Items[item].Item1.SetResourceReference(ContentProperty, type + item);
                //item.SetResourceReference(ContentProperty, type + item.Name);
            }
            // Ghost icons
            foreach (var item in data.GhostItems.Values)
            {
                item.SetResourceReference(ContentProperty, type + item.Name);
            }

            // stat/info icons
            ValorM.SetResourceReference(ContentProperty, type + "Valor");
            WisdomM.SetResourceReference(ContentProperty, type + "Wisdom");
            LimitM.SetResourceReference(ContentProperty, type + "Limit");
            MasterM.SetResourceReference(ContentProperty, type + "Master");
            FinalM.SetResourceReference(ContentProperty, type + "Final");
            HighJump.SetResourceReference(ContentProperty, type + "HighJump");
            QuickRun.SetResourceReference(ContentProperty, type + "QuickRun");
            DodgeRoll.SetResourceReference(ContentProperty, type + "DodgeRoll");
            AerialDodge.SetResourceReference(ContentProperty, type + "AerialDodge");
            Glide.SetResourceReference(ContentProperty, type + "Glide");

            // broadcast icons
            broadcast.Report.SetResourceReference(ContentProperty, type + "AnsemReport");
            broadcast.TornPage.SetResourceReference(ContentProperty, type + "TornPages");
            broadcast.Chest.SetResourceReference(ContentProperty, type + "Chest");
            broadcast.LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
            broadcast.StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
            broadcast.MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
            broadcast.DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");
            broadcast.HighJump.SetResourceReference(ContentProperty, type + "HighJump");
            broadcast.QuickRun.SetResourceReference(ContentProperty, type + "QuickRun");
            broadcast.DodgeRoll.SetResourceReference(ContentProperty, type + "DodgeRoll");
            broadcast.AerialDodge.SetResourceReference(ContentProperty, type + "AerialDodge");
            broadcast.Glide.SetResourceReference(ContentProperty, type + "Glide");
            broadcast.OnceMore.SetResourceReference(ContentProperty, type + "OnceMore");
            broadcast.SecondChance.SetResourceReference(ContentProperty, type + "SecondChance");
            broadcast.Peace.SetResourceReference(ContentProperty, type + "ProofOfPea");
            broadcast.Nonexistence.SetResourceReference(ContentProperty, type + "ProofOfNon");
            broadcast.Connection.SetResourceReference(ContentProperty, type + "ProofOfCon");
            broadcast.PromiseCharm.SetResourceReference(ContentProperty, type + "PromiseCharm");
            broadcast.Fire.SetResourceReference(ContentProperty, type + "Fire");
            broadcast.Blizzard.SetResourceReference(ContentProperty, type + "Blizzard");
            broadcast.Thunder.SetResourceReference(ContentProperty, type + "Thunder");
            broadcast.Cure.SetResourceReference(ContentProperty, type + "Cure");
            broadcast.Reflect.SetResourceReference(ContentProperty, type + "Reflect");
            broadcast.Magnet.SetResourceReference(ContentProperty, type + "Magnet");
            broadcast.Valor.SetResourceReference(ContentProperty, type + "Valor");
            broadcast.Wisdom.SetResourceReference(ContentProperty, type + "Wisdom");
            broadcast.Limit.SetResourceReference(ContentProperty, type + "Limit");
            broadcast.Master.SetResourceReference(ContentProperty, type + "Master");
            broadcast.Final.SetResourceReference(ContentProperty, type + "Final");
            broadcast.Baseball.SetResourceReference(ContentProperty, type + "ChickenLittle");
            broadcast.Lamp.SetResourceReference(ContentProperty, type + "Genie");
            broadcast.Ukulele.SetResourceReference(ContentProperty, type + "Stitch");
            broadcast.Feather.SetResourceReference(ContentProperty, type + "PeterPan");
            broadcast.MulanWep.SetResourceReference(ContentProperty, type + "MulanWep");
            broadcast.AuronWep.SetResourceReference(ContentProperty, type + "AuronWep");
            broadcast.BeastWep.SetResourceReference(ContentProperty, type + "BeastWep");
            broadcast.JackWep.SetResourceReference(ContentProperty, type + "JackWep");
            broadcast.IceCream.SetResourceReference(ContentProperty, type + "IceCream");
            broadcast.TronWep.SetResourceReference(ContentProperty, type + "TronWep");
            broadcast.Picture.SetResourceReference(ContentProperty, type + "Picture");
            broadcast.MembershipCard.SetResourceReference(ContentProperty, type + "MembershipCard");
            broadcast.SimbaWep.SetResourceReference(ContentProperty, type + "SimbaWep");
            broadcast.AladdinWep.SetResourceReference(ContentProperty, type + "AladdinWep");
            broadcast.SparrowWep.SetResourceReference(ContentProperty, type + "SparrowWep");
            broadcast.HadesCup.SetResourceReference(ContentProperty, type + "HadesCup");
            broadcast.OlympusStone.SetResourceReference(ContentProperty, type + "OlympusStone");
            broadcast.UnknownDisk.SetResourceReference(ContentProperty, type + "UnknownDisk");
            broadcast.Anti.SetResourceReference(ContentProperty, type + "Anti");




            //CustomChecksCheck();
            //ReloadBindings();
        }

        public void SetWorldImage()
        {

            string type = "";
            if (MinWorldOption.IsChecked)
            {
                type = "Min-";
            }
            if (OldWorldOption.IsChecked)
            {
                type = "Old-";
            }

            //main window worlds
            SorasHeart.SetResourceReference(ContentProperty, type + "SoraHeartImage");
            SimulatedTwilightTown.SetResourceReference(ContentProperty, type + "SimulatedImage");
            OlympusColiseum.SetResourceReference(ContentProperty, type + "OlympusImage");
            LandofDragons.SetResourceReference(ContentProperty, type + "LandofDragonsImage");
            PrideLands.SetResourceReference(ContentProperty, type + "PrideLandsImage");
            HalloweenTown.SetResourceReference(ContentProperty, type + "HalloweenTownImage");
            SpaceParanoids.SetResourceReference(ContentProperty, type + "SpaceParanoidsImage");
            GoA.SetResourceReference(ContentProperty, type + "GardenofAssemblageImage");
            DriveForms.SetResourceReference(ContentProperty, type + "DriveFormsImage");
            TwilightTown.SetResourceReference(ContentProperty, type + "TwilightTownImage");
            BeastsCastle.SetResourceReference(ContentProperty, type + "BeastCastleImage");
            Agrabah.SetResourceReference(ContentProperty, type + "AgrabahImage");
            HundredAcreWood.SetResourceReference(ContentProperty, type + "HundredAcreImage");
            PortRoyal.SetResourceReference(ContentProperty, type + "PortRoyalImage");
            TWTNW.SetResourceReference(ContentProperty, type + "TWTNWImage");
            Atlantica.SetResourceReference(ContentProperty, type + "AtlanticaImage");
            DisneyCastle.SetResourceReference(ContentProperty, type + "DisneyCastleImage");

            //broadcast window worlds
            broadcast.SorasHeart.SetResourceReference(ContentProperty, type + "SoraHeartImage");
            broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, type + "SimulatedImage");
            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, type + "OlympusImage");
            broadcast.LandofDragons.SetResourceReference(ContentProperty, type + "LandofDragonsImage");
            broadcast.PrideLands.SetResourceReference(ContentProperty, type + "PrideLandsImage");
            broadcast.HalloweenTown.SetResourceReference(ContentProperty, type + "HalloweenTownImage");
            broadcast.SpaceParanoids.SetResourceReference(ContentProperty, type + "SpaceParanoidsImage");
            broadcast.DriveForms.SetResourceReference(ContentProperty, type + "DriveFormsImage");
            broadcast.TwilightTown.SetResourceReference(ContentProperty, type + "TwilightTownImage");
            broadcast.BeastsCastle.SetResourceReference(ContentProperty, type + "BeastCastleImage");
            broadcast.Agrabah.SetResourceReference(ContentProperty, type + "AgrabahImage");
            broadcast.HundredAcreWood.SetResourceReference(ContentProperty, type + "HundredAcreImage");
            broadcast.PortRoyal.SetResourceReference(ContentProperty, type + "PortRoyalImage");
            broadcast.TWTNW.SetResourceReference(ContentProperty, type + "TWTNWImage");
            broadcast.Atlantica.SetResourceReference(ContentProperty, type + "AtlanticaImage");
            broadcast.DisneyCastle.SetResourceReference(ContentProperty, type + "DisneyCastleImage");

            //puzzle/synth display
            if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth");
                broadcast.PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth");
            }
            if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_S");
                broadcast.PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_S");
            }
            if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_P");
                broadcast.PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_P");
            }

            //CustomWorldCheck();
        }

    }
}
