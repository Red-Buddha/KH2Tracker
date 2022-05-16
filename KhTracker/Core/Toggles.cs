using System;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Windows.Data;
using System.IO;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        private void HandleItemToggle(bool toggle, Item button, bool init)
        {
            if (toggle && button.IsEnabled == false)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
                if (!init)
                {
                    IncrementTotal();
                }
            }
            else if (toggle == false && button.IsEnabled == true)
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
                DecrementTotal();

                button.HandleItemReturn();
            }
        }

        private void HandleGhostItemToggle(bool toggle, Item button)
        {
            if (toggle && button.IsEnabled == false)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else if (toggle == false && button.IsEnabled)
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;

                button.HandleItemReturn();
            }
        }

        private void HandleWorldToggle(bool toggle, Button button, UniformGrid grid)
        {
            //this chunk of garbage for using the correct vertical image
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage BarW = data.VerticalBarW;
            {
                if (CustomMode)
                {
                    if (MainWindow.CustomVBarWFound)
                        BarW = data.CustomVerticalBarW;
                }
            }

            if (toggle && button.IsEnabled == false)
            {
                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(1, GridUnitType.Star);
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else if (toggle == false && button.IsEnabled == true)
            {
                if (data.selected == button)
                {
                    data.WorldsData[button.Name].selectedBar.Source = BarW;
                    data.selected = null;
                }

                for (int i = grid.Children.Count - 1; i >= 0; --i)
                {
                    Item item = grid.Children[i] as Item;
                    item.HandleItemReturn();
                }

                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(0, GridUnitType.Star);
                button.IsEnabled = false;
                button.Visibility = Visibility.Collapsed;
            }
        }

        private void PromiseCharmToggle(object sender, RoutedEventArgs e)
        {
            PromiseCharmToggle(PromiseCharmOption.IsChecked);
        }

        private void PromiseCharmToggle(bool toggle)
        {
            Properties.Settings.Default.PromiseCharm = toggle;
            PromiseCharmOption.IsChecked = toggle;
            if (toggle)
            {
                broadcast.PromiseCharm.Visibility = Visibility.Visible;
                broadcast.PromiseCharmCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                broadcast.PromiseCharm.Visibility = Visibility.Hidden;
                broadcast.PromiseCharmCol.Width = new GridLength(0, GridUnitType.Star);
            }
            HandleItemToggle(toggle, PromiseCharm, false);
        }

        private void ReportsToggle(object sender, RoutedEventArgs e)
        {
            ReportsToggle(ReportsOption.IsChecked);
        }

        private void ReportsToggle(bool toggle)
        {
            Properties.Settings.Default.AnsemReports = toggle;
            ReportsOption.IsChecked = toggle;
            if (toggle)
            {
                ReportRow.Height = new GridLength(1.0, GridUnitType.Star);
                broadcast.AnsemReport.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                ReportRow.Height = new GridLength(0, GridUnitType.Star);
                broadcast.AnsemReport.Width = new GridLength(0, GridUnitType.Star);
            }
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(toggle, data.Reports[i], false);
            }
        }

        private void VisitLockToggle(object sender, RoutedEventArgs e)
        {
            VisitLockToggle(VisitLockOption.IsChecked);
        }

        private void VisitLockToggle(bool toggle)
        {
            Properties.Settings.Default.WorldVisitLock = toggle;
            VisitLockOption.IsChecked = toggle;

            if (toggle)
            {
                VisitChecks.Height = new GridLength(1.0, GridUnitType.Star);
                broadcast.VisitsRow.Height = new GridLength(1.75, GridUnitType.Star);

                data.WorldsData["TwilightTown"].visitLocks = 2;
                data.WorldsData["HollowBastion"].visitLocks = 1;
                data.WorldsData["BeastsCastle"].visitLocks = 1;
                data.WorldsData["OlympusColiseum"].visitLocks = 1;
                data.WorldsData["Agrabah"].visitLocks = 1;
                data.WorldsData["LandofDragons"].visitLocks = 1;
                data.WorldsData["PrideLands"].visitLocks = 1;
                data.WorldsData["HalloweenTown"].visitLocks = 1;
                data.WorldsData["PortRoyal"].visitLocks = 1;
                data.WorldsData["SpaceParanoids"].visitLocks = 1;
            }
            else
            {
                VisitChecks.Height = new GridLength(0, GridUnitType.Star);
                broadcast.VisitsRow.Height = new GridLength(0, GridUnitType.Star);

                data.WorldsData["TwilightTown"].visitLocks = 0;
                data.WorldsData["HollowBastion"].visitLocks = 0;
                data.WorldsData["BeastsCastle"].visitLocks = 0;
                data.WorldsData["OlympusColiseum"].visitLocks = 0;
                data.WorldsData["Agrabah"].visitLocks = 0;
                data.WorldsData["LandofDragons"].visitLocks = 0;
                data.WorldsData["PrideLands"].visitLocks = 0;
                data.WorldsData["HalloweenTown"].visitLocks = 0;
                data.WorldsData["PortRoyal"].visitLocks = 0;
                data.WorldsData["SpaceParanoids"].visitLocks = 0;
            }
            for (int i = 0; i < data.VisitLocks.Count; ++i)
            {
                HandleItemToggle(toggle, data.VisitLocks[i], false);
            }

            VisitLockCheck();
        }

        private void AbilitiesToggle(object sender, RoutedEventArgs e)
        {
            AbilitiesToggle(AbilitiesOption.IsChecked);
        }

        private void AbilitiesToggle(bool toggle)
        {
            Properties.Settings.Default.Abilities = toggle;
            AbilitiesOption.IsChecked = toggle;
            if (toggle)
            {
                broadcast.SecondChanceCol.Width = new GridLength(1.0, GridUnitType.Star);
                broadcast.OnceMoreCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                broadcast.SecondChanceCol.Width = new GridLength(0, GridUnitType.Star);
                broadcast.OnceMoreCol.Width = new GridLength(0, GridUnitType.Star);
            }
            HandleItemToggle(toggle, OnceMore, false);
            HandleItemToggle(toggle, SecondChance, false);
        }

        private void TornPagesToggle(object sender, RoutedEventArgs e)
        {
            TornPagesToggle(TornPagesOption.IsChecked);
        }

        private void TornPagesToggle(bool toggle)
        {
            Properties.Settings.Default.TornPages = toggle;
            TornPagesOption.IsChecked = toggle;
            if (toggle)
            {
                broadcast.PoohPage.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                broadcast.PoohPage.Width = new GridLength(0, GridUnitType.Star);
            }
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(toggle, data.TornPages[i], false);
            }
        }

        private void CureToggle(object sender, RoutedEventArgs e)
        {
            CureToggle(CureOption.IsChecked);
        }

        private void CureToggle(bool toggle)
        {
            Properties.Settings.Default.Cure = toggle;
            CureOption.IsChecked = toggle;
            HandleItemToggle(toggle, Cure1, false);
            HandleItemToggle(toggle, Cure2, false);
            HandleItemToggle(toggle, Cure3, false);
        }

        private void FinalFormToggle(object sender, RoutedEventArgs e)
        {
            FinalFormToggle(FinalFormOption.IsChecked);
        }

        private void FinalFormToggle(bool toggle)
        {
            Properties.Settings.Default.FinalForm = toggle;
            FinalFormOption.IsChecked = toggle;
            HandleItemToggle(toggle, Final, false);
        }

        private void SeedHashToggle(object sender, RoutedEventArgs e)
        {
            SeedHashToggle(SeedHashOption.IsChecked);
        }

        private void SeedHashToggle(bool toggle)
        {
            Properties.Settings.Default.SeedHash = toggle;
            SeedHashOption.IsChecked = toggle;

            if (toggle)
                HintText.Content = "";

            if (SeedHashLoaded && toggle)
            {
                HashRow.Height = new GridLength(1.0, GridUnitType.Star);
                SeedHashVisible = true;
            }
            else
            {
                HashRow.Height = new GridLength(0, GridUnitType.Star);
                SeedHashVisible = false;
            }
        }

        private void GhostItemToggle(object sender, RoutedEventArgs e)
        {
            GhostItemToggle(GhostItemOption.IsChecked);
        }

        private void GhostItemToggle(bool toggle)
        {
            Properties.Settings.Default.GhostItem = toggle;
            GhostItemOption.IsChecked = toggle;

            HandleGhostItemToggle(toggle, Ghost_Report1);
            HandleGhostItemToggle(toggle, Ghost_Report2);
            HandleGhostItemToggle(toggle, Ghost_Report3);
            HandleGhostItemToggle(toggle, Ghost_Report4);
            HandleGhostItemToggle(toggle, Ghost_Report5);
            HandleGhostItemToggle(toggle, Ghost_Report6);
            HandleGhostItemToggle(toggle, Ghost_Report7);
            HandleGhostItemToggle(toggle, Ghost_Report8);
            HandleGhostItemToggle(toggle, Ghost_Report9);
            HandleGhostItemToggle(toggle, Ghost_Report10);
            HandleGhostItemToggle(toggle, Ghost_Report11);
            HandleGhostItemToggle(toggle, Ghost_Report12);
            HandleGhostItemToggle(toggle, Ghost_Report13);
            HandleGhostItemToggle(toggle, Ghost_Fire1);
            HandleGhostItemToggle(toggle, Ghost_Fire2);
            HandleGhostItemToggle(toggle, Ghost_Fire3);
            HandleGhostItemToggle(toggle, Ghost_Blizzard1);
            HandleGhostItemToggle(toggle, Ghost_Blizzard2);
            HandleGhostItemToggle(toggle, Ghost_Blizzard3);
            HandleGhostItemToggle(toggle, Ghost_Thunder1);
            HandleGhostItemToggle(toggle, Ghost_Thunder2);
            HandleGhostItemToggle(toggle, Ghost_Thunder3);
            HandleGhostItemToggle(toggle, Ghost_Cure1);
            HandleGhostItemToggle(toggle, Ghost_Cure2);
            HandleGhostItemToggle(toggle, Ghost_Cure3);
            HandleGhostItemToggle(toggle, Ghost_Reflect1);
            HandleGhostItemToggle(toggle, Ghost_Reflect2);
            HandleGhostItemToggle(toggle, Ghost_Reflect3);
            HandleGhostItemToggle(toggle, Ghost_Magnet1);
            HandleGhostItemToggle(toggle, Ghost_Magnet2);
            HandleGhostItemToggle(toggle, Ghost_Magnet3);
            HandleGhostItemToggle(toggle, Ghost_Valor);
            HandleGhostItemToggle(toggle, Ghost_Wisdom);
            HandleGhostItemToggle(toggle, Ghost_Limit);
            HandleGhostItemToggle(toggle, Ghost_Master);
            HandleGhostItemToggle(toggle, Ghost_Final);
            HandleGhostItemToggle(toggle, Ghost_OnceMore);
            HandleGhostItemToggle(toggle, Ghost_SecondChance);
            HandleGhostItemToggle(toggle, Ghost_TornPage1);
            HandleGhostItemToggle(toggle, Ghost_TornPage2);
            HandleGhostItemToggle(toggle, Ghost_TornPage3);
            HandleGhostItemToggle(toggle, Ghost_TornPage4);
            HandleGhostItemToggle(toggle, Ghost_TornPage5);
            HandleGhostItemToggle(toggle, Ghost_Baseball);
            HandleGhostItemToggle(toggle, Ghost_Lamp);
            HandleGhostItemToggle(toggle, Ghost_Ukulele);
            HandleGhostItemToggle(toggle, Ghost_Feather);
            HandleGhostItemToggle(toggle, Ghost_Connection);
            HandleGhostItemToggle(toggle, Ghost_Nonexistence);
            HandleGhostItemToggle(toggle, Ghost_Peace);
            HandleGhostItemToggle(toggle, Ghost_PromiseCharm);
            //HandleGhostItemToggle(toggle, Ghost_HadesCup);

            WorldGrid.Ghost_Fire = 0;
            WorldGrid.Ghost_Blizzard = 0;
            WorldGrid.Ghost_Thunder = 0;
            WorldGrid.Ghost_Cure = 0;
            WorldGrid.Ghost_Reflect = 0;
            WorldGrid.Ghost_Magnet = 0;
            WorldGrid.Ghost_Pages = 0;
            WorldGrid.Ghost_Fire_obtained = 0;
            WorldGrid.Ghost_Blizzard_obtained = 0;
            WorldGrid.Ghost_Thunder_obtained = 0;
            WorldGrid.Ghost_Cure_obtained = 0;
            WorldGrid.Ghost_Reflect_obtained = 0;
            WorldGrid.Ghost_Magnet_obtained = 0;
            WorldGrid.Ghost_Pages_obtained = 0;
        }

        private void GhostMathToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.GhostMath = GhostMathOption.IsChecked;
        }

        private void ShowCheckCountToggle(object sender, RoutedEventArgs e)
        {
            ShowCheckCountToggle(CheckCountOption.IsChecked);
        }

        private void ShowCheckCountToggle(bool toggle)
        {
            Console.WriteLine("ShowCheckCountToggle pressed");

            Properties.Settings.Default.CheckCount = toggle;
            CheckCountOption.IsChecked = toggle;

            //if check count should be shown and replace the score IN POINTS MODE
            if (toggle && (Score1.IsVisible || Score10.IsVisible || Score100.IsVisible) && data.mode == Mode.DAHints)
            {
                Score1.Visibility = Visibility.Hidden;
                Score10.Visibility = Visibility.Hidden;
                Score100.Visibility = Visibility.Hidden;
                Collected.Visibility = Visibility.Visible;
                CollectedBar.Visibility = Visibility.Visible;
                CheckTotal.Visibility = Visibility.Visible;

                broadcast.Score1.Visibility = Visibility.Hidden;
                broadcast.Score10.Visibility = Visibility.Hidden;
                broadcast.Score100.Visibility = Visibility.Hidden;
                broadcast.Collected.Visibility = Visibility.Visible;
                broadcast.CollectedBar.Visibility = Visibility.Visible;
                broadcast.CheckTotal.Visibility = Visibility.Visible;
            }
            //if points should show and replace check count IN POINTS MODE
            else if (!toggle && (Collected.IsVisible || CollectedBar.IsVisible || CheckTotal.IsVisible) && data.mode == Mode.DAHints)
            {
                Score1.Visibility = Visibility.Visible;
                Score10.Visibility = Visibility.Visible;
                Score100.Visibility = Visibility.Visible;
                Collected.Visibility = Visibility.Hidden;
                CollectedBar.Visibility = Visibility.Hidden;
                CheckTotal.Visibility = Visibility.Hidden;

                broadcast.Score1.Visibility = Visibility.Visible;
                broadcast.Score10.Visibility = Visibility.Visible;
                broadcast.Score100.Visibility = Visibility.Visible;
                broadcast.Collected.Visibility = Visibility.Hidden;
                broadcast.CollectedBar.Visibility = Visibility.Hidden;
                broadcast.CheckTotal.Visibility = Visibility.Hidden;
            }
            //chek coun't should always be on in non points mode
            else if (data.mode != Mode.DAHints)
            {
                Collected.Visibility = Visibility.Visible;
                CollectedBar.Visibility = Visibility.Visible;
                CheckTotal.Visibility = Visibility.Visible;
                broadcast.Collected.Visibility = Visibility.Visible;
                broadcast.CollectedBar.Visibility = Visibility.Visible;
                broadcast.CheckTotal.Visibility = Visibility.Visible;
            }
        }

        private void WorldProgressToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WorldProgress = WorldProgressOption.IsChecked;
            if (WorldProgressOption.IsChecked)
            {
                broadcast.ToggleProgression(true);

                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Visible;

                    data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1.5, GridUnitType.Star);
                    data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(3.3, GridUnitType.Star);

                    Grid grid = data.WorldsData[key].world.Parent as Grid;
                    grid.ColumnDefinitions[0].Width = new GridLength(3.5, GridUnitType.Star);
                    grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                    grid.ColumnDefinitions[2].Width = new GridLength(2, GridUnitType.Star);
                    Grid.SetColumnSpan(data.WorldsData[key].world, 2);
                }
            }
            else
            {
                broadcast.ToggleProgression(false);

                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Hidden;

                    data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(4, GridUnitType.Star);

                    Grid grid = data.WorldsData[key].world.Parent as Grid;
                    grid.ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Star);
                    grid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                    grid.ColumnDefinitions[2].Width = new GridLength(4, GridUnitType.Star);
                    Grid.SetColumnSpan(data.WorldsData[key].world, 3);
                }
            }
        }

        private void DragDropToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DragDrop = DragAndDropOption.IsChecked;
            data.dragDrop = DragAndDropOption.IsChecked;

            foreach (Item item in data.Items)
            {
                if (item.Parent == ItemPool)
                {
                    if (data.dragDrop == false)
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;

                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                        item.MouseUp += item.Item_MouseUp;
                    }
                    else
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;
                        item.MouseMove += item.Item_MouseMove;

                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                    }
                }
            }
        }

        private void TopMostToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopMost = TopMostOption.IsChecked;
            Topmost = TopMostOption.IsChecked;
            broadcast.Topmost = TopMostOption.IsChecked;
        }

        private void BroadcastStartupToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BroadcastStartup = BroadcastStartupOption.IsChecked;
            if (BroadcastStartupOption.IsChecked)
                broadcast.Show();
        }

        private void FormsGrowthToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FormsGrowth = FormsGrowthOption.IsChecked;
            if (FormsGrowthOption.IsChecked && aTimer != null)
                FormRow.Height = new GridLength(0.65, GridUnitType.Star);
            else
                FormRow.Height = new GridLength(0, GridUnitType.Star);
        }

        private void BroadcastGrowthToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BroadcastGrowth = BroadcastGrowthOption.IsChecked;
            if (BroadcastGrowthOption.IsChecked == false && broadcast.GrowthAbilityRow.Height.Value >= 0.01)
                broadcast.GrowthAbilityRow.Height = new GridLength(0, GridUnitType.Star);
            else if (BroadcastGrowthOption.IsChecked && aTimer != null)
                broadcast.GrowthAbilityRow.Height = new GridLength(1, GridUnitType.Star);
        }

        private void BroadcastStatsToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BroadcastStats = BroadcastStatsOption.IsChecked;
            if (BroadcastStatsOption.IsChecked == false && broadcast.StatsRow.Height.Value >= 0.01)
                broadcast.StatsRow.Height = new GridLength(0, GridUnitType.Star);
            else if (BroadcastStatsOption.IsChecked && aTimer != null)
                broadcast.StatsRow.Height = new GridLength(1, GridUnitType.Star);
        }

        private void AutoDetectToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoDetect = AutoDetectOption.IsChecked;

            if (AutoDetectOption.IsChecked)
            {
                Console.WriteLine("Auto Detect enabled?");
                Connect.Source = data.AD_Connect;
                SetAutoDetectTimer();
                Connect.Visibility = Visibility.Visible;
            }
            else
                Connect.Visibility = Visibility.Hidden;
        }

        //level check toggles

        private void NextLevelCheckToggle(object sender, RoutedEventArgs e)
        {
            NextLevelCheckToggle(NextLevelCheckOption.IsChecked);
        }
        
        private void NextLevelCheckToggle(bool toggle)
        {
            Properties.Settings.Default.NextLevelCheck = toggle;
            NextLevelCheckOption.IsChecked = toggle;

            NextLevelDisplay();
        }

        private void NextLevelDisplay()
        {
            bool Visible = false;

            if (NextLevelCheckOption.IsChecked)
            {
                Visible = true;

                //HintTextParent.Margin = new Thickness(45, 0, 0, 0);
                //HintText.Margin = new Thickness(30, 0, 0, 0);
            }
            else
            {
                HintTextParent.Margin = new Thickness(0, 0, 0, 0);
                HintText.Margin = new Thickness(0, 0, 0, 0);
            }

            if (SoraLevel01Option.IsChecked)
            {
                if (memory != null && stats != null)
                {
                    try
                    {
                        stats.SetMaxLevelCheck(1);
                        stats.SetNextLevelCheck(stats.Level);
                    }
                    catch
                    {
                        Console.WriteLine("Tried to edit while loading");
                    }
                }
            }

            if (SoraLevel50Option.IsChecked)
            {
                if (memory != null && stats != null)
                {
                    try
                    {
                        stats.SetMaxLevelCheck(50);
                        stats.SetNextLevelCheck(stats.Level);
                    }
                    catch
                    {
                        Console.WriteLine("Tried to edit while loading");
                    }
                }
            }

            if (SoraLevel99Option.IsChecked)
            {
                if (memory != null && stats != null)
                {
                    try
                    {
                        stats.SetMaxLevelCheck(99);
                        stats.SetNextLevelCheck(stats.Level);
                    }
                    catch
                    {
                        Console.WriteLine("Tried to edit while loading");
                    }
                }
            }

            if (Visible && memory != null)
            {
                NextlevelValue.Width = new GridLength(2.5, GridUnitType.Star);
            }
            else
            {
                NextlevelValue.Width = new GridLength(0, GridUnitType.Star);
            }
        }

        //World toggles
        private void SoraHeartToggle(object sender, RoutedEventArgs e)
        {
            SoraHeartToggle(SoraHeartOption.IsChecked);
        }

        private void SoraHeartToggle(bool toggle)
        {
            Properties.Settings.Default.SoraHeart = toggle;
            SoraHeartOption.IsChecked = toggle;
            HandleWorldToggle(toggle, SorasHeart, SorasHeartGrid);
            if (toggle)
                broadcast.HeartCol.Width = new GridLength(1.0, GridUnitType.Star);
            else
                broadcast.HeartCol.Width = new GridLength(0, GridUnitType.Star);
        }

        private void SimulatedToggle(object sender, RoutedEventArgs e)
        {
            SimulatedToggle(SimulatedOption.IsChecked);
        }

        private void SimulatedToggle(bool toggle)
        {
            Properties.Settings.Default.Simulated = toggle;
            SimulatedOption.IsChecked = toggle;
            HandleWorldToggle(toggle, SimulatedTwilightTown, SimulatedTwilightTownGrid);
            if (toggle)
                broadcast.STTColumn.Width = new GridLength(1.0, GridUnitType.Star);
            else
                broadcast.STTColumn.Width = new GridLength(0, GridUnitType.Star);
        }

        private void HundredAcreWoodToggle(object sender, RoutedEventArgs e)
        {
            HundredAcreWoodToggle(HundredAcreWoodOption.IsChecked);
        }

        private void HundredAcreWoodToggle(bool toggle)
        {
            Properties.Settings.Default.HundredAcre = toggle;
            HundredAcreWoodOption.IsChecked = toggle;
            HandleWorldToggle(toggle, HundredAcreWood, HundredAcreWoodGrid);
            if (toggle)
                broadcast.HAWColumn.Width = new GridLength(1.0, GridUnitType.Star);
            else
                broadcast.HAWColumn.Width = new GridLength(0, GridUnitType.Star);
        }

        private void AtlanticaToggle(object sender, RoutedEventArgs e)
        {
            AtlanticaToggle(AtlanticaOption.IsChecked);
        }

        private void AtlanticaToggle(bool toggle)
        {
            Properties.Settings.Default.Atlantica = toggle;
            AtlanticaOption.IsChecked = toggle;
            HandleWorldToggle(toggle, Atlantica, AtlanticaGrid);
            if (toggle)
                broadcast.ATColumn.Width = new GridLength(1.0, GridUnitType.Star);
            else
                broadcast.ATColumn.Width = new GridLength(0, GridUnitType.Star);
        }

        private void CavernToggle(object sender, RoutedEventArgs e)
        {
            CavernToggle(CavernOption.IsChecked);
        }

        private void CavernToggle(bool toggle)
        {
            Properties.Settings.Default.Cavern = toggle;
            CavernOption.IsChecked = toggle;

            CustomWorldCheck();
        }

        private void TimelessToggle(object sender, RoutedEventArgs e)
        {
            TimelessToggle(TimelessOption.IsChecked);
        }

        private void TimelessToggle(bool toggle)
        {
            Properties.Settings.Default.Timeless = toggle;
            TimelessOption.IsChecked = toggle;

            CustomWorldCheck();
        }

        private void OCCupsToggle(object sender, RoutedEventArgs e)
        {
            OCCupsToggle(OCCupsOption.IsChecked);
        }

        private void OCCupsToggle(bool toggle)
        {
            Properties.Settings.Default.OCCups = toggle;
            OCCupsOption.IsChecked = toggle;

            CustomWorldCheck();
        }

        private void SoraLevel01Toggle(object sender, RoutedEventArgs e)
        {
            SoraLevel01Toggle(SoraLevel01Option.IsChecked);
        }

        private void SoraLevel01Toggle(bool toggle)
        {
            //mimic radio button
            if (SoraLevel01Option.IsChecked == false)
            {
                SoraLevel01Option.IsChecked = true;
                //return;
            }
            SoraLevel50Option.IsChecked = false;
            SoraLevel99Option.IsChecked = false;
            Properties.Settings.Default.WorldLevel50 = SoraLevel50Option.IsChecked;
            Properties.Settings.Default.WorldLevel99 = SoraLevel99Option.IsChecked;
            Properties.Settings.Default.WorldLevel1 = toggle;

            CustomWorldCheck();
            NextLevelDisplay();
        }

        private void SoraLevel50Toggle(object sender, RoutedEventArgs e)
        {
            SoraLevel50Toggle(SoraLevel50Option.IsChecked);
        }

        private void SoraLevel50Toggle(bool toggle)
        {
            //mimic radio button
            if (SoraLevel50Option.IsChecked == false)
            {
                SoraLevel50Option.IsChecked = true;
                //return;
            }
            SoraLevel01Option.IsChecked = false;
            SoraLevel99Option.IsChecked = false;
            Properties.Settings.Default.WorldLevel1 = SoraLevel50Option.IsChecked;
            Properties.Settings.Default.WorldLevel99 = SoraLevel99Option.IsChecked;
            Properties.Settings.Default.WorldLevel50 = toggle;

            CustomWorldCheck();
            NextLevelDisplay();
        }

        private void SoraLevel99Toggle(object sender, RoutedEventArgs e)
        {
            SoraLevel99Toggle(SoraLevel99Option.IsChecked);
        }

        private void SoraLevel99Toggle(bool toggle)
        {
            //mimic radio button
            if (SoraLevel99Option.IsChecked == false)
            {
                SoraLevel99Option.IsChecked = true;
                //return;
            }
            SoraLevel50Option.IsChecked = false;
            SoraLevel01Option.IsChecked = false;
            Properties.Settings.Default.WorldLevel50 = SoraLevel50Option.IsChecked;
            Properties.Settings.Default.WorldLevel1 = SoraLevel01Option.IsChecked;
            Properties.Settings.Default.WorldLevel99 = toggle;

            CustomWorldCheck();
            NextLevelDisplay();
        }

        //icon toggles
        private void MinCheckToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MinCheckOption.IsChecked == false)
            {
                MinCheckOption.IsChecked = true;
                return;
            }

            OldCheckOption.IsChecked = false;
            Properties.Settings.Default.MinCheck = MinCheckOption.IsChecked;
            Properties.Settings.Default.OldCheck = OldCheckOption.IsChecked;

            if (MinCheckOption.IsChecked)
            {
                ReportNumCheck();

                Fire1.SetResourceReference(ContentProperty, "Min-Fire");
                Fire2.SetResourceReference(ContentProperty, "Min-Fire");
                Fire3.SetResourceReference(ContentProperty, "Min-Fire");
                Blizzard1.SetResourceReference(ContentProperty, "Min-Blizzard");
                Blizzard2.SetResourceReference(ContentProperty, "Min-Blizzard");
                Blizzard3.SetResourceReference(ContentProperty, "Min-Blizzard");
                Thunder1.SetResourceReference(ContentProperty, "Min-Thunder");
                Thunder2.SetResourceReference(ContentProperty, "Min-Thunder");
                Thunder3.SetResourceReference(ContentProperty, "Min-Thunder");
                Cure1.SetResourceReference(ContentProperty, "Min-Cure");
                Cure2.SetResourceReference(ContentProperty, "Min-Cure");
                Cure3.SetResourceReference(ContentProperty, "Min-Cure");
                Reflect1.SetResourceReference(ContentProperty, "Min-Reflect");
                Reflect2.SetResourceReference(ContentProperty, "Min-Reflect");
                Reflect3.SetResourceReference(ContentProperty, "Min-Reflect");
                Magnet1.SetResourceReference(ContentProperty, "Min-Magnet");
                Magnet2.SetResourceReference(ContentProperty, "Min-Magnet");
                Magnet3.SetResourceReference(ContentProperty, "Min-Magnet");
                Valor.SetResourceReference(ContentProperty, "Min-Valor");
                ValorM.SetResourceReference(ContentProperty, "Min-Valor");
                Wisdom.SetResourceReference(ContentProperty, "Min-Wisdom");
                WisdomM.SetResourceReference(ContentProperty, "Min-Wisdom");
                Limit.SetResourceReference(ContentProperty, "Min-Limit");
                LimitM.SetResourceReference(ContentProperty, "Min-Limit");
                Master.SetResourceReference(ContentProperty, "Min-Master");
                MasterM.SetResourceReference(ContentProperty, "Min-Master");
                Final.SetResourceReference(ContentProperty, "Min-Final");
                FinalM.SetResourceReference(ContentProperty, "Min-Final");
                TornPage1.SetResourceReference(ContentProperty, "Min-TornPage");
                TornPage2.SetResourceReference(ContentProperty, "Min-TornPage");
                TornPage3.SetResourceReference(ContentProperty, "Min-TornPage");
                TornPage4.SetResourceReference(ContentProperty, "Min-TornPage");
                TornPage5.SetResourceReference(ContentProperty, "Min-TornPage");
                OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                HighJump.SetResourceReference(ContentProperty, "Min-HighJump");
                QuickRun.SetResourceReference(ContentProperty, "Min-QuickRun");
                DodgeRoll.SetResourceReference(ContentProperty, "Min-DodgeRoll");
                AerialDodge.SetResourceReference(ContentProperty, "Min-AerialDodge");
                Glide.SetResourceReference(ContentProperty, "Min-Glide");
                Lamp.SetResourceReference(ContentProperty, "Min-Genie");
                Ukulele.SetResourceReference(ContentProperty, "Min-Stitch");
                Baseball.SetResourceReference(ContentProperty, "Min-ChickenLittle");
                Feather.SetResourceReference(ContentProperty, "Min-PeterPan");
                Nonexistence.SetResourceReference(ContentProperty, "Min-ProofOfNon");
                Connection.SetResourceReference(ContentProperty, "Min-ProofOfCon");
                Peace.SetResourceReference(ContentProperty, "Min-ProofOfPea");
                PromiseCharm.SetResourceReference(ContentProperty, "Min-PromiseCharm");
                LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
                StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
                MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
                DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");

                broadcast.Report.SetResourceReference(ContentProperty, "Min-AnsemReport");
                broadcast.TornPage.SetResourceReference(ContentProperty, "Min-TornPages");
                broadcast.Chest.SetResourceReference(ContentProperty, "Min-Chest");
                broadcast.LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
                broadcast.StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
                broadcast.MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
                broadcast.DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");
                broadcast.HighJump.SetResourceReference(ContentProperty, "Min-HighJump");
                broadcast.QuickRun.SetResourceReference(ContentProperty, "Min-QuickRun");
                broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Min-DodgeRoll");
                broadcast.AerialDodge.SetResourceReference(ContentProperty, "Min-AerialDodge");
                broadcast.Glide.SetResourceReference(ContentProperty, "Min-Glide");
                broadcast.OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                broadcast.SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                broadcast.Peace.SetResourceReference(ContentProperty, "Min-ProofOfPea");
                broadcast.Nonexistence.SetResourceReference(ContentProperty, "Min-ProofOfNon");
                broadcast.Connection.SetResourceReference(ContentProperty, "Min-ProofOfCon");
                broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Min-PromiseCharm");
                broadcast.Fire.SetResourceReference(ContentProperty, "Min-Fire");
                broadcast.Blizzard.SetResourceReference(ContentProperty, "Min-Blizzard");
                broadcast.Thunder.SetResourceReference(ContentProperty, "Min-Thunder");
                broadcast.Cure.SetResourceReference(ContentProperty, "Min-Cure");
                broadcast.Reflect.SetResourceReference(ContentProperty, "Min-Reflect");
                broadcast.Magnet.SetResourceReference(ContentProperty, "Min-Magnet");
                broadcast.Valor.SetResourceReference(ContentProperty, "Min-Valor");
                broadcast.Wisdom.SetResourceReference(ContentProperty, "Min-Wisdom");
                broadcast.Limit.SetResourceReference(ContentProperty, "Min-Limit");
                broadcast.Master.SetResourceReference(ContentProperty, "Min-Master");
                broadcast.Final.SetResourceReference(ContentProperty, "Min-Final");
                broadcast.Baseball.SetResourceReference(ContentProperty, "Min-ChickenLittle");
                broadcast.Lamp.SetResourceReference(ContentProperty, "Min-Genie");
                broadcast.Ukulele.SetResourceReference(ContentProperty, "Min-Stitch");
                broadcast.Feather.SetResourceReference(ContentProperty, "Min-PeterPan");

                //ghost icons
                Ghost_Fire1.SetResourceReference(ContentProperty, "Min-Fire");
                Ghost_Fire2.SetResourceReference(ContentProperty, "Min-Fire");
                Ghost_Fire3.SetResourceReference(ContentProperty, "Min-Fire");
                Ghost_Blizzard1.SetResourceReference(ContentProperty, "Min-Blizzard");
                Ghost_Blizzard2.SetResourceReference(ContentProperty, "Min-Blizzard");
                Ghost_Blizzard3.SetResourceReference(ContentProperty, "Min-Blizzard");
                Ghost_Thunder1.SetResourceReference(ContentProperty, "Min-Thunder");
                Ghost_Thunder2.SetResourceReference(ContentProperty, "Min-Thunder");
                Ghost_Thunder3.SetResourceReference(ContentProperty, "Min-Thunder");
                Ghost_Cure1.SetResourceReference(ContentProperty, "Min-Cure");
                Ghost_Cure2.SetResourceReference(ContentProperty, "Min-Cure");
                Ghost_Cure3.SetResourceReference(ContentProperty, "Min-Cure");
                Ghost_Reflect1.SetResourceReference(ContentProperty, "Min-Reflect");
                Ghost_Reflect2.SetResourceReference(ContentProperty, "Min-Reflect");
                Ghost_Reflect3.SetResourceReference(ContentProperty, "Min-Reflect");
                Ghost_Magnet1.SetResourceReference(ContentProperty, "Min-Magnet");
                Ghost_Magnet2.SetResourceReference(ContentProperty, "Min-Magnet");
                Ghost_Magnet3.SetResourceReference(ContentProperty, "Min-Magnet");
                Ghost_Valor.SetResourceReference(ContentProperty, "Min-Valor");
                Ghost_Wisdom.SetResourceReference(ContentProperty, "Min-Wisdom");
                Ghost_Limit.SetResourceReference(ContentProperty, "Min-Limit");
                Ghost_Master.SetResourceReference(ContentProperty, "Min-Master");
                Ghost_Final.SetResourceReference(ContentProperty, "Min-Final");
                Ghost_TornPage1.SetResourceReference(ContentProperty, "Min-TornPage");
                Ghost_TornPage2.SetResourceReference(ContentProperty, "Min-TornPage");
                Ghost_TornPage3.SetResourceReference(ContentProperty, "Min-TornPage");
                Ghost_TornPage4.SetResourceReference(ContentProperty, "Min-TornPage");
                Ghost_TornPage5.SetResourceReference(ContentProperty, "Min-TornPage");
                Ghost_OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                Ghost_SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                Ghost_Lamp.SetResourceReference(ContentProperty, "Min-Genie");
                Ghost_Ukulele.SetResourceReference(ContentProperty, "Min-Stitch");
                Ghost_Baseball.SetResourceReference(ContentProperty, "Min-ChickenLittle");
                Ghost_Feather.SetResourceReference(ContentProperty, "Min-PeterPan");
                Ghost_Nonexistence.SetResourceReference(ContentProperty, "Min-ProofOfNon");
                Ghost_Connection.SetResourceReference(ContentProperty, "Min-ProofOfCon");
                Ghost_Peace.SetResourceReference(ContentProperty, "Min-ProofOfPea");
                Ghost_PromiseCharm.SetResourceReference(ContentProperty, "Min-PromiseCharm");


                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[1].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[2].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Lamp.Parent).RowDefinitions[1].Height = new GridLength(4.4, GridUnitType.Star);
            }

            CustomChecksCheck();
            ReloadBindings();
        }

        private void OldCheckToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (OldCheckOption.IsChecked == false)
            {
                OldCheckOption.IsChecked = true;
                return;
            }

            MinCheckOption.IsChecked = false;
            Properties.Settings.Default.MinCheck = MinCheckOption.IsChecked;
            Properties.Settings.Default.OldCheck = OldCheckOption.IsChecked;

            if (OldCheckOption.IsChecked)
            {
                ReportNumCheck();

                Fire1.SetResourceReference(ContentProperty, "Old-Fire");
                Fire2.SetResourceReference(ContentProperty, "Old-Fire");
                Fire3.SetResourceReference(ContentProperty, "Old-Fire");
                Blizzard1.SetResourceReference(ContentProperty, "Old-Blizzard");
                Blizzard2.SetResourceReference(ContentProperty, "Old-Blizzard");
                Blizzard3.SetResourceReference(ContentProperty, "Old-Blizzard");
                Thunder1.SetResourceReference(ContentProperty, "Old-Thunder");
                Thunder2.SetResourceReference(ContentProperty, "Old-Thunder");
                Thunder3.SetResourceReference(ContentProperty, "Old-Thunder");
                Cure1.SetResourceReference(ContentProperty, "Old-Cure");
                Cure2.SetResourceReference(ContentProperty, "Old-Cure");
                Cure3.SetResourceReference(ContentProperty, "Old-Cure");
                Reflect1.SetResourceReference(ContentProperty, "Old-Reflect");
                Reflect2.SetResourceReference(ContentProperty, "Old-Reflect");
                Reflect3.SetResourceReference(ContentProperty, "Old-Reflect");
                Magnet1.SetResourceReference(ContentProperty, "Old-Magnet");
                Magnet2.SetResourceReference(ContentProperty, "Old-Magnet");
                Magnet3.SetResourceReference(ContentProperty, "Old-Magnet");
                Valor.SetResourceReference(ContentProperty, "Old-Valor");
                ValorM.SetResourceReference(ContentProperty, "Old-Valor");
                Wisdom.SetResourceReference(ContentProperty, "Old-Wisdom");
                WisdomM.SetResourceReference(ContentProperty, "Old-Wisdom");
                Limit.SetResourceReference(ContentProperty, "Old-Limit");
                LimitM.SetResourceReference(ContentProperty, "Old-Limit");
                Master.SetResourceReference(ContentProperty, "Old-Master");
                MasterM.SetResourceReference(ContentProperty, "Old-Master");
                Final.SetResourceReference(ContentProperty, "Old-Final");
                FinalM.SetResourceReference(ContentProperty, "Old-Final");
                TornPage1.SetResourceReference(ContentProperty, "Old-TornPage");
                TornPage2.SetResourceReference(ContentProperty, "Old-TornPage");
                TornPage3.SetResourceReference(ContentProperty, "Old-TornPage");
                TornPage4.SetResourceReference(ContentProperty, "Old-TornPage");
                TornPage5.SetResourceReference(ContentProperty, "Old-TornPage");
                OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                HighJump.SetResourceReference(ContentProperty, "Min-HighJump");
                QuickRun.SetResourceReference(ContentProperty, "Min-QuickRun");
                DodgeRoll.SetResourceReference(ContentProperty, "Min-DodgeRoll");
                AerialDodge.SetResourceReference(ContentProperty, "Min-AerialDodge");
                Glide.SetResourceReference(ContentProperty, "Min-Glide");
                Lamp.SetResourceReference(ContentProperty, "Old-Genie");
                Ukulele.SetResourceReference(ContentProperty, "Old-Stitch");
                Baseball.SetResourceReference(ContentProperty, "Old-ChickenLittle");
                Feather.SetResourceReference(ContentProperty, "Old-PeterPan");
                Nonexistence.SetResourceReference(ContentProperty, "Old-ProofOfNon");
                Connection.SetResourceReference(ContentProperty, "Old-ProofOfCon");
                Peace.SetResourceReference(ContentProperty, "Old-ProofOfPea");
                PromiseCharm.SetResourceReference(ContentProperty, "Old-PromiseCharm");
                LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
                StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
                MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
                DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");

                broadcast.Report.SetResourceReference(ContentProperty, "Old-AnsemReport");
                broadcast.TornPage.SetResourceReference(ContentProperty, "Old-TornPages");
                broadcast.Chest.SetResourceReference(ContentProperty, "Old-Chest");
                broadcast.LevelIcon.SetResourceReference(ContentProperty, "LevelIcon");
                broadcast.StrengthIcon.SetResourceReference(ContentProperty, "StrengthIcon");
                broadcast.MagicIcon.SetResourceReference(ContentProperty, "MagicIcon");
                broadcast.DefenseIcon.SetResourceReference(ContentProperty, "DefenseIcon");
                broadcast.HighJump.SetResourceReference(ContentProperty, "Min-HighJump");
                broadcast.QuickRun.SetResourceReference(ContentProperty, "Min-QuickRun");
                broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Min-DodgeRoll");
                broadcast.AerialDodge.SetResourceReference(ContentProperty, "Min-AerialDodge");
                broadcast.Glide.SetResourceReference(ContentProperty, "Min-Glide");
                broadcast.OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                broadcast.SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                broadcast.Peace.SetResourceReference(ContentProperty, "Old-ProofOfPea");
                broadcast.Nonexistence.SetResourceReference(ContentProperty, "Old-ProofOfNon");
                broadcast.Connection.SetResourceReference(ContentProperty, "Old-ProofOfCon");
                broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Old-PromiseCharm");
                broadcast.Fire.SetResourceReference(ContentProperty, "Old-Fire");
                broadcast.Blizzard.SetResourceReference(ContentProperty, "Old-Blizzard");
                broadcast.Thunder.SetResourceReference(ContentProperty, "Old-Thunder");
                broadcast.Cure.SetResourceReference(ContentProperty, "Old-Cure");
                broadcast.Reflect.SetResourceReference(ContentProperty, "Old-Reflect");
                broadcast.Magnet.SetResourceReference(ContentProperty, "Old-Magnet");
                broadcast.Valor.SetResourceReference(ContentProperty, "Old-Valor");
                broadcast.Wisdom.SetResourceReference(ContentProperty, "Old-Wisdom");
                broadcast.Limit.SetResourceReference(ContentProperty, "Old-Limit");
                broadcast.Master.SetResourceReference(ContentProperty, "Old-Master");
                broadcast.Final.SetResourceReference(ContentProperty, "Old-Final");
                broadcast.Baseball.SetResourceReference(ContentProperty, "Old-ChickenLittle");
                broadcast.Lamp.SetResourceReference(ContentProperty, "Old-Genie");
                broadcast.Ukulele.SetResourceReference(ContentProperty, "Old-Stitch");
                broadcast.Feather.SetResourceReference(ContentProperty, "Old-PeterPan");

                Ghost_Fire1.SetResourceReference(ContentProperty, "Old-Fire");
                Ghost_Fire2.SetResourceReference(ContentProperty, "Old-Fire");
                Ghost_Fire3.SetResourceReference(ContentProperty, "Old-Fire");
                Ghost_Blizzard1.SetResourceReference(ContentProperty, "Old-Blizzard");
                Ghost_Blizzard2.SetResourceReference(ContentProperty, "Old-Blizzard");
                Ghost_Blizzard3.SetResourceReference(ContentProperty, "Old-Blizzard");
                Ghost_Thunder1.SetResourceReference(ContentProperty, "Old-Thunder");
                Ghost_Thunder2.SetResourceReference(ContentProperty, "Old-Thunder");
                Ghost_Thunder3.SetResourceReference(ContentProperty, "Old-Thunder");
                Ghost_Cure1.SetResourceReference(ContentProperty, "Old-Cure");
                Ghost_Cure2.SetResourceReference(ContentProperty, "Old-Cure");
                Ghost_Cure3.SetResourceReference(ContentProperty, "Old-Cure");
                Ghost_Reflect1.SetResourceReference(ContentProperty, "Old-Reflect");
                Ghost_Reflect2.SetResourceReference(ContentProperty, "Old-Reflect");
                Ghost_Reflect3.SetResourceReference(ContentProperty, "Old-Reflect");
                Ghost_Magnet1.SetResourceReference(ContentProperty, "Old-Magnet");
                Ghost_Magnet2.SetResourceReference(ContentProperty, "Old-Magnet");
                Ghost_Magnet3.SetResourceReference(ContentProperty, "Old-Magnet");
                Ghost_Valor.SetResourceReference(ContentProperty, "Old-Valor");
                Ghost_Wisdom.SetResourceReference(ContentProperty, "Old-Wisdom");
                Ghost_Limit.SetResourceReference(ContentProperty, "Old-Limit");
                Ghost_Master.SetResourceReference(ContentProperty, "Old-Master");
                Ghost_Final.SetResourceReference(ContentProperty, "Old-Final");
                Ghost_TornPage1.SetResourceReference(ContentProperty, "Old-TornPage");
                Ghost_TornPage2.SetResourceReference(ContentProperty, "Old-TornPage");
                Ghost_TornPage3.SetResourceReference(ContentProperty, "Old-TornPage");
                Ghost_TornPage4.SetResourceReference(ContentProperty, "Old-TornPage");
                Ghost_TornPage5.SetResourceReference(ContentProperty, "Old-TornPage");
                Ghost_OnceMore.SetResourceReference(ContentProperty, "Min-OnceMore");
                Ghost_SecondChance.SetResourceReference(ContentProperty, "Min-SecondChance");
                Ghost_Lamp.SetResourceReference(ContentProperty, "Old-Genie");
                Ghost_Ukulele.SetResourceReference(ContentProperty, "Old-Stitch");
                Ghost_Baseball.SetResourceReference(ContentProperty, "Old-ChickenLittle");
                Ghost_Feather.SetResourceReference(ContentProperty, "Old-PeterPan");
                Ghost_Nonexistence.SetResourceReference(ContentProperty, "Old-ProofOfNon");
                Ghost_Connection.SetResourceReference(ContentProperty, "Old-ProofOfCon");
                Ghost_Peace.SetResourceReference(ContentProperty, "Old-ProofOfPea");
                Ghost_PromiseCharm.SetResourceReference(ContentProperty, "Old-PromiseCharm");

                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[1].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[2].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Lamp.Parent).RowDefinitions[1].Height = new GridLength(4.4, GridUnitType.Star);
            }

            CustomChecksCheck();
            ReloadBindings();
        }

        private void MinWorldToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MinWorldOption.IsChecked == false)
            {
                MinWorldOption.IsChecked = true;
                return;
            }

            OldWorldOption.IsChecked = false;
            Properties.Settings.Default.MinWorld = MinWorldOption.IsChecked;
            Properties.Settings.Default.OldWorld = OldWorldOption.IsChecked;

            if (MinWorldOption.IsChecked)
            {
                //main window worlds
                SorasHeart.SetResourceReference(ContentProperty, "Min-SoraHeartImage");
                SimulatedTwilightTown.SetResourceReference(ContentProperty, "Min-SimulatedImage");
                OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                LandofDragons.SetResourceReference(ContentProperty, "Min-LandofDragonsImage");
                PrideLands.SetResourceReference(ContentProperty, "Min-PrideLandsImage");
                HalloweenTown.SetResourceReference(ContentProperty, "Min-HalloweenTownImage");
                SpaceParanoids.SetResourceReference(ContentProperty, "Min-SpaceParanoidsImage");
                GoA.SetResourceReference(ContentProperty, "Min-GardenofAssemblageImage");
                DriveForms.SetResourceReference(ContentProperty, "Min-DriveFormsImage");
                TwilightTown.SetResourceReference(ContentProperty, "Min-TwilightTownImage");
                BeastsCastle.SetResourceReference(ContentProperty, "Min-BeastCastleImage");
                Agrabah.SetResourceReference(ContentProperty, "Min-AgrabahImage");
                HundredAcreWood.SetResourceReference(ContentProperty, "Min-HundredAcreImage");
                PortRoyal.SetResourceReference(ContentProperty, "Min-PortRoyalImage");
                TWTNW.SetResourceReference(ContentProperty, "Min-TWTNWImage");
                Atlantica.SetResourceReference(ContentProperty, "Min-AtlanticaImage");

                //broadcast window worlds
                broadcast.SorasHeart.SetResourceReference(ContentProperty, "Min-SoraHeartImage");
                broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Min-SimulatedImage");
                broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                broadcast.LandofDragons.SetResourceReference(ContentProperty, "Min-LandofDragonsImage");
                broadcast.PrideLands.SetResourceReference(ContentProperty, "Min-PrideLandsImage");
                broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Min-HalloweenTownImage");
                broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Min-SpaceParanoidsImage");
                broadcast.DriveForms.SetResourceReference(ContentProperty, "Min-DriveFormsImage");
                broadcast.TwilightTown.SetResourceReference(ContentProperty, "Min-TwilightTownImage");
                broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Min-BeastCastleImage");
                broadcast.Agrabah.SetResourceReference(ContentProperty, "Min-AgrabahImage");
                broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Min-HundredAcreImage");
                broadcast.PortRoyal.SetResourceReference(ContentProperty, "Min-PortRoyalImage");
                broadcast.TWTNW.SetResourceReference(ContentProperty, "Min-TWTNWImage");
                broadcast.Atlantica.SetResourceReference(ContentProperty, "Min-AtlanticaImage");

                if (CavernOption.IsChecked)
                {
                    HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionCorImage");
                    broadcast.HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionCorImage");
                }
                else
                {
                    HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                    broadcast.HollowBastion.SetResourceReference(ContentProperty, "Min-HollowBastionImage");
                }

                if (TimelessOption.IsChecked)
                {
                    DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleTrImage");
                    broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleTrImage");
                }
                else
                {
                    DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleImage");
                    broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Min-DisneyCastleImage");
                }
            }

            CustomWorldCheck();
        }

        private void OldWorldToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (OldWorldOption.IsChecked == false)
            {
                OldWorldOption.IsChecked = true;
                return;
            }

            MinWorldOption.IsChecked = false;
            Properties.Settings.Default.MinWorld = MinWorldOption.IsChecked;
            Properties.Settings.Default.OldWorld = OldWorldOption.IsChecked;

            if (OldWorldOption.IsChecked)
            {
                //main window worlds
                SorasHeart.SetResourceReference(ContentProperty, "Old-SoraHeartImage");
                SimulatedTwilightTown.SetResourceReference(ContentProperty, "Old-SimulatedImage");
                OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                LandofDragons.SetResourceReference(ContentProperty, "Old-LandofDragonsImage");
                PrideLands.SetResourceReference(ContentProperty, "Old-PrideLandsImage");
                HalloweenTown.SetResourceReference(ContentProperty, "Old-HalloweenTownImage");
                SpaceParanoids.SetResourceReference(ContentProperty, "Old-SpaceParanoidsImage");
                GoA.SetResourceReference(ContentProperty, "Old-GardenofAssemblageImage");
                DriveForms.SetResourceReference(ContentProperty, "Old-DriveFormsImage");
                TwilightTown.SetResourceReference(ContentProperty, "Old-TwilightTownImage");
                BeastsCastle.SetResourceReference(ContentProperty, "Old-BeastCastleImage");
                Agrabah.SetResourceReference(ContentProperty, "Old-AgrabahImage");
                HundredAcreWood.SetResourceReference(ContentProperty, "Old-HundredAcreImage");
                PortRoyal.SetResourceReference(ContentProperty, "Old-PortRoyalImage");
                TWTNW.SetResourceReference(ContentProperty, "Old-TWTNWImage");
                Atlantica.SetResourceReference(ContentProperty, "Old-AtlanticaImage");

                //broadcast window worlds
                broadcast.SorasHeart.SetResourceReference(ContentProperty, "Old-SoraHeartImage");
                broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Old-SimulatedImage");
                broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                broadcast.LandofDragons.SetResourceReference(ContentProperty, "Old-LandofDragonsImage");
                broadcast.PrideLands.SetResourceReference(ContentProperty, "Old-PrideLandsImage");
                broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Old-HalloweenTownImage");
                broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Old-SpaceParanoidsImage");
                broadcast.DriveForms.SetResourceReference(ContentProperty, "Old-DriveFormsImage");
                broadcast.TwilightTown.SetResourceReference(ContentProperty, "Old-TwilightTownImage");
                broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Old-BeastCastleImage");
                broadcast.Agrabah.SetResourceReference(ContentProperty, "Old-AgrabahImage");
                broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Old-HundredAcreImage");
                broadcast.PortRoyal.SetResourceReference(ContentProperty, "Old-PortRoyalImage");
                broadcast.TWTNW.SetResourceReference(ContentProperty, "Old-TWTNWImage");
                broadcast.Atlantica.SetResourceReference(ContentProperty, "Old-AtlanticaImage");

                if (CavernOption.IsChecked)
                {
                    HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionCorImage");
                    broadcast.HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionCorImage");
                }
                else
                {
                    HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                    broadcast.HollowBastion.SetResourceReference(ContentProperty, "Old-HollowBastionImage");
                }

                if (TimelessOption.IsChecked)
                {
                    DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleTrImage");
                    broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleTrImage");
                }
                else
                {
                    DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleImage");
                    broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Old-DisneyCastleImage");
                }
            }

            CustomWorldCheck();
        }

        private void MinProgToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MinProgOption.IsChecked == false)
            {
                MinProgOption.IsChecked = true;
                return;
            }

            OldProgOption.IsChecked = false;
            Properties.Settings.Default.MinProg = MinProgOption.IsChecked;
            Properties.Settings.Default.OldProg = OldProgOption.IsChecked;

            if (MinProgOption.IsChecked)
            {
                SetProgressIcons();
            }
        }

        private void OldProgToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (OldProgOption.IsChecked == false)
            {
                OldProgOption.IsChecked = true;
                return;
            }

            MinProgOption.IsChecked = false;
            Properties.Settings.Default.MinProg = MinProgOption.IsChecked;
            Properties.Settings.Default.OldProg = OldProgOption.IsChecked;

            if (OldProgOption.IsChecked)
            {
                SetProgressIcons();
            }
        }

        private void MinNumToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (MinNumOption.IsChecked == false)
            {
                MinNumOption.IsChecked = true;
                return;
            }

            OldNumOption.IsChecked = false;
            Properties.Settings.Default.MinNum = MinNumOption.IsChecked;
            Properties.Settings.Default.OldNum = OldNumOption.IsChecked;

            if (MinNumOption.IsChecked)
            {
                ReportNumCheck();

                ReloadBindings();
            }

        }

        private void OldNumToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (OldNumOption.IsChecked == false)
            {
                OldNumOption.IsChecked = true;
                return;
            }

            MinNumOption.IsChecked = false;
            Properties.Settings.Default.MinNum = MinNumOption.IsChecked;
            Properties.Settings.Default.OldNum = OldNumOption.IsChecked;

            if (OldNumOption.IsChecked)
            {
                ReportNumCheck();

                ReloadBindings();
            }

        }

        private void CustomImageToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CustomIcons = CustomFolderOption.IsChecked;

            CustomChecksCheck();
            CustomWorldCheck();
            ReloadBindings();
            SetProgressIcons();

            if (!CustomFolderOption.IsChecked)
            {
                //reload check icons
                {
                    if (MinCheckOption.IsChecked)
                    {
                        MinCheckToggle(sender, e);
                    }
                    else if (OldCheckOption.IsChecked)
                    {
                        OldCheckToggle(sender, e);
                    }
                }
                //reload world icons
                {
                    if (MinWorldOption.IsChecked)
                    {
                        MinWorldToggle(sender, e);
                    }
                    else if (OldWorldOption.IsChecked)
                    {
                        OldWorldToggle(sender, e);
                    }
                }
                //reload prog icons (do i need this? only if i want switching to be dynamic i guess)
                //{
                //    if (MinProgOption.IsChecked)
                //    {
                //        MinProgToggle(sender, e);
                //    }
                //    else if (OldProgOption.IsChecked)
                //    {
                //        OldProgToggle(sender, e);
                //    }
                //}
            }
        }
    }
}
