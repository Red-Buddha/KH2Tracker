using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for BroadcastWindow.xaml
    /// </summary>
    public partial class BroadcastWindow : Window
    {
        public bool canClose = false;
        Dictionary<string, int> worlds = new Dictionary<string, int>();
        Dictionary<string, int> others = new Dictionary<string, int>();
        Dictionary<string, int> totals = new Dictionary<string, int>();
        Dictionary<string, int> important = new Dictionary<string, int>();
        public Dictionary<string, ContentControl> Progression = new Dictionary<string, ContentControl>();
        Data data;

        public BroadcastWindow(Data dataIn)
        {
            InitializeComponent();
            //Item.UpdateTotal += new Item.TotalHandler(UpdateTotal);

            worlds.Add("SorasHeart",0);
            worlds.Add("DriveForms", 0);
            worlds.Add("SimulatedTwilightTown",0);
            worlds.Add("TwilightTown",0);
            worlds.Add("HollowBastion",0);
            worlds.Add("BeastsCastle",0);
            worlds.Add("OlympusColiseum",0);
            worlds.Add("Agrabah",0);
            worlds.Add("LandofDragons",0);
            worlds.Add("HundredAcreWood",0);
            worlds.Add("PrideLands",0);
            worlds.Add("DisneyCastle",0);
            worlds.Add("HalloweenTown",0);
            worlds.Add("PortRoyal",0);
            worlds.Add("SpaceParanoids",0);
            worlds.Add("TWTNW",0);
            worlds.Add("GoA", 0);
            worlds.Add("Atlantica", 0);
            worlds.Add("PuzzSynth", 0);

            others.Add("Report", 0);
            others.Add("TornPage", 0);
            others.Add("Fire", 0);
            others.Add("Blizzard", 0);
            others.Add("Thunder", 0);
            others.Add("Cure", 0);
            others.Add("Reflect", 0);
            others.Add("Magnet", 0);

            totals.Add("SorasHeart", -1);
            totals.Add("DriveForms", -1);
            totals.Add("SimulatedTwilightTown", -1);
            totals.Add("TwilightTown", -1);
            totals.Add("HollowBastion", -1);
            totals.Add("BeastsCastle", -1);
            totals.Add("OlympusColiseum", -1);
            totals.Add("Agrabah", -1);
            totals.Add("LandofDragons", -1);
            totals.Add("HundredAcreWood", -1);
            totals.Add("PrideLands", -1);
            totals.Add("DisneyCastle", -1);
            totals.Add("HalloweenTown", -1);
            totals.Add("PortRoyal", -1);
            totals.Add("SpaceParanoids", -1);
            totals.Add("TWTNW", -1);
            totals.Add("Atlantica", -1);
            totals.Add("PuzzSynth", -1);

            important.Add("Fire", 0);
            important.Add("Blizzard", 0);
            important.Add("Thunder", 0);
            important.Add("Cure", 0);
            important.Add("Reflect", 0);
            important.Add("Magnet", 0);
            important.Add("Valor", 0);
            important.Add("Wisdom", 0);
            important.Add("Limit", 0);
            important.Add("Master", 0);
            important.Add("Final", 0);
            important.Add("Nonexistence", 0);
            important.Add("Connection", 0);
            important.Add("Peace", 0);
            important.Add("PromiseCharm", 0);
            important.Add("Feather", 0);
            important.Add("Ukulele", 0);
            important.Add("Baseball", 0);
            important.Add("Lamp", 0);
            important.Add("Report", 0);
            important.Add("TornPage", 0);
            important.Add("SecondChance", 0);
            important.Add("OnceMore", 0);
            important.Add("HadesCup", 0);
            important.Add("OlympusStone", 0);
            important.Add("UnknownDisk", 0);
            important.Add("Anti", 0);
            important.Add("BeastWep", 0);
            important.Add("JackWep", 0);
            important.Add("SimbaWep", 0);
            important.Add("AuronWep", 0);
            important.Add("MulanWep", 0);
            important.Add("SparrowWep", 0);
            important.Add("AladdinWep", 0);
            important.Add("TronWep", 0);
            important.Add("MembershipCard", 0);
            important.Add("Picture", 0);
            important.Add("IceCream", 0);

            Progression.Add("SimulatedTwilightTown", SimulatedTwilightTownProgression);
            Progression.Add("TwilightTown", TwilightTownProgression);
            Progression.Add("HollowBastion", HollowBastionProgression);
            Progression.Add("BeastsCastle", BeastsCastleProgression);
            Progression.Add("OlympusColiseum", OlympusColiseumProgression);
            Progression.Add("Agrabah", AgrabahProgression);
            Progression.Add("LandofDragons", LandofDragonsProgression);
            Progression.Add("HundredAcreWood", HundredAcreWoodProgression);
            Progression.Add("PrideLands", PrideLandsProgression);
            Progression.Add("DisneyCastle", DisneyCastleProgression);
            Progression.Add("HalloweenTown", HalloweenTownProgression);
            Progression.Add("PortRoyal", PortRoyalProgression);
            Progression.Add("SpaceParanoids", SpaceParanoidsProgression);
            Progression.Add("TWTNW", TWTNWProgression);
            Progression.Add("Atlantica", AtlanticaProgression);

            data = dataIn;

            foreach (string key in data.Items.Keys)
            {
                data.Items[key].Item1.UpdateTotal += new Item.TotalHandler(UpdateTotal);
                data.Items[key].Item1.UpdateFound += new Item.FoundHandler(UpdateFound);
            }

            Top = Properties.Settings.Default.BroadcastWindowY;
            Left = Properties.Settings.Default.BroadcastWindowX;

            Width = Properties.Settings.Default.BroadcastWindowWidth;
            Height = Properties.Settings.Default.BroadcastWindowHeight;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowY = RestoreBounds.Top;
            Properties.Settings.Default.BroadcastWindowX = RestoreBounds.Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowWidth = RestoreBounds.Width;
            Properties.Settings.Default.BroadcastWindowHeight = RestoreBounds.Height;
        }

        public void UpdateFound(string item, string world, bool add)
        {
            //while (item.Any(char.IsDigit))
            //{
            //    item = item.Remove(item.Length - 1, 1);
            //}
            //
            //if (add) important[item]++; else important[item]--;
            //
            //if (others.ContainsKey(item))
            //{
            //    others["Report"] = important["Report"];
            //    others["TornPage"] = important["TornPage"];
            //    others["Fire"] = important["Fire"];
            //    others["Blizzard"] = important["Blizzard"];
            //    others["Thunder"] = important["Thunder"];
            //    others["Cure"] = important["Cure"];
            //    others["Reflect"] = important["Reflect"];
            //    others["Magnet"] = important["Magnet"];
            //}
            //
            //UpdateNumbers();
        }

        public void UpdateNumbers()
        {
        //    //Get correct bar images
        //    bool CustomMode = Properties.Settings.Default.CustomIcons;
        //    BitmapImage NumberBartotals = data.SlashBarY;
        //    //BitmapImage NumberBarG = data.SlashBarG;
        //    //BitmapImage NumberBarB = data.SlashBarB;
        //    BitmapImage NumberBarY = data.SlashBarY;
        //    if (CustomMode)
        //    {
        //        //if (MainWindow.CustomBarGFound)
        //        //    NumberBarB = data.CustomSlashBarG;
        //        //if (MainWindow.CustomBarBFound)
        //        //    NumberBarB = data.CustomSlashBarB;
        //        if (MainWindow.CustomBarYFound)
        //            NumberBarY = data.CustomSlashBarY;
        //    }
        //    //Fix Broadcast window report and torn page numbers
        //    ReportFoundBar.Source = NumberBarY;
        //    TornPageBar.Source = NumberBarY;
        //    CollectedBar.Source = NumberBarY;
        //    ReportTotal_10.Source = UpdateNumber(1, "Y")[0];
        //    ReportTotal_01.Source = UpdateNumber(3, "Y")[0];
        //    TornPageTotal.Source = UpdateNumber(5, "S")[0];
        //
        //    foreach (KeyValuePair<string, int> world in worlds)
        //    {
        //        //update numbers
        //        if (world.Key != "GoA")
        //        {
        //            Grid broadcasthintgrid = this.FindName(world.Key + "Hint") as Grid;
        //            //SetFoundNumber(data.WorldsData[world.Key].hint, broadcasthintgrid);
        //        }
        //    }
        //
        //    foreach (KeyValuePair<string, int> other in others)
        //    {
        //        //single digit counts
        //        if (other.Key != "Report")
        //        {
        //            Image otherImage = this.FindName(other.Key + "Found") as Image;
        //
        //            if (other.Value == 0 && other.Key != "TornPage")
        //            {
        //                otherImage.Source = null;
        //            }
        //            else
        //                otherImage.Source = UpdateNumber(other.Value, "Y")[0];
        //        }
        //        else
        //        {
        //            Image otherImage01 = this.FindName(other.Key + "Found_01") as Image;
        //            Image otherImage10 = this.FindName(other.Key + "Found_10") as Image;
        //            otherImage01.Source = UpdateNumber(other.Value, "Y")[0];
        //            
        //            if (other.Value < 10)
        //                otherImage10.Source = null; //hide 10s place number
        //            else
        //                otherImage10.Source = UpdateNumber(other.Value, "Y")[1];
        //        }
        //    }
        //
        //    foreach (KeyValuePair<string, int> total in totals)
        //    {
        //        //check current color
        //        string mainColor = "Y"; //default
        //        if (data.WorldsData[total.Key].containsGhost) mainColor = "G";
        //        if (data.WorldsData[total.Key].hintedHint || data.WorldsData[total.Key].complete) mainColor = "B";
        //
        //        //update numbers
        //        if (total.Key != "GoA")
        //        {
        //            Grid broadcasthintgrid = this.FindName(total.Key + "Hint") as Grid;
        //            SetTotalNumber(broadcasthintgrid, mainColor, total.Value);
        //        }
        //    }
        //
        //    foreach (KeyValuePair<string, int> impCheck in important)
        //    {
        //        ContentControl imp = this.FindName(impCheck.Key) as ContentControl;
        //        if (impCheck.Value > 0)
        //        {
        //            imp.Opacity = 1;
        //        }
        //        else
        //        {
        //            if (impCheck.Key != "Report" && impCheck.Key != "TornPage")
        //                imp.Opacity = 0.45;
        //        }
        //    }
        }

        public void UpdateTotal(string world, int checks)
        {
            string worldName = world;
            totals[worldName] = checks;// +1;

            UpdateNumbers();
        }

        void BroadcastWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        public void OnResetHints()
        {
            //Redone so that we can get the proper custom image (if it exists)
            //bool CustomMode = Properties.Settings.Default.CustomIcons;
            //BitmapImage NumberBar = data.SlashBarY;
            //
            //if (CustomMode && MainWindow.CustomBarYFound)
            //{
            //    NumberBar = data.CustomSlashBarY;
            //}
            //
            //SorasHeartBar.Source = NumberBar;
            //DriveFormsBar.Source = NumberBar;
            //HollowBastionBar.Source = NumberBar;
            //TwilightTownBar.Source = NumberBar;
            //LandofDragonsBar.Source = NumberBar;
            //BeastsCastleBar.Source = NumberBar;
            //OlympusColiseumBar.Source = NumberBar;
            //SpaceParanoidsBar.Source = NumberBar;
            //HalloweenTownBar.Source = NumberBar;
            //PortRoyalBar.Source = NumberBar;
            //AgrabahBar.Source = NumberBar;
            //PrideLandsBar.Source = NumberBar;
            //DisneyCastleBar.Source = NumberBar;
            //HundredAcreWoodBar.Source = NumberBar;
            //SimulatedTwilightTownBar.Source = NumberBar;
            //TWTNWBar.Source = NumberBar;
            //AtlanticaBar.Source = NumberBar;
            //PuzzSynthBar.Source = NumberBar;
        }

        public void OnReset()
        {
            foreach (string key in worlds.Keys.ToList())
            {
                worlds[key] = 0;
            }

            foreach (string key in others.Keys.ToList())
            {
                others[key] = 0;
            }

            foreach (string key in totals.Keys.ToList())
            {
                totals[key] = -1;
            }

            foreach (string key in important.Keys.ToList())
            {
                important[key] = 0;
            }

            OnResetHints();

            //List<BitmapImage> CollectedNum = UpdateNumber(0, "Y");
            //Collected_01.Source = CollectedNum[0];
            //Collected_10.Source = null;
        }

        public void ToggleProgression(bool toggle)
        {
            if (toggle == true)
            {
                foreach (string key in Progression.Keys.ToList())
                {
                    Progression[key].Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (string key in Progression.Keys.ToList())
                {
                    Progression[key].Visibility = Visibility.Hidden;
                }
            }
        }

        public List<BitmapImage> UpdateNumber(int num, string color)
        {
            //we need to get all 3 sources from the grid
            //int[] FinalNum = new int[] { 0, 0, 0 }; //Default 000
            //bool OldMode = Properties.Settings.Default.OldNum;
            //bool CustomMode = Properties.Settings.Default.CustomIcons;
            //List<BitmapImage> NormalNum = data.SingleNumbers;
            //List<BitmapImage> BlueNum = data.BlueSingleNumbers;
            //List<BitmapImage> GreenNum = data.GreenSingleNumbers;
            //List<BitmapImage> NumColor;
            List<BitmapImage> Numberlist = new List<BitmapImage>();
            //
            ////Get correct number visuals
            //{
            //    if (OldMode)
            //    {
            //        NormalNum = data.OldSingleNumbers;
            //        BlueNum = data.OldBlueSingleNumbers;
            //        GreenNum = data.OldGreenSingleNumbers;
            //    }
            //
            //    if (CustomMode)
            //    {
            //        if (MainWindow.CustomNumbersFound)
            //        {
            //            NormalNum = data.CustomSingleNumbers;
            //        }
            //        if (MainWindow.CustomBlueNumbersFound)
            //        {
            //            BlueNum = data.CustomBlueSingleNumbers;
            //        }
            //        if (MainWindow.CustomGreenNumbersFound)
            //        {
            //            GreenNum = data.CustomGreenSingleNumbers;
            //        }
            //    }
            //}
            ////Get color
            //switch (color)
            //{
            //    case "Y":
            //        NumColor = NormalNum;
            //        break;
            //    case "B":
            //        NumColor = BlueNum;
            //        break;
            //    case "G":
            //        NumColor = GreenNum;
            //        break;
            //    default:
            //        NumColor = NormalNum;
            //        break;
            //}
            //
            ////if int is below 0 then we use the question mark and return
            //if (num < 0)
            //{
            //    Numberlist.Add(NumColor[10]);
            //    Numberlist.Add(NumColor[10]);
            //    Numberlist.Add(NumColor[10]);
            //
            //    return Numberlist;
            //}
            //
            ////split number into separate digits
            //List<int> listOfInts = new List<int>();
            //while (num > 0)
            //{
            //    listOfInts.Add(num % 10);
            //    num /= 10;
            //}
            //
            ////Set number images depending on number of digits
            //if (listOfInts.Count == 3)
            //{
            //    FinalNum[0] = listOfInts[0];
            //    FinalNum[1] = listOfInts[1];
            //    FinalNum[2] = listOfInts[2];
            //}
            //else if (listOfInts.Count == 2)
            //{
            //    FinalNum[0] = listOfInts[0];
            //    FinalNum[1] = listOfInts[1];
            //}
            //else if (listOfInts.Count == 1)
            //{
            //    FinalNum[0] = listOfInts[0];
            //}
            //
            //Numberlist.Add(NumColor[FinalNum[0]]);
            //Numberlist.Add(NumColor[FinalNum[1]]);
            //Numberlist.Add(NumColor[FinalNum[2]]);
            //
            return Numberlist;
        }

        //public void SetFoundNumber(Grid mainhint, Grid bhint)
        //{
        //    int worldvalue = GetWorldNumber(mainhint);
        //    bool number10s = false;
        //    bool number100s = false;
        //    if (worldvalue > 99) number100s = true;
        //    if (worldvalue > 9) number10s = true;
        //
        //    //get would numbers from main window
        //    int mainChildCount = VisualTreeHelper.GetChildrenCount(mainhint);
        //    ImageSource Num100 = null; ImageSource Num010 = null; ImageSource Num001 = null;
        //    for (int i = 0; i < mainChildCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(mainhint, i) as Image;
        //
        //        if (child == null)
        //            continue;
        //
        //        if (child is Image && child.Name.Equals(mainhint.Name + "_001"))
        //        {
        //            Num001 = child.Source;
        //            continue;
        //        }
        //        if (child is Image && child.Name.Equals(mainhint.Name + "_010"))
        //        {
        //            Num010 = child.Source;
        //            continue;
        //        }
        //        if (child is Image && child.Name.Equals(mainhint.Name + "_100"))
        //        {
        //            Num100 = child.Source;
        //            continue;
        //        }
        //    }
        //
        //    //set broadcast found numbers
        //    if (bhint != null)
        //    {
        //        string worldname = mainhint.Name.Substring(0, mainhint.Name.Length - 4);
        //        int ChildCount = VisualTreeHelper.GetChildrenCount(bhint);
        //        for (int i = 0; i < ChildCount; i++)
        //        {
        //            var child = VisualTreeHelper.GetChild(bhint, i) as Image;
        //
        //            if (child == null)
        //                continue;
        //
        //            if (child is Image && child.Name.Equals(worldname + "Found_001"))
        //            {
        //                child.Source = Num001;
        //                continue;
        //            }
        //            if (child is Image && child.Name.Equals(worldname + "Found_010"))
        //            {
        //                child.Source = Num010;
        //
        //                if (!number10s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
        //                    bhint.ColumnDefinitions[2].Width = new GridLength(0.0, GridUnitType.Star);
        //                else
        //                    bhint.ColumnDefinitions[2].Width = new GridLength(1.0, GridUnitType.Star);
        //
        //                continue;
        //            }
        //            if (child is Image && child.Name.Equals(worldname + "Found_100"))
        //            {
        //                child.Source = Num100;
        //
        //                if (!number100s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
        //                    bhint.ColumnDefinitions[1].Width = new GridLength(0.0, GridUnitType.Star);
        //                else
        //                    bhint.ColumnDefinitions[1].Width = new GridLength(1.0, GridUnitType.Star);
        //
        //                continue;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Grid broadcastG = this.FindName(mainhint.Name) as Grid;
        //        string worldname = mainhint.Name.Substring(0, mainhint.Name.Length - 4);
        //        int ChildCount = VisualTreeHelper.GetChildrenCount(broadcastG);
        //        for (int i = 0; i < ChildCount; i++)
        //        {
        //            var child = VisualTreeHelper.GetChild(broadcastG, i) as Image;
        //
        //            if (child == null)
        //                continue;
        //
        //            if (child is Image && child.Name.Equals(worldname + "Found_001"))
        //            {
        //                child.Source = Num001;
        //                continue;
        //            }
        //            if (child is Image && child.Name.Equals(worldname + "Found_010"))
        //            {
        //                child.Source = Num010;
        //
        //                if (!number10s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
        //                    broadcastG.ColumnDefinitions[2].Width = new GridLength(0.0, GridUnitType.Star);
        //                else
        //                    broadcastG.ColumnDefinitions[2].Width = new GridLength(1.0, GridUnitType.Star);
        //
        //                continue;
        //            }
        //            if (child is Image && child.Name.Equals(worldname + "Found_100"))
        //            {
        //                child.Source = Num100;
        //
        //                if (!number100s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
        //                    broadcastG.ColumnDefinitions[1].Width = new GridLength(0.0, GridUnitType.Star);
        //                else
        //                    broadcastG.ColumnDefinitions[1].Width = new GridLength(1.0, GridUnitType.Star);
        //
        //                continue;
        //            }
        //        }
        //    }
        //}

        public void SetTotalNumber(Grid hintgrid, string color, int value)
        {
            List<BitmapImage> numbers = UpdateNumber(value, color);
            bool number10s = false;
            bool number100s = false;
            if (value > 99)
                number100s = true;
            if (value > 9) 
                number10s = true;

            if (!number100s && !number10s) //adjust spacer width
                hintgrid.ColumnDefinitions[0].Width = new GridLength(1.0, GridUnitType.Star);
            else
                hintgrid.ColumnDefinitions[5].Width = new GridLength(0.0, GridUnitType.Star);

            //set broadcast found numbers
            string worldname = hintgrid.Name.Substring(0, hintgrid.Name.Length - 4);
            int ChildCount = VisualTreeHelper.GetChildrenCount(hintgrid);

            for (int i = 0; i < ChildCount; i++)
            {
                var child = VisualTreeHelper.GetChild(hintgrid, i) as Image;

                if (child == null)
                    continue;

                if (child is Image && child.Name.Equals(worldname + "Total_001"))
                {
                    child.Source = numbers[0];
                    continue;
                }
                if (child is Image && child.Name.Equals(worldname + "Total_010"))
                {
                    child.Source = numbers[1];

                    if (!number10s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
                        hintgrid.ColumnDefinitions[6].Width = new GridLength(0.0, GridUnitType.Star);
                    else
                        hintgrid.ColumnDefinitions[6].Width = new GridLength(1.0, GridUnitType.Star);

                    continue;
                }
                if (child is Image && child.Name.Equals(worldname + "Total_100"))
                {
                    child.Source = numbers[2];

                    if (!number100s || child.Source.ToString().ToLower().EndsWith("questionmark.png"))
                        hintgrid.ColumnDefinitions[5].Width = new GridLength(0.0, GridUnitType.Star);
                    else
                        hintgrid.ColumnDefinitions[5].Width = new GridLength(1.0, GridUnitType.Star);

                    continue;
                }
            }
        }

        //public int GetWorldNumber(Grid hintgrid)
        //{
        //    if (hintgrid == null)
        //        return 0;
        //    int Num100 = 0;
        //    int Num010 = 0;
        //    int Num001 = 0;
        //    string worldname = hintgrid.Name;
        //
        //    int ChildCount = VisualTreeHelper.GetChildrenCount(hintgrid);
        //
        //    for (int i = 0; i < ChildCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(hintgrid, i) as Image;
        //
        //        if (child == null)
        //            continue;
        //
        //        if (child is Image && child.Name.Equals(worldname + "_001"))
        //        {
        //            Num001 = GetImageNumber(child.Source.ToString());
        //            continue;
        //        }
        //
        //        if (child is Image && child.Name.Equals(worldname + "_010"))
        //        {
        //            Num010 = GetImageNumber(child.Source.ToString());
        //            continue;
        //        }
        //
        //        if (child is Image && child.Name.Equals(worldname + "_100"))
        //        {
        //            Num100 = GetImageNumber(child.Source.ToString());
        //            continue;
        //        }
        //    }
        //
        //    int Finalnum = Num001 + (Num010 * 10) + (Num100 * 100);
        //    return Finalnum;
        //}

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
    }
}
