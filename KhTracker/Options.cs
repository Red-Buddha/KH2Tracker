using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        /// 
        /// Options
        ///

        private void SaveProgress(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FileName = "kh2fm-tracker-save";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                // save settings
                string settings = "Settings: ";
                if (PromiseCharmOption.IsChecked)
                    settings += "Promise Charm - ";
                if (ReportsOption.IsChecked)
                    settings += "Secret Ansem Reports - ";
                if (AbilitiesOption.IsChecked)
                    settings += "Once More & Second Chance - ";
                if (TornPagesOption.IsChecked)
                    settings += "Torn Pages - ";
                if (CureOption.IsChecked)
                    settings += "Cure - ";
                if (FinalFormOption.IsChecked)
                    settings += "Final Form - ";
                if (SoraHeartOption.IsChecked)
                    settings += "Sora's Heart - ";
                if (SimulatedOption.IsChecked)
                    settings += "Simulated Twilight Town - ";
                if (HundredAcreWoodOption.IsChecked)
                    settings += "100 Acre Wood - ";
                if (AtlanticaOption.IsChecked)
                    settings += "Atlantica - ";

                // save hint state (hint info, hints, track attempts)
                string attempts = "Attempts: ";
                string reportInfo = "Info: ";
                string locations = "Locations: ";
                if (data.hintsLoaded)
                {
                    foreach (int num in data.reportAttempts)
                    {
                        attempts += " - " + num.ToString();
                    }

                    foreach (Tuple<string, int> info in data.reportInformation)
                    {
                        reportInfo += " - " + info.Item1 + " " + info.Item2.ToString();
                    }

                    foreach (string location in data.reportLocations)
                    {
                        locations += " - " + location;
                    }
                }
                // store hint values
                string hintValues = "HintValues:";
                foreach (Image hint in data.Hints.Values.ToList())
                {
                    int num = 0;
                    for (int i = 0; i < data.Numbers.Count; ++i)
                    {
                        if (hint.Source == data.Numbers[i])
                            num = i;
                    }
                    hintValues += " " + num.ToString();
                }

                // save items in worlds
                string soraHeart = "SorasHeart:";
                foreach (Item item in data.Grids["SorasHeart"].Children)
                {
                    soraHeart += " " + item.Name;
                }
                string driveForms = "DriveForms:";
                foreach (Item item in data.Grids["DriveForms"].Children)
                {
                    driveForms += " " + item.Name;
                }
                string simulated = "SimulatedTwilightTown:";
                foreach (Item item in data.Grids["SimulatedTwilightTown"].Children)
                {
                    simulated += " " + item.Name;
                }
                string twilightTown = "TwilightTown:";
                foreach (Item item in data.Grids["TwilightTown"].Children)
                {
                    twilightTown += " " + item.Name;
                }
                string hollowBastion = "HollowBastion:";
                foreach (Item item in data.Grids["HollowBastion"].Children)
                {
                    hollowBastion += " " + item.Name;
                }
                string beastCastle = "BeastsCastle:";
                foreach (Item item in data.Grids["BeastsCastle"].Children)
                {
                    beastCastle += " " + item.Name;
                }
                string olympusColiseum = "OlympusColiseum:";
                foreach (Item item in data.Grids["OlympusColiseum"].Children)
                {
                    olympusColiseum += " " + item.Name;
                }
                string agrabah = "Agrabah:";
                foreach (Item item in data.Grids["Agrabah"].Children)
                {
                    agrabah += " " + item.Name;
                }
                string landOfDragons = "LandofDragons:";
                foreach (Item item in data.Grids["LandofDragons"].Children)
                {
                    landOfDragons += " " + item.Name;
                }
                string hundredAcreWood = "HundredAcreWood:";
                foreach (Item item in data.Grids["HundredAcreWood"].Children)
                {
                    hundredAcreWood += " " + item.Name;
                }
                string prideLands = "PrideLands:";
                foreach (Item item in data.Grids["PrideLands"].Children)
                {
                    prideLands += " " + item.Name;
                }
                string disneyCastle = "DisneyCastle:";
                foreach (Item item in data.Grids["DisneyCastle"].Children)
                {
                    disneyCastle += " " + item.Name;
                }
                string halloweenTown = "HalloweenTown:";
                foreach (Item item in data.Grids["HalloweenTown"].Children)
                {
                    halloweenTown += " " + item.Name;
                }
                string portRoyal = "PortRoyal:";
                foreach (Item item in data.Grids["PortRoyal"].Children)
                {
                    portRoyal += " " + item.Name;
                }
                string spaceparanoids = "SpaceParanoids:";
                foreach (Item item in data.Grids["SpaceParanoids"].Children)
                {
                    spaceparanoids += " " + item.Name;
                }
                string TWTNW = "TWTNW:";
                foreach (Item item in data.Grids["TWTNW"].Children)
                {
                    TWTNW += " " + item.Name;
                }
                string atlantica = "Atlantica:";
                foreach (Item item in data.Grids["Atlantica"].Children)
                {
                    atlantica += " " + item.Name;
                }
                string GoA = "GoA:";
                foreach (Item item in data.Grids["GoA"].Children)
                {
                    GoA += " " + item.Name;
                }

                FileStream file = File.Create(saveFileDialog.FileName);
                StreamWriter writer = new StreamWriter(file);

                writer.WriteLine(settings);
                writer.WriteLine(data.hintsLoaded.ToString());
                if (data.hintsLoaded)
                {
                    writer.WriteLine(attempts);
                    writer.WriteLine(data.hintFileText[0]);
                    writer.WriteLine(data.hintFileText[1]);
                }
                writer.WriteLine(hintValues);
                writer.WriteLine(soraHeart);
                writer.WriteLine(driveForms);
                writer.WriteLine(simulated);
                writer.WriteLine(twilightTown);
                writer.WriteLine(hollowBastion);
                writer.WriteLine(beastCastle);
                writer.WriteLine(olympusColiseum);
                writer.WriteLine(agrabah);
                writer.WriteLine(landOfDragons);
                writer.WriteLine(hundredAcreWood);
                writer.WriteLine(prideLands);
                writer.WriteLine(disneyCastle);
                writer.WriteLine(halloweenTown);
                writer.WriteLine(portRoyal);
                writer.WriteLine(spaceparanoids);
                writer.WriteLine(TWTNW);
                writer.WriteLine(atlantica);
                writer.WriteLine(GoA);

                writer.Close();
            }
        }

        private void LoadProgress(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FileName = "kh2fm-tracker-save";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                Stream file = openFileDialog.OpenFile();
                StreamReader reader = new StreamReader(file);
                // reset tracker
                OnReset(null, null);

                // set settings
                string settings = reader.ReadLine();
                LoadSettings(settings.Substring(10));

                // set hint state
                data.hintsLoaded = bool.Parse(reader.ReadLine());
                if (data.hintsLoaded)
                {
                    string attempts = reader.ReadLine();
                    attempts = attempts.Substring(13);
                    string[] attemptsArray = attempts.Split('-');
                    for (int i = 0; i < attemptsArray.Length; ++i)
                    {
                        data.reportAttempts[i] = int.Parse(attemptsArray[i]);
                    }

                    string line1 = reader.ReadLine();
                    data.hintFileText[0] = line1;
                    string[] reportvalues = line1.Split('.');

                    string line2 = reader.ReadLine();
                    data.hintFileText[1] = line2;
                    line2 = line2.TrimEnd('.');
                    string[] reportorder = line2.Split('.');

                    for (int i = 0; i < reportorder.Length; ++i)
                    {
                        data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
                        string[] temp = reportvalues[i].Split(',');
                        data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                    }
                }

                // set hint values (DUMB)
                string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
                SetReportValue(data.Hints["SorasHeart"], int.Parse(hintValues[0]));
                SetReportValue(data.Hints["DriveForms"], int.Parse(hintValues[1]));
                SetReportValue(data.Hints["SimulatedTwilightTown"], int.Parse(hintValues[2]));
                SetReportValue(data.Hints["TwilightTown"], int.Parse(hintValues[3]));
                SetReportValue(data.Hints["HollowBastion"], int.Parse(hintValues[4]));
                SetReportValue(data.Hints["BeastsCastle"], int.Parse(hintValues[5]));
                SetReportValue(data.Hints["OlympusColiseum"], int.Parse(hintValues[6]));
                SetReportValue(data.Hints["Agrabah"], int.Parse(hintValues[7]));
                SetReportValue(data.Hints["LandofDragons"], int.Parse(hintValues[8]));
                SetReportValue(data.Hints["HundredAcreWood"], int.Parse(hintValues[9]));
                SetReportValue(data.Hints["PrideLands"], int.Parse(hintValues[10]));
                SetReportValue(data.Hints["DisneyCastle"], int.Parse(hintValues[11]));
                SetReportValue(data.Hints["HalloweenTown"], int.Parse(hintValues[12]));
                SetReportValue(data.Hints["PortRoyal"], int.Parse(hintValues[13]));
                SetReportValue(data.Hints["SpaceParanoids"], int.Parse(hintValues[14]));
                SetReportValue(data.Hints["TWTNW"], int.Parse(hintValues[15]));
                SetReportValue(data.Hints["Atlantica"], int.Parse(hintValues[16]));

                // add items to worlds
                while (reader.EndOfStream == false)
                {
                    string world = reader.ReadLine();
                    string worldName = world.Substring(0, world.IndexOf(':'));
                    string items = world.Substring(world.IndexOf(':') + 1).Trim();
                    if (items != string.Empty)
                    {
                        foreach (string item in items.Split(' '))
                        {
                            WorldGrid grid = FindName(worldName + "Grid") as WorldGrid;
                            Item importantCheck = FindName(item) as Item;

                            if (grid.Handle_Report(importantCheck, this, data))
                                grid.Add_Item(importantCheck, this);
                        }
                    }
                }
            }
        }

        private void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Path.GetExtension(files[0]).ToUpper() == ".TXT")
                    LoadHints(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                    ParseSeed(files[0]);
            }
        }

        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.Title = "Select Hints File";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadHints(openFileDialog.FileName);
            }
        }

        public void LoadHints(string filename)
        {
            SetMode(Mode.Hints);
            ResetHints();
            
            StreamReader streamReader = new StreamReader(filename);

            if (streamReader.EndOfStream)
            {
                HintText.Content = "Error loading hints";
                streamReader.Close();
                return;
            }

            string line1 = streamReader.ReadLine();
            data.hintFileText[0] = line1;
            string[] reportvalues = line1.Split('.');

            if (streamReader.EndOfStream)
            {
                HintText.Content = "Error loading hints";
                streamReader.Close();
                return;
            }

            string line2 = streamReader.ReadLine();
            data.hintFileText[1] = line2;
            line2 = line2.TrimEnd('.');
            string[] reportorder = line2.Split('.');

            LoadSettings(streamReader.ReadLine().Substring(24));

            streamReader.Close();

            for (int i = 0; i < reportorder.Length; ++i)
            {
                string location = data.codes.FindCode(reportorder[i]);
                if (location == "")
                    location = data.codes.GetDefault(i);

                data.reportLocations.Add(location);
                string[] temp = reportvalues[i].Split(',');
                data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
            }

            data.hintsLoaded = true;
            HintText.Content = "Hints Loaded";
        }

        private void ResetHints()
        {
            data.hintsLoaded = false;
            data.reportLocations.Clear();
            data.reportInformation.Clear();
            data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

            foreach (var key in data.HintedWorlds.Keys.ToList())
            {
                data.HintedWorlds[key] = false;
            }
            data.HintedWorlds["GoA"] = true;

            foreach (var key in data.HintedHintWorlds.Keys.ToList())
            {
                data.HintedHintWorlds[key] = false;
            }

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }
            
            foreach (Image hint in data.Hints.Values.ToList())
            {
                hint.Source = data.Numbers[0];
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }

            broadcast.OnResetHints();
        }

        private void LoadSettings(string settings)
        {
            bool[] newsettings = new bool[10];

            string[] settinglist = settings.Split('-');
            foreach (string setting in settinglist)
            {
                string trimmed = setting.Trim();
                switch (trimmed)
                {
                    case "Promise Charm":
                        newsettings[0] = true;
                        break;
                    case "Secret Ansem Reports":
                        newsettings[1] = true;
                        break;
                    case "Second Chance & Once More":
                        newsettings[2] = true;
                        break;
                    case "Torn Pages":
                        newsettings[3] = true;
                        break;
                    case "Cure":
                        newsettings[4] = true;
                        break;
                    case "Final Form":
                        newsettings[5] = true;
                        break;
                    case "Sora's Heart":
                        newsettings[6] = true;
                        break;
                    case "Simulated Twilight Town":
                        newsettings[7] = true;
                        break;
                    case "100 Acre Wood":
                        newsettings[8] = true;
                        break;
                    case "Atlantica":
                        newsettings[9] = true;
                        break;
                }
            }

            PromiseCharmToggle(newsettings[0]);
            ReportsToggle(newsettings[1]);
            AbilitiesToggle(newsettings[2]);
            TornPagesToggle(newsettings[3]);
            CureToggle(newsettings[4]);
            FinalFormToggle(newsettings[5]);
            SoraHeartToggle(newsettings[6]);
            SimulatedToggle(newsettings[7]);
            HundredAcreWoodToggle(newsettings[8]);
            AtlanticaToggle(newsettings[9]);

        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            ModeDisplay.Header = "Hints Mode";
            data.mode = Mode.Hints;

            collected = 0;
            Collected.Source = data.Numbers[1];
            HintText.Content = "";

            if (data.selected != null)
            {
                data.SelectedBars[data.selected.Name].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
            }
            data.selected = null;

            foreach (WorldGrid grid in data.Grids.Values.ToList())
            {
                for (int j = grid.Children.Count - 1; j >= 0; --j)
                {
                    Item item = grid.Children[j] as Item;
                    grid.Children.Remove(grid.Children[j]);
                    ItemPool.Children.Add(item);

                    item.MouseDown -= item.Item_Return;
                    item.MouseEnter -= item.Report_Hover;
                    if (data.dragDrop)
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;
                        item.MouseMove += item.Item_MouseMove;
                    }
                    else
                    {
                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                        item.MouseUp += item.Item_MouseUp;
                    }
                }
            }

            // Reset 1st column row heights
            RowDefinitionCollection rows1 = ((data.Grids["SorasHeart"].Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows1)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            // Reset 2nd column row heights
            RowDefinitionCollection rows2 = ((data.Grids["DriveForms"].Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows2)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            ResetHints();

            foreach (var key in data.HintedWorlds.Keys.ToList())
            {
                data.HintedWorlds[key] = false;
            }
            data.HintedWorlds["GoA"] = true;

            foreach (var key in data.HintedHintWorlds.Keys.ToList())
            {
                data.HintedHintWorlds[key] = false;
            }

            foreach (var key in data.WorldComplete.Keys.ToList())
            {
                data.WorldComplete[key] = false;
            }

            foreach (var key in data.WorldCheckCount.Keys.ToList())
            {
                data.WorldCheckCount[key].Clear();
            }

            LevelIcon.Visibility = Visibility.Hidden;
            Level.Visibility = Visibility.Hidden;
            StrengthIcon.Visibility = Visibility.Hidden;
            Strength.Visibility = Visibility.Hidden;
            MagicIcon.Visibility = Visibility.Hidden;
            Magic.Visibility = Visibility.Hidden;
            DefenseIcon.Visibility = Visibility.Hidden;
            Defense.Visibility = Visibility.Hidden;
            Weapon.Visibility = Visibility.Hidden;

            broadcast.LevelIcon.Visibility = Visibility.Hidden;
            broadcast.Level.Visibility = Visibility.Hidden;
            broadcast.StrengthIcon.Visibility = Visibility.Hidden;
            broadcast.Strength.Visibility = Visibility.Hidden;
            broadcast.MagicIcon.Visibility = Visibility.Hidden;
            broadcast.Magic.Visibility = Visibility.Hidden;
            broadcast.DefenseIcon.Visibility = Visibility.Hidden;
            broadcast.Defense.Visibility = Visibility.Hidden;
            broadcast.Weapon.Visibility = Visibility.Hidden;

            broadcast.GrowthAbilityRow.Height = new GridLength(0, GridUnitType.Star);

            // Reset / Turn off auto tracking
            collectedChecks.Clear();
            newChecks.Clear();
            if (aTimer != null)
                aTimer.Stop();

            fireLevel = 0;
            blizzardLevel = 0;
            thunderLevel = 0;
            cureLevel = 0;
            reflectLevel = 0;
            magnetLevel = 0;
            tornPageCount = 0;

            broadcast.OnReset();
            broadcast.UpdateNumbers();
        }
        
        private void BroadcastWindow_Open(object sender, RoutedEventArgs e)
        {
            broadcast.Show();
        }

        private void ParseSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".pnach";
            openFileDialog.Filter = "pnach files (*.pnach)|*.pnach";
            openFileDialog.Title = "Select Seed File";
            if (openFileDialog.ShowDialog() == true)
            {
                ParseSeed(openFileDialog.FileName);
            }
        }

        public void ParseSeed(string filename)
        {
            SetMode(Mode.AltHints);

            foreach (string world in data.WorldCheckCount.Keys.ToList())
            {
                data.WorldCheckCount[world].Clear();
            }

            StreamReader streamReader = new StreamReader(filename);
            bool check1 = false;
            bool check2 = false;

            while (streamReader.EndOfStream == false)
            {
                string line = streamReader.ReadLine();

                string[] codes = line.Split(',');
                if (codes.Length == 5)
                {
                    string world = data.codes.FindCode(codes[2]);

                    //stupid fix
                    string[] idCode = codes[4].Split('/', ' ');

                    int id = Convert.ToInt32(idCode[0], 16);
                    if (world == "" || world == "GoA" || data.codes.itemCodes.ContainsKey(id) == false || (id >= 226 && id <= 238))
                        continue;

                    string item = data.codes.itemCodes[Convert.ToInt32(codes[4], 16)];
                    data.WorldCheckCount[world].Add(item);
                }
                else if (codes.Length == 1)
                {
                    if (codes[0] == "//Remove High Jump LVl" || codes[0] == "//Remove Quick Run LVl")
                    {
                        check1 = true;
                    }
                    else if (codes[0] == "//Remove Dodge Roll LVl")
                    {
                        check2 = true;
                    }
                }
            }
            streamReader.Close();

            if (check1 == true && check2 == false)
            {
                foreach (string world in data.WorldCheckCount.Keys.ToList())
                {
                    data.WorldCheckCount[world].Clear();
                }
            }

            foreach (var key in data.Grids.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.Grids[key].WorldComplete();
                SetReportValue(data.Hints[key], 1);
            }
        }

        private void SetMode(Mode mode)
        {
            if (data.mode != mode || mode == Mode.AltHints)
                OnReset(null, null);

            if (mode == Mode.AltHints)
            {
                ModeDisplay.Header = "Alt Hints Mode";
                data.mode = mode;
                ReportsToggle(false);
            }
            else
            {
                ModeDisplay.Header = "Hints Mode";
                data.mode = mode;
            }
        }
    }
}
