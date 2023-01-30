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

        public void Save(string filename)
        {
            string mode = "Mode: " + data.mode.ToString();

            #region Settings
            string settings = "Settings: ";
            if (ReportsOption.IsChecked)
                settings += "Secret Ansem Reports - ";
            if (PromiseCharmOption.IsChecked)
                settings += "Promise Charm - ";
            if (AbilitiesOption.IsChecked)
                settings += "Second Chance & Once More - ";
            if (AntiFormOption.IsChecked)
                settings += "AntiForm - ";
            if (VisitLockOption.IsChecked)
                settings += "Visit Lock - ";
            if (ExtraChecksOption.IsChecked)
                settings += "Extra Checks - ";
            if (SoraLevel01Option.IsChecked)
                settings += "Level01 - ";
            if (SoraLevel50Option.IsChecked)
                settings += "Level50 - ";
            if (SoraLevel99Option.IsChecked)
                settings += "Level99 - ";
            if (SoraHeartOption.IsChecked)
                settings += "Sora's Heart - ";
            if (DrivesOption.IsChecked)
                settings += "Drive Forms - ";
            if (SimulatedOption.IsChecked)
                settings += "Simulated Twilight Town - ";
            if (TwilightTownOption.IsChecked)
                settings += "Twilight Town - ";
            if (HollowBastionOption.IsChecked)
                settings += "Hollow Bastion - ";
            if (BeastCastleOption.IsChecked)
                settings += "Beast Castle - ";
            if (OlympusOption.IsChecked)
                settings += "Olympus - ";
            if (AgrabahOption.IsChecked)
                settings += "Agrabah - ";
            if (LandofDragonsOption.IsChecked)
                settings += "Land of Dragons - ";
            if (DisneyCastleOption.IsChecked)
                settings += "Disney Castle - ";
            if (PrideLandsOption.IsChecked)
                settings += "Pride Lands - ";
            if (PortRoyalOption.IsChecked)
                settings += "Port Royal - ";
            if (HalloweenTownOption.IsChecked)
                settings += "Halloween Town - ";
            if (SpaceParanoidsOption.IsChecked)
                settings += "Space Paranoids - ";
            if (TWTNWOption.IsChecked)
                settings += "TWTNW - ";
            if (HundredAcreWoodOption.IsChecked)
                settings += "100 Acre Wood - ";
            if (AtlanticaOption.IsChecked)
                settings += "Atlantica - ";
            if (PuzzleOption.IsChecked)
                settings += "Puzzles - ";
            if (SynthOption.IsChecked)
                settings += "Synthesis - ";
            if (data.ScoreMode)
                settings += "Score Mode - ";
            if (data.BossRandoFound)
                settings += "Boss Rando - ";
            if (TornPagesOption.IsChecked)
                settings += "Torn Pages - ";
            #endregion

            // save hint state (hint info, hints, track attempts)
            string attempts = "";
            string hintValues = "";
            if (data.mode != Mode.AltHints && data.mode != Mode.OpenKHAltHints && data.mode != Mode.None)
            {
                attempts = "Attempts: ";
                if (data.hintsLoaded)
                {
                    foreach (int num in data.reportAttempts)
                    {
                        attempts += " - " + num.ToString();
                    }
                }
        
                // store hint values
                hintValues = "HintValues: ";
                foreach (WorldData worldData in data.WorldsData.Values.ToList())
                {
                    if (worldData.value == null)
                        continue;
        
                    int num = -999999;
                    if (worldData.value.Text != "?")
                        num = int.Parse(worldData.value.Text);

                    //need to recaculate correct values if ghost items and automath are toggled
                    if (worldData.containsGhost && GhostMathOption.IsChecked) 
                    {
                        num += GetGhostPoints(worldData.worldGrid);
                    }

                    hintValues += num.ToString() + " ";
                }
            }
        
            FileStream file = File.Create(filename);
            StreamWriter writer = new StreamWriter(file);
     
            writer.WriteLine(mode);
            writer.WriteLine(settings);

            if (data.BossRandoFound)
            {
                string bossText = JsonSerializer.Serialize(data.BossList);
                bossText = Convert.ToBase64String(Encoding.UTF8.GetBytes(bossText));
                writer.WriteLine(bossText);
            }

            if (data.mode == Mode.OpenKHHints || data.mode == Mode.PathHints || data.mode == Mode.SpoilerHints || data.mode == Mode.Hints)
            {
                writer.WriteLine(attempts);

                if (data.mode == Mode.Hints)
                {
                    writer.WriteLine(data.hintFileText[0]);
                    writer.WriteLine(data.hintFileText[1]);
                }
                else
                {
                    writer.WriteLine(data.openKHHintText);
                }
                
                writer.WriteLine(hintValues);
            }
            else if (data.mode == Mode.AltHints)
            {
                Dictionary<string, List<string>> test = new Dictionary<string, List<string>>();
                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    test.Add(key, data.WorldsData[key].checkCount);
                }
                string hintObject = JsonSerializer.Serialize(test);
                string hintText = Convert.ToBase64String(Encoding.UTF8.GetBytes(hintObject));
                writer.WriteLine(hintText);
            }
            else if (data.mode == Mode.OpenKHAltHints)
            {
                writer.WriteLine(data.openKHHintText);
            }
            else if (data.mode == Mode.DAHints)
            {
                writer.WriteLine(attempts);
                writer.WriteLine(data.openKHHintText);

                string worlditemlist = JsonSerializer.Serialize(Data.WorldItems);
                string worlditemlist64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(worlditemlist));
                writer.WriteLine(worlditemlist64);

                string reportlist = JsonSerializer.Serialize(data.TrackedReports);
                string reportlist64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(reportlist));
                writer.WriteLine(reportlist64);

                writer.WriteLine(hintValues);
            }

            string ProgressString = "Progress:";
            foreach (string WorldName in data.WorldsData.Keys.ToList())
            {
                if (WorldName != "SorasHeart" && WorldName != "DriveForms" && WorldName != "PuzzSynth")
                    ProgressString += " " + data.WorldsData[WorldName].progress.ToString();
            }
            writer.WriteLine(ProgressString);
        
            foreach (string WorldName in data.WorldsData.Keys.ToList())
            {
                string ItemString = WorldName + ":";
                foreach (Item item in data.WorldsData[WorldName].worldGrid.Children)
                {
                    ItemString += " " + item.Name;
                }
                writer.WriteLine(ItemString);
            }

            writer.WriteLine(data.usedPages);
            writer.WriteLine(DeathCounter);

            settings = "SettingsBar: ";
            if (SettingRow.Height.Value != 0)
            {
                if (Setting_BetterSTT.Width.Value != 0)
                {
                    settings += "Setting_BetterSTT - ";
                }
                if (Setting_Level_01.Width.Value != 0)
                {
                    settings += "Setting_Level_01 - ";
                }         
                if (Setting_Level_50.Width.Value != 0)
                {
                    settings += "Setting_Level_50 - ";
                }
                if (Setting_Level_99.Width.Value != 0)
                {
                    settings += "Setting_Level_99 - ";
                }
                if (Setting_Absent.Width.Value != 0)
                {
                    settings += "Setting_Absent - ";
                }
                if (Setting_Absent_Split.Width.Value != 0)
                {
                    settings += "Setting_Absent_Split - ";
                }
                if (Setting_Datas.Width.Value != 0)
                {
                    settings += "Setting_Datas - ";
                }
                if (Setting_Sephiroth.Width.Value != 0)
                {
                    settings += "Setting_Sephiroth - ";
                }
                if (Setting_Terra.Width.Value != 0)
                {
                    settings += "Setting_Terra - ";
                }
                if (Setting_Cups.Width.Value != 0)
                {
                    settings += "Setting_Cups - ";
                }
                if (Setting_HadesCup.Width.Value != 0)
                {
                    settings += "Setting_HadesCup - ";
                }
                if (Setting_Cavern.Width.Value != 0)
                {
                    settings += "Setting_Cavern - ";
                }
                if (Setting_Transport.Width.Value != 0)
                {
                    settings += "Setting_Transport - ";
                }
            }
            else
            {
                settings += "None - ";
            }
            writer.WriteLine(settings);

            writer.Close();
        }

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

        private void Load(string filename)
        {
            // reset tracker
            OnReset(null, null);

            Stream file = File.Open(filename, FileMode.Open);
            StreamReader reader = new StreamReader(file);

            // set settings
            string mode = reader.ReadLine().Substring(6);
            LoadSettings(reader.ReadLine().Substring(10)); //load setting first, then mode
            if (mode == "Hints")
                SetMode(Mode.Hints);
            else if (mode == "AltHints")
                SetMode(Mode.AltHints);
            else if (mode == "OpenKHHints")
                SetMode(Mode.OpenKHHints);
            else if (mode == "OpenKHAltHints")
                SetMode(Mode.OpenKHAltHints);
            else if (mode == "DAHints")
                SetMode(Mode.DAHints);
            else if (mode == "PathHints")
                SetMode(Mode.PathHints);
            else if (mode == "SpoilerHints")
                SetMode(Mode.SpoilerHints);

            if (data.BossRandoFound)
            {
                string tempList = reader.ReadLine();
                var bossText = Encoding.UTF8.GetString(Convert.FromBase64String(tempList));
                data.BossList = JsonSerializer.Deserialize<Dictionary<string, string>>(bossText);
            }

            // set hint state
            if (data.mode != Mode.None && data.mode != Mode.AltHints)
            {
                //report info
                if (data.mode != Mode.OpenKHAltHints)
                {
                    string attempts = reader.ReadLine();
                    if (attempts.Length > 13)
                    {
                        attempts = attempts.Substring(13);
                        string[] attemptsArray = attempts.Split('-');
                        for (int i = 0; i < attemptsArray.Length; ++i)
                        {
                            data.reportAttempts[i] = int.Parse(attemptsArray[i]);
                        }
                    }
                }

                //openkh .hints data
                if (data.mode != Mode.Hints)
                {
                    data.openKHHintText = reader.ReadLine();
                    var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                    var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);

                    switch (data.mode)
                    {
                        case Mode.OpenKHHints:
                            JsmarteeHints(hintObject);
                            break;
                        case Mode.OpenKHAltHints:
                            ShanHints(hintObject);
                            break;
                        case Mode.PathHints:
                            PathHints(hintObject);
                            break;
                        case Mode.SpoilerHints:
                            SpoilerHints(hintObject);
                            break;
                        case Mode.DAHints:
                            PointsHints(hintObject);

                            var witemlist64 = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
                            var witemlist = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(witemlist64);
                            Data.WorldItems = witemlist;

                            var reportlist64 = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
                            var reportlist = JsonSerializer.Deserialize<List<string>>(reportlist64);
                            data.TrackedReports = reportlist;
                            break;
                    }
                }
                //pnach jmartee hints
                else
                {
                    data.hintFileText[0] = reader.ReadLine();
                    string[] reportvalues = data.hintFileText[0].Split('.');

                    data.hintFileText[1] = reader.ReadLine();
                    string line2 = data.hintFileText[1].TrimEnd('.');
                    string[] reportorder = line2.Split('.');

                    for (int i = 0; i < reportorder.Length; ++i)
                    {
                        data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
                        string[] temp = reportvalues[i].Split(',');
                        data.reportInformation.Add(new Tuple<string, string, int>(null, data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                    }
                    data.hintsLoaded = true;
                }
            }
            //shan hints (pnach)
            else if (data.mode == Mode.AltHints)
            {
                var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
                var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintText);
        
                foreach (var world in worlds)
                {
                    if (world.Key == "GoA")
                    {
                        continue;
                    }
                    foreach (var item in world.Value)
                    {
                        data.WorldsData[world.Key].checkCount.Add(item);
                    }
                }
                foreach (var key in data.WorldsData.Keys.ToList())
                {
                    if (key == "GoA")
                        continue;
        
                    data.WorldsData[key].worldGrid.WorldComplete();
                    SetWorldValue(data.WorldsData[key].value, 0);
                }
            }

            // set hint values (DUMB)
            if (data.hintsLoaded)
            {
                string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
                SetWorldValue(data.WorldsData["SorasHeart"].value, int.Parse(hintValues[0]));
                SetWorldValue(data.WorldsData["DriveForms"].value, int.Parse(hintValues[1]));
                SetWorldValue(data.WorldsData["SimulatedTwilightTown"].value, int.Parse(hintValues[2]));
                SetWorldValue(data.WorldsData["TwilightTown"].value, int.Parse(hintValues[3]));
                SetWorldValue(data.WorldsData["HollowBastion"].value, int.Parse(hintValues[4]));
                SetWorldValue(data.WorldsData["BeastsCastle"].value, int.Parse(hintValues[5]));
                SetWorldValue(data.WorldsData["OlympusColiseum"].value, int.Parse(hintValues[6]));
                SetWorldValue(data.WorldsData["Agrabah"].value, int.Parse(hintValues[7]));
                SetWorldValue(data.WorldsData["LandofDragons"].value, int.Parse(hintValues[8]));
                SetWorldValue(data.WorldsData["HundredAcreWood"].value, int.Parse(hintValues[9]));
                SetWorldValue(data.WorldsData["PrideLands"].value, int.Parse(hintValues[10]));
                SetWorldValue(data.WorldsData["DisneyCastle"].value, int.Parse(hintValues[11]));
                SetWorldValue(data.WorldsData["HalloweenTown"].value, int.Parse(hintValues[12]));
                SetWorldValue(data.WorldsData["PortRoyal"].value, int.Parse(hintValues[13]));
                SetWorldValue(data.WorldsData["SpaceParanoids"].value, int.Parse(hintValues[14]));
                SetWorldValue(data.WorldsData["TWTNW"].value, int.Parse(hintValues[15]));
                SetWorldValue(data.WorldsData["Atlantica"].value, int.Parse(hintValues[16]));
                SetWorldValue(data.WorldsData["PuzzSynth"].value, int.Parse(hintValues[17]));
            }
            else if (mode == "SpoilerHints") //we need to do this for spoiler hints because of the optional report mode
                reader.ReadLine();
        
            string[] progress = reader.ReadLine().Substring(10).Split(' ');
            data.WorldsData["SimulatedTwilightTown"].progress = int.Parse(progress[0]);
            data.WorldsData["TwilightTown"].progress = int.Parse(progress[1]);
            data.WorldsData["HollowBastion"].progress = int.Parse(progress[2]);
            data.WorldsData["BeastsCastle"].progress = int.Parse(progress[3]);
            data.WorldsData["OlympusColiseum"].progress = int.Parse(progress[4]);
            data.WorldsData["Agrabah"].progress = int.Parse(progress[5]);
            data.WorldsData["LandofDragons"].progress = int.Parse(progress[6]);
            data.WorldsData["HundredAcreWood"].progress = int.Parse(progress[7]);
            data.WorldsData["PrideLands"].progress = int.Parse(progress[8]);
            data.WorldsData["DisneyCastle"].progress = int.Parse(progress[9]);
            data.WorldsData["HalloweenTown"].progress = int.Parse(progress[10]);
            data.WorldsData["PortRoyal"].progress = int.Parse(progress[11]);
            data.WorldsData["SpaceParanoids"].progress = int.Parse(progress[12]);
            data.WorldsData["TWTNW"].progress = int.Parse(progress[13]);
            data.WorldsData["Atlantica"].progress = int.Parse(progress[14]);
            data.WorldsData["GoA"].progress = int.Parse(progress[15]);

            SetProgressIcons();

            // add items to worlds
            for (int i = 0; i < data.WorldsData.Count; ++i)
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

                        if (grid.ReportHandler(importantCheck))
                        {
                            switch (data.mode)
                            {
                                case Mode.DAHints:
                                    if (item.StartsWith("Ghost_"))
                                        grid.Add_Ghost(importantCheck);
                                    else
                                        grid.Add_Item(importantCheck);
                                    break;
                                case Mode.SpoilerHints:
                                    if (!item.StartsWith("Ghost_"))
                                        grid.Add_Item(importantCheck);
                                    break;
                                case Mode.PathHints:
                                default:
                                    grid.Add_Item(importantCheck);
                                    break;
                            }
                        }
                    }
                }
            }

            //set extra data stuff
            string UsedPages = reader.ReadLine();
            string Deaths = reader.ReadLine();
            data.usedPages = int.Parse(UsedPages);
            DeathCounter = int.Parse(Deaths);

            LoadSettingBar(reader.ReadLine().Substring(13));

            reader.Close();
        }

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
                //progression hints per world
                data.WorldsData[key].hintedProgression = false;
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

        private void LoadSettings(string settings)
        {
            //item settings
            ReportsToggle(false);
            PromiseCharmToggle(false);
            AbilitiesToggle(false);
            VisitLockToggle(false);
            ExtraChecksToggle(false);
            AntiFormToggle(false);
            TornPagesToggle(false);

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

            string[] settinglist = settings.Split('-');
        
            foreach (string setting in settinglist)
            {
                string trimmed = setting.Trim();
                switch (trimmed)
                {
                    case "Secret Ansem Reports":
                        ReportsToggle(true);
                        break;
                    case "Promise Charm":
                        PromiseCharmToggle(true);
                        break;
                    case "Second Chance & Once More":
                        AbilitiesToggle(true);
                        break;
                    case "AntiForm":
                        AntiFormToggle(true);
                        break;
                    case "Visit Locks":
                        VisitLockToggle(true);
                        break;
                    case "Extra Checks":
                        ExtraChecksToggle(true);
                        break;
                    case "Level01":
                        SoraLevel01Toggle(true);
                        break;
                    case "Level50":
                        SoraLevel50Toggle(true);
                        break;
                    case "Level99":
                        SoraLevel99Toggle(true);
                        break;
                    case "Sora's Heart":
                        SoraHeartToggle(true);
                        break;
                    case "Drive Forms":
                        DrivesToggle(true);
                        break;
                    case "Simulated Twilight Town":
                        SimulatedToggle(true);
                        data.enabledWorlds.Add("STT");
                        break;
                    case "Twilight Town":
                        TwilightTownToggle(true);
                        data.enabledWorlds.Add("TT");
                        break;
                    case "Hollow Bastion":
                        HollowBastionToggle(true);
                        data.enabledWorlds.Add("HB");
                        break;
                    case "Beast Castle":
                        BeastCastleToggle(true);
                        data.enabledWorlds.Add("BC");
                        break;
                    case "Olympus":
                        OlympusToggle(true);
                        data.enabledWorlds.Add("OC");
                        break;
                    case "Agrabah":
                        AgrabahToggle(true);
                        data.enabledWorlds.Add("AG");
                        break;
                    case "Land of Dragons":
                        LandofDragonsToggle(true);
                        data.enabledWorlds.Add("LoD");
                        break;
                    case "Disney Castle":
                        DisneyCastleToggle(true);
                        data.enabledWorlds.Add("DC");
                        break;
                    case "Pride Lands":
                        PrideLandsToggle(true);
                        data.enabledWorlds.Add("PL");
                        break;
                    case "Port Royal":
                        PortRoyalToggle(true);
                        data.enabledWorlds.Add("PR");
                        break;
                    case "Halloween Town":
                        HalloweenTownToggle(true);
                        data.enabledWorlds.Add("HT");
                        break;
                    case "Space Paranoids":
                        SpaceParanoidsToggle(true);
                        data.enabledWorlds.Add("SP");
                        break;
                    case "TWTNW":
                        TWTNWToggle(true);
                        data.enabledWorlds.Add("TWTNW");
                        break;
                    case "100 Acre Wood":
                        HundredAcreWoodToggle(true);
                        break;
                    case "Atlantica":
                        AtlanticaToggle(true);
                        break;
                    case "Puzzles":
                        PuzzleToggle(true);
                        break;
                    case "Synthesis":
                        SynthToggle(true);
                        break;
                    case "Score Mode":
                        data.ScoreMode = true;
                        break;
                    case "Boss Rando":
                        data.BossRandoFound = true;
                        break;
                    case "Torn Pages":
                        TornPagesToggle(true);
                        break;
                }
            }
        }

        private void LoadSettingBar(string settingbar)
        {
            SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
            Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
            Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
            Setting_Absent_Split.Width = new GridLength(0, GridUnitType.Star);
            Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
            Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
            Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
            Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
            Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
            Double SpacerValue = 10;

            string[] settinglist = settingbar.Split('-');
            foreach (string setting in settinglist)
            {
                string trimmed = setting.Trim();
                switch (trimmed)
                {
                    case "None":
                        SettingRow.Height = new GridLength(0, GridUnitType.Star);
                        Setting_Spacer.Width = new GridLength(SpacerValue, GridUnitType.Star);
                        SettingsText.Text = "";
                        return;
                    case "Setting_Level_01":
                        Setting_Level_01.Width = new GridLength(1.5, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Level_50":
                        Setting_Level_50.Width = new GridLength(1.5, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Level_99":
                        Setting_Level_99.Width = new GridLength(1.5, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_BetterSTT":
                        Setting_BetterSTT.Width = new GridLength(1.1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Cavern":
                        Setting_Cavern.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Absent":
                        Setting_Absent.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Absent_Split":
                        Setting_Absent_Split.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Sephiroth":
                        Setting_Sephiroth.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Terra":
                        Setting_Terra.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Datas":
                        Setting_Datas.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Transport":
                        Setting_Transport.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_Cups":
                        Setting_Cups.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                    case "Setting_HadesCup":
                        Setting_HadesCup.Width = new GridLength(1, GridUnitType.Star);
                        SpacerValue--;
                        break;
                }

                Setting_Spacer.Width = new GridLength(SpacerValue, GridUnitType.Star);
                SettingsText.Text = "Settings:";
            }
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            if (aTimer != null)
                aTimer.Stop();

            //isWorking = false;
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
            data.BossRandoFound = false;
            data.BossList.Clear();
            data.BossRandoSeed = 0;
            data.enabledWorlds.Clear();

            //clear progression hints stuff
            data.reportLocationsUsed = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false };
            data.UsingProgressionHints = false;
            data.ProgressionPoints = 0;
            data.TotalProgressionPoints = 0;
            data.WorldsEnabled = 0;
            data.PrevEventID1 = 0;
            data.PrevEventID3 = 0;
            data.PrevWorld = "";
            data.PrevRoomNum = 0;
            data.ProgressionCurrentHint = 0;
            data.HintRevealOrder.Clear();
            data.LevelsPreviousIndex = 0;
            data.NextLevelMilestone = 9;
            data.DriveLevels = new List<int>() { 1, 1, 1, 1, 1 };
            data.HintRevealsStored.Clear();
            data.WorldsData["GoA"].value.Visibility = Visibility.Hidden;
            data.StoredWorldCompleteBonus = new Dictionary<string, int>()
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
            //Connect.Visibility = AutoDetectOption.IsChecked ? Visibility.Visible : Visibility.Hidden;
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
            WorldGrid.Proof_Count = 0;
            WorldGrid.Form_Count = 0;
            WorldGrid.Summon_Count = 0;
            WorldGrid.Ability_Count = 0;
            WorldGrid.Report_Count = 0;
            WorldGrid.Visit_Count = 0;

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

            Ghost_FireCount.Visibility = Visibility.Hidden;
            Ghost_BlizzardCount.Visibility = Visibility.Hidden;
            Ghost_ThunderCount.Visibility = Visibility.Hidden;
            Ghost_CureCount.Visibility = Visibility.Hidden;
            Ghost_ReflectCount.Visibility = Visibility.Hidden;
            Ghost_MagnetCount.Visibility = Visibility.Hidden;
            Ghost_PageCount.Visibility = Visibility.Hidden;
            Ghost_MunnyCount.Visibility = Visibility.Hidden;

            FireCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            FireCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            FireCount.Fill = (LinearGradientBrush)FindResource("Color_Fire");
            FireCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            BlizzardCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            BlizzardCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            BlizzardCount.Fill = (LinearGradientBrush)FindResource("Color_Blizzard");
            BlizzardCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            ThunderCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            ThunderCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            ThunderCount.Fill = (LinearGradientBrush)FindResource("Color_Thunder");
            ThunderCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            CureCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            CureCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            CureCount.Fill = (LinearGradientBrush)FindResource("Color_Cure");
            CureCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            MagnetCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            MagnetCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            MagnetCount.Fill = (LinearGradientBrush)FindResource("Color_Magnet");
            MagnetCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            ReflectCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            ReflectCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            ReflectCount.Fill = (LinearGradientBrush)FindResource("Color_Reflect");
            ReflectCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            PageCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            PageCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            PageCount.Fill = (LinearGradientBrush)FindResource("Color_Page");
            PageCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            MunnyCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            MunnyCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            MunnyCount.Fill = (LinearGradientBrush)FindResource("Color_Pouch");
            MunnyCount.Stroke = (SolidColorBrush)FindResource("Color_Black");

            Data.WorldItems.Clear();
            data.TrackedReports.Clear();

            CollectionGrid.Visibility = Visibility.Visible;
            ScoreGrid.Visibility = Visibility.Hidden;
            ProgressionCollectionGrid.Visibility = Visibility.Hidden;

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
            
            //if (AutoDetectOption.IsChecked)
            //{
            //    SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
            //}
            //else
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
            TornPagesToggle(true);
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

            //SetAutoDetectTimer();
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
                ModeDisplay.Header = "Shan Hints";
                data.mode = mode;
                //ReportsToggle(false);
            }
            else if (mode == Mode.Hints || mode == Mode.OpenKHHints)
            {
                ModeDisplay.Header = "Jsmartee Hints";
                data.mode = mode;
                //ReportsToggle(true);
            }
            else if (mode == Mode.DAHints)
            {
                ModeDisplay.Header = "Points Hints";
                data.mode = mode;
                //ReportsToggle(true);

                UpdatePointScore(0);
                ShowCheckCountToggle(null, null);
            }
            else if (mode == Mode.PathHints)
            {
                ModeDisplay.Header = "Path Hints";
                data.mode = mode;
                //ReportsToggle(true);
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

                ModeDisplay.Header += " | HSM";
            }

            if (data.UsingProgressionHints)
            {
                CollectionGrid.Visibility = Visibility.Collapsed;
                ScoreGrid.Visibility = Visibility.Collapsed;
                ProgressionCollectionGrid.Visibility = Visibility.Visible;

                ChestIcon.SetResourceReference(ContentProperty, "ProgPoints");

                ModeDisplay.Header += " | Progression";
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
                ZipArchiveEntry hintsfile = null;
                ZipArchiveEntry hashfile = null;
                ZipArchiveEntry enemyfile = null;

                //get and temp store these files to grab data from later.
                //we used to just read them as we wnt along, but things got more complicated as time went on..
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.Equals("HintFile.Hints"))
                    {
                        hintsfile = entry;
                    }
                    if (entry.FullName.Equals("sys.yml"))
                    {
                        hashfile = entry;
                    }
                    if (entry.FullName.Equals("enemies.rando"))
                    {
                        enemyfile = entry;
                    }
                }

                if (enemyfile != null)
                {
                    using (var reader3 = new StreamReader(enemyfile.Open()))
                    {
                        data.BossRandoFound = true;
                        string enemyText64 = reader3.ReadToEnd();
                        var enemyText = Encoding.UTF8.GetString(Convert.FromBase64String(enemyText64));
                        try 
                        {
                            var enemyObject = JsonSerializer.Deserialize<Dictionary<string, object>>(enemyText);
                            var bosses = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(enemyObject["BOSSES"].ToString());

                            foreach (var bosspair in bosses)
                            {
                                string bossOrig = bosspair["original"].ToString();
                                string bossRepl = bosspair["new"].ToString();

                                data.BossList.Add(bossOrig, bossRepl);
                            }
                        }
                        catch 
                        {
                            data.BossRandoFound = false;

                            if (App.logger != null)
                                App.logger.Record("error while trying to parse bosses.");
                        }
                        
                        reader3.Close();
                    }
                }

                if (hashfile != null)
                {
                    using (var reader2 = new StreamReader(hashfile.Open()))
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

                        HashToSeed(hash);

                        reader2.Close();
                    }
                }

                if (hintsfile != null)
                {
                    using (StreamReader reader = new StreamReader(hintsfile.Open()))
                    {
                        data.openKHHintText = reader.ReadToEnd();
                        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                        var settings = new List<string>();
                        var hintableItems = new List<string>();
                        try
                        {
                            hintableItems = new List<string>(JsonSerializer.Deserialize<List<string>>(hintObject["reveal"].ToString()));
                        }
                        catch { }

                        data.ShouldResetHash = false;

                        if (hintObject.ContainsKey("settings"))
                        {
                            settings = JsonSerializer.Deserialize<List<string>>(hintObject["settings"].ToString());

                            #region Settings

                            if (hintableItems.Contains("report"))
                                ReportsToggle(true);
                            else
                                ReportsToggle(false);

                            if (hintableItems.Contains("page"))
                                TornPagesToggle(true);
                            else
                                TornPagesToggle(false);

                            if (hintableItems.Contains("ability"))
                                AbilitiesToggle(true);
                            else
                                AbilitiesToggle(false);

                            if (hintableItems.Count == 0)
                            {
                                ReportsToggle(true);
                                TornPagesToggle(true);
                                AbilitiesToggle(true);
                            }

                            //item settings
                            PromiseCharmToggle(false);
                            //AbilitiesToggle(false);
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

                            //progression hints GoA Current Hint Count
                            data.WorldsData["GoA"].value.Visibility = Visibility.Hidden;

                            //settings visuals
                            SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
                            Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Absent_Split.Width = new GridLength(0, GridUnitType.Star);
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
                            //bool abilitiesOn = true;
                            bool dataSplitOn = false;
                            bool puzzleOn = false;
                            bool synthOn = false;

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
                                    //case "Level1Mode":
                                    //    abilitiesOn = false;
                                    //    break;
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
                                        Setting_Level_01.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "ExcludeFrom50":
                                        SoraLevel50Toggle(true);
                                        AbilitiesToggle(true);
                                        Setting_Level_50.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("SorasHeart");
                                        break;
                                    case "ExcludeFrom99":
                                        SoraLevel99Toggle(true);
                                        AbilitiesToggle(true);
                                        Setting_Level_99.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("SorasHeart");
                                        break;
                                    case "Simulated Twilight Town":
                                        SimulatedToggle(true);
                                        data.enabledWorlds.Add("STT");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("SimulatedTwilightTown");
                                        break;
                                    case "Hundred Acre Wood":
                                        HundredAcreWoodToggle(true);
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("HundredAcreWood");
                                        break;
                                    case "Atlantica":
                                        AtlanticaToggle(true);
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("Atlantica");
                                        break;
                                    case "Puzzle":
                                        PuzzleToggle(true);
                                        //data.WorldsEnabled++;
                                        //data.HintRevealOrder.Add("PuzzSynth");
                                        puzzleOn = true;
                                        data.puzzlesOn = true;
                                        break;
                                    case "Synthesis":
                                        SynthToggle(true);
                                        //data.WorldsEnabled++;
                                        //data.HintRevealOrder.Add("PuzzSynth");
                                        synthOn = true;
                                        data.synthOn = true;
                                        break;
                                    case "Form Levels":
                                        DrivesToggle(true);
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("DriveForms");
                                        break;
                                    case "Land of Dragons":
                                        LandofDragonsToggle(true);
                                        data.enabledWorlds.Add("LoD");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("LandofDragons");
                                        break;
                                    case "Beast's Castle":
                                        BeastCastleToggle(true);
                                        data.enabledWorlds.Add("BC");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("BeastsCastle");
                                        break;
                                    case "Hollow Bastion":
                                        HollowBastionToggle(true);
                                        data.enabledWorlds.Add("HB");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("HollowBastion");
                                        break;
                                    case "Twilight Town":
                                        TwilightTownToggle(true);
                                        data.enabledWorlds.Add("TT");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("TwilightTown");
                                        break;
                                    case "The World That Never Was":
                                        TWTNWToggle(true);
                                        data.enabledWorlds.Add("TWTNW");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("TWTNW");
                                        break;
                                    case "Space Paranoids":
                                        SpaceParanoidsToggle(true);
                                        data.enabledWorlds.Add("SP");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("SpaceParanoids");
                                        break;
                                    case "Port Royal":
                                        PortRoyalToggle(true);
                                        data.enabledWorlds.Add("PR");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("PortRoyal");
                                        break;
                                    case "Olympus Coliseum":
                                        OlympusToggle(true);
                                        data.enabledWorlds.Add("OC");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("OlympusColiseum");
                                        break;
                                    case "Agrabah":
                                        AgrabahToggle(true);
                                        data.enabledWorlds.Add("AG");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("Agrabah");
                                        break;
                                    case "Halloween Town":
                                        HalloweenTownToggle(true);
                                        data.enabledWorlds.Add("HT");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("HalloweenTown");
                                        break;
                                    case "Pride Lands":
                                        PrideLandsToggle(true);
                                        data.enabledWorlds.Add("PL");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("PrideLands");
                                        break;
                                    case "Disney Castle / Timeless River":
                                        DisneyCastleToggle(true);
                                        data.enabledWorlds.Add("DC");
                                        data.WorldsEnabled++;
                                        data.HintRevealOrder.Add("DisneyCastle");
                                        break;
                                    //settings
                                    case "better_stt":
                                        Setting_BetterSTT.Width = new GridLength(1.1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Cavern of Remembrance":
                                        Setting_Cavern.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Data Split":
                                        Setting_Absent_Split.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        dataSplitOn = true;
                                        break;
                                    case "Absent Silhouettes":
                                        if (!dataSplitOn) //only use if we didn't already set the data split version
                                        {
                                            Setting_Absent.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                        }
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
                                    //progression hints
                                    case "ProgressionHints":
                                        data.UsingProgressionHints = true;
                                        data.WorldsData["GoA"].value.Visibility = Visibility.Visible;
                                        data.WorldsData["GoA"].value.Text = "0";
                                        Console.WriteLine("ENABLING PROGRESSION HINTS");
                                        break;
                                }
                            }

                            //if (abilitiesOn == false)
                            //    AbilitiesToggle(false);

                            //prevent creations hinting twice for progression
                            if (puzzleOn)
                            {
                                data.WorldsEnabled++;
                                data.HintRevealOrder.Add("PuzzSynth");
                            }

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

                        if (hintObject.ContainsKey("ProgressionSettings"))
                        {
                            var progressionSettings = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(hintObject["ProgressionSettings"].ToString());

                            foreach (var setting in progressionSettings)
                            {
                                Console.WriteLine("progression setting found = " + setting.Key);

                                switch (setting.Key)
                                {
                                    case "HintCosts":
                                        data.HintCosts.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HintCosts.Add(cost);
                                        data.HintCosts.Add(data.HintCosts[data.HintCosts.Count - 1] + 1); //duplicates the last cost for logic reasons
                                        break;
                                    case "SimulatedTwilightTown":
                                        data.STT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.STT_ProgressionValues.Add(cost);
                                        break;
                                    case "TwilightTown":
                                        data.TT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.TT_ProgressionValues.Add(cost);
                                        break;
                                    case "HollowBastion":
                                        data.HB_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HB_ProgressionValues.Add(cost);
                                        break;
                                    case "CavernofRemembrance":
                                        data.CoR_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.CoR_ProgressionValues.Add(cost);
                                        break;
                                    case "LandofDragons":
                                        data.LoD_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.LoD_ProgressionValues.Add(cost);
                                        break;
                                    case "BeastsCastle":
                                        data.BC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.BC_ProgressionValues.Add(cost);
                                        break;
                                    case "OlympusColiseum":
                                        data.OC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.OC_ProgressionValues.Add(cost);
                                        break;
                                    case "DisneyCastle":
                                        data.DC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.DC_ProgressionValues.Add(cost);
                                        break;
                                    case "Agrabah":
                                        data.AG_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.AG_ProgressionValues.Add(cost);
                                        break;
                                    case "PortRoyal":
                                        data.PR_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.PR_ProgressionValues.Add(cost);
                                        break;
                                    case "HalloweenTown":
                                        data.HT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HT_ProgressionValues.Add(cost);
                                        break;
                                    case "PrideLands":
                                        data.PL_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.PL_ProgressionValues.Add(cost);
                                        break;
                                    case "HundredAcreWood":
                                        data.HAW_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HAW_ProgressionValues.Add(cost);
                                        break;
                                    case "SpaceParanoids":
                                        data.SP_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.SP_ProgressionValues.Add(cost);
                                        break;
                                    case "TWTNW":
                                        data.TWTNW_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.TWTNW_ProgressionValues.Add(cost);
                                        break;
                                    case "Atlantica":
                                        data.AT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.AT_ProgressionValues.Add(cost);
                                        break;
                                    case "ReportBonus":
                                        data.ReportBonus = setting.Value[0];
                                        break;
                                    case "WorldCompleteBonus":
                                        data.WorldCompleteBonus = setting.Value[0];
                                        break;
                                    case "Levels":
                                        data.Levels_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.Levels_ProgressionValues.Add(cost);
                                        break;
                                    case "Drives":
                                        data.Drives_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.Drives_ProgressionValues.Add(cost);
                                        break;
                                    case "FinalXemnasReveal":
                                        data.revealFinalXemnas = setting.Value[0] == 0 ? false : true;
                                        break;
                                }
                            }
                            //data.NumOfHints = data.HintCosts.Count;
                            //set text correctly
                            ProgressionCollectedValue.Text = "0";
                            ProgressionTotalValue.Text = data.HintCosts[0].ToString();
                        }

                        reader.Close();
                    }
                }

                archive.Dispose();
            }
        }
    
        //Turns the zip seed icon hash to a numerical based seed
        private void HashToSeed(string[] hash)
        {
            int icon1 = Codes.HashInt[hash[0]];
            int icon2 = Codes.HashInt[hash[1]];
            int icon3 = Codes.HashInt[hash[2]];
            int icon4 = Codes.HashInt[hash[3]];
            int icon5 = Codes.HashInt[hash[4]];
            int icon6 = Codes.HashInt[hash[5]];
            int icon7 = Codes.HashInt[hash[6]];

            int final = (icon1 + icon2) * (icon3 + icon4) * (icon5 + icon6) - icon7;

            data.BossRandoSeed = final;
            data.ProgressionHash = final;
        }
    }
}
