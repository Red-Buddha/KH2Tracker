using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

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
                    CheckTotal.Text = (int.Parse(CheckTotal.Text) + 1).ToString();
            }
            else if (toggle == false && button.IsEnabled == true)
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
                CheckTotal.Text = (int.Parse(CheckTotal.Text) - 1).ToString();

                button.HandleItemReturn();
            }
        }

        private void HandleWorldToggle(bool toggle, Button button, UniformGrid grid)
        {
            if (toggle && button.IsEnabled == false)
            {
                ((button.Parent as Grid).Parent as Grid).Height = Double.NaN;
                ((button.Parent as Grid).Parent as Grid).IsEnabled = true;
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else if (toggle == false && button.IsEnabled == true)
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

                ((button.Parent as Grid).Parent as Grid).Height = 0;
                ((button.Parent as Grid).Parent as Grid).IsEnabled = false;
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

                broadcast.Report.SetResourceReference(ContentProperty, "AnsemReport");
                broadcast.Peace.SetResourceReference(ContentProperty, "ProofOfPeace");
                broadcast.Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistence");
                broadcast.Connection.SetResourceReference(ContentProperty, "ProofOfConnection");
                broadcast.PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharm");
                broadcast.Fire.SetResourceReference(ContentProperty, "Fire");
                broadcast.Blizzard.SetResourceReference(ContentProperty, "Blizzard");
                broadcast.Thunder.SetResourceReference(ContentProperty, "Thunder");
                broadcast.Cure.SetResourceReference(ContentProperty, "Cure");
                broadcast.Reflect.SetResourceReference(ContentProperty, "Reflect");
                broadcast.Magnet.SetResourceReference(ContentProperty, "Magnet");
                broadcast.Valor.SetResourceReference(ContentProperty, "Valor");
                broadcast.Wisdom.SetResourceReference(ContentProperty, "Wisdom");
                broadcast.Limit.SetResourceReference(ContentProperty, "Limit");
                broadcast.Master.SetResourceReference(ContentProperty, "Master");
                broadcast.Final.SetResourceReference(ContentProperty, "Final");
                broadcast.Baseball.SetResourceReference(ContentProperty, "ChickenLittle");
                broadcast.Lamp.SetResourceReference(ContentProperty, "Genie");
                broadcast.Ukulele.SetResourceReference(ContentProperty, "Stitch");
                broadcast.Feather.SetResourceReference(ContentProperty, "PeterPan");
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

                broadcast.Report.SetResourceReference(ContentProperty, "AnsemReportOld");
                broadcast.Peace.SetResourceReference(ContentProperty, "ProofOfPeaceOld");
                broadcast.Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistenceOld");
                broadcast.Connection.SetResourceReference(ContentProperty, "ProofOfConnectionOld");
                broadcast.PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharmOld");
                broadcast.Fire.SetResourceReference(ContentProperty, "FireOld");
                broadcast.Blizzard.SetResourceReference(ContentProperty, "BlizzardOld");
                broadcast.Thunder.SetResourceReference(ContentProperty, "ThunderOld");
                broadcast.Cure.SetResourceReference(ContentProperty, "CureOld");
                broadcast.Reflect.SetResourceReference(ContentProperty, "ReflectOld");
                broadcast.Magnet.SetResourceReference(ContentProperty, "MagnetOld");
                broadcast.Valor.SetResourceReference(ContentProperty, "ValorOld");
                broadcast.Wisdom.SetResourceReference(ContentProperty, "WisdomOld");
                broadcast.Limit.SetResourceReference(ContentProperty, "LimitOld");
                broadcast.Master.SetResourceReference(ContentProperty, "MasterOld");
                broadcast.Final.SetResourceReference(ContentProperty, "FinalOld");
                broadcast.Baseball.SetResourceReference(ContentProperty, "ChickenLittleOld");
                broadcast.Lamp.SetResourceReference(ContentProperty, "GenieOld");
                broadcast.Ukulele.SetResourceReference(ContentProperty, "StitchOld");
                broadcast.Feather.SetResourceReference(ContentProperty, "PeterPanOld");
            }
        }

        private void WorldIconsToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WorldIcons = WorldIconsOption.IsChecked;
            if (WorldIconsOption.IsChecked)
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

                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp += item.Item_MouseUp;
                    }
                    else
                    {
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove += item.Item_MouseMove;

                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                    }
                }
            }
        }
    }
}
