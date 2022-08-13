using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

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
        //main window, image path, dictionary key, ghost, broadcast
        private Dictionary<Item, Tuple<string, string, Item, ContentControl>> CusItemCheck;
        private Dictionary<Item, Tuple<string, string>> CusItemCheckG;
        //dirty i guess, but i'll fix later maybe
        private Dictionary<ContentControl, string> ItemShadow;

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

            ///NOTE: do i need a string defines for the reference name?
            ///isn't it always the same as the CC name anyway? 
            ///maybe i can use that to simplify things even more later

            //helps determine what item images need replacing with custom image loading
            CusItemCheck = new Dictionary<Item, Tuple<string, string, Item, ContentControl>>
            {
                {Report1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report01.png", "Cus-Report1", Ghost_Report1, null)},
                {Report2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report02.png", "Cus-Report2", Ghost_Report2, null)},
                {Report3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report03.png", "Cus-Report3", Ghost_Report3, null)},
                {Report4, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report04.png", "Cus-Report4", Ghost_Report4, null)},
                {Report5, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report05.png", "Cus-Report5", Ghost_Report5, null)},
                {Report6, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report06.png", "Cus-Report6", Ghost_Report6, null)},
                {Report7, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report07.png", "Cus-Report7", Ghost_Report7, null)},
                {Report8, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report08.png", "Cus-Report8", Ghost_Report8, null)},
                {Report9, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report09.png", "Cus-Report9", Ghost_Report9, null)},
                {Report10, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report10.png", "Cus-Report10", Ghost_Report10, null)},
                {Report11, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report11.png", "Cus-Report11", Ghost_Report11, null)},
                {Report12, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report12.png", "Cus-Report12", Ghost_Report12, null)},
                {Report13, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ansem_report13.png", "Cus-Report13", Ghost_Report13, null)},
                {Fire1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/fire.png", "Cus-Fire1", Ghost_Fire1, null)},
                {Fire2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/fire.png", "Cus-Fire2", Ghost_Fire2, null)},
                {Fire3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/fire.png", "Cus-Fire3", Ghost_Fire3, null)},
                {Blizzard1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/blizzard.png", "Cus-Blizzard1", Ghost_Blizzard1, null)},
                {Blizzard2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/blizzard.png", "Cus-Blizzard2", Ghost_Blizzard2, null)},
                {Blizzard3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/blizzard.png", "Cus-Blizzard3", Ghost_Blizzard3, null)},
                {Thunder1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/thunder.png", "Cus-Thunder1", Ghost_Thunder1, null)},
                {Thunder2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/thunder.png", "Cus-Thunder2", Ghost_Thunder2, null)},
                {Thunder3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/thunder.png", "Cus-Thunder3", Ghost_Thunder3, null)},
                {Cure1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/cure.png", "Cus-Cure1", Ghost_Cure1, null)},
                {Cure2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/cure.png", "Cus-Cure2", Ghost_Cure2, null)},
                {Cure3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/cure.png", "Cus-Cure3", Ghost_Cure3, null)},
                {Reflect1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/reflect.png", "Cus-Reflect1", Ghost_Reflect1, null)},
                {Reflect2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/reflect.png", "Cus-Reflect2", Ghost_Reflect2, null)},
                {Reflect3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/reflect.png", "Cus-Reflect3", Ghost_Reflect3, null)},
                {Magnet1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/magnet.png", "Cus-Magnet1", Ghost_Magnet1, null)},
                {Magnet2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/magnet.png", "Cus-Magnet2", Ghost_Magnet2, null)},
                {Magnet3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/magnet.png", "Cus-Magnet3", Ghost_Magnet3, null)},
                {TornPage1, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/torn_pages.png", "Cus-TornPage1", Ghost_TornPage1, null)},
                {TornPage2, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/torn_pages.png", "Cus-TornPage2", Ghost_TornPage2, null)},
                {TornPage3, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/torn_pages.png", "Cus-TornPage3", Ghost_TornPage3, null)},
                {TornPage4, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/torn_pages.png", "Cus-TornPage4", Ghost_TornPage4, null)},
                {TornPage5, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/torn_pages.png", "Cus-TornPage5", Ghost_TornPage5, null)},
                {Valor, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/valor.png", "Cus-Valor", Ghost_Valor, null)},
                {Wisdom, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/wisdom.png", "Cus-Wisdom", Ghost_Wisdom, null)},
                {Limit, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/limit.png", "Cus-Limit", Ghost_Limit, null)},
                {Master, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/master.png", "Cus-Master", Ghost_Master, null)},
                {Final, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/final.png", "Cus-Final", Ghost_Final, null)},
                {Lamp, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/genie.png", "Cus-Lamp", Ghost_Lamp, null)},
                {Ukulele, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/stitch.png", "Cus-Ukulele", Ghost_Ukulele, null)},
                {Baseball, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/chicken_little.png", "Cus-Baseball", Ghost_Baseball, null)},
                {Feather, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/peter_pan.png", "Cus-Feather", Ghost_Feather, null)},
                {Nonexistence, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/proof_of_nonexistence.png", "Cus-Nonexistence", Ghost_Nonexistence, null)},
                {Connection, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/proof_of_connection.png", "Cus-Connection", Ghost_Connection, null)},
                {Peace, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/proof_of_tranquility.png", "Cus-Peace", Ghost_Peace, null)},
                {PromiseCharm, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/promise_charm.png", "Cus-PromiseCharm", Ghost_PromiseCharm, null)},
                {OnceMore, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/once_more.png", "Cus-OnceMore", Ghost_OnceMore, null)},
                {SecondChance, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/second_chance.png", "Cus-SecondChance", Ghost_SecondChance, null)},
                {MulanWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/AncestorSword.png", "Cus-MulanWep", Ghost_MulanWep, null)},
                {AuronWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/BattlefieldsofWar.png", "Cus-AuronWep", Ghost_AuronWep, null)},
                {BeastWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/BeastClaw.png", "Cus-BeastWep", Ghost_BeastWep, null)},
                {JackWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/BoneFist.png", "Cus-JackWep", Ghost_JackWep, null)},
                {IceCream, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/IceCream.png", "Cus-IceCream", Ghost_IceCream, null)},
                {TronWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/IdentityDisk.png", "Cus-TronWep", Ghost_TronWep, null)},
                {Picture, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/Picture.png", "Cus-Picture", Ghost_Picture, null)},
                {MembershipCard, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/membership_card.png", "Cus-MembershipCard", Ghost_MembershipCard, null)},
                {SimbaWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/ProudFang.png", "Cus-SimbaWep", Ghost_SimbaWep, null)},
                {AladdinWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/Scimitar.png", "Cus-AladdinWep", Ghost_AladdinWep, null)},
                {SparrowWep, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/SkillCrossbones.png", "Cus-SparrowWep", Ghost_SparrowWep, null)},
                {HadesCup, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/hades_cup.png", "Cus-HadesCup", Ghost_HadesCup, null)},
                {OlympusStone, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/olympus_stone.png", "Cus-OlympusStone", Ghost_OlympusStone, null)},
                {UnknownDisk, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/UnknownDisk.png", "Cus-UnknownDisk", Ghost_UnknownDisk, null)},
                {Anti, new Tuple<string, string, Item, ContentControl>("CustomImages/Checks/anti.png", "Cus-Anti", Ghost_Anti, null)}
            };

            //ghost items
            CusItemCheckG = new Dictionary<Item, Tuple<string, string>>
            {
                {Ghost_Report1, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report01.png",  "Cus-G_Report1")},
                {Ghost_Report2, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report02.png",  "Cus-G_Report2")},
                {Ghost_Report3, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report03.png",  "Cus-G_Report3")},
                {Ghost_Report4, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report04.png",  "Cus-G_Report4")},
                {Ghost_Report5, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report05.png",  "Cus-G_Report5")},
                {Ghost_Report6, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report06.png",  "Cus-G_Report6")},
                {Ghost_Report7, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report07.png",  "Cus-G_Report7")},
                {Ghost_Report8, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report08.png",  "Cus-G_Report8")},
                {Ghost_Report9, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report09.png",  "Cus-G_Report9")},
                {Ghost_Report10, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report10.png", "Cus-G_Report10")},
                {Ghost_Report11, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report11.png", "Cus-G_Report11")},
                {Ghost_Report12, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report12.png", "Cus-G_Report12")},
                {Ghost_Report13, new Tuple<string, string>("CustomImages/Checks/Ghost/ansem_report13.png", "Cus-G_Report13")},
                {Ghost_Fire1, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire1")},
                {Ghost_Fire2, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire2")},
                {Ghost_Fire3, new Tuple<string, string>("CustomImages/Checks/Ghost/fire.png", "Cus-G_Fire3")},
                {Ghost_Blizzard1, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard1")},
                {Ghost_Blizzard2, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard2")},
                {Ghost_Blizzard3, new Tuple<string, string>("CustomImages/Checks/Ghost/blizzard.png", "Cus-G_Blizzard3")},
                {Ghost_Thunder1, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder1")},
                {Ghost_Thunder2, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder2")},
                {Ghost_Thunder3, new Tuple<string, string>("CustomImages/Checks/Ghost/thunder.png", "Cus-G_Thunder3")},
                {Ghost_Cure1, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure1")},
                {Ghost_Cure2, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure2")},
                {Ghost_Cure3, new Tuple<string, string>("CustomImages/Checks/Ghost/cure.png", "Cus-G_Cure3")},
                {Ghost_Reflect1, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect1")},
                {Ghost_Reflect2, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect2")},
                {Ghost_Reflect3, new Tuple<string, string>("CustomImages/Checks/Ghost/reflect.png", "Cus-G_Reflect3")},
                {Ghost_Magnet1, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet1")},
                {Ghost_Magnet2, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet2")},
                {Ghost_Magnet3, new Tuple<string, string>("CustomImages/Checks/Ghost/magnet.png", "Cus-G_Magnet3")},
                {Ghost_TornPage1, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage1")},
                {Ghost_TornPage2, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage2")},
                {Ghost_TornPage3, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage3")},
                {Ghost_TornPage4, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage4")},
                {Ghost_TornPage5, new Tuple<string, string>("CustomImages/Checks/Ghost/torn_pages.png", "Cus-G_TornPage5")},
                {Ghost_Valor, new Tuple<string, string>("CustomImages/Checks/Ghost/valor.png", "Cus-G_Valor")},
                {Ghost_Wisdom, new Tuple<string, string>("CustomImages/Checks/Ghost/wisdom.png", "Cus-G_Wisdom")},
                {Ghost_Limit, new Tuple<string, string>("CustomImages/Checks/Ghost/limit.png", "Cus-G_Limit")},
                {Ghost_Master, new Tuple<string, string>("CustomImages/Checks/Ghost/master.png", "Cus-G_Master")},
                {Ghost_Final, new Tuple<string, string>("CustomImages/Checks/Ghost/final.png", "Cus-G_Final")},
                {Ghost_Lamp, new Tuple<string, string>("CustomImages/Checks/Ghost/genie.png",                           "Cus-G_Lamp"        )},
                {Ghost_Ukulele, new Tuple<string, string>("CustomImages/Checks/Ghost/stitch.png",                       "Cus-G_Ukulele"	)},
                {Ghost_Baseball, new Tuple<string, string>("CustomImages/Checks/Ghost/chicken_little.png",              "Cus-G_Baseball"	 )},
                {Ghost_Feather, new Tuple<string, string>("CustomImages/Checks/Ghost/peter_pan.png",                    "Cus-G_Feather"	)},
                {Ghost_Nonexistence, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_nonexistence.png",   "Cus-G_Nonexistence")},
                {Ghost_Connection, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_connection.png",       "Cus-G_Connection"	 )},
                {Ghost_Peace, new Tuple<string, string>("CustomImages/Checks/Ghost/proof_of_tranquility.png",           "Cus-G_Peace"		 )},
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

            ///TODO: update later
            ItemShadow = new Dictionary<ContentControl, string>
            {
                {S_Report1,        "Report1"},
                {S_Report2,        "Report2"},
                {S_Report3,        "Report3"},
                {S_Report4,        "Report4"},
                {S_Report5,        "Report5"},
                {S_Report6,        "Report6"},
                {S_Report7,        "Report7"},
                {S_Report8,        "Report8"},
                {S_Report9,        "Report9"},
                {S_Report10,       "Report10"},
                {S_Report11,       "Report11"},
                {S_Report12,       "Report12"},
                {S_Report13,       "Report13"},
                {S_Fire,           "Fire1"},
                {S_Blizzard,       "Blizzard1"},
                {S_Thunder,        "Thunder1"},
                {S_Cure,           "Cure1"},
                {S_Reflect,        "Reflect1"},
                {S_Magnet,         "Magnet1"},
                {S_TornPage,       "TornPage1"},
                {S_Valor,          "Valor"},
                {S_Wisdom,         "Wisdom"},
                {S_Limit,          "Limit"},
                {S_Master,         "Master"},
                {S_Final,          "Final"},
                {S_Lamp,           "Lamp"},
                {S_Ukulele,        "Ukulele"},
                {S_Baseball,       "Baseball"},
                {S_Feather,        "Feather"},
                {S_Nonexistence,   "Nonexistence"},
                {S_Connection,     "Connection"},
                {S_Peace,          "Peace"},
                {S_PromiseCharm,   "PromiseCharm"},
                {S_OnceMore,       "OnceMore"},
                {S_SecondChance,   "SecondChance"},
                {S_MulanWep,       "MulanWep"},
                {S_AuronWep,       "AuronWep"},
                {S_BeastWep,       "BeastWep"},
                {S_JackWep,        "JackWep"},
                {S_IceCream,       "IceCream"},
                {S_TronWep,        "TronWep"},
                {S_Picture,        "Picture"},
                {S_MembershipCard, "MembershipCard"},
                {S_SimbaWep,       "SimbaWep"},
                {S_AladdinWep,     "AladdinWep"},
                {S_SparrowWep,     "SparrowWep"},
                {S_HadesCup,       "HadesCup"},
                {S_OlympusStone,   "OlympusStone"},
                {S_UnknownDisk,    "UnknownDisk"},
                {S_Anti,           "Anti"} 
            };
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

        private void CustomChecksCheck()
        {
            if (!CustomFolderOption.IsChecked)
                return;

            string[] checkFiles = { };
            string[] checkFilesG = { };

            if (Directory.Exists("CustomImages/Checks/"))
            {
                checkFiles = Directory.GetFiles("CustomImages/Checks/", "*.png", SearchOption.TopDirectoryOnly);
            }

            if (Directory.Exists("CustomImages/Checks/Ghost/"))
            {
                checkFilesG = Directory.GetFiles("CustomImages/Checks/Ghost/", "*.png", SearchOption.TopDirectoryOnly);
            }


            //if list isn't empty then compare against dictionary to determine what icons to replace

            //    key     |   item1    |      item2     | item3  |  item4
            //main window | image path | dictionary key | ghost  | null

            // ghost should always use main widnow icons if any are found first.
            if (checkFiles.Length > 0)
            {
                //check if i actually need this lowercase edit
                checkFiles = checkFiles.Select(s => s.ToLowerInvariant()).ToArray();

                foreach (var item in CusItemCheck)
                {
                    if (checkFiles.Contains(item.Value.Item1.ToLower()))
                    {
                        //main item
                        Item main = item.Key;
                        main.SetResourceReference(ContentProperty, item.Value.Item2);

                        //ghost item
                        if (item.Value.Item3 != null)
                        {
                            Item ghost = item.Value.Item3;
                            ghost.SetResourceReference(ContentProperty, item.Value.Item2);
                        }
                    }
                }
            }

            //if custom ghost icons are found then set those (otherwise keep using the main window ones)
            if (checkFilesG.Length > 0)
            {
                //check if i actually need this lowercase edit
                checkFilesG = checkFilesG.Select(s => s.ToLowerInvariant()).ToArray();

                foreach (var item in CusItemCheckG)
                {
                    if (checkFilesG.Contains(item.Value.Item1.ToLower()))
                    {
                        //main item
                        Item ghost = item.Key as Item;
                        ghost.SetResourceReference(ContentProperty, item.Value.Item2);
                    }
                }
            }

            //check if folders exists then start checking if each file exists in it
            if (Directory.Exists("CustomImages/Checks/"))
            {
                if (File.Exists("CustomImages/Checks/jump.png"))
                    HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                if (File.Exists("CustomImages/Checks/quick.png"))
                    QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                if (File.Exists("CustomImages/Checks/dodge.png"))
                    DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                if (File.Exists("CustomImages/Checks/aerial.png"))
                    AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                if (File.Exists("CustomImages/Checks/glide.png"))
                    Glide.SetResourceReference(ContentProperty, "Cus-Glide");

                if (File.Exists("CustomImages/Checks/valor.png"))
                    ValorM.SetResourceReference(ContentProperty, "Cus-Valor");
                if (File.Exists("CustomImages/Checks/wisdom.png"))
                    WisdomM.SetResourceReference(ContentProperty, "Cus-Wisdom");
                if (File.Exists("CustomImages/Checks/limit.png"))
                    LimitM.SetResourceReference(ContentProperty, "Cus-Limit");
                if (File.Exists("CustomImages/Checks/master.png"))
                    MasterM.SetResourceReference(ContentProperty, "Cus-Master");
                if (File.Exists("CustomImages/Checks/final.png"))
                    FinalM.SetResourceReference(ContentProperty, "Cus-Final");
            }

            if (CustomLevelFound)
                LevelIcon.SetResourceReference(ContentProperty, "Cus-LevelIcon");

            if (CustomStrengthFound)
                StrengthIcon.SetResourceReference(ContentProperty, "Cus-StrengthIcon");

            if (CustomMagicFound)
                MagicIcon.SetResourceReference(ContentProperty, "Cus-MagicIcon");

            if (CustomDefenseFound)
                DefenseIcon.SetResourceReference(ContentProperty, "Cus-DefenseIcon");

            ///TODO: needs to be redone for new lock images
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
            }
            if (File.Exists("CustomImages/Other/visitlocksilver.png"))
                TwilightTownLock_1.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/visitlocksilver.png", UriKind.Absolute));

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
            }

            //DeathCounter counter skull
            if (File.Exists("CustomImages/Other/death.png"))
                Skull.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Other/death.png", UriKind.Absolute));
        }

        private void CustomWorldCheck()
        {
            if (MinWorldOption.IsChecked)
            {
                HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                
                //puzzle/synth display
                if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth");
                if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_S");
                if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                    PuzzSynth.SetResourceReference(ContentProperty, "Min-PuzzSynth_P");
            }
            
            if (OldWorldOption.IsChecked)
            {
                HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");         
            
                //puzzle/synth display
                if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth");
                if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_S");
                if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                    PuzzSynth.SetResourceReference(ContentProperty, "Old-PuzzSynth_P");
            }

            if (CustomFolderOption.IsChecked)
            {
                ///TODO: update and maybe set up dictionary?
                //Main Window
                if (Directory.Exists("CustomImages/Worlds/"))
                {
                    if (File.Exists("CustomImages/Worlds/simulated_twilight_town.png"))
                    {
                        SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-SimulatedImage");
                    }
                    if (File.Exists("CustomImages/Worlds/land_of_dragons.png"))
                    {
                        LandofDragons.SetResourceReference(ContentProperty, "Cus-LandofDragonsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/pride_land.png"))
                    {
                        PrideLands.SetResourceReference(ContentProperty, "Cus-PrideLandsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/halloween_town.png"))
                    {
                        HalloweenTown.SetResourceReference(ContentProperty, "Cus-HalloweenTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/space_paranoids.png"))
                    {
                        SpaceParanoids.SetResourceReference(ContentProperty, "Cus-SpaceParanoidsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/drive_form.png"))
                    {
                        DriveForms.SetResourceReference(ContentProperty, "Cus-DriveFormsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/twilight_town.png"))
                    {
                        TwilightTown.SetResourceReference(ContentProperty, "Cus-TwilightTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/beast's_castle.png"))
                    {
                        BeastsCastle.SetResourceReference(ContentProperty, "Cus-BeastCastleImage");
                    }
                    if (File.Exists("CustomImages/Worlds/agrabah.png"))
                    {
                        Agrabah.SetResourceReference(ContentProperty, "Cus-AgrabahImage");
                    }
                    if (File.Exists("CustomImages/Worlds/100_acre_wood.png"))
                    {
                        HundredAcreWood.SetResourceReference(ContentProperty, "Cus-HundredAcreImage");
                    }
                    if (File.Exists("CustomImages/Worlds/port_royal.png"))
                    {
                        PortRoyal.SetResourceReference(ContentProperty, "Cus-PortRoyalImage");
                    }
                    if (File.Exists("CustomImages/Worlds/the_world_that_never_was.png"))
                    {
                        TWTNW.SetResourceReference(ContentProperty, "Cus-TWTNWImage");
                    }
                    if (File.Exists("CustomImages/Worlds/atlantica.png"))
                    {
                        Atlantica.SetResourceReference(ContentProperty, "Cus-AtlanticaImage");
                    }
                    if (File.Exists("CustomImages/Worlds/replica_data.png"))
                    {
                        GoA.SetResourceReference(ContentProperty, "Cus-GardenofAssemblageImage");
                    }
                    if (File.Exists("CustomImages/Worlds/level.png"))
                    {
                        SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Worlds/disney_castle.png"))
                    {
                        DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        if (File.Exists("CustomImages/Worlds/Level01.png") && SoraLevel01Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel01");
                        }
                        if (File.Exists("CustomImages/Worlds/Level50.png") && SoraLevel50Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel50");
                        }
                        if (File.Exists("CustomImages/Worlds/Level99.png") && SoraLevel99Option.IsChecked)
                        {
                            SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel99");
                        }

                        if (File.Exists("CustomImages/Worlds/hollow_bastion.png"))
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                        }

                        if (File.Exists("CustomImages/Worlds/olympus_coliseum.png"))
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                        }

                        //puzzle/synth display
                        if (File.Exists("CustomImages/Worlds/PuzzSynth.png") && PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth");
                        }
                        if (File.Exists("CustomImages/Worlds/Synth.png") && !PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_S");
                        }
                        else if (File.Exists("CustomImages/Worlds/Puzzle.png") && PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                        {
                            PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_P");
                        }
                    }
                }
            }
        }

        ///TODO: test and make sure this works
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
            }
            
            // Ghost icons
            foreach (var item in data.GhostItems.Values)
            {
                item.SetResourceReference(ContentProperty, type + item.Name.Remove(0, 6));
            }

            //item shadows
            foreach (ContentControl item in ItemShadow.Keys)
            {
                item.SetResourceReference(ContentProperty, type + ItemShadow[item]);
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

            //CustomChecksCheck();
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

            //puzzle/synth display
            if (PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth");
            }
            if (!PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_S");
            }
            if (PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
            {
                PuzzSynth.SetResourceReference(ContentProperty, type + "PuzzSynth_P");
            }

            //CustomWorldCheck();
        }
    }
}
