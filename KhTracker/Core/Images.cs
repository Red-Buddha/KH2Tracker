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
        //item | dictionary key | ghost | shadow | path
        private Dictionary<Item, Tuple<string, Item, ContentControl, string>> CusItemCheck;
        private Dictionary<Item, Tuple<string, string>> CusItemCheckG;

        //handle adding all custom images and such
        public void InitImages()
        {
            //for autodetect (won't bother making this customizable for now)
            data.AD_Connect =   new BitmapImage(new Uri("Images/System/config/searching.png", UriKind.Relative));
            data.AD_PC =        new BitmapImage(new Uri("Images/System/config/pc_connected.png", UriKind.Relative));
            data.AD_PCred =     new BitmapImage(new Uri("Images/System/config/pc_detected.png", UriKind.Relative));
            data.AD_PS2 =       new BitmapImage(new Uri("Images/System/config/pcsx2.png", UriKind.Relative));

            //check for custom stat and weapon icons
            if (File.Exists("CustomImages/System/stats/sword.png"))
                CustomSwordFound = true;
            if (File.Exists("CustomImages/System/stats/staff.png"))
                CustomStaffFound = true;
            if (File.Exists("CustomImages/System/stats/shield.png"))
                CustomShieldFound = true;
            if (File.Exists("CustomImages/System/stats/level.png"))
                CustomLevelFound = true;
            if (File.Exists("CustomImages/System/stats/strength.png"))
                CustomStrengthFound = true;
            if (File.Exists("CustomImages/System/stats/magic.png"))
                CustomMagicFound = true;
            if (File.Exists("CustomImages/System/stats/defence.png"))
                CustomDefenseFound = true;

            //check for custom progression icons. if these 4 are there then assume all progression icons are there
            //no reason for these 4 specific ones, i just picked 4 completely unrelated ones at random.
            if (File.Exists("CustomImages/Progression/chest.png") && File.Exists("CustomImages/Progression/emblem.png") && 
                File.Exists("CustomImages/Progression/100_acre_wood/hunny_slider.png") && File.Exists("CustomImages/Progression/halloween_town/oogie_boogie.png"))
                CustomProgFound = true;

            //helps determine what item images need replacing with custom image loading
            CusItemCheck = new Dictionary<Item, Tuple<string, Item, ContentControl, string>>
            {
                {Report1,        new Tuple<string, Item, ContentControl, string>("Cus-Report1",        Ghost_Report1,        S_Report1,         "CustomImages/Checks/ansem_report01.png")},
                {Report2,        new Tuple<string, Item, ContentControl, string>("Cus-Report2",        Ghost_Report2,        S_Report2,         "CustomImages/Checks/ansem_report02.png")},
                {Report3,        new Tuple<string, Item, ContentControl, string>("Cus-Report3",        Ghost_Report3,        S_Report3,         "CustomImages/Checks/ansem_report03.png")},
                {Report4,        new Tuple<string, Item, ContentControl, string>("Cus-Report4",        Ghost_Report4,        S_Report4,         "CustomImages/Checks/ansem_report04.png")},
                {Report5,        new Tuple<string, Item, ContentControl, string>("Cus-Report5",        Ghost_Report5,        S_Report5,         "CustomImages/Checks/ansem_report05.png")},
                {Report6,        new Tuple<string, Item, ContentControl, string>("Cus-Report6",        Ghost_Report6,        S_Report6,         "CustomImages/Checks/ansem_report06.png")},
                {Report7,        new Tuple<string, Item, ContentControl, string>("Cus-Report7",        Ghost_Report7,        S_Report7,         "CustomImages/Checks/ansem_report07.png")},
                {Report8,        new Tuple<string, Item, ContentControl, string>("Cus-Report8",        Ghost_Report8,        S_Report8,         "CustomImages/Checks/ansem_report08.png")},
                {Report9,        new Tuple<string, Item, ContentControl, string>("Cus-Report9",        Ghost_Report9,        S_Report9,         "CustomImages/Checks/ansem_report09.png")},
                {Report10,       new Tuple<string, Item, ContentControl, string>("Cus-Report10",       Ghost_Report10,       S_Report10,        "CustomImages/Checks/ansem_report10.png")},
                {Report11,       new Tuple<string, Item, ContentControl, string>("Cus-Report11",       Ghost_Report11,       S_Report11,        "CustomImages/Checks/ansem_report11.png")},
                {Report12,       new Tuple<string, Item, ContentControl, string>("Cus-Report12",       Ghost_Report12,       S_Report12,        "CustomImages/Checks/ansem_report12.png")},
                {Report13,       new Tuple<string, Item, ContentControl, string>("Cus-Report13",       Ghost_Report13,       S_Report13,        "CustomImages/Checks/ansem_report13.png")},
                {Fire1,          new Tuple<string, Item, ContentControl, string>("Cus-Fire1",          Ghost_Fire1,          S_Fire,            "CustomImages/Checks/magic_fire.png")},
                {Fire2,          new Tuple<string, Item, ContentControl, string>("Cus-Fire2",          Ghost_Fire2,          null,              "CustomImages/Checks/magic_fire.png")},
                {Fire3,          new Tuple<string, Item, ContentControl, string>("Cus-Fire3",          Ghost_Fire3,          null,              "CustomImages/Checks/magic_fire.png")},
                {Blizzard1,      new Tuple<string, Item, ContentControl, string>("Cus-Blizzard1",      Ghost_Blizzard1,      S_Blizzard,        "CustomImages/Checks/magic_blizzard.png")},
                {Blizzard2,      new Tuple<string, Item, ContentControl, string>("Cus-Blizzard2",      Ghost_Blizzard2,      null,              "CustomImages/Checks/magic_blizzard.png")},
                {Blizzard3,      new Tuple<string, Item, ContentControl, string>("Cus-Blizzard3",      Ghost_Blizzard3,      null,              "CustomImages/Checks/magic_blizzard.png")},
                {Thunder1,       new Tuple<string, Item, ContentControl, string>("Cus-Thunder1",       Ghost_Thunder1,       S_Thunder,         "CustomImages/Checks/magic_thunder.png")},
                {Thunder2,       new Tuple<string, Item, ContentControl, string>("Cus-Thunder2",       Ghost_Thunder2,       null,              "CustomImages/Checks/magic_thunder.png")},
                {Thunder3,       new Tuple<string, Item, ContentControl, string>("Cus-Thunder3",       Ghost_Thunder3,       null,              "CustomImages/Checks/magic_thunder.png")},
                {Cure1,          new Tuple<string, Item, ContentControl, string>("Cus-Cure1",          Ghost_Cure1,          S_Cure,            "CustomImages/Checks/magic_cure.png")},
                {Cure2,          new Tuple<string, Item, ContentControl, string>("Cus-Cure2",          Ghost_Cure2,          null,              "CustomImages/Checks/magic_cure.png")},
                {Cure3,          new Tuple<string, Item, ContentControl, string>("Cus-Cure3",          Ghost_Cure3,          null,              "CustomImages/Checks/magic_cure.png")},
                {Reflect1,       new Tuple<string, Item, ContentControl, string>("Cus-Reflect1",       Ghost_Reflect1,       S_Reflect,         "CustomImages/Checks/magic_reflect.png")},
                {Reflect2,       new Tuple<string, Item, ContentControl, string>("Cus-Reflect2",       Ghost_Reflect2,       null,              "CustomImages/Checks/magic_reflect.png")},
                {Reflect3,       new Tuple<string, Item, ContentControl, string>("Cus-Reflect3",       Ghost_Reflect3,       null,              "CustomImages/Checks/magic_reflect.png")},
                {Magnet1,        new Tuple<string, Item, ContentControl, string>("Cus-Magnet1",        Ghost_Magnet1,        S_Magnet,          "CustomImages/Checks/magic_magnet.png")},
                {Magnet2,        new Tuple<string, Item, ContentControl, string>("Cus-Magnet2",        Ghost_Magnet2,        null,              "CustomImages/Checks/magic_magnet.png")},
                {Magnet3,        new Tuple<string, Item, ContentControl, string>("Cus-Magnet3",        Ghost_Magnet3,        null,              "CustomImages/Checks/magic_magnet.png")},
                {TornPage1,      new Tuple<string, Item, ContentControl, string>("Cus-TornPage1",      Ghost_TornPage1,      S_TornPage,        "CustomImages/Checks/torn_pages.png")},
                {TornPage2,      new Tuple<string, Item, ContentControl, string>("Cus-TornPage2",      Ghost_TornPage2,      null,              "CustomImages/Checks/torn_pages.png")},
                {TornPage3,      new Tuple<string, Item, ContentControl, string>("Cus-TornPage3",      Ghost_TornPage3,      null,              "CustomImages/Checks/torn_pages.png")},
                {TornPage4,      new Tuple<string, Item, ContentControl, string>("Cus-TornPage4",      Ghost_TornPage4,      null,              "CustomImages/Checks/torn_pages.png")},
                {TornPage5,      new Tuple<string, Item, ContentControl, string>("Cus-TornPage5",      Ghost_TornPage5,      null,              "CustomImages/Checks/torn_pages.png")},
                {Valor,          new Tuple<string, Item, ContentControl, string>("Cus-Valor",          Ghost_Valor,          S_Valor,           "CustomImages/Checks/form_valor.png")},
                {Wisdom,         new Tuple<string, Item, ContentControl, string>("Cus-Wisdom",         Ghost_Wisdom,         S_Wisdom,          "CustomImages/Checks/form_wisdom.png")},
                {Limit,          new Tuple<string, Item, ContentControl, string>("Cus-Limit",          Ghost_Limit,          S_Limit,           "CustomImages/Checks/form_limit.png")},
                {Master,         new Tuple<string, Item, ContentControl, string>("Cus-Master",         Ghost_Master,         S_Master,          "CustomImages/Checks/form_master.png")},
                {Final,          new Tuple<string, Item, ContentControl, string>("Cus-Final",          Ghost_Final,          S_Final,           "CustomImages/Checks/form_final.png")},
                {Lamp,           new Tuple<string, Item, ContentControl, string>("Cus-Lamp",           Ghost_Lamp,           S_Lamp,            "CustomImages/Checks/summon_genie.png")},
                {Ukulele,        new Tuple<string, Item, ContentControl, string>("Cus-Ukulele",        Ghost_Ukulele,        S_Ukulele,         "CustomImages/Checks/summon_stitch.png")},
                {Baseball,       new Tuple<string, Item, ContentControl, string>("Cus-Baseball",       Ghost_Baseball,       S_Baseball,        "CustomImages/Checks/summon_chicken_little.png")},
                {Feather,        new Tuple<string, Item, ContentControl, string>("Cus-Feather",        Ghost_Feather,        S_Feather,         "CustomImages/Checks/summon_peter_pan.png")},
                {Nonexistence,   new Tuple<string, Item, ContentControl, string>("Cus-Nonexistence",   Ghost_Nonexistence,   S_Nonexistence,    "CustomImages/Checks/proof_nonexistence.png")},
                {Connection,     new Tuple<string, Item, ContentControl, string>("Cus-Connection",     Ghost_Connection,     S_Connection,      "CustomImages/Checks/proof_connection.png")},
                {Peace,          new Tuple<string, Item, ContentControl, string>("Cus-Peace",          Ghost_Peace,          S_Peace,           "CustomImages/Checks/proof_peace.png")},
                {PromiseCharm,   new Tuple<string, Item, ContentControl, string>("Cus-PromiseCharm",   Ghost_PromiseCharm,   S_PromiseCharm,    "CustomImages/Checks/promise_charm.png")},
                {OnceMore,       new Tuple<string, Item, ContentControl, string>("Cus-OnceMore",       Ghost_OnceMore,       S_OnceMore,        "CustomImages/Checks/once_more.png")},
                {SecondChance,   new Tuple<string, Item, ContentControl, string>("Cus-SecondChance",   Ghost_SecondChance,   S_SecondChance,    "CustomImages/Checks/second_chance.png")},
                {MulanWep,       new Tuple<string, Item, ContentControl, string>("Cus-MulanWep",       Ghost_MulanWep,       S_MulanWep,        "CustomImages/Checks/lock_AncestorSword.png")},
                {AuronWep,       new Tuple<string, Item, ContentControl, string>("Cus-AuronWep",       Ghost_AuronWep,       S_AuronWep,        "CustomImages/Checks/lock_BattlefieldsofWar.png")},
                {BeastWep,       new Tuple<string, Item, ContentControl, string>("Cus-BeastWep",       Ghost_BeastWep,       S_BeastWep,        "CustomImages/Checks/lock_BeastClaw.png")},
                {JackWep,        new Tuple<string, Item, ContentControl, string>("Cus-JackWep",        Ghost_JackWep,        S_JackWep,         "CustomImages/Checks/lock_BoneFist.png")},
                {IceCream,       new Tuple<string, Item, ContentControl, string>("Cus-IceCream",       Ghost_IceCream,       S_IceCream,        "CustomImages/Checks/lock_IceCream.png")},
                {TronWep,        new Tuple<string, Item, ContentControl, string>("Cus-TronWep",        Ghost_TronWep,        S_TronWep,         "CustomImages/Checks/lock_IdentityDisk.png")},
                {Picture,        new Tuple<string, Item, ContentControl, string>("Cus-Picture",        Ghost_Picture,        S_Picture,         "CustomImages/Checks/lock_Picture.png")},
                {MembershipCard, new Tuple<string, Item, ContentControl, string>("Cus-MembershipCard", Ghost_MembershipCard, S_MembershipCard,  "CustomImages/Checks/lock_membership_card.png")},
                {SimbaWep,       new Tuple<string, Item, ContentControl, string>("Cus-SimbaWep",       Ghost_SimbaWep,       S_SimbaWep,        "CustomImages/Checks/lock_ProudFang.png")},
                {AladdinWep,     new Tuple<string, Item, ContentControl, string>("Cus-AladdinWep",     Ghost_AladdinWep,     S_AladdinWep,      "CustomImages/Checks/lock_Scimitar.png")},
                {SparrowWep,     new Tuple<string, Item, ContentControl, string>("Cus-SparrowWep",     Ghost_SparrowWep,     S_SparrowWep,      "CustomImages/Checks/lock_SkillCrossbones.png")},
                {HadesCup,       new Tuple<string, Item, ContentControl, string>("Cus-HadesCup",       Ghost_HadesCup,       S_HadesCup,        "CustomImages/Checks/aux_hades_cup.png")},
                {OlympusStone,   new Tuple<string, Item, ContentControl, string>("Cus-OlympusStone",   Ghost_OlympusStone,   S_OlympusStone,    "CustomImages/Checks/aux_olympus_stone.png")},
                {UnknownDisk,    new Tuple<string, Item, ContentControl, string>("Cus-UnknownDisk",    Ghost_UnknownDisk,    S_UnknownDisk,     "CustomImages/Checks/aux_UnknownDisk.png")},
                {MunnyPouch1,    new Tuple<string, Item, ContentControl, string>("Cus-MunnyPouch1",    Ghost_MunnyPouch1,    S_MunnyPouch,      "CustomImages/Checks/aux_munny_pouch.png")},
                {MunnyPouch2,    new Tuple<string, Item, ContentControl, string>("Cus-MunnyPouch2",    Ghost_MunnyPouch2,    null,              "CustomImages/Checks/aux_munny_pouch.png")},
                {Anti,           new Tuple<string, Item, ContentControl, string>("Cus-Anti",           Ghost_Anti,           S_Anti,            "CustomImages/Checks/form_anti.png")}
            };

            //ghost items
            CusItemCheckG = new Dictionary<Item, Tuple<string, string>>
            {
                {Ghost_Report1,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report1")},
                {Ghost_Report2,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report2")},
                {Ghost_Report3,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report3")},
                {Ghost_Report4,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report4")},
                {Ghost_Report5,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report5")},
                {Ghost_Report6,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report6")},
                {Ghost_Report7,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report7")},
                {Ghost_Report8,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report8")},
                {Ghost_Report9,         new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png",  "Cus-G_Report9")},
                {Ghost_Report10,        new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png", "Cus-G_Report10")},
                {Ghost_Report11,        new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png", "Cus-G_Report11")},
                {Ghost_Report12,        new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png", "Cus-G_Report12")},
                {Ghost_Report13,        new Tuple<string, string>("CustomImages/GhostChecks/ansem_report.png", "Cus-G_Report13")},
                {Ghost_Fire1,           new Tuple<string, string>("CustomImages/GhostChecks/magic_fire.png", "Cus-G_Fire1")},
                {Ghost_Fire2,           new Tuple<string, string>("CustomImages/GhostChecks/magic_fire.png", "Cus-G_Fire2")},
                {Ghost_Fire3,           new Tuple<string, string>("CustomImages/GhostChecks/magic_fire.png", "Cus-G_Fire3")},
                {Ghost_Blizzard1,       new Tuple<string, string>("CustomImages/GhostChecks/magic_blizzard.png", "Cus-G_Blizzard1")},
                {Ghost_Blizzard2,       new Tuple<string, string>("CustomImages/GhostChecks/magic_blizzard.png", "Cus-G_Blizzard2")},
                {Ghost_Blizzard3,       new Tuple<string, string>("CustomImages/GhostChecks/magic_blizzard.png", "Cus-G_Blizzard3")},
                {Ghost_Thunder1,        new Tuple<string, string>("CustomImages/GhostChecks/magic_thunder.png", "Cus-G_Thunder1")},
                {Ghost_Thunder2,        new Tuple<string, string>("CustomImages/GhostChecks/magic_thunder.png", "Cus-G_Thunder2")},
                {Ghost_Thunder3,        new Tuple<string, string>("CustomImages/GhostChecks/magic_thunder.png", "Cus-G_Thunder3")},
                {Ghost_Cure1,           new Tuple<string, string>("CustomImages/GhostChecks/magic_cure.png", "Cus-G_Cure1")},
                {Ghost_Cure2,           new Tuple<string, string>("CustomImages/GhostChecks/magic_cure.png", "Cus-G_Cure2")},
                {Ghost_Cure3,           new Tuple<string, string>("CustomImages/GhostChecks/magic_cure.png", "Cus-G_Cure3")},
                {Ghost_Reflect1,        new Tuple<string, string>("CustomImages/GhostChecks/magic_reflect.png", "Cus-G_Reflect1")},
                {Ghost_Reflect2,        new Tuple<string, string>("CustomImages/GhostChecks/magic_reflect.png", "Cus-G_Reflect2")},
                {Ghost_Reflect3,        new Tuple<string, string>("CustomImages/GhostChecks/magic_reflect.png", "Cus-G_Reflect3")},
                {Ghost_Magnet1,         new Tuple<string, string>("CustomImages/GhostChecks/magic_magnet.png", "Cus-G_Magnet1")},
                {Ghost_Magnet2,         new Tuple<string, string>("CustomImages/GhostChecks/magic_magnet.png", "Cus-G_Magnet2")},
                {Ghost_Magnet3,         new Tuple<string, string>("CustomImages/GhostChecks/magic_magnet.png", "Cus-G_Magnet3")},
                {Ghost_TornPage1,       new Tuple<string, string>("CustomImages/GhostChecks/torn_pages.png", "Cus-G_TornPage1")},
                {Ghost_TornPage2,       new Tuple<string, string>("CustomImages/GhostChecks/torn_pages.png", "Cus-G_TornPage2")},
                {Ghost_TornPage3,       new Tuple<string, string>("CustomImages/GhostChecks/torn_pages.png", "Cus-G_TornPage3")},
                {Ghost_TornPage4,       new Tuple<string, string>("CustomImages/GhostChecks/torn_pages.png", "Cus-G_TornPage4")},
                {Ghost_TornPage5,       new Tuple<string, string>("CustomImages/GhostChecks/torn_pages.png", "Cus-G_TornPage5")},
                {Ghost_Valor,           new Tuple<string, string>("CustomImages/GhostChecks/form_valor.png", "Cus-G_Valor")},
                {Ghost_Wisdom,          new Tuple<string, string>("CustomImages/GhostChecks/form_wisdom.png", "Cus-G_Wisdom")},
                {Ghost_Limit,           new Tuple<string, string>("CustomImages/GhostChecks/form_limit.png", "Cus-G_Limit")},
                {Ghost_Master,          new Tuple<string, string>("CustomImages/GhostChecks/form_master.png", "Cus-G_Master")},
                {Ghost_Final,           new Tuple<string, string>("CustomImages/GhostChecks/form_final.png", "Cus-G_Final")},
                {Ghost_Lamp,            new Tuple<string, string>("CustomImages/GhostChecks/summon_genie.png", "Cus-G_Lamp")},
                {Ghost_Ukulele,         new Tuple<string, string>("CustomImages/GhostChecks/summon_stitch.png", "Cus-G_Ukulele")},
                {Ghost_Baseball,        new Tuple<string, string>("CustomImages/GhostChecks/summon_chicken_little.png", "Cus-G_Baseball"	)},
                {Ghost_Feather,         new Tuple<string, string>("CustomImages/GhostChecks/summon_peter_pan.png", "Cus-G_Feather")},
                {Ghost_Nonexistence,    new Tuple<string, string>("CustomImages/GhostChecks/proof_nonexistence.png", "Cus-G_Nonexistence")},
                {Ghost_Connection,      new Tuple<string, string>("CustomImages/GhostChecks/proof_connection.png", "Cus-G_Connection")},
                {Ghost_Peace,           new Tuple<string, string>("CustomImages/GhostChecks/proof_peace.png", "Cus-G_Peace")},
                {Ghost_PromiseCharm,    new Tuple<string, string>("CustomImages/GhostChecks/promise_charm.png", "Cus-G_PromiseCharm")},
                {Ghost_OnceMore,        new Tuple<string, string>("CustomImages/GhostChecks/once_more.png", "Cus-G_OnceMore")},
                {Ghost_SecondChance,    new Tuple<string, string>("CustomImages/GhostChecks/second_chance.png", "Cus-G_SecondChance")},
                {Ghost_MulanWep,        new Tuple<string, string>("CustomImages/GhostChecks/lock_AncestorSword.png", "Cus-G_MulanWep")},
                {Ghost_AuronWep,        new Tuple<string, string>("CustomImages/GhostChecks/lock_BattlefieldsofWar.png", "Cus-G_AuronWep")},
                {Ghost_BeastWep,        new Tuple<string, string>("CustomImages/GhostChecks/lock_BeastClaw.png", "Cus-G_BeastWep")},
                {Ghost_JackWep,         new Tuple<string, string>("CustomImages/GhostChecks/lock_BoneFist.png", "Cus-G_JackWep")},
                {Ghost_IceCream,        new Tuple<string, string>("CustomImages/GhostChecks/lock_IceCream.png", "Cus-G_IceCream")},
                {Ghost_TronWep,         new Tuple<string, string>("CustomImages/GhostChecks/lock_IdentityDisk.png", "Cus-G_TronWep")},
                {Ghost_Picture,         new Tuple<string, string>("CustomImages/GhostChecks/lock_Picture.png", "Cus-G_Picture")},
                {Ghost_MembershipCard,  new Tuple<string, string>("CustomImages/GhostChecks/lock_membership_card.png", "Cus-G_MembershipCard")},
                {Ghost_SimbaWep,        new Tuple<string, string>("CustomImages/GhostChecks/lock_ProudFang.png", "Cus-G_SimbaWep")},
                {Ghost_AladdinWep,      new Tuple<string, string>("CustomImages/GhostChecks/lock_Scimitar.png", "Cus-G_AladdinWep")},
                {Ghost_SparrowWep,      new Tuple<string, string>("CustomImages/GhostChecks/lock_SkillCrossbones.png", "Cus-G_SparrowWep")},
                {Ghost_HadesCup,        new Tuple<string, string>("CustomImages/GhostChecks/aux_hades_cup.png", "Cus-G_HadesCup")},
                {Ghost_OlympusStone,    new Tuple<string, string>("CustomImages/GhostChecks/aux_olympus_stone.png", "Cus-G_OlympusStone")},
                {Ghost_UnknownDisk,     new Tuple<string, string>("CustomImages/GhostChecks/aux_UnknownDisk.png", "Cus-G_UnknownDisk")},
                {Ghost_MunnyPouch1,     new Tuple<string, string>("CustomImages/GhostChecks/aux_munny_pouch.png", "Cus-G_MunnyPouch1")},
                {Ghost_MunnyPouch2,     new Tuple<string, string>("CustomImages/GhostChecks/aux_munny_pouch.png", "Cus-G_MunnyPouch2")},
                {Ghost_Anti,            new Tuple<string, string>("CustomImages/GhostChecks/form_anti.png", "Cus-G_Anti")}
            };
        }

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
                if (File.Exists("CustomImages/background.png"))
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
                if (File.Exists("CustomImages/background.png"))
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
                if (File.Exists("CustomImages/background.png"))
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

            if (Directory.Exists("CustomImages/GhostChecks/"))
            {
                checkFilesG = Directory.GetFiles("CustomImages/GhostChecks/", "*.png", SearchOption.TopDirectoryOnly);
            }


            //if list isn't empty then compare against dictionary to determine what icons to replace

            //    key     |  item1  | item2 | item3  | item4
            //main window | dic key | ghost | shadow | path

            // ghost should always use main widnow icons if any are found first.
            if (checkFiles.Length > 0)
            {
                //check if i actually need this lowercase edit
                checkFiles = checkFiles.Select(s => s.ToLowerInvariant()).ToArray();

                foreach (var item in CusItemCheck)
                {
                    if (checkFiles.Contains(item.Value.Item4.ToLower()))
                    {
                        //main item
                        Item main = item.Key;
                        main.SetResourceReference(ContentProperty, item.Value.Item1);

                        //item shadow
                        if (item.Value.Item3 != null)
                        {
                            ContentControl shadow = item.Value.Item3;
                            shadow.SetResourceReference(ContentProperty, item.Value.Item1);
                        }

                        //ghost item
                        if (item.Value.Item2 != null)
                        {
                            Item ghost = item.Value.Item2;
                            if (Codes.FindItemType(ghost.Name) != "report")
                                ghost.SetResourceReference(ContentProperty, item.Value.Item1);
                            else
                                ghost.SetResourceReference(ContentProperty, "Cus-Report");
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
                        if (Codes.FindItemType(ghost.Name) != "report")
                            ghost.SetResourceReference(ContentProperty, item.Value.Item2);
                        else
                            ghost.SetResourceReference(ContentProperty, "Cus-G_Report");

                    }
                }
            }

            //check if folders exists then start checking if each file exists in it
            if (Directory.Exists("CustomImages/Checks/"))
            {
                if (File.Exists("CustomImages/System/drive_growth/high_jump.png"))
                    HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                if (File.Exists("CustomImages/System/drive_growth/quick_run.png"))
                    QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                if (File.Exists("CustomImages/System/drive_growth/dodge_roll.png"))
                    DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                if (File.Exists("CustomImages/System/drive_growth/aerial_didge.png"))
                    AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                if (File.Exists("CustomImages/System/drive_growth/glide.png"))
                    Glide.SetResourceReference(ContentProperty, "Cus-Glide");

                if (File.Exists("CustomImages/System/drive_growth/valor.png"))
                    ValorM.SetResourceReference(ContentProperty, "Cus-Valor");
                if (File.Exists("CustomImages/System/drive_growth/wisdom.png"))
                    WisdomM.SetResourceReference(ContentProperty, "Cus-Wisdom");
                if (File.Exists("CustomImages/System/drive_growth/limit.png"))
                    LimitM.SetResourceReference(ContentProperty, "Cus-Limit");
                if (File.Exists("CustomImages/System/drive_growth/master.png"))
                    MasterM.SetResourceReference(ContentProperty, "Cus-Master");
                if (File.Exists("CustomImages/System/drive_growth/final.png"))
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
            if(Directory.Exists("CustomImages/Worlds/Locks/"))
            {
                if (File.Exists("CustomImages/Worlds/Locks/HB.png"))
                    HollowBastionLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/HB.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/OC.png"))
                    OlympusColiseumLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/OC.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/LD.png"))
                    LandofDragonsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/LD.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/PL.png"))
                    PrideLandsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/PL.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/HT.png"))
                    HalloweenTownLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/HT.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/SP.png"))
                    SpaceParanoidsLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/SP.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/BC.png"))
                    BeastsCastleLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/BC.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/AG.png"))
                    AgrabahLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/AG.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/PR.png"))
                    PortRoyalLock.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/PR.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/TT3.png"))
                    TwilightTownLock_1.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/TT3.png", UriKind.Absolute));
                if (File.Exists("CustomImages/Worlds/Locks/TT2.png"))
                    TwilightTownLock_2.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/Worlds/Locks/TT2.png", UriKind.Absolute));
            }

            //world cross
            if (File.Exists("CustomImages/System/crossworld.png"))
            {
                SorasHeartCross.Source =            new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                DriveFormsCross.Source =            new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                SimulatedTwilightTownCross.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                TwilightTownCross.Source =          new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                HollowBastionCross.Source =         new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                BeastsCastleCross.Source =          new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                OlympusColiseumCross.Source =       new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                AgrabahCross.Source =               new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                LandofDragonsCross.Source =         new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                HundredAcreWoodCross.Source =       new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                PrideLandsCross.Source =            new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                DisneyCastleCross.Source =          new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                HalloweenTownCross.Source =         new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                PortRoyalCross.Source =             new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                TWTNWCross.Source =                 new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                SpaceParanoidsCross.Source =        new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                AtlanticaCross.Source =             new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                PuzzSynthCross.Source =             new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
                GoACross.Source =                   new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/crossworld.png", UriKind.Absolute));
            }

            //DeathCounter counter skull
            if (File.Exists("CustomImages/System/stats/deaths.png"))
                DeathIcon.Source = new BitmapImage(new Uri("pack://application:,,,/CustomImages/System/stats/deaths.png", UriKind.Absolute));
        }

        private void CustomWorldCheck()
        {
            if (!CustomFolderOption.IsChecked)
                return;

            string[] worldFiles = { };

            if (Directory.Exists("CustomImages/Worlds/"))
            {
                worldFiles = Directory.GetFiles("CustomImages/Worlds/", "*.png", SearchOption.TopDirectoryOnly);
            }

            if (worldFiles.Length > 0)
            {
                //check if i actually need this lowercase edit
                worldFiles = worldFiles.Select(s => s.ToLowerInvariant()).ToArray();

                foreach (var item in CusItemCheckG)
                {
                    if (worldFiles.Contains(item.Value.Item1.ToLower()))
                    {
                        //main item
                        Item ghost = item.Key as Item;
                        ghost.SetResourceReference(ContentProperty, item.Value.Item2);
                    }
                }
            }






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
                if (File.Exists("CustomImages/Worlds/hollow_bastion.png"))
                {
                    HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                }
                if (File.Exists("CustomImages/Worlds/olympus_coliseum.png"))
                {
                    OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                }

                //check for custom cavern, timeless, and cups toggles
                //if (File.Exists("CustomImages/Worlds/Level01.png") && SoraLevel01Option.IsChecked)
                //{
                //    SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel01");
                //}
                //if (File.Exists("CustomImages/Worlds/Level50.png") && SoraLevel50Option.IsChecked)
                //{
                //    SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel50");
                //}
                //if (File.Exists("CustomImages/Worlds/Level99.png") && SoraLevel99Option.IsChecked)
                //{
                //    SorasHeartType.SetResourceReference(ContentProperty, "Cus-SoraLevel99");
                //}

                //puzzle/synth display
                if (File.Exists("CustomImages/Worlds/PuzzSynth.png") && PuzzleOption.IsChecked && SynthOption.IsChecked) //both on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth");
                }
                if (File.Exists("CustomImages/Worlds/Synth.png") && !PuzzleOption.IsChecked && SynthOption.IsChecked) //synth on puzzle off
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_S");
                }
                if (File.Exists("CustomImages/Worlds/Puzzle.png") && PuzzleOption.IsChecked && !SynthOption.IsChecked) //synth off puzzle on
                {
                    PuzzSynth.SetResourceReference(ContentProperty, "Cus-PuzzSynth_P");
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
            DeathIcon.SetResourceReference(ContentProperty, "DeathIcon");

            string type = "Min-";
            if (OldCheckOption.IsChecked)
                type = "Old-";

            // Item icons
            foreach (var item in data.Items.Keys)
            {
                data.Items[item].Item1.SetResourceReference(ContentProperty, type + item);

                //dirty way of doing this, but oh well
                //shadows
                if (CusItemCheck[data.Items[item].Item1].Item3 != null)
                    CusItemCheck[data.Items[item].Item1].Item3.SetResourceReference(ContentProperty, type + item);
            }
            
            // Ghost icons
            foreach (var item in data.GhostItems.Values)
            {
                if (Codes.FindItemType(item.Name) != "report")
                    item.SetResourceReference(ContentProperty, type + item.Name.Remove(0, 6));
                else
                    item.SetResourceReference(ContentProperty, type + "Report");
            }
            
            // stat/info icons
            ValorM.SetResourceReference(ContentProperty, "Valor");
            WisdomM.SetResourceReference(ContentProperty, "Wisdom");
            LimitM.SetResourceReference(ContentProperty, "Limit");
            MasterM.SetResourceReference(ContentProperty, "Master");
            FinalM.SetResourceReference(ContentProperty, "Final");
            HighJump.SetResourceReference(ContentProperty, "HighJump");
            QuickRun.SetResourceReference(ContentProperty, "QuickRun");
            DodgeRoll.SetResourceReference(ContentProperty,  "DodgeRoll");
            AerialDodge.SetResourceReference(ContentProperty, "AerialDodge");
            Glide.SetResourceReference(ContentProperty, "Glide");

            CustomChecksCheck();
        }

        public void SetWorldImage()
        {
            string type = "Min-";
            if (OldWorldOption.IsChecked)
                type = "Old-";

            //main window worlds
            SorasHeart.SetResourceReference(ContentProperty, type + "SoraHeartImage");
            SimulatedTwilightTown.SetResourceReference(ContentProperty, type + "SimulatedImage");
            HollowBastion.SetResourceReference(ContentProperty, type + "HollowBastionImage");
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

            CustomWorldCheck();
        }
    }
}