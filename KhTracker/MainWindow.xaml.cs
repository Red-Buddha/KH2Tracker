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

            data.Worlds.Add("SorasHeart", SorasHeart);
            data.Worlds.Add("DriveForms", DriveForms);
            data.Worlds.Add("SimulatedTwilightTown", SimulatedTwilightTown);
            data.Worlds.Add("TwilightTown", TwilightTown);
            data.Worlds.Add("HollowBastion", HollowBastion);
            data.Worlds.Add("BeastsCastle", BeastsCastle);
            data.Worlds.Add("OlympusColiseum", OlympusColiseum);
            data.Worlds.Add("Agrabah", Agrabah);
            data.Worlds.Add("LandofDragons", LandofDragons);
            data.Worlds.Add("HundredAcreWood", HundredAcreWood);
            data.Worlds.Add("PrideLands", PrideLands);
            data.Worlds.Add("DisneyCastle", DisneyCastle);
            data.Worlds.Add("HalloweenTown", HalloweenTown);
            data.Worlds.Add("PortRoyal", PortRoyal);
            data.Worlds.Add("SpaceParanoids", SpaceParanoids);
            data.Worlds.Add("TWTNW", TWTNW);
            data.Worlds.Add("Atlantica", Atlantica);
            data.Worlds.Add("GoA", GoA);

            data.Hints.Add("SorasHeart", SorasHeartHint);
            data.Hints.Add("DriveForms", DriveFormsHint);
            data.Hints.Add("SimulatedTwilightTown", SimulatedTwilightTownHint);
            data.Hints.Add("TwilightTown", TwilightTownHint);
            data.Hints.Add("HollowBastion", HollowBastionHint);
            data.Hints.Add("BeastsCastle", BeastsCastleHint);
            data.Hints.Add("OlympusColiseum", OlympusColiseumHint);
            data.Hints.Add("Agrabah", AgrabahHint);
            data.Hints.Add("LandofDragons", LandofDragonsHint);
            data.Hints.Add("HundredAcreWood", HundredAcreWoodHint);
            data.Hints.Add("PrideLands", PrideLandsHint);
            data.Hints.Add("DisneyCastle", DisneyCastleHint);
            data.Hints.Add("HalloweenTown", HalloweenTownHint);
            data.Hints.Add("PortRoyal", PortRoyalHint);
            data.Hints.Add("SpaceParanoids", SpaceParanoidsHint);
            data.Hints.Add("TWTNW", TWTNWHint);
            data.Hints.Add("Atlantica", AtlanticaHint);

            data.Grids.Add("SorasHeart", SorasHeartGrid);
            data.Grids.Add("DriveForms", DriveFormsGrid);
            data.Grids.Add("SimulatedTwilightTown", SimulatedTwilightTownGrid);
            data.Grids.Add("TwilightTown", TwilightTownGrid);
            data.Grids.Add("HollowBastion", HollowBastionGrid);
            data.Grids.Add("BeastsCastle", BeastsCastleGrid);
            data.Grids.Add("OlympusColiseum", OlympusColiseumGrid);
            data.Grids.Add("Agrabah", AgrabahGrid);
            data.Grids.Add("LandofDragons", LandofDragonsGrid);
            data.Grids.Add("HundredAcreWood", HundredAcreWoodGrid);
            data.Grids.Add("PrideLands", PrideLandsGrid);
            data.Grids.Add("DisneyCastle", DisneyCastleGrid);
            data.Grids.Add("HalloweenTown", HalloweenTownGrid);
            data.Grids.Add("PortRoyal", PortRoyalGrid);
            data.Grids.Add("SpaceParanoids", SpaceParanoidsGrid);
            data.Grids.Add("TWTNW", TWTNWGrid);
            data.Grids.Add("Atlantica", AtlanticaGrid);
            data.Grids.Add("GoA", GoAGrid);

            data.SelectedBars.Add("SorasHeart", SorasHeartBar);
            data.SelectedBars.Add("DriveForms", DriveFormsBar);
            data.SelectedBars.Add("SimulatedTwilightTown", SimulatedTwilightTownBar);
            data.SelectedBars.Add("TwilightTown", TwilightTownBar);
            data.SelectedBars.Add("HollowBastion", HollowBastionBar);
            data.SelectedBars.Add("BeastsCastle", BeastsCastleBar);
            data.SelectedBars.Add("OlympusColiseum", OlympusBar);
            data.SelectedBars.Add("Agrabah", AgrabahBar);
            data.SelectedBars.Add("LandofDragons", LandofDragonsBar);
            data.SelectedBars.Add("HundredAcreWood", HundredAcreWoodBar);
            data.SelectedBars.Add("PrideLands", PrideLandsBar);
            data.SelectedBars.Add("DisneyCastle", DisneyCastleBar);
            data.SelectedBars.Add("HalloweenTown", HalloweenTownBar);
            data.SelectedBars.Add("PortRoyal", PortRoyalBar);
            data.SelectedBars.Add("SpaceParanoids", SpaceParanoidsBar);
            data.SelectedBars.Add("TWTNW", TWTNWBar);
            data.SelectedBars.Add("Atlantica", AtlanticaBar);
            data.SelectedBars.Add("GoA", GoABar);

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

            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_QuestionMark.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_0.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_1.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_2.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_3.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_4.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_5.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_6.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_7.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_8.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/_9.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/10.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/11.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/12.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/13.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/14.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/15.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/16.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/17.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/18.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/19.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/20.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/21.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/22.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/23.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/24.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/25.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/26.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/27.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/28.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/29.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/30.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/31.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/32.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/33.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/34.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/35.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/36.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/37.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/38.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/39.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/40.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/41.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/42.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/43.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/44.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/45.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/46.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/47.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/48.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/49.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/50.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/51.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/52.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/53.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/54.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/55.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/56.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/57.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/58.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/59.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/60.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/61.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/62.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/63.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/64.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/65.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/66.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/67.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/68.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/69.png", UriKind.Relative)));
            data.Numbers.Add(new BitmapImage(new Uri("Images/Numbers/70.png", UriKind.Relative)));

            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/QuestionMark.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/0.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/1.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/2.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/3.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/4.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/5.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/6.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/7.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/8.png", UriKind.Relative)));
            data.SingleNumbers.Add(new BitmapImage(new Uri("Images/Numbers/9.png", UriKind.Relative)));

            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_QuestionMark.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_0.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_1.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_2.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_3.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_4.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_5.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_6.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_7.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_8.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/_9.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/10.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/11.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/12.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/13.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/14.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/15.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/16.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/17.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/18.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/19.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/20.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/21.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/22.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/23.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/24.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/25.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/26.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/27.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/28.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/29.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/30.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/31.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/32.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/33.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/34.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/35.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/36.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/37.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/38.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/39.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/40.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/41.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/42.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/43.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/44.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/45.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/46.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/47.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/48.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/49.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/50.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/51.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/52.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/53.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/54.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/55.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/56.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/57.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/58.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/59.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/60.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/61.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/62.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/63.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/64.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/65.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/66.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/67.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/68.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/69.png", UriKind.Relative)));
            data.BlueNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/70.png", UriKind.Relative)));

            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/QuestionMark.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/0.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/1.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/2.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/3.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/4.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/5.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/6.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/7.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/8.png", UriKind.Relative)));
            data.BlueSingleNumbers.Add(new BitmapImage(new Uri("Images/NumbersBlue/9.png", UriKind.Relative)));

            data.HintedWorlds.Add("SorasHeart", false);
            data.HintedWorlds.Add("DriveForms", false);
            data.HintedWorlds.Add("SimulatedTwilightTown", false);
            data.HintedWorlds.Add("TwilightTown", false);
            data.HintedWorlds.Add("HollowBastion", false);
            data.HintedWorlds.Add("BeastsCastle", false);
            data.HintedWorlds.Add("OlympusColiseum", false);
            data.HintedWorlds.Add("Agrabah", false);
            data.HintedWorlds.Add("LandofDragons", false);
            data.HintedWorlds.Add("HundredAcreWood", false);
            data.HintedWorlds.Add("PrideLands", false);
            data.HintedWorlds.Add("DisneyCastle", false);
            data.HintedWorlds.Add("HalloweenTown", false);
            data.HintedWorlds.Add("PortRoyal", false);
            data.HintedWorlds.Add("SpaceParanoids", false);
            data.HintedWorlds.Add("TWTNW", false);
            data.HintedWorlds.Add("Atlantica", false);
            data.HintedWorlds.Add("GoA", true);

            data.HintedHintWorlds.Add("SorasHeart", false);
            data.HintedHintWorlds.Add("DriveForms", false);
            data.HintedHintWorlds.Add("SimulatedTwilightTown", false);
            data.HintedHintWorlds.Add("TwilightTown", false);
            data.HintedHintWorlds.Add("HollowBastion", false);
            data.HintedHintWorlds.Add("BeastsCastle", false);
            data.HintedHintWorlds.Add("OlympusColiseum", false);
            data.HintedHintWorlds.Add("Agrabah", false);
            data.HintedHintWorlds.Add("LandofDragons", false);
            data.HintedHintWorlds.Add("HundredAcreWood", false);
            data.HintedHintWorlds.Add("PrideLands", false);
            data.HintedHintWorlds.Add("DisneyCastle", false);
            data.HintedHintWorlds.Add("HalloweenTown", false);
            data.HintedHintWorlds.Add("PortRoyal", false);
            data.HintedHintWorlds.Add("SpaceParanoids", false);
            data.HintedHintWorlds.Add("TWTNW", false);
            data.HintedHintWorlds.Add("Atlantica", false);

            data.WorldCheckCount.Add("SorasHeart", new List<string>());
            data.WorldCheckCount.Add("DriveForms", new List<string>());
            data.WorldCheckCount.Add("SimulatedTwilightTown", new List<string>());
            data.WorldCheckCount.Add("TwilightTown", new List<string>());
            data.WorldCheckCount.Add("HollowBastion", new List<string>());
            data.WorldCheckCount.Add("BeastsCastle", new List<string>());
            data.WorldCheckCount.Add("OlympusColiseum", new List<string>());
            data.WorldCheckCount.Add("Agrabah", new List<string>());
            data.WorldCheckCount.Add("LandofDragons", new List<string>());
            data.WorldCheckCount.Add("HundredAcreWood", new List<string>());
            data.WorldCheckCount.Add("PrideLands", new List<string>());
            data.WorldCheckCount.Add("DisneyCastle", new List<string>());
            data.WorldCheckCount.Add("HalloweenTown", new List<string>());
            data.WorldCheckCount.Add("PortRoyal", new List<string>());
            data.WorldCheckCount.Add("SpaceParanoids", new List<string>());
            data.WorldCheckCount.Add("TWTNW", new List<string>());
            data.WorldCheckCount.Add("Atlantica", new List<string>());

            data.WorldComplete.Add("SorasHeart", false);
            data.WorldComplete.Add("DriveForms", false);
            data.WorldComplete.Add("SimulatedTwilightTown", false);
            data.WorldComplete.Add("TwilightTown", false);
            data.WorldComplete.Add("HollowBastion", false);
            data.WorldComplete.Add("BeastsCastle", false);
            data.WorldComplete.Add("OlympusColiseum", false);
            data.WorldComplete.Add("Agrabah", false);
            data.WorldComplete.Add("LandofDragons", false);
            data.WorldComplete.Add("HundredAcreWood", false);
            data.WorldComplete.Add("PrideLands", false);
            data.WorldComplete.Add("DisneyCastle", false);
            data.WorldComplete.Add("HalloweenTown", false);
            data.WorldComplete.Add("PortRoyal", false);
            data.WorldComplete.Add("SpaceParanoids", false);
            data.WorldComplete.Add("TWTNW", false);
            data.WorldComplete.Add("Atlantica", false);

            data.WorldProgress.Add("SorasHeart", 0);
            data.WorldProgress.Add("DriveForms", 0);
            data.WorldProgress.Add("SimulatedTwilightTown", 0);
            data.WorldProgress.Add("TwilightTown", 0);
            data.WorldProgress.Add("HollowBastion", 0);
            data.WorldProgress.Add("BeastsCastle", 0);
            data.WorldProgress.Add("OlympusColiseum", 0);
            data.WorldProgress.Add("Agrabah", 0);
            data.WorldProgress.Add("LandofDragons", 0);
            data.WorldProgress.Add("HundredAcreWood", 0);
            data.WorldProgress.Add("PrideLands", 0);
            data.WorldProgress.Add("DisneyCastle", 0);
            data.WorldProgress.Add("HalloweenTown", 0);
            data.WorldProgress.Add("PortRoyal", 0);
            data.WorldProgress.Add("SpaceParanoids", 0);
            data.WorldProgress.Add("TWTNW", 0);
            data.WorldProgress.Add("Atlantica", 0);

            data.WorldsTop.Add("SorasHeart", SorasHeartTop);
            data.WorldsTop.Add("DriveForms", DriveFormsTop);
            data.WorldsTop.Add("SimulatedTwilightTown", SimulatedTwilightTownTop);
            data.WorldsTop.Add("TwilightTown", TwilightTownTop);
            data.WorldsTop.Add("HollowBastion", HollowBastionTop);
            data.WorldsTop.Add("BeastsCastle", BeastsCastleTop);
            data.WorldsTop.Add("OlympusColiseum", OlympusColiseumTop);
            data.WorldsTop.Add("Agrabah", AgrabahTop);
            data.WorldsTop.Add("LandofDragons", LandofDragonsTop);
            data.WorldsTop.Add("HundredAcreWood", HundredAcreWoodTop);
            data.WorldsTop.Add("PrideLands", PrideLandsTop);
            data.WorldsTop.Add("DisneyCastle", DisneyCastleTop);
            data.WorldsTop.Add("HalloweenTown", HalloweenTownTop);
            data.WorldsTop.Add("PortRoyal", PortRoyalTop);
            data.WorldsTop.Add("SpaceParanoids", SpaceParanoidsTop);
            data.WorldsTop.Add("TWTNW", TWTNWTop);
            data.WorldsTop.Add("GoA", GoATop);
            data.WorldsTop.Add("Atlantica", AtlanticaTop);
            
            data.Progression.Add("SimulatedTwilightTown", SimulatedTwilightTownProgression);
            data.Progression.Add("TwilightTown", TwilightTownProgression);
            data.Progression.Add("HollowBastion", HollowBastionProgression);
            data.Progression.Add("BeastsCastle", BeastsCastleProgression);
            data.Progression.Add("OlympusColiseum", OlympusColiseumProgression);
            data.Progression.Add("Agrabah", AgrabahProgression);
            data.Progression.Add("LandofDragons", LandofDragonsProgression);
            data.Progression.Add("HundredAcreWood", HundredAcreWoodProgression);
            data.Progression.Add("PrideLands", PrideLandsProgression);
            data.Progression.Add("DisneyCastle", DisneyCastleProgression);
            data.Progression.Add("HalloweenTown", HalloweenTownProgression);
            data.Progression.Add("PortRoyal", PortRoyalProgression);
            data.Progression.Add("SpaceParanoids", SpaceParanoidsProgression);
            data.Progression.Add("TWTNW", TWTNWProgression);
            data.Progression.Add("Atlantica", AtlanticaProgression);

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

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

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

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += (o, e) =>
            {
                timer.Stop();
                SetWindowSize();
            };
            timer.Start();
        }

        private void SetWindowSize()
        {
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;

            broadcast.Width = Properties.Settings.Default.BroadcastWindowWidth;
            broadcast.Height = Properties.Settings.Default.BroadcastWindowHeight;
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
                    data.SelectedBars[data.selected.Name].Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                }

                data.selected = button;
                data.SelectedBars[button.Name].Source = new BitmapImage(new Uri("Images\\VerticalBar.png", UriKind.Relative));
            }
            else if (e.ChangedButton == MouseButton.Middle)
            {
                if (data.Hints.ContainsKey(button.Name))
                {
                    data.Hints[button.Name].Source = new BitmapImage(new Uri("Images\\QuestionMark.png", UriKind.Relative));
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            if (data.Hints.ContainsKey(button.Name))
            {
                HandleReportValue(data.Hints[button.Name], e.Delta);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && data.selected != null)
            {
                if (data.Hints.ContainsKey(data.selected.Name))
                {
                    HandleReportValue(data.Hints[data.selected.Name], -1);
                }
            }
            if (e.Key == Key.PageUp && data.selected != null)
            {
                if (data.Hints.ContainsKey(data.selected.Name))
                {
                    HandleReportValue(data.Hints[data.selected.Name], 1);
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
            if (e.PreviousSize.Height == 0 && e.PreviousSize.Width == 0)
                return;

            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;
        }

        /// 
        /// Handle UI Changes
        /// 
        private void HandleReportValue(Image Hint, int delta)
        {
            if (data.hintsLoaded || data.mode == Mode.AltHints)
                return;

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

            // cap hint value to 51
            if (num > 52)
                num = 52;

            if (num < 1 && data.mode == Mode.AltHints)
                Hint.Source = data.Numbers[1];
            else if (num < 0)
                Hint.Source = data.Numbers[0];
            else
                Hint.Source = data.Numbers[num];

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num - 1);
        }

        public void SetReportValue(Image Hint, int value)
        {
            var numList = data.Numbers;

            string location = Hint.Name.Substring(0, Hint.Name.Length - 4);
            if (data.HintedHintWorlds[location] || data.WorldComplete[location])
                numList = data.BlueNumbers;

            if (value > 52)
                value = 52;

            if (value < 1 && data.mode == Mode.AltHints)
                Hint.Source = numList[1];
            else if (value < 0)
                Hint.Source = numList[0];
            else
                Hint.Source = numList[value];
            
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
