using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;


namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool hintsLoaded = false;
        Button selected = null;
        Codes codes = new Codes();
        List<Tuple<string, int>> reportInformation = new List<Tuple<string, int>>();
        List<string> reportLocations = new List<string>();
        List<int> reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        List<Button> Worlds = new List<Button>();
        List<Image> Hints = new List<Image>();
        List<UniformGrid> Grids = new List<UniformGrid>();
        List<Button> Reports = new List<Button>();
        List<ContentControl> ReportAttemptVisual = new List<ContentControl>();
        List<Button> TornPages = new List<Button>();
        List<Image> SelectedBars = new List<Image>();
        List<BitmapImage> Numbers = new List<BitmapImage>();

        public MainWindow()
        {
            InitializeComponent();

            Worlds.Add(SorasHeart);
            Worlds.Add(DriveForms);
            Worlds.Add(SimulatedTwilightTown);
            Worlds.Add(TwilightTown);
            Worlds.Add(HollowBastion);
            Worlds.Add(BeastsCastle);
            Worlds.Add(OlympusColiseum);
            Worlds.Add(Agrabah);
            Worlds.Add(LandofDragons);
            Worlds.Add(HundredAcreWood);
            Worlds.Add(PrideLands);
            Worlds.Add(DisneyCastle);
            Worlds.Add(HalloweenTown);
            Worlds.Add(PortRoyal);
            Worlds.Add(SpaceParanoids);
            Worlds.Add(TWTNW);
            Worlds.Add(Atlantica);
            Worlds.Add(GoA);

            Hints.Add(SorasHeartHint);
            Hints.Add(DriveFormsHint);
            Hints.Add(SimulatedHint);
            Hints.Add(TwilightTownHint);
            Hints.Add(HollowBastionHint);
            Hints.Add(BeastsCastleHint);
            Hints.Add(OlympusColiseumHint);
            Hints.Add(AgrabahHint);
            Hints.Add(LandofDragonsHint);
            Hints.Add(HundredAcreWoodHint);
            Hints.Add(PrideLandsHint);
            Hints.Add(DisneyCastleHint);
            Hints.Add(HalloweenTownHint);
            Hints.Add(PortRoyalHint);
            Hints.Add(SpaceParanoidsHint);
            Hints.Add(TWTNWHint);
            Hints.Add(AtlanticaHint);

            Grids.Add(SorasHeartGrid);
            Grids.Add(DriveFormsGrid);
            Grids.Add(SimulatedGrid);
            Grids.Add(TwilightTownGrid);
            Grids.Add(HollowBastionGrid);
            Grids.Add(BeastsCastleGrid);
            Grids.Add(OlympusColiseumGrid);
            Grids.Add(AgrabahGrid);
            Grids.Add(LandofDragonsGrid);
            Grids.Add(HundredAcreWoodGrid);
            Grids.Add(PrideLandsGrid);
            Grids.Add(DisneyCastleGrid);
            Grids.Add(HalloweenTownGrid);
            Grids.Add(PortRoyalGrid);
            Grids.Add(SpaceParanoidsGrid);
            Grids.Add(TWTNWGrid);
            Grids.Add(AtlanticaGrid);
            Grids.Add(GoAGrid);

            SelectedBars.Add(SorasHeartBar);
            SelectedBars.Add(DriveFormsBar);
            SelectedBars.Add(SimulatedBar);
            SelectedBars.Add(TwilightTownBar);
            SelectedBars.Add(HollowBastionBar);
            SelectedBars.Add(BeastsCastleBar);
            SelectedBars.Add(OlympusBar);
            SelectedBars.Add(AgrabahBar);
            SelectedBars.Add(LandofDragonsBar);
            SelectedBars.Add(HundredAcreWoodBar);
            SelectedBars.Add(PrideLandsBar);
            SelectedBars.Add(DisneyCastleBar);
            SelectedBars.Add(HalloweenTownBar);
            SelectedBars.Add(PortRoyalBar);
            SelectedBars.Add(SpaceParanoidsBar);
            SelectedBars.Add(TWTNWBar);
            SelectedBars.Add(AtlanticaBar);
            SelectedBars.Add(GoABar);

            Reports.Add(Report1);
            Reports.Add(Report2);
            Reports.Add(Report3);
            Reports.Add(Report4);
            Reports.Add(Report5);
            Reports.Add(Report6);
            Reports.Add(Report7);
            Reports.Add(Report8);
            Reports.Add(Report9);
            Reports.Add(Report10);
            Reports.Add(Report11);
            Reports.Add(Report12);
            Reports.Add(Report13);

            ReportAttemptVisual.Add(Report1Attempts);
            ReportAttemptVisual.Add(Report2Attempts);
            ReportAttemptVisual.Add(Report3Attempts);
            ReportAttemptVisual.Add(Report4Attempts);
            ReportAttemptVisual.Add(Report5Attempts);
            ReportAttemptVisual.Add(Report6Attempts);
            ReportAttemptVisual.Add(Report7Attempts);
            ReportAttemptVisual.Add(Report8Attempts);
            ReportAttemptVisual.Add(Report9Attempts);
            ReportAttemptVisual.Add(Report10Attempts);
            ReportAttemptVisual.Add(Report11Attempts);
            ReportAttemptVisual.Add(Report12Attempts);
            ReportAttemptVisual.Add(Report13Attempts);

            TornPages.Add(TornPage1);
            TornPages.Add(TornPage2);
            TornPages.Add(TornPage3);
            TornPages.Add(TornPage4);
            TornPages.Add(TornPage5);

            Numbers.Add(new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Zero.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\One.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Two.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Three.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Four.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Five.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Six.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Seven.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Eight.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Nine.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Ten.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Eleven.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Twelve.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Thirteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Fourteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Fifteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Sixteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Seventeen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Eighteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Nineteen.png", UriKind.Relative)));
            Numbers.Add(new BitmapImage(new Uri("Images\\Twenty.png", UriKind.Relative)));

            InitOptions();
        }
        
        private void InitOptions()
        {
            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            HandleItemToggle(PromiseCharmOption, PromiseCharm, true);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            for (int i = 0; i < Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption, Reports[i], true);
            }

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            HandleItemToggle(AbilitiesOption, OnceMore, true);
            HandleItemToggle(AbilitiesOption, SecondChance, true);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            for (int i = 0; i < TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption, TornPages[i], true);
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
                if (selected != null)
                {
                    for(int i = 0; i < SelectedBars.Count; ++i)
                    {
                        if (Worlds[i] == selected)
                        {
                            SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                        }
                    }
                }

                selected = button;
                for (int i = 0; i < SelectedBars.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBar.png", UriKind.Relative));
                    }
                }
            }
            else if(e.ChangedButton == MouseButton.Middle)
            {
                for(int i = 0; i < Hints.Count; ++i)
                {
                    if(button == Worlds[i])
                    {
                        Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));
                        
                        Hints[i].Margin = new Thickness(25, -20, 0, 0);

                        break;
                    }
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            for (int i = 0; i < Hints.Count; ++i)
            {
                if (button == Worlds[i])
                {
                    HandleReportValue(Hints[i], e.Delta);

                    break;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && selected != null)
            {
                for (int i = 0; i < Hints.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        HandleReportValue(Hints[i], -1);
                    }
                }
            }
            if (e.Key == Key.PageUp && selected != null)
            {
                for (int i = 0; i < Hints.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        HandleReportValue(Hints[i], 1);
                    }
                }
            }
        }
        
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (selected != null)
            {
                bool isreport = false;

                // item is a report
                if (hintsLoaded && (int)button.GetValue(Grid.RowProperty) == 0)
                {
                    int index = (int)button.GetValue(Grid.ColumnProperty);

                    if (reportAttempts[index] == 0)
                        return;

                    if (reportLocations[index].Replace(" ", "") == selected.Name)
                    {
                        HintText.Content = reportInformation[index].Item1 + " has " + reportInformation[index].Item2 + " important checks.";
                        ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail0");
                        reportAttempts[index] = 3;
                        isreport = true;

                        for (int i = 0; i < Hints.Count; ++i)
                        {
                            if (Worlds[i].Name == reportInformation[index].Item1.Replace(" ", ""))
                                SetReportValue(Hints[i], reportInformation[index].Item2 + 1);
                        }
                    }
                    else
                    {
                        reportAttempts[index]--;
                        if (reportAttempts[index] == 0)
                            ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail3");
                        else if (reportAttempts[index] == 1)
                            ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail2");
                        else if (reportAttempts[index] == 2)
                            ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail1");

                        return;
                    }
                }

                if (isreport == false)
                    HintText.Content = "";

                ItemPool.Children.Remove(button);

                for (int i = 0; i < Worlds.Count; ++i)
                {
                    if(selected == Worlds[i])
                    {
                        HandleWorldGrid(Grids[i], button, true);
                        break;
                    }
                }

                int collected = int.Parse(Collected.Text) + 1;
                Collected.Text = collected.ToString();

                button.Click -= Item_Click;
                button.Click += Item_Return;

                if(isreport)
                {
                    button.MouseEnter += Report_Hover;
                }
            }
        }

        private void Item_Return(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            HandleItemReturn(button);
        }

        private void HandleItemReturn(Button button)
        {
            if (button.Parent != ItemPool)
            {
                HandleWorldGrid(button.Parent as UniformGrid, button, false);

                ItemPool.Children.Add(button);

                int collected = int.Parse(Collected.Text) - 1;
                Collected.Text = collected.ToString();

                button.Click -= Item_Return;

                button.Click += Item_Click;

                button.MouseEnter -= Report_Hover;
            }
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowY = Top;
            Properties.Settings.Default.WindowX = Left;
        }

        private void Report_Hover(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = (int)button.GetValue(Grid.ColumnProperty);
            
            HintText.Content = reportInformation[index].Item1 + " has " + reportInformation[index].Item2 + " important checks.";
        }

        /// 
        /// Options
        ///
        
        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            if(openFileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < Reports.Count; ++i)
                {
                    HandleItemReturn(Reports[i]);
                }

                Stream stream = openFileDialog.OpenFile();
                StreamReader streamReader = new StreamReader(stream);

                string line1 = streamReader.ReadLine();
                string[] reportvalues = line1.Split('.');

                string line2 = streamReader.ReadLine();
                line2 = line2.TrimEnd('.');
                string[] reportorder = line2.Split('.');

                for(int i = 0; i < reportorder.Length; ++i)
                {
                    reportLocations.Add(codes.FindCode(reportorder[i]));
                    string[] temp = reportvalues[i].Split(',');
                    reportInformation.Add(new Tuple<string, int>(codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                }

                hintsLoaded = true;
                HintText.Content = "Hints Loaded";
            }
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            Collected.Text = "0";
            HintText.Content = "";

            if (selected != null)
            {
                for (int i = 0; i < SelectedBars.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                    }
                }
            }
            selected = null;

            for (int i = 0; i < Grids.Count; ++i)
            {
                for (int j = Grids[i].Children.Count - 1; j >= 0; --j)
                {
                    Button item = Grids[i].Children[j] as Button;
                    Grids[i].Children.Remove(Grids[i].Children[j]);
                    ItemPool.Children.Add(item);

                    item.Click -= Item_Return;
                    item.Click += Item_Click;
                }

                Grids[i].Rows = 1;
                Grids[i].Height = 40;
            }

            hintsLoaded = false;
            for (int i = 0; i < Hints.Count; ++i)
            {
                Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));
            }
            reportAttempts = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            reportInformation.Clear();
            reportLocations.Clear();
            for(int i = 0; i < ReportAttemptVisual.Count; ++i)
            {
                ReportAttemptVisual[i].SetResourceReference(ContentProperty, "Fail0");
            }
        }

        private void PromiseCharmToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PromiseCharm = PromiseCharmOption.IsChecked;
            HandleItemToggle(PromiseCharmOption, PromiseCharm, false);
        }

        private void ReportsToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AnsemReports = ReportsOption.IsChecked;
            for (int i = 0; i < Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption, Reports[i], false);
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
            for (int i = 0; i < TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption, TornPages[i], false);
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
            HandleWorldToggle(SimulatedOption, SimulatedTwilightTown, SimulatedGrid);
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

            for(int i = 0; i < Numbers.Count; ++i)
            {
                if(Hint.Source == Numbers[i])
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
                Hint.Source = Numbers[0];
            else
                Hint.Source = Numbers[num];
        }

        private void SetReportValue(Image Hint, int value)
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
                Hint.Source = Numbers[0];
            else
                Hint.Source = Numbers[value];
        }

        private void HandleWorldGrid(UniformGrid grid, Button button, bool add)
        {
            if (add)
                grid.Children.Add(button);
            else
                grid.Children.Remove(button);

            int gridremainder = 0;
            if (grid.Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((grid.Children.Count / 5) + gridremainder, 1);

            grid.Rows = gridnum;
            grid.Height = grid.Rows * 40;
        }

        private void HandleItemToggle(MenuItem menuItem, Button button, bool init)
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

                HandleItemReturn(button);
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
                if (selected == button)
                {
                    for (int j = 0; j < Worlds.Count; ++j)
                    {
                        if (Worlds[j] == selected)
                            SelectedBars[j].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                    }
                    
                    selected = null;

                    for (int i = 0; i < SelectedBars.Count; ++i)
                    {
                        if (Worlds[i] == selected)
                        {
                            SelectedBars[i].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                        }
                    }
                }

                for (int i = grid.Children.Count - 1; i >= 0; --i)
                {
                    Button item = grid.Children[i] as Button;
                    HandleItemReturn(item);
                }

                ((button.Parent as StackPanel).Parent as StackPanel).Height = 0;
                ((button.Parent as StackPanel).Parent as StackPanel).IsEnabled = false;
            }
        }
    }
}
