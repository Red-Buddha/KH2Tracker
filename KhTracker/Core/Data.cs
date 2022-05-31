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

        public string openKHHintText = "";
        public string[] hintFileText = new string[2];
        public Codes codes = new Codes();

        public List<Tuple<string, int>> reportInformation = new List<Tuple<string, int>>();
        public List<Tuple<string, string>> pointreportInformation = new List<Tuple<string, string>>();
        public List<Tuple<string, string, int>> pathreportInformation = new List<Tuple<string, string, int>>();
        public List<string> reportLocations = new List<string>();
        public List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

        public Dictionary<string, List<string>> ProgressKeys = new Dictionary<string, List<string>>();

        public Dictionary<string, Grid> WorldsTop = new Dictionary<string, Grid>();

        public Dictionary<string, WorldData> WorldsData = new Dictionary<string, WorldData>();

        public List<Item> Reports = new List<Item>();
        public List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        public List<Item> TornPages = new List<Item>();
        public List<Item> VisitLocks = new List<Item>();

        public List<BitmapImage> SingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> BlueSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> GreenSingleNumbers = new List<BitmapImage>();

        public List<BitmapImage> OldSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> OldBlueSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> OldGreenSingleNumbers = new List<BitmapImage>();

        public List<BitmapImage> CustomSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> CustomBlueSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> CustomGreenSingleNumbers = new List<BitmapImage>();

        public List<Item> Items = new List<Item>();

        //stupid bar images
        public BitmapImage VerticalBarY;
        public BitmapImage VerticalBarW;

        public BitmapImage CustomVerticalBarY;
        public BitmapImage CustomVerticalBarW;

        public BitmapImage SlashBarY;
        public BitmapImage SlashBarB;

        public BitmapImage CustomSlashBarY;
        public BitmapImage CustomSlashBarB;

        //auto-detect
        public BitmapImage AD_Connect;
        public BitmapImage AD_PC;
        public BitmapImage AD_PCred;
        public BitmapImage AD_PS2;

        //for points hints
        public static Dictionary<string, Item> GhostItems = new Dictionary<string, Item>();
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

        //for timed hints
        public int timedHintsTimer = 0;
        public bool startedTimedHints = false;
        public int currentHint = 0;
        //                                    Sora   Drive   STT    TT     HB     BC     OC     AG     LoD   100AW   PL     DC     HT     PR     SP   TWTNW    GoA    AT
        //                                      0      1      2      3      4      5      6      7      8      9     10     11     12     13     14     15     16     17
        public int[] worldStoredHintCount = {   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0   };
        public int[] worldStoredOrigCount = {   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0   };
        public int[] worldHintNumber      = {  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1   };
        public bool[] isHintedHint        = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public string[] isHintedBy        = {  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""   };
        public int[] hintOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public int seedTimeLoaded = 0;
        //public int lastStoredSeedHash = 0;
        //public int lastStoredSeedHashTemp = 0;

        public void ResetTimedHints()
        {
            worldStoredHintCount = new int[] {   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0   };
            worldStoredOrigCount = new int[] {   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0  ,   0   };
            isHintedHint        = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            isHintedBy        = new string[] {  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""  ,  ""   };
        }

        //public void ShuffleHintOrder(int seed)
        //{
        //    var rng = new Random(seed);
        //    int n = hintOrder.Length;
        //
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        int value = hintOrder[k];
        //        hintOrder[k] = hintOrder[n];
        //        hintOrder[n] = value;
        //    }
        //}

        //public string PrintHintOrder(int[] arr)
        //{
        //    string output = "";
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        output += arr[i] + " ";
        //    }
        //    return output;
        //}
    }

    public class WorldData
    {
        public bool hinted;
        public bool hintedHint;
        public bool complete;
        public int progress;
        public bool containsGhost;
        public int visitLocks;

        public List<string> checkCount = new List<string>();

        public Grid top;
        public Button world;
        public ContentControl progression;
        public Grid hint;
        public WorldGrid worldGrid;
        public Image selectedBar;

        public WorldData(Grid Top, Button World, ContentControl Progression, Grid Hint, WorldGrid grid, Image SelectedBar, bool Hinted, int VisitLock)
        {
            top = Top;
            world = World;
            progression = Progression;
            hint = Hint;
            worldGrid = grid;
            selectedBar = SelectedBar;
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
        TimeHints,
        None
    }
}