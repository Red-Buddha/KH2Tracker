using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;

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
                foreach (Image hint in data.Hints)
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
                foreach (Item item in data.Grids[0].Children)
                {
                    soraHeart += " " + item.Name;
                }
                string driveForms = "DriveForms:";
                foreach (Item item in data.Grids[1].Children)
                {
                    driveForms += " " + item.Name;
                }
                string simulated = "SimulatedTwilightTown:";
                foreach (Item item in data.Grids[2].Children)
                {
                    simulated += " " + item.Name;
                }
                string twilightTown = "TwilightTown:";
                foreach (Item item in data.Grids[3].Children)
                {
                    twilightTown += " " + item.Name;
                }
                string hollowBastion = "HollowBastion:";
                foreach (Item item in data.Grids[4].Children)
                {
                    hollowBastion += " " + item.Name;
                }
                string beastCastle = "BeastsCastle:";
                foreach (Item item in data.Grids[5].Children)
                {
                    beastCastle += " " + item.Name;
                }
                string olympusColiseum = "OlympusColiseum:";
                foreach (Item item in data.Grids[6].Children)
                {
                    olympusColiseum += " " + item.Name;
                }
                string agrabah = "Agrabah:";
                foreach (Item item in data.Grids[7].Children)
                {
                    agrabah += " " + item.Name;
                }
                string landOfDragons = "LandofDragons:";
                foreach (Item item in data.Grids[8].Children)
                {
                    landOfDragons += " " + item.Name;
                }
                string hundredAcreWood = "HundredAcreWood:";
                foreach (Item item in data.Grids[9].Children)
                {
                    hundredAcreWood += " " + item.Name;
                }
                string prideLands = "PrideLands:";
                foreach (Item item in data.Grids[10].Children)
                {
                    prideLands += " " + item.Name;
                }
                string disneyCastle = "DisneyCastle:";
                foreach (Item item in data.Grids[11].Children)
                {
                    disneyCastle += " " + item.Name;
                }
                string halloweenTown = "HalloweenTown:";
                foreach (Item item in data.Grids[12].Children)
                {
                    halloweenTown += " " + item.Name;
                }
                string portRoyal = "PortRoyal:";
                foreach (Item item in data.Grids[13].Children)
                {
                    portRoyal += " " + item.Name;
                }
                string spaceparanoids = "SpaceParanoids:";
                foreach (Item item in data.Grids[14].Children)
                {
                    spaceparanoids += " " + item.Name;
                }
                string TWTNW = "TWTNW:";
                foreach (Item item in data.Grids[15].Children)
                {
                    TWTNW += " " + item.Name;
                }
                string atlantica = "Atlantica:";
                foreach (Item item in data.Grids[16].Children)
                {
                    atlantica += " " + item.Name;
                }
                string GoA = "GoA:";
                foreach (Item item in data.Grids[17].Children)
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
                // set hint values
                string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
                for (int i = 0; i < hintValues.Length; ++i)
                {
                    SetReportValue(data.Hints[i], int.Parse(hintValues[i]));
                }
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

        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                ResetHints();

                Stream stream = openFileDialog.OpenFile();
                StreamReader streamReader = new StreamReader(stream);

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
                    data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
                    string[] temp = reportvalues[i].Split(',');
                    data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                }

                data.hintsLoaded = true;
                HintText.Content = "Hints Loaded";
            }
        }

        private void ResetHints()
        {
            data.hintsLoaded = false;
            data.reportLocations.Clear();
            data.reportInformation.Clear();
            data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }

            for (int i = 0; i < data.Hints.Count; ++i)
            {
                data.Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));

                (data.Hints[i].Parent as Grid).ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                (data.Hints[i].Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.1, GridUnitType.Star);
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }
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
            Collected.Text = "0";
            HintText.Content = "";

            if (data.selected != null)
            {
                for (int i = 0; i < data.SelectedBars.Count; ++i)
                {
                    if (data.Worlds[i] == data.selected)
                    {
                        data.SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                    }
                }
            }
            data.selected = null;

            for (int i = 0; i < data.Grids.Count; ++i)
            {
                for (int j = data.Grids[i].Children.Count - 1; j >= 0; --j)
                {
                    Item item = data.Grids[i].Children[j] as Item;
                    data.Grids[i].Children.Remove(data.Grids[i].Children[j]);
                    ItemPool.Children.Add(item);

                    if (data.dragDrop)
                    {
                        item.MouseDown -= item.Item_Return;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove += item.Item_MouseMove;
                    }
                    else
                    {
                        item.MouseDown -= item.Item_Return;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp += item.Item_MouseUp;
                    }
                }
            }

            // Reset 1st column row heights
            RowDefinitionCollection rows1 = ((data.Grids[0].Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows1)
            {
                row.Height = new GridLength(1, GridUnitType.Star);
            }

            // Reset 2nd column row heights
            RowDefinitionCollection rows2 = ((data.Grids[1].Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows2)
            {
                row.Height = new GridLength(1, GridUnitType.Star);
            }

            ResetHints();

            double broadcastLeft = broadcast.Left;
            double broadcastTop = broadcast.Top;
            bool broadcastVisible = broadcast.IsVisible;
            broadcast.canClose = true;
            broadcast.Close();
            broadcast = new BroadcastWindow(data);
            broadcast.Left = broadcastLeft;
            broadcast.Top = broadcastTop;
            if (broadcastVisible)
                broadcast.Show();
        }
        
        private void BroadcastWindow_Open(object sender, RoutedEventArgs e)
        {
            broadcast.Show();
        }
    }
}
