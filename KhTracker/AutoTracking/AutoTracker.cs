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

            //importantChecks.Add(valor = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 1, 0x0032EE26, "Valor"));
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

            world = new World(memory, ADDRESS_OFFSET, 0x0032BAE0);
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

            SetBindings();
            SetTimer();
            OnTimedEvent(null, null);
        }

        private void findAddressOffset()
        {
            bool found = false;
            Int32 offset = 0x00000000;
            Int32 testAddr = 0x0010001C;
            string good = "280C";
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

            BindAbilityLevel(broadcast.HighJumpLevel, "Level", highJump, new HighJumpConverter());
            BindAbilityLevel(broadcast.QuickRunLevel, "Level", quickRun, new QuickRunConverter());
            BindAbilityLevel(broadcast.DodgeRollLevel, "Level", dodgeRoll, new DodgeRollConverter());
            BindAbilityLevel(broadcast.AerialDodgeLevel, "Level", aerialDodge, new AerialDodgeConverter());
            BindAbilityLevel(broadcast.GlideLevel, "Level", glide, new GlideConverter());
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
            previousChecks = newChecks;
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
                if (item.Name.Contains(itemName))
                {
                    if (world.Handle_Report(item as Item, this, data))
                        world.Add_Item(item as Item, this);

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
            if (world.world == "SimulatedTwilightTown")
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
                    // skip auto tracking final if it was forced
                    if (check.Name == "Final" && stats.form == 5)
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
            if (newChecks.Count > 0)
            {
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
                    }
                    else if (driveRewards.Exists(x => x == check.Name))
                    {
                        // add check to drives
                        TrackItem(check.Name + count, DriveFormsGrid);
                    }
                    else
                    {
                        // add check to current world
                        foreach (WorldGrid grid in data.Grids)
                        {
                            if (world.world == grid.Name.Substring(0, grid.Name.Length - 4))
                            {
                                TrackItem(check.Name + count, grid);
                            }
                        }
                    }
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

        public string GetWorld()
        {
            return world.world;
        }
    }
}