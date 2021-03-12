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
        public Mode mode;
        public bool hintsLoaded = false;
        public Button selected = null;
        public bool dragDrop = true;

        public string[] hintFileText = new string[2];
        public Codes codes = new Codes();

        public List<Tuple<string, int>> reportInformation = new List<Tuple<string, int>>();
        public List<string> reportLocations = new List<string>();
        public List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

        public Dictionary<string, bool> HintedWorlds = new Dictionary<string, bool>();
        public Dictionary<string, bool> HintedHintWorlds = new Dictionary<string, bool>();
        public Dictionary<string, List<string>> WorldCheckCount = new Dictionary<string, List<string>>();
        public Dictionary<string, bool> WorldComplete = new Dictionary<string, bool>();
        public Dictionary<string, int> WorldProgress = new Dictionary<string, int>();

        public Dictionary<string, Grid> WorldsTop = new Dictionary<string, Grid>();
        public Dictionary<string, Button> Worlds = new Dictionary<string, Button>();
        public Dictionary<string, ContentControl> Progression = new Dictionary<string, ContentControl>();
        public Dictionary<string, Image> Hints = new Dictionary<string, Image>();
        public Dictionary<string, WorldGrid> Grids = new Dictionary<string, WorldGrid>();
        public Dictionary<string, Image> SelectedBars = new Dictionary<string, Image>();

        public List<Item> Reports = new List<Item>();
        public List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        public List<Item> TornPages = new List<Item>();

        public List<BitmapImage> Numbers = new List<BitmapImage>();
        public List<BitmapImage> SingleNumbers = new List<BitmapImage>();
        public List<BitmapImage> BlueNumbers = new List<BitmapImage>();
        public List<BitmapImage> BlueSingleNumbers = new List<BitmapImage>();

        public List<Item> Items = new List<Item>();
    }

    public enum Mode
    {
        Hints,
        AltHints
    }
}
