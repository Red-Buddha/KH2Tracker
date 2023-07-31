using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Text.Json;
using Path = System.IO.Path;
using KhTracker.Hotkeys;
using System.Windows.Input;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using System.Security.Policy;
using System.Linq.Expressions;
using System.Windows.Markup;
using System.Reflection;
using System.Diagnostics.PerformanceData;
using MessageForm = System.Windows.Forms;

//using System.Text.Json.Serialization;
//using YamlDotNet.Serialization;

namespace KhTracker
{
    public partial class MainWindow : Window
    {
        /// 
        /// Save/load progress
        ///

        private void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Path.GetExtension(files[0]).ToUpper() == ".HINT")
                    LoadHints(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                    ParseSeed(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".ZIP")
                    OpenKHSeed(files[0]);
                else if (Path.GetExtension(files[0]).ToUpper() == ".TSV")
                    Load(files[0]);
            }
        }

        private void SaveProgress(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".tsv",
                Filter = "Tracker Save File (*.tsv)|*.tsv",
                FileName = "kh2fm-tracker-save",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName);
            }
        }

        public void Save(string filename)
        {
            #region Settings
            var settingInfo = new bool[31];
            //Display toggles
            settingInfo[0] = ReportsOption.IsChecked;
            settingInfo[1] = TornPagesOption.IsChecked;
            settingInfo[2] = PromiseCharmOption.IsChecked;
            settingInfo[3] = AbilitiesOption.IsChecked;
            settingInfo[4] = AntiFormOption.IsChecked;
            settingInfo[5] = VisitLockOption.IsChecked;
            settingInfo[6] = ExtraChecksOption.IsChecked;
            settingInfo[7] = SoraLevel01Option.IsChecked;
            settingInfo[8] = SoraLevel50Option.IsChecked;
            settingInfo[9] = SoraLevel99Option.IsChecked;
            //World toggles
            settingInfo[10] = SoraHeartOption.IsChecked;
            settingInfo[11] = DrivesOption.IsChecked;
            settingInfo[12] = SimulatedOption.IsChecked;
            settingInfo[13] = TwilightTownOption.IsChecked;
            settingInfo[14] = HollowBastionOption.IsChecked;
            settingInfo[15] = BeastCastleOption.IsChecked;
            settingInfo[16] = OlympusOption.IsChecked;
            settingInfo[17] = AgrabahOption.IsChecked;
            settingInfo[18] = LandofDragonsOption.IsChecked;
            settingInfo[19] = DisneyCastleOption.IsChecked;
            settingInfo[20] = PrideLandsOption.IsChecked;
            settingInfo[21] = PortRoyalOption.IsChecked;
            settingInfo[22] = HalloweenTownOption.IsChecked;
            settingInfo[23] = SpaceParanoidsOption.IsChecked;
            settingInfo[24] = TWTNWOption.IsChecked;
            settingInfo[25] = HundredAcreWoodOption.IsChecked;
            settingInfo[26] = AtlanticaOption.IsChecked;
            settingInfo[27] = SynthOption.IsChecked;
            settingInfo[28] = PuzzleOption.IsChecked;
            //other
            settingInfo[29] = GhostItemOption.IsChecked;
            settingInfo[30] = GhostMathOption.IsChecked;
            #endregion

            #region ReportInfo
            var attempsInfo = new int[13];
            for (int i = 0; i < 13; ++i)
            {
                int attempts = 3;
                if (data.hintsLoaded)
                    attempts = data.reportAttempts[i];

                attempsInfo[i] = attempts;
            }
            #endregion

            #region WorldInfo
            Dictionary<string, object> worldvalueInfo = new Dictionary<string, object>();
            foreach (string worldKey in data.WorldsData.Keys.ToList())
            {
                var worldData = data.WorldsData[worldKey];
                List<string> worldItems = new List<string>();
                foreach (Item item in worldData.worldGrid.Children)
                {
                    worldItems.Add(item.Name);
                }
                var testingthing = new
                {
                    //Value = worldData.value.Text, //do i need this?
                    //Progression = worldData.progress, //or this?
                    Items = worldItems
                    //Hinted = worldData.hinted,
                    //HintedHint = worldData.hintedHint,
                    //GhostHint = worldData.containsGhost,
                    //Complete = worldData.complete,
                    //Locks = worldData.visitLocks,
                };
                worldvalueInfo.Add(worldKey, testingthing);
            };
            #endregion

            #region Counters
            var counterInfo = new int[8]{1,1,1,1,1,1,0,0};
            counterInfo[0] = data.DriveLevels[0];
            counterInfo[1] = data.DriveLevels[1];
            counterInfo[2] = data.DriveLevels[2];
            counterInfo[3] = data.DriveLevels[3];
            counterInfo[4] = data.DriveLevels[4];
            if(stats != null)
                counterInfo[5] = stats.Level;
            counterInfo[6] = DeathCounter;
            counterInfo[7] = data.usedPages;
            #endregion

            FileStream file = File.Create(filename);
            StreamWriter writer = new StreamWriter(file);
            var saveInfo = new
            {
                Version = Title,
                SeedHash = data.seedHashVisual,
                Settings = settingInfo,
                SeedHints = data.openKHHintText,
                BossHints = data.openKHBossText,
                RandomSeed = data.convertedSeedHash,
                Worlds = worldvalueInfo,
                Reports = data.reportInformation,
                Attemps = attempsInfo,
                Counters = counterInfo,
                ForcedFinal = data.forcedFinal,
                Events = data.eventLog,
                BossEvents = data.bossEventLog,
                LegacyJsmartee = data.legacyJsmartee,
                LegacyJHints = data.hintFileText,
                LegacyShan = data.legacyShan,
                LegacySHints = data.shanHintFileText
            };

            var saveFinal = JsonSerializer.Serialize(saveInfo);
            string saveFinal64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(saveFinal));
            string saveScrambled = ScrambleText(saveFinal64, true);
            writer.WriteLine(saveScrambled);
            writer.Close();
        }

        private void LoadProgress(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".tsv",
                Filter = "Tracker Save File (*.tsv)|*.tsv",
                FileName = "kh2fm-tracker-save",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Load(openFileDialog.FileName);
            }
        }

        private void Load(string filename)
        {
            if (!InProgressCheck("tsv"))
                return;

            //open file
            StreamReader reader = new StreamReader(File.Open(filename, FileMode.Open));
            var savescrambled = reader.ReadLine();
            reader.Close();

            //start reading save
            var save64 = ScrambleText(savescrambled, false);
            var saveData = Encoding.UTF8.GetString(Convert.FromBase64String(save64));
            var saveObject = JsonSerializer.Deserialize<Dictionary<string, object>>(saveData);

            //check save version
            if (saveObject.ContainsKey("Version"))
            {
                string saveVer = saveObject["Version"].ToString();
                if (saveVer != Title)
                {
                    //Console.WriteLine("Different save version!");
                    string message = "This save was made with a different version of the tracker. " +
                        "\n Loading this may cause unintended effects. " +
                        "\n Do you still want to continue loading?";
                    string caption = "Save Version Mismatch";
                    MessageForm.MessageBoxButtons buttons = MessageForm.MessageBoxButtons.YesNo;
                    MessageForm.DialogResult result;
            
                    result = MessageForm.MessageBox.Show(message, caption, buttons);
                    if (result == MessageForm.DialogResult.No)
                    {
                        return;
                    }
                }
            }

            //reset
            OnReset(null, null);

            //check legacy hint styles
            if (saveObject.ContainsKey("LegacyJsmartee"))
            {
                if (saveObject["LegacyJsmartee"].ToString().ToLower() == "true")
                {
                    LoadLegacy(saveObject, "Jsmartee");
                    return;
                }
            }
            if (saveObject.ContainsKey("LegacyShan"))
            {
                if (saveObject["LegacyShan"].ToString().ToLower() == "true")
                {
                    LoadLegacy(saveObject, "Shan");
                    return;
                }
            }

            //continue to loading normally
            LoadNormal(saveObject);

            if (data.wasTracking)
            {
                InitTracker();
            }
        }

        private void LoadNormal(Dictionary<string, object> Savefile)
        {
            //Check Settings
            if (Savefile.ContainsKey("Settings"))
            {
                var setting = JsonSerializer.Deserialize<bool[]>(Savefile["Settings"].ToString());
                //Display toggles
                ReportsToggle(setting[0]);
                TornPagesToggle(setting[1]);
                PromiseCharmToggle(setting[2]);
                AbilitiesToggle(setting[3]);
                AntiFormToggle(setting[4]);
                VisitLockToggle(setting[5]);
                ExtraChecksToggle(setting[6]);
                if (setting[7])
                    SoraLevel01Toggle(true);
                else if (setting[8])
                    SoraLevel50Toggle(true);
                else if (setting[9])
                    SoraLevel99Toggle(true);
                //World toggles
                SoraHeartToggle(setting[10]);
                DrivesToggle(setting[11]);
                SimulatedToggle(setting[12]);
                TwilightTownToggle(setting[13]);
                HollowBastionToggle(setting[14]);
                BeastCastleToggle(setting[15]);
                OlympusToggle(setting[16]);
                AgrabahToggle(setting[17]);
                LandofDragonsToggle(setting[18]);
                DisneyCastleToggle(setting[19]);
                PrideLandsToggle(setting[20]);
                PortRoyalToggle(setting[21]);
                HalloweenTownToggle(setting[22]);
                SpaceParanoidsToggle(setting[23]);
                TWTNWToggle(setting[24]);
                HundredAcreWoodToggle(setting[25]);
                AtlanticaToggle(setting[26]);
                SynthToggle(setting[27]);
                PuzzleToggle(setting[28]);
                ////other
                //settingInfo[29] = GhostItemOption.IsChecked;
                //settingInfo[30] = GhostMathOption.IsChecked;
            }

            //check if enemy rando data exists
            if (Savefile.ContainsKey("BossHints"))
            {
                if (Savefile["BossHints"].ToString() != "None")
                {
                    data.BossRandoFound = true;
                    data.openKHBossText = Savefile["BossHints"].ToString();

                    var enemyText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHBossText));
                    try
                    {
                        var enemyObject = JsonSerializer.Deserialize<Dictionary<string, object>>(enemyText);
                        var bosses = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(enemyObject["BOSSES"].ToString());

                        foreach (var bosspair in bosses)
                        {
                            string bossOrig = bosspair["original"].ToString();
                            string bossRepl = bosspair["new"].ToString();

                            data.BossList.Add(bossOrig, bossRepl);
                        }
                    }
                    catch
                    {
                        data.BossRandoFound = false;
                        data.openKHBossText = "None";
                        App.logger?.Record("error while trying to parse bosses from save.");
                    }
                }
            }

            //use random seed from save
            if(Savefile.ContainsKey("RandomSeed"))
            {
                if (Savefile["RandomSeed"] != null)
                {
                    var seednumber = JsonSerializer.Deserialize<int>(Savefile["RandomSeed"].ToString());
                    data.convertedSeedHash = seednumber;
                }
            }

            //check hintsdata
            if (Savefile.ContainsKey("SeedHints"))
            {
                if (Savefile["SeedHints"].ToString() != "None")
                {
                    data.openKHHintText = Savefile["SeedHints"].ToString();
                    var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                    var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                    var settings = new List<string>();
                    var hintableItems = new List<string>();
                    //fallback for older seeds
                    try
                    {
                        hintableItems = new List<string>(JsonSerializer.Deserialize<List<string>>(hintObject["reveal"].ToString()));
                    }
                    catch
                    {
                        App.logger?.Record("Older seed. no reveal list. (This is probably fine)");
                    }

                    data.ShouldResetHash = false;

                    //if (hintObject.ContainsKey("generatorVersion"))
                    //{
                    //    data.seedgenVersion = hintObject["generatorVersion"].ToString();
                    //}

                    if (hintObject.ContainsKey("dummy_forms"))
                    {
                        if (hintObject["dummy_forms"].ToString() == "true")
                            data.altFinalTracking = true;
                    }

                    if (hintObject.ContainsKey("settings"))
                    {
                        settings = JsonSerializer.Deserialize<List<string>>(hintObject["settings"].ToString());

                        #region Settings

                        if (hintableItems.Contains("report"))
                            ReportsToggle(true);
                        else
                            ReportsToggle(false);

                        if (hintableItems.Contains("page"))
                            TornPagesToggle(true);
                        else
                            TornPagesToggle(false);

                        if (hintableItems.Contains("ability"))
                            AbilitiesToggle(true);
                        else
                            AbilitiesToggle(false);

                        if (hintableItems.Count == 0)
                        {
                            ReportsToggle(true);
                            TornPagesToggle(true);
                            AbilitiesToggle(true);
                        }

                        //item settings
                        PromiseCharmToggle(false);
                        //AbilitiesToggle(false);
                        VisitLockToggle(false);
                        ExtraChecksToggle(false);
                        AntiFormToggle(false);

                        //world settings
                        SoraHeartToggle(true);
                        DrivesToggle(false);
                        SimulatedToggle(false);
                        TwilightTownToggle(false);
                        HollowBastionToggle(false);
                        BeastCastleToggle(false);
                        OlympusToggle(false);
                        AgrabahToggle(false);
                        LandofDragonsToggle(false);
                        DisneyCastleToggle(false);
                        PrideLandsToggle(false);
                        PortRoyalToggle(false);
                        HalloweenTownToggle(false);
                        SpaceParanoidsToggle(false);
                        TWTNWToggle(false);
                        HundredAcreWoodToggle(false);
                        AtlanticaToggle(false);
                        PuzzleToggle(false);
                        SynthToggle(false);

                        //progression hints GoA Current Hint Count
                        data.WorldsData["GoA"].value.Visibility = Visibility.Hidden;

                        //settings visuals
                        SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
                        Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Absent_Split.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
                        Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
                        Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
                        Double SpacerValue = 10;
                        #endregion

                        //to be safe about this i guess
                        //bool abilitiesOn = true;
                        bool puzzleOn = false;
                        //bool synthOn = false;

                        //load settings from hints
                        foreach (string setting in settings)
                        {
                            Console.WriteLine("setting found = " + setting);

                            switch (setting)
                            {
                                //items
                                case "PromiseCharm":
                                    PromiseCharmToggle(true);
                                    break;
                                //case "Level1Mode":
                                //    abilitiesOn = false;
                                //    break;
                                case "visit_locking":
                                    VisitLockToggle(true);
                                    break;
                                case "extra_ics":
                                    ExtraChecksToggle(true);
                                    break;
                                case "Anti-Form":
                                    AntiFormToggle(true);
                                    break;
                                //worlds
                                case "Level":
                                    SoraHeartToggle(false);
                                    SoraLevel01Toggle(true);
                                    AbilitiesToggle(true);
                                    Setting_Level_01.Width = new GridLength(1.5, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "ExcludeFrom50":
                                    SoraLevel50Toggle(true);
                                    AbilitiesToggle(true);
                                    Setting_Level_50.Width = new GridLength(1.5, GridUnitType.Star);
                                    SpacerValue--;
                                    data.HintRevealOrder.Add("SorasHeart");
                                    break;
                                case "ExcludeFrom99":
                                    SoraLevel99Toggle(true);
                                    AbilitiesToggle(true);
                                    Setting_Level_99.Width = new GridLength(1.5, GridUnitType.Star);
                                    SpacerValue--;
                                    data.HintRevealOrder.Add("SorasHeart");
                                    break;
                                case "Simulated Twilight Town":
                                    SimulatedToggle(true);
                                    data.enabledWorlds.Add("STT");
                                    data.HintRevealOrder.Add("SimulatedTwilightTown");
                                    break;
                                case "Hundred Acre Wood":
                                    HundredAcreWoodToggle(true);
                                    data.enabledWorlds.Add("HundredAcreWood");
                                    data.HintRevealOrder.Add("HundredAcreWood");
                                    break;
                                case "Atlantica":
                                    AtlanticaToggle(true);
                                    data.enabledWorlds.Add("Atlantica");
                                    data.HintRevealOrder.Add("Atlantica");
                                    break;
                                case "Puzzle":
                                    PuzzleToggle(true);
                                    puzzleOn = true;
                                    data.puzzlesOn = true;
                                    break;
                                case "Synthesis":
                                    SynthToggle(true);
                                    //synthOn = true;
                                    data.synthOn = true;
                                    break;
                                case "Form Levels":
                                    DrivesToggle(true);
                                    data.HintRevealOrder.Add("DriveForms");
                                    break;
                                case "Land of Dragons":
                                    LandofDragonsToggle(true);
                                    data.enabledWorlds.Add("LoD");
                                    data.HintRevealOrder.Add("LandofDragons");
                                    break;
                                case "Beast's Castle":
                                    BeastCastleToggle(true);
                                    data.enabledWorlds.Add("BC");
                                    data.HintRevealOrder.Add("BeastsCastle");
                                    break;
                                case "Hollow Bastion":
                                    HollowBastionToggle(true);
                                    data.enabledWorlds.Add("HB");
                                    data.HintRevealOrder.Add("HollowBastion");
                                    break;
                                case "Twilight Town":
                                    TwilightTownToggle(true);
                                    data.enabledWorlds.Add("TT");
                                    data.HintRevealOrder.Add("TwilightTown");
                                    break;
                                case "The World That Never Was":
                                    TWTNWToggle(true);
                                    data.enabledWorlds.Add("TWTNW");
                                    data.HintRevealOrder.Add("TWTNW");
                                    break;
                                case "Space Paranoids":
                                    SpaceParanoidsToggle(true);
                                    data.enabledWorlds.Add("SP");
                                    data.HintRevealOrder.Add("SpaceParanoids");
                                    break;
                                case "Port Royal":
                                    PortRoyalToggle(true);
                                    data.enabledWorlds.Add("PR");
                                    data.HintRevealOrder.Add("PortRoyal");
                                    break;
                                case "Olympus Coliseum":
                                    OlympusToggle(true);
                                    data.enabledWorlds.Add("OC");
                                    data.HintRevealOrder.Add("OlympusColiseum");
                                    break;
                                case "Agrabah":
                                    AgrabahToggle(true);
                                    data.enabledWorlds.Add("AG");
                                    data.HintRevealOrder.Add("Agrabah");
                                    break;
                                case "Halloween Town":
                                    HalloweenTownToggle(true);
                                    data.enabledWorlds.Add("HT");
                                    data.HintRevealOrder.Add("HalloweenTown");
                                    break;
                                case "Pride Lands":
                                    PrideLandsToggle(true);
                                    data.enabledWorlds.Add("PL");
                                    data.HintRevealOrder.Add("PrideLands");
                                    break;
                                case "Disney Castle / Timeless River":
                                    DisneyCastleToggle(true);
                                    data.enabledWorlds.Add("DC");
                                    data.HintRevealOrder.Add("DisneyCastle");
                                    break;
                                //settings
                                case "better_stt":
                                    Setting_BetterSTT.Width = new GridLength(1.1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Cavern of Remembrance":
                                    Setting_Cavern.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Data Split":
                                    Setting_Absent_Split.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    data.dataSplit = true;
                                    break;
                                case "Absent Silhouettes":
                                    if (!data.dataSplit) //only use if we didn't already set the data split version
                                    {
                                        Setting_Absent.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                    }
                                    break;
                                case "Sephiroth":
                                    Setting_Sephiroth.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Lingering Will (Terra)":
                                    Setting_Terra.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Data Organization XIII":
                                    Setting_Datas.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Transport to Remembrance":
                                    Setting_Transport.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Olympus Cups":
                                    Setting_Cups.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "Hades Paradox Cup":
                                    Setting_HadesCup.Width = new GridLength(1, GridUnitType.Star);
                                    SpacerValue--;
                                    break;
                                case "ScoreMode":
                                    data.ScoreMode = true;
                                    break;
                                //progression hints
                                case "ProgressionHints":
                                    data.UsingProgressionHints = true;
                                    data.WorldsData["GoA"].value.Visibility = Visibility.Visible;
                                    data.WorldsData["GoA"].value.Text = "0";
                                    //Console.WriteLine("ENABLING PROGRESSION HINTS");
                                    break;
                            }
                        }

                        //if (abilitiesOn == false)
                        //    AbilitiesToggle(false);

                        //prevent creations hinting twice for progression
                        if ((puzzleOn || hintObject["hintsType"].ToString() == "Path") && !data.HintRevealOrder.Contains("PuzzSynth"))
                        {
                            data.HintRevealOrder.Add("PuzzSynth");
                        }

                        Setting_Spacer.Width = new GridLength(SpacerValue, GridUnitType.Star);
                        SettingsText.Text = "Settings:";

                    }

                    if (hintObject.ContainsKey("ProgressionType"))
                    {
                        data.progressionType = hintObject["ProgressionType"].ToString();
                    }

                    if (hintObject.ContainsKey("ProgressionSettings"))
                    {
                        if (data.progressionType != "Bosses")
                            data.progressionType = "Reports";

                        var progressionSettings = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(hintObject["ProgressionSettings"].ToString());

                        foreach (var setting in progressionSettings)
                        {
                            //Console.WriteLine("progression setting found = " + setting.Key);

                            switch (setting.Key)
                            {
                                case "HintCosts":
                                    data.HintCosts.Clear();
                                    foreach (int cost in setting.Value)
                                        data.HintCosts.Add(cost);
                                    data.HintCosts.Add(data.HintCosts[data.HintCosts.Count - 1] + 1); //duplicates the last cost for logic reasons
                                    break;
                                case "SimulatedTwilightTown":
                                    data.STT_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.STT_ProgressionValues.Add(cost);
                                    break;
                                case "TwilightTown":
                                    data.TT_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.TT_ProgressionValues.Add(cost);
                                    break;
                                case "HollowBastion":
                                    data.HB_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.HB_ProgressionValues.Add(cost);
                                    break;
                                case "CavernofRemembrance":
                                    data.CoR_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.CoR_ProgressionValues.Add(cost);
                                    break;
                                case "LandofDragons":
                                    data.LoD_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.LoD_ProgressionValues.Add(cost);
                                    break;
                                case "BeastsCastle":
                                    data.BC_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.BC_ProgressionValues.Add(cost);
                                    break;
                                case "OlympusColiseum":
                                    data.OC_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.OC_ProgressionValues.Add(cost);
                                    break;
                                case "DisneyCastle":
                                    data.DC_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.DC_ProgressionValues.Add(cost);
                                    break;
                                case "Agrabah":
                                    data.AG_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.AG_ProgressionValues.Add(cost);
                                    break;
                                case "PortRoyal":
                                    data.PR_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.PR_ProgressionValues.Add(cost);
                                    break;
                                case "HalloweenTown":
                                    data.HT_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.HT_ProgressionValues.Add(cost);
                                    break;
                                case "PrideLands":
                                    data.PL_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.PL_ProgressionValues.Add(cost);
                                    break;
                                case "HundredAcreWood":
                                    data.HAW_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.HAW_ProgressionValues.Add(cost);
                                    break;
                                case "SpaceParanoids":
                                    data.SP_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.SP_ProgressionValues.Add(cost);
                                    break;
                                case "TWTNW":
                                    data.TWTNW_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.TWTNW_ProgressionValues.Add(cost);
                                    break;
                                case "Atlantica":
                                    data.AT_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.AT_ProgressionValues.Add(cost);
                                    break;
                                case "ReportBonus":
                                    data.ReportBonus = setting.Value[0];
                                    break;
                                case "WorldCompleteBonus":
                                    data.WorldCompleteBonus = setting.Value[0];
                                    break;
                                case "Levels":
                                    data.Levels_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.Levels_ProgressionValues.Add(cost);
                                    break;
                                case "Drives":
                                    data.Drives_ProgressionValues.Clear();
                                    foreach (int cost in setting.Value)
                                        data.Drives_ProgressionValues.Add(cost);
                                    break;
                                case "FinalXemnasReveal":
                                    data.revealFinalXemnas = setting.Value[0] == 0 ? false : true;
                                    break;
                            }
                        }
                        //data.NumOfHints = data.HintCosts.Count;
                        //set text correctly
                        ProgressionCollectedValue.Visibility = Visibility.Visible;
                        ProgressionCollectedBar.Visibility = Visibility.Visible;
                        ProgressionCollectedValue.Text = "0";
                        ProgressionTotalValue.Text = data.HintCosts[0].ToString();
                    }

                    switch (hintObject["hintsType"].ToString())
                    {
                        case "Shananas":
                            {
                                SetMode(Mode.OpenKHShanHints);
                                ShanHints(hintObject);
                            }
                            break;
                        case "JSmartee":
                            {
                                SetMode(Mode.OpenKHJsmarteeHints);
                                JsmarteeHints(hintObject);
                            }
                            break;
                        case "Points":
                            {
                                SetMode(Mode.PointsHints);
                                PointsHints(hintObject);
                            }
                            break;
                        case "Path":
                            {
                                SetMode(Mode.PathHints);
                                PathHints(hintObject);
                            }
                            break;
                        case "Spoiler":
                            {
                                SetMode(Mode.SpoilerHints);
                                SpoilerHints(hintObject);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }

            //replace the hint text with the one in the save
            //why? i dunno might be important incase the way i gen boss hints changes or somethin
            if (Savefile.ContainsKey("Reports"))
            {
                var reportInfo = JsonSerializer.Deserialize<List<Tuple<string, string, int>>>(Savefile["Reports"].ToString());
                data.reportInformation = reportInfo;
            }

            //forced final check (unsure if this will actually help with it not mistracking)
            if (Savefile.ContainsKey("ForcedFinal"))
            {
                string forced = Savefile["ForcedFinal"].ToString().ToLower();
                if (forced == "true")
                    data.forcedFinal = true;
                else
                    data.forcedFinal = false;
            }

            //track obtained items
            if (Savefile.ContainsKey("Worlds"))
            {
                var worlds = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(Savefile["Worlds"].ToString());
                foreach (var world in worlds)
                {
                    var itemlist = JsonSerializer.Deserialize<List<string>>(world.Value["Items"].ToString());
                    foreach (string item in itemlist)
                    {
                        WorldGrid grid = FindName(world.Key + "Grid") as WorldGrid;
                        Item importantCheck = FindName(item) as Item;
                        if (grid.ReportHandler(importantCheck))
                        {
                            //add items, skip ghosts. ghosts are always added by reports anyway
                            if (!item.StartsWith("Ghost_"))
                                grid.Add_Item(importantCheck);
                        }
                    }
                }
            }

            //track events/progression
            if (Savefile.ContainsKey("Events"))
            {
                var eventlist = JsonSerializer.Deserialize<List<Tuple<string, int, int, int, int, int>>>(Savefile["Events"].ToString());
                for (int i = 0; i < eventlist.Count; ++i)
                {
                    UpdateWorldProgress(null, true, eventlist[i]);
                }
            }

            //track boss events
            if (Savefile.ContainsKey("BossEvents") && data.BossRandoFound)
            {
                var bossEventlist = JsonSerializer.Deserialize<List<Tuple<string, int, int, int, int, int>>>(Savefile["BossEvents"].ToString());
                for (int i = 0; i < bossEventlist.Count; ++i)
                {
                    GetBoss(null, true, bossEventlist[i]);
                }
            }

            //fix counters
            if (Savefile.ContainsKey("Counters"))
            {
                var counters = JsonSerializer.Deserialize<int[]>(Savefile["Counters"].ToString());
                for (int i = 0; i < counters.Length; ++i)
                {
                    if (i < 5)
                    {
                        FakeDrivesProgressionBonus(i, counters[i]);
                    }
                    else if (i == 5)
                    {
                        //need to add sora levels one at a time to get points correctly
                        for (int l = 0; l < counters[i]; ++l)
                        {
                            FakeLevelsProgressionBonus(l+1);
                        }
                    }
                }
                DeathCounter = counters[6];
                data.usedPages = counters[7];
            }

            //fix reports attempts
            if (Savefile.ContainsKey("Attemps"))
            {
                var attempts = JsonSerializer.Deserialize<int[]>(Savefile["Attemps"].ToString());
                string[] failNames = new string[4]{ "Fail3", "Fail2", "Fail1", "Fail0"};

                for (int i = 0; i < 13; ++i)
                {
                    data.ReportAttemptVisual[i].SetResourceReference(ContentControl.ContentProperty, failNames[attempts[i]]);
                    data.reportAttempts[i] = attempts[i];
                }
            }

            //check hash
            if (Savefile.ContainsKey("SeedHash"))
            {
                if (Savefile["SeedHash"] != null)
                {
                    try
                    {
                        var hash = JsonSerializer.Deserialize<string[]>(Savefile["SeedHash"].ToString());
                        data.seedHashVisual = hash;

                        //Set Icons
                        HashIcon1.SetResourceReference(ContentProperty, hash[0]);
                        HashIcon2.SetResourceReference(ContentProperty, hash[1]);
                        HashIcon3.SetResourceReference(ContentProperty, hash[2]);
                        HashIcon4.SetResourceReference(ContentProperty, hash[3]);
                        HashIcon5.SetResourceReference(ContentProperty, hash[4]);
                        HashIcon6.SetResourceReference(ContentProperty, hash[5]);
                        HashIcon7.SetResourceReference(ContentProperty, hash[6]);
                        data.SeedHashLoaded = true;

                        //make visible
                        if (SeedHashOption.IsChecked)
                        {
                            SetHintText("");
                            HashGrid.Visibility = Visibility.Visible;
                        }
                    }
                    catch
                    {
                        data.seedHashVisual = null;
                        HashGrid.Visibility = Visibility.Hidden;
                        App.logger?.Record("error while trying to parse seed hash. text corrupted?");
                    }
                }

            }

            //end of loading
            data.saveFileLoaded = true;
        }

        private void LoadLegacy(Dictionary<string, object> Savefile, string LegacyType)
        {
            if (LegacyType == "Jsmartee")
            {
                SetMode(Mode.JsmarteeHints);

                var hintStrings = JsonSerializer.Deserialize<string[]>(Savefile["LegacyJHints"].ToString());
                string line1 = hintStrings[0];
                data.hintFileText[0] = line1;
                string[] reportvalues = line1.Split('.');

                string line2 = hintStrings[1];
                data.hintFileText[1] = line2;
                line2 = line2.TrimEnd('.');
                string[] reportorder = line2.Split('.');

                string line3 = hintStrings[2];
                data.hintFileText[2] = line3;
                LoadSettings(line3);

                for (int i = 0; i < reportorder.Length; ++i)
                {
                    string location = data.codes.FindCode(reportorder[i]);
                    if (location == "")
                        location = data.codes.GetDefault(i);

                    data.reportLocations.Add(location);
                    string[] temp = reportvalues[i].Split(',');
                    data.reportInformation.Add(new Tuple<string, string, int>(null, data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
                }

                //end of loading
                data.hintsLoaded = true;
                data.legacyJsmartee = true;
                data.saveFileLoaded = true;
            }
            else
            {
                //bool autotrackeron = false;
                //bool ps2tracking = false;
                ////check for autotracking on and which version
                //if (aTimer != null)
                //    autotrackeron = true;
                //
                //if (pcsx2tracking)
                //    ps2tracking = true;

                //FixDictionary();
                SetMode(Mode.ShanHints);

                var hintStrings = JsonSerializer.Deserialize<string[]>(Savefile["LegacySHints"].ToString());

                if (data.shanHintFileText != null)
                {
                    data.shanHintFileText = null;
                }

                foreach (string world in data.WorldsData.Keys.ToList())
                {
                    data.WorldsData[world].checkCount.Clear();
                }

                bool check1 = false;
                bool check2 = false;
                for (int i = 0; i < hintStrings.Length; ++i)
                {
                    string line = hintStrings[i];
                    data.shanHintFileText[i] = line;

                    // ignore comment lines
                    if (line.Length >= 2 && line[0] == '/' && line[1] == '/')
                        continue;

                    string[] codes = line.Split(',');
                    if (codes.Length == 5)
                    {
                        string world = data.codes.FindCode(codes[2]);

                        //stupid fix
                        string[] idCode = codes[4].Split('/', ' ');

                        int id = Convert.ToInt32(idCode[0], 16);
                        if (world == "" || world == "GoA" || data.codes.itemCodes.ContainsKey(id) == false || (id >= 226 && id <= 238))
                            continue;

                        string item = data.codes.itemCodes[Convert.ToInt32(codes[4], 16)];
                        data.WorldsData[world].checkCount.Add(item);
                    }
                    else if (codes.Length == 1)
                    {
                        if (codes[0] == "//Remove High Jump LVl" || codes[0] == "//Remove Quick Run LVl")
                        {
                            check1 = true;
                        }
                        else if (codes[0] == "//Remove Dodge Roll LVl")
                        {
                            check2 = true;
                        }
                    }
                }
                data.legacyShan = true;

                if (check1 == true && check2 == false)
                {
                    foreach (string world in data.WorldsData.Keys.ToList())
                    {
                        data.WorldsData[world].checkCount.Clear();
                    }
                }

                foreach (var key in data.WorldsData.Keys.ToList())
                {
                    if (key == "GoA")
                        continue;

                    data.WorldsData[key].worldGrid.WorldComplete();
                    SetWorldValue(data.WorldsData[key].value, 0);
                }

                //end of loading
                data.saveFileLoaded = true;

                //if (autotrackeron)
                //{
                //    InitAutoTracker(ps2tracking);
                //}
            }
        }

        //progress helpers

        private string ScrambleText(string input, bool scramble)
        {
            //scrambles/unscrambles input text based on a seed
            //why have this? i dunno i suppose to make saves more "secure"
            //figure if people really want to cheat they would have to look at this code
            Random r = new Random(16964); //why this number? who knows... (let me know if you figure it out lol)
            if (scramble)
            {
                char[] chars = input.ToArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    int randomIndex = r.Next(0, chars.Length);
                    char temp = chars[randomIndex];
                    chars[randomIndex] = chars[i];
                    chars[i] = temp;
                }
                return new string(chars);
            }
            else
            {
                char[] scramChars = input.ToArray();
                List<int> swaps = new List<int>();
                for (int i = 0; i < scramChars.Length; i++)
                {
                    swaps.Add(r.Next(0, scramChars.Length));
                }
                for (int i = scramChars.Length - 1; i >= 0; i--)
                {
                    char temp = scramChars[swaps[i]];
                    scramChars[swaps[i]] = scramChars[i];
                    scramChars[i] = temp;
                }
                return new string(scramChars);
            }
        }

        //trigger autotracker specific stuff with save loading
        private void FakeDrivesProgressionBonus(int drive, int level)
        {
            if (!data.UsingProgressionHints)
                return;

            while (drive == 0 && (level > data.DriveLevels[0]))
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[0] - 1]);
                data.DriveLevels[0]++;
            }
            while (drive == 1 && (level > data.DriveLevels[1]))
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[1] - 1]);
                data.DriveLevels[1]++;
            }
            while (drive == 2 && (level > data.DriveLevels[2]))
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[2] - 1]);
                data.DriveLevels[2]++;
            }
            while (drive == 3 && (level > data.DriveLevels[3]))
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[3] - 1]);
                data.DriveLevels[3]++;
            }
            while (drive == 4 && (level > data.DriveLevels[4]))
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[4] - 1]);
                data.DriveLevels[4]++;
            }
        }

        private void FakeLevelsProgressionBonus(int level)
        {
            //if sora's current level is great than the max specified level (usually 50), then do nothing
            if (level > (data.Levels_ProgressionValues.Count * 10) || !data.UsingProgressionHints)
                return;

            //every 10 levels, reward the player the progression points for that part
            while (level > data.NextLevelMilestone)
            {
                data.NextLevelMilestone += 10;
                AddProgressionPoints(data.Levels_ProgressionValues[data.LevelsPreviousIndex++]);
            }
        }

        /// 
        /// Load hints
        ///

        //Shans Classic
        private void ParseSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".pnach",
                Filter = "pnach files (*.pnach)|*.pnach",
                Title = "Select Seed File"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ParseSeed(openFileDialog.FileName);
            }
        }

        public void ParseSeed(string filename)
        {
            if (!InProgressCheck("seed"))
                return;

            //bool autotrackeron = false;
            //bool ps2tracking = false;
            //check for autotracking on and which version
            //if (aTimer != null)
            //    autotrackeron = true;
            //
            //if (pcsx2tracking)
            //    ps2tracking = true;

            //FixDictionary();
            SetMode(Mode.ShanHints);

            if (data.shanHintFileText != null)
            {
                data.shanHintFileText = null;
            }

            foreach (string world in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[world].checkCount.Clear();
            }

            StreamReader streamReader = new StreamReader(filename);
            bool check1 = false;
            bool check2 = false;

            int lineNum = 0;
            while (streamReader.EndOfStream == false)
            {
                string line = streamReader.ReadLine();
                data.shanHintFileText[lineNum] = line;

                // ignore comment lines
                if (line.Length >= 2 && line[0] == '/' && line[1] == '/')
                    continue;

                string[] codes = line.Split(',');
                if (codes.Length == 5)
                {
                    string world = data.codes.FindCode(codes[2]);

                    //stupid fix
                    string[] idCode = codes[4].Split('/', ' ');

                    int id = Convert.ToInt32(idCode[0], 16);
                    if (world == "" || world == "GoA" || data.codes.itemCodes.ContainsKey(id) == false || (id >= 226 && id <= 238))
                        continue;

                    string item = data.codes.itemCodes[Convert.ToInt32(codes[4], 16)];
                    data.WorldsData[world].checkCount.Add(item);
                }
                else if (codes.Length == 1)
                {
                    if (codes[0] == "//Remove High Jump LVl" || codes[0] == "//Remove Quick Run LVl")
                    {
                        check1 = true;
                    }
                    else if (codes[0] == "//Remove Dodge Roll LVl")
                    {
                        check2 = true;
                    }
                }

                lineNum++;
            }
            streamReader.Close();
            data.legacyShan = true;

            if (check1 == true && check2 == false)
            {
                foreach (string world in data.WorldsData.Keys.ToList())
                {
                    data.WorldsData[world].checkCount.Clear();
                }
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            data.seedLoaded = true;

            if (data.wasTracking)
                InitTracker();
        }

        //Jsmartee Classic
        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".hint",
                Filter = "Jsmartee hint file (*.hint)|*.hint",
                Title = "Select Hint File"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                LoadHints(openFileDialog.FileName);
            }
        }

        public void LoadHints(string filename)
        {
            if (!InProgressCheck("hints"))
                return;

            SetMode(Mode.JsmarteeHints);
            //ResetHints();

            StreamReader streamReader = new StreamReader(filename);

            if (streamReader.EndOfStream)
            {
                SetHintText("Error loading hints");
                streamReader.Close();
                return;
            }

            string line1 = streamReader.ReadLine();
            data.hintFileText[0] = line1;
            string[] reportvalues = line1.Split('.');

            if (streamReader.EndOfStream)
            {
                SetHintText("Error loading hints");
                streamReader.Close();
                return;
            }

            string line2 = streamReader.ReadLine();
            data.hintFileText[1] = line2;
            line2 = line2.TrimEnd('.');
            string[] reportorder = line2.Split('.');

            string line3 = streamReader.ReadLine().Substring(24);
            data.hintFileText[2] = line3;
            LoadSettings(line3);

            streamReader.Close();

            for (int i = 0; i < reportorder.Length; ++i)
            {
                string location = data.codes.FindCode(reportorder[i]);
                if (location == "")
                    location = data.codes.GetDefault(i);

                data.reportLocations.Add(location);
                string[] temp = reportvalues[i].Split(',');
                data.reportInformation.Add(new Tuple<string, string, int>(null, data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
            }

            data.hintsLoaded = true;
            data.legacyJsmartee = true;

            if (data.wasTracking)
                InitTracker();
        }

        //Seed Gen
        private void OpenKHSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".zip",
                Filter = "OpenKH Seeds (*.zip)|*.zip",
                Title = "Select Seed File"
            };
            if (openFileDialog.ShowDialog() == true)
                OpenKHSeed(openFileDialog.FileName);
        }

        private void OpenKHSeed(string filename)
        {
            if (!InProgressCheck("seed"))
                return;

            using (ZipArchive archive = ZipFile.OpenRead(filename))
            {
                ZipArchiveEntry hintsfile = null;
                ZipArchiveEntry hashfile = null;
                ZipArchiveEntry hashfileBackup = null;
                ZipArchiveEntry enemyfile = null;

                //get and temp store these files to grab data from later.
                //we used to just read them as we went along, but things got more complicated as time went on..
                foreach (var entry in archive.Entries)
                {
                    switch (entry.Name)
                    {
                        case "HintFile.Hints":
                            hintsfile = entry;
                            break;
                        case "enemies.rando":
                            enemyfile = entry;
                            break;
                        case "randoseed-hash-icons.csv":
                            hashfile = entry;
                            break;
                        case "sys.yml":
                            hashfileBackup = entry;
                            break;
                        default:
                            break;
                    }
                }

                if (enemyfile != null)
                {
                    using (var reader3 = new StreamReader(enemyfile.Open()))
                    {
                        data.BossRandoFound = true;
                        data.openKHBossText = reader3.ReadToEnd();
                        var enemyText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHBossText));
                        try
                        {
                            var enemyObject = JsonSerializer.Deserialize<Dictionary<string, object>>(enemyText);
                            var bosses = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(enemyObject["BOSSES"].ToString());

                            foreach (var bosspair in bosses)
                            {
                                string bossOrig = bosspair["original"].ToString();
                                string bossRepl = bosspair["new"].ToString();

                                data.BossList.Add(bossOrig, bossRepl);
                            }
                        }
                        catch
                        {
                            data.BossRandoFound = false;
                            data.openKHBossText = "None";
                            App.logger?.Record("error while trying to parse bosses.");
                        }

                        reader3.Close();
                    }
                }

                if (hashfile != null || hashfileBackup != null)
                {
                    string[] hash = null;
                    //new method
                    if (hashfile != null)
                    {
                        using (var reader = new StreamReader(hashfile.Open()))
                        {
                            string[] separatingStrings = { "," };
                            string text = reader.ReadToEnd();
                            hash = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                            reader.Close();
                        }
                    }
                    //old method
                    else if (hashfileBackup != null)
                    {
                        using (var readerB = new StreamReader(hashfileBackup.Open()))
                        {
                            string[] separatingStrings = { "- en: ", " ", "'", "{", "}", ":", "icon" };
                            string text1 = readerB.ReadLine();
                            string text2 = readerB.ReadLine();
                            string text = text1 + text2;
                            hash = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                            readerB.Close();
                        }
                    }
                    //load hash visual
                    if (hash != null)
                    {
                        //Set Icons
                        HashIcon1.SetResourceReference(ContentProperty, hash[0]);
                        HashIcon2.SetResourceReference(ContentProperty, hash[1]);
                        HashIcon3.SetResourceReference(ContentProperty, hash[2]);
                        HashIcon4.SetResourceReference(ContentProperty, hash[3]);
                        HashIcon5.SetResourceReference(ContentProperty, hash[4]);
                        HashIcon6.SetResourceReference(ContentProperty, hash[5]);
                        HashIcon7.SetResourceReference(ContentProperty, hash[6]);
                        data.SeedHashLoaded = true;
                        data.seedHashVisual = hash;

                        //make visible
                        if (SeedHashOption.IsChecked)
                        {
                            SetHintText("");
                            HashGrid.Visibility = Visibility.Visible;
                        }

                        HashToSeed(hash);
                    }
                }

                if (hintsfile != null)
                {
                    using (StreamReader reader = new StreamReader(hintsfile.Open()))
                    {
                        data.openKHHintText = reader.ReadToEnd();
                        var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                        var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
                        var settings = new List<string>();
                        var hintableItems = new List<string>();
                        //fallback for older seeds
                        try
                        {
                            hintableItems = new List<string>(JsonSerializer.Deserialize<List<string>>(hintObject["hintableItems"].ToString()));
                        }
                        catch { }

                        data.ShouldResetHash = false;

                        if (hintObject.ContainsKey("generatorVersion"))
                        {
                            data.seedgenVersion = hintObject["generatorVersion"].ToString();
                        }

                        if (hintObject.ContainsKey("settings"))
                        {
                            settings = JsonSerializer.Deserialize<List<string>>(hintObject["settings"].ToString());

                            #region Settings

                            TornPagesToggle(false);
                            AbilitiesToggle(false);
                            ReportsToggle(false);
                            ExtraChecksToggle(false);
                            VisitLockToggle(false);
                            foreach (string item in hintableItems)
                            {
                                switch (item)
                                {
                                    case "magic":
                                        break;
                                    case "form":
                                        break;
                                    case "summon":
                                        break;
                                    case "page":
                                        TornPagesToggle(true);
                                        break;
                                    case "ability":
                                        AbilitiesToggle(true);
                                        break;
                                    case "report":
                                        ReportsToggle(true);
                                        break;
                                    case "other":
                                        ExtraChecksToggle(true);
                                        break;
                                    case "visit":
                                        VisitLockToggle(true);
                                        break;
                                    case "proof":
                                        break;
                                }
                            }

                            //if (hintableItems.Contains("report"))
                            //    ReportsToggle(true);
                            //else
                            //    ReportsToggle(false);
                            //
                            //if (hintableItems.Contains("page"))
                            //    TornPagesToggle(true);
                            //else
                            //    TornPagesToggle(false);
                            //
                            //if (hintableItems.Contains("ability"))
                            //    AbilitiesToggle(true);
                            //else
                            //    AbilitiesToggle(false);
                            //
                            //if (hintableItems.Count == 0)
                            //{
                            //    ReportsToggle(true);
                            //    TornPagesToggle(true);
                            //    AbilitiesToggle(true);
                            //}

                            //item settings
                            PromiseCharmToggle(false);
                            //AbilitiesToggle(false);
                            //VisitLockToggle(false);
                            //ExtraChecksToggle(false);
                            AntiFormToggle(false);

                            //world settings
                            SoraHeartToggle(true);
                            DrivesToggle(false);
                            SimulatedToggle(false);
                            TwilightTownToggle(false);
                            HollowBastionToggle(false);
                            BeastCastleToggle(false);
                            OlympusToggle(false);
                            AgrabahToggle(false);
                            LandofDragonsToggle(false);
                            DisneyCastleToggle(false);
                            PrideLandsToggle(false);
                            PortRoyalToggle(false);
                            HalloweenTownToggle(false);
                            SpaceParanoidsToggle(false);
                            TWTNWToggle(false);
                            HundredAcreWoodToggle(false);
                            AtlanticaToggle(false);
                            PuzzleToggle(false);
                            SynthToggle(false);

                            //progression hints GoA Current Hint Count
                            data.WorldsData["GoA"].value.Visibility = Visibility.Hidden;

                            //settings visuals
                            SettingRow.Height = new GridLength(0.5, GridUnitType.Star);
                            Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Absent_Split.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
                            Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
                            Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
                            Double SpacerValue = 10;
                            #endregion

                            //to be safe about this i guess
                            //bool abilitiesOn = true;
                            bool puzzleOn = false;
                            bool synthOn = false;

                            //load settings from hints
                            foreach (string setting in settings)
                            {
                                Console.WriteLine("setting found = " + setting);

                                switch (setting)
                                {
                                    //items
                                    case "PromiseCharm":
                                        PromiseCharmToggle(true);
                                        break;
                                    //case "Level1Mode":
                                    //    abilitiesOn = false;
                                    //    break;
                                    case "visit_locking":
                                        VisitLockToggle(true);
                                        break;
                                    //case "extra_ics":
                                    //    ExtraChecksToggle(true);
                                    //    break;
                                    case "Anti-Form":
                                        AntiFormToggle(true);
                                        break;
                                    //worlds
                                    case "Level":
                                        SoraHeartToggle(false);
                                        SoraLevel01Toggle(true);
                                        //AbilitiesToggle(true);
                                        Setting_Level_01.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "ExcludeFrom50":
                                        SoraLevel50Toggle(true);
                                        //AbilitiesToggle(true);
                                        Setting_Level_50.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        data.HintRevealOrder.Add("SorasHeart");
                                        break;
                                    case "ExcludeFrom99":
                                        SoraLevel99Toggle(true);
                                        //AbilitiesToggle(true);
                                        Setting_Level_99.Width = new GridLength(1.5, GridUnitType.Star);
                                        SpacerValue--;
                                        data.HintRevealOrder.Add("SorasHeart");
                                        break;
                                    case "Simulated Twilight Town":
                                        SimulatedToggle(true);
                                        data.enabledWorlds.Add("STT");
                                        data.HintRevealOrder.Add("SimulatedTwilightTown");
                                        break;
                                    case "Hundred Acre Wood":
                                        HundredAcreWoodToggle(true);
                                        data.enabledWorlds.Add("HundredAcreWood");
                                        data.HintRevealOrder.Add("HundredAcreWood");
                                        break;
                                    case "Atlantica":
                                        AtlanticaToggle(true);
                                        data.enabledWorlds.Add("Atlantica");
                                        data.HintRevealOrder.Add("Atlantica");
                                        break;
                                    case "Puzzle":
                                        PuzzleToggle(true);
                                        puzzleOn = true;
                                        data.puzzlesOn = true;
                                        break;
                                    case "Synthesis":
                                        SynthToggle(true);
                                        synthOn = true;
                                        data.synthOn = true;
                                        break;
                                    case "Form Levels":
                                        DrivesToggle(true);
                                        data.HintRevealOrder.Add("DriveForms");
                                        break;
                                    case "Land of Dragons":
                                        LandofDragonsToggle(true);
                                        data.enabledWorlds.Add("LoD");
                                        data.HintRevealOrder.Add("LandofDragons");
                                        break;
                                    case "Beast's Castle":
                                        BeastCastleToggle(true);
                                        data.enabledWorlds.Add("BC");
                                        data.HintRevealOrder.Add("BeastsCastle");
                                        break;
                                    case "Hollow Bastion":
                                        HollowBastionToggle(true);
                                        data.enabledWorlds.Add("HB");
                                        data.HintRevealOrder.Add("HollowBastion");
                                        break;
                                    case "Twilight Town":
                                        TwilightTownToggle(true);
                                        data.enabledWorlds.Add("TT");
                                        data.HintRevealOrder.Add("TwilightTown");
                                        break;
                                    case "The World That Never Was":
                                        TWTNWToggle(true);
                                        data.enabledWorlds.Add("TWTNW");
                                        data.HintRevealOrder.Add("TWTNW");
                                        break;
                                    case "Space Paranoids":
                                        SpaceParanoidsToggle(true);
                                        data.enabledWorlds.Add("SP");
                                        data.HintRevealOrder.Add("SpaceParanoids");
                                        break;
                                    case "Port Royal":
                                        PortRoyalToggle(true);
                                        data.enabledWorlds.Add("PR");
                                        data.HintRevealOrder.Add("PortRoyal");
                                        break;
                                    case "Olympus Coliseum":
                                        OlympusToggle(true);
                                        data.enabledWorlds.Add("OC");
                                        data.HintRevealOrder.Add("OlympusColiseum");
                                        break;
                                    case "Agrabah":
                                        AgrabahToggle(true);
                                        data.enabledWorlds.Add("AG");
                                        data.HintRevealOrder.Add("Agrabah");
                                        break;
                                    case "Halloween Town":
                                        HalloweenTownToggle(true);
                                        data.enabledWorlds.Add("HT");
                                        data.HintRevealOrder.Add("HalloweenTown");
                                        break;
                                    case "Pride Lands":
                                        PrideLandsToggle(true);
                                        data.enabledWorlds.Add("PL");
                                        data.HintRevealOrder.Add("PrideLands");
                                        break;
                                    case "Disney Castle / Timeless River":
                                        DisneyCastleToggle(true);
                                        data.enabledWorlds.Add("DC");
                                        data.HintRevealOrder.Add("DisneyCastle");
                                        break;
                                    //settings
                                    case "better_stt":
                                        Setting_BetterSTT.Width = new GridLength(1.1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Cavern of Remembrance":
                                        Setting_Cavern.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Data Split":
                                        Setting_Absent_Split.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        data.dataSplit = true;
                                        break;
                                    case "Absent Silhouettes":
                                        if (!data.dataSplit) //only use if we didn't already set the data split version
                                        {
                                            Setting_Absent.Width = new GridLength(1, GridUnitType.Star);
                                            SpacerValue--;
                                        }
                                        break;
                                    case "Sephiroth":
                                        Setting_Sephiroth.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Lingering Will (Terra)":
                                        Setting_Terra.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Data Organization XIII":
                                        Setting_Datas.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Transport to Remembrance":
                                        Setting_Transport.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Olympus Cups":
                                        Setting_Cups.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "Hades Paradox Cup":
                                        Setting_HadesCup.Width = new GridLength(1, GridUnitType.Star);
                                        SpacerValue--;
                                        break;
                                    case "ScoreMode":
                                        data.ScoreMode = true;
                                        break;
                                    //progression hints
                                    case "ProgressionHints":
                                        data.UsingProgressionHints = true;
                                        data.WorldsData["GoA"].value.Visibility = Visibility.Visible;
                                        data.WorldsData["GoA"].value.Text = "0";
                                        //Console.WriteLine("ENABLING PROGRESSION HINTS");
                                        break;
                                }
                            }

                            //if (abilitiesOn == false)
                            //    AbilitiesToggle(false);

                            //prevent creations hinting twice for progression
                            if ((puzzleOn || hintObject["hintsType"].ToString() == "Path") && !data.HintRevealOrder.Contains("PuzzSynth"))
                            {
                                data.HintRevealOrder.Add("PuzzSynth");
                            }

                            Setting_Spacer.Width = new GridLength(SpacerValue, GridUnitType.Star);
                            SettingsText.Text = "Settings:";

                        }

                        if (hintObject.ContainsKey("ProgressionType"))
                        {
                            data.progressionType = hintObject["ProgressionType"].ToString();
                        }

                        if (hintObject.ContainsKey("ProgressionSettings"))
                        {
                            var progressionSettings = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(hintObject["ProgressionSettings"].ToString());

                            if (data.progressionType != "Bosses")
                                data.progressionType = "Reports";

                            foreach (var setting in progressionSettings)
                            {
                                //Console.WriteLine("progression setting found = " + setting.Key);

                                switch (setting.Key)
                                {
                                    case "HintCosts":
                                        data.HintCosts.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HintCosts.Add(cost);
                                        data.HintCosts.Add(data.HintCosts[data.HintCosts.Count - 1] + 1); //duplicates the last cost for logic reasons
                                        break;
                                    case "SimulatedTwilightTown":
                                        data.STT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.STT_ProgressionValues.Add(cost);
                                        break;
                                    case "TwilightTown":
                                        data.TT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.TT_ProgressionValues.Add(cost);
                                        break;
                                    case "HollowBastion":
                                        data.HB_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HB_ProgressionValues.Add(cost);
                                        break;
                                    case "CavernofRemembrance":
                                        data.CoR_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.CoR_ProgressionValues.Add(cost);
                                        break;
                                    case "LandofDragons":
                                        data.LoD_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.LoD_ProgressionValues.Add(cost);
                                        break;
                                    case "BeastsCastle":
                                        data.BC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.BC_ProgressionValues.Add(cost);
                                        break;
                                    case "OlympusColiseum":
                                        data.OC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.OC_ProgressionValues.Add(cost);
                                        break;
                                    case "DisneyCastle":
                                        data.DC_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.DC_ProgressionValues.Add(cost);
                                        break;
                                    case "Agrabah":
                                        data.AG_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.AG_ProgressionValues.Add(cost);
                                        break;
                                    case "PortRoyal":
                                        data.PR_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.PR_ProgressionValues.Add(cost);
                                        break;
                                    case "HalloweenTown":
                                        data.HT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HT_ProgressionValues.Add(cost);
                                        break;
                                    case "PrideLands":
                                        data.PL_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.PL_ProgressionValues.Add(cost);
                                        break;
                                    case "HundredAcreWood":
                                        data.HAW_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.HAW_ProgressionValues.Add(cost);
                                        break;
                                    case "SpaceParanoids":
                                        data.SP_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.SP_ProgressionValues.Add(cost);
                                        break;
                                    case "TWTNW":
                                        data.TWTNW_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.TWTNW_ProgressionValues.Add(cost);
                                        break;
                                    case "Atlantica":
                                        data.AT_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.AT_ProgressionValues.Add(cost);
                                        break;
                                    case "ReportBonus":
                                        data.ReportBonus = setting.Value[0];
                                        break;
                                    case "WorldCompleteBonus":
                                        data.WorldCompleteBonus = setting.Value[0];
                                        break;
                                    case "Levels":
                                        data.Levels_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.Levels_ProgressionValues.Add(cost);
                                        break;
                                    case "Drives":
                                        data.Drives_ProgressionValues.Clear();
                                        foreach (int cost in setting.Value)
                                            data.Drives_ProgressionValues.Add(cost);
                                        break;
                                    case "FinalXemnasReveal":
                                        data.revealFinalXemnas = setting.Value[0] == 0 ? false : true;
                                        break;
                                }
                            }
                            //data.NumOfHints = data.HintCosts.Count;
                            //set text correctly
                            ProgressionCollectedValue.Visibility = Visibility.Visible;
                            ProgressionCollectedBar.Visibility = Visibility.Visible;
                            ProgressionCollectedValue.Text = "0";
                            ProgressionTotalValue.Text = data.HintCosts[0].ToString();
                        }

                        switch (hintObject["hintsType"].ToString())
                        {
                            case "Shananas":
                                {
                                    SetMode(Mode.OpenKHShanHints);
                                    ShanHints(hintObject);
                                }
                                break;
                            case "JSmartee":
                                {
                                    SetMode(Mode.OpenKHJsmarteeHints);
                                    JsmarteeHints(hintObject);
                                }
                                break;
                            case "Points":
                                {
                                    SetMode(Mode.PointsHints);
                                    PointsHints(hintObject);
                                }
                                break;
                            case "Path":
                                {
                                    SetMode(Mode.PathHints);
                                    PathHints(hintObject);
                                }
                                break;
                            case "Spoiler":
                                {
                                    SetMode(Mode.SpoilerHints);
                                    SpoilerHints(hintObject);
                                }
                                break;
                            default:
                                break;
                        }

                        reader.Close();
                    }
                }

                archive.Dispose();

                data.seedLoaded = true;
            }

            if (data.wasTracking)
            {
                InitTracker();
            }
        }

        //hint helpers

        private void SetMode(Mode mode)
        {
            if (mode == Mode.ShanHints || mode == Mode.OpenKHShanHints)
            {
                ModeDisplay.Header = "Shan Hints";
                data.mode = mode;
                //ReportsToggle(false);
            }
            else if (mode == Mode.JsmarteeHints || mode == Mode.OpenKHJsmarteeHints)
            {
                ModeDisplay.Header = "Jsmartee Hints";
                data.mode = mode;
                //ReportsToggle(true);
            }
            else if (mode == Mode.PointsHints)
            {
                ModeDisplay.Header = "Points Hints";
                data.mode = mode;
                //ReportsToggle(true);
                //high score mode should not be on if points hints is on
                if (data.ScoreMode == true)
                    data.ScoreMode = false;

                UpdatePointScore(0);
                ShowCheckCountToggle(null, null);
            }
            else if (mode == Mode.PathHints)
            {
                ModeDisplay.Header = "Path Hints";
                data.mode = mode;
                //ReportsToggle(true);
            }
            else if (mode == Mode.SpoilerHints)
            {
                ModeDisplay.Header = "Spoiler Hints";
                data.mode = mode;
            }

            if (data.ScoreMode && mode != Mode.PointsHints)
            {
                UpdatePointScore(0);
                ShowCheckCountToggle(null, null);

                ModeDisplay.Header += " | HSM";
            }

            if (data.UsingProgressionHints)
            {
                if (data.progressionType == "Reports")
                {
                    CollectionGrid.Visibility = Visibility.Collapsed;
                    ScoreGrid.Visibility = Visibility.Collapsed;
                    ProgressionCollectionGrid.Visibility = Visibility.Visible;
                    ChestIcon.SetResourceReference(ContentProperty, "ProgPoints");
                    ModeDisplay.Header += " | Progression";
                }
                else if (data.progressionType == "Bosses")
                    ModeDisplay.Header += " | Prog. Bosses";
            }
        }

        private void LoadSettings(string settings)
        {
            //item settings
            ReportsToggle(false);
            PromiseCharmToggle(false);
            AbilitiesToggle(false);
            VisitLockToggle(false);
            ExtraChecksToggle(false);
            AntiFormToggle(false);
            TornPagesToggle(false);

            //world settings
            SoraHeartToggle(true);
            DrivesToggle(false);
            SimulatedToggle(false);
            TwilightTownToggle(false);
            HollowBastionToggle(false);
            BeastCastleToggle(false);
            OlympusToggle(false);
            AgrabahToggle(false);
            LandofDragonsToggle(false);
            DisneyCastleToggle(false);
            PrideLandsToggle(false);
            PortRoyalToggle(false);
            HalloweenTownToggle(false);
            SpaceParanoidsToggle(false);
            TWTNWToggle(false);
            HundredAcreWoodToggle(false);
            AtlanticaToggle(false);
            PuzzleToggle(false);
            SynthToggle(false);

            string[] settinglist = settings.Split('-');

            foreach (string setting in settinglist)
            {
                string trimmed = setting.Trim();
                switch (trimmed)
                {
                    case "Secret Ansem Reports":
                        ReportsToggle(true);
                        break;
                    case "Promise Charm":
                        PromiseCharmToggle(true);
                        break;
                    case "Second Chance & Once More":
                        AbilitiesToggle(true);
                        break;
                    case "AntiForm":
                        AntiFormToggle(true);
                        break;
                    case "Visit Locks":
                        VisitLockToggle(true);
                        break;
                    case "Extra Checks":
                        ExtraChecksToggle(true);
                        break;
                    case "Level01":
                        SoraLevel01Toggle(true);
                        break;
                    case "Level50":
                        SoraLevel50Toggle(true);
                        break;
                    case "Level99":
                        SoraLevel99Toggle(true);
                        break;
                    case "Sora's Heart":
                        SoraHeartToggle(true);
                        break;
                    case "Drive Forms":
                        DrivesToggle(true);
                        break;
                    case "Simulated Twilight Town":
                        SimulatedToggle(true);
                        data.enabledWorlds.Add("STT");
                        break;
                    case "Twilight Town":
                        TwilightTownToggle(true);
                        data.enabledWorlds.Add("TT");
                        break;
                    case "Hollow Bastion":
                        HollowBastionToggle(true);
                        data.enabledWorlds.Add("HB");
                        break;
                    case "Beast Castle":
                        BeastCastleToggle(true);
                        data.enabledWorlds.Add("BC");
                        break;
                    case "Olympus":
                        OlympusToggle(true);
                        data.enabledWorlds.Add("OC");
                        break;
                    case "Agrabah":
                        AgrabahToggle(true);
                        data.enabledWorlds.Add("AG");
                        break;
                    case "Land of Dragons":
                        LandofDragonsToggle(true);
                        data.enabledWorlds.Add("LoD");
                        break;
                    case "Disney Castle":
                        DisneyCastleToggle(true);
                        data.enabledWorlds.Add("DC");
                        break;
                    case "Pride Lands":
                        PrideLandsToggle(true);
                        data.enabledWorlds.Add("PL");
                        break;
                    case "Port Royal":
                        PortRoyalToggle(true);
                        data.enabledWorlds.Add("PR");
                        break;
                    case "Halloween Town":
                        HalloweenTownToggle(true);
                        data.enabledWorlds.Add("HT");
                        break;
                    case "Space Paranoids":
                        SpaceParanoidsToggle(true);
                        data.enabledWorlds.Add("SP");
                        break;
                    case "TWTNW":
                        TWTNWToggle(true);
                        data.enabledWorlds.Add("TWTNW");
                        break;
                    case "100 Acre Wood":
                        HundredAcreWoodToggle(true);
                        break;
                    case "Atlantica":
                        AtlanticaToggle(true);
                        break;
                    case "Puzzles":
                        PuzzleToggle(true);
                        break;
                    case "Synthesis":
                        SynthToggle(true);
                        break;
                    case "Score Mode":
                        data.ScoreMode = true;
                        break;
                    case "Boss Rando":
                        data.BossRandoFound = true;
                        break;
                    case "Torn Pages":
                        TornPagesToggle(true);
                        break;
                }
            }
        }

        //Turns the zip seed icon hash to a numerical based seed
        private void HashToSeed(string[] hash)
        {
            int icon1 = Codes.HashInt[hash[0]];
            int icon2 = Codes.HashInt[hash[1]];
            int icon3 = Codes.HashInt[hash[2]];
            int icon4 = Codes.HashInt[hash[3]];
            int icon5 = Codes.HashInt[hash[4]];
            int icon6 = Codes.HashInt[hash[5]];
            int icon7 = Codes.HashInt[hash[6]];

            int final = (icon1 + icon2) * (icon3 + icon4) * (icon5 + icon6) - icon7;
            data.convertedSeedHash = final;
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            if (aTimer != null)
            {
                aTimer.Stop();
                aTimer = null;
                pcFilesLoaded = false;
            }

            if (sender != null && !AutoConnectOption.IsChecked)
                data.wasTracking = false;

            //chnage visuals based on if autotracking was done before
            if (data.wasTracking) 
            {
                //connection trying visual
                Connect.Visibility = Visibility.Visible;
                Connect2.Visibility = Visibility.Collapsed;
            }
            else
            {
                Connect.Visibility = Visibility.Collapsed;
                Connect2.Visibility = Visibility.Collapsed;

                SettingRow.Height = new GridLength(0, GridUnitType.Star);
                FormRow.Height = new GridLength(0, GridUnitType.Star);
                Level.Visibility = Visibility.Collapsed;
                Strength.Visibility = Visibility.Collapsed;
                Magic.Visibility = Visibility.Collapsed;
                Defense.Visibility = Visibility.Collapsed;
            }

            collectedChecks.Clear();
            newChecks.Clear();
            ModeDisplay.Header = "";
            HintTextMiddle.Text = "";
            HintTextBegin.Text = "";
            HintTextEnd.Text = "";
            data.mode = Mode.None;
            collected = 0;
            PointTotal = 0;
            data.SpoilerRevealTypes.Clear();
            data.SpoilerReportMode = false;
            data.SpoilerWorldCompletion = false;
            data.usedPages = 0;
            CollectedValue.Text = "0";
            data.ScoreMode = false;
            data.forcedFinal = false;
            data.BossRandoFound = false;
            data.dataSplit = false;
            data.BossList.Clear();
            data.bossEventLog.Clear();
            data.convertedSeedHash = 0;
            data.enabledWorlds.Clear();
            data.seedgenVersion = "";
            data.altFinalTracking = false;
            data.eventLog.Clear();
            data.openKHHintText = "None";
            data.openKHBossText = "None";
            data.legacyJsmartee = false;
            data.hintFileText = null;
            data.legacyShan = false;
            data.shanHintFileText = null;
            data.hintsLoaded = false;
            data.seedLoaded = false;
            data.saveFileLoaded = false;

            //prog boss hint stuff
            BossHintTextMiddle.Text = "";
            BossHintTextBegin.Text = "";
            BossHintTextEnd.Text = "";
            data.progBossInformation.Clear();
            data.progressionType = "DummyText";
            InfoRow.Height = new GridLength(0.8, GridUnitType.Star);
            InfoTextRow.Height = new GridLength(1, GridUnitType.Star);
            BossTextRow.Height = new GridLength(0, GridUnitType.Star);
            MainTextRow.Height = new GridLength(1, GridUnitType.Star);
            HashBossSpacer.Height = new GridLength(0, GridUnitType.Star);
            DC_Row1.Height = new GridLength(0, GridUnitType.Star);
            TextRowSpacer.Height = new GridLength(0, GridUnitType.Star);
            Grid.SetColumnSpan(MainTextVB, 1);

            //clear progression hints stuff
            data.reportLocationsUsed = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false };
            data.UsingProgressionHints = false;
            data.ProgressionPoints = 0;
            data.TotalProgressionPoints = 0;
            data.ReportBonus = 1;
            data.WorldCompleteBonus = 0;
            data.ProgressionCurrentHint = 0;
            data.WorldsEnabled = 0;
            data.HintRevealOrder.Clear();
            data.LevelsPreviousIndex = 0;
            data.NextLevelMilestone = 9;
            data.Levels_ProgressionValues = new List<int>() { 1, 1, 1, 2, 4 };
            data.Drives_ProgressionValues = new List<int>() { 0, 0, 0, 1, 0, 2 };
            data.DriveLevels = new List<int>() { 1, 1, 1, 1, 1 };
            data.HintRevealsStored.Clear();
            data.WorldsData["GoA"].value.Visibility = Visibility.Hidden;
            //clear last hinted green world
            if (data.previousWorldsHinted.Count >= 0)
            {
                foreach (var world in data.previousWorldsHinted)
                {
                    if (world == null || world == "")
                        continue;

                    foreach (var Box in data.WorldsData[world].top.Children.OfType<Rectangle>())
                    {
                        if (Box.Opacity != 0.9 && !Box.Name.EndsWith("SelWG"))
                            Box.Fill = (SolidColorBrush)FindResource("DefaultRec");

                        if (Box.Name.EndsWith("SelWG") && !WorldHighlightOption.IsChecked)
                            Box.Visibility = Visibility.Collapsed;
                    }
                }
            }
            data.previousWorldsHinted.Clear();
            data.StoredWorldCompleteBonus = new Dictionary<string, int>()
            {
                { "SorasHeart", 0 },
                { "DriveForms", 0 },
                { "SimulatedTwilightTown", 0 },
                { "TwilightTown", 0 },
                { "HollowBastion", 0 },
                { "BeastsCastle", 0 },
                { "OlympusColiseum", 0 },
                { "Agrabah", 0 },
                { "LandofDragons", 0 },
                { "HundredAcreWood", 0 },
                { "PrideLands", 0 },
                { "DisneyCastle", 0 },
                { "HalloweenTown", 0 },
                { "PortRoyal", 0 },
                { "SpaceParanoids", 0 },
                { "TWTNW", 0 },
                { "GoA", 0 },
                { "Atlantica", 0 },
                { "PuzzSynth", 0 }
            };
            data.STT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            data.TT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            data.HB_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            data.CoR_ProgressionValues = new List<int>() { 0, 0, 0, 0, 0 };
            data.BC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            data.OC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            data.AG_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            data.LoD_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            data.HAW_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6 };
            data.PL_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            data.AT_ProgressionValues = new List<int>() { 1, 2, 3 };
            data.DC_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            data.HT_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            data.PR_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            data.SP_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6 };
            data.TWTNW_ProgressionValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            data.HintCosts = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 };

            //hotkey stuff
            data.usedHotkey = false;

            //unselect any currently selected world grid
            if (data.selected != null)
            {
                foreach (var Box in data.WorldsData[data.selected.Name].top.Children.OfType<Rectangle>())
                {
                    if (Box.Opacity != 0.9 && !Box.Name.EndsWith("SelWG"))
                        Box.Fill = (SolidColorBrush)FindResource("DefaultRec");

                    if (Box.Name.EndsWith("SelWG"))
                        Box.Visibility = Visibility.Collapsed;
                }
            }
            data.selected = null;

            //return items to itempool
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                for (int j = worldData.worldGrid.Children.Count - 1; j >= 0; --j)
                {
                    Item item = worldData.worldGrid.Children[j] as Item;
                    Grid pool;

                    if(item.Name.StartsWith("Ghost_"))
                        pool = VisualTreeHelper.GetChild(ItemPool, 4) as Grid;
                    else
                        pool = data.Items[item.Name].Item2;
            
                    worldData.worldGrid.Children.Remove(worldData.worldGrid.Children[j]);
                    pool.Children.Add(item);
            
                    item.MouseDown -= item.Item_Return;
                    item.MouseEnter -= item.Report_Hover;
                    if (data.dragDrop)
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;
                        item.MouseMove += item.Item_MouseMove;
                    }
                    else
                    {
                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                        item.MouseUp += item.Item_MouseUp;
                    }
                }
            }

            // Reset 1st column row heights
            RowDefinitionCollection rows1 = ((data.WorldsData["SorasHeart"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows1)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }
            
            // Reset 2nd column row heights
            RowDefinitionCollection rows2 = ((data.WorldsData["DriveForms"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows2)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            //fix puzzsynth value if it was hidden (progression hints)
            if (data.WorldsData["PuzzSynth"].value.Visibility == Visibility.Hidden)
            {
                data.WorldsData["PuzzSynth"].value.Visibility = Visibility.Visible;
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].complete = false;
                data.WorldsData[key].checkCount.Clear();
                data.WorldsData[key].progress = 0;

                //world cross reset
                string crossname = key + "Cross";

                if (data.WorldsData[key].top.FindName(crossname) is Image Cross)
                {
                    Cross.Visibility = Visibility.Collapsed;
                }

                //reset highlighted world
                foreach (Rectangle Box in data.WorldsData[key].top.Children.OfType<Rectangle>().Where(Box => Box.Name.EndsWith("SelWG")))
                {
                    Box.Visibility = Visibility.Collapsed;
                }
            }

            TwilightTownProgression.SetResourceReference(ContentProperty, "");
            HollowBastionProgression.SetResourceReference(ContentProperty, "");
            LandofDragonsProgression.SetResourceReference(ContentProperty, "");
            BeastsCastleProgression.SetResourceReference(ContentProperty, "");
            OlympusColiseumProgression.SetResourceReference(ContentProperty, "");
            SpaceParanoidsProgression.SetResourceReference(ContentProperty, "");
            HalloweenTownProgression.SetResourceReference(ContentProperty, "");
            PortRoyalProgression.SetResourceReference(ContentProperty, "");
            AgrabahProgression.SetResourceReference(ContentProperty, "");
            PrideLandsProgression.SetResourceReference(ContentProperty, "");
            DisneyCastleProgression.SetResourceReference(ContentProperty, "");
            HundredAcreWoodProgression.SetResourceReference(ContentProperty, "");
            SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "");
            TWTNWProgression.SetResourceReference(ContentProperty, "");
            AtlanticaProgression.SetResourceReference(ContentProperty, "");
            GoAProgression.SetResourceReference(ContentProperty, "");
            DriveFormsCap.SetResourceReference(ContentProperty, "");
            ChestIcon.SetResourceReference(ContentProperty, "Chest");

            SorasHeartWeapon.SetResourceReference(ContentProperty, "");

            NextLevelCol.Width = new GridLength(0, GridUnitType.Star);

            ValorM.Opacity = .45;
            WisdomM.Opacity = .45;
            LimitM.Opacity = .45;
            MasterM.Opacity = .45;
            FinalM.Opacity = .45;
            HighJump.Opacity = .45;
            QuickRun.Opacity = .45;
            DodgeRoll.Opacity = .45;
            AerialDodge.Opacity = .45;
            Glide.Opacity = .45;

            ValorLevel.Text = "1";
            WisdomLevel.Text = "1";
            LimitLevel.Text = "1";
            MasterLevel.Text = "1";
            FinalLevel.Text = "1";
            HighJumpLevel.Text = "";
            QuickRunLevel.Text = "";
            DodgeRollLevel.Text = "";
            AerialDodgeLevel.Text = "";
            GlideLevel.Text = "";

            fireLevel = 0;
            blizzardLevel = 0;
            thunderLevel = 0;
            cureLevel = 0;
            reflectLevel = 0;
            magnetLevel = 0;
            tornPageCount = 0;

            if (fire != null)
                fire.Level = 0;
            if (blizzard != null)
                blizzard.Level = 0;
            if (thunder != null)
                thunder.Level = 0;
            if (cure != null)
                cure.Level = 0;
            if (reflect != null)
                reflect.Level = 0;
            if (magnet != null)
                magnet.Level = 0;
            if (pages != null)
                pages.Quantity = 0;

            if (highJump != null)
                highJump.Level = 0;
            if (quickRun != null)
                quickRun.Level = 0;
            if (dodgeRoll != null)
                dodgeRoll.Level = 0;
            if (aerialDodge != null)
                aerialDodge.Level = 0;
            if (glide != null)
                glide.Level = 0;

            //hide & reset seed hash
            if (data.ShouldResetHash)
            {
                HashGrid.Visibility = Visibility.Collapsed;
                data.SeedHashLoaded = false;
            }

            foreach (string value in data.PointsDatanew.Keys.ToList())
            {
                data.PointsDatanew[value] = 0;
            }

            foreach (string world in WorldPoints.Keys.ToList())
            {
                WorldPoints[world] = 0;
                WorldPoints_c[world] = 0;
            }

            WorldGrid.Real_Fire = 0;
            WorldGrid.Real_Blizzard = 0;
            WorldGrid.Real_Thunder = 0;
            WorldGrid.Real_Cure = 0;
            WorldGrid.Real_Reflect = 0;
            WorldGrid.Real_Magnet = 0;
            WorldGrid.Real_Pages = 0;
            WorldGrid.Real_Pouches = 0;
            WorldGrid.Proof_Count = 0;
            WorldGrid.Form_Count = 0;
            WorldGrid.Summon_Count = 0;
            WorldGrid.Ability_Count = 0;
            WorldGrid.Report_Count = 0;
            WorldGrid.Visit_Count = 0;

            FireCount.Text = "3";
            BlizzardCount.Text = "3";
            ThunderCount.Text = "3";
            CureCount.Text = "3";
            ReflectCount.Text = "3";
            MagnetCount.Text = "3";
            PageCount.Text = "5";
            MunnyCount.Text = "2";

            WorldGrid.Ghost_Fire = 0;
            WorldGrid.Ghost_Blizzard = 0;
            WorldGrid.Ghost_Thunder = 0;
            WorldGrid.Ghost_Cure = 0;
            WorldGrid.Ghost_Reflect = 0;
            WorldGrid.Ghost_Magnet = 0;
            WorldGrid.Ghost_Pages = 0;
            WorldGrid.Ghost_Pouches = 0;
            WorldGrid.Ghost_Fire_obtained = 0;
            WorldGrid.Ghost_Blizzard_obtained = 0;
            WorldGrid.Ghost_Thunder_obtained = 0;
            WorldGrid.Ghost_Cure_obtained = 0;
            WorldGrid.Ghost_Reflect_obtained = 0;
            WorldGrid.Ghost_Magnet_obtained = 0;
            WorldGrid.Ghost_Pages_obtained = 0;
            WorldGrid.Ghost_Pouches_obtained = 0;

            Ghost_FireCount.Visibility = Visibility.Hidden;
            Ghost_BlizzardCount.Visibility = Visibility.Hidden;
            Ghost_ThunderCount.Visibility = Visibility.Hidden;
            Ghost_CureCount.Visibility = Visibility.Hidden;
            Ghost_ReflectCount.Visibility = Visibility.Hidden;
            Ghost_MagnetCount.Visibility = Visibility.Hidden;
            Ghost_PageCount.Visibility = Visibility.Hidden;
            Ghost_MunnyCount.Visibility = Visibility.Hidden;

            FireCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            FireCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            FireCount.Fill = (LinearGradientBrush)FindResource("Color_Fire");
            FireCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            BlizzardCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            BlizzardCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            BlizzardCount.Fill = (LinearGradientBrush)FindResource("Color_Blizzard");
            BlizzardCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            ThunderCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            ThunderCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            ThunderCount.Fill = (LinearGradientBrush)FindResource("Color_Thunder");
            ThunderCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            CureCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            CureCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            CureCount.Fill = (LinearGradientBrush)FindResource("Color_Cure");
            CureCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            MagnetCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            MagnetCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            MagnetCount.Fill = (LinearGradientBrush)FindResource("Color_Magnet");
            MagnetCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            ReflectCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            ReflectCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            ReflectCount.Fill = (LinearGradientBrush)FindResource("Color_Reflect");
            ReflectCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            PageCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            PageCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            PageCount.Fill = (LinearGradientBrush)FindResource("Color_Page");
            PageCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
            MunnyCount.Fill = (SolidColorBrush)FindResource("Color_Black");
            MunnyCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
            MunnyCount.Fill = (LinearGradientBrush)FindResource("Color_Pouch");
            MunnyCount.Stroke = (SolidColorBrush)FindResource("Color_Black");

            Data.WorldItems.Clear();
            data.TrackedReports.Clear();

            CollectionGrid.Visibility = Visibility.Visible;
            ScoreGrid.Visibility = Visibility.Hidden;
            ProgressionCollectionGrid.Visibility = Visibility.Hidden;

            //reset settings row
            SettingsText.Text = "";
            Setting_BetterSTT.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_01.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_50.Width = new GridLength(0, GridUnitType.Star);
            Setting_Level_99.Width = new GridLength(0, GridUnitType.Star);
            Setting_Absent.Width = new GridLength(0, GridUnitType.Star);
            Setting_Absent_Split.Width = new GridLength(0, GridUnitType.Star);
            Setting_Datas.Width = new GridLength(0, GridUnitType.Star);
            Setting_Sephiroth.Width = new GridLength(0, GridUnitType.Star);
            Setting_Terra.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cups.Width = new GridLength(0, GridUnitType.Star);
            Setting_HadesCup.Width = new GridLength(0, GridUnitType.Star);
            Setting_Cavern.Width = new GridLength(0, GridUnitType.Star);
            Setting_Transport.Width = new GridLength(0, GridUnitType.Star);
            Setting_Spacer.Width = new GridLength(10, GridUnitType.Star);
            
            //reset pathhints edits
            foreach (string key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
            
                Grid pathgrid = data.WorldsData[key].top.FindName(key + "Path") as Grid;
                pathgrid.Visibility = Visibility.Hidden;
                foreach (Image child in pathgrid.Children)
                {
                    if (child.Name.Contains(key + "Path_Non") && child.Source.ToString().Contains("cross.png")) //reset non icon to default image
                        child.Source = new BitmapImage(new Uri("Images/Checks/Simple/proof_of_nonexistence.png", UriKind.Relative));
                    child.Visibility = Visibility.Hidden;
                }
            }

            UpdatePointScore(0);
            ReportsToggle(true);
            TornPagesToggle(true);
            ResetHints();
            VisitLockToggle(VisitLockOption.IsChecked);

            DeathCounter = 0;
            DeathValue.Text = "0";
            DeathCol.Width = new GridLength(0, GridUnitType.Star);
            DeathCounterGrid.Visibility = Visibility.Collapsed;

            foreach (Grid itempool in ItemPool.Children)
            {
                foreach (var item in itempool.Children)
                {
                    ContentControl check = item as ContentControl;

                    if (check != null && !check.Name.Contains("Ghost"))
                        check.Opacity = 1.0;
                }
            }

            NextLevelDisplay();

            //reset progression visuals
            PPCount.Width = new GridLength(1.15, GridUnitType.Star);
            PPSep.Width = new GridLength(0.3, GridUnitType.Star);

            if (data.wasTracking && sender != null)
                InitTracker();
        }

        private bool InProgressCheck(string type)
        {
            string message = "";
            string caption = "";

            if (data.seedLoaded | data.saveFileLoaded)
            {
                if(type == "tsv")
                {
                    message = "Hints were already loaded into the tracker!" +
                        "\n Any progress made so far would be lost if you continue." +
                        "\n Proceed anyway?";
                    caption = "Progress Load Confirmation";
                }
                if (type == "seed")
                {
                    message = "A Randomizer Seed was already loaded into the tracker!" +
                        "\n Any progress made so far would be lost if you continue." +
                        "\n Proceed anyway?";
                    caption = "Seed Load Confirmation";
                }
                if (type == "hints")
                {
                    message = "Hints were already loaded into the tracker!" +
                        "\n Any progress made so far would be lost if you continue." +
                        "\n Proceed anyway?";
                    caption = "Hints Load Confirmation";

                }

                MessageForm.MessageBoxButtons buttons = MessageForm.MessageBoxButtons.OKCancel;
                MessageForm.DialogResult result;

                result = MessageForm.MessageBox.Show(message, caption, buttons);
                if (result == MessageForm.DialogResult.Cancel)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }

        private void ResetHints()
        {
            data.hintsLoaded = false;
            data.reportLocations.Clear();
            data.reportInformation.Clear();
            data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
                data.WorldsData[key].hintedHint = false;
                data.WorldsData[key].containsGhost = false;
                //progression hints per world
                data.WorldsData[key].hintedProgression = false;
            }
            data.WorldsData["GoA"].hinted = true;

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }

            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                if (worldData.value != null)
                    SetWorldValue(worldData.value, -999999);
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }
        }

        /// 
        /// Hotkey logic
        ///

        private void LoadHotkeyBind()
        {
            if (!Directory.Exists("./KhTrackerSettings"))
            {
                Directory.CreateDirectory("./KhTrackerSettings");
            }

            if (!File.Exists("./KhTrackerSettings/AutoTrackerKeybinds.txt"))
            {
                //Console.WriteLine("File not found, making");
                using (FileStream fs = File.Create("./KhTrackerSettings/AutoTrackerKeybinds.txt"))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Control\n");
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("F12");
                    fs.Write(author, 0, author.Length);
                }
            }
            string[] lines = System.IO.File.ReadAllLines("./KhTrackerSettings/AutoTrackerKeybinds.txt");
            string mod1 = "";
            ModifierKeys _mod1 = ModifierKeys.None;
            string mod2 = "";
            ModifierKeys _mod2 = ModifierKeys.None;
            string mod3 = "";
            ModifierKeys _mod3 = ModifierKeys.None;
            string key = "";
            int modsUsed = 0;
            Key _key;

            Console.WriteLine(lines[1]);

            //break out early if empty file
            if (lines.Length == 0)
            {
                Console.WriteLine("No keybind set");
                data.startAutoTracker1 = null;
                return;
            }

            if (lines.Length > 1)
                key = lines[1];

            //get first line, split around +'s
            string modifiers = lines[0].ToLower();
            if (modifiers.IndexOf('+') > 0)
            {
                mod1 = modifiers.Substring(0, modifiers.IndexOf('+'));
                modifiers = modifiers.Substring(modifiers.IndexOf('+') + 1);
                modsUsed++;
            }
            else
            {
                mod1 = modifiers;
            }
            if (modifiers.IndexOf('+') > 0)
            {
                mod2 = modifiers.Substring(0, modifiers.IndexOf('+'));
                modifiers = modifiers.Substring(modifiers.IndexOf('+') + 1);
                modsUsed++;
            }
            else
            {
                mod2 = modifiers;
            }
            if (modifiers.Length > 0)
            {
                mod3 = modifiers;
                modsUsed++;
            }

            if (mod1.Contains("ctrl"))
                mod1 = "control";
            if (mod2.Contains("ctrl"))
                mod2 = "control";
            if (mod3.Contains("ctrl"))
                mod3 = "control";

            //capitalize all letters
            mod1 = UpperCaseFirst(mod1);
            mod2 = UpperCaseFirst(mod2);
            mod3 = UpperCaseFirst(mod3);
            key = UpperCaseFirst(key);

            //if no modifiers, only 1 key
            if (key == "")
            {
                Enum.TryParse(mod1, out _key);
                data.startAutoTracker1 = new GlobalHotkey(ModifierKeys.None, _key, StartHotkey);
                HotkeysManager.AddHotkey(data.startAutoTracker1);
                return;
            }

            //check for modifiers, however many
            if (mod1 != "")
                Enum.TryParse(mod1, out _mod1);
            if (mod2 != "")
                Enum.TryParse(mod2, out _mod2);
            if (mod3 != "")
                Enum.TryParse(mod3, out _mod3);

            //per used amount
            if (modsUsed == 3)
            {
                Console.WriteLine("idk = " + mod1 + " " + mod2 + " " + mod3 + " " + key);
                if (key == "1" || key == "2" || key == "3" || key == "4" || key == "5"
                     || key == "6" || key == "7" || key == "8" || key == "9" || key == "0")
                {
                    Enum.TryParse(ConvertKeyNumber(key, true), out _key);
                    data.startAutoTracker1 = new GlobalHotkey((_mod1 | _mod2 | _mod3), _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker1);

                    Enum.TryParse(ConvertKeyNumber(key, false), out _key);
                    data.startAutoTracker2 = new GlobalHotkey((_mod1 | _mod2 | _mod3), _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker2);
                    return;
                }
                Enum.TryParse(key, out _key);
                data.startAutoTracker1 = new GlobalHotkey((_mod1 | _mod2 | _mod3), _key, StartHotkey);
                HotkeysManager.AddHotkey(data.startAutoTracker1);
                return;
            }
            else if (modsUsed == 2)
            {
                Console.WriteLine("idk = " + mod1 + " " + mod2 + " " + key);
                if (key == "1" || key == "2" || key == "3" || key == "4" || key == "5"
                     || key == "6" || key == "7" || key == "8" || key == "9" || key == "0")
                {
                    Enum.TryParse(ConvertKeyNumber(key, true), out _key);
                    data.startAutoTracker1 = new GlobalHotkey((_mod1 | _mod2), _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker1);

                    Enum.TryParse(ConvertKeyNumber(key, false), out _key);
                    data.startAutoTracker2 = new GlobalHotkey((_mod1 | _mod2), _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker2);
                    return;
                }
                Enum.TryParse(key, out _key);
                data.startAutoTracker1 = new GlobalHotkey((_mod1 | _mod2), _key, StartHotkey);
                HotkeysManager.AddHotkey(data.startAutoTracker1);
                return;
            }
            else
            {
                Console.WriteLine("idk = " + mod1 + " " + key);
                if (key == "1" || key == "2" || key == "3" || key == "4" || key == "5"
                     || key == "6" || key == "7" || key == "8" || key == "9" || key == "0")
                {
                    Enum.TryParse(ConvertKeyNumber(key, true), out _key);
                    data.startAutoTracker1 = new GlobalHotkey(_mod1, _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker1);

                    Enum.TryParse(ConvertKeyNumber(key, false), out _key);
                    data.startAutoTracker2 = new GlobalHotkey(_mod1, _key, StartHotkey);
                    HotkeysManager.AddHotkey(data.startAutoTracker2);
                    return;
                }
                Enum.TryParse(ConvertKey(key), out _key);
                data.startAutoTracker1 = new GlobalHotkey(_mod1, _key, StartHotkey);
                HotkeysManager.AddHotkey(data.startAutoTracker1);
                return;
            }
        }

        private string UpperCaseFirst(string word)
        {
            if (word.Length <= 0)
                return "";

            string firstLetter1 = word.Substring(0, 1);
            string firstLetter2 = firstLetter1.ToUpper();
            string rest = word.Substring(1);

            return firstLetter2 + rest;
        }

        private string ConvertKey(string key)
        {
            switch (key)
            {
                case ".":
                    return "OemPeriod";
                case ",":
                    return "OemComma";
                case "?":
                    return "OemPeriod";
                case "\"":
                    return "OemQuestion";
                case "'":
                    return "OemQuotes";
                case "[":
                    return "OemOpenBrackets";
                case "{":
                    return "OemOpenBrackets";
                case "]":
                    return "OemCloseBrackets";
                case "}":
                    return "OemCloseBrackets";
                case "\\":
                    return "OemBackslash";
                case ":":
                    return "OemSemicolon";
                case ";":
                    return "OemSemicolon";
                case "-":
                    return "OemMinus";
                case "_":
                    return "OemMinus";
                case "+":
                    return "OemPlus";
                case "=":
                    return "OemPlus";
                case "|":
                    return "OemPipe";

                default:
                    return key;
            }
        }

        private string ConvertKeyNumber(string num, bool type)
        {
            switch (num)
            {
                case "1":
                    if (type)
                        return "D1";
                    else
                        return "NumPad1";
                case "2":
                    if (type)
                        return "D2";
                    else
                        return "NumPad2";
                case "3":
                    if (type)
                        return "D3";
                    else
                        return "NumPad3";
                case "4":
                    if (type)
                        return "D4";
                    else
                        return "NumPad4";
                case "5":
                    if (type)
                        return "D5";
                    else
                        return "NumPad5";
                case "6":
                    if (type)
                        return "D6";
                    else
                        return "NumPad6";
                case "7":
                    if (type)
                        return "D7";
                    else
                        return "NumPad7";
                case "8":
                    if (type)
                        return "D8";
                    else
                        return "NumPad8";
                case "9":
                    if (type)
                        return "D9";
                    else
                        return "NumPad9";
                default:
                    if (type)
                        return "D0";
                    else
                        return "NumPad0";
            }
        }
    }
}
