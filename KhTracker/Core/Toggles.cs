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
                var outerGrid = ((button.Parent as Grid).Parent as Grid).Parent as Grid;
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
                        if (Box.Opacity != 0.9)
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
                var outerGrid = ((button.Parent as Grid).Parent as Grid).Parent as Grid;
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(0, GridUnitType.Star);
                button.IsEnabled = false;
                button.Visibility = Visibility.Collapsed;
            }
        }

        ///
        /// Options
        ///

        private void TopMostToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopMost = TopMostOption.IsChecked;
            Topmost = TopMostOption.IsChecked;
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

            foreach (string key in data.Items.Keys)
            {
                Item item = data.Items[key].Item1;
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

        private void AutoDetectToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoDetect = AutoDetectOption.IsChecked;

            if (AutoDetectOption.IsChecked)
            {
                Connect.Visibility = Visibility.Visible;
                Connect2.Visibility = Visibility.Collapsed;
                SetAutoDetectTimer();

                SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
            }
            else
            {
                Connect.Visibility = Visibility.Collapsed;
                Connect2.Visibility = Visibility.Collapsed;

                if (SettingsText.Text == "Settings:")
                    SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
                else
                    SettingRow.Height = new GridLength(0, GridUnitType.Star);
            }              
        }

        ///
        /// Toggles
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

            //set reports
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(toggle, data.Reports[i], false);
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
                PromiseCharmCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                PromiseCharmCol.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, PromiseCharm, false);
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
                OnceMoreCol.Width = new GridLength(1.0, GridUnitType.Star);
                SecondChanceCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                OnceMoreCol.Width = new GridLength(0, GridUnitType.Star);
                SecondChanceCol.Width = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, OnceMore, false);
            HandleItemToggle(toggle, SecondChance, false);
        }

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
                AntiFormCol.Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                AntiFormCol.Width = new GridLength(0, GridUnitType.Star);
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

            int value = 0;

            if (toggle)
            {
                value = 1;

                S_MulanWep.Visibility = Visibility.Visible;
                S_AuronWep.Visibility = Visibility.Visible;
                S_BeastWep.Visibility = Visibility.Visible;
                S_JackWep.Visibility = Visibility.Visible;
                S_IceCream.Visibility = Visibility.Visible;
                S_TronWep.Visibility = Visibility.Visible;
                S_Picture.Visibility = Visibility.Visible;
                S_MembershipCard.Visibility = Visibility.Visible;
                S_SimbaWep.Visibility = Visibility.Visible;
                S_AladdinWep.Visibility = Visibility.Visible;
                S_SparrowWep.Visibility = Visibility.Visible;

                Cross01.Visibility = Visibility.Collapsed;
                Cross02.Visibility = Visibility.Collapsed;
                Cross03.Visibility = Visibility.Collapsed;
                Cross04.Visibility = Visibility.Collapsed;
                Cross05.Visibility = Visibility.Collapsed;
                Cross06.Visibility = Visibility.Collapsed;
                Cross07.Visibility = Visibility.Collapsed;
                Cross08.Visibility = Visibility.Collapsed;
                Cross09.Visibility = Visibility.Collapsed;
                Cross10.Visibility = Visibility.Collapsed;
                Cross11.Visibility = Visibility.Collapsed;

                VisitsRow.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                S_MulanWep.Visibility = Visibility.Collapsed;
                S_AuronWep.Visibility = Visibility.Collapsed;
                S_BeastWep.Visibility = Visibility.Collapsed;
                S_JackWep.Visibility = Visibility.Collapsed;
                S_IceCream.Visibility = Visibility.Collapsed;
                S_TronWep.Visibility = Visibility.Collapsed;
                S_Picture.Visibility = Visibility.Collapsed;
                S_MembershipCard.Visibility = Visibility.Collapsed;
                S_SimbaWep.Visibility = Visibility.Collapsed;
                S_AladdinWep.Visibility = Visibility.Collapsed;
                S_SparrowWep.Visibility = Visibility.Collapsed;

                Cross01.Visibility = Visibility.Visible;
                Cross02.Visibility = Visibility.Visible;
                Cross03.Visibility = Visibility.Visible;
                Cross04.Visibility = Visibility.Visible;
                Cross05.Visibility = Visibility.Visible;
                Cross06.Visibility = Visibility.Visible;
                Cross07.Visibility = Visibility.Visible;
                Cross08.Visibility = Visibility.Visible;
                Cross09.Visibility = Visibility.Visible;
                Cross10.Visibility = Visibility.Visible;
                Cross11.Visibility = Visibility.Visible;

                if(ExtraChecksOption.IsChecked)
                    VisitsRow.Height = new GridLength(1, GridUnitType.Star);
                else
                    VisitsRow.Height = new GridLength(0, GridUnitType.Star);
            }


            //set lock values
            data.WorldsData["TwilightTown"].visitLocks = value * 11;
            data.WorldsData["HollowBastion"].visitLocks = value;
            data.WorldsData["BeastsCastle"].visitLocks = value;
            data.WorldsData["OlympusColiseum"].visitLocks = value;
            data.WorldsData["Agrabah"].visitLocks = value;
            data.WorldsData["LandofDragons"].visitLocks = value;
            data.WorldsData["PrideLands"].visitLocks = value;
            data.WorldsData["HalloweenTown"].visitLocks = value;
            data.WorldsData["PortRoyal"].visitLocks = value;
            data.WorldsData["SpaceParanoids"].visitLocks = value;

            for (int i = 0; i < data.VisitLocks.Count; ++i)
            {
                HandleItemToggle(toggle, data.VisitLocks[i], false);
            }

            VisitLockCheck();
        }

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
                HadesCupCol.Width = new GridLength(1.0, GridUnitType.Star);
                OlympusStoneCol.Width = new GridLength(1.0, GridUnitType.Star);
                UnknownDiskCol.Width = new GridLength(1.0, GridUnitType.Star);

                MunnySep.Width = new GridLength(0.1, GridUnitType.Star);
                MunnyNum.Width = new GridLength(0.6, GridUnitType.Star);
                MunnyImg.Width = new GridLength(1.0, GridUnitType.Star);

                VisitsRow.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                HadesCupCol.Width = new GridLength(0, GridUnitType.Star);
                OlympusStoneCol.Width = new GridLength(0, GridUnitType.Star);
                UnknownDiskCol.Width = new GridLength(0, GridUnitType.Star);

                MunnySep.Width = new GridLength(0, GridUnitType.Star);
                MunnyNum.Width = new GridLength(0, GridUnitType.Star);
                MunnyImg.Width = new GridLength(0, GridUnitType.Star);

                if (VisitLockOption.IsChecked)
                    VisitsRow.Height = new GridLength(1, GridUnitType.Star);
                else
                    VisitsRow.Height = new GridLength(0, GridUnitType.Star);
            }

            HandleItemToggle(toggle, HadesCup, false);
            HandleItemToggle(toggle, OlympusStone, false);
            HandleItemToggle(toggle, UnknownDisk, false);
            HandleItemToggle(toggle, MunnyPouch1, false);
            HandleItemToggle(toggle, MunnyPouch2, false);
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
                HintText.Text = "";

            if (data.SeedHashLoaded && toggle)
                HashGrid.Visibility = Visibility.Visible;
            else
                HashGrid.Visibility = Visibility.Collapsed;
        }

        private void WorldProgressToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WorldProgress = WorldProgressOption.IsChecked;
            if (WorldProgressOption.IsChecked)
            {
                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Hidden;
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
            WorldGrid.Ghost_Pouches_obtained = 0;
        }

        private void GhostMathToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.GhostMath = GhostMathOption.IsChecked;

            if (GhostItemOption.IsChecked && (data.mode == Mode.DAHints || data.ScoreMode))
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

        private void ShowCheckCountToggle(object sender, RoutedEventArgs e)
        {
            ShowCheckCountToggle(CheckCountOption.IsChecked);
        }

        private void ShowCheckCountToggle(bool toggle)
        {
            Properties.Settings.Default.CheckCount = toggle;
            CheckCountOption.IsChecked = toggle;

            //don't do anything if not in points mode
            if (data.mode != Mode.DAHints && !data.ScoreMode)
                return;

            //if check count should be shown and replace the score IN POINTS MODE
            if (toggle)
            {
                CollectionGrid.Visibility = Visibility.Visible;
                ScoreGrid.Visibility = Visibility.Collapsed;
            }
            //if points should show and replace check count IN POINTS MODE
            else
            {
                CollectionGrid.Visibility = Visibility.Collapsed;
                ScoreGrid.Visibility = Visibility.Visible;
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

            if (levelsetting == 1 || Level.Visibility != Visibility.Visible)
            {
                NextLevelCol.Width = new GridLength(0, GridUnitType.Star);
                return;
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

                HashSpacer1.Width = new GridLength(0.00, GridUnitType.Star);
                HashSpacer2.Width = new GridLength(1.75, GridUnitType.Star);
            }
            else
            {
                DeathCounterGrid.Visibility = Visibility.Collapsed;
                DeathCol.Width = new GridLength(0, GridUnitType.Star);

                HashSpacer1.Width = new GridLength(0.75, GridUnitType.Star);
                HashSpacer2.Width = new GridLength(0.75, GridUnitType.Star);
            }
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

            NextLevelDisplay();
        }

        ///
        /// Visual
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

            SetProgressIcons();
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

            SetProgressIcons();
        }

        private void CustomImageToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CustomIcons = CustomFolderOption.IsChecked;

            if (CustomFolderOption.IsChecked)
            {
                CustomChecksCheck();
                CustomWorldCheck();
                SetProgressIcons();
            }
            else
            {
                //reload check icons
                SetItemImage();
                //reload world icons
                SetWorldImage();
            }
        }

        ///
        /// Worlds
        /// 

        private void SoraHeartToggle(object sender, RoutedEventArgs e)
        {
            SoraHeartToggle(SoraHeartOption.IsChecked);
        }

        private void SoraHeartToggle(bool toggle)
        {
            Properties.Settings.Default.SoraHeart = toggle;
            SoraHeartOption.IsChecked = toggle;
            HandleWorldToggle(toggle, SorasHeart, SorasHeartGrid);
        }

        private void DrivesToggle(object sender, RoutedEventArgs e)
        {
            DrivesToggle(DrivesOption.IsChecked);
        }

        private void DrivesToggle(bool toggle)
        {
            Properties.Settings.Default.Drives = toggle;
            DrivesOption.IsChecked = toggle;
            HandleWorldToggle(toggle, DriveForms, DriveFormsGrid);
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
        }

        private void TwilightTownToggle(object sender, RoutedEventArgs e)
        {
            TwilightTownToggle(TwilightTownOption.IsChecked);
        }

        private void TwilightTownToggle(bool toggle)
        {
            Properties.Settings.Default.TwilightTown = toggle;
            TwilightTownOption.IsChecked = toggle;
            HandleWorldToggle(toggle, TwilightTown, TwilightTownGrid);
        }

        private void HollowBastionToggle(object sender, RoutedEventArgs e)
        {
            HollowBastionToggle(HollowBastionOption.IsChecked);
        }

        private void HollowBastionToggle(bool toggle)
        {
            Properties.Settings.Default.HollowBastion = toggle;
            HollowBastionOption.IsChecked = toggle;
            HandleWorldToggle(toggle, HollowBastion, HollowBastionGrid);
        }

        private void BeastCastleToggle(object sender, RoutedEventArgs e)
        {
            BeastCastleToggle(BeastCastleOption.IsChecked);
        }

        private void BeastCastleToggle(bool toggle)
        {
            Properties.Settings.Default.BeastCastle = toggle;
            BeastCastleOption.IsChecked = toggle;
            HandleWorldToggle(toggle, BeastsCastle, BeastsCastleGrid);
        }

        private void OlympusToggle(object sender, RoutedEventArgs e)
        {
            OlympusToggle(OlympusOption.IsChecked);
        }

        private void OlympusToggle(bool toggle)
        {
            Properties.Settings.Default.Olympus = toggle;
            OlympusOption.IsChecked = toggle;
            HandleWorldToggle(toggle, OlympusColiseum, OlympusColiseumGrid);
        }

        private void AgrabahToggle(object sender, RoutedEventArgs e)
        {
            AgrabahToggle(AgrabahOption.IsChecked);
        }

        private void AgrabahToggle(bool toggle)
        {
            Properties.Settings.Default.Agrabah = toggle;
            AgrabahOption.IsChecked = toggle;
            HandleWorldToggle(toggle, Agrabah, AgrabahGrid);
        }

        private void LandofDragonsToggle(object sender, RoutedEventArgs e)
        {
            LandofDragonsToggle(LandofDragonsOption.IsChecked);
        }

        private void LandofDragonsToggle(bool toggle)
        {
            Properties.Settings.Default.LandofDragons = toggle;
            LandofDragonsOption.IsChecked = toggle;
            HandleWorldToggle(toggle, LandofDragons, LandofDragonsGrid);
        }

        private void DisneyCastleToggle(object sender, RoutedEventArgs e)
        {
            DisneyCastleToggle(DisneyCastleOption.IsChecked);
        }

        private void DisneyCastleToggle(bool toggle)
        {
            Properties.Settings.Default.DisneyCastle = toggle;
            DisneyCastleOption.IsChecked = toggle;
            HandleWorldToggle(toggle, DisneyCastle, DisneyCastleGrid);
        }

        private void PrideLandsToggle(object sender, RoutedEventArgs e)
        {
            PrideLandsToggle(PrideLandsOption.IsChecked);
        }

        private void PrideLandsToggle(bool toggle)
        {
            Properties.Settings.Default.PrideLands = toggle;
            PrideLandsOption.IsChecked = toggle;
            HandleWorldToggle(toggle, PrideLands, PrideLandsGrid);
        }

        private void PortRoyalToggle(object sender, RoutedEventArgs e)
        {
            PortRoyalToggle(PortRoyalOption.IsChecked);
        }

        private void PortRoyalToggle(bool toggle)
        {
            Properties.Settings.Default.PortRoyal = toggle;
            PortRoyalOption.IsChecked = toggle;
            HandleWorldToggle(toggle, PortRoyal, PortRoyalGrid);
        }

        private void HalloweenTownToggle(object sender, RoutedEventArgs e)
        {
            HalloweenTownToggle(HalloweenTownOption.IsChecked);
        }

        private void HalloweenTownToggle(bool toggle)
        {
            Properties.Settings.Default.HalloweenTown = toggle;
            HalloweenTownOption.IsChecked = toggle;
            HandleWorldToggle(toggle, HalloweenTown, HalloweenTownGrid);
        }

        private void SpaceParanoidsToggle(object sender, RoutedEventArgs e)
        {
            SpaceParanoidsToggle(SpaceParanoidsOption.IsChecked);
        }

        private void SpaceParanoidsToggle(bool toggle)
        {
            Properties.Settings.Default.SpaceParanoids = toggle;
            SpaceParanoidsOption.IsChecked = toggle;
            HandleWorldToggle(toggle, SpaceParanoids, SpaceParanoidsGrid);
        }

        private void TWTNWToggle(object sender, RoutedEventArgs e)
        {
            TWTNWToggle(TWTNWOption.IsChecked);
        }

        private void TWTNWToggle(bool toggle)
        {
            Properties.Settings.Default.TWTNW = toggle;
            TWTNWOption.IsChecked = toggle;
            HandleWorldToggle(toggle, TWTNW, TWTNWGrid);
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
            }
            else //check and change display
            {
                if (!PuzzleOn) //puzzles wasn't on before so show world
                {
                    HandleWorldToggle(true, PuzzSynth, PuzzSynthGrid);
                }
                SetWorldImage();
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
            }
            else //check and change display
            {
                if (!SynthOn) //synth wasn't on before so show world
                {
                    HandleWorldToggle(true, PuzzSynth, PuzzSynthGrid);
                }
                SetWorldImage();
            }
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

    }
}
