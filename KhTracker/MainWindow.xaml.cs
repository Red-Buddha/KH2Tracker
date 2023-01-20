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
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;
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

            OnReset(null, null);

            //Init auto-detect
            //AutoDetectOption.IsChecked = Properties.Settings.Default.AutoDetect;
            //AutoDetectToggle(null, null);
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
            data.WorldsData.Add("GoA",                      new WorldData(GoATop, GoA, GoAProgression, GoAHint, GoAGrid, true, 0));
            data.WorldsData.Add("Atlantica",                new WorldData(AtlanticaTop, Atlantica, AtlanticaProgression, AtlanticaHint, AtlanticaGrid, false, 0));
            data.WorldsData.Add("PuzzSynth",                new WorldData(PuzzSynthTop, PuzzSynth, null, PuzzSynthHint, PuzzSynthGrid, false, 0));

            data.ProgressKeys.Add("SimulatedTwilightTown",  new List<string>() { "", "Chests", "Minigame", "TwilightThorn", "Axel1", "Struggle", "ComputerRoom", "Axel", "DataRoxas" });
            data.ProgressKeys.Add("TwilightTown",           new List<string>() { "", "Chests", "Station", "MysteriousTower", "Sandlot", "Mansion", "BetwixtAndBetween", "DataAxel" });
            data.ProgressKeys.Add("HollowBastion",          new List<string>() { "", "Chests", "Bailey", "AnsemStudy", "Corridor", "Dancers", "HBDemyx", "FinalFantasy", "1000Heartless", "Sephiroth", "DataDemyx", "SephiDemyx" });
            data.ProgressKeys.Add("BeastsCastle",           new List<string>() { "", "Chests", "Thresholder", "Beast", "DarkThorn", "Dragoons", "Xaldin", "DataXaldin" });
            data.ProgressKeys.Add("OlympusColiseum",        new List<string>() { "", "Chests", "Cerberus", "Urns", "OCDemyx", "OCPete", "Hydra", "AuronStatue", "Hades", "Zexion" });
            data.ProgressKeys.Add("Agrabah",                new List<string>() { "", "Chests", "Abu", "Chasm", "TreasureRoom", "Lords", "Carpet", "GenieJafar", "Lexaeus" });
            data.ProgressKeys.Add("LandofDragons",          new List<string>() { "", "Chests", "Missions", "Mountain", "Cave", "Summit", "ShanYu", "ThroneRoom", "StormRider", "DataXigbar" });
            data.ProgressKeys.Add("HundredAcreWood",        new List<string>() { "", "Chests", "Piglet", "Rabbit", "Kanga", "SpookyCave", "StarryHill" });
            data.ProgressKeys.Add("PrideLands",             new List<string>() { "", "Chests", "Simba", "Hyenas1", "Scar", "Hyenas2", "GroundShaker", "DataSaix" });
            data.ProgressKeys.Add("DisneyCastle",           new List<string>() { "", "Chests", "Minnie", "OldPete", "Windows", "BoatPete", "DCPete", "Marluxia", "LingeringWill", "Marluxia_LingeringWill" });
            data.ProgressKeys.Add("HalloweenTown",          new List<string>() { "", "Chests", "CandyCaneLane", "PrisonKeeper", "OogieBoogie", "Children", "Presents", "Experiment", "Vexen" });
            data.ProgressKeys.Add("PortRoyal",              new List<string>() { "", "Chests", "Town", "1Minute", "Medallions", "Barrels", "Barbossa", "GrimReaper1", "Gambler", "GrimReaper", "DataLuxord" });
            data.ProgressKeys.Add("SpaceParanoids",         new List<string>() { "", "Chests", "Screens", "HostileProgram", "SolarSailer", "MCP", "Larxene" });
            data.ProgressKeys.Add("TWTNW",                  new List<string>() { "", "Chests", "Roxas", "Xigbar", "Luxord", "Saix", "Xemnas1", "DataXemnas" });
            data.ProgressKeys.Add("Atlantica",              new List<string>() { "", "Tutorial", "Ursula", "NewDay" });
            data.ProgressKeys.Add("GoA",                    new List<string>() { "", "Chests", "Fight1", "Fight2", "Transport", "Valves" });

            //testing tooltip descriptions
            //(adding to existing list above cause it's easier) 
            data.ProgressKeys.Add("SimulatedTwilightTownDesc",  new List<string>() { "", "Early Checks", "Part-Time Job", "Twilight Thorn (Boss)", "Axel 1 (Boss)", "Beat Setzer", "Mansion Computer Room", "Axel 2 (Boss)", "Roxas (Data)" });
            data.ProgressKeys.Add("TwilightTownDesc",           new List<string>() { "", "Early Checks", "Train Station Fight", "Mysterious Tower", "Sandlot Fight", "Mansion Gate Fight", "Betwixt And Between", "Axel (Data)" });
            data.ProgressKeys.Add("HollowBastionDesc",          new List<string>() { "", "Early Checks", "Bailey Gate Fight", "Ansem's Study", "Corridor Fight", "Restoration Site Fight", "Demyx (Boss)", "Final Fantasy Fights", "1000 Heartless", "Sephiroth (Boss)", "Demyx (Data)", "Sephiroth and Demyx" });
            data.ProgressKeys.Add("BeastsCastleDesc",           new List<string>() { "", "Early Checks", "Thresholder (Boss)", "Beast Fight", "Dark Thorn (Boss)", "Dragoons Forced Fight", "Xaldin (Boss)", "Xaldin (Data)" });
            data.ProgressKeys.Add("OlympusColiseumDesc",        new List<string>() { "", "Early Checks", "Cerberus (Boss)", "Phil's Training", "Demyx Fight", "Pete Fight", "Hydra (Boss)", "Hades' Chamber Fight", "Hades (Boss)", "Zexion (AS/Data)" });
            data.ProgressKeys.Add("AgrabahDesc",                new List<string>() { "", "Early Checks", "Abu Minigame", "Chasm of Challenges", "Treasure Room Fight", "Twin Lords (Boss)", "Carpet Magic Minigame", "Genie Jafar (Boss)", "Lexaeus (AS/Data)" });
            data.ProgressKeys.Add("LandofDragonsDesc",          new List<string>() { "", "Early Checks", "Mission 3 (The Search)", "Mountain Climb", "Town Cave Fight", "Summmit Fight", "Shan Yu (Boss)", "Throne Room", "Storm Rider (Boss)", "Xigbar (Data)" });
            data.ProgressKeys.Add("HundredAcreWoodDesc",        new List<string>() { "", "Early Checks", "Entered Piglet's House", "Entered Rabbit's House", "Entered Kanga's House", "Entered Spooky Cave", "Entered Starry Hill" });
            data.ProgressKeys.Add("PrideLandsDesc",             new List<string>() { "", "Early Checks", "Met Simba", "Hyenas Fight (1st Visit)", "Scar (Boss)", "Hyenas Fight (2nd Visit)", "Ground Shaker (Boss)", "Saix (Data)" });
            data.ProgressKeys.Add("DisneyCastleDesc",           new List<string>() { "", "Early Checks", "Minnie Escort", "Past Pete Fight", "Windows of Time", "Steamboat Fight", "Pete (Boss)", "Marluxia (AS/Data)", "Lingering Will (Boss)", "Marluxia and Lingering Will" });
            data.ProgressKeys.Add("HalloweenTownDesc",          new List<string>() { "", "Early Checks", "Candy Cane Lane Fight", "Prison Keeper (Boss)", "Oogie Boogie (Boss)", "Lock, Shock, and Barrel", "Made Decoy Presents", "The Experiment (Boss)", "Vexen (AS/Data)" });
            data.ProgressKeys.Add("PortRoyalDesc",              new List<string>() { "", "Early Checks", "Town Fight", "1 Minute Isle Fight", "Interceptor Medallion Fight", "Interceptor Barrels", "Barbossa (Boss)", "Grim Reaper 1 (Boss)", "1st Gambler Medallion", "Grim Reaper 2 (Boss)", "Luxord (Data)" });
            data.ProgressKeys.Add("SpaceParanoidsDesc",         new List<string>() { "", "Early Checks", "Dataspace Fight", "Hostile Program (Boss)", "Solar Sailer", "MCP (Boss)", "Larxene (AS/Data)" });
            data.ProgressKeys.Add("TWTNWDesc",                  new List<string>() { "", "Early Checks", "Roxas (Boss)", "Xigbar (Boss)", "Luxord (Boss)", "Saix (Boss)", "Xemnas 1 (Boss)", "Xemnas (Data)" });
            data.ProgressKeys.Add("AtlanticaDesc",              new List<string>() { "", "Music Tutorial", "Ursula's Revenge", "A New Day is Dawning" });
            data.ProgressKeys.Add("GoADesc",                    new List<string>() { "", "Early Checks", "Forced Fight 1", "Forced Fight 2", "Transport to Rememberance", "Steam Valves (CoR Skip)" });

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
                            //Console.WriteLine(check.Name);
                        }
                        else
                        {
                            data.GhostItems.Add(check.Name, check);     //list of all valid ghost items (why is this a dictionary????)
                        }
                    }
                }
            }
        }

        private void InitOptions()
        {
            #region Options

            TopMostOption.IsChecked = Properties.Settings.Default.TopMost;
            TopMostToggle(null, null);

            DragAndDropOption.IsChecked = Properties.Settings.Default.DragDrop;
            DragDropToggle(null, null);

            AutoSaveProgressOption.IsChecked = Properties.Settings.Default.AutoSaveProgress;

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

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            TornPagesToggle(TornPagesOption.IsChecked);

            ExtraChecksOption.IsChecked = Properties.Settings.Default.ExtraChecks;
            ExtraChecksToggle(ExtraChecksOption.IsChecked);

            //Visual
            SeedHashOption.IsChecked = Properties.Settings.Default.SeedHash;
            SeedHashToggle(SeedHashOption.IsChecked);

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

            FormsGrowthOption.IsChecked = Properties.Settings.Default.FormsGrowth;
            FormsGrowthToggle(null, null);

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

            WorldHighlightOption.IsChecked = Properties.Settings.Default.WorldHighlight;

            #endregion

            #region Visual

            NewWorldLayoutOption.IsChecked = Properties.Settings.Default.NewWorldLayout;
            if (NewWorldLayoutOption.IsChecked)
                NewWorldLayoutToggle(null, null);

            OldWorldLayoutOption.IsChecked = Properties.Settings.Default.OldWorldLayout;
            if (OldWorldLayoutOption.IsChecked)
                OldWorldLayoutToggle(null, null);

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

            ColorHintOption.IsChecked = Properties.Settings.Default.ColorHints;

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
            switch (e.ChangedButton)
            {
                case MouseButton.Left: //for changing world selection visuals
                    if (data.selected != null) //set previousl selected world to default colors
                    {
                        foreach (var Box in data.WorldsData[data.selected.Name].top.Children.OfType<Rectangle>())
                        {
                            if (Box.Opacity != 0.9 && !Box.Name.EndsWith("SelWG"))
                                Box.Fill = (SolidColorBrush)FindResource("DefaultRec");

                            if (Box.Name.EndsWith("SelWG") && !WorldHighlightOption.IsChecked)
                                Box.Visibility = Visibility.Collapsed;
                        }
                    }
                    data.selected = button;
                    foreach (var Box in data.WorldsData[button.Name].top.Children.OfType<Rectangle>()) //set currently selected world colors
                    {
                        if (Box.Opacity != 0.9 && !Box.Name.EndsWith("SelWG"))
                            Box.Fill = (SolidColorBrush)FindResource("SelectedRec");

                        if (Box.Name.EndsWith("SelWG") && !WorldHighlightOption.IsChecked)
                            Box.Visibility = Visibility.Visible;
                    }
                    break;
                case MouseButton.Right: //for setting world cross icon
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
                    }
                    break;
                case MouseButton.Middle: //setting world value back to "?" if not using any hints
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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
            if (AutoSaveProgressOption.IsChecked)
            {
                if (!Directory.Exists("KhTrackerAutoSaves"))
                {
                    Directory.CreateDirectory("KhTrackerAutoSaves\\");
                }

                Save("KhTrackerAutoSaves\\" + "Tracker-Backup_" + DateTime.Now.ToString("yy-MM-dd_H-m") + ".txt");
            }
            Properties.Settings.Default.Save();
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
        }

        /// 
        /// Handle UI Changes
        /// 

        //Used for when no hints are loaded. use the scroll wheel to change world number.
        private void ManualWorldValue(OutlinedTextBlock Hint, int delta)
        {
            //return if the a hint mode is loaded
            if (data.mode != Mode.None && !Hint.Name.Contains("GoA"))
                return;

            int num;

            //get current number
            if (Hint.Text == "?")
            {
                if (delta > 0 && Hint.Name.Contains("GoA")) 
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

            if (data.UsingProgressionHints)
            {
                if (num <= 0 && data.ProgressionCurrentHint >= 0 && Hint.Name.Contains("GoA"))
                {
                    if (data.ProgressionCurrentHint == 0)
                        num = 0;
                    else
                        num = 1;
                }
                else if (num > data.HintCosts.Count && data.ProgressionCurrentHint > 0 && Hint.Name.Contains("GoA"))
                    if (data.HintCosts.Count > data.ProgressionCurrentHint)
                        num = data.ProgressionCurrentHint;
                if (num > data.ProgressionCurrentHint && Hint.Name.Contains("GoA"))
                    num = data.ProgressionCurrentHint;

                if (num > 0)
                {
                    Tuple<string, string, string, bool, bool, bool> temp = data.HintRevealsStored[num - 1];
                    SetHintText(temp.Item1, temp.Item2, temp.Item3, temp.Item4, temp.Item5, temp.Item6);
                }
            }

            Hint.Text = num.ToString();
        }

        public void SetWorldValue(OutlinedTextBlock worldValue, int value)
        {
            string location = worldValue.Name.Substring(0, worldValue.Name.Length - 4);
            SolidColorBrush Color = (SolidColorBrush)FindResource("DefaultWhite"); //default

            if (data.WorldsData[location].containsGhost) //turn green if it conains ghost item
                Color = (SolidColorBrush)FindResource("GhostHint");

            if (worldValue == null || worldValue.Name.Contains("GoA") ||
                (data.UsingProgressionHints && !data.WorldsData[worldValue.Name.Substring(0, worldValue.Name.Length - 4)].hintedProgression))
            {
                if (data.mode == Mode.DAHints)
                {
                    worldValue.Fill = Color;
                }

                return;
            }

            if (data.WorldsData[location].complete) //turn blue if it's marked as hinted hint or complete
                Color = (SolidColorBrush)FindResource("HintedHint");

            if (data.WorldsData[location].hintedHint) //turn blue if it's marked as hinted hint or complete
                Color = (SolidColorBrush)FindResource("ClassicYellow");

            if (value == -999999)
            {
                worldValue.Text = "?";
            }
            else
                worldValue.Text = value.ToString();

            worldValue.Fill = Color;
        }

        public void SetCollected(bool add)
        {
            //AddProgressionPoints(1);

            if (add)
                ++collected;
            else
                --collected;

            CollectedValue.Text = collected.ToString();
        }

        public void SetTotal(bool add)
        {
            if (add)
                ++total;
            else
                --total;

            TotalValue.Text = total.ToString();
        }

        //full
        public void SetHintText(string textBeg, string textMid, string textEnd, bool Color1, bool Color2, bool Color3)
        {
            if (data.SeedHashLoaded && HashGrid.Visibility == Visibility.Visible)
            {
                HashGrid.Visibility = Visibility.Collapsed;
            }

            string colorBeg = "DefWhite";
            string colorMid = "DefWhite";
            string colorEnd = "DefWhite";

            if (ColorHintOption.IsChecked)
            {
                if (Color1)
                    colorBeg = Codes.GetTextColor(textBeg);
                if (Color2)
                    colorMid = Codes.GetTextColor(textMid);
                if (Color3)
                    colorEnd = Codes.GetTextColor(textEnd);
            }

            HintTextBegin.Text = textBeg;
            HintTextBegin.Foreground = (SolidColorBrush)FindResource(colorBeg);

            HintTextMiddle.Text = textMid;
            HintTextMiddle.Foreground = (SolidColorBrush)FindResource(colorMid);

            HintTextEnd.Text = textEnd;
            HintTextEnd.Foreground = (SolidColorBrush)FindResource(colorEnd);

        }

        //single lines
        public void SetHintText(string text)
        {
            if (data.SeedHashLoaded && HashGrid.Visibility == Visibility.Visible)
            {
                HashGrid.Visibility = Visibility.Collapsed;
            }

            HintTextBegin.Text = text;
            HintTextMiddle.Text = "";
            HintTextEnd.Text = "";

            HintTextBegin.Foreground = (SolidColorBrush)FindResource("DefWhite");
            HintTextMiddle.Foreground = (SolidColorBrush)FindResource("DefWhite");
            HintTextEnd.Foreground = (SolidColorBrush)FindResource("DefWhite");
        }

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
                                TwilightTown.Opacity = 1;
                                break;
                            case 1:
                                TwilightTownLock_1.Visibility = Visibility.Visible;
                                TwilightTownLock_2.Visibility = Visibility.Collapsed;
                                TwilightTown.Opacity = 0.45;
                                break;
                            case 10:
                                TwilightTownLock_1.Visibility = Visibility.Collapsed;
                                TwilightTownLock_2.Visibility = Visibility.Visible;
                                TwilightTown.Opacity = 0.45;
                                break;
                            default:
                                TwilightTownLock_1.Visibility = Visibility.Visible;
                                TwilightTownLock_2.Visibility = Visibility.Visible;
                                TwilightTown.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "HollowBastion":
                        switch (data.WorldsData["HollowBastion"].visitLocks)
                        {
                            case 0:
                                HollowBastionLock.Visibility = Visibility.Hidden;
                                HollowBastion.Opacity = 1;
                                break;
                            default:
                                HollowBastionLock.Visibility = Visibility.Visible;
                                HollowBastion.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "BeastsCastle":
                        switch (data.WorldsData["BeastsCastle"].visitLocks)
                        {
                            case 0:
                                BeastsCastleLock.Visibility = Visibility.Hidden;
                                BeastsCastle.Opacity = 1;
                                break;
                            default:
                                BeastsCastleLock.Visibility = Visibility.Visible;
                                BeastsCastle.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "OlympusColiseum":
                        switch (data.WorldsData["OlympusColiseum"].visitLocks)
                        {
                            case 0:
                                OlympusColiseumLock.Visibility = Visibility.Hidden;
                                OlympusColiseum.Opacity = 1;
                                break;
                            default:
                                OlympusColiseumLock.Visibility = Visibility.Visible;
                                OlympusColiseum.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "Agrabah":
                        switch (data.WorldsData["Agrabah"].visitLocks)
                        {
                            case 0:
                                AgrabahLock.Visibility = Visibility.Hidden;
                                Agrabah.Opacity = 1;
                                break;
                            default:
                                AgrabahLock.Visibility = Visibility.Visible;
                                Agrabah.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "LandofDragons":
                        switch (data.WorldsData["LandofDragons"].visitLocks)
                        {
                            case 0:
                                LandofDragonsLock.Visibility = Visibility.Hidden;
                                LandofDragons.Opacity = 1;
                                break;
                            default:
                                LandofDragonsLock.Visibility = Visibility.Visible;
                                LandofDragons.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "PrideLands":
                        switch (data.WorldsData["PrideLands"].visitLocks)
                        {
                            case 0:
                                PrideLandsLock.Visibility = Visibility.Hidden;
                                PrideLands.Opacity = 1;
                                break;
                            default:
                                PrideLandsLock.Visibility = Visibility.Visible;
                                PrideLands.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "HalloweenTown":
                        switch (data.WorldsData["HalloweenTown"].visitLocks)
                        {
                            case 0:
                                HalloweenTownLock.Visibility = Visibility.Hidden;
                                HalloweenTown.Opacity = 1;
                                break;
                            default:
                                HalloweenTownLock.Visibility = Visibility.Visible;
                                HalloweenTown.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "PortRoyal":
                        switch (data.WorldsData["PortRoyal"].visitLocks)
                        {
                            case 0:
                                PortRoyalLock.Visibility = Visibility.Hidden;
                                PortRoyal.Opacity = 1;
                                break;
                            default:
                                PortRoyalLock.Visibility = Visibility.Visible;
                                PortRoyal.Opacity = 0.45;
                                break;
                        }
                        break;
                    case "SpaceParanoids":
                        switch (data.WorldsData["SpaceParanoids"].visitLocks)
                        {
                            case 0:
                                SpaceParanoidsLock.Visibility = Visibility.Hidden;
                                SpaceParanoids.Opacity = 1;
                                break;
                            default:
                                SpaceParanoidsLock.Visibility = Visibility.Visible;
                                SpaceParanoids.Opacity = 0.45;
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

            foreach (Item ghost in data.GhostItems.Values.ToList())
            {
                if (worlditems.Children.Contains(ghost))
                {
                    points += data.PointsDatanew[Codes.FindItemType(ghost.Name)];
                }
            }

            return points;
        }

    }
}