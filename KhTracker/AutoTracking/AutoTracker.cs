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

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MemoryReader memory;

        private Int32 ADDRESS_OFFSET;
        private static DispatcherTimer aTimer;
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

        private Magic fire;
        private Magic blizzard;
        private Magic thunder;
        private Magic magnet;
        private Magic reflect;
        private Magic cure;

        private Report rep1;
        private Report rep2;
        private Report rep3;
        private Report rep4;
        private Report rep5;
        private Report rep6;
        private Report rep7;
        private Report rep8;
        private Report rep9;
        private Report rep10;
        private Report rep11;
        private Report rep12;
        private Report rep13;

        private Summon chickenLittle;
        private Summon stitch;
        private Summon genie;
        private Summon peterPan;

        private ImportantCheck promiseCharm;
        private ImportantCheck peace;
        private ImportantCheck nonexist;
        private ImportantCheck connection;

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

        public void InitAutoTracker(object sender, RoutedEventArgs e)
        {
            int tries = 0;
            do
            {
                memory = new MemoryReader();
                if (tries < 20)
                {
                    tries++;
                }
                else
                {
                    memory = null;
                    MessageBox.Show("Please launch PCSX2 before loading the Auto Tracker.");
                    return;
                }
            } while (!memory.Hooked);
            this.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./resources/#KH2 ALL MENU");
            findAddressOffset();

            importantChecks = new List<ImportantCheck>();
            importantChecks.Add(highJump = new Ability(memory, 0x0032E0FE, ADDRESS_OFFSET, 93, "HighJump"));
            importantChecks.Add(quickRun = new Ability(memory, 0x0032E100, ADDRESS_OFFSET, 97, "QuickRun"));
            importantChecks.Add(dodgeRoll = new Ability(memory, 0x0032E102, ADDRESS_OFFSET, 563, "DodgeRoll"));
            importantChecks.Add(aerialDodge = new Ability(memory, 0x0032E104, ADDRESS_OFFSET, 101, "AerialDodge"));
            importantChecks.Add(glide = new Ability(memory, 0x0032E106, ADDRESS_OFFSET, 105, "Glide"));

            importantChecks.Add(secondChance = new Ability(memory, 0x0032E074, ADDRESS_OFFSET, "SecondChance"));
            importantChecks.Add(onceMore = new Ability(memory, 0x0032E074, ADDRESS_OFFSET, "OnceMore"));
            
            importantChecks.Add(valor = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 1, 0x0032EE26, "Valor"));
            importantChecks.Add(wisdom = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 2, 0x0032EE5E, "Wisdom"));
            importantChecks.Add(limit = new DriveForm(memory, 0x0032F1FA, ADDRESS_OFFSET, 3, 0x0032EE96, "Limit"));
            importantChecks.Add(master = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 6, 0x0032EECE, "Master"));
            importantChecks.Add(final = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 4, 0x0032EF06, "Final"));

            importantChecks.Add(fire = new Magic(memory, 0x0032F0C4, 0x0032D822, ADDRESS_OFFSET, "Fire"));
            importantChecks.Add(blizzard = new Magic(memory, 0x0032F0C5, 0x0032D823, ADDRESS_OFFSET, "Blizzard"));
            importantChecks.Add(thunder = new Magic(memory, 0x0032F0C6, 0x0032D824, ADDRESS_OFFSET, "Thunder"));
            importantChecks.Add(cure = new Magic(memory, 0x0032F0C7, 0x0032D825, ADDRESS_OFFSET, "Cure"));
            importantChecks.Add(magnet = new Magic(memory, 0x0032F0FF, 0x0032D826, ADDRESS_OFFSET, "Magnet"));
            importantChecks.Add(reflect = new Magic(memory, 0x0032F100, 0x0032D827, ADDRESS_OFFSET, "Reflect"));

            importantChecks.Add(rep1 = new Report(memory, 0x0032F1F4, ADDRESS_OFFSET, 6, "Report1"));
            importantChecks.Add(rep2 = new Report(memory, 0x0032F1F4, ADDRESS_OFFSET, 7, "Report2"));
            importantChecks.Add(rep3 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 0, "Report3"));
            importantChecks.Add(rep4 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 1, "Report4"));
            importantChecks.Add(rep5 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 2, "Report5"));
            importantChecks.Add(rep6 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 3, "Report6"));
            importantChecks.Add(rep7 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 4, "Report7"));
            importantChecks.Add(rep8 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 5, "Report8"));
            importantChecks.Add(rep9 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 6, "Report9"));
            importantChecks.Add(rep10 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 7, "Report10"));
            importantChecks.Add(rep11 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 0, "Report11"));
            importantChecks.Add(rep12 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 1, "Report12"));
            importantChecks.Add(rep13 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 2, "Report13"));

            importantChecks.Add(chickenLittle = new Summon(memory, 0x0032F1F0, ADDRESS_OFFSET, 3, "Baseball"));
            importantChecks.Add(stitch = new Summon(memory, 0x0032F1F0, ADDRESS_OFFSET, 0, "Ukulele"));
            importantChecks.Add(genie = new Summon(memory, 0x0032F1F4, ADDRESS_OFFSET, 4, "Lamp"));
            importantChecks.Add(peterPan = new Summon(memory, 0x0032F1F4, ADDRESS_OFFSET, 5, "Feather"));

            importantChecks.Add(promiseCharm = new Proof(memory, 0x0032F1C4, ADDRESS_OFFSET, "PromiseCharm"));
            importantChecks.Add(peace = new Proof(memory, 0x0032F1E4, ADDRESS_OFFSET, "Peace"));
            importantChecks.Add(nonexist = new Proof(memory, 0x0032F1E3, ADDRESS_OFFSET, "Nonexistence"));
            importantChecks.Add(connection = new Proof(memory, 0x0032F1E2, ADDRESS_OFFSET, "Connection"));

            importantChecks.Add(pages = new TornPage(memory, 0x0032F0C8, ADDRESS_OFFSET, "TornPage"));

            world = new World(memory, ADDRESS_OFFSET, 0x0032BAE0, 0x00351EC8);
            stats = new Stats(memory, ADDRESS_OFFSET, 0x0032E02E, 0x01C6C8D8, 0x0032F054);
            rewards = new Rewards(memory, ADDRESS_OFFSET);

            LevelIcon.Visibility = Visibility.Visible;
            Level.Visibility = Visibility.Visible;
            StrengthIcon.Visibility = Visibility.Visible;
            Strength.Visibility = Visibility.Visible;
            MagicIcon.Visibility = Visibility.Visible;
            Magic.Visibility = Visibility.Visible;
            DefenseIcon.Visibility = Visibility.Visible;
            Defense.Visibility = Visibility.Visible;
            Weapon.Visibility = Visibility.Visible;

            broadcast.LevelIcon.Visibility = Visibility.Visible;
            broadcast.Level.Visibility = Visibility.Visible;
            broadcast.StrengthIcon.Visibility = Visibility.Visible;
            broadcast.Strength.Visibility = Visibility.Visible;
            broadcast.MagicIcon.Visibility = Visibility.Visible;
            broadcast.Magic.Visibility = Visibility.Visible;
            broadcast.DefenseIcon.Visibility = Visibility.Visible;
            broadcast.Defense.Visibility = Visibility.Visible;
            broadcast.Weapon.Visibility = Visibility.Visible;

            broadcast.WorldRow.Height = new GridLength(6, GridUnitType.Star);
            broadcast.GrowthAbilityRow.Height = new GridLength(1, GridUnitType.Star);

            SetBindings();
            SetTimer();
            OnTimedEvent(null, null);
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
                    offset = offset + 0x10000000;
                }
            }
            ADDRESS_OFFSET = offset;
        }

        private void SetBindings()
        {
            BindStats(Level, "Level", stats);
            BindWeapon(Weapon, "Weapon", stats);
            BindStats(Strength, "Strength", stats);
            BindStats(Magic, "Magic", stats);
            BindStats(Defense, "Defense", stats);

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
        }

        private void SetTimer()
        {
            aTimer = new DispatcherTimer();
            aTimer.Tick += OnTimedEvent;
            aTimer.Interval = new TimeSpan(0, 0, 1);
            aTimer.Start();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            previousChecks.Clear();
            previousChecks.AddRange(newChecks);
            newChecks.Clear();

            if (ADDRESS_OFFSET == 0)
            {
                findAddressOffset();
            }

            // Checks to see if the connection has exited.
            byte[] tester = memory.ReadMemory(0x0010001C + ADDRESS_OFFSET, 2);
            if (Enumerable.SequenceEqual(tester, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                aTimer.Stop();
                MessageBox.Show("PCSX2 has exited. Stopping auto tracker.");
                return;
            }

            stats.UpdateMemory();
            world.UpdateMemory();
            UpdateMagicAddresses();
            UpdateWorldProgress(world);


            importantChecks.ForEach(delegate (ImportantCheck importantCheck)
            {
                importantCheck.UpdateMemory();
            });

            UpdateCollectedItems();
            DetermineItemLocations();
        }

        private void TrackItem(string itemName, WorldGrid world)
        {
            foreach (ContentControl item in ItemPool.Children)
            {
                if (item.Name == itemName && item.IsVisible)
                {
                    if (world.Handle_Report(item as Item, this, data))
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
            if (world.worldName == "SimulatedTwilightTown")
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
                    if ((check.Name == "Final" && stats.form == 5) || check.Name == "Valor")
                    {
                        collectedChecks.Add(check);
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
                    // add check to current world
                    TrackItem(check.Name + count, data.WorldsData[world.worldName].worldGrid);
                }
            }
        }

        void UpdateWorldProgress(World world)
        {
            if (world.worldName == "SimulatedTwilightTown")
            {
                if (world.roomNumber == 21 && world.eventID1 == 7 && world.eventID3 == 16 && data.WorldProgress[world.worldName] <= 0) // Mansion: Computer Room
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "ComputerRoom");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "ComputerRoom");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 137 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Axel finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "Axel");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "Axel");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 21 && world.eventID1 == 99 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Data Roxas finish
                {
                    broadcast.SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "DataRoxas");
                    SimulatedTwilightTownProgression.SetResourceReference(ContentProperty, "DataRoxas");
                    data.WorldProgress[world.worldName] = 3;
                }
            }
            else if (world.worldName == "TwilightTown")
            {
                if (world.roomNumber == 27 && world.eventID1 == 2 && world.eventID3 == 4 && data.WorldProgress[world.worldName] <= 0) // Yen Sid after new clothes
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "MysteriousTower");
                    TwilightTownProgression.SetResourceReference(ContentProperty, "MysteriousTower");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 80 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Sandlot finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "Sandlot");
                    TwilightTownProgression.SetResourceReference(ContentProperty, "Sandlot");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 41 && world.eventID1 == 186 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Mansion fight finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "Mansion");
                    TwilightTownProgression.SetResourceReference(ContentProperty, "Mansion");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 40 && world.eventID1 == 161 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Betwixt and Between finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "BetwixtandBetween");
                    TwilightTownProgression.SetResourceReference(ContentProperty, "BetwixtandBetween");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 213 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Data Axel finish
                {
                    broadcast.TwilightTownProgression.SetResourceReference(ContentProperty, "DataAxel");
                    TwilightTownProgression.SetResourceReference(ContentProperty, "DataAxel");
                    data.WorldProgress[world.worldName] = 5;
                }
            }
            else if (world.worldName == "HollowBastion")
            {
                if (world.roomNumber == 10 && data.WorldProgress[world.worldName] <= 0) // Marketplace
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "HBChests");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "HBChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                if (world.roomNumber == 8 && world.eventID1 == 52 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Bailey finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "Bailey");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "Bailey");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 5 && world.eventID3 == 20 && data.WorldProgress[world.worldName] <= 2) // Ansem Study post Computer
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "AnsemStudy");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "AnsemStudy");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 86 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Corridor finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "Corridor");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "Corridor");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 18 && world.eventID1 == 73 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Dancers finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "Dancers");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "Dancers");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 55 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // HB Demyx finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "HBDemyx");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "HBDemyx");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 1 && world.eventID3 == 22 && data.WorldProgress[world.worldName] <= 6) // Crystal Fissure (forgot to get the FF fight ids)
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "FinalFantasy");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "FinalFantasy");
                    data.WorldProgress[world.worldName] = 7;
                }
                else if (world.roomNumber == 17 && world.eventID1 == 66 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 7) // 1k Heartless finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "1000Heartless");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "1000Heartless");
                    data.WorldProgress[world.worldName] = 8;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 75 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 8) // Sephiroth finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "Sephiroth");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "Sephiroth");
                    data.WorldProgress[world.worldName] = 9;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 114 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 9) // Data Demyx finish
                {
                    broadcast.HollowBastionProgression.SetResourceReference(ContentProperty, "DataDemyx");
                    HollowBastionProgression.SetResourceReference(ContentProperty, "DataDemyx");
                    data.WorldProgress[world.worldName] = 10;
                }
            }
            else if (world.worldName == "BeastsCastle")
            {
                if (world.roomNumber == 1 && world.eventID1 == 68 && data.WorldProgress[world.worldName] <= 0) // Parlor fight
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "BCChests");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "BCChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 11 && world.eventID1 == 72 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Thresholder finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "Thresholder");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "Thresholder");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 69 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Beast finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "Beast");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "Beast");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 79 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Dark Thorn finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "DarkThorn");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "DarkThorn");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 74 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Dragoons finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "Dragoons");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "Dragoons");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 82 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Xaldin finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "Xaldin");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "Xaldin");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 97 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Data Xaldin finish
                {
                    broadcast.BeastsCastleProgression.SetResourceReference(ContentProperty, "DataXaldin");
                    BeastsCastleProgression.SetResourceReference(ContentProperty, "DataXaldin");
                    data.WorldProgress[world.worldName] = 7;
                }
            }
            else if (world.worldName == "OlympusColiseum")
            {
                if (world.roomNumber == 3 && data.WorldProgress[world.worldName] <= 0) // Underworld Entrance
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCChests");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 114 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Cerberus finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "Cerberus");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "Cerberus");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 17 && world.eventID1 == 123 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // OC Demyx finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCDemyx");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCDemyx");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 8 && world.eventID1 == 116 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // OC Pete finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCPete");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "OCPete");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 18 && world.eventID1 == 171 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Hydra finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "Hydra");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "Hydra");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 6 && world.eventID1 == 126 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Auron Statue fight finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "AuronStatue");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "AuronStatue");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 19 && world.eventID1 == 202 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Hades finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "Hades");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "Hades");
                    data.WorldProgress[world.worldName] = 7;
                }
                else if (world.roomNumber == 34 && world.eventID1 == 151 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 7) // Zexion finish
                {
                    broadcast.OlympusColiseumProgression.SetResourceReference(ContentProperty, "Zexion");
                    OlympusColiseumProgression.SetResourceReference(ContentProperty, "Zexion");
                    data.WorldProgress[world.worldName] = 8;
                }
            }
            else if (world.worldName == "Agrabah")
            {
                if (world.roomNumber == 0 && world.eventID1 == 57 && data.WorldProgress[world.worldName] <= 0) // Agrabah fight
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "AGChests");
                    AgrabahProgression.SetResourceReference(ContentProperty, "AGChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 2 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Abu finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "Abu");
                    AgrabahProgression.SetResourceReference(ContentProperty, "Abu");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 13 && world.eventID1 == 79 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Chasm fight finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "Chasm");
                    AgrabahProgression.SetResourceReference(ContentProperty, "Chasm");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 58 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Treasure Room finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "TreasureRoom");
                    AgrabahProgression.SetResourceReference(ContentProperty, "TreasureRoom");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 59 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Lords finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "Lords");
                    AgrabahProgression.SetResourceReference(ContentProperty, "Lords");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 100 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Carpet finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "Carpet");
                    AgrabahProgression.SetResourceReference(ContentProperty, "Carpet");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 62 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Genie Jafar finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "GenieJafar");
                    AgrabahProgression.SetResourceReference(ContentProperty, "GenieJafar");
                    data.WorldProgress[world.worldName] = 7;
                }
                else if (world.roomNumber == 33 && world.eventID1 == 142 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 7) // Lexaeus finish
                {
                    broadcast.AgrabahProgression.SetResourceReference(ContentProperty, "Lexaeus");
                    AgrabahProgression.SetResourceReference(ContentProperty, "Lexaeus");
                    data.WorldProgress[world.worldName] = 8;
                }
            }
            else if (world.worldName == "LandofDragons")
            {
                if (world.roomNumber == 0 && world.eventID3 == 19 && data.WorldProgress[world.worldName] <= 0) // Bamboo Grove
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "LoDChests");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "LoDChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 5 && world.eventID1 == 72 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Cave finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "Cave");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "Cave");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 73 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Summit finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "Summit");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "Summit");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 75 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Shan Yu finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "ShanYu");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "ShanYu");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 78 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Antechamber fight finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "ThroneRoom");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "ThroneRoom");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 8 && world.eventID1 == 79 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Storm Rider finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "StormRider");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "StormRider");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 100 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Data Xigbar finish
                {
                    broadcast.LandofDragonsProgression.SetResourceReference(ContentProperty, "DataXigbar");
                    LandofDragonsProgression.SetResourceReference(ContentProperty, "DataXigbar");
                    data.WorldProgress[world.worldName] = 7;
                }
            }
            else if (world.worldName == "HundredAcreWood")
            {
                if (world.roomNumber == 2 && data.WorldProgress[world.worldName] <= 0) // Pooh's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Pooh");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Pooh");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 4 && data.WorldProgress[world.worldName] <= 1) // Piglet's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Piglet");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Piglet");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 3 && data.WorldProgress[world.worldName] <= 2) // Rabbit's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Rabbit");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Rabbit");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 5 && data.WorldProgress[world.worldName] <= 3) // Kanga's house
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Kanga");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "Kanga");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 9 && data.WorldProgress[world.worldName] <= 4) // Spooky Cave
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "SpookyCave");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "SpookyCave");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 1 && world.eventID3 == 2 && data.WorldProgress[world.worldName] <= 5) // Starry Hill
                {
                    broadcast.HundredAcreWoodProgression.SetResourceReference(ContentProperty, "StarryHill");
                    HundredAcreWoodProgression.SetResourceReference(ContentProperty, "StarryHill");
                    data.WorldProgress[world.worldName] = 6;
                }
            }
            else if (world.worldName == "PrideLands")
            {
                if (world.roomNumber == 6 && world.eventID1 == 1 && world.eventID3 == 19 && data.WorldProgress[world.worldName] <= 0) // first room
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "PLChests");
                    PrideLandsProgression.SetResourceReference(ContentProperty, "PLChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 9 && world.eventID3 == 21 && data.WorldProgress[world.worldName] <= 1) // oasis after talking to simba
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "Simba");
                    PrideLandsProgression.SetResourceReference(ContentProperty, "Simba");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 55 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Scar finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "Scar");
                    PrideLandsProgression.SetResourceReference(ContentProperty, "Scar");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 59 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Groundshaker finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "Groundshaker");
                    PrideLandsProgression.SetResourceReference(ContentProperty, "Groundshaker");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 102 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Data Saix finish
                {
                    broadcast.PrideLandsProgression.SetResourceReference(ContentProperty, "DataSaix");
                    PrideLandsProgression.SetResourceReference(ContentProperty, "DataSaix");
                    data.WorldProgress[world.worldName] = 5;
                }
            }
            else if (world.worldName == "DisneyCastle")
            {
                if (world.roomNumber == 6 && world.eventID3 == 22 && data.WorldProgress[world.worldName] <= 0) // Gummi hangar
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "DCChests");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "DCChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 0 && world.eventID1 == 51 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Hall of the cornerstone
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "Minnie");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "Minnie");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 58 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Old pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "OldPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "OldPete");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 0 && world.eventID1 == 0 && world.eventID2 == 0 && world.eventID3 == 0 && data.WorldProgress[world.worldName] <= 3) // Windows popup
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "Windows");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "Windows");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 2 && world.eventID1 == 52 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Boat Pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "BoatPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "BoatPete");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 53 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // DC Pete finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "DCPete");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "DCPete");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 38 && world.eventID1 == 145 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Marluxia finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "Marluxia");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "Marluxia");
                    data.WorldProgress[world.worldName] = 7;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 67 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 7) // Lingering Will finish
                {
                    broadcast.DisneyCastleProgression.SetResourceReference(ContentProperty, "LingeringWill");
                    DisneyCastleProgression.SetResourceReference(ContentProperty, "LingeringWill");
                    data.WorldProgress[world.worldName] = 8;
                }
            }
            else if (world.worldName == "HalloweenTown")
            {
                if (world.roomNumber == 2 && world.eventID1 == 5 && world.eventID3 == 21 && data.WorldProgress[world.worldName] <= 0) // graveyard
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "HTChests");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "HTChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 6 && world.eventID1 == 53 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Candy Cane Lane fight finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "CandyCaneLane");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "CandyCaneLane");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 52 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Prison Keeper finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "PrisonKeeper");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "PrisonKeeper");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 55 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Oogie Boogie finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "OogieBoogie");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "OogieBoogie");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 63 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Presents minigame
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "Presents");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "Presents");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 64 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Experiment finish
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "Experiment");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "Experiment");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 32 && world.eventID1 == 115 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // vexen finished
                {
                    broadcast.HalloweenTownProgression.SetResourceReference(ContentProperty, "Vexen");
                    HalloweenTownProgression.SetResourceReference(ContentProperty, "Vexen");
                    data.WorldProgress[world.worldName] = 7;
                }
            }
            else if (world.worldName == "PortRoyal")
            {
                if (world.roomNumber == 0 && world.eventID1 == 0 && data.WorldProgress[world.worldName] <= 0) // rampart
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "PRChests");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "PRChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 2 && world.eventID1 == 55 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Town finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "Town");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "Town");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 60 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Barbossa finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "Barbossa");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "Barbossa");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 62 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Gambler finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "Gambler");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "Gambler");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 1 && world.eventID1 == 54 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Grim Reaper finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "GrimReaper");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "GrimReaper");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 101 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Data Luxord finish
                {
                    broadcast.PortRoyalProgression.SetResourceReference(ContentProperty, "DataLuxord");
                    PortRoyalProgression.SetResourceReference(ContentProperty, "DataLuxord");
                    data.WorldProgress[world.worldName] = 6;
                }
            }
            else if (world.worldName == "SpaceParanoids")
            {
                if (world.roomNumber == 0 && world.eventID2 == 1 && world.eventID3 == 2 && data.WorldProgress[world.worldName] <= 0) // Door
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "SPChests");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "SPChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 3 && world.eventID1 == 54 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Screens finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "Screens");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "Screens");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 4 && world.eventID1 == 55 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Hostile Program finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "HostileProgram");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "HostileProgram");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 7 && world.eventID1 == 57 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Solar Sailer finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "SolarSailer");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "SolarSailer");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 9 && world.eventID1 == 59 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // MCP finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "MCP");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "MCP");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 33 && world.eventID1 == 143 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Larxene finish
                {
                    broadcast.SpaceParanoidsProgression.SetResourceReference(ContentProperty, "Larxene");
                    SpaceParanoidsProgression.SetResourceReference(ContentProperty, "Larxene");
                    data.WorldProgress[world.worldName] = 6;
                }
            }
            else if (world.worldName == "TWTNW")
            {
                if (world.roomNumber == 1 && world.eventID1 == 3 && world.eventID3 == 22 && data.WorldProgress[world.worldName] <= 0) // Alley to Between
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "TWTNWChests");
                    TWTNWProgression.SetResourceReference(ContentProperty, "TWTNWChests");
                    data.WorldProgress[world.worldName] = 1;
                }
                else if (world.roomNumber == 21 && world.eventID1 == 65 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 1) // Roxas finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "Roxas");
                    TWTNWProgression.SetResourceReference(ContentProperty, "Roxas");
                    data.WorldProgress[world.worldName] = 2;
                }
                else if (world.roomNumber == 10 && world.eventID1 == 57 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 2) // Xigbar finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "Xigbar");
                    TWTNWProgression.SetResourceReference(ContentProperty, "Xigbar");
                    data.WorldProgress[world.worldName] = 3;
                }
                else if (world.roomNumber == 14 && world.eventID1 == 58 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 3) // Luxord finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "Luxord");
                    TWTNWProgression.SetResourceReference(ContentProperty, "Luxord");
                    data.WorldProgress[world.worldName] = 4;
                }
                else if (world.roomNumber == 15 && world.eventID1 == 56 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 4) // Saix finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "Saix");
                    TWTNWProgression.SetResourceReference(ContentProperty, "Saix");
                    data.WorldProgress[world.worldName] = 5;
                }
                else if (world.roomNumber == 19 && world.eventID1 == 59 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 5) // Xemnas 1 finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "Xemnas1");
                    TWTNWProgression.SetResourceReference(ContentProperty, "Xemnas1");
                    data.WorldProgress[world.worldName] = 6;
                }
                else if (world.roomNumber == 20 && world.eventID1 == 98 && world.eventComplete == 1 && data.WorldProgress[world.worldName] <= 6) // Data Xemnas finish
                {
                    broadcast.TWTNWProgression.SetResourceReference(ContentProperty, "DataXemnas");
                    TWTNWProgression.SetResourceReference(ContentProperty, "DataXemnas");
                    data.WorldProgress[world.worldName] = 7;
                }
            }
        }

        private string BytesToHex(byte[] bytes)
        {
            if (Enumerable.SequenceEqual(bytes, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                return "Service not started. Waiting for PCSX2";
            }
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        
        private void BindStats(Image img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new NumberConverter();
            img.SetBinding(Image.SourceProperty, binding);
        }

        private void BindLevel(Image img, string property, object source)
        {
            Binding binding = new Binding(property);
            binding.Source = source;
            binding.Converter = new LevelConverter();
            img.SetBinding(Image.SourceProperty, binding);
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

        private void BindAbility(Image img, string property, object source)
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
    }
}