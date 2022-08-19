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

        //this is stupid. Hash kept auto reseting because of SetMode during hint loading.
        //this is here as a toggle to only reset the hash when i want it to
        public bool ShouldResetHash = true;
        public bool SeedHashLoaded = false;
        public bool SeedHashVisible = false; //TODO: remove this one. i think i don't need it anymore
        public bool SpoilerWorldCompletion = false;
        public bool SpoilerReportMode = false;

        public string openKHHintText = "";
        public string[] hintFileText = new string[2];
        public Codes codes = new Codes();

        public List<Tuple<string, string, int>> reportInformation = new List<Tuple<string, string, int>>();
        public List<string> reportLocations = new List<string>();
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

        //for points hints
        public Dictionary<string, Item> GhostItems = new Dictionary<string, Item>();
        public Dictionary<string, int> PointsDatanew = new Dictionary<string, int>()
        {
            { "proof", 0 },
            { "form", 0 },
            { "magic", 0 },
            { "summon", 0 },
            { "ability", 0 },
            { "page", 0 },
            { "report", 0 },
            { "complete", 0 },
            { "bonus", 0 },
            { "formlv", 0 },
            { "other", 0},
            { "visit", 0}
        };
        public static Dictionary<string, List<string>> WorldItems = new Dictionary<string, List<string>>();
        public List<string> TrackedReports = new List<string>();
        public List<string> SpoilerRevealTypes = new List<string>();
    }

    public class WorldData
    {
        public bool hinted;         //currently hinted? (for hinted hint logic)
        public bool hintedHint;     //currently hinted hint?
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