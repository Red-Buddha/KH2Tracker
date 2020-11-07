using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Documents;
using System.Runtime.InteropServices;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;
        private BroadcastWindow broadcast;

        public MainWindow()
        {
            InitializeComponent();

            InitData();

            InitOptions();
        }
        
        private void InitData()
        {
            data = new Data();

            data.Worlds.Add(SorasHeart);
            data.Worlds.Add(DriveForms);
            data.Worlds.Add(SimulatedTwilightTown);
            data.Worlds.Add(TwilightTown);
            data.Worlds.Add(HollowBastion);
            data.Worlds.Add(BeastsCastle);
            data.Worlds.Add(OlympusColiseum);
            data.Worlds.Add(Agrabah);
            data.Worlds.Add(LandofDragons);
            data.Worlds.Add(HundredAcreWood);
            data.Worlds.Add(PrideLands);
            data.Worlds.Add(DisneyCastle);
            data.Worlds.Add(HalloweenTown);
            data.Worlds.Add(PortRoyal);
            data.Worlds.Add(SpaceParanoids);
            data.Worlds.Add(TWTNW);
            data.Worlds.Add(Atlantica);
            data.Worlds.Add(GoA);

            data.Hints.Add(SorasHeartHint);
            data.Hints.Add(DriveFormsHint);
            data.Hints.Add(SimulatedHint);
            data.Hints.Add(TwilightTownHint);
            data.Hints.Add(HollowBastionHint);
            data.Hints.Add(BeastsCastleHint);
            data.Hints.Add(OlympusColiseumHint);
            data.Hints.Add(AgrabahHint);
            data.Hints.Add(LandofDragonsHint);
            data.Hints.Add(HundredAcreWoodHint);
            data.Hints.Add(PrideLandsHint);
            data.Hints.Add(DisneyCastleHint);
            data.Hints.Add(HalloweenTownHint);
            data.Hints.Add(PortRoyalHint);
            data.Hints.Add(SpaceParanoidsHint);
            data.Hints.Add(TWTNWHint);
            data.Hints.Add(AtlanticaHint);

            data.Grids.Add(SorasHeartGrid);
            data.Grids.Add(DriveFormsGrid);
            data.Grids.Add(SimulatedTwilightTownGrid);
            data.Grids.Add(TwilightTownGrid);
            data.Grids.Add(HollowBastionGrid);
            data.Grids.Add(BeastsCastleGrid);
            data.Grids.Add(OlympusColiseumGrid);
            data.Grids.Add(AgrabahGrid);
            data.Grids.Add(LandofDragonsGrid);
            data.Grids.Add(HundredAcreWoodGrid);
            data.Grids.Add(PrideLandsGrid);
            data.Grids.Add(DisneyCastleGrid);
            data.Grids.Add(HalloweenTownGrid);
            data.Grids.Add(PortRoyalGrid);
            data.Grids.Add(SpaceParanoidsGrid);
            data.Grids.Add(TWTNWGrid);
            data.Grids.Add(AtlanticaGrid);
            data.Grids.Add(GoAGrid);

            data.SelectedBars.Add(SorasHeartBar);
            data.SelectedBars.Add(DriveFormsBar);
            data.SelectedBars.Add(SimulatedBar);
            data.SelectedBars.Add(TwilightTownBar);
            data.SelectedBars.Add(HollowBastionBar);
            data.SelectedBars.Add(BeastsCastleBar);
            data.SelectedBars.Add(OlympusBar);
            data.SelectedBars.Add(AgrabahBar);
            data.SelectedBars.Add(LandofDragonsBar);
            data.SelectedBars.Add(HundredAcreWoodBar);
            data.SelectedBars.Add(PrideLandsBar);
            data.SelectedBars.Add(DisneyCastleBar);
            data.SelectedBars.Add(HalloweenTownBar);
            data.SelectedBars.Add(PortRoyalBar);
            data.SelectedBars.Add(SpaceParanoidsBar);
            data.SelectedBars.Add(TWTNWBar);
            data.SelectedBars.Add(AtlanticaBar);
            data.SelectedBars.Add(GoABar);

            data.Reports.Add(Report1);
            data.Reports.Add(Report2);
            data.Reports.Add(Report3);
            data.Reports.Add(Report4);
            data.Reports.Add(Report5);
            data.Reports.Add(Report6);
            data.Reports.Add(Report7);
            data.Reports.Add(Report8);
            data.Reports.Add(Report9);
            data.Reports.Add(Report10);
            data.Reports.Add(Report11);
            data.Reports.Add(Report12);
            data.Reports.Add(Report13);

            data.ReportAttemptVisual.Add(Report1Attempts);
            data.ReportAttemptVisual.Add(Report2Attempts);
            data.ReportAttemptVisual.Add(Report3Attempts);
            data.ReportAttemptVisual.Add(Report4Attempts);
            data.ReportAttemptVisual.Add(Report5Attempts);
            data.ReportAttemptVisual.Add(Report6Attempts);
            data.ReportAttemptVisual.Add(Report7Attempts);
            data.ReportAttemptVisual.Add(Report8Attempts);
            data.ReportAttemptVisual.Add(Report9Attempts);
            data.ReportAttemptVisual.Add(Report10Attempts);
            data.ReportAttemptVisual.Add(Report11Attempts);
            data.ReportAttemptVisual.Add(Report12Attempts);
            data.ReportAttemptVisual.Add(Report13Attempts);

            data.TornPages.Add(TornPage1);
            data.TornPages.Add(TornPage2);
            data.TornPages.Add(TornPage3);
            data.TornPages.Add(TornPage4);
            data.TornPages.Add(TornPage5);

            data.Numbers.Add(new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Zero.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\One.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Two.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Three.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Four.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Five.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Six.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Seven.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Eight.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Nine.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Ten.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Eleven.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Twelve.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Thirteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Fourteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Fifteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Sixteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Seventeen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Eighteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Nineteen.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Twenty.png", UriKind.Relative)));

            foreach (ContentControl item in ItemPool.Children)
            {
                if (item is Item)
                {
                    data.Items.Add(item as Item);
                }
            }

            broadcast = new BroadcastWindow(data);

        }

        private void InitOptions()
        {
            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            HandleItemToggle(PromiseCharmOption, PromiseCharm, true);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption, data.Reports[i], true);
            }

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            HandleItemToggle(AbilitiesOption, OnceMore, true);
            HandleItemToggle(AbilitiesOption, SecondChance, true);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption, data.TornPages[i], true);
            }

            CureOption.IsChecked = Properties.Settings.Default.Cure;
            HandleItemToggle(CureOption, Cure1, true);
            HandleItemToggle(CureOption, Cure2, true);
            HandleItemToggle(CureOption, Cure3, true);

            FinalFormOption.IsChecked = Properties.Settings.Default.FinalForm;
            HandleItemToggle(FinalFormOption, Final, true);

            SimpleOption.IsChecked = Properties.Settings.Default.Simple;
            SimpleToggle(null, null);

            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(null, null);
            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(null, null);
            HundredAcreWoodOption.IsChecked = Properties.Settings.Default.HundredAcre;
            HundredAcreWoodToggle(null, null);
            AtlanticaOption.IsChecked = Properties.Settings.Default.Atlantica;
            AtlanticaToggle(null, null);

            Top = Properties.Settings.Default.WindowY;
            Left = Properties.Settings.Default.WindowX;
        }


        /// 
        /// Input Handling
        /// 
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            
            if (e.ChangedButton == MouseButton.Left)
            {
                if (data.selected != null)
                {
                    for(int i = 0; i < data.SelectedBars.Count; ++i)
                    {
                        if (data.Worlds[i] == data.selected)
                        {
                            data.SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                        }
                    }
                }

                data.selected = button;
                for (int i = 0; i < data.SelectedBars.Count; ++i)
                {
                    if (data.Worlds[i] == data.selected)
                    {
                        data.SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBar.png", UriKind.Relative));
                    }
                }
            }
            else if(e.ChangedButton == MouseButton.Middle)
            {
                for(int i = 0; i < data.Hints.Count; ++i)
                {
                    if(button == data.Worlds[i])
                    {
                        data.Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));

                        data.Hints[i].Margin = new Thickness(25, -20, 0, 0);

                        break;
                    }
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            for (int i = 0; i < data.Hints.Count; ++i)
            {
                if (button == data.Worlds[i])
                {
                    HandleReportValue(data.Hints[i], e.Delta);

                    break;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && data.selected != null)
            {
                for (int i = 0; i < data.Hints.Count; ++i)
                {
                    if (data.Worlds[i] == data.selected)
                    {
                        HandleReportValue(data.Hints[i], -1);
                    }
                }
            }
            if (e.Key == Key.PageUp && data.selected != null)
            {
                for (int i = 0; i < data.Hints.Count; ++i)
                {
                    if (data.Worlds[i] == data.selected)
                    {
                        HandleReportValue(data.Hints[i], 1);
                    }
                }
            }
        }
        
        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
            broadcast.canClose = true;
            broadcast.Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowY = Top;
            Properties.Settings.Default.WindowX = Left;
        }

        /// 
        /// Options
        ///
        
        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                data.reportLocations.Clear();
                data.reportInformation.Clear();
                data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

                for (int i = 0; i < data.Reports.Count; ++i)
                {
                    data.Reports[i].HandleItemReturn();
                }

                Stream stream = openFileDialog.OpenFile();
                StreamReader streamReader = new StreamReader(stream);

                if (streamReader.EndOfStream)
                {
                    HintText.Content = "Error loading hints";
                    streamReader.Close();
                    return;
                }

                string line1 = streamReader.ReadLine();
                string[] reportvalues = line1.Split('.');

                if (streamReader.EndOfStream)
                {
                    HintText.Content = "Error loading hints";
                    streamReader.Close();
                    return;
                }

                string line2 = streamReader.ReadLine();
                line2 = line2.TrimEnd('.');
                string[] reportorder = line2.Split('.');

                streamReader.Close();

                for(int i = 0; i < reportorder.Length; ++i)
                {
                    data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
                    string[] temp = reportvalues[i].Split(',');
                    data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                }

                data.hintsLoaded = true;
                HintText.Content = "Hints Loaded";
            }
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

                    item.MouseDoubleClick -= item.Item_Return;
                    item.MouseDoubleClick += item.Item_Click;
                }

                data.Grids[i].Rows = 1;
                data.Grids[i].Height = 40;
            }

            data.hintsLoaded = false;
            for (int i = 0; i < data.Hints.Count; ++i)
            {
                data.Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));
            }
            data.reportAttempts = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            data.reportInformation.Clear();
            data.reportLocations.Clear();
            for(int i = 0; i < data.ReportAttemptVisual.Count; ++i)
            {
                data.ReportAttemptVisual[i].SetResourceReference(ContentProperty, "Fail0");
            }
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

        private void PromiseCharmToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PromiseCharm = PromiseCharmOption.IsChecked;
            HandleItemToggle(PromiseCharmOption, PromiseCharm, false);
        }

        private void ReportsToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AnsemReports = ReportsOption.IsChecked;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption, data.Reports[i], false);
            }
        }

        private void AbilitiesToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Abilities = AbilitiesOption.IsChecked;
            HandleItemToggle(AbilitiesOption, OnceMore, false);
            HandleItemToggle(AbilitiesOption, SecondChance, false);
        }

        private void TornPagesToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TornPages = TornPagesOption.IsChecked;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption, data.TornPages[i], false);
            }
        }

        private void CureToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Cure = CureOption.IsChecked;
            HandleItemToggle(CureOption, Cure1, false);
            HandleItemToggle(CureOption, Cure2, false);
            HandleItemToggle(CureOption, Cure3, false);
        }

        private void FinalFormToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FinalForm = FinalFormOption.IsChecked;
            HandleItemToggle(FinalFormOption, Final, false);
        }

        private void SoraHeartToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SoraHeart = SoraHeartOption.IsChecked;
            HandleWorldToggle(SoraHeartOption, SorasHeart, SorasHeartGrid);
        }

        private void SimulatedToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Simulated = SimulatedOption.IsChecked;
            HandleWorldToggle(SimulatedOption, SimulatedTwilightTown, SimulatedTwilightTownGrid);
        }

        private void HundredAcreWoodToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.HundredAcre = HundredAcreWoodOption.IsChecked;
            HandleWorldToggle(HundredAcreWoodOption, HundredAcreWood, HundredAcreWoodGrid);
        }

        private void AtlanticaToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Atlantica = AtlanticaOption.IsChecked;
            HandleWorldToggle(AtlanticaOption, Atlantica, AtlanticaGrid);
        }

        private void SimpleToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Simple = SimpleOption.IsChecked;
            if (SimpleOption.IsChecked)
            {
                Report1.SetResourceReference(ContentProperty, "AnsemReport1");
                Report2.SetResourceReference(ContentProperty, "AnsemReport2");
                Report3.SetResourceReference(ContentProperty, "AnsemReport3");
                Report4.SetResourceReference(ContentProperty, "AnsemReport4");
                Report5.SetResourceReference(ContentProperty, "AnsemReport5");
                Report6.SetResourceReference(ContentProperty, "AnsemReport6");
                Report7.SetResourceReference(ContentProperty, "AnsemReport7");
                Report8.SetResourceReference(ContentProperty, "AnsemReport8");
                Report9.SetResourceReference(ContentProperty, "AnsemReport9");
                Report10.SetResourceReference(ContentProperty, "AnsemReport10");
                Report11.SetResourceReference(ContentProperty, "AnsemReport11");
                Report12.SetResourceReference(ContentProperty, "AnsemReport12");
                Report13.SetResourceReference(ContentProperty, "AnsemReport13");
                Fire1.SetResourceReference(ContentProperty, "Fire");
                Fire2.SetResourceReference(ContentProperty, "Fire");
                Fire3.SetResourceReference(ContentProperty, "Fire");
                Blizzard1.SetResourceReference(ContentProperty, "Blizzard");
                Blizzard2.SetResourceReference(ContentProperty, "Blizzard");
                Blizzard3.SetResourceReference(ContentProperty, "Blizzard");
                Thunder1.SetResourceReference(ContentProperty, "Thunder");
                Thunder2.SetResourceReference(ContentProperty, "Thunder");
                Thunder3.SetResourceReference(ContentProperty, "Thunder");
                Cure1.SetResourceReference(ContentProperty, "Cure");
                Cure2.SetResourceReference(ContentProperty, "Cure");
                Cure3.SetResourceReference(ContentProperty, "Cure");
                Reflect1.SetResourceReference(ContentProperty, "Reflect");
                Reflect2.SetResourceReference(ContentProperty, "Reflect");
                Reflect3.SetResourceReference(ContentProperty, "Reflect");
                Magnet1.SetResourceReference(ContentProperty, "Magnet");
                Magnet2.SetResourceReference(ContentProperty, "Magnet");
                Magnet3.SetResourceReference(ContentProperty, "Magnet");
                Valor.SetResourceReference(ContentProperty, "Valor");
                Wisdom.SetResourceReference(ContentProperty, "Wisdom");
                Limit.SetResourceReference(ContentProperty, "Limit");
                Master.SetResourceReference(ContentProperty, "Master");
                Final.SetResourceReference(ContentProperty, "Final");
                OnceMore.SetResourceReference(ContentProperty, "OnceMore");
                SecondChance.SetResourceReference(ContentProperty, "SecondChance");
                TornPage1.SetResourceReference(ContentProperty, "TornPage");
                TornPage2.SetResourceReference(ContentProperty, "TornPage");
                TornPage3.SetResourceReference(ContentProperty, "TornPage");
                TornPage4.SetResourceReference(ContentProperty, "TornPage");
                TornPage5.SetResourceReference(ContentProperty, "TornPage");
                Lamp.SetResourceReference(ContentProperty, "Genie");
                Ukulele.SetResourceReference(ContentProperty, "Stitch");
                Baseball.SetResourceReference(ContentProperty, "ChickenLittle");
                Feather.SetResourceReference(ContentProperty, "PeterPan");
                Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistence");
                Connection.SetResourceReference(ContentProperty, "ProofOfConnection");
                Peace.SetResourceReference(ContentProperty, "ProofOfPeace");
                PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharm");
            }
            else
            {
                Report1.SetResourceReference(ContentProperty, "AnsemReportOld1");
                Report2.SetResourceReference(ContentProperty, "AnsemReportOld2");
                Report3.SetResourceReference(ContentProperty, "AnsemReportOld3");
                Report4.SetResourceReference(ContentProperty, "AnsemReportOld4");
                Report5.SetResourceReference(ContentProperty, "AnsemReportOld5");
                Report6.SetResourceReference(ContentProperty, "AnsemReportOld6");
                Report7.SetResourceReference(ContentProperty, "AnsemReportOld7");
                Report8.SetResourceReference(ContentProperty, "AnsemReportOld8");
                Report9.SetResourceReference(ContentProperty, "AnsemReportOld9");
                Report10.SetResourceReference(ContentProperty, "AnsemReportOld10");
                Report11.SetResourceReference(ContentProperty, "AnsemReportOld11");
                Report12.SetResourceReference(ContentProperty, "AnsemReportOld12");
                Report13.SetResourceReference(ContentProperty, "AnsemReportOld13");
                Fire1.SetResourceReference(ContentProperty, "FireOld");
                Fire2.SetResourceReference(ContentProperty, "FireOld");
                Fire3.SetResourceReference(ContentProperty, "FireOld");
                Blizzard1.SetResourceReference(ContentProperty, "BlizzardOld");
                Blizzard2.SetResourceReference(ContentProperty, "BlizzardOld");
                Blizzard3.SetResourceReference(ContentProperty, "BlizzardOld");
                Thunder1.SetResourceReference(ContentProperty, "ThunderOld");
                Thunder2.SetResourceReference(ContentProperty, "ThunderOld");
                Thunder3.SetResourceReference(ContentProperty, "ThunderOld");
                Cure1.SetResourceReference(ContentProperty, "CureOld");
                Cure2.SetResourceReference(ContentProperty, "CureOld");
                Cure3.SetResourceReference(ContentProperty, "CureOld");
                Reflect1.SetResourceReference(ContentProperty, "ReflectOld");
                Reflect2.SetResourceReference(ContentProperty, "ReflectOld");
                Reflect3.SetResourceReference(ContentProperty, "ReflectOld");
                Magnet1.SetResourceReference(ContentProperty, "MagnetOld");
                Magnet2.SetResourceReference(ContentProperty, "MagnetOld");
                Magnet3.SetResourceReference(ContentProperty, "MagnetOld");
                Valor.SetResourceReference(ContentProperty, "ValorOld");
                Wisdom.SetResourceReference(ContentProperty, "WisdomOld");
                Limit.SetResourceReference(ContentProperty, "LimitOld");
                Master.SetResourceReference(ContentProperty, "MasterOld");
                Final.SetResourceReference(ContentProperty, "FinalOld");
                OnceMore.SetResourceReference(ContentProperty, "OnceMoreOld");
                SecondChance.SetResourceReference(ContentProperty, "SecondChanceOld");
                TornPage1.SetResourceReference(ContentProperty, "TornPageOld");
                TornPage2.SetResourceReference(ContentProperty, "TornPageOld");
                TornPage3.SetResourceReference(ContentProperty, "TornPageOld");
                TornPage4.SetResourceReference(ContentProperty, "TornPageOld");
                TornPage5.SetResourceReference(ContentProperty, "TornPageOld");
                Lamp.SetResourceReference(ContentProperty, "GenieOld");
                Ukulele.SetResourceReference(ContentProperty, "StitchOld");
                Baseball.SetResourceReference(ContentProperty, "ChickenLittleOld");
                Feather.SetResourceReference(ContentProperty, "PeterPanOld");
                Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistenceOld");
                Connection.SetResourceReference(ContentProperty, "ProofOfConnectionOld");
                Peace.SetResourceReference(ContentProperty, "ProofOfPeaceOld");
                PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharmOld");
            }
        }

        private void WorldIconsToggle(object sender, RoutedEventArgs e)
        {
            if(WorldIconsOption.IsChecked)
            {
                SorasHeart.SetResourceReference(ContentProperty, "SoraHeartImage");
                SimulatedTwilightTown.SetResourceReference(ContentProperty, "SimulatedImage");
                HollowBastion.SetResourceReference(ContentProperty, "HollowBastionImage");
                OlympusColiseum.SetResourceReference(ContentProperty, "OlympusImage");
                LandofDragons.SetResourceReference(ContentProperty, "LandofDragonsImage");
                PrideLands.SetResourceReference(ContentProperty, "PrideLandsImage");
                HalloweenTown.SetResourceReference(ContentProperty, "HalloweenTownImage");
                SpaceParanoids.SetResourceReference(ContentProperty, "SpaceParanoidsImage");
                GoA.SetResourceReference(ContentProperty, "GardenofAssemblageImage");
                
                DriveForms.SetResourceReference(ContentProperty, "DriveFormsImage");
                TwilightTown.SetResourceReference(ContentProperty, "TwilightTownImage");
                BeastsCastle.SetResourceReference(ContentProperty, "BeastCastleImage");
                Agrabah.SetResourceReference(ContentProperty, "AgrabahImage");
                HundredAcreWood.SetResourceReference(ContentProperty, "HundredAcreImage");
                DisneyCastle.SetResourceReference(ContentProperty, "DisneyCastleImage");
                PortRoyal.SetResourceReference(ContentProperty, "PortRoyalImage");
                TWTNW.SetResourceReference(ContentProperty, "TWTNWImage");
                Atlantica.SetResourceReference(ContentProperty, "AtlanticaImage");
            }
            else
            {
                SorasHeart.SetResourceReference(ContentProperty, "SoraHeartText");
                SimulatedTwilightTown.SetResourceReference(ContentProperty, "SimulatedText");
                HollowBastion.SetResourceReference(ContentProperty, "HollowBastionText");
                OlympusColiseum.SetResourceReference(ContentProperty, "OlympusText");
                LandofDragons.SetResourceReference(ContentProperty, "LandofDragonsText");
                PrideLands.SetResourceReference(ContentProperty, "PrideLandsText");
                HalloweenTown.SetResourceReference(ContentProperty, "HalloweenTownText");
                SpaceParanoids.SetResourceReference(ContentProperty, "SpaceParanoidsText");
                GoA.SetResourceReference(ContentProperty, "GardenofAssemblageText");
                
                DriveForms.SetResourceReference(ContentProperty, "DriveFormsText");
                TwilightTown.SetResourceReference(ContentProperty, "TwilightTownText");
                BeastsCastle.SetResourceReference(ContentProperty, "BeastCastleText");
                Agrabah.SetResourceReference(ContentProperty, "AgrabahText");
                HundredAcreWood.SetResourceReference(ContentProperty, "HundredAcreText");
                DisneyCastle.SetResourceReference(ContentProperty, "DisneyCastleText");
                PortRoyal.SetResourceReference(ContentProperty, "PortRoyalText");
                TWTNW.SetResourceReference(ContentProperty, "TWTNWText");
                Atlantica.SetResourceReference(ContentProperty, "AtlanticaText");
            }
        }


        /// 
        /// Handle UI Changes
        /// 
        private void HandleReportValue(Image Hint, int delta)
        {
            int num = 0;

            for(int i = 0; i < data.Numbers.Count; ++i)
            {
                if(Hint.Source == data.Numbers[i])
                {
                    num = i;
                }
            }

            if (delta > 0)
                ++num;
            else
                --num;

            // cap hint value to 20
            if (num > 21)
                num = 21;

            if (num >= 11)
            {
                    Hint.Margin = new Thickness(15, Hint.Margin.Top, 0, 0);
            }
            else
            {
                    Hint.Margin = new Thickness(25, Hint.Margin.Top, 0, 0);
            }

            if (num < 0)
                Hint.Source = data.Numbers[0];
            else
                Hint.Source = data.Numbers[num];

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num-1);
        }

        public void SetReportValue(Image Hint, int value)
        {
            if (value > 21)
                value = 21;

            if (value >= 11)
            {
                Hint.Margin = new Thickness(15, Hint.Margin.Top, 0, 0);
            }
            else
            {
                Hint.Margin = new Thickness(25, Hint.Margin.Top, 0, 0);
            }

            if (value < 0)
                Hint.Source = data.Numbers[0];
            else
                Hint.Source = data.Numbers[value];

        }

        

        private void HandleItemToggle(MenuItem menuItem, Item button, bool init)
        {
            if (menuItem.IsChecked)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
                if(!init)
                    CheckTotal.Text = (int.Parse(CheckTotal.Text) + 1).ToString();
            }
            else
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
                CheckTotal.Text = (int.Parse(CheckTotal.Text) - 1).ToString();

                button.HandleItemReturn();
            }
        }

        private void HandleWorldToggle(MenuItem menuItem, Button button, UniformGrid grid)
        {
            if (menuItem.IsChecked)
            {
                ((button.Parent as StackPanel).Parent as StackPanel).Height = Double.NaN;
                ((button.Parent as StackPanel).Parent as StackPanel).IsEnabled = true;
            }
            else
            {
                if (data.selected == button)
                {
                    for (int j = 0; j < data.Worlds.Count; ++j)
                    {
                        if (data.Worlds[j] == data.selected)
                            data.SelectedBars[j].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                    }

                    data.selected = null;

                    for (int i = 0; i < data.SelectedBars.Count; ++i)
                    {
                        if (data.Worlds[i] == data.selected)
                        {
                            data.SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                        }
                    }
                }

                for (int i = grid.Children.Count - 1; i >= 0; --i)
                {
                    Item item = grid.Children[i] as Item;
                    item.HandleItemReturn();
                }

                ((button.Parent as StackPanel).Parent as StackPanel).Height = 0;
                ((button.Parent as StackPanel).Parent as StackPanel).IsEnabled = false;
            }
        }

        public void IncrementCollected()
        {
            Collected.Text = (int.Parse(Collected.Text) + 1).ToString();
        }

        public void DecrementCollected()
        {
            Collected.Text = (int.Parse(Collected.Text) - 1).ToString();
        }

        public void SetHintText(string text)
        {
            HintText.Content = text;
        }

        private void BroadcastWindow_Open(object sender, RoutedEventArgs e)
        {
            broadcast.Show();
        }
    }
}
