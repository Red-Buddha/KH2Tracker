using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Linq;
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
        private int total = 62;
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
            VisitLockCheck();

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

            data.VisitLocks.Add(AuronWep);
            data.VisitLocks.Add(MulanWep);
            data.VisitLocks.Add(BeastWep);
            data.VisitLocks.Add(JackWep);
            data.VisitLocks.Add(SimbaWep);
            data.VisitLocks.Add(SparrowWep);
            data.VisitLocks.Add(AladdinWep);
            data.VisitLocks.Add(TronWep);
            data.VisitLocks.Add(MembershipCard);
            data.VisitLocks.Add(IceCream);
            data.VisitLocks.Add(Picture);

            data.WorldsData.Add("SorasHeart", new WorldData(SorasHeartTop, SorasHeart, null, SorasHeartHint, SorasHeartGrid, SorasHeartBar, false, 0));
            data.WorldsData.Add("DriveForms", new WorldData(DriveFormsTop, DriveForms, null, DriveFormsHint, DriveFormsGrid, DriveFormsBar, false, 0));
            data.WorldsData.Add("SimulatedTwilightTown", new WorldData(SimulatedTwilightTownTop, SimulatedTwilightTown, SimulatedTwilightTownProgression, SimulatedTwilightTownHint, SimulatedTwilightTownGrid, SimulatedTwilightTownBar, false, 0));
            data.WorldsData.Add("TwilightTown", new WorldData(TwilightTownTop, TwilightTown, TwilightTownProgression, TwilightTownHint, TwilightTownGrid, TwilightTownBar, false, 0));
            data.WorldsData.Add("HollowBastion", new WorldData(HollowBastionTop, HollowBastion, HollowBastionProgression, HollowBastionHint, HollowBastionGrid, HollowBastionBar, false, 0));
            data.WorldsData.Add("BeastsCastle", new WorldData(BeastsCastleTop, BeastsCastle, BeastsCastleProgression, BeastsCastleHint, BeastsCastleGrid, BeastsCastleBar, false, 0));
            data.WorldsData.Add("OlympusColiseum", new WorldData(OlympusColiseumTop, OlympusColiseum, OlympusColiseumProgression, OlympusColiseumHint, OlympusColiseumGrid, OlympusBar, false, 0));
            data.WorldsData.Add("Agrabah", new WorldData(AgrabahTop, Agrabah, AgrabahProgression, AgrabahHint, AgrabahGrid, AgrabahBar, false, 0));
            data.WorldsData.Add("LandofDragons", new WorldData(LandofDragonsTop, LandofDragons, LandofDragonsProgression, LandofDragonsHint, LandofDragonsGrid, LandofDragonsBar, false, 0));
            data.WorldsData.Add("HundredAcreWood", new WorldData(HundredAcreWoodTop, HundredAcreWood, HundredAcreWoodProgression, HundredAcreWoodHint, HundredAcreWoodGrid, HundredAcreWoodBar, false, 0));
            data.WorldsData.Add("PrideLands", new WorldData(PrideLandsTop, PrideLands, PrideLandsProgression, PrideLandsHint, PrideLandsGrid, PrideLandsBar, false, 0));
            data.WorldsData.Add("DisneyCastle", new WorldData(DisneyCastleTop, DisneyCastle, DisneyCastleProgression, DisneyCastleHint, DisneyCastleGrid, DisneyCastleBar, false, 0));
            data.WorldsData.Add("HalloweenTown", new WorldData(HalloweenTownTop, HalloweenTown, HalloweenTownProgression, HalloweenTownHint, HalloweenTownGrid, HalloweenTownBar, false, 0));
            data.WorldsData.Add("PortRoyal", new WorldData(PortRoyalTop, PortRoyal, PortRoyalProgression, PortRoyalHint, PortRoyalGrid, PortRoyalBar, false, 0));
            data.WorldsData.Add("SpaceParanoids", new WorldData(SpaceParanoidsTop, SpaceParanoids, SpaceParanoidsProgression, SpaceParanoidsHint, SpaceParanoidsGrid, SpaceParanoidsBar, false, 0));
            data.WorldsData.Add("TWTNW", new WorldData(TWTNWTop, TWTNW, TWTNWProgression, TWTNWHint, TWTNWGrid, TWTNWBar, false, 0));
            data.WorldsData.Add("GoA", new WorldData(GoATop, GoA, null, null, GoAGrid, GoABar, true, 0));
            data.WorldsData.Add("Atlantica", new WorldData(AtlanticaTop, Atlantica, AtlanticaProgression, AtlanticaHint, AtlanticaGrid, AtlanticaBar, false, 0));
            data.WorldsData.Add("PuzzSynth", new WorldData(PuzzSynthTop, PuzzSynth, null, PuzzSynthHint, PuzzSynthGrid, PuzzSynthBar, false, 0));

            data.ProgressKeys.Add("SimulatedTwilightTown", new List<string>() { "", "STTChests", "TwilightThorn", "Struggle", "ComputerRoom", "Axel", "DataRoxas" });
            data.ProgressKeys.Add("TwilightTown", new List<string>() { "", "TTChests", "MysteriousTower", "Sandlot", "Mansion", "BetwixtAndBetween", "DataAxel" });
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
            //Item toggles
            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            PromiseCharmToggle(PromiseCharmOption.IsChecked);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            ReportsToggle(ReportsOption.IsChecked);

            VisitLockOption.IsChecked = Properties.Settings.Default.WorldVisitLock;
            VisitLockToggle(VisitLockOption.IsChecked);

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            AbilitiesToggle(AbilitiesOption.IsChecked);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            TornPagesToggle(TornPagesOption.IsChecked);

            CureOption.IsChecked = Properties.Settings.Default.Cure;
            CureToggle(CureOption.IsChecked);

            FinalFormOption.IsChecked = Properties.Settings.Default.FinalForm;
            FinalFormToggle(FinalFormOption.IsChecked);

            //World Toggles
            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(SoraHeartOption.IsChecked);

            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(SimulatedOption.IsChecked);

            HundredAcreWoodOption.IsChecked = Properties.Settings.Default.HundredAcre;
            HundredAcreWoodToggle(HundredAcreWoodOption.IsChecked);

            AtlanticaOption.IsChecked = Properties.Settings.Default.Atlantica;
            AtlanticaToggle(AtlanticaOption.IsChecked);

            PuzzleOption.IsChecked = Properties.Settings.Default.Puzzle;
            PuzzleToggle(PuzzleOption.IsChecked);

            SynthOption.IsChecked = Properties.Settings.Default.Synth;
            SynthToggle(SynthOption.IsChecked);

            CavernOption.IsChecked = Properties.Settings.Default.Cavern;
            CavernToggle(CureOption.IsChecked);

            TerraOption.IsChecked = Properties.Settings.Default.Terra;
            TerraToggle(TerraOption.IsChecked);

            OCCupsOption.IsChecked = Properties.Settings.Default.OCCups;
            OCCupsToggle(OCCupsOption.IsChecked);

            //Visial Toggles
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

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

            CustomFolderOption.IsChecked = Properties.Settings.Default.CustomIcons;
            CustomImageToggle(null, null);

            SeedHashOption.IsChecked = Properties.Settings.Default.SeedHash;
            SeedHashToggle(null, null);

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
            GhostItemToggle(GhostItemOption.IsChecked);

            GhostMathOption.IsChecked = Properties.Settings.Default.GhostMath;
            GhostMathToggle(null, null);

            AutoDetectOption.IsChecked = Properties.Settings.Default.AutoDetect;
            AutoDetectToggle(null, null);

            CheckCountOption.IsChecked = Properties.Settings.Default.CheckCount;
            if (CheckCountOption.IsChecked)
                ShowCheckCountToggle(null, null);

            NextLevelCheckOption.IsChecked = Properties.Settings.Default.NextLevelCheck;
            if (NextLevelCheckOption.IsChecked)
                NextLevelCheckToggle(null, null);

            SoraLevel01Option.IsChecked = Properties.Settings.Default.WorldLevel1;
            if (SoraLevel01Option.IsChecked)
                SoraLevel01Toggle(null, null);

            SoraLevel50Option.IsChecked = Properties.Settings.Default.WorldLevel50;
            if (SoraLevel50Option.IsChecked)
                SoraLevel50Toggle(null, null);

            SoraLevel99Option.IsChecked = Properties.Settings.Default.WorldLevel99;
            if (SoraLevel99Option.IsChecked)
                SoraLevel99Toggle(null, null);

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
                    SetWorldNumber(data.WorldsData[button.Name].hint, -1, "Y");
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (data.WorldsData.ContainsKey(button.Name))
                {
                    string crossname = button.Name + "Cross";
                    Image Cross = data.WorldsData[button.Name].top.FindName(crossname) as Image;
                    Image CrossB = broadcast.FindName(crossname) as Image;

                    if (Cross != null)
                    {
                        if (Cross.Visibility == Visibility.Collapsed)
                            Cross.Visibility = Visibility.Visible;
                        else
                            Cross.Visibility = Visibility.Collapsed;
                    }
                    if (CrossB != null)
                    {
                        if (CrossB.Visibility == Visibility.Collapsed)
                            CrossB.Visibility = Visibility.Visible;
                        else
                            CrossB.Visibility = Visibility.Collapsed;
                    }
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

        //for changing an icon's appearance on right click. i need to revisit this soon
        //private void OnMouseRightClick(object sender, MouseWheelEventArgs e)
        //{
        //    Button button = sender as Button;
        //    //BitmapImage Normal = 
        //    string test = SecondChance.ContentStringFormat;
        //    Console.WriteLine(test);
        //    //SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");
        //
        //    //if (e.ChangedButton == MouseButton.Right)
        //    //{
        //    //    HandleReportValue(data.WorldsData[button.Name].hint, e.Delta);
        //    //}
        //}

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

        private void HandleReportValue(Grid Hint, int delta)
        {
            //return if the a hint mode is loaded
            if (data.mode != Mode.None)
                return;

            int num = GetWorldNumber(Hint);

            if (delta > 0)
                ++num;
            else
                --num;

            // cap hint value to 51
            if (num > 999)
                num = 999;

            SetWorldNumber(Hint, num, "Y");
            broadcast.SetFoundNumber(Hint, null);
        }

        public void SetReportValue(Grid Hint, int value)
        {
            if (data.mode == Mode.DAHints && Hint == null)
                return;

            string location = Hint.Name.Substring(0, Hint.Name.Length - 4);
            string Color = "Y"; //default

            if (data.WorldsData[location].containsGhost) //turn green if it conains ghost item
                Color = "G";

            if (data.WorldsData[location].hintedHint || data.WorldsData[location].complete) //turn blue if it's marked as hinted hint or complete
                Color = "B";

            SetWorldNumber(Hint, value, Color);

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), value);
        }

        public void IncrementCollected()
        {
            ++collected;
            //i don't want to update this code every time a new IC is added.
            //just setting it to the max of 99 for now. if it breaks i know where to look
            if (collected > 99)
                collected = 99;

            List<BitmapImage> CollectedNum = UpdateNumber(collected, "Y");
            if (collected < 10)
                CollectedNum[1] = null;

            Collected_01.Source = CollectedNum[0];
            Collected_10.Source = CollectedNum[1];
            broadcast.Collected_01.Source = CollectedNum[0];
            broadcast.Collected_10.Source = CollectedNum[1];
        }

        public void DecrementCollected()
        {
            --collected;
            if (collected < 0)
                collected = 0;

            List<BitmapImage> CollectedNum = UpdateNumber(collected, "Y");
            if (collected < 10)
                CollectedNum[1] = null;

            Collected_01.Source = CollectedNum[0];
            Collected_10.Source = CollectedNum[1];
            broadcast.Collected_01.Source = CollectedNum[0];
            broadcast.Collected_10.Source = CollectedNum[1];
        }

        public void IncrementTotal()
        {
            ++total;
            if (total > 99)
                total = 99;

            List<BitmapImage> TotalNum = UpdateNumber(total, "Y");

            //CheckTotal.Source = GetDataNumber("Y")[total + 1];
            CheckTotal_01.Source = TotalNum[0];
            CheckTotal_10.Source = TotalNum[1];
            //broadcast.CheckTotal.Source = GetDataNumber("Y")[total + 1];
            broadcast.CheckTotal_01.Source = TotalNum[0];
            broadcast.CheckTotal_10.Source = TotalNum[1];
        }

        public void DecrementTotal()
        {
            --total;
            if (total < 0)
                total = 0;

            List<BitmapImage> TotalNum = UpdateNumber(total, "Y");
            //CheckTotal.Source = GetDataNumber("Y")[total + 1];
            CheckTotal_01.Source = TotalNum[0];
            CheckTotal_10.Source = TotalNum[1];
            //broadcast.CheckTotal.Source = GetDataNumber("Y")[total + 1];
            broadcast.CheckTotal_01.Source = TotalNum[0];
            broadcast.CheckTotal_10.Source = TotalNum[1];
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

        //might not use???
        //public List<BitmapImage> UpdateNumber(int num, string color)
        //{
        //    int[] FinalNum = new int[] { 1, 1, 1 }; //Default 000
        //    bool OldMode = Properties.Settings.Default.OldNum;
        //    bool CustomMode = Properties.Settings.Default.CustomIcons;
        //    List<BitmapImage> NormalNum = data.SingleNumbers;
        //    List<BitmapImage> BlueNum = data.BlueSingleNumbers;
        //    List<BitmapImage> GreenNum = data.GreenSingleNumbers;
        //    List<BitmapImage> NumColor;
        //
        //    //Get correct number visuals
        //    {
        //        if (OldMode)
        //        {
        //            NormalNum = data.OldSingleNumbers;
        //            BlueNum = data.OldBlueSingleNumbers;
        //            GreenNum = data.OldGreenSingleNumbers;
        //        }
        //
        //        if (CustomMode)
        //        {
        //            if (CustomNumbersFound)
        //            {
        //                NormalNum = data.CustomSingleNumbers;
        //            }
        //            if (CustomBlueNumbersFound)
        //            {
        //                BlueNum = data.CustomBlueSingleNumbers;
        //            }
        //            if (CustomGreenNumbersFound)
        //            {
        //                GreenNum = data.CustomGreenSingleNumbers;
        //            }
        //        }
        //    }
        //
        //    //split number into separate digits
        //    List<int> listOfInts = new List<int>();
        //    while (num > 0)
        //    {
        //        listOfInts.Add(num % 10);
        //        num /= 10;
        //    }
        //
        //    //Set number images depending on number of digits
        //    if (listOfInts.Count == 3)
        //    {
        //        FinalNum[0] = listOfInts[0];
        //        FinalNum[1] = listOfInts[1];
        //        FinalNum[2] = listOfInts[2];
        //    }
        //    else if (listOfInts.Count == 2)
        //    {
        //        FinalNum[0] = listOfInts[0];
        //        FinalNum[1] = listOfInts[1];
        //    }
        //    else if (listOfInts.Count == 1)
        //    {
        //        FinalNum[0] = listOfInts[0];
        //    }
        //
        //    //Get color
        //    switch (color)
        //    {
        //        case "Y":
        //            NumColor = NormalNum;
        //            break;
        //        case "B":
        //            NumColor = BlueNum;
        //            break;
        //        case "G":
        //            NumColor = GreenNum;
        //            break;
        //        default:
        //            NumColor = NormalNum;
        //            break;
        //    }
        //
        //
        //    List<BitmapImage> Numberlist = new List<BitmapImage>
        //    {
        //        NumColor[FinalNum[0]],
        //        NumColor[FinalNum[1]],
        //        NumColor[FinalNum[2]]
        //    };
        //
        //    return Numberlist;
        //}

        public List<BitmapImage> UpdateNumber(int num, string color)
        {
            //we need to get all 3 sources from the grid
            int[] FinalNum = new int[] { 0, 0, 0 }; //Default 000
            bool OldMode = Properties.Settings.Default.OldNum;
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            List<BitmapImage> NormalNum = data.SingleNumbers;
            List<BitmapImage> BlueNum = data.BlueSingleNumbers;
            List<BitmapImage> GreenNum = data.GreenSingleNumbers;
            List<BitmapImage> NumColor;
            List<BitmapImage> Numberlist = new List<BitmapImage>();

            //Get correct number visuals
            {
                if (OldMode)
                {
                    NormalNum = data.OldSingleNumbers;
                    BlueNum = data.OldBlueSingleNumbers;
                    GreenNum = data.OldGreenSingleNumbers;
                }

                if (CustomMode)
                {
                    if (CustomNumbersFound)
                    {
                        NormalNum = data.CustomSingleNumbers;
                    }
                    if (CustomBlueNumbersFound)
                    {
                        BlueNum = data.CustomBlueSingleNumbers;
                    }
                    if (CustomGreenNumbersFound)
                    {
                        GreenNum = data.CustomGreenSingleNumbers;
                    }
                }
            }
            //Get color
            switch (color)
            {
                case "Y":
                    NumColor = NormalNum;
                    break;
                case "B":
                    NumColor = BlueNum;
                    break;
                case "G":
                    NumColor = GreenNum;
                    break;
                default:
                    NumColor = NormalNum;
                    break;
            }

            //if int is below 0 then we use the question mark and return
            if (num < 0)
            {
                Numberlist.Add(NumColor[10]);
                Numberlist.Add(NumColor[10]);
                Numberlist.Add(NumColor[10]);

                return Numberlist;
            }

            //split number into separate digits
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num /= 10;
            }

            //Set number images depending on number of digits
            if (listOfInts.Count == 3)
            {
                FinalNum[0] = listOfInts[0];
                FinalNum[1] = listOfInts[1];
                FinalNum[2] = listOfInts[2];
            }
            else if (listOfInts.Count == 2)
            {
                FinalNum[0] = listOfInts[0];
                FinalNum[1] = listOfInts[1];
            }
            else if (listOfInts.Count == 1)
            {
                FinalNum[0] = listOfInts[0];
            }

            Numberlist.Add(NumColor[FinalNum[0]]);
            Numberlist.Add(NumColor[FinalNum[1]]);
            Numberlist.Add(NumColor[FinalNum[2]]);

            return Numberlist;
        }

        public void SetWorldNumber(Grid hintgrid, int worldnum, string color)
        {
            string worldname = hintgrid.Name;
            List<BitmapImage> WorldNumImage = UpdateNumber(worldnum, color);
            int ChildCount = VisualTreeHelper.GetChildrenCount(hintgrid);
            bool number10s = false;
            bool number100s = false;

            if (worldnum > 99)
                number100s = true;
            if (worldnum > 9)
                number10s = true;

            for (int i = 0; i < ChildCount; i++)
            {
                var child = VisualTreeHelper.GetChild(hintgrid, i) as Image;

                if (child == null)
                    continue;
                if (child is Image && child.Name.Equals(worldname + "_001"))
                {
                    child.Source = WorldNumImage[0];
                    continue;
                }
                if (child is Image && child.Name.Equals(worldname + "_010"))
                {
                    child.Source = WorldNumImage[1];

                    string name = WorldNumImage[1].ToString();
                    if (!name.Contains("Question_Mark") && number10s)
                        child.Visibility = Visibility.Visible;
                    else if (!number10s)
                        child.Visibility = Visibility.Hidden;

                    continue;
                }
                if (child is Image && child.Name.Equals(worldname + "_100"))
                {
                    child.Source = WorldNumImage[2];

                    string name = WorldNumImage[2].ToString();
                    if (!name.Contains("Question_Mark") && number100s)
                        child.Visibility = Visibility.Visible;
                    else if (!number100s)
                        child.Visibility = Visibility.Hidden;

                    continue;
                }
            }

        }

        public int GetWorldNumber(Grid hintgrid)
        {
            int Num100 = 0;
            int Num010 = 0;
            int Num001 = 0;
            string worldname = hintgrid.Name;

            int ChildCount = VisualTreeHelper.GetChildrenCount(hintgrid);

            for (int i = 0; i < ChildCount; i++)
            {
                var child = VisualTreeHelper.GetChild(hintgrid, i) as Image;

                if (child == null)
                    continue;

                if (child is Image && child.Name.Equals(worldname + "_001"))
                {
                    Num001 = GetImageNumber(child.Source.ToString());
                    continue;
                }

                if (child is Image && child.Name.Equals(worldname + "_010"))
                {
                    Num010 = GetImageNumber(child.Source.ToString());
                    continue;
                }

                if (child is Image && child.Name.Equals(worldname + "_100"))
                {
                    Num100 = GetImageNumber(child.Source.ToString());
                    continue;
                }
            }

            int Finalnum;

            //check for question marks
            if (Num001 == 10 && Num010 == 10 && Num100 == 10)
            {
                Finalnum = -1;
            }
            else
                Finalnum = Num001 + (Num010 * 10) + (Num100 * 100);

            return Finalnum;
        }

        public int GetImageNumber(string ImagePath)
        {
            int number = 10;

            if (!ImagePath.EndsWith("QuestionMark.png") && ImagePath != null)
            {
                string val = ImagePath;
                val = val.Substring(val.LastIndexOf('/') + 1);
                number = int.Parse(val.Substring(0, val.IndexOf('.')));

                if (number > 9 || number < 0)
                    number = 10;
            }

            return number;
        }

        public void VisitLockCheck()
        {
            foreach (string World in data.WorldsData.Keys.ToList())
            {
                //could probably be handled better. oh well
                switch(World)
                {
                    case "TwilightTown":
                        switch (data.WorldsData["TwilightTown"].visitLocks)
                        {
                            case 0:
                                TwilightTownLock_1.Visibility = Visibility.Collapsed;
                                TwilightTownLock_2.Visibility = Visibility.Collapsed;
                                broadcast.TwilightTownLock_1.Visibility = Visibility.Collapsed;
                                broadcast.TwilightTownLock_2.Visibility = Visibility.Collapsed;
                                break;
                            case 1:
                                TwilightTownLock_1.Visibility = Visibility.Visible;
                                TwilightTownLock_2.Visibility = Visibility.Collapsed;
                                broadcast.TwilightTownLock_1.Visibility = Visibility.Visible;
                                broadcast.TwilightTownLock_2.Visibility = Visibility.Collapsed;
                                break;
                            case 10:
                                TwilightTownLock_1.Visibility = Visibility.Collapsed;
                                TwilightTownLock_2.Visibility = Visibility.Visible;
                                broadcast.TwilightTownLock_1.Visibility = Visibility.Collapsed;
                                broadcast.TwilightTownLock_2.Visibility = Visibility.Visible;
                                break;
                            default:
                                TwilightTownLock_1.Visibility = Visibility.Visible;
                                TwilightTownLock_2.Visibility = Visibility.Visible;
                                broadcast.TwilightTownLock_1.Visibility = Visibility.Visible;
                                broadcast.TwilightTownLock_2.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "HollowBastion":
                        switch (data.WorldsData["HollowBastion"].visitLocks)
                        {
                            case 0:
                                HollowBastionLock.Visibility = Visibility.Hidden;
                                broadcast.HollowBastionLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                HollowBastionLock.Visibility = Visibility.Visible;
                                broadcast.HollowBastionLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "BeastsCastle":
                        switch (data.WorldsData["BeastsCastle"].visitLocks)
                        {
                            case 0:
                                BeastsCastleLock.Visibility = Visibility.Hidden;
                                broadcast.BeastsCastleLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                BeastsCastleLock.Visibility = Visibility.Visible;
                                broadcast.BeastsCastleLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "OlympusColiseum":
                        switch (data.WorldsData["OlympusColiseum"].visitLocks)
                        {
                            case 0:
                                OlympusColiseumLock.Visibility = Visibility.Hidden;
                                broadcast.OlympusColiseumLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                OlympusColiseumLock.Visibility = Visibility.Visible;
                                broadcast.OlympusColiseumLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "Agrabah":
                        switch (data.WorldsData["Agrabah"].visitLocks)
                        {
                            case 0:
                                AgrabahLock.Visibility = Visibility.Hidden;
                                broadcast.AgrabahLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                AgrabahLock.Visibility = Visibility.Visible;
                                broadcast.AgrabahLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "LandofDragons":
                        switch (data.WorldsData["LandofDragons"].visitLocks)
                        {
                            case 0:
                                LandofDragonsLock.Visibility = Visibility.Hidden;
                                broadcast.LandofDragonsLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                LandofDragonsLock.Visibility = Visibility.Visible;
                                broadcast.LandofDragonsLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "PrideLands":
                        switch (data.WorldsData["PrideLands"].visitLocks)
                        {
                            case 0:
                                PrideLandsLock.Visibility = Visibility.Hidden;
                                broadcast.PrideLandsLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                PrideLandsLock.Visibility = Visibility.Visible;
                                broadcast.PrideLandsLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "HalloweenTown":
                        switch (data.WorldsData["HalloweenTown"].visitLocks)
                        {
                            case 0:
                                HalloweenTownLock.Visibility = Visibility.Hidden;
                                broadcast.HalloweenTownLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                HalloweenTownLock.Visibility = Visibility.Visible;
                                broadcast.HalloweenTownLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "PortRoyal":
                        switch (data.WorldsData["PortRoyal"].visitLocks)
                        {
                            case 0:
                                PortRoyalLock.Visibility = Visibility.Hidden;
                                broadcast.PortRoyalLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                PortRoyalLock.Visibility = Visibility.Visible;
                                broadcast.PortRoyalLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case "SpaceParanoids":
                        switch (data.WorldsData["SpaceParanoids"].visitLocks)
                        {
                            case 0:
                                SpaceParanoidsLock.Visibility = Visibility.Hidden;
                                broadcast.SpaceParanoidsLock.Visibility = Visibility.Hidden;
                                break;
                            default:
                                SpaceParanoidsLock.Visibility = Visibility.Visible;
                                broadcast.SpaceParanoidsLock.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public int GetGhostPoints(WorldGrid worlditems)
        {
            int points = 0;

            foreach (Item ghost in Data.GhostItems.Values.ToList())
            {
                if (worlditems.Children.Contains(ghost))
                {
                    points += data.PointsDatanew[GetGhostType[ghost.Name]];
                }
            }

            return points;
        }

        private Dictionary<string, string> GetGhostType = new Dictionary<string, string>()
        {
            {"Ghost_Report1", "report"},
            {"Ghost_Report2", "report"},
            {"Ghost_Report3", "report"},
            {"Ghost_Report4", "report"},
            {"Ghost_Report5", "report"},
            {"Ghost_Report6", "report"},
            {"Ghost_Report7", "report"},
            {"Ghost_Report8", "report"},
            {"Ghost_Report9", "report"},
            {"Ghost_Report10", "report"},
            {"Ghost_Report11", "report"},
            {"Ghost_Report12", "report"},
            {"Ghost_Report13", "report"},
            {"Ghost_Fire1", "magic"},
            {"Ghost_Fire2", "magic"},
            {"Ghost_Fire3", "magic"},
            {"Ghost_Blizzard1", "magic"},
            {"Ghost_Blizzard2", "magic"},
            {"Ghost_Blizzard3", "magic"},
            {"Ghost_Thunder1", "magic"},
            {"Ghost_Thunder2", "magic"},
            {"Ghost_Thunder3", "magic"},
            {"Ghost_Cure1", "magic"},
            {"Ghost_Cure2", "magic"},
            {"Ghost_Cure3", "magic"},
            {"Ghost_Reflect1", "magic"},
            {"Ghost_Reflect2", "magic"},
            {"Ghost_Reflect3", "magic"},
            {"Ghost_Magnet1", "magic"},
            {"Ghost_Magnet2", "magic"},
            {"Ghost_Magnet3", "magic"},
            {"Ghost_Valor", "form"},
            {"Ghost_Wisdom", "form"},
            {"Ghost_Limit", "form"},
            {"Ghost_Master", "form"},
            {"Ghost_Final", "form"},
            {"Ghost_OnceMore", "ability"},
            {"Ghost_SecondChance", "ability"},
            {"Ghost_TornPage1", "page"},
            {"Ghost_TornPage2", "page"},
            {"Ghost_TornPage3", "page"},
            {"Ghost_TornPage4", "page"},
            {"Ghost_TornPage5", "page"},
            {"Ghost_Baseball", "summon"},
            {"Ghost_Lamp", "summon"},
            {"Ghost_Ukulele", "summon"},
            {"Ghost_Feather", "summon"},
            {"Ghost_Connection", "proof"},
            {"Ghost_Nonexistence", "proof"},
            {"Ghost_Peace", "proof"},
            {"Ghost_PromiseCharm", "proof"},
            {"Ghost_AuronWep", "visit"},
            {"Ghost_MulanWep", "visit"},
            {"Ghost_BeastWep", "visit"},
            {"Ghost_JackWep", "visit"},
            {"Ghost_SimbaWep", "visit"},
            {"Ghost_SparrowWep", "visit"},
            {"Ghost_AladdinWep", "visit"},
            {"Ghost_TronWep", "visit"},
            {"Ghost_MembershipCard", "visit"},
            {"Ghost_IceCream", "visit"},
            {"Ghost_Picture", "visit"}
        };

    }

}