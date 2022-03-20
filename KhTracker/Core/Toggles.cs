using System;
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

            //SeedHashVisibility(toggle);
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

            //foreach (string item in Data.GhostItems.Keys.ToList())
            //{
            //    if (Data.GhostItems[item].Parent == ItemPool)
            //    {
            //        if (data.dragDrop == false)
            //        {
            //            Data.GhostItems[item].MouseDoubleClick -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseMove -= Data.GhostItems[item].Item_Ghost;
            //
            //            Data.GhostItems[item].MouseDown -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseDown += Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseUp -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseUp += Data.GhostItems[item].Item_Ghost;
            //        }
            //        else
            //        {
            //            Data.GhostItems[item].MouseDoubleClick -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseDoubleClick += Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseMove -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseMove += Data.GhostItems[item].Item_Ghost;
            //
            //            Data.GhostItems[item].MouseDown -= Data.GhostItems[item].Item_Ghost;
            //            Data.GhostItems[item].MouseUp -= Data.GhostItems[item].Item_Ghost;
            //        }
            //    }
            //}
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

        private void CustomChecksCheck()
        {
            if (CustomFolderOption.IsChecked)
            {
                //check if folders exists then start checking if each file exists in it
                if (Directory.Exists("CustomImages/Checks/"))
                {
                    //Main Window
                    if (File.Exists("CustomImages/Checks/ansem_report01.png"))
                        Report1.SetResourceReference(ContentProperty, "Cus-AnsemReport01");
                    if (File.Exists("CustomImages/Checks/ansem_report02.png"))
                        Report2.SetResourceReference(ContentProperty, "Cus-AnsemReport02");
                    if (File.Exists("CustomImages/Checks/ansem_report03.png"))
                        Report3.SetResourceReference(ContentProperty, "Cus-AnsemReport03");
                    if (File.Exists("CustomImages/Checks/ansem_report04.png"))
                        Report4.SetResourceReference(ContentProperty, "Cus-AnsemReport04");
                    if (File.Exists("CustomImages/Checks/ansem_report05.png"))
                        Report5.SetResourceReference(ContentProperty, "Cus-AnsemReport05");
                    if (File.Exists("CustomImages/Checks/ansem_report06.png"))
                        Report6.SetResourceReference(ContentProperty, "Cus-AnsemReport06");
                    if (File.Exists("CustomImages/Checks/ansem_report07.png"))
                        Report7.SetResourceReference(ContentProperty, "Cus-AnsemReport07");
                    if (File.Exists("CustomImages/Checks/ansem_report08.png"))
                        Report8.SetResourceReference(ContentProperty, "Cus-AnsemReport08");
                    if (File.Exists("CustomImages/Checks/ansem_report09.png"))
                        Report9.SetResourceReference(ContentProperty, "Cus-AnsemReport09");
                    if (File.Exists("CustomImages/Checks/ansem_report10.png"))
                        Report10.SetResourceReference(ContentProperty, "Cus-AnsemReport10");
                    if (File.Exists("CustomImages/Checks/ansem_report11.png"))
                        Report11.SetResourceReference(ContentProperty, "Cus-AnsemReport11");
                    if (File.Exists("CustomImages/Checks/ansem_report12.png"))
                        Report12.SetResourceReference(ContentProperty, "Cus-AnsemReport12");
                    if (File.Exists("CustomImages/Checks/ansem_report13.png"))
                        Report13.SetResourceReference(ContentProperty, "Cus-AnsemReport13");

                    if (File.Exists("CustomImages/Checks/jump.png"))
                    {
                        HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                        broadcast.HighJump.SetResourceReference(ContentProperty, "Cus-HighJump");
                    }
                    if (File.Exists("CustomImages/Checks/quick.png"))
                    {
                        QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                        broadcast.QuickRun.SetResourceReference(ContentProperty, "Cus-QuickRun");
                    }
                    if (File.Exists("CustomImages/Checks/dodge.png"))
                    {
                        DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                        broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Cus-DodgeRoll");
                    }
                    if (File.Exists("CustomImages/Checks/aerial.png"))
                    {
                        AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                        broadcast.AerialDodge.SetResourceReference(ContentProperty, "Cus-AerialDodge");
                    }
                    if (File.Exists("CustomImages/Checks/glide.png"))
                    {
                        Glide.SetResourceReference(ContentProperty, "Cus-Glide");
                        broadcast.Glide.SetResourceReference(ContentProperty, "Cus-Glide");
                    }

                    if (File.Exists("CustomImages/Checks/fire.png"))
                    {
                        Fire1.SetResourceReference(ContentProperty, "Cus-Fire");
                        Fire2.SetResourceReference(ContentProperty, "Cus-Fire");
                        Fire3.SetResourceReference(ContentProperty, "Cus-Fire");
                        broadcast.Fire.SetResourceReference(ContentProperty, "Cus-Fire");
                    }
                    if (File.Exists("CustomImages/Checks/blizzard.png"))
                    {
                        Blizzard1.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        Blizzard2.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        Blizzard3.SetResourceReference(ContentProperty, "Cus-Blizzard");
                        broadcast.Blizzard.SetResourceReference(ContentProperty, "Cus-Blizzard");
                    }
                    if (File.Exists("CustomImages/Checks/thunder.png"))
                    {
                        Thunder1.SetResourceReference(ContentProperty, "Cus-Thunder");
                        Thunder2.SetResourceReference(ContentProperty, "Cus-Thunder");
                        Thunder3.SetResourceReference(ContentProperty, "Cus-Thunder");
                        broadcast.Thunder.SetResourceReference(ContentProperty, "Cus-Thunder");
                    }
                    if (File.Exists("CustomImages/Checks/cure.png"))
                    {
                        Cure1.SetResourceReference(ContentProperty, "Cus-Cure");
                        Cure2.SetResourceReference(ContentProperty, "Cus-Cure");
                        Cure3.SetResourceReference(ContentProperty, "Cus-Cure");
                        broadcast.Cure.SetResourceReference(ContentProperty, "Cus-Cure");
                    }
                    if (File.Exists("CustomImages/Checks/reflect.png"))
                    {
                        Reflect1.SetResourceReference(ContentProperty, "Cus-Reflect");
                        Reflect2.SetResourceReference(ContentProperty, "Cus-Reflect");
                        Reflect3.SetResourceReference(ContentProperty, "Cus-Reflect");
                        broadcast.Reflect.SetResourceReference(ContentProperty, "Cus-Reflect");
                    }
                    if (File.Exists("CustomImages/Checks/magnet.png"))
                    {
                        Magnet1.SetResourceReference(ContentProperty, "Cus-Magnet");
                        Magnet2.SetResourceReference(ContentProperty, "Cus-Magnet");
                        Magnet3.SetResourceReference(ContentProperty, "Cus-Magnet");
                        broadcast.Magnet.SetResourceReference(ContentProperty, "Cus-Magnet");
                    }

                    if (File.Exists("CustomImages/Checks/torn_pages.png"))
                    {
                        TornPage1.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage2.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage3.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage4.SetResourceReference(ContentProperty, "Cus-TornPage");
                        TornPage5.SetResourceReference(ContentProperty, "Cus-TornPage");
                    }

                    if (File.Exists("CustomImages/Checks/valor.png"))
                    {
                        Valor.SetResourceReference(ContentProperty, "Cus-Valor");
                        ValorM.SetResourceReference(ContentProperty, "Cus-Valor");
                        broadcast.Valor.SetResourceReference(ContentProperty, "Cus-Valor");
                    }
                    if (File.Exists("CustomImages/Checks/wisdom.png"))
                    {
                        Wisdom.SetResourceReference(ContentProperty, "Cus-Wisdom");
                        WisdomM.SetResourceReference(ContentProperty, "Cus-Wisdom");
                        broadcast.Wisdom.SetResourceReference(ContentProperty, "Cus-Wisdom");
                    }
                    if (File.Exists("CustomImages/Checks/limit.png"))
                    {
                        Limit.SetResourceReference(ContentProperty, "Cus-Limit");
                        LimitM.SetResourceReference(ContentProperty, "Cus-Limit");
                        broadcast.Limit.SetResourceReference(ContentProperty, "Cus-Limit");
                    }
                    if (File.Exists("CustomImages/Checks/master.png"))
                    {
                        Master.SetResourceReference(ContentProperty, "Cus-Master");
                        MasterM.SetResourceReference(ContentProperty, "Cus-Master");
                        broadcast.Master.SetResourceReference(ContentProperty, "Cus-Master");
                    }
                    if (File.Exists("CustomImages/Checks/final.png"))
                    {
                        Final.SetResourceReference(ContentProperty, "Cus-Final");
                        FinalM.SetResourceReference(ContentProperty, "Cus-Final");
                        broadcast.Final.SetResourceReference(ContentProperty, "Cus-Final");
                    }

                    if (File.Exists("CustomImages/Checks/genie.png"))
                    {
                        Lamp.SetResourceReference(ContentProperty, "Cus-Genie");
                        broadcast.Lamp.SetResourceReference(ContentProperty, "Cus-Genie");
                    }
                    if (File.Exists("CustomImages/Checks/stitch.png"))
                    {
                        Ukulele.SetResourceReference(ContentProperty, "Cus-Stitch");
                        broadcast.Ukulele.SetResourceReference(ContentProperty, "Cus-Stitch");
                    }
                    if (File.Exists("CustomImages/Checks/chicken_little.png"))
                    {
                        Baseball.SetResourceReference(ContentProperty, "Cus-ChickenLittle");
                        broadcast.Baseball.SetResourceReference(ContentProperty, "Cus-ChickenLittle");
                    }
                    if (File.Exists("CustomImages/Checks/peter_pan.png"))
                    {
                        Feather.SetResourceReference(ContentProperty, "Cus-PeterPan");
                        broadcast.Feather.SetResourceReference(ContentProperty, "Cus-PeterPan");
                    }

                    if (File.Exists("CustomImages/Checks/proof_of_nonexistence.png"))
                    {
                        Nonexistence.SetResourceReference(ContentProperty, "Cus-ProofOfNon");
                        broadcast.Nonexistence.SetResourceReference(ContentProperty, "Cus-ProofOfNon");
                    }
                    if (File.Exists("CustomImages/Checks/proof_of_connection.png"))
                    {
                        Connection.SetResourceReference(ContentProperty, "Cus-ProofOfCon");
                        broadcast.Connection.SetResourceReference(ContentProperty, "Cus-ProofOfCon");
                    }
                    if (File.Exists("CustomImages/Checks/proof_of_tranquility.png"))
                    {
                        Peace.SetResourceReference(ContentProperty, "Cus-ProofOfPea");
                        broadcast.Peace.SetResourceReference(ContentProperty, "Cus-ProofOfPea");
                    }
                    if (File.Exists("CustomImages/Checks/promise_charm.png"))
                    {
                        PromiseCharm.SetResourceReference(ContentProperty, "Cus-PromiseCharm");
                        broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Cus-PromiseCharm");
                    }
                    if (File.Exists("CustomImages/Checks/once_more.png"))
                    {
                        OnceMore.SetResourceReference(ContentProperty, "Cus-OnceMore");
                        broadcast.OnceMore.SetResourceReference(ContentProperty, "Cus-OnceMore");
                    }
                    if (File.Exists("CustomImages/Checks/second_chance.png"))
                    {
                        SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");
                        broadcast.SecondChance.SetResourceReference(ContentProperty, "Cus-SecondChance");
                    }
                }

                if (CustomLevelFound)
                {
                    LevelIcon.SetResourceReference(ContentProperty, "Cus-LevelIcon");
                    broadcast.LevelIcon.SetResourceReference(ContentProperty, "Cus-LevelIcon");
                }

                if (CustomStrengthFound)
                {
                    StrengthIcon.SetResourceReference(ContentProperty, "Cus-StrengthIcon");
                    broadcast.StrengthIcon.SetResourceReference(ContentProperty, "Cus-StrengthIcon");
                }

                if (CustomMagicFound)
                {
                    MagicIcon.SetResourceReference(ContentProperty, "Cus-MagicIcon");
                    broadcast.MagicIcon.SetResourceReference(ContentProperty, "Cus-MagicIcon");
                }

                if (CustomDefenseFound)
                {
                    DefenseIcon.SetResourceReference(ContentProperty, "Cus-DefenseIcon");
                    broadcast.DefenseIcon.SetResourceReference(ContentProperty, "Cus-DefenseIcon");
                }

                //broadcast window specific
                if (File.Exists("CustomImages/Broadcast/Other/ansem_report.png"))
                {
                    broadcast.Report.SetResourceReference(ContentProperty, "Cus-B_AnsemReport");
                }
                if (File.Exists("CustomImages/Broadcast/Other/torn_pages.png"))
                {
                    broadcast.TornPage.SetResourceReference(ContentProperty, "Cus-B_TornPage");
                }
                if (File.Exists("CustomImages/Broadcast/Other/chest.png"))
                {
                    broadcast.Chest.SetResourceReference(ContentProperty, "Cus-B_Chest");
                }

                if (Directory.Exists("CustomImages/Broadcast/Checks/"))
                {
                    if (File.Exists("CustomImages/Broadcast/Checks/jump.png"))
                    {
                        broadcast.HighJump.SetResourceReference(ContentProperty, "Cus-B_HighJump");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/quick.png"))
                    {
                        broadcast.QuickRun.SetResourceReference(ContentProperty, "Cus-B_QuickRun");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/dodge.png"))
                    {
                        broadcast.DodgeRoll.SetResourceReference(ContentProperty, "Cus-B_DodgeRoll");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/aerial.png"))
                    {
                        broadcast.AerialDodge.SetResourceReference(ContentProperty, "Cus-B_AerialDodge");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/glide.png"))
                    {
                        broadcast.Glide.SetResourceReference(ContentProperty, "Cus-B_Glide");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/fire.png"))
                    {
                        broadcast.Fire.SetResourceReference(ContentProperty, "Cus-B_Fire");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/blizzard.png"))
                    {
                        broadcast.Blizzard.SetResourceReference(ContentProperty, "Cus-B_Blizzard");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/thunder.png"))
                    {
                        broadcast.Thunder.SetResourceReference(ContentProperty, "Cus-B_Thunder");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/cure.png"))
                    {
                        broadcast.Cure.SetResourceReference(ContentProperty, "Cus-B_Cure");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/reflect.png"))
                    {
                        broadcast.Reflect.SetResourceReference(ContentProperty, "Cus-B_Reflect");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/magnet.png"))
                    {
                        broadcast.Magnet.SetResourceReference(ContentProperty, "Cus-B_Magnet");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/valor.png"))
                    {
                        broadcast.Valor.SetResourceReference(ContentProperty, "Cus-B_Valor");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/wisdom.png"))
                    {
                        broadcast.Wisdom.SetResourceReference(ContentProperty, "Cus-B_Wisdom");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/limit.png"))
                    {
                        broadcast.Limit.SetResourceReference(ContentProperty, "Cus-B_Limit");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/master.png"))
                    {
                        broadcast.Master.SetResourceReference(ContentProperty, "Cus-B_Master");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/final.png"))
                    {
                        broadcast.Final.SetResourceReference(ContentProperty, "Cus-B_Final");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/genie.png"))
                    {
                        broadcast.Lamp.SetResourceReference(ContentProperty, "Cus-B_Genie");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/stitch.png"))
                    {
                        broadcast.Ukulele.SetResourceReference(ContentProperty, "Cus-B_Stitch");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/chicken_little.png"))
                    {
                        broadcast.Baseball.SetResourceReference(ContentProperty, "Cus-B_ChickenLittle");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/peter_pan.png"))
                    {
                        broadcast.Feather.SetResourceReference(ContentProperty, "Cus-B_PeterPan");
                    }

                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_nonexistence.png"))
                    {
                        broadcast.Nonexistence.SetResourceReference(ContentProperty, "Cus-B_ProofOfNon");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_connection.png"))
                    {
                        broadcast.Connection.SetResourceReference(ContentProperty, "Cus-B_ProofOfCon");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/proof_of_tranquility.png"))
                    {
                        broadcast.Peace.SetResourceReference(ContentProperty, "Cus-B_ProofOfPea");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/promise_charm.png"))
                    {
                        broadcast.PromiseCharm.SetResourceReference(ContentProperty, "Cus-B_PromiseCharm");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/once_more.png"))
                    {
                        broadcast.OnceMore.SetResourceReference(ContentProperty, "Cus-B_OnceMore");
                    }
                    if (File.Exists("CustomImages/Broadcast/Checks/second_chance.png"))
                    {
                        broadcast.SecondChance.SetResourceReference(ContentProperty, "Cus-B_SecondChance");
                    }
                }
            }
        }

        //What a mess this all is
        private void CustomWorldCheck()
        {
            {
                if (MinWorldOption.IsChecked)
                {
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

                    if (OCCupsOption.IsChecked)
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusCupsImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusCupsImage");
                    }
                    else
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Min-OlympusImage");
                    }
                }
                if (OldWorldOption.IsChecked)
                {
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

                    if (OCCupsOption.IsChecked)
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusCupsImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusCupsImage");
                    }
                    else
                    {
                        OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                        broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Old-OlympusImage");
                    }
                }
            }

            if (CustomFolderOption.IsChecked)
            {
                //Main Window
                //We set both main and broadcast window worlds to the same image here
                //this makes it so that the broadcast window uses the same custom images that the
                //main window uses unless the specific broadacst window world folder is found
                if (Directory.Exists("CustomImages/Worlds/"))
                {
                    if (File.Exists("CustomImages/Worlds/level.png"))
                    {
                        SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Worlds/simulated_twilight_town.png"))
                    {
                        SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-SimulatedImage");
                        broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-SimulatedImage");
                    }
                    if (File.Exists("CustomImages/Worlds/land_of_dragons.png"))
                    {
                        LandofDragons.SetResourceReference(ContentProperty, "Cus-LandofDragonsImage");
                        broadcast.LandofDragons.SetResourceReference(ContentProperty, "Cus-LandofDragonsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/pride_land.png"))
                    {
                        PrideLands.SetResourceReference(ContentProperty, "Cus-PrideLandsImage");
                        broadcast.PrideLands.SetResourceReference(ContentProperty, "Cus-PrideLandsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/halloween_town.png"))
                    {
                        HalloweenTown.SetResourceReference(ContentProperty, "Cus-HalloweenTownImage");
                        broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Cus-HalloweenTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/space_paranoids.png"))
                    {
                        SpaceParanoids.SetResourceReference(ContentProperty, "Cus-SpaceParanoidsImage");
                        broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Cus-SpaceParanoidsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/drive_form.png"))
                    {
                        DriveForms.SetResourceReference(ContentProperty, "Cus-DriveFormsImage");
                        broadcast.DriveForms.SetResourceReference(ContentProperty, "Cus-DriveFormsImage");
                    }
                    if (File.Exists("CustomImages/Worlds/twilight_town.png"))
                    {
                        TwilightTown.SetResourceReference(ContentProperty, "Cus-TwilightTownImage");
                        broadcast.TwilightTown.SetResourceReference(ContentProperty, "Cus-TwilightTownImage");
                    }
                    if (File.Exists("CustomImages/Worlds/beast's_castle.png"))
                    {
                        BeastsCastle.SetResourceReference(ContentProperty, "Cus-BeastCastleImage");
                        broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Cus-BeastCastleImage");
                    }
                    if (File.Exists("CustomImages/Worlds/agrabah.png"))
                    {
                        Agrabah.SetResourceReference(ContentProperty, "Cus-AgrabahImage");
                        broadcast.Agrabah.SetResourceReference(ContentProperty, "Cus-AgrabahImage");
                    }
                    if (File.Exists("CustomImages/Worlds/100_acre_wood.png"))
                    {
                        HundredAcreWood.SetResourceReference(ContentProperty, "Cus-HundredAcreImage");
                        broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Cus-HundredAcreImage");
                    }
                    if (File.Exists("CustomImages/Worlds/port_royal.png"))
                    {
                        PortRoyal.SetResourceReference(ContentProperty, "Cus-PortRoyalImage");
                        broadcast.PortRoyal.SetResourceReference(ContentProperty, "Cus-PortRoyalImage");
                    }
                    if (File.Exists("CustomImages/Worlds/the_world_that_never_was.png"))
                    {
                        TWTNW.SetResourceReference(ContentProperty, "Cus-TWTNWImage");
                        broadcast.TWTNW.SetResourceReference(ContentProperty, "Cus-TWTNWImage");
                    }
                    if (File.Exists("CustomImages/Worlds/atlantica.png"))
                    {
                        Atlantica.SetResourceReference(ContentProperty, "Cus-AtlanticaImage");
                        broadcast.Atlantica.SetResourceReference(ContentProperty, "Cus-AtlanticaImage");
                    }
                    if (File.Exists("CustomImages/Worlds/replica_data.png"))
                    {
                        GoA.SetResourceReference(ContentProperty, "Cus-GardenofAssemblageImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        //basically we only want to set the custom images if they exist and if the toggle is on/off
                        //otherwise they just use the defaults that were defined in the beggining of this function
                        if (File.Exists("CustomImages/Worlds/hollow_bastion.png") && !CavernOption.IsChecked)
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/hollow_bastion_cor.png") && CavernOption.IsChecked)
                        {
                            HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionCorImage");
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-HollowBastionCorImage");
                        }

                        if (File.Exists("CustomImages/Worlds/disney_castle.png") && !TimelessOption.IsChecked)
                        {
                            DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/disney_castle_tr.png") && TimelessOption.IsChecked)
                        {
                            DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleTrImage");
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-DisneyCastleTrImage");
                        }

                        if (File.Exists("CustomImages/Worlds/olympus_coliseum.png") && !OCCupsOption.IsChecked)
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusImage");
                        }
                        else if (File.Exists("CustomImages/Worlds/olympus_coliseum_cups.png") && OCCupsOption.IsChecked)
                        {
                            OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusCupsImage");
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-OlympusCupsImage");
                        }
                    }

                }

                //Broadcast Window
                if (Directory.Exists("CustomImages/Broadcast/Worlds/"))
                {
                    if (File.Exists("CustomImages/Broadcast/Worlds/level.png"))
                    {
                        broadcast.SorasHeart.SetResourceReference(ContentProperty, "Cus-B_SoraHeartImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/simulated_twilight_town.png"))
                    {
                        broadcast.SimulatedTwilightTown.SetResourceReference(ContentProperty, "Cus-B_SimulatedImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/land_of_dragons.png"))
                    {
                        broadcast.LandofDragons.SetResourceReference(ContentProperty, "Cus-B_LandofDragonsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/pride_land.png"))
                    {
                        broadcast.PrideLands.SetResourceReference(ContentProperty, "Cus-B_PrideLandsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/halloween_town.png"))
                    {
                        broadcast.HalloweenTown.SetResourceReference(ContentProperty, "Cus-B_HalloweenTownImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/space_paranoids.png"))
                    {
                        broadcast.SpaceParanoids.SetResourceReference(ContentProperty, "Cus-B_SpaceParanoidsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/drive_form.png"))
                    {
                        broadcast.DriveForms.SetResourceReference(ContentProperty, "Cus-B_DriveFormsImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/twilight_town.png"))
                    {
                        broadcast.TwilightTown.SetResourceReference(ContentProperty, "Cus-B_TwilightTownImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/beast's_castle.png"))
                    {
                        broadcast.BeastsCastle.SetResourceReference(ContentProperty, "Cus-B_BeastCastleImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/agrabah.png"))
                    {
                        broadcast.Agrabah.SetResourceReference(ContentProperty, "Cus-B_AgrabahImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/100_acre_wood.png"))
                    {
                        broadcast.HundredAcreWood.SetResourceReference(ContentProperty, "Cus-B_HundredAcreImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/port_royal.png"))
                    {
                        broadcast.PortRoyal.SetResourceReference(ContentProperty, "Cus-B_PortRoyalImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/the_world_that_never_was.png"))
                    {
                        broadcast.TWTNW.SetResourceReference(ContentProperty, "Cus-B_TWTNWImage");
                    }
                    if (File.Exists("CustomImages/Broadcast/Worlds/atlantica.png"))
                    {
                        broadcast.Atlantica.SetResourceReference(ContentProperty, "Cus-B_AtlanticaImage");
                    }

                    //check for custom cavern, timeless, and cups toggles
                    {
                        //basically we only want to set the custom images if they exist and if the toggle is on/off
                        //otherwise they just use the defaults that were defined in the beggining of this function
                        if (File.Exists("CustomImages/Broadcast/Worlds/hollow_bastion.png") && !CavernOption.IsChecked)
                        {
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-B_HollowBastionImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/hollow_bastion_cor.png") && CavernOption.IsChecked)
                        {
                            broadcast.HollowBastion.SetResourceReference(ContentProperty, "Cus-B_HollowBastionCorImage");
                        }
                        
                        if (File.Exists("CustomImages/Broadcast/Worlds/disney_castle.png") && !TimelessOption.IsChecked)
                        {
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/disney_castle_tr.png") && TimelessOption.IsChecked)
                        {
                            broadcast.DisneyCastle.SetResourceReference(ContentProperty, "Cus-B_DisneyCastleTrImage");
                        }

                        if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum.png") && !OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusImage");
                        }
                        else if (File.Exists("CustomImages/Broadcast/Worlds/olympus_coliseum_cups.png") && OCCupsOption.IsChecked)
                        {
                            broadcast.OlympusColiseum.SetResourceReference(ContentProperty, "Cus-B_OlympusCupsImage");
                        }
                    }

                }
            }
        }

        private void ReportNumCheck()
        {
            if (MinCheckOption.IsChecked && !CustomFolderOption.IsChecked)
            {
                if (OldNumOption.IsChecked)
                {
                    Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01_old");
                    Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02_old");
                    Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03_old");
                    Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04_old");
                    Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05_old");
                    Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06_old");
                    Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07_old");
                    Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08_old");
                    Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09_old");
                    Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10_old");
                    Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11_old");
                    Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12_old");
                    Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13_old");
                }
                else
                {
                    Report1.SetResourceReference(ContentProperty, "Min-AnsemReport01");
                    Report2.SetResourceReference(ContentProperty, "Min-AnsemReport02");
                    Report3.SetResourceReference(ContentProperty, "Min-AnsemReport03");
                    Report4.SetResourceReference(ContentProperty, "Min-AnsemReport04");
                    Report5.SetResourceReference(ContentProperty, "Min-AnsemReport05");
                    Report6.SetResourceReference(ContentProperty, "Min-AnsemReport06");
                    Report7.SetResourceReference(ContentProperty, "Min-AnsemReport07");
                    Report8.SetResourceReference(ContentProperty, "Min-AnsemReport08");
                    Report9.SetResourceReference(ContentProperty, "Min-AnsemReport09");
                    Report10.SetResourceReference(ContentProperty, "Min-AnsemReport10");
                    Report11.SetResourceReference(ContentProperty, "Min-AnsemReport11");
                    Report12.SetResourceReference(ContentProperty, "Min-AnsemReport12");
                    Report13.SetResourceReference(ContentProperty, "Min-AnsemReport13");
                }
            }

            if (OldCheckOption.IsChecked && !CustomFolderOption.IsChecked)
            {

                if (OldNumOption.IsChecked)
                {
                    Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01_old");
                    Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02_old");
                    Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03_old");
                    Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04_old");
                    Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05_old");
                    Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06_old");
                    Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07_old");
                    Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08_old");
                    Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09_old");
                    Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10_old");
                    Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11_old");
                    Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12_old");
                    Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13_old");
                }
                else
                {
                    Report1.SetResourceReference(ContentProperty, "Old-AnsemReport01");
                    Report2.SetResourceReference(ContentProperty, "Old-AnsemReport02");
                    Report3.SetResourceReference(ContentProperty, "Old-AnsemReport03");
                    Report4.SetResourceReference(ContentProperty, "Old-AnsemReport04");
                    Report5.SetResourceReference(ContentProperty, "Old-AnsemReport05");
                    Report6.SetResourceReference(ContentProperty, "Old-AnsemReport06");
                    Report7.SetResourceReference(ContentProperty, "Old-AnsemReport07");
                    Report8.SetResourceReference(ContentProperty, "Old-AnsemReport08");
                    Report9.SetResourceReference(ContentProperty, "Old-AnsemReport09");
                    Report10.SetResourceReference(ContentProperty, "Old-AnsemReport10");
                    Report11.SetResourceReference(ContentProperty, "Old-AnsemReport11");
                    Report12.SetResourceReference(ContentProperty, "Old-AnsemReport12");
                    Report13.SetResourceReference(ContentProperty, "Old-AnsemReport13");
                }
            }
        }

    }
}
