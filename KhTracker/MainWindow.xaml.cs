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

            collectedChecks = new List<ImportantCheck>();
            newChecks = new List<ImportantCheck>();
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
            SimpleToggle(null, null);

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

            if (Width  < 490)
            {
                HintText.FontSize = 13;
                CollectedBar.Height = 25;
                CheckTotal.FontSize = 25;
                Collected.FontSize = 25;
            }
            else
            {
                HintText.FontSize = 16;
                CollectedBar.Height = 30;
                CheckTotal.FontSize = 30;
                Collected.FontSize = 30;
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

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num-1);
        }

        public void SetReportValue(Image Hint, int value)
        {
            if (value > 21)
                value = 21;
            
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

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            broadcast.Width = 500;
            broadcast.Height = 680;
        }
    }
}
