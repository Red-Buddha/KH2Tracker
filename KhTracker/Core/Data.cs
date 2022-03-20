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
        public List<string> reportLocations = new List<string>();
        public List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

        public Dictionary<string, List<string>> ProgressKeys = new Dictionary<string, List<string>>();

        public Dictionary<string, Grid> WorldsTop = new Dictionary<string, Grid>();

        public Dictionary<string, WorldData> WorldsData = new Dictionary<string, WorldData>();

        public List<Item> Reports = new List<Item>();
        public List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        public List<Item> TornPages = new List<Item>();

        public List<BitmapImage> Numbers = new List<BitmapImage>();
        public List<BitmapImage> SingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> BlueNumbers = new List<BitmapImage>();
        public List<BitmapImage> BlueSingleNumbers = new List<BitmapImage>();

        public List<BitmapImage> OldNumbers = new List<BitmapImage>();
        public List<BitmapImage> OldSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> OldBlueNumbers = new List<BitmapImage>();
        public List<BitmapImage> OldBlueSingleNumbers = new List<BitmapImage>();

        public List<BitmapImage> CustomNumbers = new List<BitmapImage>();
        public List<BitmapImage> CustomSingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> CustomBlueNumbers = new List<BitmapImage>();
        public List<BitmapImage> CustomBlueSingleNumbers = new List<BitmapImage>();

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
            { "other", 0}
        };
        public static Dictionary<string, List<string>> WorldItems = new Dictionary<string, List<string>>();

    }

    public class WorldData
    {
        public bool hinted;
        public bool hintedHint;
        public bool complete;
        public int progress;

        public List<string> checkCount = new List<string>();

        public Grid top;
        public Button world;
        public ContentControl progression;
        public Image hint;
        public WorldGrid worldGrid;
        public Image selectedBar;

        public WorldData(Grid Top, Button World, ContentControl Progression, Image Hint, WorldGrid grid, Image SelectedBar, bool Hinted)
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
        }
    }

    public enum Mode
    {
        Hints,
        AltHints,
        OpenKHHints,
        OpenKHAltHints,
        DAHints,
        TimeHints,
        None
    }
}
