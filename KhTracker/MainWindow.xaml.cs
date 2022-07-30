using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private int total;
        public static int PointTotal = 0;
        public int DeathCounter = 0;

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
            AutoDetectOption.IsChecked = Properties.Settings.Default.AutoDetect;
            if (AutoDetectOption.IsChecked)
            {
                AutoDetectToggle(null, null);
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

            data.WorldsData.Add("SorasHeart",               new WorldData(SorasHeartTop, SorasHeart, null, SorasHeartHint, SorasHeartGrid, false, 0));
            data.WorldsData.Add("DriveForms",               new WorldData(DriveFormsTop, DriveForms, null, DriveFormsHint, DriveFormsGrid, false, 0));
            data.WorldsData.Add("SimulatedTwilightTown",    new WorldData(SimulatedTwilightTownTop, SimulatedTwilightTown, SimulatedTwilightTownProgression, SimulatedTwilightTownHint, SimulatedTwilightTownGrid, false, 0));
            data.WorldsData.Add("TwilightTown",             new WorldData(TwilightTownTop, TwilightTown, TwilightTownProgression, TwilightTownHint, TwilightTownGrid, false, 0));
            data.WorldsData.Add("HollowBastion",            new WorldData(HollowBastionTop, HollowBastion, HollowBastionProgression, HollowBastionHint, HollowBastionGrid, false, 0));
            data.WorldsData.Add("BeastsCastle",             new WorldData(BeastsCastleTop, BeastsCastle, BeastsCastleProgression, BeastsCastleHint, BeastsCastleGrid, false, 0));
            data.WorldsData.Add("OlympusColiseum",          new WorldData(OlympusColiseumTop, OlympusColiseum, OlympusColiseumProgression, OlympusColiseumHint, OlympusColiseumGrid, false, 0));
            data.WorldsData.Add("Agrabah",                  new WorldData(AgrabahTop, Agrabah, AgrabahProgression, AgrabahHint, AgrabahGrid, false, 0));
            data.WorldsData.Add("LandofDragons",            new WorldData(LandofDragonsTop, LandofDragons, LandofDragonsProgression, LandofDragonsHint, LandofDragonsGrid, false, 0));
            data.WorldsData.Add("HundredAcreWood",          new WorldData(HundredAcreWoodTop, HundredAcreWood, HundredAcreWoodProgression, HundredAcreWoodHint, HundredAcreWoodGrid, false, 0));
            data.WorldsData.Add("PrideLands",               new WorldData(PrideLandsTop, PrideLands, PrideLandsProgression, PrideLandsHint, PrideLandsGrid, false, 0));
            data.WorldsData.Add("DisneyCastle",             new WorldData(DisneyCastleTop, DisneyCastle, DisneyCastleProgression, DisneyCastleHint, DisneyCastleGrid, false, 0));
            data.WorldsData.Add("HalloweenTown",            new WorldData(HalloweenTownTop, HalloweenTown, HalloweenTownProgression, HalloweenTownHint, HalloweenTownGrid, false, 0));
            data.WorldsData.Add("PortRoyal",                new WorldData(PortRoyalTop, PortRoyal, PortRoyalProgression, PortRoyalHint, PortRoyalGrid, false, 0));
            data.WorldsData.Add("SpaceParanoids",           new WorldData(SpaceParanoidsTop, SpaceParanoids, SpaceParanoidsProgression, SpaceParanoidsHint, SpaceParanoidsGrid, false, 0));
            data.WorldsData.Add("TWTNW",                    new WorldData(TWTNWTop, TWTNW, TWTNWProgression, TWTNWHint, TWTNWGrid, false, 0));
            data.WorldsData.Add("GoA",                      new WorldData(GoATop, GoA, null, null, GoAGrid, true, 0));
            data.WorldsData.Add("Atlantica",                new WorldData(AtlanticaTop, Atlantica, AtlanticaProgression, AtlanticaHint, AtlanticaGrid, false, 0));
            data.WorldsData.Add("PuzzSynth",                new WorldData(PuzzSynthTop, PuzzSynth, null, PuzzSynthHint, PuzzSynthGrid, false, 0));

            data.ProgressKeys.Add("SimulatedTwilightTown",  new List<string>() { "", "STTChests", "TwilightThorn", "Struggle", "ComputerRoom", "Axel", "DataRoxas" });
            data.ProgressKeys.Add("TwilightTown",           new List<string>() { "", "TTChests", "MysteriousTower", "Sandlot", "Mansion", "BetwixtAndBetween", "DataAxel" });
            data.ProgressKeys.Add("HollowBastion",          new List<string>() { "", "HBChests", "Bailey", "AnsemStudy", "Corridor", "Dancers", "HBDemyx", "FinalFantasy", "1000Heartless", "Sephiroth", "DataDemyx" });
            data.ProgressKeys.Add("BeastsCastle",           new List<string>() { "", "BCChests", "Thresholder", "Beast", "DarkThorn", "Dragoons", "Xaldin", "DataXaldin" });
            data.ProgressKeys.Add("OlympusColiseum",        new List<string>() { "", "OCChests", "Cerberus", "OCDemyx", "OCPete", "Hydra", "AuronStatue", "Hades", "Zexion" });
            data.ProgressKeys.Add("Agrabah",                new List<string>() { "", "AGChests", "Abu", "Chasm", "TreasureRoom", "Lords", "Carpet", "GenieJafar", "Lexaeus" });
            data.ProgressKeys.Add("LandofDragons",          new List<string>() { "", "LoDChests", "Cave", "Summmit", "ShanYu", "ThroneRoom", "StormRider", "DataXigbar" });
            data.ProgressKeys.Add("HundredAcreWood",        new List<string>() { "", "Pooh", "Piglet", "Rabbit", "Kanga", "SpookyCave", "StarryHill" });
            data.ProgressKeys.Add("PrideLands",             new List<string>() { "", "PLChests", "Simba", "Scar", "GroundShaker", "DataSaix" });
            data.ProgressKeys.Add("DisneyCastle",           new List<string>() { "", "DCChests", "Minnie", "OldPete", "Windows", "BoatPete", "DCPete", "Marluxia", "LingeringWill" });
            data.ProgressKeys.Add("HalloweenTown",          new List<string>() { "", "HTChests", "CandyCaneLane", "PrisonKeeper", "OogieBoogie", "Presents", "Experiment", "Vexen" });
            data.ProgressKeys.Add("PortRoyal",              new List<string>() { "", "PRChests", "Town", "Barbossa", "Gambler", "GrimReaper", "DataLuxord" });
            data.ProgressKeys.Add("SpaceParanoids",         new List<string>() { "", "SPChests", "Screens", "HostileProgram", "SolarSailer", "MCP", "Larxene" });
            data.ProgressKeys.Add("TWTNW",                  new List<string>() { "", "TWTNWChests", "Roxas", "Xigbar", "Luxord", "Saix", "Xemnas1", "DataXemnas" });
            data.ProgressKeys.Add("Atlantica",              new List<string>() { "", "Tutorial", "Ursula", "NewDay" });

            foreach (Grid itemrow in ItemPool.Children)
            {
                foreach (object item in itemrow.Children)
                {
                    if (item is Item)
                    {
                        Item check = item as Item;
                        if (!check.Name.StartsWith("Ghost_"))
                        {
                            data.Items.Add(check.Name, new Tuple<Item, Grid>(check, check.Parent as Grid));  //list of all valid items
                            //data.ItemsGrid.Add(check.Parent as Grid);   //list of grids each item belongs to
                            ++total;
                        }
                        else
                        {
                            data.GhostItems.Add(check.Name, check);     //list of all valid ghost items (why is this a dictionary????)
                        }
                    }
                }
            }

            broadcast = new BroadcastWindow(data);
        }

        private void InitOptions()
        {
            #region Options

            BroadcastStartupOption.IsChecked = Properties.Settings.Default.BroadcastStartup;
            BroadcastStartupToggle(null, null);

            TopMostOption.IsChecked = Properties.Settings.Default.TopMost;
            TopMostToggle(null, null);

            DragAndDropOption.IsChecked = Properties.Settings.Default.DragDrop;
            DragDropToggle(null, null);

            #endregion

            #region Toggles

            //Items
            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            ReportsToggle(ReportsOption.IsChecked);

            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            PromiseCharmToggle(PromiseCharmOption.IsChecked);

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            AbilitiesToggle(AbilitiesOption.IsChecked);

            AntiFormOption.IsChecked = Properties.Settings.Default.AntiForm;
            AntiFormToggle(AntiFormOption.IsChecked);

            VisitLockOption.IsChecked = Properties.Settings.Default.WorldVisitLock;
            VisitLockToggle(VisitLockOption.IsChecked);

            ExtraChecksOption.IsChecked = Properties.Settings.Default.ExtraChecks;
            ExtraChecksToggle(ExtraChecksOption.IsChecked);

            //Visual
            SeedHashOption.IsChecked = Properties.Settings.Default.SeedHash;
            SeedHashToggle(SeedHashOption.IsChecked);

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

            FormsGrowthOption.IsChecked = Properties.Settings.Default.FormsGrowth;
            FormsGrowthToggle(null, null);

            BroadcastGrowthOption.IsChecked = Properties.Settings.Default.BroadcastGrowth;
            BroadcastGrowthToggle(null, null);

            BroadcastStatsOption.IsChecked = Properties.Settings.Default.BroadcastStats;
            BroadcastStatsToggle(null, null);

            //points related
            GhostItemOption.IsChecked = Properties.Settings.Default.GhostItem;
            GhostItemToggle(GhostItemOption.IsChecked);

            GhostMathOption.IsChecked = Properties.Settings.Default.GhostMath;
            GhostMathToggle(null, null);

            CheckCountOption.IsChecked = Properties.Settings.Default.CheckCount;
            ShowCheckCountToggle(CheckCountOption.IsChecked);

            //Levelvisuals
            NextLevelCheckOption.IsChecked = Properties.Settings.Default.NextLevelCheck;
            NextLevelCheckToggle(NextLevelCheckOption.IsChecked);

            DeathCounterOption.IsChecked = Properties.Settings.Default.DeathCounter;
            DeathCounterToggle(DeathCounterOption.IsChecked);

            SoraLevel01Option.IsChecked = Properties.Settings.Default.WorldLevel1;
            SoraLevel50Option.IsChecked = Properties.Settings.Default.WorldLevel50;
            SoraLevel99Option.IsChecked = Properties.Settings.Default.WorldLevel99;
            if (SoraLevel01Option.IsChecked)
                SoraLevel01Toggle(null, null);
            if (SoraLevel50Option.IsChecked)
                SoraLevel50Toggle(null, null);          
            if (SoraLevel99Option.IsChecked)
                SoraLevel99Toggle(null, null);

            #endregion

            #region Visual

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


            CustomFolderOption.IsChecked = Properties.Settings.Default.CustomIcons;
            CustomImageToggle(null, null);

            //testing background settings stuff (i thought this would be simplier than the above methods)
            //maybe i was wrong. (at least everything is done by 2 settings instead of 8)
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

            #endregion

            #region Worlds

            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(SoraHeartOption.IsChecked);

            DrivesOption.IsChecked = Properties.Settings.Default.Drives;
            DrivesToggle(DrivesOption.IsChecked);

            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(SimulatedOption.IsChecked);

            TwilightTownOption.IsChecked = Properties.Settings.Default.TwilightTown;
            TwilightTownToggle(TwilightTownOption.IsChecked);

            HollowBastionOption.IsChecked = Properties.Settings.Default.HollowBastion;
            HollowBastionToggle(HollowBastionOption.IsChecked);

            BeastCastleOption.IsChecked = Properties.Settings.Default.BeastCastle;
            BeastCastleToggle(BeastCastleOption.IsChecked);

            OlympusOption.IsChecked = Properties.Settings.Default.Olympus;
            OlympusToggle(OlympusOption.IsChecked);

            AgrabahOption.IsChecked = Properties.Settings.Default.Agrabah;
            AgrabahToggle(AgrabahOption.IsChecked);

            LandofDragonsOption.IsChecked = Properties.Settings.Default.LandofDragons;
            LandofDragonsToggle(LandofDragonsOption.IsChecked);

            DisneyCastleOption.IsChecked = Properties.Settings.Default.DisneyCastle;
            DisneyCastleToggle(DisneyCastleOption.IsChecked);

            PrideLandsOption.IsChecked = Properties.Settings.Default.PrideLands;
            PrideLandsToggle(PrideLandsOption.IsChecked);

            PortRoyalOption.IsChecked = Properties.Settings.Default.PortRoyal;
            PortRoyalToggle(PortRoyalOption.IsChecked);

            HalloweenTownOption.IsChecked = Properties.Settings.Default.HalloweenTown;
            HalloweenTownToggle(HalloweenTownOption.IsChecked);

            SpaceParanoidsOption.IsChecked = Properties.Settings.Default.SpaceParanoids;
            SpaceParanoidsToggle(SpaceParanoidsOption.IsChecked);

            TWTNWOption.IsChecked = Properties.Settings.Default.TWTNW;
            TWTNWToggle(TWTNWOption.IsChecked);

            HundredAcreWoodOption.IsChecked = Properties.Settings.Default.HundredAcre;
            HundredAcreWoodToggle(HundredAcreWoodOption.IsChecked);

            AtlanticaOption.IsChecked = Properties.Settings.Default.Atlantica;
            AtlanticaToggle(AtlanticaOption.IsChecked);

            PuzzleOption.IsChecked = Properties.Settings.Default.Puzzle;
            PuzzleToggle(PuzzleOption.IsChecked);

            SynthOption.IsChecked = Properties.Settings.Default.Synth;
            SynthToggle(SynthOption.IsChecked);

            #endregion

            ///TODO: repurpose for legacy layout mode later
            //LegacyOption.IsChecked = Properties.Settings.Default.Legacy;
            //if (LegacyOption.IsChecked)
            //    LegacyToggle(null, null);

            Top = Properties.Settings.Default.WindowY;
            Left = Properties.Settings.Default.WindowX;

            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
        }

        /// 
        /// Input Handling
        /// 
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;

            switch(e.ChangedButton)
            {
                case MouseButton.Left:
                    if (data.selected != null)
                    {
                        foreach (var Box in data.WorldsData[data.selected.Name].top.Children.OfType<Rectangle>())
                        {
                            Box.Fill = (SolidColorBrush)FindResource("DefaultRec");
                        }
                    }
                    data.selected = button;
                    foreach (var Box in data.WorldsData[button.Name].top.Children.OfType<Rectangle>())
                    {
                        Box.Fill = (SolidColorBrush)FindResource("SelectedRec");
                    }
                    break;
                case MouseButton.Right:
                    if (data.WorldsData.ContainsKey(button.Name))
                    {
                        string crossname = button.Name + "Cross";

                        if (data.WorldsData[button.Name].top.FindName(crossname) is Image Cross)
                        {
                            if (Cross.Visibility == Visibility.Collapsed)
                                Cross.Visibility = Visibility.Visible;
                            else
                                Cross.Visibility = Visibility.Collapsed;
                        }
                        if (broadcast.FindName(crossname) is Image CrossB)
                        {
                            if (CrossB.Visibility == Visibility.Collapsed)
                                CrossB.Visibility = Visibility.Visible;
                            else
                                CrossB.Visibility = Visibility.Collapsed;
                        }
                    }
                    break;
                case MouseButton.Middle:
                    if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].value != null && data.mode == Mode.None)
                    {
                        data.WorldsData[button.Name].value.Text = "?";
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].value != null)
            {
                ManualWorldValue(data.WorldsData[button.Name].value, e.Delta);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].value != null)
                {
                    SetWorldValue(data.WorldsData[data.selected.Name].value, -1);
                }
            }
            if (e.Key == Key.PageUp && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].value != null)
                {
                    SetWorldValue(data.WorldsData[data.selected.Name].value, 1);
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

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            broadcast.Width = 500;
            broadcast.Height = 680;
        }

        /// 
        /// Handle UI Changes
        /// 

        //Used for when no hints are loaded. use the scroll wheel to change world number.
        private void ManualWorldValue(OutlinedTextBlock Hint, int delta)
        {
            //return if the a hint mode is loaded
            if (data.mode != Mode.None)
                return;

            int num;

            //get current number
            if (Hint.Text == "?")
            {
                if (delta > 0) 
                    num = -1; // if adding then start at -1 so next number is 0
                else
                    num = 0; //if subtracting start at 0 so next number is -1
            }
            else
                num = int.Parse(Hint.Text);

            if (delta > 0)
                ++num;
            else
                --num;

            Hint.Text = num.ToString();
            //broadcast.SetFoundNumber(Hint, null);
        }

        public void SetWorldValue(OutlinedTextBlock worldValue, int value)
        {
            if (worldValue == null)
                return;

            string location = worldValue.Name.Substring(0, worldValue.Name.Length - 4);
            SolidColorBrush Color = (SolidColorBrush)FindResource("DefaultWhite"); //default

            if (data.WorldsData[location].containsGhost) //turn green if it conains ghost item
                Color = (SolidColorBrush)FindResource("GhostHint");

            if (data.WorldsData[location].hintedHint || data.WorldsData[location].complete) //turn blue if it's marked as hinted hint or complete
                Color = (SolidColorBrush)FindResource("HintedHint");

            if (value == -999999)
            {
                worldValue.Text = "?";
            }
            else
                worldValue.Text = value.ToString();

            worldValue.Fill = Color;

            //broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), value);
        }

        public void SetCollected(bool add)
        {
            if (add)
                ++collected;
            else
                --collected;

            CollectedValue.Text = collected.ToString();
            //broadcast.Collected_01.Source = CollectedNum[0];
            //broadcast.Collected_10.Source = CollectedNum[1];
        }

        public void SetTotal(bool add)
        {
            if (add)
                ++total;
            else
                --total;

            TotalValue.Text = total.ToString();
            //broadcast.CheckTotal_01.Source = TotalNum[0];
            //broadcast.CheckTotal_10.Source = TotalNum[1];
        }

        public void SetHintText(string text)
        {
            if (data.SeedHashLoaded && HashGrid.Visibility == Visibility.Visible)
            {
                HashGrid.Visibility = Visibility.Collapsed;
                data.SeedHashVisible = false;
            }
            HintText.Text = text;
        }

        ///i might not need this anymore if i am going to set colors everytime
        //public void SetJokeText(string text)
        //{
        //    if (data.SeedHashLoaded)
        //    {
        //        HashRow.Height = new GridLength(0, GridUnitType.Star);
        //        SeedHashVisible = false;
        //    }
        //
        //    HintText.Text = text;
        //    HintText.Fill = Brushes.LightBlue;
        //}

        ///might need for magic numbers??
        //public int GetImageNumber(string ImagePath)
        //{
        //    int number = 10;
        //
        //    if (!ImagePath.EndsWith("QuestionMark.png") && ImagePath != null)
        //    {
        //        string val = ImagePath;
        //        val = val.Substring(val.LastIndexOf('/') + 1);
        //        number = int.Parse(val.Substring(0, val.IndexOf('.')));
        //
        //        if (number > 9 || number < 0)
        //            number = 10;
        //    }
        //
        //    return number;
        //}

        public void VisitLockCheck()
        {
            //we use this to check the current lock state and set lock visuals as needed while doing so
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

        ///TODO: fix for ghost stuff
        public int GetGhostPoints(WorldGrid worlditems)
        {
            int points = 0;

            foreach (Item ghost in data.GhostItems.Values.ToList())
            {
                if (worlditems.Children.Contains(ghost))
                {
                    //points += data.PointsDatanew[GetGhostType[ghost.Name]];
                }
            }

            return points;
        }

        ///put this elsewhere
        //private Dictionary<string, string> GetGhostType = new Dictionary<string, string>()
        //{
        //    {"Ghost_Report1", "report"},
        //    {"Ghost_Report2", "report"},
        //    {"Ghost_Report3", "report"},
        //    {"Ghost_Report4", "report"},
        //    {"Ghost_Report5", "report"},
        //    {"Ghost_Report6", "report"},
        //    {"Ghost_Report7", "report"},
        //    {"Ghost_Report8", "report"},
        //    {"Ghost_Report9", "report"},
        //    {"Ghost_Report10", "report"},
        //    {"Ghost_Report11", "report"},
        //    {"Ghost_Report12", "report"},
        //    {"Ghost_Report13", "report"},
        //    {"Ghost_Fire1", "magic"},
        //    {"Ghost_Fire2", "magic"},
        //    {"Ghost_Fire3", "magic"},
        //    {"Ghost_Blizzard1", "magic"},
        //    {"Ghost_Blizzard2", "magic"},
        //    {"Ghost_Blizzard3", "magic"},
        //    {"Ghost_Thunder1", "magic"},
        //    {"Ghost_Thunder2", "magic"},
        //    {"Ghost_Thunder3", "magic"},
        //    {"Ghost_Cure1", "magic"},
        //    {"Ghost_Cure2", "magic"},
        //    {"Ghost_Cure3", "magic"},
        //    {"Ghost_Reflect1", "magic"},
        //    {"Ghost_Reflect2", "magic"},
        //    {"Ghost_Reflect3", "magic"},
        //    {"Ghost_Magnet1", "magic"},
        //    {"Ghost_Magnet2", "magic"},
        //    {"Ghost_Magnet3", "magic"},
        //    {"Ghost_Valor", "form"},
        //    {"Ghost_Wisdom", "form"},
        //    {"Ghost_Limit", "form"},
        //    {"Ghost_Master", "form"},
        //    {"Ghost_Final", "form"},
        //    {"Ghost_OnceMore", "ability"},
        //    {"Ghost_SecondChance", "ability"},
        //    {"Ghost_TornPage1", "page"},
        //    {"Ghost_TornPage2", "page"},
        //    {"Ghost_TornPage3", "page"},
        //    {"Ghost_TornPage4", "page"},
        //    {"Ghost_TornPage5", "page"},
        //    {"Ghost_Baseball", "summon"},
        //    {"Ghost_Lamp", "summon"},
        //    {"Ghost_Ukulele", "summon"},
        //    {"Ghost_Feather", "summon"},
        //    {"Ghost_Connection", "proof"},
        //    {"Ghost_Nonexistence", "proof"},
        //    {"Ghost_Peace", "proof"},
        //    {"Ghost_PromiseCharm", "proof"},
        //    {"Ghost_AuronWep", "visit"},
        //    {"Ghost_MulanWep", "visit"},
        //    {"Ghost_BeastWep", "visit"},
        //    {"Ghost_JackWep", "visit"},
        //    {"Ghost_SimbaWep", "visit"},
        //    {"Ghost_SparrowWep", "visit"},
        //    {"Ghost_AladdinWep", "visit"},
        //    {"Ghost_TronWep", "visit"},
        //    {"Ghost_MembershipCard", "visit"},
        //    {"Ghost_IceCream", "visit"},
        //    {"Ghost_Picture", "visit"}
        //};

    }
}