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
        public bool hintsLoaded = false;
        public Button selected = null;
        public bool dragDrop = true;

        public string[] hintFileText = new string[2];
        public Codes codes = new Codes();
        public List<Tuple<string, int>> reportInformation = new List<Tuple<string, int>>();
        public List<string> reportLocations = new List<string>();
        public List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        public List<Button> Worlds = new List<Button>();
        public List<Image> Hints = new List<Image>();
        public List<WorldGrid> Grids = new List<WorldGrid>();
        public List<Item> Reports = new List<Item>();
        public List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        public List<Item> TornPages = new List<Item>();
        public List<Image> SelectedBars = new List<Image>();
        public List<BitmapImage> Numbers = new List<BitmapImage>();
        public List<Item> Items = new List<Item>();
    }
}
