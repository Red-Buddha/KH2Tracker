using System;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;
using System.IO;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        ///
        /// Helpers
        ///

        private void HandleItemToggle(bool toggle, Item button, bool init)
        {
            if (toggle && button.IsEnabled == false)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
                if (!init)
                {
                    SetTotal(true);
                }
            }
            else if (toggle == false && button.IsEnabled)
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
                SetTotal(false);

                button.HandleItemReturn();
            }
        }

        ///rethink this. do i really need it?
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
            //make grid visible
            if (toggle && !button.IsEnabled)
            {
                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(1, GridUnitType.Star);
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else if (!toggle && button.IsEnabled)
            {
                //if world was previously selected, unselect it and reset highlighted visual
                if (data.selected == button)
                {
                    foreach (var Box in data.WorldsData[button.Name].top.Children.OfType<Rectangle>())
                    {
                        Box.Fill = (SolidColorBrush)FindResource("DefaultRec");
                    }
                    data.selected = null;
                }

                //return all items in world to itempool
                for (int i = grid.Children.Count - 1; i >= 0; --i)
                {
                    Item item = grid.Children[i] as Item;
                    item.HandleItemReturn();
                }

                //resize grid and collapse it
                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(0, GridUnitType.Star);
                button.IsEnabled = false;
                button.Visibility = Visibility.Collapsed;
            }
        }

        ///
        /// Option Toggles
        ///



        ///
        /// Item Toggles
        ///

        private void ReportsToggle(object sender, RoutedEventArgs e)
        {
            ReportsToggle(ReportsOption.IsChecked);
        }

        private void ReportsToggle(bool toggle)
        {
            Properties.Settings.Default.AnsemReports = toggle;
            ReportsOption.IsChecked = toggle;
            Double Size;

            //Set gridsizes based on toggle
            if (toggle)
                Size = 1.0;
            else
                Size = 0.0;

            ReportRow.Height = new GridLength(Size, GridUnitType.Star);
            broadcast.AnsemReport.Width = new GridLength(Size, GridUnitType.Star);

            //set reports
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(toggle, data.Reports[i], false);
            }
        }

        ///TODO: fix main window icon hiding
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
                //Pr_PromiseCharm.Width = new GridLength(1.0, GridUnitType.Star);

                broadcast.PromiseCharm.Visibility = Visibility.Visible;
                broadcast.PromiseCharmCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                //Pr_PromiseCharm.Width = new GridLength(0, GridUnitType.Star);

                broadcast.PromiseCharm.Visibility = Visibility.Hidden;
                broadcast.PromiseCharmCol.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, PromiseCharm, false);
        }

        ///TODO: fix main window icon hiding
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
                //Ab_OnceMore.Width = new GridLength(1.0, GridUnitType.Star);
                //Ab_SecondChance.Width = new GridLength(1.0, GridUnitType.Star);

                broadcast.SecondChanceCol.Width = new GridLength(1.0, GridUnitType.Star);
                broadcast.OnceMoreCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                //Ab_OnceMore.Width = new GridLength(0, GridUnitType.Star);
                //Ab_SecondChance.Width = new GridLength(0, GridUnitType.Star);

                broadcast.SecondChanceCol.Width = new GridLength(0, GridUnitType.Star);
                broadcast.OnceMoreCol.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, OnceMore, false);
            HandleItemToggle(toggle, SecondChance, false);
        }

        ///TODO: fix main window icon hiding
        private void AntiFormToggle(object sender, RoutedEventArgs e)
        {
            AntiFormToggle(AntiFormOption.IsChecked);
        }

        private void AntiFormToggle(bool toggle)
        {
            Properties.Settings.Default.AntiForm = toggle;
            AntiFormOption.IsChecked = toggle;

            if (toggle)
            {
                //Ex_Anti.Width = new GridLength(1.0, GridUnitType.Star);
                broadcast.Ex_Anti.Width = new GridLength(2.0, GridUnitType.Star);
            }
            else
            {
                //Ex_Anti.Width = new GridLength(0, GridUnitType.Star);
                broadcast.Ex_Anti.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, Anti, false);
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

                data.WorldsData["TwilightTown"].visitLocks = 11;
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

        ///TODO: fix main window icon hiding
        private void ExtraChecksToggle(object sender, RoutedEventArgs e)
        {
            ExtraChecksToggle(ExtraChecksOption.IsChecked);
        }

        private void ExtraChecksToggle(bool toggle)
        {
            Properties.Settings.Default.ExtraChecks = toggle;
            ExtraChecksOption.IsChecked = toggle;

            if (toggle)
            {
                //Ex_HadesCup.Width = new GridLength(1.0, GridUnitType.Star);
                //Ex_OlympusStone.Width = new GridLength(1.0, GridUnitType.Star);
                //Ex_UnknownDisk.Width = new GridLength(1.0, GridUnitType.Star);

                broadcast.Ex_HadesCup.Width = new GridLength(1.0, GridUnitType.Star);
                broadcast.Ex_OlympusStone.Width = new GridLength(1.0, GridUnitType.Star);
                broadcast.Ex_UnknownDisk.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                //Ex_HadesCup.Width = new GridLength(0, GridUnitType.Star);
                //Ex_OlympusStone.Width = new GridLength(0, GridUnitType.Star);
                //Ex_UnknownDisk.Width = new GridLength(0, GridUnitType.Star);

                broadcast.Ex_HadesCup.Width = new GridLength(0, GridUnitType.Star);
                broadcast.Ex_OlympusStone.Width = new GridLength(0, GridUnitType.Star);
                broadcast.Ex_UnknownDisk.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, HadesCup, false);
            HandleItemToggle(toggle, OlympusStone, false);
            HandleItemToggle(toggle, UnknownDisk, false);
        }
        
        ///
        /// Info Display Toggles
        /// 

        private void SeedHashToggle(object sender, RoutedEventArgs e)
        {
            SeedHashToggle(SeedHashOption.IsChecked);
        }

        private void SeedHashToggle(bool toggle)
        {
            Properties.Settings.Default.SeedHash = toggle;
            SeedHashOption.IsChecked = toggle;

            if (toggle)
                HintText.Text = "";

            if (data.SeedHashLoaded && toggle)
                HashGrid.Visibility = Visibility.Visible;
            else
                HashGrid.Visibility = Visibility.Hidden;
        }

        ///TODO: fix progression visibility
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

                    //data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1.5, GridUnitType.Star);
                    //data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(3.3, GridUnitType.Star);

                    //Grid grid = data.WorldsData[key].world.Parent as Grid;
                    //grid.ColumnDefinitions[0].Width = new GridLength(3.5, GridUnitType.Star);
                    //grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                    //grid.ColumnDefinitions[2].Width = new GridLength(2, GridUnitType.Star);
                    //Grid.SetColumnSpan(data.WorldsData[key].world, 2);
                }
            }
            else
            {
                broadcast.ToggleProgression(false);

                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Hidden;

                    //data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    //data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(4, GridUnitType.Star);

                    //Grid grid = data.WorldsData[key].world.Parent as Grid;
                    //grid.ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Star);
                    //grid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                    //grid.ColumnDefinitions[2].Width = new GridLength(4, GridUnitType.Star);
                    //Grid.SetColumnSpan(data.WorldsData[key].world, 3);
                }
            }
        }

        private void FormsGrowthToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FormsGrowth = FormsGrowthOption.IsChecked;

            if (FormsGrowthOption.IsChecked && aTimer != null)
                FormRow.Height = new GridLength(0.5, GridUnitType.Star);
            else
                FormRow.Height = new GridLength(0, GridUnitType.Star);
        }

        ///TODO: look this over when fixing broadcast window
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

        private void GhostItemToggle(object sender, RoutedEventArgs e)
        {
            GhostItemToggle(GhostItemOption.IsChecked);
        }

        private void GhostItemToggle(bool toggle)
        {
            Properties.Settings.Default.GhostItem = toggle;
            GhostItemOption.IsChecked = toggle;

            foreach (var item in data.GhostItems.Values)
            {
                HandleItemToggle(toggle, item, false);
            }

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

            if (GhostItemOption.IsChecked && data.mode == Mode.DAHints)
            {
                int add = -1;

                //subtract points (math on)
                if (!GhostMathOption.IsChecked)
                    add = 1;

                foreach (WorldData worldData in data.WorldsData.Values.ToList())
                {
                    if (worldData.value == null)
                        continue;

                    if (worldData.containsGhost)
                    {
                        int ghostnum = GetGhostPoints(worldData.worldGrid) * add;
                        int worldnum = -1;

                        if (worldData.value.Text != "?")
                            worldnum = int.Parse(worldData.value.Text);

                        SetWorldValue(worldData.value, worldnum + ghostnum);
                    }
                }
            }
        }

        ///TODO: look this over when fixing broadcast window
        private void ShowCheckCountToggle(object sender, RoutedEventArgs e)
        {
            ShowCheckCountToggle(CheckCountOption.IsChecked);
        }

        private void ShowCheckCountToggle(bool toggle)
        {
            Properties.Settings.Default.CheckCount = toggle;
            CheckCountOption.IsChecked = toggle;

            //don't do anything if not in points mode
            if (data.mode != Mode.DAHints)
                return;

            //if check count should be shown and replace the score IN POINTS MODE
            if (toggle)
            {
                CollectionGrid.Visibility = Visibility.Visible;
                ScoreGrid.Visibility = Visibility.Collapsed;

                //broadcast.Score.Visibility = Visibility.Hidden;
                //broadcast.Collected.Visibility = Visibility.Visible;
            }
            //if points should show and replace check count IN POINTS MODE
            else
            {
                CollectionGrid.Visibility = Visibility.Collapsed;
                ScoreGrid.Visibility = Visibility.Visible;

                //broadcast.Score.Visibility = Visibility.Visible;
                //broadcast.Collected.Visibility = Visibility.Hidden;
            }
        }

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
            bool Visible = NextLevelCheckOption.IsChecked;

            //default
            int levelsetting = 1;

            if (SoraLevel50Option.IsChecked)
                levelsetting = 50;

            if (SoraLevel99Option.IsChecked)
                levelsetting = 99;

            if (memory != null && stats != null)
            {
                try
                {
                    stats.SetMaxLevelCheck(levelsetting);
                    stats.SetNextLevelCheck(stats.Level);
                }
                catch
                {
                    Console.WriteLine("Tried to edit while loading");
                }
            }

            if (Visible && memory != null)
            {
                NextLevelCol.Width = new GridLength(0.6, GridUnitType.Star);
            }
            else
            {
                NextLevelCol.Width = new GridLength(0, GridUnitType.Star);
            }
        }

        ///TODO: look this over when fixing broadcast window
        private void DeathCounterToggle(object sender, RoutedEventArgs e)
        {
            DeathCounterToggle(DeathCounterOption.IsChecked);
        }

        private void DeathCounterToggle(bool toggle)
        {
            Properties.Settings.Default.DeathCounter = toggle;
            DeathCounterOption.IsChecked = toggle;

            DeathCounterDisplay();
        }

        private void DeathCounterDisplay()
        {
            if (DeathCounterOption.IsChecked && memory != null)
            {
                DeathCounterGrid.Visibility = Visibility.Visible;
                DeathCol.Width = new GridLength(0.2, GridUnitType.Star);

                //broadcast.DeathCounter.Width = new GridLength(0.6, GridUnitType.Star);
            }
            else
            {
                DeathCounterGrid.Visibility = Visibility.Collapsed;
                DeathCol.Width = new GridLength(0, GridUnitType.Star);

                //broadcast.DeathCounter.Width = new GridLength(0, GridUnitType.Star);
            }
        }

        ///
        /// Visual Toggles
        /// 

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

            SetItemImage();
            CustomChecksCheck();
            //ReloadBindings();
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

            SetItemImage();
            CustomChecksCheck();
            //ReloadBindings();
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

            SetWorldImage();
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

            SetWorldImage();
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










        private void AutoDetectToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoDetect = AutoDetectOption.IsChecked;

            if (AutoDetectOption.IsChecked)
            {
                //Console.WriteLine("Auto Detect enabled?");
                Connect.Source = data.AD_Connect;
                //SetAutoDetectTimer();
                Connect.Visibility = Visibility.Visible;
                SetAutoDetectTimer();
            }
            else
                Connect.Visibility = Visibility.Hidden;
        }



        //private void LegacyToggle(object sender, RoutedEventArgs e)
        //{
        //    LegacyToggle(LegacyOption.IsChecked);
        //}
        //
        //private void LegacyToggle(bool toggle)
        //{
        //    Properties.Settings.Default.Legacy = toggle;
        //    LegacyOption.IsChecked = toggle;
        //}

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

        private void SynthToggle(object sender, RoutedEventArgs e)
        {
            SynthToggle(SynthOption.IsChecked);
        }

        private void SynthToggle(bool toggle)
        {
            Properties.Settings.Default.Synth = toggle;
            SynthOption.IsChecked = toggle;

            //Check puzzle state
            bool PuzzleOn = PuzzleOption.IsChecked;

            //hide if both off
            if (!toggle && !PuzzleOn)
            {
                HandleWorldToggle(false, PuzzSynth, PuzzSynthGrid);
                broadcast.PuzzSynthCol.Width = new GridLength(0, GridUnitType.Star);
            }
            else //check and change display
            {
                if (!PuzzleOn) //puzzles wasn't on before so show world
                {
                    HandleWorldToggle(true, PuzzSynth, PuzzSynthGrid);
                    broadcast.PuzzSynthCol.Width = new GridLength(1.0, GridUnitType.Star);
                }
                CustomWorldCheck();
            }
        }

        private void PuzzleToggle(object sender, RoutedEventArgs e)
        {
            PuzzleToggle(PuzzleOption.IsChecked);
        }

        private void PuzzleToggle(bool toggle)
        {
            Properties.Settings.Default.Puzzle = toggle;
            PuzzleOption.IsChecked = toggle;

            //Check synth state
            bool SynthOn = SynthOption.IsChecked;

            if (!toggle && !SynthOn) //hide if both off
            {
                HandleWorldToggle(false, PuzzSynth, PuzzSynthGrid);
                broadcast.PuzzSynthCol.Width = new GridLength(0, GridUnitType.Star);
            }
            else //check and change display
            {
                if (!SynthOn) //synth wasn't on before so show world
                {
                    HandleWorldToggle(true, PuzzSynth, PuzzSynthGrid);
                    broadcast.PuzzSynthCol.Width = new GridLength(1.0, GridUnitType.Star);
                }
                CustomWorldCheck();
            }
        }

        ///Alt World Toggles
        //private void CavernToggle(object sender, RoutedEventArgs e)
        //{
        //    CavernToggle(CavernOption.IsChecked);
        //}
        //
        //private void CavernToggle(bool toggle)
        //{
        //    Properties.Settings.Default.Cavern = toggle;
        //    CavernOption.IsChecked = toggle;
        //
        //    CustomWorldCheck();
        //}
        //
        //private void TerraToggle(object sender, RoutedEventArgs e)
        //{
        //    TerraToggle(TerraOption.IsChecked);
        //}
        //
        //private void TerraToggle(bool toggle)
        //{
        //    Properties.Settings.Default.Terra = toggle;
        //    TerraOption.IsChecked = toggle;
        //
        //    if (toggle)
        //    {
        //        //DisneyCastleLW.Visibility = Visibility.Visible;
        //        broadcast.DisneyCastleLW.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        //DisneyCastleLW.Visibility = Visibility.Collapsed;
        //        broadcast.DisneyCastleLW.Visibility = Visibility.Collapsed;
        //    }
        //
        //    CustomWorldCheck();
        //}
        //
        //private void OCCupsToggle(object sender, RoutedEventArgs e)
        //{
        //    OCCupsToggle(OCCupsOption.IsChecked);
        //}
        //
        //private void OCCupsToggle(bool toggle)
        //{
        //    Properties.Settings.Default.OCCups = toggle;
        //    OCCupsOption.IsChecked = toggle;
        //
        //    CustomWorldCheck();
        //}

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

            SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel01");
            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel01");

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

            SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel50");
            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel50");

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

            SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel99");
            broadcast.SorasHeartType.SetResourceReference(ContentProperty, "Min-SoraLevel99");

            CustomWorldCheck();
            NextLevelDisplay();
        }






        private void CustomImageToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CustomIcons = CustomFolderOption.IsChecked;

            CustomChecksCheck();
            CustomWorldCheck();
            //ReloadBindings();
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
                //reload visit locks
                {
                    HollowBastionLock.Source =   new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    OlympusColiseumLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    LandofDragonsLock.Source =   new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    PrideLandsLock.Source =      new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    HalloweenTownLock.Source =   new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    SpaceParanoidsLock.Source =  new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    BeastsCastleLock.Source =    new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    AgrabahLock.Source =         new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    PortRoyalLock.Source =       new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    TwilightTownLock_2.Source =  new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    TwilightTownLock_1.Source =  new BitmapImage(new Uri("Images/Other/visitlocksilver.png", UriKind.Relative));

                    broadcast.HollowBastionLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.OlympusColiseumLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.LandofDragonsLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.PrideLandsLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.HalloweenTownLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.SpaceParanoidsLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.BeastsCastleLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.AgrabahLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.PortRoyalLock.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.TwilightTownLock_2.Source = new BitmapImage(new Uri("Images/Other/visitlock.png", UriKind.Relative));
                    broadcast.TwilightTownLock_1.Source = new BitmapImage(new Uri("Images/Other/visitlocksilver.png", UriKind.Relative));
                }
                //reload others
                {
                    SorasHeartCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    DriveFormsCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    SimulatedTwilightTownCross.Source = new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    TwilightTownCross.Source =          new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    HollowBastionCross.Source =         new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    BeastsCastleCross.Source =          new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    OlympusColiseumCross.Source =       new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    AgrabahCross.Source =               new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    LandofDragonsCross.Source =         new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    HundredAcreWoodCross.Source =       new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    PrideLandsCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    DisneyCastleCross.Source =          new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    HalloweenTownCross.Source =         new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    PortRoyalCross.Source =             new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    TWTNWCross.Source =                 new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    SpaceParanoidsCross.Source =        new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    AtlanticaCross.Source =             new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    PuzzSynthCross.Source =             new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    GoACross.Source =                   new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));

                    broadcast.SorasHeartCross.Source =              new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.DriveFormsCross.Source =              new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.SimulatedTwilightTownCross.Source =   new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.TwilightTownCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.HollowBastionCross.Source =           new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.BeastsCastleCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.OlympusColiseumCross.Source =         new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.AgrabahCross.Source =                 new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.LandofDragonsCross.Source =           new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.HundredAcreWoodCross.Source =         new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.PrideLandsCross.Source =              new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.DisneyCastleCross.Source =            new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.HalloweenTownCross.Source =           new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.PortRoyalCross.Source =               new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.TWTNWCross.Source =                   new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.SpaceParanoidsCross.Source =          new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.AtlanticaCross.Source =               new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));
                    broadcast.PuzzSynthCross.Source =               new BitmapImage(new Uri("Images/Other/crossworld.png", UriKind.Relative));

                    Skull.Source =           new BitmapImage(new Uri("Images/Other/generic skull.png", UriKind.Relative));
                    broadcast.Skull.Source = new BitmapImage(new Uri("Images/Other/generic skull.png", UriKind.Relative));
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













        private void BroadcastStartupToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BroadcastStartup = BroadcastStartupOption.IsChecked;
            if (BroadcastStartupOption.IsChecked)
                broadcast.Show();
        }

        private void TopMostToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopMost = TopMostOption.IsChecked;
            Topmost = TopMostOption.IsChecked;
            broadcast.Topmost = TopMostOption.IsChecked;
        }

        private void DragDropToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DragDrop = DragAndDropOption.IsChecked;
            data.dragDrop = DragAndDropOption.IsChecked;

            List<Grid> itempools = new List<Grid>();
            foreach (Grid pool in ItemPool.Children)
            {
                itempools.Add(pool);
            }

            foreach (Item item in data.Items.Keys)
            {
                if (itempools.Contains(item.Parent))
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







    }
}
