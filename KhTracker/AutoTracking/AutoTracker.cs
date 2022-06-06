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
//using System.IO;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MemoryReader memory, testMemory;

        private Int32 ADDRESS_OFFSET;
        private static DispatcherTimer aTimer, autoTimer;
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

        private Magic fire;
        private Magic blizzard;
        private Magic thunder;
        private Magic magnet;
        private Magic reflect;
        private Magic cure;

        //private Report rep1;
        //private Report rep2;
        //private Report rep3;
        //private Report rep4;
        //private Report rep5;
        //private Report rep6;
        //private Report rep7;
        //private Report rep8;
        //private Report rep9;
        //private Report rep10;
        //private Report rep11;
        //private Report rep12;
        //private Report rep13;
        //private Summon chickenLittle;
        //private Summon stitch;
        //private Summon genie;
        //private Summon peterPan;
        //private ImportantCheck promiseCharm;
        //private ImportantCheck peace;
        //private ImportantCheck nonexist;
        //private ImportantCheck connection;
        //private ImportantCheck auronWep;
        //private ImportantCheck mulanWep;
        //private ImportantCheck beastWep;
        //private ImportantCheck jackWep;
        //private ImportantCheck simbaWep;
        //private ImportantCheck sparrowWep;
        //private ImportantCheck aladdinWep;
        //private ImportantCheck tronWep;
        //private ImportantCheck poster;
        //private ImportantCheck iceCream;
        //private ImportantCheck picture;
        //private ImportantCheck hadescup;

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

        private bool forcedFinal;
        private CheckEveryCheck checkEveryCheck;

        public static bool pcsx2tracking = false;

        //Auto-Detect Control Stuff
        private int storedDetectedVersion = 0; //0 = nothing detected, 1 = PC, 2 = PCSX2
        private bool isWorking = false;
        private bool firstRun = true;
        private bool titleloaded = false;
        private bool onContinue = false;

        public void InitPCSX2Tracker(object sender, RoutedEventArgs e)
        {
            pcsx2tracking = true;
            InitAutoTracker(true);
        }

        public void InitPCTracker(object sender, RoutedEventArgs e)
        {
            pcsx2tracking = false;
            InitAutoTracker(false);
        }

        private void SetAutoDetectTimer()
        {
            //SetDetectionText();

            if (isWorking)
                return;

            if (aTimer != null)
                aTimer.Stop();

            //autoTimer = new DispatcherTimer();
            if (firstRun)
            {
                //Console.WriteLine("Started search");
                autoTimer = new DispatcherTimer();
                autoTimer.Tick += searchVersion;
                firstRun = false;
                autoTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            }
            autoTimer.Start();

            //Console.WriteLine("AutoDetect Started");
        }

        private bool alternateCheck = false; //true = PCSX2, false = PC
        private int alternateCheckInt = 1;

        public void searchVersion(object sender, EventArgs e)
        {
            if (!AutoDetectOption.IsChecked)
            {
                Console.WriteLine("disabling auto-detect");
                //SetDetectionText(); //change with icons later
                Connect.Visibility = Visibility.Hidden;
                autoTimer.Stop();
                return;
            }

            if (isWorking)
                return;

            Console.WriteLine("searchVersion called");

            if (CheckVersion(alternateCheck))
            {
                autoTimer.Stop();

                if (alternateCheck)
                {
                    Console.WriteLine("PCSX2 Found, starting Auto-Tracker");
                    //SetHintText("PCSX2 Detected - Tracking", 30000, ""); //change with icons later
                    Connect.Source = data.AD_PS2;

                }
                else
                {
                    Console.WriteLine("PC Found, starting Auto-Tracker");
                    //SetDetectionText("PC Detected - Connecting..."); //change with icons later
                    Connect.Source = data.AD_PC;
                }

                if (storedDetectedVersion != alternateCheckInt && storedDetectedVersion != 0)
                {
                    //Console.WriteLine("storedDetectedVerison = " + storedDetectedVersion + " || alternateCheck = " + alternateCheck);
                    OnReset(null, null);
                }
                storedDetectedVersion = alternateCheckInt;

                InitAutoTracker(alternateCheck);

                isWorking = true;

                return;
            }

            alternateCheck = !alternateCheck;
            alternateCheckInt = alternateCheckInt == 1 ? 2 : 1;

        }

        public bool CheckVersion(bool state)
        {
            if (isWorking)
                return true;

            int tries = 0;
            do
            {
                testMemory = new MemoryReader(state);
                if (tries < 20)
                {
                    tries++;
                }
                else
                {
                    testMemory = null;
                    Console.WriteLine("No game running");
                    return false;
                }
            } while (!testMemory.Hooked);

            return true;
        }

        public void SetWorking(bool state)
        {
            isWorking = state;
        }

        public void InitAutoTracker(bool PCSX2)
        {
            int tries = 0;
            do
            {
                memory = new MemoryReader(PCSX2);
                if (tries < 20)
                {
                    tries++;
                }
                else
                {
                    memory = null;
                    MessageBox.Show("Please start KH2 before loading the Auto Tracker.");
                    return;
                }
            } while (!memory.Hooked);

            // PC Address anchors
            int Now = 0x0714DB8;
            int Save = 0x09A70B0;
            int Sys3 = 0x2A59DF0;
            int Bt10 = 0x2A74880;
            int BtlEnd = 0x2A0D3E0;
            int Slot1 = 0x2A20C98;

            if (PCSX2 == false)
            {
                while (!titleloaded)
                {
                    titleloaded = CheckPCTitle();
                }

                if (titleloaded)
                {
                    Connect.Source = data.AD_PCred;
                    FinishSetupPC(PCSX2, Now, Save, Sys3, Bt10, BtlEnd, Slot1);
                }
            }
            else
            {
                try
                {
                    findAddressOffset();
                }
                catch (Win32Exception)
                {
                    memory = null;
                    MessageBox.Show("Unable to access PCSX2 try running KHTracker as admin");
                    isWorking = false;
                    SetAutoDetectTimer();
                    return;
                }
                catch
                {
                    memory = null;
                    MessageBox.Show("Error connecting to PCSX2");
                    isWorking = false;
                    SetAutoDetectTimer();
                    return;
                }

                // PCSX2 anchors 
                Now = 0x032BAE0;
                Save = 0x032BB30;
                Sys3 = 0x1CCB300;
                Bt10 = 0x1CE5D80;
                BtlEnd = 0x1D490C0;
                Slot1 = 0x1C6C750;

                Console.WriteLine("PCSX2 Detected - Tracking");
                Connect.Source = data.AD_PS2;
                FinishSetup(PCSX2, Now, Save, Sys3, Bt10, BtlEnd, Slot1);
            }
        }

        private void FinishSetup(bool PCSX2, Int32 Now, Int32 Save, Int32 Sys3, Int32 Bt10, Int32 BtlEnd, Int32 Slot1)
        {
            importantChecks = new List<ImportantCheck>();
            importantChecks.Add(highJump = new Ability(memory, Save + 0x25CE, ADDRESS_OFFSET, 93, "HighJump"));
            importantChecks.Add(quickRun = new Ability(memory, Save + 0x25D0, ADDRESS_OFFSET, 97, "QuickRun"));
            importantChecks.Add(dodgeRoll = new Ability(memory, Save + 0x25D2, ADDRESS_OFFSET, 563, "DodgeRoll"));
            importantChecks.Add(aerialDodge = new Ability(memory, Save + 0x25D4, ADDRESS_OFFSET, 101, "AerialDodge"));
            importantChecks.Add(glide = new Ability(memory, Save + 0x25D6, ADDRESS_OFFSET, 105, "Glide"));

            importantChecks.Add(secondChance = new Ability(memory, Save + 0x2544, ADDRESS_OFFSET, "SecondChance", Save));
            importantChecks.Add(onceMore = new Ability(memory, Save + 0x2544, ADDRESS_OFFSET, "OnceMore", Save));

            importantChecks.Add(valor = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 1, Save + 0x32F6, Save + 0x06B2, "Valor"));
            importantChecks.Add(wisdom = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 2, Save + 0x332E, "Wisdom"));
            importantChecks.Add(limit = new DriveForm(memory, Save + 0x36CA, ADDRESS_OFFSET, 3, Save + 0x3366, "Limit"));
            importantChecks.Add(master = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 6, Save + 0x339E, "Master"));
            importantChecks.Add(final = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 4, Save + 0x33D6, "Final"));
            importantChecks.Add(anti = new DriveForm(memory, Save + 0x36C0, ADDRESS_OFFSET, 5, Save + 0x340C, "Anti"));

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

            int count = pages != null ? pages.Quantity : 0;
            importantChecks.Add(pages = new TornPage(memory, Save + 0x3598, ADDRESS_OFFSET, "TornPage"));
            pages.Quantity = count;

            if (PCSX2)
                world = new World(memory, ADDRESS_OFFSET, Now, 0x00351EC8, Save + 0x1CFF);
            else
                world = new World(memory, ADDRESS_OFFSET, Now, BtlEnd + 0x820, Save + 0x1CFF);

            stats = new Stats(memory, ADDRESS_OFFSET, Save + 0x24FE, Slot1 + 0x188, Save + 0x3524, Save + 0x3700);
            rewards = new Rewards(memory, ADDRESS_OFFSET, Bt10);

            forcedFinal = false;
            checkEveryCheck = new CheckEveryCheck(memory, ADDRESS_OFFSET, Save, Sys3, Bt10, world, stats, rewards, valor, wisdom, limit, master, final);

            LevelIcon.Visibility = Visibility.Visible;
            Level.Visibility = Visibility.Visible;
            StrengthIcon.Visibility = Visibility.Visible;
            Strength.Visibility = Visibility.Visible;
            MagicIcon.Visibility = Visibility.Visible;
            Magic.Visibility = Visibility.Visible;
            DefenseIcon.Visibility = Visibility.Visible;
            Defense.Visibility = Visibility.Visible;
            Weapon.Visibility = Visibility.Visible;

            if (AutoDetectOption.IsChecked)
                Connect.Visibility = Visibility.Visible;

            broadcast.LevelIcon.Visibility = Visibility.Visible;
            broadcast.Level.Visibility = Visibility.Visible;
            broadcast.StrengthIcon.Visibility = Visibility.Visible;
            broadcast.Strength.Visibility = Visibility.Visible;
            broadcast.MagicIcon.Visibility = Visibility.Visible;
            broadcast.Magic.Visibility = Visibility.Visible;
            broadcast.DefenseIcon.Visibility = Visibility.Visible;
            broadcast.Defense.Visibility = Visibility.Visible;
            broadcast.Weapon.Visibility = Visibility.Visible;

            broadcast.ValorLevel.Visibility = Visibility.Visible;
            broadcast.WisdomLevel.Visibility = Visibility.Visible;
            broadcast.LimitLevel.Visibility = Visibility.Visible;
            broadcast.MasterLevel.Visibility = Visibility.Visible;
            broadcast.FinalLevel.Visibility = Visibility.Visible;

            if (BroadcastGrowthOption.IsChecked)
                broadcast.GrowthAbilityRow.Height = new GridLength(1, GridUnitType.Star);

            if (BroadcastStatsOption.IsChecked)
                broadcast.StatsRow.Height = new GridLength(1, GridUnitType.Star);

            if (FormsGrowthOption.IsChecked)
                FormRow.Height = new GridLength(0.65, GridUnitType.Star);

            //levelcheck visibility
            NextLevelDisplay();
            DeathCounterDisplay();
            SetBindings();
            SetTimer();
            OnTimedEvent(null, null);

            titleloaded = false;
        }

        private async void FinishSetupPC(bool PCSX2, Int32 Now, Int32 Save, Int32 Sys3, Int32 Bt10, Int32 BtlEnd, Int32 Slot1)
        {
            int Delay = 4000;

            if (!AutoDetectOption.IsChecked)
                Delay = 0;

            await Task.Delay(Delay);
            try
            {
                CheckPCOffset();
            }
            catch (Win32Exception)
            {
                memory = null;
                MessageBox.Show("Unable to access KH2FM try running KHTracker as admin");
                isWorking = false;
                SetAutoDetectTimer();
                return;
            }
            catch
            {
                memory = null;
                MessageBox.Show("Error connecting to KH2FM");
                isWorking = false;
                SetAutoDetectTimer();
                return;
            }

            Console.WriteLine("PC Detected - Tracking");
            Connect.Source = data.AD_PC;
            FinishSetup(PCSX2, Now, Save, Sys3, Bt10, BtlEnd, Slot1);
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

        private bool CheckPCTitle()
        {
            //checks if the title.2ld has been loaded into memeory
            Int32 testAddr = 0x29F09E4;
            string good = "6D656E75";
            string tester = BytesToHex(memory.ReadMemory(testAddr, 4));

            //fallback to check if world loaded isn't the title screen/RoD
            //needed if tracker was started after the title screen
            Int32 testAddrw = 0x0714DB8;
            string goodworld = "FFFF";
            string testerw = BytesToHex(memory.ReadMemory(testAddrw, 2));

            if (tester == good)
            {
                return true;
            }
            else if (testerw != goodworld)
            {
                return true;
            }
            else
                return false;
        }

        private void findAddressOffset()
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

        private void SetBindings()
        {
            BindNumberTens(Level_01, Level_10, "Level", stats);
            BindNumberFull(Strength_001, Strength_010, Strength_100, "Strength", stats);
            BindNumberFull(Magic_001, Magic_010, Magic_100, "Magic", stats);
            BindNumberFull(Defense_001, Defense_010, Defense_100, "Defense", stats);

            BindWeapon(Weapon, "Weapon", stats);
            BindWeapon(broadcast.Weapon, "Weapon", stats);

            BindNumberTens(broadcast.Level_01, broadcast.Level_10, "Level", stats);
            BindNumberFull(broadcast.Strength_001, broadcast.Strength_010, broadcast.Strength_100, "Strength", stats);
            BindNumberFull(broadcast.Magic_001, broadcast.Magic_010, broadcast.Magic_100, "Magic", stats);
            BindNumberFull(broadcast.Defense_001, broadcast.Defense_010, broadcast.Defense_100, "Defense", stats);

            BindLevel(broadcast.ValorLevel, "Level", valor);
            BindLevel(broadcast.WisdomLevel, "Level", wisdom);
            BindLevel(broadcast.LimitLevel, "Level", limit);
            BindLevel(broadcast.MasterLevel, "Level", master);
            BindLevel(broadcast.FinalLevel, "Level", final);

            BindAbility(broadcast.HighJump, "Obtained", highJump);
            BindAbility(broadcast.QuickRun, "Obtained", quickRun);
            BindAbility(broadcast.DodgeRoll, "Obtained", dodgeRoll);
            BindAbility(broadcast.AerialDodge, "Obtained", aerialDodge);
            BindAbility(broadcast.Glide, "Obtained", glide);

            BindAbilityLevel(broadcast.HighJumpLevel, "Level", highJump, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.QuickRunLevel, "Level", quickRun, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.DodgeRollLevel, "Level", dodgeRoll, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.AerialDodgeLevel, "Level", aerialDodge, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.GlideLevel, "Level", glide, new GrowthAbilityConverter());

            //track in main window
            BindAbility(HighJump, "Obtained", highJump);
            BindAbility(QuickRun, "Obtained", quickRun);
            BindAbility(DodgeRoll, "Obtained", dodgeRoll);
            BindAbility(AerialDodge, "Obtained", aerialDodge);
            BindAbility(Glide, "Obtained", glide);

            BindAbilityLevel(HighJumpLevel, "Level", highJump, new GrowthAbilityConverter());
            BindAbilityLevel(QuickRunLevel, "Level", quickRun, new GrowthAbilityConverter());
            BindAbilityLevel(DodgeRollLevel, "Level", dodgeRoll, new GrowthAbilityConverter());
            BindAbilityLevel(AerialDodgeLevel, "Level", aerialDodge, new GrowthAbilityConverter());
            BindAbilityLevel(GlideLevel, "Level", glide, new GrowthAbilityConverter());

            BindLevel(ValorLevel, "Level", valor);
            BindLevel(WisdomLevel, "Level", wisdom);
            BindLevel(LimitLevel, "Level", limit);
            BindLevel(MasterLevel, "Level", master);
            BindLevel(FinalLevel, "Level", final);

            BindForm(ValorM, "Obtained", valor);
            BindForm(WisdomM, "Obtained", wisdom);
            BindForm(LimitM, "Obtained", limit);
            BindForm(MasterM, "Obtained", master);
            BindForm(FinalM, "Obtained", final);
        }

        private void SetTimer()
        {
            if (aTimer != null)
                aTimer.Stop();

            aTimer = new DispatcherTimer();
            aTimer.Tick += OnTimedEvent;
            aTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            aTimer.Start();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            previousChecks.Clear();
            previousChecks.AddRange(newChecks);
            newChecks.Clear();

            try
            {
                stats.UpdateMemory();
                world.UpdateMemory();
                UpdateMagicAddresses();
                UpdateWorldProgress(world);
                UpdatePointScore(0);
                StatResize();
                DeathCheck(pcsx2tracking);

                //Console.WriteLine("room num = " + world.roomNumber);
                //Console.WriteLine("world num = " + world.worldNum);
                //Console.WriteLine("event id1 = " + world.eventID1);
                //Console.WriteLine("event id2 = " + world.eventID2);
                //Console.WriteLine("event id3 = " + world.eventID3);
                //string cntrl = BytesToHex(memory.ReadMemory(0x2A148E8, 1)); //sora controlable
                //Console.WriteLine(cntrl);

                importantChecks.ForEach(delegate (ImportantCheck importantCheck)
                {
                    importantCheck.UpdateMemory();
                });
            }
            catch
            {
                aTimer.Stop();
                //MessageBox.Show("KH2FM has exited. Stopping Auto Tracker.");
                Console.WriteLine("Connection Lost, Reconnecting..."); //change to icon
                Connect.Source = data.AD_Connect;
                isWorking = false;
                SetAutoDetectTimer();
                return;
            }

            UpdateCollectedItems();
            DetermineItemLocations();

            //next level check
            stats.SetNextLevelCheck(stats.Level);
            List<BitmapImage> LevelCheckNum = UpdateNumber(stats.LevelCheck, "G");
            if (stats.LevelCheck < 9)
            {
                LevelCheck_01.Source = null;
                LevelCheck_10.Source = LevelCheckNum[0];
                broadcast.LevelCheck_01.Source = null;
                broadcast.LevelCheck_10.Source = LevelCheckNum[0];
            }
            else
            {
                LevelCheck_01.Source = LevelCheckNum[0];
                LevelCheck_10.Source = LevelCheckNum[1];
                broadcast.LevelCheck_01.Source = LevelCheckNum[0];
                broadcast.LevelCheck_10.Source = LevelCheckNum[1];
            }
        }

        private void TrackItem(string itemName, WorldGrid world)
        {
            if (!GetItemPool.ContainsKey(itemName))
                return;

            Grid ItemRow = VisualTreeHelper.GetChild(ItemPool, GetItemPool[itemName]) as Grid;

            foreach (ContentControl item in ItemRow.Children)
            {
                if (item.Name == itemName && item.IsVisible)
                {
                    bool ReportType;
                    if (data.mode == Mode.DAHints)
                    {
                        ReportType = world.Handle_PointReport(item as Item, this, data);
                    }
                    else if (data.mode == Mode.PathHints)
                    {
                        ReportType = world.Handle_PathReport(item as Item, this, data);
                    }
                    else
                    {
                        ReportType = world.Handle_Report(item as Item, this, data);
                    }

                    if (ReportType)
                    {
                        world.Add_Item(item as Item, this);
                        if (App.logger != null)
                            App.logger.Record(item.Name + " tracked");
                    }
                    break;
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

        private void UpdateMagicAddresses()
        {
            if (LegacyOption.IsChecked && world.worldName == "SimulatedTwilightTown"  // (and not in Data Roxas fight)
                && !(world.roomNumber == 21 && (world.eventID1 == 99 || world.eventID3 == 113 || world.eventID1 == 114)))
            {
                fire.UseSTTAddress(true);
                blizzard.UseSTTAddress(true);
                thunder.UseSTTAddress(true);
                cure.UseSTTAddress(true);
                reflect.UseSTTAddress(true);
                magnet.UseSTTAddress(true);
            }
            else
            {
                fire.UseSTTAddress(false);
                blizzard.UseSTTAddress(false);
                thunder.UseSTTAddress(false);
                cure.UseSTTAddress(false);
                reflect.UseSTTAddress(false);
                magnet.UseSTTAddress(false);
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
                    if (check.Name == "Valor" && valor.genieFix == true)
                    {
                        valor.Obtained = false;
                    }
                    else if (check.Name == "Final")
                    {
                        // if forced Final, start tracking the Final Form check
                        if (!forcedFinal && stats.form == 5)
                        {
                            forcedFinal = true;
                            checkEveryCheck.TrackCheck(0x001D);
                        }
                        // if not forced Final, track Final Form check like normal
                        // else if Final was forced, check the tracked Final Form check
                        else if (!forcedFinal || checkEveryCheck.UpdateTargetMemory())
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

        // Sometimes level rewards and levels dont update on the same tick
        // Previous tick checks are placed on the current tick with the info of both ticks
        // This way level checks don't get misplaced 
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
                    if(CheckSynthPuzzle(pcsx2tracking))
                    {
                        TrackItem(check.Name + count, data.WorldsData["PuzzSynth"].worldGrid);
                    }
                    else
                    {
                        if (data.WorldsData.ContainsKey(world.previousworldName))
                        {
                            // add check to current world
                            TrackItem(check.Name + count, data.WorldsData[world.previousworldName].worldGrid);
                        }
                    }
                }
            }
        }

        void UpdateWorldProgress(World world)
        {
            bool OldToggled = Properties.Settings.Default.OldProg;
            bool CustomToggled = Properties.Settings.Default.CustomIcons;
            string Prog = "Min-"; //Default
            if (OldToggled)
                Prog = "Old-";
            if (CustomProgFound && CustomToggled)
                Prog = "Cus-";

            if (world.worldName == "SimulatedTwilightTown")
            {
                if (world.roomNumber == 1 && world.eventID3 == 56 && data.WorldsData[world.worldName].progress == 0) // Roxas' Room (Day 1)
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "STTChests");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "STTChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 34 && world.eventID1 == 157 && world.eventComplete == 1) // Twilight Thorn finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "TwilightThorn");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "TwilightThorn");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 88 && world.eventComplete == 1) // Setzer finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Struggle");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Struggle");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 1 && world.eventID3 == 55 && data.WorldsData[world.worldName].progress == 0) // Roxas' Room (Day 6)
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "STTChests");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "STTChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 21 && world.eventID3 == 1) // Mansion: Computer Room
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "ComputerRoom");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "ComputerRoom");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 137 && world.eventComplete == 1) // Axel finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Axel");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Axel");
                    data.WorldsData[world.worldName].progress = 5;
                }
            }
            else if (world.worldName == "TwilightTown")
            {
                if (world.roomNumber == 9 && world.eventID3 == 117 && data.WorldsData[world.worldName].progress == 0) // Roxas' Room (Day 1)
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "TTChests");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "TTChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                if (world.roomNumber == 27 && world.eventID3 == 4) // Yen Sid after new clothes
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "MysteriousTower");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "MysteriousTower");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 80 && world.eventComplete == 1) // Sandlot finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Sandlot");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Sandlot");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 41 && world.eventID1 == 186 && world.eventComplete == 1) // Mansion fight finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Mansion");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "Mansion");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 40 && world.eventID1 == 161 && world.eventComplete == 1) // Betwixt and Between finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "BetwixtandBetween");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "BetwixtandBetween");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 213 && world.eventComplete == 1) // Data Axel finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "DataAxel");
                    TwilightTownProgression.SetResourceReference(ContentProperty, Prog + "DataAxel");
                    data.WorldsData[world.worldName].progress = 5;
                }
            }
            else if (world.worldName == "HollowBastion")
            {
                if (world.roomNumber == 0 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Villain's Vale (HB1)
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBChests");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 8 && world.eventID1 == 52 && world.eventComplete == 1) // Bailey finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Bailey");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Bailey");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 10 && world.eventID3 == 2 && data.WorldsData[world.worldName].progress == 0) // Marketplace (HB2)
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBChests");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 5 && world.eventID3 == 20) // Ansem Study post Computer
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "AnsemStudy");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "AnsemStudy");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 86 && world.eventComplete == 1) // Corridor finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Corridor");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Corridor");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 18 && world.eventID1 == 73 && world.eventComplete == 1) // Dancers finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Dancers");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Dancers");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 55 && world.eventComplete == 1) // HB Demyx finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBDemyx");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "HBDemyx");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 16 && world.eventID1 == 65 && world.eventComplete == 1) // FF Cloud finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "FinalFantasy");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "FinalFantasy");
                    data.WorldsData[world.worldName].progress = 7;
                }
                else if (world.roomNumber == 17 && world.eventID1 == 66 && world.eventComplete == 1) // 1k Heartless finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "1000Heartless");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "1000Heartless");
                    data.WorldsData[world.worldName].progress = 8;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 75 && world.eventComplete == 1) // Sephiroth finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Sephiroth");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "Sephiroth");
                    data.WorldsData[world.worldName].progress = 9;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 114 && world.eventComplete == 1) // Data Demyx finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "DataDemyx");
                    HollowBastionProgression.SetResourceReference(ContentProperty, Prog + "DataDemyx");
                    data.WorldsData[world.worldName].progress = 10;
                }
            }
            else if (world.worldName == "BeastsCastle")
            {
                if (world.roomNumber == 0 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Entrance Hall (BC1)
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "BCChests");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "BCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 11 && world.eventID1 == 72 && world.eventComplete == 1) // Thresholder finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Thresholder");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Thresholder");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 69 && world.eventComplete == 1) // Beast finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Beast");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Beast");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 79 && world.eventComplete == 1) // Dark Thorn finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "DarkThorn");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "DarkThorn");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 2 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Belle's Room (BC2)
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "BCChests");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "BCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 74 && world.eventComplete == 1) // Dragoons finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Dragoons");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Dragoons");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 82 && world.eventComplete == 1) // Xaldin finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Xaldin");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "Xaldin");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 97 && world.eventComplete == 1) // Data Xaldin finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "DataXaldin");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, Prog + "DataXaldin");
                    data.WorldsData[world.worldName].progress = 7;
                }
            }
            else if (world.worldName == "OlympusColiseum")
            {
                if (world.roomNumber == 0 & world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // The Coliseum (OC1)
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCChests");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 114 && world.eventComplete == 1) // Cerberus finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Cerberus");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Cerberus");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 17 && world.eventID1 == 123 && world.eventComplete == 1) // OC Demyx finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCDemyx");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCDemyx");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 8 && world.eventID1 == 116 && world.eventComplete == 1) // OC Pete finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCPete");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCPete");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 18 && world.eventID1 == 171 && world.eventComplete == 1) // Hydra finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Hydra");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Hydra");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 3 & world.eventID3 == 12 && data.WorldsData[world.worldName].progress == 0) // Underworld Entrance (OC2)
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCChests");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "OCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 6 && world.eventID1 == 126 && world.eventComplete == 1) // Auron Statue fight finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "AuronStatue");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "AuronStatue");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 19 && world.eventID1 == 202 && world.eventComplete == 1) // Hades finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Hades");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Hades");
                    data.WorldsData[world.worldName].progress = 7;
                }
                else if (world.roomNumber == 34 && (world.eventID1 == 151 || world.eventID1 == 152) && world.eventComplete == 1) // Zexion finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Zexion");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, Prog + "Zexion");
                    data.WorldsData[world.worldName].progress = 8;
                }
            }
            else if (world.worldName == "Agrabah")
            {
                if (world.roomNumber == 0 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Agrabah (Ag1)
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "AGChests");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "AGChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 2 && world.eventComplete == 1) // Abu finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Abu");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Abu");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 13 && world.eventID1 == 79 && world.eventComplete == 1) // Chasm fight finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Chasm");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Chasm");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 58 && world.eventComplete == 1) // Treasure Room finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "TreasureRoom");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "TreasureRoom");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 59 && world.eventComplete == 1) // Lords finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Lords");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Lords");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 4 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // The Vault (Ag2)
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "AGChests");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "AGChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 100 && world.eventComplete == 1) // Carpet finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Carpet");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Carpet");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 62 && world.eventComplete == 1) // Genie Jafar finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "GenieJafar");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "GenieJafar");
                    data.WorldsData[world.worldName].progress = 7;
                }
                else if (world.roomNumber == 33 && (world.eventID1 == 142 || world.eventID1 == 147) && world.eventComplete == 1) // Lexaeus finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Lexaeus");
                    AgrabahProgression.SetResourceReference(ContentProperty, Prog + "Lexaeus");
                    data.WorldsData[world.worldName].progress = 8;
                }
            }
            else if (world.worldName == "LandofDragons")
            {
                if (world.roomNumber == 0 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Bamboo Grove (LoD1)
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "LoDChests");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "LoDChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 72 && world.eventComplete == 1) // Cave finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "Cave");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "Cave");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 73 && world.eventComplete == 1) // Summit finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "Summit");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "Summit");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 75 && world.eventComplete == 1) // Shan Yu finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "ShanYu");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "ShanYu");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 12 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Village (LoD2)
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "LoDChests");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "LoDChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 78 && world.eventComplete == 1) // Antechamber fight finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "ThroneRoom");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "ThroneRoom");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 8 && world.eventID1 == 79 && world.eventComplete == 1) // Storm Rider finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "StormRider");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "StormRider");
                    data.WorldsData[world.worldName].progress = 6;
                }
            }
            else if (world.worldName == "HundredAcreWood")
            {
                if (world.roomNumber == 2 && (world.eventID3 == 1 || world.eventID3 == 21 || world.eventID3 == 22)) // Pooh's house (eventID3 == 1 is when not skipping AW0)
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Pooh");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Pooh");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 4 && world.eventID3 == 1) // Piglet's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Piglet");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Piglet");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 3 && world.eventID3 == 1) // Rabbit's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Rabbit");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Rabbit");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 5 && world.eventID3 == 1) // Kanga's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Kanga");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "Kanga");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 9 && world.eventID3 == 1) // Spooky Cave
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "SpookyCave");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "SpookyCave");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 1 && world.eventID3 == 1) // Starry Hill
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "StarryHill");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, Prog + "StarryHill");
                    data.WorldsData[world.worldName].progress = 6;
                }
            }
            else if (world.worldName == "PrideLands")
            {
                if (world.roomNumber == 16 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Wildebeest Valley (PL1)
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "PLChests");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "PLChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 12 && world.eventID3 == 1) // Oasis after talking to Simba
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Simba");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Simba");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 55 && world.eventComplete == 1) // Scar finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Scar");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Scar");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 4 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Savannah (PL2)
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "PLChests");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "PLChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 59 && world.eventComplete == 1) // Groundshaker finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Groundshaker");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "Groundshaker");
                    data.WorldsData[world.worldName].progress = 4;
                }
            }
            else if (world.worldName == "Atlantica")
            {
                if (world.roomNumber == 2 && world.eventID1 == 63) // Tutorial
                {
                    broadcast.AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "Tutorial");
                    AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "Tutorial");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 65) // Ursula's Revenge
                {
                    broadcast.AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "Ursula");
                    AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "Ursula");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 55) // A New Day is Dawning
                {
                    broadcast.AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "NewDay");
                    AtlanticaProgression.SetResourceReference(ContentProperty, Prog + "NewDay");
                    data.WorldsData[world.worldName].progress = 3;
                }
            }
            else if (world.worldName == "DisneyCastle")
            {
                if (world.roomNumber == 1 && world.eventID1 == 53 && data.WorldsData[world.worldName].progress == 0) // Library (DC)
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCChests");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 0 && world.eventID1 == 51 && world.eventComplete == 1) // Minnie Escort finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Minnie");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Minnie");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 0 && world.eventID3 == 22 && data.WorldsData[world.worldName].progress == 0) // Cornerstone Hill (TR) (Audience Chamber has no Evt 0x16)
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCChests");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 58 && world.eventComplete == 1) // Old Pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "OldPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "OldPete");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 0 && world.eventID3 == 6) // Windows popup (Audience Chamber has no Evt 0x06)
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Windows");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Windows");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 2 && world.eventID1 == 52 && world.eventComplete == 1) // Boat Pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "BoatPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "BoatPete");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 53 && world.eventComplete == 1) // DC Pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "DCPete");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 38 && (world.eventID1 == 145 || world.eventID1 == 150) && world.eventComplete == 1) // Marluxia finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Marluxia");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "Marluxia");
                    data.WorldsData[world.worldName].progress = 7;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 67 && world.eventComplete == 1) // Lingering Will finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "LingeringWill");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, Prog + "LingeringWill");
                    data.WorldsData[world.worldName].progress = 8;
                }
            }
            else if (world.worldName == "HalloweenTown")
            {
                if (world.roomNumber == 4 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Hinterlands (HT1)
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "HTChests");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "HTChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 6 && world.eventID1 == 53 && world.eventComplete == 1) // Candy Cane Lane fight finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "CandyCaneLane");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "CandyCaneLane");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 52 && world.eventComplete == 1) // Prison Keeper finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "PrisonKeeper");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "PrisonKeeper");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 55 && world.eventComplete == 1) // Oogie Boogie finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "OogieBoogie");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "OogieBoogie");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 1 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Dr. Finklestein's Lab (HT2)
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "HTChests");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "HTChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 63 && world.eventComplete == 1) // Presents minigame
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Presents");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Presents");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 64 && world.eventComplete == 1) // Experiment finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Experiment");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Experiment");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 32 && (world.eventID1 == 115 || world.eventID1 == 146) && world.eventComplete == 1) // Vexen finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Vexen");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, Prog + "Vexen");
                    data.WorldsData[world.worldName].progress = 7;
                }
            }
            else if (world.worldName == "PortRoyal")
            {
                if (world.roomNumber == 0 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Rampart (PR1)
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "PRChests");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "PRChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 2 && world.eventID1 == 55 && world.eventComplete == 1) // Town finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Town");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Town");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 60 && world.eventComplete == 1) // Barbossa finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Barbossa");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Barbossa");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 10 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Treasure Heap (PR2)
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "PRChests");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "PRChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 62 && world.eventComplete == 1) // Gambler finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Gambler");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "Gambler");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 54 && world.eventComplete == 1) // Grim Reaper finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "GrimReaper");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "GrimReaper");
                    data.WorldsData[world.worldName].progress = 5;
                }
            }
            else if (world.worldName == "SpaceParanoids")
            {
                if (world.roomNumber == 1 && world.eventID3 == 1 && data.WorldsData[world.worldName].progress == 0) // Canyon (SP1)
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SPChests");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SPChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 54 && world.eventComplete == 1) // Screens finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "Screens");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "Screens");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 55 && world.eventComplete == 1) // Hostile Program finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "HostileProgram");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "HostileProgram");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 1 && world.eventID3 == 10 && data.WorldsData[world.worldName].progress == 0) // Canyon (SP2)
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SPChests");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SPChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 57 && world.eventComplete == 1) // Solar Sailer finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SolarSailer");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "SolarSailer");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 59 && world.eventComplete == 1) // MCP finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "MCP");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "MCP");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 33 && (world.eventID1 == 143 || world.eventID1 == 148) && world.eventComplete == 1) // Larxene finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "Larxene");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, Prog + "Larxene");
                    data.WorldsData[world.worldName].progress = 6;
                }
            }
            else if (world.worldName == "TWTNW")
            {
                if (world.roomNumber == 1 && world.eventID3 == 1) // Alley to Between
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "TWTNWChests");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "TWTNWChests");
                    data.WorldsData[world.worldName].progress = 1;
                }
                else if (world.roomNumber == 21 && world.eventID1 == 65 && world.eventComplete == 1) // Roxas finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Roxas");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Roxas");
                    data.WorldsData[world.worldName].progress = 2;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 57 && world.eventComplete == 1) // Xigbar finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Xigbar");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Xigbar");
                    data.WorldsData[world.worldName].progress = 3;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 58 && world.eventComplete == 1) // Luxord finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Luxord");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Luxord");
                    data.WorldsData[world.worldName].progress = 4;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 56 && world.eventComplete == 1) // Saix finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Saix");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Saix");
                    data.WorldsData[world.worldName].progress = 5;
                }
                else if (world.roomNumber == 19 && world.eventID1 == 59 && world.eventComplete == 1) // Xemnas 1 finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Xemnas1");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "Xemnas1");
                    data.WorldsData[world.worldName].progress = 6;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 98 && world.eventComplete == 1) // Data Xemnas finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, Prog + "DataXemnas");
                    TWTNWProgression.SetResourceReference(ContentProperty, Prog + "DataXemnas");
                    data.WorldsData[world.worldName].progress = 7;
                }

                // Handle data fights
                else if (world.roomNumber == 21 && world.eventID1 == 99 && world.eventComplete == 1) // Data Roxas finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "DataRoxas");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, Prog + "DataRoxas");
                    data.WorldsData["SimulatedTwilightTown"].progress = 3;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 100 && world.eventComplete == 1) // Data Xigbar finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "DataXigbar");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, Prog + "DataXigbar");
                    data.WorldsData["LandofDragons"].progress = 7;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 102 && world.eventComplete == 1) // Data Saix finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "DataSaix");
                    PrideLandsProgression.SetResourceReference(ContentProperty, Prog + "DataSaix");
                    data.WorldsData["PrideLands"].progress = 5;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 101 && world.eventComplete == 1) // Data Luxord finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "DataLuxord");
                    PortRoyalProgression.SetResourceReference(ContentProperty, Prog + "DataLuxord");
                    data.WorldsData["PortRoyal"].progress = 6;
                }
            }
            else if (world.worldName == "GoA")
            {
                if (world.roomNumber == 32)
                {
                    if (SeedHashVisible)
                    {
                        HashRow.Height = new GridLength(0, GridUnitType.Star);
                        SeedHashVisible = false;
                    }
                }
                
            }

            //Console.WriteLine("Seed hash visible? = " + SeedHashVisible);

        }

        private string BytesToHex(byte[] bytes)
        {
            if (Enumerable.SequenceEqual(bytes, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                return "Service not started. Waiting for PCSX2";
            }
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        private void BindLevel(Image img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new LevelConverter();
            img.SetBinding(Image.SourceProperty, binding);
        }

        private void BindForm(ContentControl img, string property, object source)
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

        private void BindAbilityLevel(Image img, string property, object source, IValueConverter convertor)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = convertor;
            img.SetBinding(Image.SourceProperty, binding);
        }

        private void BindAbility(ContentControl img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new ObtainedConverter();
            img.SetBinding(OpacityProperty, binding);
        }

        public string GetWorld()
        {
            return world.worldName;
        }

        //used to get the numbers for everything to change color corectly when changing icon modes
        private void ReloadBindings()
        {
            BindNumberTens(Level_01, Level_10, "Level", stats);
            BindNumberFull(Strength_001, Strength_010, Strength_100, "Strength", stats);
            BindNumberFull(Magic_001, Magic_010, Magic_100, "Magic", stats);
            BindNumberFull(Defense_001, Defense_010, Defense_100, "Defense", stats);

            BindWeapon(Weapon, "Weapon", stats);
            BindWeapon(broadcast.Weapon, "Weapon", stats);

            BindNumberTens(broadcast.Level_01, broadcast.Level_10, "Level", stats);
            BindNumberFull(broadcast.Strength_001, broadcast.Strength_010, broadcast.Strength_100, "Strength", stats);
            BindNumberFull(broadcast.Magic_001, broadcast.Magic_010, broadcast.Magic_100, "Magic", stats);
            BindNumberFull(broadcast.Defense_001, broadcast.Defense_010, broadcast.Defense_100, "Defense", stats);

            BindLevel(broadcast.ValorLevel, "Level", valor);
            BindLevel(broadcast.WisdomLevel, "Level", wisdom);
            BindLevel(broadcast.LimitLevel, "Level", limit);
            BindLevel(broadcast.MasterLevel, "Level", master);
            BindLevel(broadcast.FinalLevel, "Level", final);

            BindAbility(broadcast.HighJump, "Obtained", highJump);
            BindAbility(broadcast.QuickRun, "Obtained", quickRun);
            BindAbility(broadcast.DodgeRoll, "Obtained", dodgeRoll);
            BindAbility(broadcast.AerialDodge, "Obtained", aerialDodge);
            BindAbility(broadcast.Glide, "Obtained", glide);

            BindAbility(HighJump, "Obtained", highJump);
            BindAbility(QuickRun, "Obtained", quickRun);
            BindAbility(DodgeRoll, "Obtained", dodgeRoll);
            BindAbility(AerialDodge, "Obtained", aerialDodge);
            BindAbility(Glide, "Obtained", glide);

            BindAbilityLevel(broadcast.HighJumpLevel, "Level", highJump, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.QuickRunLevel, "Level", quickRun, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.DodgeRollLevel, "Level", dodgeRoll, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.AerialDodgeLevel, "Level", aerialDodge, new GrowthAbilityConverter());
            BindAbilityLevel(broadcast.GlideLevel, "Level", glide, new GrowthAbilityConverter());

            BindAbilityLevel(HighJumpLevel, "Level", highJump, new GrowthAbilityConverter());
            BindAbilityLevel(QuickRunLevel, "Level", quickRun, new GrowthAbilityConverter());
            BindAbilityLevel(DodgeRollLevel, "Level", dodgeRoll, new GrowthAbilityConverter());
            BindAbilityLevel(AerialDodgeLevel, "Level", aerialDodge, new GrowthAbilityConverter());
            BindAbilityLevel(GlideLevel, "Level", glide, new GrowthAbilityConverter());

            BindLevel(ValorLevel, "Level", valor);
            BindLevel(WisdomLevel, "Level", wisdom);
            BindLevel(LimitLevel, "Level", limit);
            BindLevel(MasterLevel, "Level", master);
            BindLevel(FinalLevel, "Level", final);

            Updatenumbers();
            broadcast.UpdateNumbers();

            UpdatePointScore(0);
        }

        private void Updatenumbers()
        {
            //get correct slash image
            bool CustomMode = Properties.Settings.Default.CustomIcons;
            BitmapImage NumberBarY = data.SlashBarY;
            if (CustomMode && CustomBarYFound)
            {
                NumberBarY = data.CustomSlashBarY;
            }

            //Get and set world values
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                bool isBlue = false;
                bool isGreen = false;

                if (worldData.complete || worldData.hintedHint)
                    isBlue = true;
                if (worldData.containsGhost && data.mode == Mode.DAHints)
                    isGreen = true;

                if (worldData.hint != null)
                {
                    int WorldNumber = GetWorldNumber(worldData.hint);

                    if (isGreen)
                    {
                        SetWorldNumber(worldData.hint, WorldNumber, "G");
                    }
                    else if (isBlue)
                    {
                        SetWorldNumber(worldData.hint, WorldNumber, "B");
                    }
                    else
                    {
                        SetWorldNumber(worldData.hint, WorldNumber, "Y");
                    }
                }

                //might as well get and set the correct vertical bar image here while we have this data loop
                if (worldData.world.IsPressed)
                {
                    if (CustomMode)
                        worldData.selectedBar.Source = data.CustomVerticalBarY;
                    else
                        worldData.selectedBar.Source = data.VerticalBarY;
                }
                else
                {
                    if (CustomMode)
                        worldData.selectedBar.Source = data.CustomVerticalBarW;
                    else
                        worldData.selectedBar.Source = data.VerticalBarW;
                }
            }


            //update collected count numbers
            List<BitmapImage> CollectedNum = UpdateNumber(collected, "Y");
            if (collected < 10)
                CollectedNum[1] = null;

            Collected_01.Source = CollectedNum[0];
            Collected_10.Source = CollectedNum[1];
            broadcast.Collected_01.Source = CollectedNum[0];
            broadcast.Collected_10.Source = CollectedNum[1];

            //update total count numbers
            List<BitmapImage> TotalNum = UpdateNumber(total, "Y");
            CheckTotal_01.Source = TotalNum[0];
            CheckTotal_10.Source = TotalNum[1];
            broadcast.CheckTotal_01.Source = TotalNum[0];
            broadcast.CheckTotal_10.Source = TotalNum[1];

            CollectedBar.Source = NumberBarY;
        }

        private void BindNumberFull(Image img1, Image img2, Image img3, string property, object source)
        {
            if (source != null)
            {
                Binding binding1 = new Binding(property);
                Binding binding2 = new Binding(property);
                Binding binding3 = new Binding(property);
                binding1.Source = source;
                binding2.Source = source;
                binding3.Source = source;
                binding1.Converter = new NumberConverter001();
                binding2.Converter = new NumberConverter010();
                binding3.Converter = new NumberConverter100();

                if (img1 != null)
                    img1.SetBinding(Image.SourceProperty, binding1);

                if (img2 != null)
                    img2.SetBinding(Image.SourceProperty, binding2);

                if (img3 != null)
                    img3.SetBinding(Image.SourceProperty, binding3);
            }
        }

        private void BindNumberTens(Image img1, Image img2, string property, object source)
        {
            if (source != null)
            {
                Binding binding1 = new Binding(property);
                Binding binding2 = new Binding(property);
                binding1.Source = source;
                binding2.Source = source;
                binding1.Converter = new NumberConverter001();
                binding2.Converter = new NumberConverter010();

                if (img1 != null)
                    img1.SetBinding(Image.SourceProperty, binding1);

                if (img2 != null)
                    img2.SetBinding(Image.SourceProperty, binding2);
            }
        }

        private bool CheckSynthPuzzle(bool ps2)
        {
            if (ps2)
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

        private void DeathCheck(bool ps2)
        {
            //Note: 04 = dying, 05 = continue screen.
            //note: if i try tracking a death when pausecheck is "0400" then that should give a
            //more accurate death count in the event that continue is selected too fast (i hope)

            string PauseCheck;
            if (ps2)
            {
                PauseCheck = BytesToHex(memory.ReadMemory(0x0347E08 + ADDRESS_OFFSET, 2));
            }
            else
            {
                PauseCheck = BytesToHex(memory.ReadMemory(0xAB9078, 2));
            }

            if (onContinue)
            {
                if (PauseCheck == "0400" || PauseCheck == "0500")
                    return;
                else
                    onContinue = false;
            }

            if (PauseCheck == "0400" || PauseCheck == "0500")
            {
                DeathCounter += 1;
                onContinue = true;
            }

            List<BitmapImage> DeathNum = UpdateNumber(DeathCounter, "Y");
            Death_01.Source = DeathNum[0];
            broadcast.Death_01.Source = DeathNum[0];
            if (DeathCounter < 10)
            {
                Death_10.Source = null;
                broadcast.Death_10.Source = null;
            }
            else
            {
                Death_10.Source = DeathNum[1];
                broadcast.Death_10.Source = DeathNum[1];
            }
        }

        private void StatResize()
        {
            if (stats.Strength < 100)
            {
                Strength.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                Strength.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            }

            if (stats.Magic < 100)
            {
                Magic.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                Magic.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            }

            if (stats.Defense < 100)
            {
                Defense.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                Defense.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            }


        }
    }
}
