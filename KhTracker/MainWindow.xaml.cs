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
        public int collected;
        private int total = 51;

        public MainWindow()
        {
            InitializeComponent();

            InitData();

            InitOptions();

            collectedChecks = new List<ImportantCheck>();
            newChecks = new List<ImportantCheck>();
            previousChecks = new List<ImportantCheck>();
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
            data.Hints.Add(SimulatedTwilightTownHint);
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
            data.SelectedBars.Add(SimulatedTwilightTownBar);
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

            data.ReportAttemptVisual.Add(Attempts1);
            data.ReportAttemptVisual.Add(Attempts2);
            data.ReportAttemptVisual.Add(Attempts3);
            data.ReportAttemptVisual.Add(Attempts4);
            data.ReportAttemptVisual.Add(Attempts5);
            data.ReportAttemptVisual.Add(Attempts6);
            data.ReportAttemptVisual.Add(Attempts7);
            data.ReportAttemptVisual.Add(Attempts8);
            data.ReportAttemptVisual.Add(Attempts9);
            data.ReportAttemptVisual.Add(Attempts10);
            data.ReportAttemptVisual.Add(Attempts11);
            data.ReportAttemptVisual.Add(Attempts12);
            data.ReportAttemptVisual.Add(Attempts13);

            data.TornPages.Add(TornPage1);
            data.TornPages.Add(TornPage2);
            data.TornPages.Add(TornPage3);
            data.TornPages.Add(TornPage4);
            data.TornPages.Add(TornPage5);

            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\QuestionMark.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\0.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\1.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\2.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\3.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\4.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\5.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\6.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\7.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\8.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\9.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\10.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\11.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\12.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\13.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\14.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\15.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\16.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\17.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\18.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\19.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\20.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\21.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\22.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\23.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\24.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\25.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\26.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\27.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\28.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\29.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\30.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\31.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\32.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\33.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\34.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\35.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\36.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\37.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\38.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\39.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\40.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\41.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\42.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\43.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\44.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\45.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\46.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\47.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\48.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\49.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\50.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\51.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\52.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\53.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\54.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\55.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\56.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\57.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\58.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\59.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\60.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\61.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\62.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\63.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\64.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\65.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\66.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\67.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\68.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\69.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images\\Numbers\\70.png", UriKind.Relative)));

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
            HandleItemToggle(PromiseCharmOption.IsChecked, PromiseCharm, true);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption.IsChecked, data.Reports[i], true);
            }

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            HandleItemToggle(AbilitiesOption.IsChecked, OnceMore, true);
            HandleItemToggle(AbilitiesOption.IsChecked, SecondChance, true);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption.IsChecked, data.TornPages[i], true);
            }

            CureOption.IsChecked = Properties.Settings.Default.Cure;
            HandleItemToggle(CureOption.IsChecked, Cure1, true);
            HandleItemToggle(CureOption.IsChecked, Cure2, true);
            HandleItemToggle(CureOption.IsChecked, Cure3, true);

            FinalFormOption.IsChecked = Properties.Settings.Default.FinalForm;
            HandleItemToggle(FinalFormOption.IsChecked, Final, true);

            SimpleOption.IsChecked = Properties.Settings.Default.Simple;
            if (SimpleOption.IsChecked)
                SimpleToggle(null, null);

            OrbOption.IsChecked = Properties.Settings.Default.Orb;
            if (OrbOption.IsChecked)
                OrbToggle(null, null);

            AltOption.IsChecked = Properties.Settings.Default.Alt;
            if (AltOption.IsChecked)
                AltToggle(null, null);

            WorldIconsOption.IsChecked = Properties.Settings.Default.WorldIcons;
            WorldIconsToggle(null, null);

            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(SoraHeartOption.IsChecked);
            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(SimulatedOption.IsChecked);
            HundredAcreWoodOption.IsChecked = Properties.Settings.Default.HundredAcre;
            HundredAcreWoodToggle(HundredAcreWoodOption.IsChecked);
            AtlanticaOption.IsChecked = Properties.Settings.Default.Atlantica;
            AtlanticaToggle(AtlanticaOption.IsChecked);

            DragAndDropOption.IsChecked = Properties.Settings.Default.DragDrop;
            DragDropToggle(null, null);

            TopMostOption.IsChecked = Properties.Settings.Default.TopMost;
            TopMostToggle(null, null);

            Top = Properties.Settings.Default.WindowY;
            Left = Properties.Settings.Default.WindowX;

            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
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
                    for (int i = 0; i < data.SelectedBars.Count; ++i)
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
            else if (e.ChangedButton == MouseButton.Middle)
            {
                for (int i = 0; i < data.Hints.Count; ++i)
                {
                    if (button == data.Worlds[i])
                    {
                        data.Hints[i].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));

                        (data.Hints[i].Parent as Grid).ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                        (data.Hints[i].Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.1, GridUnitType.Star);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;

            if (Width < 490)
            {
                HintText.FontSize = 13;
                CollectedBar.Height = 25;
            }
            else
            {
                HintText.FontSize = 16;
                CollectedBar.Height = 30;
            }
        }

        /// 
        /// Handle UI Changes
        /// 
        private void HandleReportValue(Image Hint, int delta)
        {
            int num = 0;

            for (int i = 0; i < data.Numbers.Count; ++i)
            {
                if (Hint.Source == data.Numbers[i])
                {
                    num = i;
                }
            }

            if (delta > 0)
                ++num;
            else
                --num;

            // cap hint value to 20
            if (num > 52)
                num = 52;

            if (num < 0)
                Hint.Source = data.Numbers[0];
            else
                Hint.Source = data.Numbers[num];

            // Format fixing for double digit numbers
            if (num > 10)
            {
                (Hint.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                (Hint.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.15, GridUnitType.Star);
            }
            else
            {
                (Hint.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                (Hint.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.1, GridUnitType.Star);

            }

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num - 1);
        }

        public void SetReportValue(Image Hint, int value)
        {
            if (value > 52)
                value = 52;

            if (value < 0)
                Hint.Source = data.Numbers[0];
            else
                Hint.Source = data.Numbers[value];

            // Format fixing for double digit numbers
            if (value > 10)
            {
                (Hint.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                (Hint.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.15, GridUnitType.Star);
            }
            else
            {
                (Hint.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                (Hint.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.1, GridUnitType.Star);

            }
            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), value - 1);
        }

        public void IncrementCollected()
        {
            ++collected;
            if (collected > 51)
                collected = 51;

            Collected.Source = data.Numbers[collected + 1];
            broadcast.Collected.Source = data.Numbers[collected + 1];

        }

        public void DecrementCollected()
        {
            --collected;
            if (collected < 0)
                collected = 0;

            Collected.Source = data.Numbers[collected + 1];
            broadcast.Collected.Source = data.Numbers[collected + 1];
        }

        public void IncrementTotal()
        {
            ++total;
            if (total > 51)
                total = 51;

            CheckTotal.Source = data.Numbers[total + 1];
            broadcast.CheckTotal.Source = data.Numbers[total + 1];
        }

        public void DecrementTotal()
        {
            --total;
            if (total < 0)
                total = 0;

            CheckTotal.Source = data.Numbers[total + 1];
            broadcast.CheckTotal.Source = data.Numbers[total + 1];
        }

        public void SetHintText(string text)
        {
            HintText.Content = text;
        }

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            broadcast.Width = 500;
            broadcast.Height = 680;
        }
    }
}
