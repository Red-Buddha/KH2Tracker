using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Text.Json;
using Path = System.IO.Path;

//using System.Text.Json.Serialization;
//using YamlDotNet.Serialization;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        /// 
        /// Options
        ///

        private void SaveProgress(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "txt files (*.txt)|*.txt",
                FileName = "kh2fm-tracker-save",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName);
            }
        }

        ///TODO: redo entire saving code once i have everything set
        public void Save(string filename) { }
        //{
        //    string mode = "Mode: " + data.mode.ToString();
        //
        //    // save settings
        //    string settings = "Settings: ";
        //    if (PromiseCharmOption.IsChecked)
        //        settings += "Promise Charm - ";
        //    if (ReportsOption.IsChecked)
        //        settings += "Secret Ansem Reports - ";
        //    if (AbilitiesOption.IsChecked)
        //        settings += "Second Chance & Once More - ";
        //    //if (TornPagesOption.IsChecked)
        //    //    settings += "Torn Pages - ";
        //    //if (CureOption.IsChecked)
        //    //    settings += "Cure - ";
        //    //if (FinalFormOption.IsChecked)
        //    //    settings += "Final Form - ";
        //    if (VisitLockOption.IsChecked)
        //        settings += "Visit Locks - ";
        //    if (ExtraChecksOption.IsChecked)
        //        settings += "Extra Checks - ";
        //    if (SoraHeartOption.IsChecked)
        //        settings += "Sora's Heart - ";
        //    if (SoraLevel01Option.IsChecked)
        //        settings += "Level01 - ";
        //    if (SoraLevel50Option.IsChecked)
        //        settings += "Level50 - ";
        //    if (SoraLevel99Option.IsChecked)
        //        settings += "Level99 - ";
        //    if (SimulatedOption.IsChecked)
        //        settings += "Simulated Twilight Town - ";
        //    if (HundredAcreWoodOption.IsChecked)
        //        settings += "100 Acre Wood - ";
        //    if (AtlanticaOption.IsChecked)
        //        settings += "Atlantica - ";
        //    //if (CavernOption.IsChecked)
        //    //    settings += "Cavern of Remembrance - ";
        //    //if (OCCupsOption.IsChecked)
        //    //    settings += "Olympus Cups - ";
        //    //if (TerraOption.IsChecked)
        //    //    settings += "Lingering Will (Terra) - ";
        //    if (PuzzleOption.IsChecked)
        //        settings += "Puzzles - ";
        //    if (SynthOption.IsChecked)
        //        settings += "Synthesis - ";
        //
        //    // save hint state (hint info, hints, track attempts)
        //    string attempts = "";
        //    string hintValues = "";
        //    if (data.mode == Mode.Hints || data.mode == Mode.OpenKHHints || data.mode == Mode.DAHints || data.mode == Mode.PathHints || data.mode == Mode.SpoilerHints)
        //    {
        //        attempts = "Attempts: ";
        //        if (data.hintsLoaded || data.mode == Mode.SpoilerHints)
        //        {
        //            foreach (int num in data.reportAttempts)
        //            {
        //                attempts += " - " + num.ToString();
        //            }
        //        }
        //
        //        // store hint values
        //        hintValues = "HintValues: ";
        //        foreach (WorldData worldData in data.WorldsData.Values.ToList())
        //        {
        //            if (worldData.value == null)
        //                continue;
        //
        //            int num = -1; //= GetWorldNumber(worldData.hint);
        //            if (worldData.value.Text != "?")
        //                num = int.Parse(worldData.value.Text);
        //            if (worldData.containsGhost && GhostMathOption.IsChecked) //need to recaculate correct values if ghost items and automath are toggled
        //            {
        //                num += GetGhostPoints(worldData.worldGrid);
        //            }
        //            hintValues += num.ToString() + " ";
        //        }
        //    }
        //
        //    FileStream file = File.Create(filename);
        //    StreamWriter writer = new StreamWriter(file);
        //
        //    writer.WriteLine(mode);
        //    writer.WriteLine(settings);
        //    if (data.mode == Mode.Hints)
        //    {
        //        writer.WriteLine(attempts);
        //        writer.WriteLine(data.hintFileText[0]);
        //        writer.WriteLine(data.hintFileText[1]);
        //        writer.WriteLine(hintValues);
        //    }
        //    else if (data.mode == Mode.OpenKHHints)
        //    {
        //        writer.WriteLine(attempts);
        //        writer.WriteLine(data.openKHHintText);
        //        writer.WriteLine(hintValues);
        //    }
        //    else if (data.mode == Mode.AltHints)
        //    {
        //        Dictionary<string, List<string>> test = new Dictionary<string, List<string>>();
        //        foreach (string key in data.WorldsData.Keys.ToList())
        //        {
        //            test.Add(key, data.WorldsData[key].checkCount);
        //        }
        //        string hintObject = JsonSerializer.Serialize(test);
        //        string hintText = Convert.ToBase64String(Encoding.UTF8.GetBytes(hintObject));
        //        writer.WriteLine(hintText);
        //    }
        //    else if (data.mode == Mode.OpenKHAltHints)
        //    {
        //        writer.WriteLine(data.openKHHintText);
        //    }
        //    else if (data.mode == Mode.PathHints)
        //    {
        //        writer.WriteLine(attempts);
        //        writer.WriteLine(data.openKHHintText);
        //        writer.WriteLine(hintValues);
        //    }
        //    else if (data.mode == Mode.DAHints)
        //    {
        //        writer.WriteLine(attempts);
        //        writer.WriteLine(data.openKHHintText);
        //        string worlditemlist = JsonSerializer.Serialize(Data.WorldItems);
        //        string worlditemlist64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(worlditemlist));
        //        writer.WriteLine(worlditemlist64);
        //        string reportlist = JsonSerializer.Serialize(data.TrackedReports);
        //        string reportlist64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(reportlist));
        //        writer.WriteLine(reportlist64);
        //        writer.WriteLine(hintValues);
        //    }
        //    else if (data.mode == Mode.SpoilerHints)
        //    {
        //        writer.WriteLine(attempts);
        //        writer.WriteLine(data.openKHHintText);
        //        writer.WriteLine(hintValues);
        //    }
        //
        //    string ProgressString = "Progress:";
        //    foreach (string WorldName in data.WorldsData.Keys.ToList())
        //    {
        //        if (WorldName != "GoA" && WorldName != "SorasHeart" && WorldName != "DriveForms" && WorldName != "PuzzSynth")
        //            ProgressString += " " + data.WorldsData[WorldName].progress.ToString();
        //    }
        //    writer.WriteLine(ProgressString);
        //
        //    foreach (string WorldName in data.WorldsData.Keys.ToList())
        //    {
        //        string ItemString = WorldName + ":";
        //        foreach (Item item in data.WorldsData[WorldName].worldGrid.Children)
        //        {
        //            ItemString += " " + item.Name;
        //        }
        //        writer.WriteLine(ItemString);
        //    }
        //
        //    writer.Close();
        //}

        private void LoadProgress(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "txt files (*.txt)|*.txt",
                FileName = "kh2fm-tracker-save",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Load(openFileDialog.FileName);
            }
        }

        ///TODO: redo entire loading code once i have everything set
        private void Load(string filename) { }
        //{
        //    Stream file = File.Open(filename, FileMode.Open);
        //    StreamReader reader = new StreamReader(file);
        //    // reset tracker
        //    OnReset(null, null);
        //
        //    string mode = reader.ReadLine().Substring(6);
        //    if (mode == "Hints")
        //        SetMode(Mode.Hints);
        //    else if (mode == "AltHints")
        //        SetMode(Mode.AltHints);
        //    else if (mode == "OpenKHHints")
        //        SetMode(Mode.OpenKHHints);
        //    else if (mode == "OpenKHAltHints")
        //        SetMode(Mode.OpenKHAltHints);
        //    else if (mode == "DAHints")
        //        SetMode(Mode.DAHints);
        //    else if (mode == "PathHints")
        //        SetMode(Mode.PathHints);
        //    else if (mode == "SpoilerHints")
        //        SetMode(Mode.SpoilerHints);
        //
        //    // set settings
        //    string settings = reader.ReadLine();
        //    LoadSettings(settings.Substring(10));
        //
        //    // set hint state
        //    if (mode == "Hints")
        //    {
        //        string attempts = reader.ReadLine();
        //        attempts = attempts.Substring(13);
        //        string[] attemptsArray = attempts.Split('-');
        //        for (int i = 0; i < attemptsArray.Length; ++i)
        //        {
        //            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
        //        }
        //        
        //        string line1 = reader.ReadLine();
        //        data.hintFileText[0] = line1;
        //        string[] reportvalues = line1.Split('.');
        //
        //        string line2 = reader.ReadLine();
        //        data.hintFileText[1] = line2;
        //        line2 = line2.TrimEnd('.');
        //        string[] reportorder = line2.Split('.');
        //
        //        for (int i = 0; i < reportorder.Length; ++i)
        //        {
        //            data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
        //            string[] temp = reportvalues[i].Split(',');
        //            data.reportInformation.Add(new Tuple<string, string, int>(null, data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
        //        }
        //
        //        data.hintsLoaded = true;
        //        HintText.Text = "Hints Loaded";
        //    }
        //    else if (mode == "AltHints")
        //    {
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
        //        var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintText);
        //
        //        foreach (var world in worlds)
        //        {
        //            if (world.Key == "GoA")
        //            {
        //                continue;
        //            }
        //            foreach (var item in world.Value)
        //            {
        //                data.WorldsData[world.Key].checkCount.Add(item);
        //            }
        //
        //        }
        //        foreach (var key in data.WorldsData.Keys.ToList())
        //        {
        //            if (key == "GoA")
        //                continue;
        //
        //            data.WorldsData[key].worldGrid.WorldComplete();
        //            SetWorldValue(data.WorldsData[key].value, 0);
        //        }
        //    }
        //    else if (mode == "OpenKHHints")
        //    {
        //        string attempts = reader.ReadLine();
        //        attempts = attempts.Substring(13);
        //        string[] attemptsArray = attempts.Split('-');
        //        for (int i = 0; i < attemptsArray.Length; ++i)
        //        {
        //            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
        //        }
        //        data.openKHHintText = reader.ReadLine();
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
        //        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
        //        JsmarteeHints(hintObject);
        //
        //    }
        //    else if (mode == "OpenKHAltHints")
        //    {
        //        data.openKHHintText = reader.ReadLine();
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
        //        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
        //        ShanHints(hintObject);
        //
        //    }
        //    else if (mode == "DAHints")
        //    {
        //        string attempts = reader.ReadLine();
        //        attempts = attempts.Substring(13);
        //        string[] attemptsArray = attempts.Split('-');
        //        for (int i = 0; i < attemptsArray.Length; ++i)
        //        {
        //            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
        //        }
        //        data.openKHHintText = reader.ReadLine();
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
        //        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
        //        PointsHints(hintObject);
        //
        //        var witemlist64 = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
        //        var witemlist = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(witemlist64);
        //        Data.WorldItems = witemlist;
        //
        //        var reportlist64 = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
        //        var reportlist = JsonSerializer.Deserialize<List<string>>(reportlist64);
        //        data.TrackedReports = reportlist;
        //    }
        //    else if (mode == "PathHints")
        //    {
        //        string attempts = reader.ReadLine();
        //        attempts = attempts.Substring(13);
        //        string[] attemptsArray = attempts.Split('-');
        //        for (int i = 0; i < attemptsArray.Length; ++i)
        //        {
        //            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
        //        }
        //        data.openKHHintText = reader.ReadLine();
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
        //        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
        //        PathHints(hintObject);
        //        //var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
        //        //
        //        //List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
        //        //reportKeys.Sort();
        //        //
        //        //foreach (var report in reportKeys)
        //        //{
        //        //    var hinttext = reports[report.ToString()]["Text"].ToString();
        //        //    var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
        //        //
        //        //    data.reportInformation.Add(new Tuple<string, int>(hinttext, 0));
        //        //    data.reportLocations.Add(location);
        //        //}
        //        //
        //        //data.hintsLoaded = true;
        //        //HintText.Content = "Hints Loaded";
        //    }
        //    else if (mode == "SpoilerHints")
        //    {
        //        string attempts = reader.ReadLine();
        //        attempts = attempts.Substring(13);
        //        string[] attemptsArray = attempts.Split('-');
        //        for (int i = 0; i < attemptsArray.Length; ++i)
        //        {
        //            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
        //        }
        //        data.openKHHintText = reader.ReadLine();
        //        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
        //        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
        //        SpoilerHints(hintObject);
        //    }
        //
        //    // set hint values (DUMB)
        //    if (data.hintsLoaded)
        //    {
        //        string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
        //        SetWorldValue(data.WorldsData["SorasHeart"].value, int.Parse(hintValues[0]));
        //        SetWorldValue(data.WorldsData["DriveForms"].value, int.Parse(hintValues[1]));
        //        SetWorldValue(data.WorldsData["SimulatedTwilightTown"].value, int.Parse(hintValues[2]));
        //        SetWorldValue(data.WorldsData["TwilightTown"].value, int.Parse(hintValues[3]));
        //        SetWorldValue(data.WorldsData["HollowBastion"].value, int.Parse(hintValues[4]));
        //        SetWorldValue(data.WorldsData["BeastsCastle"].value, int.Parse(hintValues[5]));
        //        SetWorldValue(data.WorldsData["OlympusColiseum"].value, int.Parse(hintValues[6]));
        //        SetWorldValue(data.WorldsData["Agrabah"].value, int.Parse(hintValues[7]));
        //        SetWorldValue(data.WorldsData["LandofDragons"].value, int.Parse(hintValues[8]));
        //        SetWorldValue(data.WorldsData["HundredAcreWood"].value, int.Parse(hintValues[9]));
        //        SetWorldValue(data.WorldsData["PrideLands"].value, int.Parse(hintValues[10]));
        //        SetWorldValue(data.WorldsData["DisneyCastle"].value, int.Parse(hintValues[11]));
        //        SetWorldValue(data.WorldsData["HalloweenTown"].value, int.Parse(hintValues[12]));
        //        SetWorldValue(data.WorldsData["PortRoyal"].value, int.Parse(hintValues[13]));
        //        SetWorldValue(data.WorldsData["SpaceParanoids"].value, int.Parse(hintValues[14]));
        //        SetWorldValue(data.WorldsData["TWTNW"].value, int.Parse(hintValues[15]));
        //        SetWorldValue(data.WorldsData["Atlantica"].value, int.Parse(hintValues[16]));
        //        SetWorldValue(data.WorldsData["PuzzSynth"].value, int.Parse(hintValues[17]));
        //    }
        //    else if (mode == "SpoilerHints") //we need to do this for spoiler hints because of the optional report mode
        //        reader.ReadLine();
        //
        //    string[] progress = reader.ReadLine().Substring(10).Split(' ');
        //    data.WorldsData["SimulatedTwilightTown"].progress = int.Parse(progress[0]);
        //    data.WorldsData["TwilightTown"].progress = int.Parse(progress[1]);
        //    data.WorldsData["HollowBastion"].progress = int.Parse(progress[2]);
        //    data.WorldsData["BeastsCastle"].progress = int.Parse(progress[3]);
        //    data.WorldsData["OlympusColiseum"].progress = int.Parse(progress[4]);
        //    data.WorldsData["Agrabah"].progress = int.Parse(progress[5]);
        //    data.WorldsData["LandofDragons"].progress = int.Parse(progress[6]);
        //    data.WorldsData["HundredAcreWood"].progress = int.Parse(progress[7]);
        //    data.WorldsData["PrideLands"].progress = int.Parse(progress[8]);
        //    data.WorldsData["DisneyCastle"].progress = int.Parse(progress[9]);
        //    data.WorldsData["HalloweenTown"].progress = int.Parse(progress[10]);
        //    data.WorldsData["PortRoyal"].progress = int.Parse(progress[11]);
        //    data.WorldsData["SpaceParanoids"].progress = int.Parse(progress[12]);
        //    data.WorldsData["TWTNW"].progress = int.Parse(progress[13]);
        //    data.WorldsData["Atlantica"].progress = int.Parse(progress[14]);
        //
        //    SetProgressIcons();
        //
        //    // add items to worlds
        //    while (reader.EndOfStream == false)
        //    {
        //        string world = reader.ReadLine();
        //        string worldName = world.Substring(0, world.IndexOf(':'));
        //        string items = world.Substring(world.IndexOf(':') + 1).Trim();
        //
        //        if (items != string.Empty)
        //        {
        //            foreach (string item in items.Split(' '))
        //            {
        //                WorldGrid grid = FindName(worldName + "Grid") as WorldGrid;
        //                Item importantCheck = FindName(item) as Item;
        //
        //                if (grid.ReportHandler(importantCheck))
        //                {
        //                    switch (data.mode)
        //                    {
        //                        case Mode.DAHints:
        //                            if (item.StartsWith("Ghost_"))
        //                                grid.Add_Ghost(importantCheck);
        //                            else
        //                                grid.Add_Item(importantCheck);
        //                            break;
        //                        case Mode.SpoilerHints:
        //                            if (!item.StartsWith("Ghost_"))
        //                                grid.Add_Item(importantCheck);
        //                            break;
        //                        case Mode.PathHints:
        //                        default:
        //                            grid.Add_Item(importantCheck);
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //
        //    reader.Close();
        //}

        private void SetProgressIcons()
        {
            bool OldToggled = Properties.Settings.Default.OldProg;
            bool CustomToggled = Properties.Settings.Default.CustomIcons;
            string Prog = "Min-"; //Default
            if (OldToggled)
                Prog = "Old-";
            if (CustomProgFound && CustomToggled)
                Prog = "Cus-";

            foreach (string world in data.WorldsData.Keys.ToList())
            {
                if (world == "SorasHeart" || world == "DriveForms" || world == "PuzzSynth")
                    continue;

                data.WorldsData[world].progression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys[world][data.WorldsData[world].progress]);
                data.WorldsData[world].progression.ToolTip = data.ProgressKeys[world + "Desc"][data.WorldsData[world].progress];
            }

            //////////
            //string STTkey = Prog + data.ProgressKeys["SimulatedTwilightTown"][data.WorldsData["SimulatedTwilightTown"].progress];
            //string STTDes = data.ProgressKeys["SimulatedTwilightTownDesc"][data.WorldsData["SimulatedTwilightTown"].progress];
            //data.WorldsData["SimulatedTwilightTown"].progression.SetResourceReference(ContentProperty, STTkey);
            //data.WorldsData["SimulatedTwilightTown"].progression.ToolTip = STTDes;
            //
            //string TTkey = Prog + data.ProgressKeys["TwilightTown"][data.WorldsData["TwilightTown"].progress];
            //data.WorldsData["TwilightTown"].progression.SetResourceReference(ContentProperty, TTkey);
            //
            //string HBkey = Prog + data.ProgressKeys["HollowBastion"][data.WorldsData["HollowBastion"].progress];
            //data.WorldsData["HollowBastion"].progression.SetResourceReference(ContentProperty, HBkey);
            //
            //string BCkey = Prog + data.ProgressKeys["BeastsCastle"][data.WorldsData["BeastsCastle"].progress];
            //data.WorldsData["BeastsCastle"].progression.SetResourceReference(ContentProperty, BCkey);
            //
            //string OCkey = Prog + data.ProgressKeys["OlympusColiseum"][data.WorldsData["OlympusColiseum"].progress];
            //data.WorldsData["OlympusColiseum"].progression.SetResourceReference(ContentProperty, OCkey);
            //
            //string AGkey = Prog + data.ProgressKeys["Agrabah"][data.WorldsData["Agrabah"].progress];
            //data.WorldsData["Agrabah"].progression.SetResourceReference(ContentProperty, AGkey);
            //
            //string LoDkey = Prog + data.ProgressKeys["LandofDragons"][data.WorldsData["LandofDragons"].progress];
            //data.WorldsData["LandofDragons"].progression.SetResourceReference(ContentProperty, LoDkey);
            //
            //string HAWkey = Prog + data.ProgressKeys["HundredAcreWood"][data.WorldsData["HundredAcreWood"].progress];
            //data.WorldsData["HundredAcreWood"].progression.SetResourceReference(ContentProperty, HAWkey);
            //
            //string PLkey = Prog + data.ProgressKeys["PrideLands"][data.WorldsData["PrideLands"].progress];
            //data.WorldsData["PrideLands"].progression.SetResourceReference(ContentProperty, PLkey);
            //
            //string DCkey = Prog + data.ProgressKeys["DisneyCastle"][data.WorldsData["DisneyCastle"].progress];
            //data.WorldsData["DisneyCastle"].progression.SetResourceReference(ContentProperty, DCkey);
            //
            //string HTkey = Prog + data.ProgressKeys["HalloweenTown"][data.WorldsData["HalloweenTown"].progress];
            //data.WorldsData["HalloweenTown"].progression.SetResourceReference(ContentProperty, HTkey);
            //
            //string PRkey = Prog + data.ProgressKeys["PortRoyal"][data.WorldsData["PortRoyal"].progress];
            //data.WorldsData["PortRoyal"].progression.SetResourceReference(ContentProperty, PRkey);
            //
            //string SPkey = Prog + data.ProgressKeys["SpaceParanoids"][data.WorldsData["SpaceParanoids"].progress];
            //data.WorldsData["SpaceParanoids"].progression.SetResourceReference(ContentProperty, SPkey);
            //
            //string TWTNWkey = Prog + data.ProgressKeys["TWTNW"][data.WorldsData["TWTNW"].progress];
            //data.WorldsData["TWTNW"].progression.SetResourceReference(ContentProperty, TWTNWkey);
            //
            //string ATkey = Prog + data.ProgressKeys["Atlantica"][data.WorldsData["Atlantica"].progress];
            //data.WorldsData["Atlantica"].progression.SetResourceReference(ContentProperty, ATkey);
            //
            //string CoRkey = Prog + data.ProgressKeys["GoA"][data.WorldsData["GoA"].progress];
            //data.WorldsData["GoA"].progression.SetResourceReference(ContentProperty, CoRkey);
        }

        private void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Path.GetExtension(files[0]).ToUpper() == ".HINT")
                    LoadHints(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                    ParseSeed(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".ZIP")
                    OpenKHSeed(files[0]);
            }
        }

        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".hint",
                Filter = "Jsmartee hint file (*.hint)|*.hint",
                Title = "Select Hint File"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                LoadHints(openFileDialog.FileName);
            }
        }

        public void LoadHints(string filename)
        {
            OnReset(null, null);
            SetMode(Mode.Hints);
            //ResetHints();

            StreamReader streamReader = new StreamReader(filename);

            if (streamReader.EndOfStream)
            {
                SetHintText("Error loading hints");
                streamReader.Close();
                return;
            }

            string line1 = streamReader.ReadLine();
            data.hintFileText[0] = line1;
            string[] reportvalues = line1.Split('.');

            if (streamReader.EndOfStream)
            {
                SetHintText("Error loading hints");
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
                data.reportInformation.Add(new Tuple<string, string, int>(null, data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
            }

            data.hintsLoaded = true;
            //HintText.Text = "Hints Loaded";
        }

        private void ResetHints()
        {
            data.hintsLoaded = false;
            data.reportLocations.Clear();
            data.reportInformation.Clear();
            data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
                data.WorldsData[key].hintedHint = false;
                data.WorldsData[key].containsGhost = false;
            }
            data.WorldsData["GoA"].hinted = true;

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }
            
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                if (worldData.value != null)
                    SetWorldValue(worldData.value, -999999);
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }
        }

        ///TODO: update with new settings
        private void LoadSettings(string settings) { }
        //{
        //    string[] settinglist = settings.Split('-');
        //
        //    foreach (string setting in settinglist)
        //    {
        //        string trimmed = setting.Trim();
        //        switch (trimmed)
        //        {
        //            case "Promise Charm":
        //                PromiseCharmToggle(true);
        //                break;
        //            case "Secret Ansem Reports":
        //                ReportsToggle(true);
        //                break;
        //            case "Second Chance & Once More":
        //                AbilitiesToggle(true);
        //                break;
        //            //case "Torn Pages":
        //            //    TornPagesToggle(true);
        //            //    break;
        //            //case "Cure":
        //            //    CureToggle(true);
        //            //    break;
        //            //case "Final Form":
        //            //    FinalFormToggle(true);
        //            //    break;
        //            case "Visit Locks":
        //                VisitLockToggle(true);
        //                break;
        //            case "Sora's Heart":
        //                SoraHeartToggle(true);
        //                break;
        //            case "Level01":
        //                SoraLevel01Toggle(true);
        //                break;
        //            case "Level50":
        //                SoraLevel50Toggle(true);
        //                break;
        //            case "Level99":
        //                SoraLevel99Toggle(true);
        //                break;
        //            case "Simulated Twilight Town":
        //                SimulatedToggle(true);
        //                break;
        //            case "100 Acre Wood":
        //                HundredAcreWoodToggle(true);
        //                break;
        //            case "Atlantica":
        //                AtlanticaToggle(true);
        //                break;
        //            //case "Cavern of Remembrance":
        //            //    CavernToggle(true);
        //            //    break;
        //            //case "Olympus Cups":
        //            //    OCCupsToggle(true);
        //            //    break;
        //            //case "Lingering Will (Terra)":
        //            //    TerraToggle(true);
        //            //    break;
        //            case "Extra Checks":
        //                ExtraChecksToggle(true);
        //                break;
        //            case "Puzzles":
        //                PuzzleToggle(true);
        //                break;
        //            case "Synthesis":
        //                SynthToggle(true);
        //                break;
        //        }
        //    }
        //}

        private void OnReset(object sender, RoutedEventArgs e)
        {
            if (aTimer != null)
                aTimer.Stop();

            isWorking = false;
            collectedChecks.Clear();
            newChecks.Clear();
            ModeDisplay.Header = "";
            HintTextMiddle.Text = "";
            HintTextBegin.Text = "";
            HintTextEnd.Text = "";
            data.mode = Mode.None;
            collected = 0;
            PointTotal = 0;
            data.SpoilerRevealTypes.Clear();
            data.SpoilerReportMode = false;
            data.SpoilerWorldCompletion = false;
            data.usedPages = 0;
            CollectedValue.Text = "0";
            data.ScoreMode = false;
            data.forcedFinal = false;

            //unselect any currently selected world grid
            if (data.selected != null)
            {
                foreach (var Box in data.WorldsData[data.selected.Name].top.Children.OfType<Rectangle>())
                {
                    if (Box.Opacity != 0.9 && !Box.Name.EndsWith("SelWG"))
                        Box.Fill = (SolidColorBrush)FindResource("DefaultRec");

                    if (Box.Name.EndsWith("SelWG"))
                        Box.Visibility = Visibility.Collapsed;
                }
            }
            data.selected = null;

            //return items to itempool
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                for (int j = worldData.worldGrid.Children.Count - 1; j >= 0; --j)
                {
                    Item item = worldData.worldGrid.Children[j] as Item;
                    Grid pool;

                    if(item.Name.StartsWith("Ghost_"))
                        pool = VisualTreeHelper.GetChild(ItemPool, 4) as Grid;
                    else
                        pool = data.Items[item.Name].Item2;
            
                    worldData.worldGrid.Children.Remove(worldData.worldGrid.Children[j]);
                    pool.Children.Add(item);
            
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
            RowDefinitionCollection rows1 = ((data.WorldsData["SorasHeart"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows1)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }
            
            // Reset 2nd column row heights
            RowDefinitionCollection rows2 = ((data.WorldsData["DriveForms"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows2)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].complete = false;
                data.WorldsData[key].checkCount.Clear();
                data.WorldsData[key].progress = 0;

                //world cross reset
                string crossname = key + "Cross";

                if (data.WorldsData[key].top.FindName(crossname) is Image Cross)
                {
                    Cross.Visibility = Visibility.Collapsed;
                }
            }

            TwilightTownProgression.SetResourceReference(ContentProperty, "");
            HollowBastionProgression.SetResourceReference(ContentProperty, "");
            LandofDragonsProgression.SetResourceReference(ContentProperty, "");
            BeastsCastleProgression.SetResourceReference(ContentProperty, "");
            OlympusColiseumProgression.SetResourceReference(ContentProperty, "");
            SpaceParanoidsProgression.SetResourceReference(ContentProperty, "");
            HalloweenTownProgression.SetResourceReference(ContentProperty, "");
            PortRoyalProgression.SetResourceReference(ContentProperty, "");
            AgrabahProgression.SetResourceReference(ContentProperty, "");
            PrideLandsProgression.SetResourceReference(ContentProperty, "");
            DisneyCastleProgression.SetResourceReference(ContentProperty, "");
            HundredAcreWoodProgression.SetResourceReference(ContentProperty, "");
            SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "");
            TWTNWProgression.SetResourceReference(ContentProperty, "");
            AtlanticaProgression.SetResourceReference(ContentProperty, "");
            GoAProgression.SetResourceReference(ContentProperty, "");
            DriveFormsCap.SetResourceReference(ContentProperty, "");
            ChestIcon.SetResourceReference(ContentProperty, "Chest");

            Level.Visibility = Visibility.Collapsed;
            Strength.Visibility = Visibility.Collapsed;
            Magic.Visibility = Visibility.Collapsed;
            Defense.Visibility = Visibility.Collapsed;
            Connect.Visibility = AutoDetectOption.IsChecked ? Visibility.Visible : Visibility.Hidden;
            SorasHeartWeapon.SetResourceReference(ContentProperty, "");

            FormRow.Height = new GridLength(0, GridUnitType.Star);
            NextLevelCol.Width = new GridLength(0, GridUnitType.Star);

            ValorM.Opacity = .45;
            WisdomM.Opacity = .45;
            LimitM.Opacity = .45;
            MasterM.Opacity = .45;
            FinalM.Opacity = .45;
            HighJump.Opacity = .45;
            QuickRun.Opacity = .45;
            DodgeRoll.Opacity = .45;
            AerialDodge.Opacity = .45;
            Glide.Opacity = .45;

            ValorLevel.Text = "1";
            WisdomLevel.Text = "1";
            LimitLevel.Text = "1";
            MasterLevel.Text = "1";
            FinalLevel.Text = "1";
            HighJumpLevel.Text = "";
            QuickRunLevel.Text = "";
            DodgeRollLevel.Text = "";
            AerialDodgeLevel.Text = "";
            GlideLevel.Text = "";

            fireLevel = 0;
            blizzardLevel = 0;
            thunderLevel = 0;
            cureLevel = 0;
            reflectLevel = 0;
            magnetLevel = 0;
            tornPageCount = 0;

            if (fire != null)
                fire.Level = 0;
            if (blizzard != null)
                blizzard.Level = 0;
            if (thunder != null)
                thunder.Level = 0;
            if (cure != null)
                cure.Level = 0;
            if (reflect != null)
                reflect.Level = 0;
            if (magnet != null)
                magnet.Level = 0;
            if (pages != null)
                pages.Quantity = 0;

            if (highJump != null)
                highJump.Level = 0;
            if (quickRun != null)
                quickRun.Level = 0;
            if (dodgeRoll != null)
                dodgeRoll.Level = 0;
            if (aerialDodge != null)
                aerialDodge.Level = 0;
            if (glide != null)
                glide.Level = 0;

            //hide & reset seed hash
            if (data.ShouldResetHash)
            {
                HashGrid.Visibility = Visibility.Collapsed;
                data.SeedHashLoaded = false;
                //data.SeedHashVisible = false;
            }

            foreach (string value in data.PointsDatanew.Keys.ToList())
            {
                data.PointsDatanew[value] = 0;
            }

            foreach (string world in WorldPoints.Keys.ToList())
            {
                WorldPoints[world] = 0;
                WorldPoints_c[world] = 0;
            }

            WorldGrid.Real_Fire = 0;
            WorldGrid.Real_Blizzard = 0;
            WorldGrid.Real_Thunder = 0;
            WorldGrid.Real_Cure = 0;
            WorldGrid.Real_Reflect = 0;
            WorldGrid.Real_Magnet = 0;
            WorldGrid.Real_Pages = 0;
            WorldGrid.Real_Pouches = 0;
            FireCount.Text = "3";
            BlizzardCount.Text = "3";
            ThunderCount.Text = "3";
            CureCount.Text = "3";
            ReflectCount.Text = "3";
            MagnetCount.Text = "3";
            PageCount.Text = "5";
            MunnyCount.Text = "2";

            WorldGrid.Ghost_Fire = 0;
            WorldGrid.Ghost_Blizzard = 0;
            WorldGrid.Ghost_Thunder = 0;
            WorldGrid.Ghost_Cure = 0;
            WorldGrid.Ghost_Reflect = 0;
            WorldGrid.Ghost_Magnet = 0;
            WorldGrid.Ghost_Pages = 0;
            WorldGrid.Ghost_Pouches = 0;
            WorldGrid.Ghost_Fire_obtained = 0;
            WorldGrid.Ghost_Blizzard_obtained = 0;
            WorldGrid.Ghost_Thunder_obtained = 0;
            WorldGrid.Ghost_Cure_obtained = 0;
            WorldGrid.Ghost_Reflect_obtained = 0;
            WorldGrid.Ghost_Magnet_obtained = 0;
            WorldGrid.Ghost_Pages_obtained = 0;
            WorldGrid.Ghost_Pouches_obtained = 0;

            Data.WorldItems.Clear();
            data.TrackedReports.Clear();

            CollectionGrid.Visibility = Visibility.Visible;
            ScoreGrid.Visibility = Visibility.Hidden;

            //reset settings row
            SettingsText.Text = "";
            Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
            Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
            Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
            Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
            Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
            Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
            Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
            Setting_Spacer.Width = new GridLength(10, GridUnitType.Star);
            
            if (AutoDetectOption.IsChecked)
            {
                SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
            }
            else
                SettingRow.Height = new GridLength(0, GridUnitType.Star);

            //reset pathhints edits
            foreach (string key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
            
                Grid pathgrid = data.WorldsData[key].top.FindName(key + "Path") as Grid;
                pathgrid.Visibility = Visibility.Hidden;
                foreach (Image child in pathgrid.Children)
                {
                    if (child.Name.Contains(key + "Path_Non") && child.Source.ToString().Contains("cross.png")) //reset non icon to default image
                        child.Source = new BitmapImage(new Uri("Images/Checks/Simple/proof_of_nonexistence.png", UriKind.Relative));
                    child.Visibility = Visibility.Hidden;
                }
            }

            UpdatePointScore(0);
            ReportsToggle(true);
            ResetHints();
            VisitLockToggle(VisitLockOption.IsChecked);

            DeathCounter = 0;
            DeathValue.Text = "0";
            
            //DeathCounterGrid.Visibility = Visibility.Collapsed;
            DeathCol.Width = new GridLength(0, GridUnitType.Star);

            foreach (Grid itempool in ItemPool.Children)
            {
                foreach (var item in itempool.Children)
                {
                    ContentControl check = item as ContentControl;

                    if (check != null && !check.Name.Contains("Ghost"))
                        check.Opacity = 1.0;
                }
            }

            SetAutoDetectTimer();
            NextLevelDisplay();
            //DeathCounterDisplay();
        }

        private void ParseSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".pnach",
                Filter = "pnach files (*.pnach)|*.pnach",
                Title = "Select Seed File"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ParseSeed(openFileDialog.FileName);
            }
        }

        public void ParseSeed(string filename)
        {
            bool autotrackeron = false;
            bool ps2tracking = false;
            //check for autotracking on and which version
            if (aTimer != null)
                autotrackeron = true;

            if (pcsx2tracking)
                ps2tracking = true;

            //FixDictionary();
            SetMode(Mode.AltHints);

            foreach (string world in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[world].checkCount.Clear();
            }

            StreamReader streamReader = new StreamReader(filename);
            bool check1 = false;
            bool check2 = false;

            while (streamReader.EndOfStream == false)
            {
                string line = streamReader.ReadLine();

                // ignore comment lines
                if (line.Length >= 2 && line[0] == '/' && line[1] == '/')
                    continue;

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
                    data.WorldsData[world].checkCount.Add(item);
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
                foreach (string world in data.WorldsData.Keys.ToList())
                {
                    data.WorldsData[world].checkCount.Clear();
                }
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            if (autotrackeron)
            {
                InitAutoTracker(ps2tracking);
            }
        }

        private void SetMode(Mode mode)
        {
            //if ((data.mode != mode && data.mode != Mode.None) || mode == Mode.AltHints || mode == Mode.OpenKHAltHints || mode == Mode.DAHints || mode == Mode.PathHints || mode == Mode.SpoilerHints)
            //{
            //    OnReset(null, null);
            //}

            if (mode == Mode.AltHints || mode == Mode.OpenKHAltHints)
            {
                ModeDisplay.Header = "Alt Hints Mode";
                data.mode = mode;
                ReportsToggle(false);
            }
            else if (mode == Mode.Hints || mode == Mode.OpenKHHints)
            {
                ModeDisplay.Header = "Hints Mode";
                data.mode = mode;
                ReportsToggle(true);
            }
            else if (mode == Mode.DAHints)
            {
                ModeDisplay.Header = "Points Mode";
                data.mode = mode;
                ReportsToggle(true);

                UpdatePointScore(0);
                ShowCheckCountToggle(null, null);

                //ChestIcon.SetResourceReference(ContentProperty, "Score");
            }
            else if (mode == Mode.PathHints)
            {
                ModeDisplay.Header = "Path Hints";
                data.mode = mode;
                ReportsToggle(true);
            }
            else if (mode == Mode.SpoilerHints)
            {
                ModeDisplay.Header = "Spoiler Hints";
                data.mode = mode;
            }

            if (data.ScoreMode)
            {
                UpdatePointScore(0);
                ShowCheckCountToggle(null, null);

                ModeDisplay.Header += " | Hi-Score Mode";
                //ChestIcon.SetResourceReference(ContentProperty, "Score");
            }
        }

        private void OpenKHSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".zip",
                Filter = "OpenKH Seeds (*.zip)|*.zip",
                Title = "Select Seed File"
            };
            if (openFileDialog.ShowDialog() == true)
                OpenKHSeed(openFileDialog.FileName);
        }

        private void OpenKHSeed(string filename)
        {
            OnReset(null, null);

            foreach (string world in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[world].checkCount.Clear();
            }

            using (ZipArchive archive = ZipFile.OpenRead(filename))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".Hints"))
                    {
                        using (StreamReader reader = new StreamReader(entry.Open()))
                        {
                            data.openKHHintText = reader.ReadToEnd();
                            var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                            var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                            var settings = new List<string>();

                            data.ShouldResetHash = false;

                            if (hintObject.ContainsKey("settings"))
                            {
                                settings = JsonSerializer.Deserialize<List<string>>(hintObject["settings"].ToString());

                                #region Settings

                                //item settings
                                PromiseCharmToggle(false);
                                AbilitiesToggle(false);
                                VisitLockToggle(false);
                                ExtraChecksToggle(false);
                                AntiFormToggle(false);

                                //world settings
                                SoraHeartToggle(true);
                                DrivesToggle(false);
                                SimulatedToggle(false);
                                TwilightTownToggle(false);
                                HollowBastionToggle(false);
                                BeastCastleToggle(false);
                                OlympusToggle(false);
                                AgrabahToggle(false);
                                LandofDragonsToggle(false);
                                DisneyCastleToggle(false);
                                PrideLandsToggle(false);
                                PortRoyalToggle(false);
                                HalloweenTownToggle(false);
                                SpaceParanoidsToggle(false);
                                TWTNWToggle(false);
                                HundredAcreWoodToggle(false);
                                AtlanticaToggle(false);
                                PuzzleToggle(false);
                                SynthToggle(false);

                                //settings visuals
                                SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
                                Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
                                Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
                                Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
                                Double SpacerValue = 10;
                                #endregion

                                //to be safe about this i guess
                                bool abilitiesOn = true;

                                //load settings from hints
                                foreach (string setting in settings)
                                {
                                    Console.WriteLine("setting found = " + setting);

                                    switch (setting)
                                    {
                                        //items
                                        case "PromiseCharm":
                                            PromiseCharmToggle(true);
                                            break;
                                        case "Level1Mode":
                                            abilitiesOn = false;
                                            break;
                                        case "visit_locking":
                                            VisitLockToggle(true);
                                            break;
                                        case "extra_ics":
                                            ExtraChecksToggle(true);
                                            break;
                                        case "Anti-Form":
                                            AntiFormToggle(true);
                                            break;
                                        //worlds
                                        case "Level":
                                            SoraHeartToggle(false);
                                            SoraLevel01Toggle(true);
                                            AbilitiesToggle(true);
                                            Setting_Level_01.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "ExcludeFrom50":                                          
                                            SoraLevel50Toggle(true);
                                            AbilitiesToggle(true);
                                            Setting_Level_50.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "ExcludeFrom99":
                                            SoraLevel99Toggle(true);
                                            AbilitiesToggle(true);
                                            Setting_Level_99.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Simulated Twilight Town":
                                            SimulatedToggle(true);
                                            break;
                                        case "Hundred Acre Wood":
                                            HundredAcreWoodToggle(true);
                                            break;
                                        case "Atlantica":
                                            AtlanticaToggle(true);
                                            break;
                                        case "Puzzle":
                                            PuzzleToggle(true);
                                            break;
                                        case "Synthesis":
                                            SynthToggle(true);
                                            break;
                                        case "Form Levels":
                                            DrivesToggle(true);
                                            break;
                                        case "Land of Dragons":
                                            LandofDragonsToggle(true);
                                            break;
                                        case "Beast's Castle":
                                            BeastCastleToggle(true);
                                            break;
                                        case "Hollow Bastion":
                                            HollowBastionToggle(true);
                                            break;
                                        case "Twilight Town":
                                            TwilightTownToggle(true);
                                            break;
                                        case "The World That Never Was":
                                            TWTNWToggle(true);
                                            break;
                                        case "Space Paranoids":
                                            SpaceParanoidsToggle(true);
                                            break;
                                        case "Port Royal":
                                            PortRoyalToggle(true);
                                            break;
                                        case "Olympus Coliseum":
                                            OlympusToggle(true);
                                            break;
                                        case "Agrabah":
                                            AgrabahToggle(true);
                                            break;
                                        case "Halloween Town":
                                            HalloweenTownToggle(true);
                                            break;
                                        case "Pride Lands":
                                            PrideLandsToggle(true);
                                            break;
                                        case "Disney Castle / Timeless River":
                                            DisneyCastleToggle(true);
                                            break;
                                        //settings
                                        case "better_stt":
                                            Setting_BetterSTT.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Cavern of Remembrance":
                                            Setting_Cavern.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Absent Silhouettes":
                                            Setting_Absent.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Sephiroth":
                                            Setting_Sephiroth.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Lingering Will (Terra)":
                                            Setting_Terra.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Data Organization XIII":
                                            Setting_Datas.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Transport to Remembrance":
                                            Setting_Transport.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Olympus Cups":
                                            Setting_Cups.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "Hades Paradox Cup":
                                            Setting_HadesCup.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                            break;
                                        case "ScoreMode":
                                            data.ScoreMode = true;
                                            break;
                                    }
                                }

                                if(abilitiesOn == false)
                                    AbilitiesToggle(false);

                                Setting_Spacer.Width = new GridLength(SpacerValue, GridUnitType.Star);
                                SettingsText.Text = "Settings:";
                                
                            }

                            switch (hintObject["hintsType"].ToString())
                            {
                                case "Shananas":
                                    {
                                        SetMode(Mode.OpenKHAltHints);
                                        ShanHints(hintObject);
                                    }
                                    break;
                                case "JSmartee":
                                    {
                                        SetMode(Mode.OpenKHHints);
                                        JsmarteeHints(hintObject);
                                    }
                                    break;
                                case "Points":
                                    {
                                        SetMode(Mode.DAHints);
                                        PointsHints(hintObject);
                                    }
                                    break;
                                case "Path":
                                    {
                                        SetMode(Mode.PathHints);
                                        PathHints(hintObject);
                                    }
                                    break;
                                case "Spoiler":
                                    {
                                        SetMode(Mode.SpoilerHints);
                                        SpoilerHints(hintObject);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            reader.Close();
                        }
                    }

                    if (entry.FullName.Equals("sys.yml"))
                    {
                        using (var reader2 = new StreamReader(entry.Open()))
                        {
                            string[] separatingStrings = { "- en: ", " ", "'", "{", "}", ":", "icon" };
                            string text1 = reader2.ReadLine();
                            string text2 = reader2.ReadLine();
                            string text = text1 + text2;
                            string[] hash = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                            //Set Icons
                            HashIcon1.SetResourceReference(ContentProperty, hash[0]);
                            HashIcon2.SetResourceReference(ContentProperty, hash[1]);
                            HashIcon3.SetResourceReference(ContentProperty, hash[2]);
                            HashIcon4.SetResourceReference(ContentProperty, hash[3]);
                            HashIcon5.SetResourceReference(ContentProperty, hash[4]);
                            HashIcon6.SetResourceReference(ContentProperty, hash[5]);
                            HashIcon7.SetResourceReference(ContentProperty, hash[6]);
                            data.SeedHashLoaded = true;

                            //make visible
                            if (SeedHashOption.IsChecked)
                            {
                                SetHintText("");
                                HashGrid.Visibility = Visibility.Visible;
                            }
                            reader2.Close();
                        }
                    }

                    if (entry.FullName.Equals("enemyspoilers.txt"))
                    {
                        //only care about parsing this with points hints or score mode
                        if (data.mode != Mode.DAHints && !data.ScoreMode)
                            return;

                        using (var reader3 = new StreamReader(entry.Open()))
                        {
                            string firstLine = reader3.ReadLine();
                            if (firstLine != "BOSSES")
                            {
                                Console.WriteLine("No Bosses Present? Expected \"BOSSES\" but got " + firstLine);
                                reader3.Close();
                                return;
                            }

                            //we found bosses, set bool to alter how boss points are awarded
                            data.BossRandoFound = true;

                            string[] separatingString = { " became " };
                            int lineNumber = 85;
                            for (int i = 1; i < lineNumber; i++)
                            {
                                string curLine = reader3.ReadLine().Trim();
                                string[] bosses = curLine.Split(separatingString, System.StringSplitOptions.RemoveEmptyEntries);

                                data.BossList.Add(bosses[0], bosses[1]);

                                //check boss name. We want to stop at data zexion as he is always at the end.
                                switch (bosses[0])
                                {
                                    case "Zexion (Data)":
                                        lineNumber = i;
                                        Console.WriteLine("Boss: " + bosses[0] + " | Replacement: " + bosses[1]);
                                        break;
                                    default:
                                        Console.WriteLine("Boss: " + bosses[0] + " | Replacement: " + bosses[1]);
                                        break;
                                }
                            }
                        }
                    }
                }
                archive.Dispose();
            }
        }
    }
}
