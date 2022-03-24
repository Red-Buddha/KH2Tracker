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
        Dictionary<string, int> totals = new Dictionary<string, int>();
        Dictionary<string, int> important = new Dictionary<string, int>();
        Dictionary<string, ContentControl> Progression = new Dictionary<string, ContentControl>();
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
            worlds.Add("Report", 0);
            worlds.Add("TornPage", 0);
            worlds.Add("Fire", 0);
            worlds.Add("Blizzard", 0);
            worlds.Add("Thunder", 0);
            worlds.Add("Cure", 0);
            worlds.Add("Reflect", 0);
            worlds.Add("Magnet", 0);
            worlds.Add("Atlantica", 0);

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
            //important.Add("HadesCup", 0);

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

            foreach (Item item in data.Items)
            {
                item.UpdateTotal += new Item.TotalHandler(UpdateTotal);
                item.UpdateFound += new Item.FoundHandler(UpdateFound);
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
            string worldName = world;
            if (add) worlds[worldName]++; else worlds[worldName]--;
            //Console.WriteLine(worlds[worldName]);

            while (item.Any(char.IsDigit))
            {
                item = item.Remove(item.Length - 1, 1);
            }

            if (add) important[item]++; else important[item]--;
            worlds["Report"] = important["Report"];
            worlds["TornPage"] = important["TornPage"];
            worlds["Fire"] = important["Fire"];
            worlds["Blizzard"] = important["Blizzard"];
            worlds["Thunder"] = important["Thunder"];
            worlds["Cure"] = important["Cure"];
            worlds["Reflect"] = important["Reflect"];
            worlds["Magnet"] = important["Magnet"];
            //Console.WriteLine(item);

            UpdateNumbers();
            
        }

        public void UpdateNumbers()
        {
            //Get correct bar images
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage NumberBarB = data.SlashBarB;
            BitmapImage NumberBarY = data.SlashBarY;
            if (CustomMode)
            {
                if (MainWindow.CustomBarBFound)
                    NumberBarB = data.CustomSlashBarB;
                if (MainWindow.CustomBarYFound)
                    NumberBarY = data.CustomSlashBarY;
            }

            //Fix Broadcast window report and torn page numbers
            {
                ReportFoundBar.Source = NumberBarY;
                TornPageBar.Source = NumberBarY;
                CollectedBar.Source = NumberBarY;
                ReportFoundTotal.Source = GetDataNumber("Y")[14];
                TornPageTotal.Source = GetDataNumber("S")[6];
            }

            foreach (KeyValuePair<string,int> world in worlds)
            {
                if (world.Value < 52)
                {
                    //Correctly set yellow num and bar
                    BitmapImage number = GetDataNumber("Y")[world.Value + 1];

                    if ((data.WorldsData.ContainsKey(world.Key) && world.Key != "GoA" && data.WorldsData[world.Key].hintedHint == false)
                        || (data.WorldsData.ContainsKey(world.Key) && world.Key != "GoA" && data.WorldsData[world.Key].complete == false))
                    {
                        number = GetDataNumber("Y")[world.Value + 1];
                        Image bar = FindName(world.Key + "Bar") as Image;
                        bar.Source = NumberBarY;
                    }

                    if (data.WorldsData.ContainsKey(world.Key) && world.Key != "GoA" && data.WorldsData[world.Key].containsGhost)
                    {
                        number = GetDataNumber("G")[world.Value + 1];

                    }

                    if ((data.WorldsData.ContainsKey(world.Key) && world.Key != "GoA" && data.WorldsData[world.Key].hintedHint) 
                        || (data.WorldsData.ContainsKey(world.Key) &&  world.Key != "GoA" && data.WorldsData[world.Key].complete))
                    {
                        number = GetDataNumber("B")[world.Value + 1];
                        Image bar = FindName(world.Key + "Bar") as Image;
                        bar.Source = NumberBarB;
                    }

                    if (world.Key == "TornPage" || world.Key == "Fire" || world.Key == "Blizzard"
                        || world.Key == "Thunder" || world.Key == "Cure" || world.Key == "Reflect" || world.Key == "Magnet")
                    {
                        number = GetDataNumber("S")[world.Value + 1];
                    }

                    Image worldFound = this.FindName(world.Key + "Found") as Image;
                    worldFound.Source = number;

                    if (world.Key == "Fire" || world.Key == "Blizzard" || world.Key == "Thunder" 
                        || world.Key == "Cure" || world.Key == "Reflect" || world.Key == "Magnet")
                    {
                        if (world.Value == 0)
                        {
                            worldFound.Source = null;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, int> total in totals)
            {
                Image worldTotal = this.FindName(total.Key + "Total") as Image;
                if (total.Value <= -1)
                {
                    worldTotal.Source = GetDataNumber("S")[0];
                }
                else if (data.WorldsData.ContainsKey(total.Key) && total.Key != "GoA" && data.WorldsData[total.Key].containsGhost)
                {
                    if (total.Value <= 10)
                        worldTotal.Source = GetDataNumber("SG")[total.Value];
                    else
                        worldTotal.Source = GetDataNumber("G")[total.Value];
                }
                else if ((data.WorldsData.ContainsKey(total.Key) && total.Key != "GoA" && data.WorldsData[total.Key].hintedHint)
                    || (data.WorldsData.ContainsKey(total.Key) && total.Key != "GoA" && data.WorldsData[total.Key].complete))
                {
                    if (total.Value <= 10)
                        worldTotal.Source = GetDataNumber("SB")[total.Value];
                    else
                        worldTotal.Source = GetDataNumber("B")[total.Value];
                }
                else
                {
                    if (total.Value <= 10)
                        worldTotal.Source = GetDataNumber("S")[total.Value];
                    else
                        worldTotal.Source = GetDataNumber("Y")[total.Value];
                }

                // Format fixing for double digit numbers
                if (total.Key != "GoA" && total.Key != "Atlantica")
                {
                    if (total.Value >= 11)
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[3].Width = new GridLength(2, GridUnitType.Star);
                        (worldTotal.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                    }
                    else
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                        (worldTotal.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    }
                }
            }

            foreach(KeyValuePair<string,int> impCheck in important)
            {
                ContentControl imp = this.FindName(impCheck.Key) as ContentControl;
                if (impCheck.Value > 0)
                {
                    imp.Opacity = 1;
                }
                else
                {
                    if (impCheck.Key != "Report" && impCheck.Key != "TornPage")
                    imp.Opacity = 0.45;
                }
            }
        }

        public void UpdateTotal(string world, int checks)
        {
            string worldName = world;
            totals[worldName] = checks+1;

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
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage NumberBar = data.SlashBarY;

            if (CustomMode && MainWindow.CustomBarYFound)
            {
                NumberBar = data.CustomSlashBarY;
            }

            SorasHeartBar.Source = NumberBar;
            DriveFormsBar.Source = NumberBar;
            HollowBastionBar.Source = NumberBar;
            TwilightTownBar.Source = NumberBar;
            LandofDragonsBar.Source = NumberBar;
            BeastsCastleBar.Source = NumberBar;
            OlympusColiseumBar.Source = NumberBar;
            SpaceParanoidsBar.Source = NumberBar;
            HalloweenTownBar.Source = NumberBar;
            PortRoyalBar.Source = NumberBar;
            AgrabahBar.Source = NumberBar;
            PrideLandsBar.Source = NumberBar;
            DisneyCastleBar.Source = NumberBar;
            HundredAcreWoodBar.Source = NumberBar;
            SimulatedTwilightTownBar.Source = NumberBar;
            TWTNWBar.Source = NumberBar;
            AtlanticaBar.Source = NumberBar;
        }

        public void OnReset()
        {
            worlds.Clear();
            worlds.Add("SorasHeart", 0);
            worlds.Add("DriveForms", 0);
            worlds.Add("SimulatedTwilightTown", 0);
            worlds.Add("TwilightTown", 0);
            worlds.Add("HollowBastion", 0);
            worlds.Add("BeastsCastle", 0);
            worlds.Add("OlympusColiseum", 0);
            worlds.Add("Agrabah", 0);
            worlds.Add("LandofDragons", 0);
            worlds.Add("HundredAcreWood", 0);
            worlds.Add("PrideLands", 0);
            worlds.Add("DisneyCastle", 0);
            worlds.Add("HalloweenTown", 0);
            worlds.Add("PortRoyal", 0);
            worlds.Add("SpaceParanoids", 0);
            worlds.Add("TWTNW", 0);
            worlds.Add("GoA", 0);
            worlds.Add("Report", 0);
            worlds.Add("TornPage", 0);
            worlds.Add("Fire", 0);
            worlds.Add("Blizzard", 0);
            worlds.Add("Thunder", 0);
            worlds.Add("Cure", 0);
            worlds.Add("Reflect", 0);
            worlds.Add("Magnet", 0);
            worlds.Add("Atlantica", 0);

            totals.Clear();
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

            important.Clear();
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
            //important.Add("HadesCup", 0);

            OnResetHints();

            Collected.Source = GetDataNumber("Y")[1];
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

        //stuff to call the right number image
        public List<BitmapImage> GetDataNumber(string type)
        {
            bool OldMode = Properties.Settings.Default.OldNum;
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            var NormalNum = data.Numbers;
            var BlueNum = data.BlueNumbers;
            var GreenNum = data.GreenNumbers;
            var SingleNum = data.SingleNumbers;
            var SingleBlueNum = data.BlueSingleNumbers;
            var SingleGreenNum = data.GreenSingleNumbers;

            //Get correct numbers
            {
                if (OldMode)
                {
                    NormalNum = data.OldNumbers;
                    BlueNum = data.OldBlueNumbers;
                    GreenNum = data.OldGreenNumbers;
                    SingleNum = data.OldSingleNumbers;
                    SingleBlueNum = data.OldBlueSingleNumbers;
                    SingleGreenNum = data.OldGreenSingleNumbers;
                }

                if (CustomMode)
                {
                    if (MainWindow.CustomNumbersFound)
                    {
                        NormalNum = data.CustomNumbers;
                        SingleNum = data.CustomSingleNumbers;
                    }
                    if (MainWindow.CustomBlueNumbersFound)
                    {
                        BlueNum = data.CustomBlueNumbers;
                        SingleBlueNum = data.CustomBlueSingleNumbers;
                    }
                    if (MainWindow.CustomGreenNumbersFound)
                    {
                        GreenNum = data.CustomGreenNumbers;
                        SingleGreenNum = data.CustomGreenSingleNumbers;
                    }
                }
            }

            //return correct number list
            switch(type)
            {
                case "Y":
                    return NormalNum;
                case "B":
                    return BlueNum;
                case "S":
                    return SingleNum;
                case "SB":
                    return SingleBlueNum;
                case "G":
                    return GreenNum;
                case "SG":
                    return SingleGreenNum;
                default:
                    return NormalNum;
            }
        }
    }
}
