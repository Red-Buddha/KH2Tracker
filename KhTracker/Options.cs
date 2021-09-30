using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FileName = "kh2fm-tracker-save";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName);
            }
        }

        public void Save(string filename)
        {
            string mode = "Mode: " + data.mode.ToString();
            // save settings
            string settings = "Settings: ";
            if (PromiseCharmOption.IsChecked)
                settings += "Promise Charm - ";
            if (ReportsOption.IsChecked)
                settings += "Secret Ansem Reports - ";
            if (AbilitiesOption.IsChecked)
                settings += "Second Chance & Once More - ";
            if (TornPagesOption.IsChecked)
                settings += "Torn Pages - ";
            if (CureOption.IsChecked)
                settings += "Cure - ";
            if (FinalFormOption.IsChecked)
                settings += "Final Form - ";
            //if (HadesCupOption.IsChecked)
            //    settings += "Hades Cup - ";
            if (SoraHeartOption.IsChecked)
                settings += "Sora's Heart - ";
            if (SimulatedOption.IsChecked)
                settings += "Simulated Twilight Town - ";
            if (HundredAcreWoodOption.IsChecked)
                settings += "100 Acre Wood - ";
            if (AtlanticaOption.IsChecked)
                settings += "Atlantica - ";
            if (CavernOption.IsChecked)
                settings += "Cavern of Remembrance - ";
            if (TimelessOption.IsChecked)
                settings += "Timeless River - ";
            if (OCCupsOption.IsChecked)
                settings += "Olympus Cups - ";
            //if (PuzzleOption.IsChecked)
            //    settings += "Puzzle Rewards - ";

            // save hint state (hint info, hints, track attempts)
            string attempts = "";
            string hintValues = "";
            if (data.mode == Mode.Hints || data.mode == Mode.OpenKHHints)
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
                hintValues = "HintValues:";
                foreach (WorldData worldData in data.WorldsData.Values.ToList())
                {
                    if (worldData.hint == null)
                        continue;

                    int num = 0;
                    for (int i = 0; i < data.Numbers.Count; ++i)
                    {
                        if (worldData.hint.Source == data.Numbers[i])
                            num = i;
                    }
                    hintValues += " " + num.ToString();
                }
            }

            // Save progress of worlds
            string Progress = "Progress:";
            Progress += " " + data.WorldsData["SimulatedTwilightTown"].progress.ToString();
            Progress += " " + data.WorldsData["TwilightTown"].progress.ToString();
            Progress += " " + data.WorldsData["HollowBastion"].progress.ToString();
            Progress += " " + data.WorldsData["BeastsCastle"].progress.ToString();
            Progress += " " + data.WorldsData["OlympusColiseum"].progress.ToString();
            Progress += " " + data.WorldsData["Agrabah"].progress.ToString();
            Progress += " " + data.WorldsData["LandofDragons"].progress.ToString();
            Progress += " " + data.WorldsData["HundredAcreWood"].progress.ToString();
            Progress += " " + data.WorldsData["PrideLands"].progress.ToString();
            Progress += " " + data.WorldsData["DisneyCastle"].progress.ToString();
            Progress += " " + data.WorldsData["HalloweenTown"].progress.ToString();
            Progress += " " + data.WorldsData["PortRoyal"].progress.ToString();
            Progress += " " + data.WorldsData["SpaceParanoids"].progress.ToString();
            Progress += " " + data.WorldsData["TWTNW"].progress.ToString();
            Progress += " " + data.WorldsData["Atlantica"].progress.ToString();

            // save items in worlds
            string soraHeart = "SorasHeart:";
            foreach (Item item in data.WorldsData["SorasHeart"].worldGrid.Children)
            {
                soraHeart += " " + item.Name;
            }
            string driveForms = "DriveForms:";
            foreach (Item item in data.WorldsData["DriveForms"].worldGrid.Children)
            {
                driveForms += " " + item.Name;
            }
            string simulated = "SimulatedTwilightTown:";
            foreach (Item item in data.WorldsData["SimulatedTwilightTown"].worldGrid.Children)
            {
                simulated += " " + item.Name;
            }
            string twilightTown = "TwilightTown:";
            foreach (Item item in data.WorldsData["TwilightTown"].worldGrid.Children)
            {
                twilightTown += " " + item.Name;
            }
            string hollowBastion = "HollowBastion:";
            foreach (Item item in data.WorldsData["HollowBastion"].worldGrid.Children)
            {
                hollowBastion += " " + item.Name;
            }
            string beastCastle = "BeastsCastle:";
            foreach (Item item in data.WorldsData["BeastsCastle"].worldGrid.Children)
            {
                beastCastle += " " + item.Name;
            }
            string olympusColiseum = "OlympusColiseum:";
            foreach (Item item in data.WorldsData["OlympusColiseum"].worldGrid.Children)
            {
                olympusColiseum += " " + item.Name;
            }
            string agrabah = "Agrabah:";
            foreach (Item item in data.WorldsData["Agrabah"].worldGrid.Children)
            {
                agrabah += " " + item.Name;
            }
            string landOfDragons = "LandofDragons:";
            foreach (Item item in data.WorldsData["LandofDragons"].worldGrid.Children)
            {
                landOfDragons += " " + item.Name;
            }
            string hundredAcreWood = "HundredAcreWood:";
            foreach (Item item in data.WorldsData["HundredAcreWood"].worldGrid.Children)
            {
                hundredAcreWood += " " + item.Name;
            }
            string prideLands = "PrideLands:";
            foreach (Item item in data.WorldsData["PrideLands"].worldGrid.Children)
            {
                prideLands += " " + item.Name;
            }
            string disneyCastle = "DisneyCastle:";
            foreach (Item item in data.WorldsData["DisneyCastle"].worldGrid.Children)
            {
                disneyCastle += " " + item.Name;
            }
            string halloweenTown = "HalloweenTown:";
            foreach (Item item in data.WorldsData["HalloweenTown"].worldGrid.Children)
            {
                halloweenTown += " " + item.Name;
            }
            string portRoyal = "PortRoyal:";
            foreach (Item item in data.WorldsData["PortRoyal"].worldGrid.Children)
            {
                portRoyal += " " + item.Name;
            }
            string spaceparanoids = "SpaceParanoids:";
            foreach (Item item in data.WorldsData["SpaceParanoids"].worldGrid.Children)
            {
                spaceparanoids += " " + item.Name;
            }
            string TWTNW = "TWTNW:";
            foreach (Item item in data.WorldsData["TWTNW"].worldGrid.Children)
            {
                TWTNW += " " + item.Name;
            }
            string atlantica = "Atlantica:";
            foreach (Item item in data.WorldsData["Atlantica"].worldGrid.Children)
            {
                atlantica += " " + item.Name;
            }
            string GoA = "GoA:";
            foreach (Item item in data.WorldsData["GoA"].worldGrid.Children)
            {
                GoA += " " + item.Name;
            }

            FileStream file = File.Create(filename);
            StreamWriter writer = new StreamWriter(file);

            writer.WriteLine(mode);
            writer.WriteLine(settings);
            if (data.mode == Mode.Hints)
            {
                writer.WriteLine(attempts);
                writer.WriteLine(data.hintFileText[0]);
                writer.WriteLine(data.hintFileText[1]);
                writer.WriteLine(hintValues);
            }
            else if (data.mode == Mode.OpenKHHints)
            {
                writer.WriteLine(attempts);
                writer.WriteLine(data.openKHHintText);
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
            writer.WriteLine(Progress);
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

        private void LoadProgress(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FileName = "kh2fm-tracker-save";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                Load(openFileDialog.FileName);
            }
        }

        private void Load(string filename)
        {
            Stream file = File.Open(filename, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            // reset tracker
            OnReset(null, null);

            string mode = reader.ReadLine().Substring(6);
            if (mode == "Hints")
                SetMode(Mode.Hints);
            else if (mode == "AltHints")
                SetMode(Mode.AltHints);
            else if (mode == "OpenKHHints")
                SetMode(Mode.OpenKHHints);
            else if (mode == "OpenKHAltHints")
                SetMode(Mode.OpenKHAltHints);

            // set settings
            string settings = reader.ReadLine();
            LoadSettings(settings.Substring(10));

            // set hint state
            if (mode == "Hints")
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

                data.hintsLoaded = true;
                HintText.Content = "Hints Loaded";
            }
            else if (mode == "AltHints")
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
                    SetReportValue(data.WorldsData[key].hint, 1);
                }
            }
            else if (mode == "OpenKHHints")
            {
                string attempts = reader.ReadLine();
                attempts = attempts.Substring(13);
                string[] attemptsArray = attempts.Split('-');
                for (int i = 0; i < attemptsArray.Length; ++i)
                {
                    data.reportAttempts[i] = int.Parse(attemptsArray[i]);
                }
                data.openKHHintText = reader.ReadLine();
                var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());

                List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
                reportKeys.Sort();

                foreach (var report in reportKeys)
                {
                    var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
                    var count = reports[report.ToString()]["Count"].ToString();
                    var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
                    data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
                    data.reportLocations.Add(location);
                }

                data.hintsLoaded = true;
                HintText.Content = "Hints Loaded";
            }
            else if (mode == "OpenKHAltHints")
            {
                data.openKHHintText = reader.ReadLine();
                var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());

                foreach (var world in worlds)
                {
                    if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                    {
                        continue;
                    }
                    foreach (var item in world.Value)
                    {
                        data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
                    }

                }
                foreach (var key in data.WorldsData.Keys.ToList())
                {
                    if (key == "GoA")
                        continue;

                    data.WorldsData[key].worldGrid.WorldComplete();
                    SetReportValue(data.WorldsData[key].hint, 1);
                }
            }


            // set hint values (DUMB)
            if (data.hintsLoaded)
            {
                string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
                SetReportValue(data.WorldsData["SorasHeart"].hint, int.Parse(hintValues[0]));
                SetReportValue(data.WorldsData["DriveForms"].hint, int.Parse(hintValues[1]));
                SetReportValue(data.WorldsData["SimulatedTwilightTown"].hint, int.Parse(hintValues[2]));
                SetReportValue(data.WorldsData["TwilightTown"].hint, int.Parse(hintValues[3]));
                SetReportValue(data.WorldsData["HollowBastion"].hint, int.Parse(hintValues[4]));
                SetReportValue(data.WorldsData["BeastsCastle"].hint, int.Parse(hintValues[5]));
                SetReportValue(data.WorldsData["OlympusColiseum"].hint, int.Parse(hintValues[6]));
                SetReportValue(data.WorldsData["Agrabah"].hint, int.Parse(hintValues[7]));
                SetReportValue(data.WorldsData["LandofDragons"].hint, int.Parse(hintValues[8]));
                SetReportValue(data.WorldsData["HundredAcreWood"].hint, int.Parse(hintValues[9]));
                SetReportValue(data.WorldsData["PrideLands"].hint, int.Parse(hintValues[10]));
                SetReportValue(data.WorldsData["DisneyCastle"].hint, int.Parse(hintValues[11]));
                SetReportValue(data.WorldsData["HalloweenTown"].hint, int.Parse(hintValues[12]));
                SetReportValue(data.WorldsData["PortRoyal"].hint, int.Parse(hintValues[13]));
                SetReportValue(data.WorldsData["SpaceParanoids"].hint, int.Parse(hintValues[14]));
                SetReportValue(data.WorldsData["TWTNW"].hint, int.Parse(hintValues[15]));
                SetReportValue(data.WorldsData["Atlantica"].hint, int.Parse(hintValues[16]));
            }

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
            //backward compatability with old progress saves
            if (progress.Length == 15)
                data.WorldsData["Atlantica"].progress = int.Parse(progress[14]);
            else
                data.WorldsData["Atlantica"].progress = 0;

            SetProgressIcons();

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

            string STTkey = Prog + data.ProgressKeys["SimulatedTwilightTown"][data.WorldsData["SimulatedTwilightTown"].progress];
            data.WorldsData["SimulatedTwilightTown"].progression.SetResourceReference(ContentProperty, STTkey);
            broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, STTkey);

            string TTkey = Prog + data.ProgressKeys["TwilightTown"][data.WorldsData["TwilightTown"].progress];
            data.WorldsData["TwilightTown"].progression.SetResourceReference(ContentProperty, TTkey);
            broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, TTkey);

            string HBkey = Prog + data.ProgressKeys["HollowBastion"][data.WorldsData["HollowBastion"].progress];
            data.WorldsData["HollowBastion"].progression.SetResourceReference(ContentProperty, HBkey);
            broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, HBkey);

            string BCkey = Prog + data.ProgressKeys["BeastsCastle"][data.WorldsData["BeastsCastle"].progress];
            data.WorldsData["BeastsCastle"].progression.SetResourceReference(ContentProperty, BCkey);
            broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, BCkey);

            string OCkey = Prog + data.ProgressKeys["OlympusColiseum"][data.WorldsData["OlympusColiseum"].progress];
            data.WorldsData["OlympusColiseum"].progression.SetResourceReference(ContentProperty, OCkey);
            broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, OCkey);

            string AGkey = Prog + data.ProgressKeys["Agrabah"][data.WorldsData["Agrabah"].progress];
            data.WorldsData["Agrabah"].progression.SetResourceReference(ContentProperty, AGkey);
            broadcast.AgrabahProgression.SetResourceReference(ContentProperty, AGkey);

            string LoDkey = Prog + data.ProgressKeys["LandofDragons"][data.WorldsData["LandofDragons"].progress];
            data.WorldsData["LandofDragons"].progression.SetResourceReference(ContentProperty, LoDkey);
            broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, LoDkey);

            string HAWkey = Prog + data.ProgressKeys["HundredAcreWood"][data.WorldsData["HundredAcreWood"].progress];
            data.WorldsData["HundredAcreWood"].progression.SetResourceReference(ContentProperty, HAWkey);
            broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, LoDkey);

            string PLkey = Prog + data.ProgressKeys["PrideLands"][data.WorldsData["PrideLands"].progress];
            data.WorldsData["PrideLands"].progression.SetResourceReference(ContentProperty, PLkey);
            broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, PLkey);

            string DCkey = Prog + data.ProgressKeys["DisneyCastle"][data.WorldsData["DisneyCastle"].progress];
            data.WorldsData["DisneyCastle"].progression.SetResourceReference(ContentProperty, DCkey);
            broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, DCkey);

            string HTkey = Prog + data.ProgressKeys["HalloweenTown"][data.WorldsData["HalloweenTown"].progress];
            data.WorldsData["HalloweenTown"].progression.SetResourceReference(ContentProperty, HTkey);
            broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, HTkey);

            string PRkey = Prog + data.ProgressKeys["PortRoyal"][data.WorldsData["PortRoyal"].progress];
            data.WorldsData["PortRoyal"].progression.SetResourceReference(ContentProperty, PRkey);
            broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, PRkey);

            string SPkey = Prog + data.ProgressKeys["SpaceParanoids"][data.WorldsData["SpaceParanoids"].progress];
            data.WorldsData["SpaceParanoids"].progression.SetResourceReference(ContentProperty, SPkey);
            broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, SPkey);

            string TWTNWkey = Prog + data.ProgressKeys["TWTNW"][data.WorldsData["TWTNW"].progress];
            data.WorldsData["TWTNW"].progression.SetResourceReference(ContentProperty, TWTNWkey);
            broadcast.TWTNWProgression.SetResourceReference(ContentProperty, TWTNWkey);

            string ATkey = Prog + data.ProgressKeys["Atlantica"][data.WorldsData["Atlantica"].progress];
            data.WorldsData["Atlantica"].progression.SetResourceReference(ContentProperty, ATkey);
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".hint";
            openFileDialog.Filter = "hint files (*.hint)|*.hint";
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

            //get correct numbers when resetting (i really need to make a custom funtion just for this)
            bool OldMode = Properties.Settings.Default.OldNum;
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            var NormalNum = data.Numbers;
            {
                //check numbers
                if (OldMode)
                    NormalNum = data.OldNumbers;

                if (CustomMode && CustomNumbersFound)
                    NormalNum = data.CustomNumbers;
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
            }
            data.WorldsData["GoA"].hinted = true;

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hintedHint = false;
            }

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }
            
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                if (worldData.hint != null)
                    worldData.hint.Source = NormalNum[0];
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }

            broadcast.OnResetHints();
        }

        private void LoadSettings(string settings)
        {
            bool[] newsettings = new bool[13];

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
                    case "Cavern of Remembrance":
                        newsettings[10] = true;
                        break;
                    case "Timeless River":
                        newsettings[11] = true;
                        break;
                    case "Olympus Cups":
                        newsettings[12] = true;
                        break;
                    //case "Hades Cup":
                    //    newsettings[13] = true;
                    //    break;
                    //case "Puzzle Rewards":
                    //    newsettings[14] = true;
                    //    break;
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
            CavernToggle(newsettings[10]);
            TimelessToggle(newsettings[11]);
            OCCupsToggle(newsettings[12]);
            //HadesCupToggle(newsettings[13]);
            //PuzzleToggle(newsettings[14]);

        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            ModeDisplay.Header = " ";
            data.mode = Mode.None;

            collected = 0;            
            HintText.Content = "";

            //stop autotracking
            if (aTimer != null)
                aTimer.Stop();

            //all this garbage to get the correct number images when changing visual styles
            bool OldMode = Properties.Settings.Default.OldNum;
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage BarW = data.VerticalBarW;
            Collected.Source = data.Numbers[1];
            //CollectedBar.SetResourceReference(ContentProperty, "CollectedBarYellow");
            {
                //check numbers
                if (OldMode)
                {
                    Collected.Source = data.OldNumbers[1];
                }

                if (CustomMode)
                {
                    if (MainWindow.CustomNumbersFound)
                        Collected.Source = data.CustomNumbers[1];
                    if (MainWindow.CustomVBarWFound)
                        BarW = data.CustomVerticalBarW;
                }
            }

            if (data.selected != null)
            {
                data.WorldsData[data.selected.Name].selectedBar.Source = BarW;
            }
            data.selected = null;

            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                for (int j = worldData.worldGrid.Children.Count - 1; j >= 0; --j)
                {
                    Item item = worldData.worldGrid.Children[j] as Item;
                    worldData.worldGrid.Children.Remove(worldData.worldGrid.Children[j]);
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

            //restore reports only if toggle was on before
            if (ReportsOption.IsChecked)
            {
                ReportsToggle(true);
                ReportRow.Height = new GridLength(1, GridUnitType.Star);
            }


            ResetHints();

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
                data.WorldsData[key].hintedHint = false;
                data.WorldsData[key].complete = false;
                data.WorldsData[key].checkCount.Clear();
                data.WorldsData[key].progress = 0;
            }
            data.WorldsData["GoA"].hinted = true;            

            broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "");
            broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "");
            broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "");
            broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "");
            broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "");
            broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "");
            broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "");
            broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "");
            broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "");
            broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "");
            broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "");
            broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "");
            broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "");
            broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "");
            broadcast.AtlanticaProgression.SetResourceReference(ContentProperty, "");

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

            FormRow.Height = new GridLength(0, GridUnitType.Star);
            broadcast.GrowthAbilityRow.Height = new GridLength(0, GridUnitType.Star);
            broadcast.StatsRow.Height = new GridLength(0, GridUnitType.Star);


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

            ValorLevel.Source = null;
            WisdomLevel.Source = null;
            LimitLevel.Source = null;
            MasterLevel.Source = null;
            FinalLevel.Source = null;
            HighJumpLevel.Source = null;
            QuickRunLevel.Source = null;
            DodgeRollLevel.Source = null;
            AerialDodgeLevel.Source = null;
            GlideLevel.Source = null;

            broadcast.ValorLevel.Source = null;
            broadcast.WisdomLevel.Source = null;
            broadcast.LimitLevel.Source = null;
            broadcast.MasterLevel.Source = null;
            broadcast.FinalLevel.Source = null;
            broadcast.HighJumpLevel.Source = null;
            broadcast.QuickRunLevel.Source = null;
            broadcast.DodgeRollLevel.Source = null;
            broadcast.AerialDodgeLevel.Source = null;
            broadcast.GlideLevel.Source = null;

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

            broadcast.OnReset();
            broadcast.UpdateNumbers();

            HideSeedHash();
        }
        
        private void BroadcastWindow_Open(object sender, RoutedEventArgs e)
        {
            //ExtraItemToggleCheck();
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
            bool autotrackeron = false;
            bool ps2tracking = false;
            //check for autotracking on and which version
            {
                if (aTimer != null)
                    autotrackeron = true;

                if (pcsx2tracking)
                    ps2tracking = true;
            }

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
                SetReportValue(data.WorldsData[key].hint, 1);
            }

            if (autotrackeron)
            {
                InitAutoTracker(ps2tracking);
            }
        }

        private void SetMode(Mode mode)
        {
            if ((data.mode != mode && data.mode != Mode.None) || mode == Mode.AltHints || mode == Mode.OpenKHAltHints)
                OnReset(null, null);

            if (mode == Mode.AltHints || mode == Mode.OpenKHAltHints)
            {
                ModeDisplay.Header = "Alt Hints Mode";
                data.mode = mode;
                ReportsToggle(false);
                ReportRow.Height = new GridLength(0, GridUnitType.Star);
            }
            else if (mode == Mode.Hints || mode == Mode.OpenKHHints)
            {
                ModeDisplay.Header = "Hints Mode";
                data.mode = mode;
                ReportRow.Height = new GridLength(1, GridUnitType.Star);
            }
        }

        private void OpenKHSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".zip";
            openFileDialog.Filter = "OpenKH Seeds (*.zip)|*.zip";
            openFileDialog.Title = "Select Seed File";
            if (openFileDialog.ShowDialog() == true)
            {
                OpenKHSeed(openFileDialog.FileName);
            }
        }

        private Dictionary<string, string> convertOpenKH = new Dictionary<string, string>()
        {
            {"Level", "SorasHeart" },
            {"Form Levels", "DriveForms" },
            {"Simulated Twilight Town", "SimulatedTwilightTown" },
            {"Twilight Town", "TwilightTown" },
            {"Hollow Bastion", "HollowBastion" },
            {"Beast's Castle", "BeastsCastle" },
            {"Olympus Coliseum", "OlympusColiseum" },
            {"Agrabah", "Agrabah" },
            {"Land of Dragons", "LandofDragons" },
            {"Hundred Acre Wood", "HundredAcreWood" },
            {"Pride Lands", "PrideLands" },
            {"Disney Castle / Timeless River", "DisneyCastle" },
            {"Halloween Town", "HalloweenTown" },
            {"Port Royal", "PortRoyal" },
            {"Space Paranoids", "SpaceParanoids" },
            {"The World That Never Was", "TWTNW" },
            {"Atlantica", "Atlantica" },
            {"Proof of Connection", "Connection" },
            {"Proof of Nonexistence", "Nonexistence" },
            {"Proof of Peace", "Peace" },
            {"PromiseCharm", "PromiseCharm" },
            {"Valor Form", "Valor" },
            {"Wisdom Form", "Wisdom" },
            {"Limit Form", "Limit" },
            {"Master Form", "Master" },
            {"Final Form", "Final" },
            {"Fire Element", "Fire" },
            {"Blizzard Element", "Blizzard" },
            {"Thunder Element", "Thunder" },
            {"Cure Element", "Cure" },
            {"Magnet Element", "Magnet" },
            {"Reflect Element", "Reflect" },
            {"Ukulele Charm (Stitch)", "Ukulele" },
            {"Baseball Charm (Chicken Little)", "Baseball" },
            {"Lamp Charm (Genie)", "Lamp" },
            {"Feather Charm (Peter Pan)", "Feather" },
            {"Torn Pages", "TornPage" },
            {"Second Chance", "SecondChance" },
            {"Once More", "OnceMore" },
            {"", "GoA"}
            //{"Hades Cup", "HadesCup"}
        };

        private void OpenKHSeed(string filename)
        {
            HintText.Content = "";

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
                            var hintObject = JsonSerializer.Deserialize<Dictionary<string,object>>(hintText);
                            switch (hintObject["hintsType"].ToString())
                            {
                                case "Shananas":
                                    SetMode(Mode.OpenKHAltHints);
                                    var worlds = JsonSerializer.Deserialize<Dictionary<string,List<string>>>(hintObject["world"].ToString());

                                    foreach (var world in worlds)
                                    {
                                        if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                                        {
                                            continue;
                                        }
                                        foreach (var item in world.Value)
                                        {
                                            data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
                                        }

                                    }
                                    foreach (var key in data.WorldsData.Keys.ToList())
                                    {
                                        if (key == "GoA")
                                            continue;

                                        data.WorldsData[key].worldGrid.WorldComplete();
                                        SetReportValue(data.WorldsData[key].hint, 1);
                                    }

                                    break;

                                case "JSmartee":
                                    SetMode(Mode.OpenKHHints);
                                    var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,object>>>(hintObject["Reports"].ToString());

                                    List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
                                    reportKeys.Sort();

                                    foreach (var report in reportKeys)
                                    {
                                        var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
                                        var count = reports[report.ToString()]["Count"].ToString();
                                        var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
                                        data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
                                        data.reportLocations.Add(location);
                                    }
                                    ReportsToggle(true);
                                    data.hintsLoaded = true;
                                    //HintText.Content = "Hints Loaded";

                                    break;

                                default:
                                    break;
                            }
                            //Console.WriteLine();
                        }
                    }
                
                    if (entry.FullName == "sys.yml")
                    {
                        using (var reader = new StreamReader(entry.Open()))
                        {
                            string[] separatingStrings = { "- en: ", " ", "'", "{", "}", ":", "icon" };
                            string text1 = reader.ReadLine();
                            string text2 = reader.ReadLine();
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
                            SeedHashLoaded = true;

                            //make visible
                            if (SeedHashOption.IsChecked)
                            {
                                HashText.Visibility = Visibility.Visible;
                                HashIcon1.Visibility = Visibility.Visible;
                                HashIcon2.Visibility = Visibility.Visible;
                                HashIcon3.Visibility = Visibility.Visible;
                                HashIcon4.Visibility = Visibility.Visible;
                                HashIcon5.Visibility = Visibility.Visible;
                                HashIcon6.Visibility = Visibility.Visible;
                                HashIcon7.Visibility = Visibility.Visible;

                                SeedHashVisible = true;
                            }
                        }
                    }
                }
            }
        }

        //Disabled. added here incase i might need it in the future
        private void FixDictionary()
        {
            //buncha garbage to adds hades Cup (and others) as imortant checks in Shan's hints based on toggle state
            //toggle needs to be set before loading the seed into the tracker

            //bool HadesCupOn = HadesTrophyOption.IsChecked;
            //bool MemberCardOn = HBCardOption.IsChecked;
            //bool StoneOn = OStoneOption.IsChecked;
            //bool ComboMasterOn = ComboMasterOption.IsChecked;
            //bool FoundCup;
            //bool FoundCard;
            //bool FoundStone;
            //bool FoundCM;
            //
            ////hades cup check
            //{
            //    if (data.codes.itemCodes.ContainsKey(537))
            //    {
            //        FoundCup = true;
            //    }
            //    else FoundCup = false;
            //}
            //{
            //    if (HadesCupOn && FoundCup == false)
            //    {
            //        data.codes.itemCodes.Add(537, "HadesCup");
            //        //data.codes.itemPoints.Add("HadesCup", 3);
            //    }
            //    else if (HadesCupOn == false && FoundCup)
            //    {
            //        data.codes.itemCodes.Remove(537);
            //        //data.codes.itemPoints.Remove("HadesCup");
            //    }
            //}
            //
            ////membership card check
            //{
            //    if (data.codes.itemCodes.ContainsKey(369))
            //    {
            //        FoundCard = true;
            //    }
            //    else FoundCard = false;
            //}
            //{
            //    if (MemberCardOn && FoundCard == false)
            //    {
            //        data.codes.itemCodes.Add(369, "MembershipCard");
            //        //data.codes.itemPoints.Add("MembershipCard", 3);
            //    }
            //    else if (MemberCardOn == false && FoundCard)
            //    {
            //        data.codes.itemCodes.Remove(369);
            //        //data.codes.itemPoints.Remove("MembershipCard");
            //    }
            //}
            //
            ////olympus stone check
            //{
            //    if (data.codes.itemCodes.ContainsKey(370))
            //    {
            //        FoundStone = true;
            //    }
            //    else FoundStone = false;
            //}
            //{
            //    if (StoneOn && FoundStone == false)
            //    {
            //        data.codes.itemCodes.Add(370, "OlympusStone");
            //        //data.codes.itemPoints.Add("OlympusStone", 3);
            //    }
            //    else if (StoneOn == false && FoundStone)
            //    {
            //        data.codes.itemCodes.Remove(370);
            //        //data.codes.itemPoints.Remove("OlympusStone");
            //    }
            //}
            //
            ////combo master check
            //{
            //    if (data.codes.itemCodes.ContainsKey(539))
            //    {
            //        FoundCM = true;
            //    }
            //    else FoundCM = false;
            //}
            //{
            //    if (ComboMasterOn && FoundCM == false)
            //    {
            //        data.codes.itemCodes.Add(539, "ComboMaster");
            //        //data.codes.itemPoints.Add("ComboMaster", 6);
            //    }
            //    else if (ComboMasterOn == false && FoundCM)
            //    {
            //        data.codes.itemCodes.Remove(539);
            //        //data.codes.itemPoints.Remove("ComboMaster");
            //    }
            //}
        }

        //point based hints test. almost fully working
        public void ParseSeedPoints(string filename)
        {
            //FixDictionary();

            //SetMode(Mode.DAHints);

            //create lists
            List<string> MagicItems = new List<string>();
            List<string> SummonItems = new List<string>();
            List<string> FormItems = new List<string>();
            List<string> AbilityItems = new List<string>();
            List<string> ProofItems = new List<string>();
            List<string> OtherItems = new List<string>();

            MagicItems.Clear();
            SummonItems.Clear();
            FormItems.Clear();
            AbilityItems.Clear();
            ProofItems.Clear();
            OtherItems.Clear();

            MagicItems.Add("Fire");
            MagicItems.Add("Blizzard");
            MagicItems.Add("Thunder");
            MagicItems.Add("Cure");
            MagicItems.Add("Magnet");
            MagicItems.Add("Reflect");
            SummonItems.Add("Ukulele");
            SummonItems.Add("Lamp");
            SummonItems.Add("Feather");
            SummonItems.Add("Baseball");
            FormItems.Add("Valor");
            FormItems.Add("Wisdom");
            FormItems.Add("Final");
            FormItems.Add("Master");
            FormItems.Add("Limit");
            AbilityItems.Add("SecondChance");
            AbilityItems.Add("OnceMore");
            AbilityItems.Add("ComboMaster");
            ProofItems.Add("PromiseCharm");
            ProofItems.Add("Connection");
            ProofItems.Add("Nonexistence");
            ProofItems.Add("Peace");
            OtherItems.Add("HadesCup");
            OtherItems.Add("OlympusStone");
            OtherItems.Add("Membership");

            //set points to 0
            int LevelPoints = 0;
            int DrivePoints = 0;
            int STTPoints = 0;
            int HBPoints = 0;
            int OCPoints = 0;
            int LoDPoints = 0;
            int PLPoints = 0;
            int HTPoints = 0;
            int SPPoints = 0;
            int TTPoints = 0;
            int BCPoints = 0;
            int AGPoints = 0;
            int HAWPoints = 0;
            int DCPoints = 0;
            int PRPoints = 0;
            int TWTNWPoints = 0;
            int ATPoints = 0;


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

                    //check worlds and add points
                    {
                        if (world == "SimulatedTwilightTown")
                        {
                            if (MagicItems.Contains(item))
                                STTPoints += 8;

                            if (SummonItems.Contains(item))
                                STTPoints += 5;

                            if (FormItems.Contains(item))
                                STTPoints += 10;

                            if (AbilityItems.Contains(item))
                                STTPoints += 6;

                            if (ProofItems.Contains(item))
                                STTPoints += 12;

                            if (OtherItems.Contains(item))
                                STTPoints += 3;

                            if (item == "TornPage")
                                STTPoints += 4;
                        }

                        if (world == "TwilightTown")
                        {
                            if (MagicItems.Contains(item))
                                TTPoints += 8;

                            if (SummonItems.Contains(item))
                                TTPoints += 5;

                            if (FormItems.Contains(item))
                                TTPoints += 10;

                            if (AbilityItems.Contains(item))
                                TTPoints += 6;

                            if (ProofItems.Contains(item))
                                TTPoints += 12;

                            if (OtherItems.Contains(item))
                                TTPoints += 3;

                            if (item == "TornPage")
                                TTPoints += 4;
                        }

                        if (world == "HollowBastion")
                        {
                            if (MagicItems.Contains(item))
                                HBPoints += 8;

                            if (SummonItems.Contains(item))
                                HBPoints += 5;

                            if (FormItems.Contains(item))
                                HBPoints += 10;

                            if (AbilityItems.Contains(item))
                                HBPoints += 6;

                            if (ProofItems.Contains(item))
                                HBPoints += 12;

                            if (OtherItems.Contains(item))
                                HBPoints += 3;

                            if (item == "TornPage")
                                HBPoints += 4;
                        }

                        if (world == "LandofDragons")
                        {
                            if (MagicItems.Contains(item))
                                LoDPoints += 8;

                            if (SummonItems.Contains(item))
                                LoDPoints += 5;

                            if (FormItems.Contains(item))
                                LoDPoints += 10;

                            if (AbilityItems.Contains(item))
                                LoDPoints += 6;

                            if (ProofItems.Contains(item))
                                LoDPoints += 12;

                            if (OtherItems.Contains(item))
                                LoDPoints += 3;

                            if (item == "TornPage")
                                LoDPoints += 4;
                        }

                        if (world == "BeastsCastle")
                        {
                            if (MagicItems.Contains(item))
                                BCPoints += 8;

                            if (SummonItems.Contains(item))
                                BCPoints += 5;

                            if (FormItems.Contains(item))
                                BCPoints += 10;

                            if (AbilityItems.Contains(item))
                                BCPoints += 6;

                            if (ProofItems.Contains(item))
                                BCPoints += 12;

                            if (OtherItems.Contains(item))
                                BCPoints += 3;

                            if (item == "TornPage")
                                BCPoints += 4;
                        }

                        if (world == "OlympusColiseum")
                        {
                            if (MagicItems.Contains(item))
                                OCPoints += 8;

                            if (SummonItems.Contains(item))
                                OCPoints += 5;

                            if (FormItems.Contains(item))
                                OCPoints += 10;

                            if (AbilityItems.Contains(item))
                                OCPoints += 6;

                            if (ProofItems.Contains(item))
                                OCPoints += 12;

                            if (OtherItems.Contains(item))
                                OCPoints += 3;

                            if (item == "TornPage")
                                OCPoints += 4;
                        }

                        if (world == "DisneyCastle")
                        {
                            if (MagicItems.Contains(item))
                                DCPoints += 8;

                            if (SummonItems.Contains(item))
                                DCPoints += 5;

                            if (FormItems.Contains(item))
                                DCPoints += 10;

                            if (AbilityItems.Contains(item))
                                DCPoints += 6;

                            if (ProofItems.Contains(item))
                                DCPoints += 12;

                            if (OtherItems.Contains(item))
                                DCPoints += 3;

                            if (item == "TornPage")
                                DCPoints += 4;
                        }

                        if (world == "PortRoyal")
                        {
                            if (MagicItems.Contains(item))
                                PRPoints += 8;

                            if (SummonItems.Contains(item))
                                PRPoints += 5;

                            if (FormItems.Contains(item))
                                PRPoints += 10;

                            if (AbilityItems.Contains(item))
                                PRPoints += 6;

                            if (ProofItems.Contains(item))
                                PRPoints += 12;

                            if (OtherItems.Contains(item))
                                PRPoints += 3;

                            if (item == "TornPage")
                                PRPoints += 4;
                        }

                        if (world == "Agrabah")
                        {
                            if (MagicItems.Contains(item))
                                AGPoints += 8;

                            if (SummonItems.Contains(item))
                                AGPoints += 5;

                            if (FormItems.Contains(item))
                                AGPoints += 10;

                            if (AbilityItems.Contains(item))
                                AGPoints += 6;

                            if (ProofItems.Contains(item))
                                AGPoints += 12;

                            if (OtherItems.Contains(item))
                                AGPoints += 3;

                            if (item == "TornPage")
                                AGPoints += 4;
                        }

                        if (world == "HalloweenTown")
                        {
                            if (MagicItems.Contains(item))
                                HTPoints += 8;

                            if (SummonItems.Contains(item))
                                HTPoints += 5;

                            if (FormItems.Contains(item))
                                HTPoints += 10;

                            if (AbilityItems.Contains(item))
                                HTPoints += 6;

                            if (ProofItems.Contains(item))
                                HTPoints += 12;

                            if (OtherItems.Contains(item))
                                HTPoints += 3;

                            if (item == "TornPage")
                                HTPoints += 4;
                        }

                        if (world == "PrideLands")
                        {
                            if (MagicItems.Contains(item))
                                PLPoints += 8;

                            if (SummonItems.Contains(item))
                                PLPoints += 5;

                            if (FormItems.Contains(item))
                                PLPoints += 10;

                            if (AbilityItems.Contains(item))
                                PLPoints += 6;

                            if (ProofItems.Contains(item))
                                PLPoints += 12;

                            if (OtherItems.Contains(item))
                                PLPoints += 3;

                            if (item == "TornPage")
                                PLPoints += 4;
                        }

                        if (world == "Atlantica")
                        {
                            if (MagicItems.Contains(item))
                                ATPoints += 8;

                            if (SummonItems.Contains(item))
                                ATPoints += 5;

                            if (FormItems.Contains(item))
                                ATPoints += 10;

                            if (AbilityItems.Contains(item))
                                ATPoints += 6;

                            if (ProofItems.Contains(item))
                                ATPoints += 12;

                            if (OtherItems.Contains(item))
                                ATPoints += 3;

                            if (item == "TornPage")
                                ATPoints += 4;
                        }

                        if (world == "HundredAcreWood")
                        {
                            if (MagicItems.Contains(item))
                                HAWPoints += 8;

                            if (SummonItems.Contains(item))
                                HAWPoints += 5;

                            if (FormItems.Contains(item))
                                HAWPoints += 10;

                            if (AbilityItems.Contains(item))
                                HAWPoints += 6;

                            if (ProofItems.Contains(item))
                                HAWPoints += 12;

                            if (OtherItems.Contains(item))
                                HAWPoints += 3;

                            if (item == "TornPage")
                                HAWPoints += 4;
                        }

                        if (world == "SpaceParanoids")
                        {
                            if (MagicItems.Contains(item))
                                SPPoints += 8;

                            if (SummonItems.Contains(item))
                                SPPoints += 5;

                            if (FormItems.Contains(item))
                                SPPoints += 10;

                            if (AbilityItems.Contains(item))
                                SPPoints += 6;

                            if (ProofItems.Contains(item))
                                SPPoints += 12;

                            if (OtherItems.Contains(item))
                                SPPoints += 3;

                            if (item == "TornPage")
                                SPPoints += 4;
                        }

                        if (world == "TWTNW")
                        {
                            if (MagicItems.Contains(item))
                                TWTNWPoints += 8;

                            if (SummonItems.Contains(item))
                                TWTNWPoints += 5;

                            if (FormItems.Contains(item))
                                TWTNWPoints += 10;

                            if (AbilityItems.Contains(item))
                                TWTNWPoints += 6;

                            if (ProofItems.Contains(item))
                                TWTNWPoints += 12;

                            if (OtherItems.Contains(item))
                                TWTNWPoints += 3;

                            if (item == "TornPage")
                                TWTNWPoints += 4;
                        }

                        if (world == "DriveForms")
                        {
                            if (MagicItems.Contains(item))
                                DrivePoints += 8;

                            if (SummonItems.Contains(item))
                                DrivePoints += 5;

                            if (FormItems.Contains(item))
                                DrivePoints += 10;

                            if (AbilityItems.Contains(item))
                                DrivePoints += 6;

                            if (ProofItems.Contains(item))
                                DrivePoints += 12;

                            if (OtherItems.Contains(item))
                                DrivePoints += 3;

                            if (item == "TornPage")
                                DrivePoints += 4;
                        }

                        if (world == "SorasHeart")
                        {
                            if (MagicItems.Contains(item))
                                LevelPoints += 8;

                            if (SummonItems.Contains(item))
                                LevelPoints += 5;

                            if (FormItems.Contains(item))
                                LevelPoints += 10;

                            if (AbilityItems.Contains(item))
                                LevelPoints += 6;

                            if (ProofItems.Contains(item))
                                LevelPoints += 12;

                            if (OtherItems.Contains(item))
                                LevelPoints += 3;

                            if (item == "TornPage")
                                LevelPoints += 4;
                        }
                    }


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

                //data.WorldsData[key].worldGrid.WorldComplete();
                //SetReportValue(data.WorldsData[key].hint, 1);

                if (key == "SimulatedTwilightTown")
                    SetReportValue(data.WorldsData[key].hint, STTPoints + 1);

                if (key == "TwilightTown")
                    SetReportValue(data.WorldsData[key].hint, TTPoints + 1);

                if (key == "HollowBastion")
                    SetReportValue(data.WorldsData[key].hint, HBPoints + 1);

                if (key == "LandofDragons")
                    SetReportValue(data.WorldsData[key].hint, LoDPoints + 1);

                if (key == "BeastsCastle")
                    SetReportValue(data.WorldsData[key].hint, BCPoints + 1);

                if (key == "OlympusColiseum")
                    SetReportValue(data.WorldsData[key].hint, OCPoints + 1);

                if (key == "DisneyCastle")
                    SetReportValue(data.WorldsData[key].hint, DCPoints + 1);

                if (key == "PortRoyal")
                    SetReportValue(data.WorldsData[key].hint, PRPoints + 1);

                if (key == "Agrabah")
                    SetReportValue(data.WorldsData[key].hint, AGPoints + 1);

                if (key == "HalloweenTown")
                    SetReportValue(data.WorldsData[key].hint, HTPoints + 1);

                if (key == "PrideLands")
                    SetReportValue(data.WorldsData[key].hint, PLPoints + 1);

                if (key == "Atlantica")
                    SetReportValue(data.WorldsData[key].hint, ATPoints + 1);

                if (key == "HundredAcreWood")
                    SetReportValue(data.WorldsData[key].hint, HAWPoints + 1);

                if (key == "SpaceParanoids")
                    SetReportValue(data.WorldsData[key].hint, SPPoints + 1);

                if (key == "TWTNW")
                    SetReportValue(data.WorldsData[key].hint, TWTNWPoints + 1);

                if (key == "DriveForms")
                    SetReportValue(data.WorldsData[key].hint, DrivePoints + 1);

                if (key == "SorasHeart")
                    SetReportValue(data.WorldsData[key].hint, LevelPoints + 1);
            }

            HintText.Content = "Points Mode";
        }
    }
}
