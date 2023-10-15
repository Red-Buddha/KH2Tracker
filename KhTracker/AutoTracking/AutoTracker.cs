using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
//using System.IO;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        MemoryReader memory;//, testMemory;

        private Int32 ADDRESS_OFFSET;
        private static DispatcherTimer aTimer, checkTimer;
        private List<ImportantCheck> importantChecks;
        private Ability highJump;
        private Ability quickRun;
        private Ability dodgeRoll;
        private Ability aerialDodge;
        private Ability glide;

        private Ability secondChance;
        private Ability onceMore;

        private DriveForm valor;
        private DriveForm wisdom;
        private DriveForm master;
        private DriveForm limit;
        private DriveForm final;
        private DriveForm anti;

        private DriveForm finalReal;
        private DriveForm valorReal;

        private Magic fire;
        private Magic blizzard;
        private Magic thunder;
        private Magic magnet;
        private Magic reflect;
        private Magic cure;

        private Report reportItem;
        private Summon charmItem;
        private ImportantCheck proofItem;
        private ImportantCheck visitItem;
        private ImportantCheck extraItem;

        private TornPage pages;

        private World world;
        private Stats stats;
        private Rewards rewards;
        private List<ImportantCheck> collectedChecks;
        private List<ImportantCheck> newChecks;
        private List<ImportantCheck> previousChecks;

        private int fireLevel;
        private int blizzardLevel;
        private int thunderLevel;
        private int cureLevel;
        private int reflectLevel;
        private int magnetLevel;
        private int tornPageCount;

        private CheckEveryCheck checkEveryCheck;

        private bool pcFilesLoaded = false;

        public static bool pcsx2tracking = false; //game version
        private bool onContinue = false; //for death counter
        private bool eventInProgress = false; //boss detection

        //private int lastVersion = 0;

        private int[] temp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] tempPre = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        #endregion

        ///
        /// Autotracking Startup
        ///

        public void StartHotkey()
        {
            if (data.usedHotkey)
                return;

            data.usedHotkey = true;
            InitTracker(null, null);
            //int vercheck = CheckVersion();
            //if (vercheck == 1)
            //{             
            //    InitAutoTracker(true);
            //    return;
            //}
            //else if (vercheck == 2)
            //{
            //    InitAutoTracker(false);
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("No game detected.\nPlease start KH2 before using Hotkey.");
            //    data.usedHotkey = false;
            //}
        }

        //buttons merged so no need for both of these anymore
        //public void InitPCSX2Tracker(object sender, RoutedEventArgs e)
        //{
        //    pcsx2tracking = true;
        //    InitAutoTracker(true);
        //}
        //
        //public void InitPCTracker(object sender, RoutedEventArgs e)
        //{
        //    pcsx2tracking = false;
        //    InitAutoTracker(false);
        //}

        public void InitTracker(object sender, RoutedEventArgs e)
        {
            if (aTimer != null && aTimer.IsEnabled)
            {
                return;
            }

            InitTracker();
        }

        private void InitTracker()
        {
            //connection trying visual
            Connect.Visibility = Visibility.Visible;
            Connect2.Visibility = Visibility.Collapsed;

            //check timer already running!
            if (checkTimer != null && checkTimer.IsEnabled)
                return;

            //reset timer if already running
            aTimer?.Stop();

            //start timer for checking game version
            checkTimer = new DispatcherTimer();
            checkTimer.Tick += InitSearch;
            checkTimer.Interval = new TimeSpan(0, 0, 0, 2, 5);
            checkTimer.Start();
        }

        public void InitSearch(object sender, EventArgs e)
        {
            //NOTE: connected version
            //0 = none | 1 = ps2 | 2 = pc
            int checkedVer = CheckVersion();

            if (checkedVer == 0) //no game was detected.
            {
                //return and keep trying to connect if auto-connect is enabled.
                if (AutoConnectOption.IsChecked) 
                {
                    return;
                }
                else
                {
                    Connect.Visibility = Visibility.Collapsed;
                    Connect2.Visibility = Visibility.Visible;
                    Connect2.Source = data.AD_Cross;
                    checkTimer.Stop();
                    checkTimer = null;
                    memory = null;
                    if(data.usedHotkey)
                    {
                        MessageBox.Show("No game detected.\nPlease start KH2 before using Hotkey.");
                        data.usedHotkey = false;
                    }
                    else
                        MessageBox.Show("Please start KH2 before starting the Auto Tracker.");
                }
            }
            else
            {
                //if for some reason user starts playing an different version
                if (data.lastVersion !=0 && data.lastVersion != checkedVer)
                {
                    //reset tracker
                    OnReset(null, null);
                }

                //stop timer for checking game version
                if (checkTimer!= null)
                {
                    checkTimer.Stop();
                    checkTimer = null;
                }

                //set correct connect visual
                if (data.lastVersion == 1)
                {
                    //Console.WriteLine("PCSX2 Found, starting Auto-Tracker");
                    Connect2.Source = data.AD_PS2;
                }
                else
                {
                    //Console.WriteLine("PC Found, starting Auto-Tracker");
                    Connect2.Source = data.AD_PCred;
                }

                //make visual visible
                Connect.Visibility = Visibility.Collapsed;
                Connect2.Visibility = Visibility.Visible;

                //finally start auto-tracking process
                InitAutoTracker(pcsx2tracking);
            }
        }

        public int CheckVersion()
        {
            bool pcsx2Success = true;
            bool pcSuccess = true;
            int tries = 0;

            //check emulator
            do
            {
                memory = new MemoryReader(true);
                if (tries < 20)
                {
                    tries++;
                }
                else
                {
                    memory = null;
                    //Console.WriteLine("No PCSX2 Version Detected");
                    pcsx2Success = false;
                    break;
                }
            } while (!memory.Hooked);
            if (pcsx2Success)
            {
                pcsx2tracking = true;
                if (data.lastVersion == 0)
                    data.lastVersion = 1;
                return 1;
            }

            //check pc now
            tries = 0;
            do
            {
                memory = new MemoryReader(false);
                if (tries < 20)
                {
                    tries++;
                }
                else
                {
                    memory = null;
                    //Console.WriteLine("No PC Version Detected");
                    pcSuccess = false;
                    break;
                }
            } while (!memory.Hooked);
            if (pcSuccess)
            {
                pcsx2tracking = false;
                if (data.lastVersion == 0)
                    data.lastVersion = 2;
                return 2;
            }

            //no version found
            return 0;
        }

        public async void InitAutoTracker(bool PCSX2)
        {
            // PC Address anchors
            int Now = 0x0714DB8;
            int Save = 0x09A70B0;
            int Sys3 = 0x0; //old base address 0x2A59DF0;
            int Bt10 = 0x0; //old base address 0x2A74880;
            int BtlEnd = 0x2A0D3E0;
            int Slot1 = 0x2A20C98;
            int NextSlot = 0x278;

            if (!PCSX2)
            {
                Connect2.Source = data.AD_PCred;

                try
                {
                    CheckPCOffset();
                }
                catch (Win32Exception)
                {
                    memory = null;
                    Connect2.Source = data.AD_Cross;
                    MessageBox.Show("Unable to access KH2FM try running KHTracker as admin");
                    return;
                }
                catch
                {
                    memory = null;
                    Connect2.Source = data.AD_Cross;
                    MessageBox.Show("Error connecting to KH2FM");
                    return;
                }


                //Connect2.Source = data.AD_PCred;
                //Connect.Visibility = Visibility.Collapsed;
                //Connect2.Visibility = Visibility.Visible;
                //check for if the system files are loaded
                //this helps ensure that ICs on levels/drives don't mistrack
                while (!pcFilesLoaded)
                {
                    Sys3 = ReadPcPointer(0x2AE3550);
                    Bt10 = ReadPcPointer(0x2AE3558);
                    pcFilesLoaded = CheckPCLoaded(Sys3, Bt10);
                    await Task.Delay(100);
                }

                FinishSetup(PCSX2, Now, Save, Sys3, Bt10, BtlEnd, Slot1, NextSlot);
            }
            else
            {
                try
                {
                    CheckPS2Offset();
                }
                catch (Win32Exception)
                {
                    memory = null;
                    Connect2.Source = data.AD_Cross;
                    MessageBox.Show("Unable to access PCSX2 try running KHTracker as admin");
                    return;
                }
                catch
                {
                    memory = null;
                    Connect2.Source = data.AD_Cross;
                    MessageBox.Show("Error connecting to PCSX2");
                    return;
                }

                // PCSX2 anchors 
                Now = 0x032BAE0;
                Save = 0x032BB30;
                Sys3 = ReadMemInt(0x1C61AF8); //old base address 0x1CCB300;
                Bt10 = ReadMemInt(0x1C61AFC); //old base address 0x1CE5D80;
                BtlEnd = 0x1D490C0;
                Slot1 = 0x1C6C750;
                NextSlot = 0x268;

                FinishSetup(PCSX2, Now, Save, Sys3, Bt10, BtlEnd, Slot1, NextSlot);
            }
        }

        private void CheckPS2Offset()
        {
            bool found = false;
            Int32 offset = 0x00000000;
            Int32 testAddr = 0x0032EE36;
            string good = "F680";
            while (!found)
            {
                string tester = BytesToHex(memory.ReadMemory(testAddr + offset, 2));
                if (tester == "Service not started. Waiting for PCSX2")
                {
                    break;
                }
                else if (tester == good)
                {
                    found = true;
                }
                else
                {
                    offset += 0x10000000;
                }
            }
            ADDRESS_OFFSET = offset;
        }

        private void CheckPCOffset()
        {
            Int32 testAddr = 0x009AA376 - 0x1000;
            string good = "F680";
            string tester = BytesToHex(memory.ReadMemory(testAddr, 2));
            if (tester == good)
            {
                ADDRESS_OFFSET = -0x1000;
            }
        }

        private bool CheckPCLoaded(int system3, int battle0)
        {
            //Testchecks if these files have been loaded into memeory
            string testS = ReadMemString(system3, 3);
            string testB = ReadMemString(battle0, 3);

            //Console.WriteLine("sys: " + testS);
            //Console.WriteLine("btl: " + testB);

            if (testB == testS && testS == "BAR")
            {
                //all important files loaded
                Connect2.Source = data.AD_PC;
                return true;
            }

            //Console.WriteLine("Not yet");
            return false;
        }

        private void FinishSetup(bool PCSX2, Int32 Now, Int32 Save, Int32 Sys3, Int32 Bt10, Int32 BtlEnd, Int32 Slot1, Int32 NextSlot)
        {
            #region Add ICs
            importantChecks = new List<ImportantCheck>();
            importantChecks.Add(highJump = new Ability(memory, Save + 0x25CE, ADDRESS_OFFSET, 93, "HighJump"));
            importantChecks.Add(quickRun = new Ability(memory, Save + 0x25D0, ADDRESS_OFFSET, 97, "QuickRun"));
            importantChecks.Add(dodgeRoll = new Ability(memory, Save + 0x25D2, ADDRESS_OFFSET, 563, "DodgeRoll"));
            importantChecks.Add(aerialDodge = new Ability(memory, Save + 0x25D4, ADDRESS_OFFSET, 101, "AerialDodge"));
            importantChecks.Add(glide = new Ability(memory, Save + 0x25D6, ADDRESS_OFFSET, 105, "Glide"));

            importantChecks.Add(secondChance = new Ability(memory, Save + 0x2544, ADDRESS_OFFSET, "SecondChance", Save));
            importantChecks.Add(onceMore = new Ability(memory, Save + 0x2544, ADDRESS_OFFSET, "OnceMore", Save));

            importantChecks.Add(wisdom = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 2, Save + 0x332E, "Wisdom"));
            importantChecks.Add(limit = new DriveForm(memory, Save + 0x36CA, ADDRESS_OFFSET, 3, Save + 0x3366, "Limit"));
            importantChecks.Add(master = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 6, Save + 0x339E, "Master"));
            importantChecks.Add(anti = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 5, Save + 0x340C, "Anti"));

            if (!data.altFinalTracking)
            {
                importantChecks.Add(valor = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 1, Save + 0x32F6, Save + 0x06B2, "Valor"));
                importantChecks.Add(final = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 4, Save + 0x33D6, "Final"));
            }
            else
            {
                importantChecks.Add(valor = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 7, Save + 0x32F6, "Valor"));
                importantChecks.Add(final = new DriveForm(memory, Save + 0x36C2, ADDRESS_OFFSET, 1, Save + 0x33D6, "Final"));

                importantChecks.Add(finalReal = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 4, Save + 0x33D6, "FinalReal"));
                importantChecks.Add(valorReal = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 1, Save + 0x32F6, Save + 0x06B2, "ValorReal"));
            }
               
            int fireCount = fire != null ? fire.Level : 0;
            int blizzardCount = blizzard != null ? blizzard.Level : 0;
            int thunderCount = thunder != null ? thunder.Level : 0;
            int cureCount = cure != null ? cure.Level : 0;
            int magnetCount = magnet != null ? magnet.Level : 0;
            int reflectCount = reflect != null ? reflect.Level : 0;

            importantChecks.Add(fire = new Magic(memory, Save + 0x3594, Save + 0x1CF2, ADDRESS_OFFSET, "Fire"));
            importantChecks.Add(blizzard = new Magic(memory, Save + 0x3595, Save + 0x1CF3, ADDRESS_OFFSET, "Blizzard"));
            importantChecks.Add(thunder = new Magic(memory, Save + 0x3596, Save + 0x1CF4, ADDRESS_OFFSET, "Thunder"));
            importantChecks.Add(cure = new Magic(memory, Save + 0x3597, Save + 0x1CF5, ADDRESS_OFFSET, "Cure"));
            importantChecks.Add(magnet = new Magic(memory, Save + 0x35CF, Save + 0x1CF6, ADDRESS_OFFSET, "Magnet"));
            importantChecks.Add(reflect = new Magic(memory, Save + 0x35D0, Save + 0x1CF7, ADDRESS_OFFSET, "Reflect"));

            fire.Level = fireCount;
            blizzard.Level = blizzardCount;
            thunder.Level = thunderCount;
            cure.Level = cureCount;
            magnet.Level = magnetCount;
            reflect.Level = reflectCount;

            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C4, ADDRESS_OFFSET, 6, "Report1"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C4, ADDRESS_OFFSET, 7, "Report2"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 0, "Report3"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 1, "Report4"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 2, "Report5"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 3, "Report6"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 4, "Report7"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 5, "Report8"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 6, "Report9"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C5, ADDRESS_OFFSET, 7, "Report10"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C6, ADDRESS_OFFSET, 0, "Report11"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C6, ADDRESS_OFFSET, 1, "Report12"));
            importantChecks.Add(reportItem = new Report(memory, Save + 0x36C6, ADDRESS_OFFSET, 2, "Report13"));

            importantChecks.Add(charmItem = new Summon(memory, Save + 0x36C0, ADDRESS_OFFSET, 3, "Baseball"));
            importantChecks.Add(charmItem = new Summon(memory, Save + 0x36C0, ADDRESS_OFFSET, 0, "Ukulele"));
            importantChecks.Add(charmItem = new Summon(memory, Save + 0x36C4, ADDRESS_OFFSET, 4, "Lamp"));
            importantChecks.Add(charmItem = new Summon(memory, Save + 0x36C4, ADDRESS_OFFSET, 5, "Feather"));

            importantChecks.Add(proofItem = new Proof(memory, Save + 0x3694, ADDRESS_OFFSET, "PromiseCharm"));
            importantChecks.Add(proofItem = new Proof(memory, Save + 0x36B4, ADDRESS_OFFSET, "Peace"));
            importantChecks.Add(proofItem = new Proof(memory, Save + 0x36B3, ADDRESS_OFFSET, "Nonexistence"));
            importantChecks.Add(proofItem = new Proof(memory, Save + 0x36B2, ADDRESS_OFFSET, "Connection"));

            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35AE, ADDRESS_OFFSET, "AuronWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35AF, ADDRESS_OFFSET, "MulanWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35B3, ADDRESS_OFFSET, "BeastWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35B4, ADDRESS_OFFSET, "JackWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35B5, ADDRESS_OFFSET, "SimbaWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35B6, ADDRESS_OFFSET, "SparrowWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35C0, ADDRESS_OFFSET, "AladdinWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x35C2, ADDRESS_OFFSET, "TronWep"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x3643, ADDRESS_OFFSET, "MembershipCard"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x3649, ADDRESS_OFFSET, "IceCream"));
            importantChecks.Add(visitItem = new Visit(memory, Save + 0x364A, ADDRESS_OFFSET, "Picture"));

            importantChecks.Add(extraItem = new Extra(memory, Save + 0x3696, ADDRESS_OFFSET, "HadesCup"));
            importantChecks.Add(extraItem = new Extra(memory, Save + 0x3644, ADDRESS_OFFSET, "OlympusStone"));
            importantChecks.Add(extraItem = new Extra(memory, Save + 0x365F, ADDRESS_OFFSET, "UnknownDisk"));
            importantChecks.Add(extraItem = new Extra(memory, Save + 0x363C, ADDRESS_OFFSET, "MunnyPouch1"));
            importantChecks.Add(extraItem = new Extra(memory, Save + 0x3695, ADDRESS_OFFSET, "MunnyPouch2"));

            //change this for flag checking to determine amount of pages?
            int count = pages != null ? pages.Quantity : 0;
            importantChecks.Add(pages = new TornPage(memory, Save + 0x3598, ADDRESS_OFFSET, "TornPage"));
            pages.Quantity = count;

            #endregion

            if (PCSX2)
                world = new World(memory, ADDRESS_OFFSET, Now, 0x00351EC8, Save + 0x1CFF);
            else
                world = new World(memory, ADDRESS_OFFSET, Now, BtlEnd + 0x820, Save + 0x1CFF);

            stats = new Stats(memory, ADDRESS_OFFSET, Save + 0x24FE, Slot1 + 0x188, Save + 0x3524, Save + 0x3700, NextSlot);
            rewards = new Rewards(memory, ADDRESS_OFFSET, Bt10);

            if(!data.altFinalTracking)
                checkEveryCheck = new CheckEveryCheck(memory, ADDRESS_OFFSET, Save, Sys3, Bt10, world, stats, rewards, valor, wisdom, limit, master, final);


            // set stat info visibiliy
            Level.Visibility = Visibility.Visible;
            Strength.Visibility = Visibility.Visible;
            Magic.Visibility = Visibility.Visible;
            Defense.Visibility = Visibility.Visible;

            if (FormsGrowthOption.IsChecked)
                FormRow.Height = new GridLength(0.5, GridUnitType.Star);

            //levelcheck visibility
            NextLevelDisplay();
            DeathCounterDisplay();
            SetBindings();
            SetTimer();
            //OnTimedEvent(null, null);
        }

        ///
        /// Autotracking general
        ///

        private void SetTimer()
        {
            aTimer?.Stop();
            aTimer = new DispatcherTimer();
            aTimer.Tick += OnTimedEvent;
            aTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            aTimer.Start();

            data.wasTracking = true;
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            previousChecks.Clear();
            previousChecks.AddRange(newChecks);
            newChecks.Clear();
            int correctSlot = 0;

            try
            {
                //current world
                world.UpdateMemory();        

                //test displaying sora's correct stats for PR 1st forsed fight
                if (world.worldNum == 16 && world.roomNumber == 1 && (world.eventID1 == 0x33 || world.eventID1 == 0x34))
                    correctSlot = 2; //move forward this number of slots

                //updates
                stats.UpdateMemory(correctSlot);        
                HighlightWorld(world);
                UpdateStatValues();
                UpdateWorldProgress(world, false, null);
                UpdateFormProgression();
                DeathCheck();
                LevelsProgressionBonus();
                DrivesProgressionBonus();
                if (LevelValue.Text == "1" && StrengthValue.Text == "0" && MagicValue.Text == "0")
                    AddProgressionPoints(0);

                if (data.mode == Mode.PointsHints || data.ScoreMode)
                {
                    UpdatePointScore(0); //update score
                    GetBoss(world, false, null);
                }

                importantChecks.ForEach(delegate (ImportantCheck importantCheck)
                {
                    importantCheck.UpdateMemory();
                });

                #region For Debugging
                ////Modified to only update if any of these actually change instead of updating every tick
                //temp[0] = world.roomNumber;
                //temp[1] = world.worldNum;
                //temp[2] = world.eventID1;
                //temp[3] = world.eventID2;
                //temp[4] = world.eventID3;
                //temp[5] = world.eventComplete;
                //temp[6] = world.cupRound;
                //if (!Enumerable.SequenceEqual(temp, tempPre))
                //{
                //    Console.WriteLine("world num = " + world.worldNum);
                //    Console.WriteLine("room num  = " + world.roomNumber);
                //    Console.WriteLine("event id1 = " + world.eventID1);
                //    Console.WriteLine("event id2 = " + world.eventID2);
                //    Console.WriteLine("event id3 = " + world.eventID3);
                //    Console.WriteLine("event cpl = " + world.eventComplete);
                //    Console.WriteLine("Cup Round = " + world.cupRound);
                //    Console.WriteLine("===========================");
                //    tempPre[0] = temp[0];
                //    tempPre[1] = temp[1];
                //    tempPre[2] = temp[2];
                //    tempPre[3] = temp[3];
                //    tempPre[4] = temp[4];
                //    tempPre[5] = temp[5];
                //    tempPre[6] = temp[6];
                //}

                //string cntrl = BytesToHex(memory.ReadMemory(0x2A148E8, 1)); //sora controlable
                //Console.WriteLine(cntrl);

                //string tester = BytesToHex(memory.ReadMemory(0x2A22BC0, 4));
                //Console.WriteLine(tester);

                //int testint = BitConverter.ToInt32(memory.ReadMemory(0x2A22BC0, 4), 0);
                //Console.WriteLine(testint);
                //Console.WriteLine(testint+0x2A22BC0+0x10);
                #endregion
            }
            catch
            {

                aTimer.Stop();
                //aTimer = null;
                pcFilesLoaded = false;

                if (AutoConnectOption.IsChecked)
                {
                    InitTracker();
                }
                else
                {
                    Connect.Visibility = Visibility.Collapsed;
                    Connect2.Visibility = Visibility.Visible;
                    Connect2.Source = data.AD_Cross;
                    if (Disconnect.IsChecked)
                    {
                        MessageBox.Show("KH2FM has exited. Stopping Auto Tracker.");
                    }
                    data.usedHotkey = false;
                }

                if(AutoSaveProgress2Option.IsChecked)
                {
                    if (!Directory.Exists("KhTrackerAutoSaves"))
                    {
                        Directory.CreateDirectory("KhTrackerAutoSaves\\");
                    }
                    Save("KhTrackerAutoSaves\\" + "ConnectionLost-Backup_" + DateTime.Now.ToString("yy-MM-dd_H-m") + ".tsv");
                }

                //reset currently highlighted world
                if (WorldHighlightOption.IsChecked && world.previousworldName != null && data.WorldsData.ContainsKey(world.previousworldName))
                {
                    foreach (Rectangle Box in data.WorldsData[world.previousworldName].top.Children.OfType<Rectangle>().Where(Box => Box.Name.EndsWith("SelWG")))
                    {
                        Box.Visibility = Visibility.Collapsed;
                    }
                }

                return;
            }

            UpdateCollectedItems();
            DetermineItemLocations();
        }

        private bool CheckSynthPuzzle()
        {
            if (pcsx2tracking)
            {
                //reminder: FFFF = unloaded)
                string Jounal = BytesToHex(memory.ReadMemory(0x035F144 + ADDRESS_OFFSET, 2)); //in journal
                //reminder: FF = none | 01 = save menu | 03 = load menu | 05 = moogle | 07 = item popup | 08 = pause menu (cutscene/fight) | 0A = pause Menu (normal)
                string menu = BytesToHex(memory.ReadMemory(0x035F2EC + ADDRESS_OFFSET, 2)); //in a menu

                if ((Jounal == "FFFF" && menu == "0500") || (Jounal != "FFFF" && menu == "0A00")) // in moogle shop / in puzzle menu
                {
                    return true;
                }
                return false;
            }
            else
            {
                string Jounal = BytesToHex(memory.ReadMemory(0x741230, 2)); //in journal
                //reminder: FF = none | 01 = save menu | 03 = load menu | 05 = moogle | 07 = item popup | 08 = pause menu (cutscene/fight) | 0A = pause Menu (normal)
                string menu = BytesToHex(memory.ReadMemory(0x741320, 2)); //in a menu

                if ((Jounal == "FFFF" && menu == "0500") || (Jounal != "FFFF" && menu == "0A00")) // in moogle shop / in puzzle menu
                {
                    return true;
                }
                return false;
            }
        }

        //private bool CheckTornPage(Item item)
        //{
        //    //return true and track item for anything that isn't a torn page
        //    if (!item.Name.StartsWith("TornPage"))
        //        return true;
        //
        //    int Tracked = WorldGrid.Real_Pages; //current number of pages tracked to any of the world grids
        //    int Inventory = memory.ReadMemory(ADDRESS_OFFSET + 0x09A70B0 + 0x3598, 1)[0]; //number of pages currently in sora's inventory
        //    int Used = 0; //number of torn pages used so far in 100 AW
        //
        //    //don't try tracking a torn page if we already tracked 5
        //    //as there should only ever be 5 total under normal means.
        //    if(Tracked >= 5)
        //        return false;
        //
        //    //note: Save = 0x09A70B0;
        //    //check current 100 AW story flags to see what pages have been used already.
        //    if (new BitArray(memory.ReadMemory(ADDRESS_OFFSET + 0x09A70B0 + 0x1DB1, 1))[1]) //page 1 used flag
        //        Used = 1;
        //    if (new BitArray(memory.ReadMemory(ADDRESS_OFFSET + 0x09A70B0 + 0x1DB1, 1))[1]) //page 2 used flag
        //        Used = 2;
        //    if (new BitArray(memory.ReadMemory(ADDRESS_OFFSET + 0x09A70B0 + 0x1DB1, 1))[1]) //page 3 used flag
        //        Used = 3;
        //    if (new BitArray(memory.ReadMemory(ADDRESS_OFFSET + 0x09A70B0 + 0x1DB1, 1))[1]) //page 4 used flag
        //        Used = 4;
        //
        //    //if number of torn pages used + current number of pages in sora's inventory
        //    //are equal to the current number of pages tracked, then don't track anything.
        //    if (Used + Inventory == Tracked)
        //        return false;
        //
        //    return true;
        //}

        private void DeathCheck()
        {
            //Note: 04 = dying, 05 = continue screen.
            //note: if i try tracking a death when pausecheck is "0400" then that should give a
            //more accurate death count in the event that continue is selected too fast (i hope)

            string PauseCheck;

            if (pcsx2tracking)
            {
                PauseCheck = BytesToHex(memory.ReadMemory(0x0347E08 + ADDRESS_OFFSET, 2));
            }
            else
            {
                PauseCheck = BytesToHex(memory.ReadMemory(0xAB9078, 2));
            }

            //if oncontinue is true then we want to check if the values for sora is currently dying or on continue screen.
            //we need to chck this to prevent the counter rapidly counting up every frame adnd such
            if (onContinue)
            {
                if (PauseCheck == "0400" || PauseCheck == "0500")
                    return;
                else
                    onContinue = false;
            }

            // if sora is currently dying or on the continue screen
            // then increase death count and set oncontinue
            if (PauseCheck == "0400" || PauseCheck == "0500")
            {
                DeathCounter++;
                onContinue = true;
            }

            DeathValue.Text = DeathCounter.ToString();
        }

        private void UpdateStatValues()
        {
            // we don't need bindings anymore (i think) so use this instead

            //Main window
            //Stats
            stats.SetNextLevelCheck(stats.Level);
            LevelValue.Text = stats.Level.ToString();
            StrengthValue.Text = stats.Strength.ToString();
            MagicValue.Text = stats.Magic.ToString();
            DefenseValue.Text = stats.Defense.ToString();
            //forms
            ValorLevel.Text = valor.VisualLevel.ToString();
            WisdomLevel.Text = wisdom.VisualLevel.ToString();
            LimitLevel.Text = limit.VisualLevel.ToString();
            MasterLevel.Text = master.VisualLevel.ToString();
            FinalLevel.Text = final.VisualLevel.ToString();
            //growth
            HighJumpLevel.Text = highJump.Level.ToString();
            QuickRunLevel.Text = quickRun.Level.ToString();
            DodgeRollLevel.Text = dodgeRoll.Level.ToString();
            AerialDodgeLevel.Text = aerialDodge.Level.ToString();
            GlideLevel.Text = glide.Level.ToString();
        }

        private void TrackItem(string itemName, WorldGrid world)
        {
            Grid ItemRow;
            try //try getting itemrow grid from dictionary
            {
                ItemRow = data.Items[itemName].Item2;
            }
            catch //if item is not from pool (growth) then log the item and return
            {
                App.logger?.Record(itemName + " tracked");
                return;
            }

            //do a check in the report handler to actually make sure reports don't
            //track to the wrong place in the case of mismatched seeds/hints
            if (ItemRow.FindName(itemName) is Item item && item.IsVisible)
            {
                bool validItem = world.ReportHandler(item);

                if (validItem)
                {
                    world.Add_Item(item);
                    App.logger?.Record(item.Name + " tracked");
                }
            }
        }
        
        private void TrackQuantities()
        {
            while (fire.Level > fireLevel)
            {
                ++fireLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Fire" + fireLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (blizzard.Level > blizzardLevel)
            {
                ++blizzardLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Blizzard" + blizzardLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (thunder.Level > thunderLevel)
            {
                ++thunderLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Thunder" + thunderLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (cure.Level > cureLevel)
            {
                ++cureLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Cure" + cureLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (reflect.Level > reflectLevel)
            {
                ++reflectLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Reflect" + reflectLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (magnet.Level > magnetLevel)
            {
                ++magnetLevel;
                Magic magic = new Magic(null, 0, 0, 0, "Magnet" + magnetLevel.ToString());
                newChecks.Add(magic);
                collectedChecks.Add(magic);
            }
            while (pages.Quantity > tornPageCount)
            {
                ++tornPageCount;
                TornPage page = new TornPage(null, 0, 0, "TornPage" + tornPageCount.ToString());
                newChecks.Add(page);
                collectedChecks.Add(page);
            }
        }

        //progression hints level bonus
        private void LevelsProgressionBonus()
        {
            //if sora's current level is great than the max specified level (usually 50), then do nothing
            if (stats.Level > (data.Levels_ProgressionValues.Count * 10) || !data.UsingProgressionHints)
                return;

            //every 10 levels, reward the player the progression points for that part
            while (stats.Level > data.NextLevelMilestone)
            {
                data.NextLevelMilestone += 10;
                AddProgressionPoints(data.Levels_ProgressionValues[data.LevelsPreviousIndex++]);
            }
        }

        private void DrivesProgressionBonus()
        {
            if (!data.UsingProgressionHints)
                return;

            //check valor
            while (valor.Level > data.DriveLevels[0])
            {
                //Console.WriteLine("data.DriveLevels[0] Current = " + data.DriveLevels[0]);
                //Console.WriteLine("data.Drives_ProgressionValues[data.DriveLevels[0]] = " + data.Drives_ProgressionValues[data.DriveLevels[0] - 1]);
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[0] - 1]);
                data.DriveLevels[0]++;
            }
            while (wisdom.Level > data.DriveLevels[1])
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[1] - 1]);
                data.DriveLevels[1]++;
            }
            while (limit.Level > data.DriveLevels[2])
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[2] - 1]);
                data.DriveLevels[2]++;
            }
            while (master.Level > data.DriveLevels[3])
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[3] - 1]);
                data.DriveLevels[3]++;
            }
            while (final.Level > data.DriveLevels[4])
            {
                AddProgressionPoints(data.Drives_ProgressionValues[data.DriveLevels[4] - 1]);
                data.DriveLevels[4]++;
            }
        }

        private void UpdateWorldProgress(World world, bool usingSave, Tuple<string, int, int, int, int, int> saveTuple)
        {
            string wName;
            int wRoom;
            int wID1;
            int wID2;
            int wID3;
            int wCom;
            if (!usingSave)
            {
                wName = world.worldName;
                wRoom = world.roomNumber;
                wID1 = world.eventID1;
                wID2 = world.eventID2;
                wID3 = world.eventID3;
                wCom = world.eventComplete;
            }
            else
            {
                wName = saveTuple.Item1;
                wRoom = saveTuple.Item2;
                wID1 = saveTuple.Item3;
                wID2 = saveTuple.Item4;
                wID3 = saveTuple.Item5;
                wCom = 1;
            }

            if (wName == "DestinyIsland" || wName == "Unknown")
                return;

            //check event
            var eventTuple = new Tuple<string, int, int, int, int, int>(wName, wRoom, wID1, wID2, wID3, 0);
            if (data.eventLog.Contains(eventTuple))
                return;

            //check for valid progression Content Controls first
            ContentControl progressionM = data.WorldsData[wName].progression;

            //Get current icon prefixes (simple, game, or custom icons)
            bool OldToggled = Properties.Settings.Default.OldProg;
            bool CustomToggled = Properties.Settings.Default.CustomIcons;
            string Prog = "Min-"; //Default
            if (OldToggled)
                Prog = "Old-";
            if (CustomProgFound && CustomToggled)
                Prog = "Cus-";

            //progression defaults
            int curProg = data.WorldsData[wName].progress; //current world progress int
            int newProg = 99;
            bool updateProgression = true;
            bool updateProgressionPoints = true;

            //get current world's new progress key
            switch (wName)
            {
                case "SimulatedTwilightTown":
                    switch (wRoom) //check based on room number now, then based on events in each room
                    {
                        case 1:
                            if ((wID3 == 56 || wID3 == 55) && curProg == 0) // Roxas' Room (Day 1)/(Day 6)
                                newProg = 1;
                            break;
                        case 8:
                            if (wID1 == 110 || wID1 == 111) // Get Ollete Munny Pouch (min/max munny cutscenes)
                                newProg = 2;
                            break;
                        case 34:
                            if (wID1 == 157 && wCom == 1) // Twilight Thorn finish
                                newProg = 3;
                            break;
                        case 5:
                            if (wID1 == 87 && wCom == 1) // Axel 1 Finish
                                newProg = 4;
                            if (wID1 == 88 && wCom == 1) // Setzer finish
                                newProg = 5;
                            break;
                        case 21:
                            if (wID3 == 1) // Mansion: Computer Room
                                newProg = 6;
                            break;
                        case 20:
                            if (wID1 == 137 && wCom == 1) // Axel 2 finish
                                newProg = 7;
                            break;
                        default: //if not in any of the above rooms then just leave
                            updateProgression = false;
                            break;
                    }
                    break;
                case "TwilightTown":
                    switch (wRoom)
                    {
                        case 9:
                            if (wID3 == 117 && curProg == 0) // Roxas' Room (Day 1)
                                newProg = 1;
                            break;
                        case 8:
                            if (wID3 == 108 && wCom == 1) // Station Nobodies
                                newProg = 2;
                            break;
                        case 27:
                            if (wID3 == 4) // Yen Sid after new clothes
                                newProg = 3;
                            break;
                        case 4:
                            if (wID1 == 80 && wCom == 1) // Sandlot finish
                                newProg = 4;
                            break;
                        case 41:
                            if (wID1 == 186 && wCom == 1) // Mansion fight finish
                                newProg = 5;
                            break;
                        case 40:
                            if (wID1 == 161 && wCom == 1) // Betwixt and Between finish
                                newProg = 6;
                            break;
                        case 20:
                            if (wID1 == 213 && wCom == 1) // Data Axel finish
                                newProg = 7;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "HollowBastion":
                    switch (wRoom)
                    {
                        case 0:
                        case 10:
                            if ((wID3 == 1 || wID3 == 2) && curProg == 0) // Villain's Vale (HB1)
                                newProg = 1;
                            break;
                        case 8:
                            if (wID1 == 52 && wCom == 1) // Bailey finish
                                newProg = 2;
                            break;
                        case 5:
                            if (wID3 == 20) // Ansem Study post Computer
                                newProg = 3;
                            break;
                        case 20:
                            if (wID1 == 86 && wCom == 1) // Corridor finish
                                newProg = 4;
                            break;
                        case 18:
                            if (wID1 == 73 && wCom == 1) // Dancers finish
                                newProg = 5;
                            break;
                        case 4:
                            if (wID1 == 55 && wCom == 1) // HB Demyx finish
                                newProg = 6;
                            else if (wID1 == 114 && wCom == 1) // Data Demyx finish
                            {
                                if (curProg == 9) //sephi finished
                                    newProg = 11; //data demyx + sephi finished
                                else if (curProg != 11) //just demyx
                                    newProg = 10;
                                if (data.UsingProgressionHints)
                                {
                                    UpdateProgressionPoints(wName, 10);
                                    updateProgressionPoints = false;
                                }
                            }
                            break;
                        case 16:
                            if (wID1 == 65 && wCom == 1) // FF Cloud finish
                                newProg = 7;
                            break;
                        case 17:
                            if (wID1 == 66 && wCom == 1) // 1k Heartless finish
                                newProg = 8;
                            break;
                        case 1:
                            if (wID1 == 75 && wCom == 1) // Sephiroth finish
                            {
                                if (curProg == 10) //demyx finish
                                    newProg = 11; //data demyx + sephi finished
                                else if (curProg != 11) //just sephi
                                    newProg = 9;
                                if(data.UsingProgressionHints)
                                {
                                    UpdateProgressionPoints(wName, 9);
                                    updateProgressionPoints = false;
                                }
                            }
                            break;
                        //CoR
                        case 21:
                            if ((wID3 == 1 || wID3 == 2) && data.WorldsData["GoA"].progress == 0) //Enter CoR
                            {
                                GoAProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["GoA"][1]);
                                data.WorldsData["GoA"].progress = 1;
                                data.WorldsData["GoA"].progression.ToolTip = data.ProgressKeys["GoADesc"][1];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("CavernofRemembrance", 1);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 22:
                            if (wID3 == 1 && data.WorldsData["GoA"].progress <= 1 && wCom == 1) //valves after skip
                            {
                                GoAProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["GoA"][5]);
                                data.WorldsData["GoA"].progress = 5;
                                data.WorldsData["GoA"].progression.ToolTip = data.ProgressKeys["GoADesc"][5];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("CavernofRemembrance", 3);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 24:
                            if (wID3 == 1 && wCom == 1) //first fight
                            {
                                GoAProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["GoA"][2]);
                                data.WorldsData["GoA"].progress = 2;
                                data.WorldsData["GoA"].progression.ToolTip = data.ProgressKeys["GoADesc"][2];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("CavernofRemembrance", 2);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            if (wID3 == 2 && wCom == 1) //second fight
                            {
                                GoAProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["GoA"][3]);
                                data.WorldsData["GoA"].progress = 3;
                                data.WorldsData["GoA"].progression.ToolTip = data.ProgressKeys["GoADesc"][3];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("CavernofRemembrance", 4);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 25:
                            if (wID3 == 3 && wCom == 1) //transport
                            {
                                GoAProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["GoA"][4]);
                                data.WorldsData["GoA"].progress = 4;
                                data.WorldsData["GoA"].progression.ToolTip = data.ProgressKeys["GoADesc"][4];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("CavernofRemembrance", 5);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "BeastsCastle":
                    switch (wRoom)
                    {
                        case 0:
                        case 2:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Entrance Hall (BC1)
                                newProg = 1;
                            break;
                        case 11:
                            if (wID1 == 72 && wCom == 1) // Thresholder finish
                                newProg = 2;
                            break;
                        case 3:
                            if (wID1 == 69 && wCom == 1) // Beast finish
                                newProg = 3;
                            break;
                        case 5:
                            if (wID1 == 79 && wCom == 1) // Dark Thorn finish
                                newProg = 4;
                            break;
                        case 4:
                            if (wID1 == 74 && wCom == 1) // Dragoons finish
                                newProg = 5;
                            break;
                        case 15:
                            if (wID1 == 82 && wCom == 1) // Xaldin finish
                                newProg = 6;
                            else if (wID1 == 97 && wCom == 1) // Data Xaldin finish
                                newProg = 7;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "OlympusColiseum":
                    switch (wRoom)
                    {
                        case 3:
                            if ((wID3 == 1 || wID3 == 12) && curProg == 0) // The Coliseum (OC1) | Underworld Entrance (OC2)
                                newProg = 1;
                            break;
                        case 7:
                            if (wID1 == 114 && wCom == 1) // Cerberus finish
                                newProg = 2;
                            break;
                        case 0:
                            if ((wID3 == 1 || wID3 == 12) && curProg == 0) // (reverse rando)
                                newProg = 1;
                            if (wID1 == 141 && wCom == 1) // Urns finish
                                newProg = 3;
                            break;
                        case 17:
                            if (wID1 == 123 && wCom == 1) // OC Demyx finish
                                newProg = 4;
                            break;
                        case 8:
                            if (wID1 == 116 && wCom == 1) // OC Pete finish
                                newProg = 5;
                            break;
                        case 18:
                            if (wID1 == 171 && wCom == 1) // Hydra finish
                                newProg = 6;
                            break;
                        case 6:
                            if (wID1 == 126 && wCom == 1) // Auron Statue fight finish
                                newProg = 7;
                            break;
                        case 19:
                            if (wRoom == 19 && wID1 == 202 && wCom == 1) // Hades finish
                                newProg = 8;
                            break;
                        case 34:
                            if (wID1 == 151 && wCom == 1) // AS Zexion finish
                                newProg = 9;
                            if (wID1 == 152 && wCom == 1) // Data Zexion finish
                                newProg = 10;
                            //else if ((wID1 == 152) && wCom == 1) // Data Zexion finish
                            //{
                            //    if (data.UsingProgressionHints)
                            //        UpdateProgressionPoints(wName, 10);
                            //    data.eventLog.Add(eventTuple);
                            //    return;
                            //}
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "Agrabah":
                    switch (wRoom)
                    {
                        case 0:
                        case 4:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Agrabah (Ag1) || The Vault (Ag2)
                                newProg = 1;
                            break;
                        case 9:
                            if (wID1 == 2 && wCom == 1) // Abu finish
                                newProg = 2;
                            break;
                        case 13:
                            if (wID1 == 79 && wCom == 1) // Chasm fight finish
                                newProg = 3;
                            break;
                        case 10:
                            if (wID1 == 58 && wCom == 1) // Treasure Room finish
                                newProg = 4;
                            break;
                        case 3:
                            if (wID1 == 59 && wCom == 1) // Lords finish
                                newProg = 5;
                            break;
                        case 14:
                            if (wID1 == 101 && wCom == 1) // Carpet finish
                                newProg = 6;
                            break;
                        case 5:
                            if (wID1 == 62 && wCom == 1) // Genie Jafar finish
                                newProg = 7;
                            break;
                        case 33:
                            if (wID1 == 142 && wCom == 1) // AS Lexaeus finish
                                newProg = 8;
                            if (wID1 == 147 && wCom == 1) // Data Lexaeus finish
                                newProg = 9;
                            //else if ((wID1 == 147) && wCom == 1) // Data Lexaeus
                            //{
                            //    if (data.UsingProgressionHints)
                            //        UpdateProgressionPoints(wName, 9);
                            //    data.eventLog.Add(eventTuple);
                            //    return;
                            //}
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "LandofDragons":
                    switch (wRoom)
                    {
                        case 0:
                        case 12:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Bamboo Grove (LoD1)
                                newProg = 1;
                            break;
                        case 1:
                            if (wID1 == 70 && wCom == 1) // Mission 3 (Search) finish
                                newProg = 2;
                            break;
                        case 3:
                            if (wID1 == 71 && wCom == 1) // Mountain Climb finish
                                newProg = 3;
                            break;
                        case 5:
                            if (wID1 == 72 && wCom == 1) // Cave finish
                                newProg = 4;
                            break;
                        case 7:
                            if (wID1 == 73 && wCom == 1) // Summit finish
                                newProg = 5;
                            break;
                        case 9:
                            if (wID1 == 75 && wCom == 1) // Shan Yu finish
                                newProg = 6;
                            break;
                        case 10:
                            if (wID1 == 78 && wCom == 1) // Antechamber fight finish
                                newProg = 7;
                            break;
                        case 8:
                            if (wID1 == 79 && wCom == 1) // Storm Rider finish
                                newProg = 8;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "HundredAcreWood":
                    switch (wRoom)
                    {
                        case 2:
                            if ((wID3 == 1 || wID3 == 21 || wID3 == 22) && curProg == 0) // Pooh's house
                                newProg = 1;
                            break;
                        case 6:
                            if (wID1 == 55 && wCom == 1) //A Blustery Rescue Complete
                                newProg = 2;
                            break;
                        case 7:
                            if (wID1 == 57 && wCom == 1) //Hunny Slider Complete
                                newProg = 3;
                            break;
                        case 8:
                            if (wID1 == 59 && wCom == 1) //Balloon Bounce Complete
                                newProg = 4;
                            break;
                        case 9:
                            if (wID1 == 61 && wCom == 1) //The Expotition Complete
                                newProg = 5;
                            break;
                        case 1:
                            if (wID1 == 52 && wCom == 1) //The Hunny Pot Complete
                                newProg = 6;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "PrideLands":
                    switch (wRoom)
                    {
                        case 4:
                        case 16:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Wildebeest Valley (PL1)
                                newProg = 1;
                            break;
                        case 12:
                            if (wID3 == 1) // Oasis after talking to Simba
                                newProg = 2;
                            break;
                        case 2:
                            if (wID1 == 51 && wCom == 1) // Hyenas 1 Finish
                                newProg = 3;
                            break;
                        case 14:
                            if (wID1 == 55 && wCom == 1) // Scar finish
                                newProg = 4;
                            break;
                        case 5:
                            if (wID1 == 57 && wCom == 1) // Hyenas 2 Finish
                                newProg = 5;
                            break;
                        case 15:
                            if (wID1 == 59 && wCom == 1) // Groundshaker finish
                                newProg = 6;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "Atlantica":
                    switch (wRoom)
                    {
                        case 2:
                            if (wID1 == 63) // Tutorial
                                newProg = 1;
                            break;
                        case 9:
                            if (wID1 == 65) // Ursula's Revenge
                                newProg = 2;
                            break;
                        case 4:
                            if (wID1 == 55) // A New Day is Dawning
                                newProg = 3;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "DisneyCastle":
                    switch (wRoom)
                    {
                        case 0:
                            if (wID3 == 22 && curProg == 0) // Cornerstone Hill (TR) (Audience Chamber has no Evt 0x16)
                                newProg = 1;
                            else if (wID1 == 51 && wCom == 1) // Minnie Escort finish
                                newProg = 2;
                            else if (wID3 == 6) // Windows popup (Audience Chamber has no Evt 0x06)
                                newProg = 4;
                            break;
                        case 1:
                            if (wID1 == 53 && curProg == 0) // Library (DC)
                                newProg = 1;
                            else if (wID1 == 58 && wCom == 1) // Old Pete finish
                                newProg = 3;
                            break;
                        case 2:
                            if (wID1 == 52 && wCom == 1) // Boat Pete finish
                                newProg = 5;
                            break;
                        case 3:
                            if (wID1 == 53 && wCom == 1) // DC Pete finish
                                newProg = 6;
                            break;
                        //case 38:
                        //    if ((wID1 == 145 || wID1 == 150) && wCom == 1) // Marluxia finish
                        //    {
                        //        if (curProg == 8)
                        //            newProg = 9; //marluxia + LW finished
                        //        else if (curProg != 9)
                        //            newProg = 7;
                        //        if(data.UsingProgressionHints) 
                        //        {
                        //            if (wID1 == 145)
                        //                UpdateProgressionPoints(wName, 7); // AS
                        //            else
                        //            {
                        //                UpdateProgressionPoints(wName, 8); // Data
                        //                data.eventLog.Add(eventTuple);
                        //                return;
                        //            }
                        //
                        //            updateProgressionPoints = false;
                        //        }
                        //    }
                        //    break;
                        case 38:
                        case 7:
                            if ((wID1 == 145 || wID1 == 150) && wCom == 1) // Marluxia finish
                            {
                                //Marluxia
                                if (curProg != 9 && curProg != 10 && curProg != 11)
                                {
                                    //check if as/data
                                    if (wID1 == 145)
                                        newProg = 7;
                                    if (wID1 == 150)
                                        newProg = 8;
                                }
                                //check for LW
                                else if (curProg == 9 || curProg == 10)
                                {
                                    //check if as/data
                                    if (wID1 == 145)
                                        newProg = 10;
                                    if (wID1 == 150)
                                        newProg = 11;
                                }
                                //progression
                                if (data.UsingProgressionHints)
                                {
                                    if (wID1 == 145)
                                        UpdateProgressionPoints(wName, 7); // AS
                                    else
                                    {
                                        UpdateProgressionPoints(wName, 8); // Data
                                        data.eventLog.Add(eventTuple);
                                        return;
                                    }
                                    updateProgressionPoints = false;
                                }
                            }
                            if (wID1 == 67 && wCom == 1) // Lingering Will finish
                            {
                                //LW
                                if (curProg != 7 && curProg != 8)
                                {
                                    newProg = 9;
                                }
                                //as marluxia beaten
                                else if (curProg == 7)
                                {
                                    newProg = 10;
                                }
                                //data marluxia
                                else if (curProg == 8)
                                {
                                    newProg = 11;
                                }
                                //progression
                                if (data.UsingProgressionHints)
                                {
                                    UpdateProgressionPoints(wName, 9);
                                    updateProgressionPoints = false;
                                }

                            }
                            break;
                            //if (wID1 == 67 && wCom == 1) // Lingering Will finish
                            //{
                            //    if (curProg == 7)
                            //        newProg = 9; //marluxia + LW finished
                            //    else if (curProg != 9)
                            //        newProg = 8;
                            //    if (data.UsingProgressionHints)
                            //    {
                            //        UpdateProgressionPoints(wName, 9);
                            //        updateProgressionPoints = false;
                            //    }
                            //}
                            //break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "HalloweenTown":
                    switch (wRoom)
                    {
                        case 1:
                        case 4:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Hinterlands (HT1)
                                newProg = 1;
                            break;
                        case 6:
                            if (wID1 == 53 && wCom == 1) // Candy Cane Lane fight finish
                                newProg = 2;
                            break;
                        case 3:
                            if (wID1 == 52 && wCom == 1) // Prison Keeper finish
                                newProg = 3;
                            break;
                        case 9:
                            if (wID1 == 55 && wCom == 1) // Oogie Boogie finish
                                newProg = 4;
                            break;
                        case 10:
                            if (wID1 == 62 && wCom == 1) // Children Fight
                                newProg = 5;
                            if (wID1 == 63 && wCom == 1) // Presents minigame
                                newProg = 6;
                            break;
                        case 7:
                            if (wID1 == 64 && wCom == 1) // Experiment finish
                                newProg = 7;
                            break;
                        case 32:
                            if (wID1 == 115 && wCom == 1) // AS Vexen finish
                                newProg = 8;
                            if (wID1 == 146 && wCom == 1) // Data Vexen finish
                                newProg = 9;
                            //else if (wID1 == 146 && wCom == 1) // Data Vexen finish
                            //{
                            //    if(data.UsingProgressionHints)
                            //        UpdateProgressionPoints(wName, 9);
                            //    data.eventLog.Add(eventTuple);
                            //    return;
                            //}
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "PortRoyal":
                    switch (wRoom)
                    {
                        case 0:
                            if (wID3 == 1 && curProg == 0) // Rampart (PR1)
                                newProg = 1;
                            break;
                        case 10:
                            if (wID3 == 10 && curProg == 0) // Treasure Heap (PR2)
                                newProg = 1;
                            if (wID1 == 60 && wCom == 1) // Barbossa finish
                                newProg = 6;
                            break;
                        case 2:
                            if (wID1 == 55 && wCom == 1) // Town finish
                                newProg = 2;
                            break;
                        case 9:
                            if (wID1 == 59 && wCom == 1) // 1min pirates finish
                                newProg = 3;
                            break;
                        case 7:
                            if (wID1 == 58 && wCom == 1) // Medalion fight finish
                                newProg = 4;
                            break;
                        case 3:
                            if (wID1 == 56 && wCom == 1) // barrels finish
                                newProg = 5;
                            break;
                        case 18:
                            if (wID1 == 85 && wCom == 1) // Grim Reaper 1 finish
                                newProg = 7;
                            break;
                        case 14:
                            if (wID1 == 62 && wCom == 1) // Gambler finish
                                newProg = 8;
                            break;
                        case 1:
                            if (wID1 == 54 && wCom == 1) // Grim Reaper 2 finish
                                newProg = 9;
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "SpaceParanoids":
                    switch (wRoom)
                    {
                        case 1:
                            if ((wID3 == 1 || wID3 == 10) && curProg == 0) // Canyon (SP1)
                                newProg = 1;
                            break;
                        case 3:
                            if (wID1 == 54 && wCom == 1) // Screens finish
                                newProg = 2;
                            break;
                        case 4:
                            if (wID1 == 55 && wCom == 1) // Hostile Program finish
                                newProg = 3;
                            break;
                        case 7:
                            if (wID1 == 57 && wCom == 1) // Solar Sailer finish
                                newProg = 4;
                            break;
                        case 9:
                            if (wID1 == 59 && wCom == 1) // MCP finish
                                newProg = 5;
                            break;
                        case 33:
                            if (wID1 == 143 && wCom == 1) // AS Larxene finish
                                newProg = 6;
                            if (wID1 == 148 && wCom == 1) // Data Larxene finish
                                newProg = 7;
                            //else if (wID1 == 148 && wCom == 1) // Data Larxene finish
                            //{
                            //    if (data.UsingProgressionHints)
                            //        UpdateProgressionPoints(wName, 7);
                            //    data.eventLog.Add(eventTuple);
                            //    return;
                            //}
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "TWTNW":
                    switch (wRoom)
                    {
                        case 1:
                            if (wID3 == 1) // Alley to Between
                                newProg = 1;
                            break;
                        case 21:
                            if (wID1 == 65 && wCom == 1) // Roxas finish
                                newProg = 2;
                            else if (wID1 == 99 && wCom == 1) // Data Roxas finish
                            {
                                SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["SimulatedTwilightTown"][8]);
                                data.WorldsData["SimulatedTwilightTown"].progress = 8;
                                data.WorldsData["SimulatedTwilightTown"].progression.ToolTip = data.ProgressKeys["SimulatedTwilightTownDesc"][8];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("SimulatedTwilightTown", 8);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 10:
                            if (wID1 == 57 && wCom == 1) // Xigbar finish
                                newProg = 3;
                            else if (wID1 == 100 && wCom == 1) // Data Xigbar finish
                            {
                                LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["LandofDragons"][9]);
                                data.WorldsData["LandofDragons"].progress = 9;
                                data.WorldsData["LandofDragons"].progression.ToolTip = data.ProgressKeys["LandofDragonsDesc"][9];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("LandofDragons", 9);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 14:
                            if (wID1 == 58 && wCom == 1) // Luxord finish
                                newProg = 4;
                            else if (wID1 == 101 && wCom == 1) // Data Luxord finish
                            {
                                PortRoyalProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["PortRoyal"][10]);
                                data.WorldsData["PortRoyal"].progress = 10;
                                data.WorldsData["PortRoyal"].progression.ToolTip = data.ProgressKeys["PortRoyalDesc"][10];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("PortRoyal", 10);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 15:
                            if (wID1 == 56 && wCom == 1) // Saix finish
                                newProg = 5;
                            else if (wID1 == 102 && wCom == 1) // Data Saix finish
                            {
                                PrideLandsProgression.SetResourceReference(ContentProperty, Prog + data.ProgressKeys["PrideLands"][7]);
                                data.WorldsData["PrideLands"].progress = 7;
                                data.WorldsData["PrideLands"].progression.ToolTip = data.ProgressKeys["PrideLandsDesc"][7];
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPoints("PrideLands", 7);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        case 19:
                            if (wID1 == 59 && wCom == 1) // Xemnas 1 finish
                                newProg = 6;
                            break;
                        case 20:
                            if (wID1 == 98 && wCom == 1) // Data Xemnas finish
                            {
                                newProg = 7;
                            }
                            else if (wID1 == 74 && wCom == 1 && data.revealFinalXemnas) // Regular Final Xemnas finish
                            {
                                if (data.UsingProgressionHints)
                                    UpdateProgressionPointsTWTNW(wName);
                                data.eventLog.Add(eventTuple);
                                return;
                            }
                            break;
                        default:
                            updateProgression = false;
                            break;
                    }
                    break;
                case "GoA":
                    if (wRoom == 32)
                    {
                        if (HashGrid.Visibility == Visibility.Visible)
                        {
                            HashGrid.Visibility = Visibility.Collapsed;
                        }
                    }
                    return;
                default: //return if any other world
                    return;
            }

            //progression wasn't updated
            if (newProg == 99 || updateProgression == false)
                return;

            //progression points
            if (updateProgressionPoints == true && data.UsingProgressionHints)
                UpdateProgressionPoints(wName, newProg);

            //made it this far, now just set the progression icon based on newProg
            progressionM.SetResourceReference(ContentProperty, Prog + data.ProgressKeys[wName][newProg]);
            data.WorldsData[wName].progress = newProg;
            data.WorldsData[wName].progression.ToolTip = data.ProgressKeys[wName + "Desc"][newProg];

            //log event
            data.eventLog.Add(eventTuple);
        }

        // Sometimes level rewards and levels dont update on the same tick
        // Previous tick checks are placed on the current tick with the info of both ticks
        // This way level checks don't get misplaced 
        //Note: apparently the above is completely untrue, but its's not like it currently breaks anything so...
        private void DetermineItemLocations()
        {
            if (previousChecks.Count == 0)
                return;

            // Get rewards between previous level and current level
            List<string> levelRewards = rewards.GetLevelRewards(stats.Weapon)
                .Where(reward => reward.Item1 > stats.previousLevels[0] && reward.Item1 <= stats.Level)
                .Select(reward => reward.Item2).ToList();
            // Get drive rewards between previous level and current level
            List<string> driveRewards = rewards.valorChecks
                .Where(reward => reward.Item1 > valor.previousLevels[0] && reward.Item1 <= valor.Level)
                .Select(reward => reward.Item2).ToList();
            driveRewards.AddRange(rewards.wisdomChecks
                .Where(reward => reward.Item1 > wisdom.previousLevels[0] && reward.Item1 <= wisdom.Level)
                .Select(reward => reward.Item2));
            driveRewards.AddRange(rewards.limitChecks
                .Where(reward => reward.Item1 > limit.previousLevels[0] && reward.Item1 <= limit.Level)
                .Select(reward => reward.Item2));
            driveRewards.AddRange(rewards.masterChecks
                .Where(reward => reward.Item1 > master.previousLevels[0] && reward.Item1 <= master.Level)
                .Select(reward => reward.Item2));
            driveRewards.AddRange(rewards.finalChecks
                .Where(reward => reward.Item1 > final.previousLevels[0] && reward.Item1 <= final.Level)
                .Select(reward => reward.Item2));

            if (stats.Level > stats.previousLevels[0] && App.logger != null)
                App.logger.Record("Levels " + stats.previousLevels[0].ToString() + " to " + stats.Level.ToString());
            if (valor.Level > valor.previousLevels[0] && App.logger != null)
                App.logger.Record("Valor Levels " + valor.previousLevels[0].ToString() + " to " + valor.Level.ToString());
            if (wisdom.Level > wisdom.previousLevels[0] && App.logger != null)
                App.logger.Record("Wisdom Levels " + wisdom.previousLevels[0].ToString() + " to " + wisdom.Level.ToString());
            if (limit.Level > limit.previousLevels[0] && App.logger != null)
                App.logger.Record("Limit Levels " + limit.previousLevels[0].ToString() + " to " + limit.Level.ToString());
            if (master.Level > master.previousLevels[0] && App.logger != null)
                App.logger.Record("Master Levels " + master.previousLevels[0].ToString() + " to " + master.Level.ToString());
            if (final.Level > final.previousLevels[0] && App.logger != null)
                App.logger.Record("Final Levels " + final.previousLevels[0].ToString() + " to " + final.Level.ToString());
            foreach (string str in levelRewards)
            {
                if (App.logger != null)
                    App.logger.Record("Level reward " + str);
            }
            foreach (string str in driveRewards)
            {
                if (App.logger != null)
                    App.logger.Record("Drive reward " + str);
            }

            foreach (ImportantCheck check in previousChecks)
            {
                string count = "";
                // remove magic and torn page count for comparison with item codes and readd to track specific ui copies
                if (check.GetType() == typeof(Magic) || check.GetType() == typeof(TornPage))
                {
                    count = check.Name.Substring(check.Name.Length - 1);
                    check.Name = check.Name.Substring(0, check.Name.Length - 1);
                }

                if (levelRewards.Exists(x => x == check.Name))
                {
                    // add check to levels
                    TrackItem(check.Name + count, SorasHeartGrid);
                    levelRewards.Remove(check.Name);
                }
                else if (driveRewards.Exists(x => x == check.Name))
                {
                    // add check to drives
                    TrackItem(check.Name + count, DriveFormsGrid);
                    driveRewards.Remove(check.Name);
                }
                else
                {
                    //check if user is currently in shop or puzzle and track item to Creations if so
                    if (CheckSynthPuzzle())
                    {
                        TrackItem(check.Name + count, data.WorldsData["PuzzSynth"].worldGrid);
                    }
                    else
                    {
                        if (world.previousworldName != null && data.WorldsData.ContainsKey(world.previousworldName))
                        {
                            // add check to current world
                            TrackItem(check.Name + count, data.WorldsData[world.previousworldName].worldGrid);
                        }
                    }
                }
            }
        }

        private void UpdateCollectedItems()
        {
            foreach (ImportantCheck check in importantChecks)
            {
                // handle these separately due to the way they are stored in memory
                if (check.GetType() == typeof(Magic) || check.GetType() == typeof(TornPage))
                    continue;

                if (check.Obtained && collectedChecks.Contains(check) == false)
                {
                    // skip auto tracking final if it was forced and valor
                    if (check.Name == "Valor" && valor.genieFix == true && !data.altFinalTracking)
                    {
                        valor.Obtained = false;
                    }
                    else if (check.Name == "Final" && !data.altFinalTracking)
                    {
                        // if forced Final, start tracking the Final Form check
                        if (!data.forcedFinal && stats.form == 5)
                        {
                            data.forcedFinal = true;
                            checkEveryCheck.TrackCheck(0x001D);
                        }
                        // if not forced Final, track Final Form check like normal
                        // else if Final was forced, check the tracked Final Form check
                        else if (!data.forcedFinal || checkEveryCheck.UpdateTargetMemory())
                        {
                            collectedChecks.Add(check);
                            newChecks.Add(check);
                        }
                    }
                    else
                    {
                        collectedChecks.Add(check);
                        newChecks.Add(check);
                    }
                }
            }
            TrackQuantities();
        }

        private void GetBoss(World world, bool usingSave, Tuple<string, int, int, int, int, int> saveTuple)
        {
            //temp values
            string boss = "None";
            string wName;
            int wRoom;
            int wID1;
            int wID2;
            int wID3;
            int wCup;
            if (!usingSave)
            {
                wName = world.worldName;
                wRoom = world.roomNumber;
                wID1 = world.eventID1;
                wID2 = world.eventID2;
                wID3 = world.eventID3;
                wCup = world.cupRound;
            }
            else
            {
                wName = saveTuple.Item1;
                wRoom = saveTuple.Item2;
                wID1 = saveTuple.Item3;
                wID2 = saveTuple.Item4;
                wID3 = saveTuple.Item5;
                wCup = saveTuple.Item6;
            }

            //stops awarding points for a single boss each tick
            if (!usingSave)
            {
                if (world.eventComplete == 1 && eventInProgress)
                    return;
                else
                    eventInProgress = false;
            }

            //eventlog check
            var eventTuple = new Tuple<string, int, int, int, int, int>(wName, wRoom, wID1, wID2, wID3, wCup);
            if (data.bossEventLog.Contains(eventTuple))
                return;


            //boss beaten events (taken mostly from progression code)
            switch (wName)
            {
                case "SimulatedTwilightTown":
                    switch (wRoom) //check based on room number now, then based on events in each room
                    {
                        case 34:
                            if (wID1 == 157) // Twilight Thorn finish
                                boss = "Twilight Thorn";
                            break;
                        case 3:
                            if (wID1 == 180) // Seifer Battle (Day 4)
                                boss = "Seifer";
                            break;
                        case 4:
                            //Tutorial Seifer shouldn't give points
                            //if (wID1 == 77) // Tutorial 4 - Fighting
                            //    boss = "Seifer (1)";
                            if (wID1 == 78) // Seifer I Battle
                                boss = "Seifer (2)";
                            break;
                        case 5:
                            if (wID1 == 84) // Hayner Struggle
                                boss = "Hayner";
                            if (wID1 == 85) // Vivi Struggle
                                boss = "Vivi";
                            if (wID1 == 87) // Axel 1 Finish
                                boss = "Axel I";
                            if (wID1 == 88) // Setzer Struggle
                                boss = "Setzer";
                            break;
                        case 20:
                            if (wID1 == 137) // Axel 2 finish
                                boss = "Axel II";
                            break;
                        default:
                            break;
                    }
                    break;
                case "TwilightTown":
                    switch (wRoom)
                    {
                        case 20:
                            if (wID1 == 213) // Data Axel finish
                                boss = "Axel (Data)";
                            break;
                        case 4:
                            if (wID1 == 181) // Seifer II Battle
                                boss = "Seifer (3)";
                            if (wID1 == 182) // Hayner Battle (Struggle Competition)
                                boss = "Hayner (SR)";
                            if (wID1 == 183) // Setzer Battle (Struggle Competition)
                                boss = "Setzer (SR)";
                            if (wID1 == 184) // Seifer Battle (Struggle Competition)
                                boss = "Seifer (4)";
                            break;
                        default:
                            break;
                    }
                    break;
                case "HollowBastion":
                    switch (wRoom)
                    {
                        case 4:
                            if (wID1 == 55) // HB Demyx finish
                                boss = "Demyx";
                            else if (wID1 == 114) // Data Demyx finish
                                boss = "Demyx (Data)";
                            break;
                        case 1:
                            if (wID1 == 75) // Sephiroth finish
                                boss = "Sephiroth";
                            break;
                        default:
                            break;
                    }
                    break;
                case "BeastsCastle":
                    switch (wRoom)
                    {
                        case 11:
                            if (wID1 == 72) // Thresholder finish
                                boss = "Thresholder";
                            break;
                        case 3:
                            if (wID1 == 69) // Beast finish
                                boss = "The Beast";
                            break;
                        case 5:
                            if (wID1 == 78) // Shadow Stalker
                                boss = "Shadow Stalker";
                            if (wID1 == 79) // Dark Thorn finish
                                boss = "Dark Thorn";
                            break;
                        case 15:
                            if (wID1 == 82) // Xaldin finish
                                boss = "Xaldin";
                            else if (wID1 == 97) // Data Xaldin finish
                                boss = "Xaldin (Data)";
                            break;
                        default:
                            break;
                    }
                    break;
                case "OlympusColiseum":
                    switch (wRoom)
                    {
                        case 7:
                            if (wID1 == 114) // Cerberus finish
                                boss = "Cerberus";
                            break;
                        case 8:
                            if (wID1 == 116) // OC Pete finish
                                boss = "Pete OC II";
                            break;
                        case 18:
                            if (wID1 == 171) // Hydra finish
                                boss = "Hydra";
                            break;
                        case 19:
                            if (wID1 == 202) // Hades finish
                                boss = "Hades II (1)";
                            break;
                        case 34:
                            if (wID1 == 151) // Zexion finish
                                boss = "Zexion";
                            else if (wID1 == 152) // Data Zexion finish
                                boss = "Zexion (Data)";
                            break;
                        case 9: //Cups
                            if (wID1 == 189 && wCup == 10)
                                boss = "FF Team 1"; //Leon & Yuffie
                            if (wID1 == 190 && wCup == 10)
                                boss = "Cerberus (Cups)";
                            if (wID1 == 191 && wCup == 10)
                                boss = "Hercules";
                            if (wID1 == 192 && wCup == 10)
                                boss = "Hades Cups";
                            //paradox cups
                            if (wID1 == 193 && wCup == 10)
                                boss = "FF Team 2"; //Leon (3) & Yuffie (3)
                            if (wID1 == 194 && wCup == 10)
                                boss = "Cerberus (Cups)";
                            if (wID1 == 195 && wCup == 10)
                                boss = "Hercules";
                            //hades paradox
                            if (wID1 == 196 && wCup == 5)
                                boss = "Volcano Lord (Cups)";
                            if (wID1 == 196 && wCup == 10)
                                boss = "FF Team 3"; // Yuffie (1) & Tifa
                            if (wID1 == 196 && wCup == 15)
                                boss = "Blizzard Lord (Cups)";
                            if (wID1 == 196 && wCup == 20)
                                boss = "Pete Cups";
                            if (wID1 == 196 && wCup == 25)
                                boss = "FF Team 4"; // Cloud & Tifa (1)
                            if (wID1 == 196 && wCup == 30)
                                boss = "Hades Cups";
                            if (wID1 == 196 && wCup == 40)
                                boss = "FF Team 5"; // Leon (1) & Cloud (1)
                            if (wID1 == 196 && wCup == 48)
                                boss = "Cerberus (Cups)";
                            if (wID1 == 196 && wCup == 49)
                                boss = "FF Team 6"; // Leon (2), Cloud (2), Yuffie (2), & Tifa (2)
                            if (wID1 == 196 && wCup == 50)
                                boss = "Hades II";
                            break;
                        default:
                            break;
                    }
                    break;
                case "Agrabah":
                    switch (wRoom)
                    {
                        case 3:
                            if (wID1 == 59) // Lords finish
                                boss = "Twin Lords";
                            break;
                        case 5:
                            if (wID1 == 62) // Genie Jafar finish
                                boss = "Jafar";
                            break;
                        case 33:
                            if (wID1 == 142) // Lexaeus finish
                                boss = "Lexaeus";
                            else if (wID1 == 147) // Data Lexaeus finish
                                boss = "Lexaeus (Data)";
                            break;
                        default:
                            break;
                    }
                    break;
                case "LandofDragons":
                    switch (wRoom)
                    {
                        case 9:
                            if (wID1 == 75) // Shan Yu finish
                                boss = "Shan-Yu";
                            break;
                        case 7:
                            if (wID1 == 76) // Riku
                                boss = "Riku";
                            break;
                        case 8:
                            if (wID1 == 79) // Storm Rider finish
                                boss = "Storm Rider";
                            break;
                        default:
                            break;
                    }
                    break;
                case "PrideLands":
                    switch (wRoom)
                    {
                        case 14:
                            if (wID1 == 55) // Scar finish
                                boss = "Scar";
                            break;
                        case 15:
                            if (wID1 == 59) // Groundshaker finish
                                boss = "Groundshaker";
                            break;
                        default:
                            break;
                    }
                    break;
                case "DisneyCastle":
                    switch (wRoom)
                    {
                        case 1:
                            if (wID1 == 58) // Old Pete finish
                                boss = "Past Pete";
                            break;
                        case 2:
                            if (wID1 == 52) // Boat Pete finish
                                boss = "Boat Pete";
                            break;
                        case 3:
                            if (wID1 == 53) // DC Pete finish
                                boss = "Pete TR";
                            break;
                        case 38:
                            if (wID1 == 145) // Marluxia finish
                                boss = "Marluxia";
                            else if (wID1 == 150) // Data Marluxia finish
                                boss = "Marluxia (Data)";
                            break;
                        case 7:
                            if (wID1 == 67) // Lingering Will finish
                                boss = "Terra";
                            break;
                        default:
                            break;
                    }
                    break;
                case "HalloweenTown":
                    switch (wRoom)
                    {
                        case 3:
                            if (wID1 == 52) // Prison Keeper finish
                                boss = "Prison Keeper";
                            break;
                        case 9:
                            if (wID1 == 55) // Oogie Boogie finish
                                boss = "Oogie Boogie";
                            break;
                        case 7:
                            if (wID1 == 64) // Experiment finish
                                boss = "The Experiment";
                            break;
                        case 32:
                            if (wID1 == 115) // Vexen finish
                                boss = "Vexen";
                            if (wID1 == 146) // Data Vexen finish
                                boss = "Vexen (Data)";
                            break;
                        default:
                            break;
                    }
                    break;
                case "PortRoyal":
                    switch (wRoom)
                    {
                        case 10:
                            if (wID1 == 60) // Barbossa finish
                                boss = "Barbossa";
                            break;
                        case 18:
                            if (wID1 == 85) // Grim Reaper 1 finish
                                boss = "Grim Reaper I";
                            break;
                        case 1:
                            if (wID1 == 54) // Grim Reaper 2 finish
                                boss = "Grim Reaper II";
                            break;
                        default:
                            break;
                    }
                    break;
                case "SpaceParanoids":
                    switch (wRoom)
                    {
                        case 4:
                            if (wID1 == 55) // Hostile Program finish
                                boss = "Hostile Program";
                            break;
                        case 9:
                            if (wID1 == 58) // Sark finish
                                boss = "Sark";
                            else if (wID1 == 59) // MCP finish
                                boss = "MCP";
                            break;
                        case 33:
                            if (wID1 == 143) // Larxene finish
                                boss = "Larxene";
                            else if (wID1 == 148) // Data Larxene finish
                                boss = "Larxene (Data)";
                            break;
                        default:
                            break;
                    }
                    break;
                case "TWTNW":
                    switch (wRoom)
                    {
                        case 21:
                            if (wID1 == 65) // Roxas finish
                                boss = "Roxas";
                            else if (wID1 == 99) // Data Roxas finish
                                boss = "Roxas (Data)";
                            break;
                        case 10:
                            if (wID1 == 57) // Xigbar finish
                                boss = "Xigbar";
                            else if (wID1 == 100) // Data Xigbar finish
                                boss = "Xigbar (Data)";
                            break;
                        case 14:
                            if (wID1 == 58) // Luxord finish
                                boss = "Luxord";
                            else if (wID1 == 101) // Data Luxord finish
                                boss = "Luxord (Data)";
                            break;
                        case 15:
                            if (wID1 == 56) // Saix finish
                                boss = "Saix";
                            else if (wID1 == 102) // Data Saix finish
                                boss = "Saix (Data)";
                            break;
                        case 19:
                            if (wID1 == 59) // Xemnas 1 finish
                                boss = "Xemnas";
                            else if (wID1 == 97) // Data Xemnas I finish
                                boss = "Xemnas (Data)";
                            break;
                        case 20:
                            if (wID1 == 74) // Final Xemnas finish
                                boss = "Final Xemnas";
                            else if (wID1 == 98) // Data Final Xemnas finish
                                boss = "Final Xemnas (Data)";
                            break;
                        case 23:
                            if (wID1 == 73) // Armor Xemnas II
                                boss = "Armor Xemnas II";
                            break;
                        case 24:
                            if (wID1 == 71) // Armor Xemnas I
                                boss = "Armor Xemnas I";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            //return if bo boss beaten found


            if (!usingSave)
            {
                //if the boss was found and beaten then set flag
                //we do this to stop things happening every frame
                if (world.eventComplete == 1)
                    eventInProgress = true;
                else
                    return;
            }

            if (boss == "None")
                return;
                
            App.logger?.Record("Beaten Boss: " + boss);

            //get points for boss kills
            GetBossPoints(boss);

            //add to log
            data.bossEventLog.Add(eventTuple);
        }

        private void GetBossPoints(string boss)
        {
            int points;
            string bossType;
            string replacementType;

            if (boss == "Twin Lords")
            {
                if (data.BossRandoFound)
                {
                    //BlizzardLord
                    replacementType = Codes.FindBossType(data.BossList["Blizzard Lord"]);
                    if (replacementType == "Unknown")
                    {
                        Console.WriteLine("Unknown Replacement Boss: " + data.BossList["Blizzard Lord"] + ". Using default points.");

                        if (App.logger != null)
                            App.logger.Record("Unknown Replacement Boss: " + data.BossList["Blizzard Lord"] + ". Using default points.");

                        replacementType = "boss_other";
                    }
                    else
                    {
                        if (App.logger != null)
                            App.logger.Record("Blizzard Lord Replacement: " + data.BossList["Blizzard Lord"]);
                    }

                    points = data.PointsDatanew[replacementType];

                    //Volcano Lord
                    replacementType = Codes.FindBossType(data.BossList["Volcano Lord"]);
                    if (replacementType == "Unknown")
                    {
                        Console.WriteLine("Unknown Replacement Boss: " + data.BossList["Volcano Lord"] + ". Using default points.");

                        if (App.logger != null)
                            App.logger.Record("Unknown Replacement Boss: " + data.BossList["Volcano Lord"] + ". Using default points.");

                        replacementType = "boss_other";
                    }
                    else
                    {
                        if (App.logger != null)
                            App.logger.Record("Volcano Lord Replacement: " + data.BossList["Volcano Lord"]);
                    }

                    points += data.PointsDatanew[replacementType];

                    //bonus points here should be sum of both boss types / 2
                    if (points > 1)
                        points += points / 2;
                }
                else
                {
                    points = data.PointsDatanew["boss_other"] * 2;
                }
            }
            else if (boss.StartsWith("FF Team"))
            {
                if (data.BossRandoFound)
                {
                    string[] test = { "Unknown", "Unknown", "Unknown", "Unknown" };

                    if (boss == "FF Team 6")
                    {
                        test[0] = "Leon (2)";
                        test[1] = "Cloud (2)";
                        test[2] = "Yuffie (2)";
                        test[3] = "Tifa (2)";

                        replacementType = Codes.FindBossType(data.BossList[test[0]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[0]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[0]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[0] + " Replacement: " + data.BossList[test[0]]);

                        points = data.PointsDatanew[replacementType];

                        replacementType = Codes.FindBossType(data.BossList[test[1]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[1]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[1]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[1] + " Replacement: " + data.BossList[test[1]]);

                        points += data.PointsDatanew[replacementType];

                        replacementType = Codes.FindBossType(data.BossList[test[2]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[2]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[2]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[2] + " Replacement: " + data.BossList[test[2]]);

                        points += data.PointsDatanew[replacementType];

                        replacementType = Codes.FindBossType(data.BossList[test[3]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[3]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[3]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[3] + " Replacement: " + data.BossList[test[3]]);

                        points += data.PointsDatanew[replacementType];

                        //bonus points here should be sum of both boss types / 2
                        if (points > 1)
                            points += points / 2;
                    }
                    else
                    {
                        if (boss == "FF Team 1")
                        {
                            test[0] = "Leon";
                            test[1] = "Yuffie";
                        }
                        if (boss == "FF Team 2")
                        {
                            test[0] = "Leon (3)";
                            test[1] = "Yuffie (3)";
                        }
                        if (boss == "FF Team 3")
                        {
                            test[0] = "Yuffie (1)";
                            test[1] = "Tifa";
                        }
                        if (boss == "FF Team 4")
                        {
                            test[0] = "Cloud";
                            test[1] = "Tifa (1)";
                        }
                        if (boss == "FF Team 5")
                        {
                            test[0] = "Leon (1)";
                            test[1] = "Cloud (1)";
                        }

                        replacementType = Codes.FindBossType(data.BossList[test[0]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[0]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[0]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[0] + " Replacement: " + data.BossList[test[0]]);

                        points = data.PointsDatanew[replacementType];

                        replacementType = Codes.FindBossType(data.BossList[test[1]]);
                        if (replacementType == "Unknown")
                        {
                            //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[test[1]] + ". Using default points.");
                            App.logger?.Record("Unknown Replacement Boss: " + data.BossList[test[1]] + ". Using default points.");
                            replacementType = "boss_other";
                        }
                        else App.logger?.Record(test[1] + " Replacement: " + data.BossList[test[1]]);

                        points += data.PointsDatanew[replacementType];

                        //bonus points here should be sum of both boss types / 2
                        if (points > 1)
                            points += points / 2;
                    }
                }
                else
                {
                    if (boss == "FF Team 6")
                    {
                        points = data.PointsDatanew["boss_other"] * 4;
                    }
                    else
                    {
                        points = data.PointsDatanew["boss_other"] * 2;
                    }
                }
            }
            else
            {
                bossType = Codes.FindBossType(boss);
                if (bossType == "Unknown")
                {
                    Console.WriteLine("Unknown Boss: " + boss + ". Using default points.");

                    if (App.logger != null)
                        App.logger.Record("Unknown Boss: " + boss + ". Using default points.");

                    bossType = "boss_other";
                }

                if (data.BossRandoFound && data.BossList.ContainsKey(boss))
                {
                    replacementType = Codes.FindBossType(data.BossList[boss]);

                    if (replacementType == "Unknown")
                    {
                        //Console.WriteLine("Unknown Replacement Boss: " + data.BossList[boss] + ". Using default points.");
                        App.logger?.Record("Unknown Replacement Boss: " + data.BossList[boss] + ". Using default points.");

                        replacementType = "boss_other";
                    }
                    else
                    {
                        App.logger?.Record(boss + " Replacement: " + data.BossList[boss]);
                    }

                    points = data.PointsDatanew[replacementType];

                    //add extra points for bosses in special arenas
                    int bonuspoints = 0;
                    switch (bossType)
                    {
                        case "boss_as":
                        case "boss_datas":
                        case "boss_sephi":
                        case "boss_terra":
                        //case "boss_final":
                            bonuspoints += data.PointsDatanew[bossType];
                            break;
                        case "boss_other":
                            if (boss == "Final Xemnas")
                                bonuspoints += data.PointsDatanew["boss_final"];
                            break;
                    }

                    points += bonuspoints;
                }
                else
                {
                    points = data.PointsDatanew[bossType];

                    //logging
                    if(data.BossRandoFound)
                    {
                        App.logger?.Record("No replacement found? Boss: " + boss);
                    }
                }
            }

            UpdatePointScore(points);
        }

        private void HighlightWorld(World world)
        {
            if (WorldHighlightOption.IsChecked == false)
                return;

            if (world.previousworldName != null && data.WorldsData.ContainsKey(world.previousworldName))
            {
                foreach (Rectangle Box in data.WorldsData[world.previousworldName].top.Children.OfType<Rectangle>().Where(Box => Box.Name.EndsWith("SelWG")))
                {
                    Box.Visibility = Visibility.Collapsed;
                }
            }

            if (data.WorldsData.ContainsKey(world.worldName))
            {
                foreach (Rectangle Box in data.WorldsData[world.worldName].top.Children.OfType<Rectangle>().Where(Box => Box.Name.EndsWith("SelWG")))
                {
                    Box.Visibility = Visibility.Visible;
                }
            }
        }

        ///
        /// Bindings & helpers
        ///

        private void SetBindings()
        {
            BindWeapon(SorasHeartWeapon, "Weapon", stats);

            //changes opacity for stat icons
            BindAbility(HighJump, "Obtained", highJump);
            BindAbility(QuickRun, "Obtained", quickRun);
            BindAbility(DodgeRoll, "Obtained", dodgeRoll);
            BindAbility(AerialDodge, "Obtained", aerialDodge);
            BindAbility(Glide, "Obtained", glide);
            
            BindForm(WisdomM, "Obtained", wisdom);
            BindForm(LimitM, "Obtained", limit);
            BindForm(MasterM, "Obtained", master);
            
            if (data.altFinalTracking)
            {
                BindForm(ValorM, "Obtained", valorReal);
                BindForm(FinalM, "Obtained", finalReal);
            }
            else
            {
                BindForm(ValorM, "Obtained", valor);
                BindForm(FinalM, "Obtained", final);
            }
        }

        private void BindForm(ContentControl img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new ObtainedConverter();
            img.SetBinding(OpacityProperty, binding);
        }

        private void BindAbility(ContentControl img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new ObtainedConverter();
            img.SetBinding(OpacityProperty, binding);
        }

        private void BindWeapon(Image img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new WeaponConverter();
            img.SetBinding(Image.SourceProperty, binding);
        }

        private string BytesToHex(byte[] bytes)
        {
            if (Enumerable.SequenceEqual(bytes, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                return "Service not started. Waiting for PCSX2";
            }
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public string GetWorld()
        {
            return world.worldName;
        }

        //public void UpdateUsedPages()
        //{
        //
        //
        //    data.usedPages++;
        //}

        public int GetUsedPages(int save)
        {
            save = save - 0x3598;
            int used = 0;
            bool PigFlag = new BitArray(memory.ReadMemory(save + 0x1DB0, 1))[1];
            bool Page1Flag = new BitArray(memory.ReadMemory(save + 0x1DB1, 1))[1];
            bool Page2Flag = new BitArray(memory.ReadMemory(save + 0x1DB2, 1))[1];
            bool Page3Flag = new BitArray(memory.ReadMemory(save + 0x1DB3, 1))[1];
            bool Page4Flag = new BitArray(memory.ReadMemory(save + 0x1DB4, 1))[1];
            bool Page5Flag = new BitArray(memory.ReadMemory(save + 0x1DB5, 1))[0];

            if (PigFlag && Page5Flag)
            {
                data.usedPages = 5;
                return data.usedPages;
            }

            if (Page1Flag) used++;
            if (Page2Flag) used++;
            if (Page3Flag) used++;
            if (Page4Flag) used++;

            data.usedPages = used;

            return data.usedPages;
        }

        public void UpdateFormProgression()
        {
            int found = 0;
            string drives = "";
            bool OldToggled = Properties.Settings.Default.OldProg;
            bool CustomToggled = Properties.Settings.Default.CustomIcons;
            string Prog = "Min-"; //Default
            if (OldToggled)
                Prog = "Old-";
            if (CustomProgFound && CustomToggled)
                Prog = "Cus-";

            if (ValorM.Opacity == 1)
                found++;
            if (WisdomM.Opacity == 1)
                found++;
            if (LimitM.Opacity == 1)
                found++;
            if (MasterM.Opacity == 1)
                found++;
            if (FinalM.Opacity == 1)
                found++;


            switch (found)
            {
                case 1:
                    drives = "Drive3";
                    break;
                case 2:
                    drives = "Drive4";
                    break;
                case 3:
                    drives = "Drive5";
                    break;
                case 4:
                    drives = "Drive6";
                    break;
                case 5:
                    drives = "Drive7";
                    break;
                default:
                    drives = "Drive2";
                    break;
            }

            DriveFormsCap.SetResourceReference(ContentProperty, Prog + drives);
        }

        private int ReadMemInt(int address)
        {
            address = address + ADDRESS_OFFSET;
            return BitConverter.ToInt32(memory.ReadMemory(address, 4), 0);
        }

        private string ReadMemString(int address, int length)
        {
            address = address + ADDRESS_OFFSET;
            string result = Encoding.Default.GetString(memory.ReadMemory(address, length), 0, length);
            return result.TrimEnd('\0');
        }

        private int ReadPcPointer(int address)
        {
            long origAddress = BitConverter.ToInt64(memory.ReadMemory(address, 8), 0);
            long baseAddress = memory.GetBaseAddress();
            long result = origAddress - baseAddress;
            return (int)result;
        }

        //progression hints - compare last saved progression point
        //must be checked this way cause of OnTimedEvent
        public void UpdateProgressionPoints(string worldName, int prog)
        {
            //if event is current, skip
            //if ((world.eventID1 == data.PrevEventID1 && world.eventID3 == data.PrevEventID3
            //    && world.worldName == data.PrevWorld && world.roomNumber == data.PrevRoomNum)
            //    || !data.UsingProgressionHints)
            //    return;

            AddProgressionPoints(GetProgressionPointsReward(worldName, prog));

            //data.PrevEventID1 = world.eventID1;
            //data.PrevEventID3 = world.eventID3;
            //data.PrevWorld = world.worldName;
            //data.PrevRoomNum = world.roomNumber;
        }
        public void UpdateProgressionPointsTWTNW(string worldName)
        {
            //if event is current, skip
            //if ((world.eventID1 == data.PrevEventID1 && world.eventID3 == data.PrevEventID3
            //    && world.worldName == data.PrevWorld && world.roomNumber == data.PrevRoomNum)
            //    || !data.UsingProgressionHints)
            //    return;
            //Console.WriteLine("Defeated Final Xemnas");

            data.TWTNW_ProgressionValues.Add(200);
            AddProgressionPoints(GetProgressionPointsReward(worldName, data.TWTNW_ProgressionValues.Count));
            data.TWTNW_ProgressionValues.RemoveAt(data.TWTNW_ProgressionValues.Count - 1);

            data.TWTNW_ProgressionValues.Add(-200);
            AddProgressionPoints(GetProgressionPointsReward(worldName, data.TWTNW_ProgressionValues.Count));
            data.TWTNW_ProgressionValues.RemoveAt(data.TWTNW_ProgressionValues.Count - 1);

            //data.PrevEventID1 = world.eventID1;
            //data.PrevEventID3 = world.eventID3;
            //data.PrevWorld = world.worldName;
            //data.PrevRoomNum = world.roomNumber;
        }
    }
}