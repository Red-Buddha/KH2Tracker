using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;
        private BroadcastWindow broadcast;
        public int collected;
        private int total = 51;
        public static int PointTotal = 0;
        //public static int World = 0;
        public static bool SeedHashLoaded = false;
        public static bool SeedHashVisible = false;

        //this is stupid. Hash kept auto reseting because of SetMode during hint loading.
        //this is here as a toggle to only reset the hash when i want it to
        public static bool ShouldResetHash = true;

        public MainWindow()
        {
            InitializeComponent();
            InitData();
            InitImages();

            collectedChecks = new List<ImportantCheck>();
            newChecks = new List<ImportantCheck>();
            previousChecks = new List<ImportantCheck>();

            InitOptions();

            //Init auto-detect
            if (AutoDetectOption.IsChecked)
            {
                AutoDetectToggle(null, null);
                //SetAutoDetectTimer();
            }
        }

        private void InitData()
        {
            data = new Data();

            data.Reports.Add(Report1);
            data.Reports.Add(Report2);
            data.Reports.Add(Report3);
            data.Reports.Add(Report4);
            data.Reports.Add(Report5);
            data.Reports.Add(Report6);
            data.Reports.Add(Report7);
            data.Reports.Add(Report8);
            data.Reports.Add(Report9);
            data.Reports.Add(Report10);
            data.Reports.Add(Report11);
            data.Reports.Add(Report12);
            data.Reports.Add(Report13);

            data.ReportAttemptVisual.Add(Attempts1);
            data.ReportAttemptVisual.Add(Attempts2);
            data.ReportAttemptVisual.Add(Attempts3);
            data.ReportAttemptVisual.Add(Attempts4);
            data.ReportAttemptVisual.Add(Attempts5);
            data.ReportAttemptVisual.Add(Attempts6);
            data.ReportAttemptVisual.Add(Attempts7);
            data.ReportAttemptVisual.Add(Attempts8);
            data.ReportAttemptVisual.Add(Attempts9);
            data.ReportAttemptVisual.Add(Attempts10);
            data.ReportAttemptVisual.Add(Attempts11);
            data.ReportAttemptVisual.Add(Attempts12);
            data.ReportAttemptVisual.Add(Attempts13);

            data.TornPages.Add(TornPage1);
            data.TornPages.Add(TornPage2);
            data.TornPages.Add(TornPage3);
            data.TornPages.Add(TornPage4);
            data.TornPages.Add(TornPage5);

            data.WorldsData.Add("SorasHeart", new WorldData(SorasHeartTop, SorasHeart, null, SorasHeartHint, SorasHeartGrid, SorasHeartBar, false));
            data.WorldsData.Add("DriveForms", new WorldData(DriveFormsTop, DriveForms, null, DriveFormsHint, DriveFormsGrid, DriveFormsBar, false));
            data.WorldsData.Add("SimulatedTwilightTown", new WorldData(SimulatedTwilightTownTop, SimulatedTwilightTown, SimulatedTwilightTownProgression, SimulatedTwilightTownHint, SimulatedTwilightTownGrid, SimulatedTwilightTownBar, false));
            data.WorldsData.Add("TwilightTown", new WorldData(TwilightTownTop, TwilightTown, TwilightTownProgression, TwilightTownHint, TwilightTownGrid, TwilightTownBar, false));
            data.WorldsData.Add("HollowBastion", new WorldData(HollowBastionTop, HollowBastion, HollowBastionProgression, HollowBastionHint, HollowBastionGrid, HollowBastionBar, false));
            data.WorldsData.Add("BeastsCastle", new WorldData(BeastsCastleTop, BeastsCastle, BeastsCastleProgression, BeastsCastleHint, BeastsCastleGrid, BeastsCastleBar, false));
            data.WorldsData.Add("OlympusColiseum", new WorldData(OlympusColiseumTop, OlympusColiseum, OlympusColiseumProgression, OlympusColiseumHint, OlympusColiseumGrid, OlympusBar, false));
            data.WorldsData.Add("Agrabah", new WorldData(AgrabahTop, Agrabah, AgrabahProgression, AgrabahHint, AgrabahGrid, AgrabahBar, false));
            data.WorldsData.Add("LandofDragons", new WorldData(LandofDragonsTop, LandofDragons, LandofDragonsProgression, LandofDragonsHint, LandofDragonsGrid, LandofDragonsBar, false));
            data.WorldsData.Add("HundredAcreWood", new WorldData(HundredAcreWoodTop, HundredAcreWood, HundredAcreWoodProgression, HundredAcreWoodHint, HundredAcreWoodGrid, HundredAcreWoodBar, false));
            data.WorldsData.Add("PrideLands", new WorldData(PrideLandsTop, PrideLands, PrideLandsProgression, PrideLandsHint, PrideLandsGrid, PrideLandsBar, false));
            data.WorldsData.Add("DisneyCastle", new WorldData(DisneyCastleTop, DisneyCastle, DisneyCastleProgression, DisneyCastleHint, DisneyCastleGrid, DisneyCastleBar, false));
            data.WorldsData.Add("HalloweenTown", new WorldData(HalloweenTownTop, HalloweenTown, HalloweenTownProgression, HalloweenTownHint, HalloweenTownGrid, HalloweenTownBar, false));
            data.WorldsData.Add("PortRoyal", new WorldData(PortRoyalTop, PortRoyal, PortRoyalProgression, PortRoyalHint, PortRoyalGrid, PortRoyalBar, false));
            data.WorldsData.Add("SpaceParanoids", new WorldData(SpaceParanoidsTop, SpaceParanoids, SpaceParanoidsProgression, SpaceParanoidsHint, SpaceParanoidsGrid, SpaceParanoidsBar, false));
            data.WorldsData.Add("TWTNW", new WorldData(TWTNWTop, TWTNW, TWTNWProgression, TWTNWHint, TWTNWGrid, TWTNWBar, false));
            data.WorldsData.Add("GoA", new WorldData(GoATop, GoA, null, null, GoAGrid, GoABar, true));
            data.WorldsData.Add("Atlantica", new WorldData(AtlanticaTop, Atlantica, AtlanticaProgression, AtlanticaHint, AtlanticaGrid, AtlanticaBar, false));

            data.ProgressKeys.Add("SimulatedTwilightTown", new List<string>() { "", "STTChests", "TwilightThorn", "Struggle", "ComputerRoom", "Axel", "DataRoxas" });
            data.ProgressKeys.Add("TwilightTown", new List<string>() { "", "MysteriousTower", "Sandlot", "Mansion", "BetwixtAndBetween", "DataAxel" });
            data.ProgressKeys.Add("HollowBastion", new List<string>() { "", "HBChests", "Bailey", "AnsemStudy", "Corridor", "Dancers", "HBDemyx", "FinalFantasy", "1000Heartless", "Sephiroth", "DataDemyx" });
            data.ProgressKeys.Add("BeastsCastle", new List<string>() { "", "BCChests", "Thresholder", "Beast", "DarkThorn", "Dragoons", "Xaldin", "DataXaldin" });
            data.ProgressKeys.Add("OlympusColiseum", new List<string>() { "", "OCChests", "Cerberus", "OCDemyx", "OCPete", "Hydra", "AuronStatue", "Hades", "Zexion" });
            data.ProgressKeys.Add("Agrabah", new List<string>() { "", "AGChests", "Abu", "Chasm", "TreasureRoom", "Lords", "Carpet", "GenieJafar", "Lexaeus" });
            data.ProgressKeys.Add("LandofDragons", new List<string>() { "", "LoDChests", "Cave", "Summmit", "ShanYu", "ThroneRoom", "StormRider", "DataXigbar" });
            data.ProgressKeys.Add("HundredAcreWood", new List<string>() { "", "Pooh", "Piglet", "Rabbit", "Kanga", "SpookyCave", "StarryHill" });
            data.ProgressKeys.Add("PrideLands", new List<string>() { "", "PLChests", "Simba", "Scar", "GroundShaker", "DataSaix" });
            data.ProgressKeys.Add("DisneyCastle", new List<string>() { "", "DCChests", "Minnie", "OldPete", "Windows", "BoatPete", "DCPete", "Marluxia", "LingeringWill" });
            data.ProgressKeys.Add("HalloweenTown", new List<string>() { "", "HTChests", "CandyCaneLane", "PrisonKeeper", "OogieBoogie", "Presents", "Experiment", "Vexen" });
            data.ProgressKeys.Add("PortRoyal", new List<string>() { "", "PRChests", "Town", "Barbossa", "Gambler", "GrimReaper", "DataLuxord" });
            data.ProgressKeys.Add("SpaceParanoids", new List<string>() { "", "SPChests", "Screens", "HostileProgram", "SolarSailer", "MCP", "Larxene" });
            data.ProgressKeys.Add("TWTNW", new List<string>() { "", "TWTNWChests", "Roxas", "Xigbar", "Luxord", "Saix", "Xemnas1", "DataXemnas" });
            data.ProgressKeys.Add("Atlantica", new List<string>() { "", "Tutorial", "Ursula", "NewDay" });

            foreach (ContentControl item in ItemPool.Children)
            {
                if (item is Item)
                {
                    if (!item.Name.StartsWith("Ghost_"))
                        data.Items.Add(item as Item);
                    else
                        Data.GhostItems.Add(item.Name, item as Item);
                }
            }

            broadcast = new BroadcastWindow(data);
        }

        private void InitOptions()
        {
            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            HandleItemToggle(PromiseCharmOption.IsChecked, PromiseCharm, true);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption.IsChecked, data.Reports[i], true);
            }

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            HandleItemToggle(AbilitiesOption.IsChecked, OnceMore, true);
            HandleItemToggle(AbilitiesOption.IsChecked, SecondChance, true);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption.IsChecked, data.TornPages[i], true);
            }

            CureOption.IsChecked = Properties.Settings.Default.Cure;
            HandleItemToggle(CureOption.IsChecked, Cure1, true);
            HandleItemToggle(CureOption.IsChecked, Cure2, true);
            HandleItemToggle(CureOption.IsChecked, Cure3, true);

            FinalFormOption.IsChecked = Properties.Settings.Default.FinalForm;
            HandleItemToggle(FinalFormOption.IsChecked, Final, true);

            MinWorldOption.IsChecked = Properties.Settings.Default.MinWorld;
            if (MinWorldOption.IsChecked)
                MinWorldToggle(null, null);

            OldWorldOption.IsChecked = Properties.Settings.Default.OldWorld;
            if (OldWorldOption.IsChecked)
                OldWorldToggle(null, null);

            MinCheckOption.IsChecked = Properties.Settings.Default.MinCheck;
            if (MinCheckOption.IsChecked)
                MinCheckToggle(null, null);

            OldCheckOption.IsChecked = Properties.Settings.Default.OldCheck;
            if (OldCheckOption.IsChecked)
                OldCheckToggle(null, null);

            MinProgOption.IsChecked = Properties.Settings.Default.MinProg;
            if (MinProgOption.IsChecked)
                MinProgToggle(null, null);

            OldProgOption.IsChecked = Properties.Settings.Default.OldProg;
            if (OldProgOption.IsChecked)
                OldProgToggle(null, null);

            MinNumOption.IsChecked = Properties.Settings.Default.MinNum;
            if (MinNumOption.IsChecked)
                MinNumToggle(null, null);

            OldNumOption.IsChecked = Properties.Settings.Default.OldNum;
            if (OldNumOption.IsChecked)
                OldNumToggle(null, null);

            CavernOption.IsChecked = Properties.Settings.Default.Cavern;
            CavernToggle(null, null);

            TimelessOption.IsChecked = Properties.Settings.Default.Timeless;
            TimelessToggle(null, null);

            OCCupsOption.IsChecked = Properties.Settings.Default.OCCups;
            OCCupsToggle(null, null);

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

            CustomFolderOption.IsChecked = Properties.Settings.Default.CustomIcons;
            CustomImageToggle(null, null);

            SeedHashOption.IsChecked = Properties.Settings.Default.SeedHash;
            SeedHashToggle(null, null);

            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(SoraHeartOption.IsChecked);
            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(SimulatedOption.IsChecked);
            HundredAcreWoodOption.IsChecked = Properties.Settings.Default.HundredAcre;
            HundredAcreWoodToggle(HundredAcreWoodOption.IsChecked);
            AtlanticaOption.IsChecked = Properties.Settings.Default.Atlantica;
            AtlanticaToggle(AtlanticaOption.IsChecked);

            DragAndDropOption.IsChecked = Properties.Settings.Default.DragDrop;
            DragDropToggle(null, null);

            TopMostOption.IsChecked = Properties.Settings.Default.TopMost;
            TopMostToggle(null, null);

            BroadcastStartupOption.IsChecked = Properties.Settings.Default.BroadcastStartup;
            BroadcastStartupToggle(null, null);

            FormsGrowthOption.IsChecked = Properties.Settings.Default.FormsGrowth;
            FormsGrowthToggle(null, null);

            BroadcastGrowthOption.IsChecked = Properties.Settings.Default.BroadcastGrowth;
            BroadcastGrowthToggle(null, null);

            BroadcastStatsOption.IsChecked = Properties.Settings.Default.BroadcastStats;
            BroadcastStatsToggle(null, null);

            GhostItemOption.IsChecked = Properties.Settings.Default.GhostItem;
            {
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report4);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report5);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report6);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report7);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report8);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report9);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report10);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report11);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report12);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Report13);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Fire1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Fire2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Fire3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Blizzard1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Blizzard2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Blizzard3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Thunder1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Thunder2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Thunder3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Cure1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Cure2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Cure3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Reflect1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Reflect2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Reflect3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Magnet1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Magnet2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Magnet3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Valor);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Wisdom);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Limit);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Master);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Final);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_OnceMore);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_SecondChance);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_TornPage1);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_TornPage2);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_TornPage3);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_TornPage4);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_TornPage5);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Baseball);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Lamp);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Ukulele);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Feather);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Connection);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Nonexistence);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_Peace);
                HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_PromiseCharm);
                //HandleGhostItemToggle(GhostItemOption.IsChecked, Ghost_HadesCup);
            }

            GhostMathOption.IsChecked = Properties.Settings.Default.GhostMath;
            GhostMathToggle(null, null);

            AutoDetectOption.IsChecked = Properties.Settings.Default.AutoDetect;
            AutoDetectToggle(null, null);

            //Next Level Check
            NextLevelCheckOption1.IsChecked = Properties.Settings.Default.Level1;
            if (NextLevelCheckOption1.IsChecked)
                NextLevelCheck1Option(null, null);

            NextLevelCheckOption50.IsChecked = Properties.Settings.Default.Level50;
            if (NextLevelCheckOption50.IsChecked)
                NextLevelCheck50Option(null, null);

            NextLevelCheckOption99.IsChecked = Properties.Settings.Default.Level99;
            if (NextLevelCheckOption99.IsChecked)
                NextLevelCheck99Option(null, null);

            CheckCountOption.IsChecked = Properties.Settings.Default.CheckCount;
            if (CheckCountOption.IsChecked)
                ShowCheckCountToggle(null, null);

            Top = Properties.Settings.Default.WindowY;
            Left = Properties.Settings.Default.WindowX;

            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;


            //testing background settings stuff (i thought this would be simplier than the above methods)
            //maybe i was wrong. (at least everything is done by 2 settings instead of 8)
            {
                int MainBG = Properties.Settings.Default.MainBG;
                if (MainBG == 1)
                {
                    MainDefOption.IsChecked = false;
                    MainImg1Option.IsChecked = true;
                    MainImg2Option.IsChecked = false;
                    MainImg3Option.IsChecked = false;
                    MainBG_Img1Toggle(null, null);
                }
                else if (MainBG == 2)
                {
                    MainDefOption.IsChecked = false;
                    MainImg1Option.IsChecked = false;
                    MainImg2Option.IsChecked = true;
                    MainImg3Option.IsChecked = false;
                    MainBG_Img2Toggle(null, null);
                }
                else if (MainBG == 3)
                {
                    MainDefOption.IsChecked = false;
                    MainImg1Option.IsChecked = false;
                    MainImg2Option.IsChecked = false;
                    MainImg3Option.IsChecked = true;
                    MainBG_Img3Toggle(null, null);
                }
                else
                {
                    MainDefOption.IsChecked = true;
                    MainImg1Option.IsChecked = false;
                    MainImg2Option.IsChecked = false;
                    MainImg3Option.IsChecked = false;
                    MainBG_DefToggle(null, null);
                }
            }
            {
                int BroadcastBG = Properties.Settings.Default.BroadcastBG;
                if (BroadcastBG == 1)
                {
                    BroadcastDefOption.IsChecked = false;
                    BroadcastImg1Option.IsChecked = true;
                    BroadcastImg2Option.IsChecked = false;
                    BroadcastImg3Option.IsChecked = false;
                    BroadcastBG_Img1Toggle(null, null);
                }
                else if (BroadcastBG == 2)
                {
                    BroadcastDefOption.IsChecked = false;
                    BroadcastImg1Option.IsChecked = false;
                    BroadcastImg2Option.IsChecked = true;
                    BroadcastImg3Option.IsChecked = false;
                    BroadcastBG_Img2Toggle(null, null);
                }
                else if (BroadcastBG == 3)
                {
                    BroadcastDefOption.IsChecked = false;
                    BroadcastImg1Option.IsChecked = false;
                    BroadcastImg2Option.IsChecked = false;
                    BroadcastImg3Option.IsChecked = true;
                    BroadcastBG_Img3Toggle(null, null);
                }
                else
                {
                    BroadcastDefOption.IsChecked = true;
                    BroadcastImg1Option.IsChecked = false;
                    BroadcastImg2Option.IsChecked = false;
                    BroadcastImg3Option.IsChecked = false;
                    BroadcastBG_DefToggle(null, null);
                }
            }
        }

        /// 
        /// Input Handling
        /// 
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //this chunk of garbage for using the correct vertical images
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage BarW = data.VerticalBarW;
            BitmapImage BarY = data.VerticalBarY;
            if (CustomMode)
            {
                if (CustomVBarWFound)
                    BarW = data.CustomVerticalBarW;
                if (CustomVBarYFound)
                    BarY = data.CustomVerticalBarY;
            }


            Button button = sender as Button;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (data.selected != null)
                {
                    data.WorldsData[data.selected.Name].selectedBar.Source = BarW;
                }

                data.selected = button;
                data.WorldsData[button.Name].selectedBar.Source = BarY;
            }
            else if (e.ChangedButton == MouseButton.Middle)
            {
                if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].hint != null && data.mode == Mode.None)
                {
                    data.WorldsData[button.Name].hint.Source = GetDataNumber("Y")[0];
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].hint != null)
            {
                HandleReportValue(data.WorldsData[button.Name].hint, e.Delta);
            }
        }

        private void OnMouseRightClick(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;
            //BitmapImage Normal = 
            string test = SecondChance.ContentStringFormat;
            Console.WriteLine(test);
            //SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");

            //if (e.ChangedButton == MouseButton.Right)
            //{
            //    HandleReportValue(data.WorldsData[button.Name].hint, e.Delta);
            //}
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].hint != null)
                {
                    HandleReportValue(data.WorldsData[data.selected.Name].hint, -1);
                }
            }
            if (e.Key == Key.PageUp && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].hint != null)
                {
                    HandleReportValue(data.WorldsData[data.selected.Name].hint, 1);
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Save("kh2fm-tracker-autosave.txt");
            Properties.Settings.Default.Save();
            broadcast.canClose = true;
            broadcast.Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowY = RestoreBounds.Top;
            Properties.Settings.Default.WindowX = RestoreBounds.Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.Width = RestoreBounds.Width;
            Properties.Settings.Default.Height = RestoreBounds.Height;
        }

        /// 
        /// Handle UI Changes
        /// 
        private void HandleReportValue(Image Hint, int delta)
        {
            if (data.mode != Mode.None)
                return;

            int num = 0;

            for (int i = 0; i < data.Numbers.Count; ++i)
            {
                if (Hint.Source == GetDataNumber("Y")[i])
                {
                    num = i;
                }
            }

            if (delta > 0)
                ++num;
            else
                --num;

            // cap hint value to 51
            if (num > 52)
                num = 52;

            if (num < 0)
                Hint.Source = GetDataNumber("Y")[0];
            else
                Hint.Source = GetDataNumber("Y")[num];

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num - 1);
        }

        public void SetReportValue(Image Hint, int value)
        {
            if (data.mode == Mode.DAHints && Hint == null)
                return;

            string Color = "Y";
            string location = Hint.Name.Substring(0, Hint.Name.Length - 4);
            if (data.WorldsData[location].hintedHint || data.WorldsData[location].complete)
                Color = "B";

            if (data.mode == Mode.DAHints)
            {
                if (value > 100)
                {
                    //for testing. basically if a number is blue then i either need to
                    //lower values or find a way to add triple digets
                    Color = "B";
                    value = 100;
                }

                if (data.WorldsData[location].containsGhost)
                {
                    Color = "G";
                }
            }
            else
            {
                if (value > 52)
                    value = 52;
            }


            if (value < 1 && (data.mode == Mode.AltHints || data.mode == Mode.OpenKHAltHints))
                Hint.Source = GetDataNumber(Color)[1];
            else if (value < 0)
                Hint.Source = GetDataNumber(Color)[0];
            else
                Hint.Source = GetDataNumber(Color)[value];
            
            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), value - 1);
        }

        public void IncrementCollected()
        {
            ++collected;
            if (collected > 51)
                collected = 51;

            Collected.Source = GetDataNumber("Y")[collected + 1];
            broadcast.Collected.Source = GetDataNumber("Y")[collected + 1];
        }

        public void DecrementCollected()
        {
            --collected;
            if (collected < 0)
                collected = 0;

            Collected.Source = GetDataNumber("Y")[collected + 1];
            broadcast.Collected.Source = GetDataNumber("Y")[collected + 1];
        }

        public void IncrementTotal()
        {
            ++total;
            if (total > 51)
                total = 51;

            CheckTotal.Source = GetDataNumber("Y")[total + 1];
            broadcast.CheckTotal.Source = GetDataNumber("Y")[total + 1];
        }

        public void DecrementTotal()
        {
            --total;
            if (total < 0)
                total = 0;

            CheckTotal.Source = GetDataNumber("Y")[total + 1];
            broadcast.CheckTotal.Source = GetDataNumber("Y")[total + 1];
        }

        public void SetHintText(string text)
        {
            if (SeedHashLoaded)
            {
                HashRow.Height = new GridLength(0, GridUnitType.Star);
                SeedHashVisible = false;
            }

            HintText.Content = text;
        }

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            broadcast.Width = 500;
            broadcast.Height = 680;
        }
    }
}
