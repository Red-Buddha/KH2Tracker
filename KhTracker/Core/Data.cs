using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace KhTracker
{
    public class Data
    {
        public Mode mode = Mode.None;
        public bool hintsLoaded = false;
        public Button selected = null;
        public bool dragDrop = true;
        public bool ScoreMode = false;
        public int usedPages = 0;
        public bool forcedFinal;
        public bool dataSplit = false;
        public string seedgenVersion = "";
        public bool altFinalTracking = true;

        //this is stupid. Hash kept auto reseting because of SetMode during hint loading.
        //this is here as a toggle to only reset the hash when i want it to
        public bool ShouldResetHash = true;
        public bool SeedHashLoaded = false;
        public bool SpoilerWorldCompletion = false;
        public bool SpoilerReportMode = false;

        public string openKHHintText = "";
        public string[] hintFileText = new string[2];
        public Codes codes = new Codes();

        public List<Tuple<string, string, int>> reportInformation = new List<Tuple<string, string, int>>();
        public List<string> reportLocations = new List<string>();
        public List<bool> reportLocationsUsed = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false };
        public List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        public Dictionary<string, List<string>> ProgressKeys = new Dictionary<string, List<string>>();
        public Dictionary<string, Grid> WorldsTop = new Dictionary<string, Grid>();
        public Dictionary<string, WorldData> WorldsData = new Dictionary<string, WorldData>();

        public List<Item> Reports = new List<Item>();
        public List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        public List<Item> TornPages = new List<Item>();
        public List<Item> VisitLocks = new List<Item>();
        public Dictionary<string, Tuple<Item, Grid>> Items = new Dictionary<string, Tuple<Item, Grid>>();

        //auto-detect
        public BitmapImage AD_Connect;
        public BitmapImage AD_PC;
        public BitmapImage AD_PCred;
        public BitmapImage AD_PS2;
        public BitmapImage AD_Cross;

        //for points hints
        public Dictionary<string, Item> GhostItems = new Dictionary<string, Item>();
        public Dictionary<string, int> PointsDatanew = new Dictionary<string, int>()
        {
            //items
            { "proof", 0 },
            { "form", 0 },
            { "magic", 0 },
            { "summon", 0 },
            { "ability", 0 },
            { "page", 0 },
            { "report", 0 },
            { "other", 0},
            { "visit", 0},
            //bossrelated
            { "boss_as", 0 },
            { "boss_datas", 0 },
            { "boss_sephi", 0 },
            { "boss_terra", 0 },
            { "boss_other", 0 },
            { "boss_final", 0 },
            //other
            { "complete", 0 },
            { "bonus", 0 },
            { "formlv", 0 },
            { "deaths", 0 },
            //collection bonus
            { "collection_magic", 0 },
            { "collection_page", 0 },
            { "collection_pouches", 0},

            { "collection_proof", 0 },
            { "collection_form", 0 },
            { "collection_summon", 0 },
            { "collection_ability", 0 },
            { "collection_report", 0 },
            { "collection_visit", 0},
        };
        public static Dictionary<string, List<string>> WorldItems = new Dictionary<string, List<string>>();
        public List<string> TrackedReports = new List<string>();
        public List<string> SpoilerRevealTypes = new List<string>();

        //for boss rando points
        public bool BossRandoFound = false;
        public Dictionary<string, string> BossList = new Dictionary<string, string>();
        public int BossRandoSeed;

        public List<string> enabledWorlds = new List<string>();

        //Progression Hints stuff
        public bool UsingProgressionHints = false;
        public int ProgressionPoints = 0;
        public int TotalProgressionPoints = 0;
        public int WorldsEnabled = 0;
        public int ProgressionHash = 0;
        public bool revealFinalXemnas = false;

        #region Progression Tracking
        public int PrevEventID1 = 0;
        public int PrevEventID3 = 0;
        public string PrevWorld = "";
        public int PrevRoomNum = 0;
        #endregion

        #region Hint Order Logic
        public List<int> HintCosts = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 };
        //public int NumOfHints = 20;
        public int ProgressionCurrentHint = 0;
        public List<string> HintRevealOrder = new List<string>();
        public List<Tuple<string, string, string, bool, bool, bool>> HintRevealsStored = new List<Tuple<string, string, string, bool, bool, bool>>();
        public bool synthOn = false;
        public bool puzzlesOn = false;
        public string previousWorldHinted = "";
        #endregion

        #region Bonuses and Sora/Drive Levels
        public int ReportBonus = 1;
        public int WorldCompleteBonus = 0;
        public Dictionary<string, int> StoredWorldCompleteBonus = new Dictionary<string, int>()
        {
            { "SorasHeart", 0 },
            { "DriveForms", 0 },
            { "SimulatedTwilightTown", 0 },
            { "TwilightTown", 0 },
            { "HollowBastion", 0 },
            { "BeastsCastle", 0 },
            { "OlympusColiseum", 0 },
            { "Agrabah", 0 },
            { "LandofDragons", 0 },
            { "HundredAcreWood", 0 },
            { "PrideLands", 0 },
            { "DisneyCastle", 0 },
            { "HalloweenTown", 0 },
            { "PortRoyal", 0 },
            { "SpaceParanoids", 0 },
            { "TWTNW", 0 },
            { "GoA", 0 },
            { "Atlantica", 0 },
            { "PuzzSynth", 0 }
        };
        //                                              Sora Level - 10 20 30 40 50
        public List<int> Levels_ProgressionValues = new List<int>() { 1, 1, 1, 2, 4 };
        public int LevelsPreviousIndex = 0;
        public int NextLevelMilestone = 9;
        //                                             Drive Level -  2  3  4  5  6  7
        public List<int> Drives_ProgressionValues = new List<int>() { 0, 0, 0, 1, 0, 2 };
        public List<int> DriveLevels = new List<int>() { 1, 1, 1, 1, 1 };
        #endregion

        #region World Progression Values
        public List<int> STT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<int> TT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        public List<int> HB_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public List<int> CoR_ProgressionValues = new List<int>() { 0, 0, 0, 0, 0 };
        public List<int> BC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        public List<int> OC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<int> AG_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<int> LoD_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<int> HAW_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6 };
        public List<int> PL_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        public List<int> AT_ProgressionValues = new List<int>() { 1, 2, 3 };
        public List<int> DC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<int> HT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<int> PR_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public List<int> SP_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6 };
        public List<int> TWTNW_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        #endregion
    }

    public class WorldData
    {
        public bool hinted;         //currently hinted? (for hinted hint logic)
        public bool hintedHint;     //currently hinted hint?
        //Progression Hints
        public bool hintedProgression;
        public bool complete;       //are all checks found?
        public int progress;        //current world progression
        public bool containsGhost;  //contains ghost item?
        public int visitLocks;      //visit lock progress

        public List<string> checkCount = new List<string>();

        public Grid top;
        public Button world;
        public ContentControl progression;
        public OutlinedTextBlock value;
        public WorldGrid worldGrid;

        public WorldData(Grid Top, Button World, ContentControl Progression, OutlinedTextBlock Value, WorldGrid itemgrid, bool Hinted, int VisitLock)
        {
            top = Top;
            world = World;
            progression = Progression;
            value = Value;
            worldGrid = itemgrid;
            hinted = Hinted;
            hintedHint = false;
            complete = false;
            progress = 0;
            containsGhost = false;
            visitLocks = VisitLock;
        }
    }

    public enum Mode
    {
        Hints,
        AltHints,
        OpenKHHints,
        OpenKHAltHints,
        DAHints,
        PathHints,
        SpoilerHints,
        None
    }
}