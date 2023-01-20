using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace KhTracker
{
    public partial class MainWindow
    {
        private void ShanHints(Dictionary<string, object> hintObject)
        {
            data.ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());

            //Joke Hints test
            bool debug = false;
            if (debug)
            {
                var random = new Random();
                List<int> usedvalues = new List<int>();
                for (int i = 1; i <= 13; i++) //do this for each of the 13 reports
                {
                    //get random number
                    int index = random.Next(JokeHints.Count);

                    //should never happen, but might as well make a failsafe
                    if (usedvalues.Count == JokeHints.Count)
                        usedvalues.Clear();

                    //prevent the same hint appearing in multiple reports
                    while (usedvalues.Contains(index))
                        index = random.Next(JokeHints.Count);

                    //add joke hint to report
                    string joke = JokeHints[index];
                    data.reportInformation.Add(new Tuple<string, string, int>(joke, joke, -99)); //-99 is used to define the report as a joke
                    data.reportLocations.Add("Joke"); //location "Joke" used so that the tracker doesn't actually care where the hint is placed (doesn't matter for shan hints)

                    usedvalues.Add(index);
                }

                //start adding score data
                if (data.ScoreMode)
                    ScoreModifier(hintObject);

                //turn reports back on
                ReportsToggle(true);
                data.hintsLoaded = true;
            }

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    data.WorldsData[Codes.ConvertSeedGenName(world.Key)].checkCount.Add(Codes.ConvertSeedGenName(item));
                }
                //for progression hints
                data.reportInformation.Add(new Tuple<string, string, int>(world.Key, null, 0));
            }

            if (!data.UsingProgressionHints)
            {
                foreach (var key in data.WorldsData.Keys.ToList())
                {
                    if (key == "GoA")
                        continue;

                    data.WorldsData[key].worldGrid.WorldComplete();
                    SetWorldValue(data.WorldsData[key].value, 0);
                }
            }

            SetProgressionHints(data.UsingProgressionHints);
        }

        private void JsmarteeHints(Dictionary<string, object> hintObject)
        {
            if (data.UsingProgressionHints)
            {
                ProgressionJsmarteeHints(hintObject);
                return;
            }

            data.ShouldResetHash = true;
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            foreach (var report in reportKeys)
            {
                var world = Codes.ConvertSeedGenName(reports[report.ToString()]["World"].ToString());
                var count = reports[report.ToString()]["Count"].ToString();
                var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());
                data.reportInformation.Add(new Tuple<string, string, int>(null, world, int.Parse(count)));
                data.reportLocations.Add(location);
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            ReportsToggle(true);
            data.hintsLoaded = true;
        }

        private void ProgressionJsmarteeHints(Dictionary<string, object> hintObject)
        {
            data.ShouldResetHash = true;
            var progHints = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> progHintsKeys = progHints.Keys.Select(int.Parse).ToList();
            progHintsKeys.Sort();

            if (data.UsingProgressionHints) //clear the reveal order since it assumes shans/points
                data.HintRevealOrder.Clear();

            //int i = 0;
            foreach (var hint in progHintsKeys)
            {
                var world = Codes.ConvertSeedGenName(progHints[hint.ToString()]["World"].ToString());
                var count = progHints[hint.ToString()]["Count"].ToString();
                var location = Codes.ConvertSeedGenName(progHints[hint.ToString()]["Location"].ToString());
                data.reportInformation.Add(new Tuple<string, string, int>(null, world, int.Parse(count)));
                data.reportLocations.Add(location);

                //Console.WriteLine("WORLD | LOCATION = " + world + " | " + location);

                //data.HintRevealOrder.Add(world);

                //data.worldStoredHintCount[Codes.WorldNameToInt[data.reportLocations[i]]]++; //counts how many reports are in a world
                //data.worldStoredOrigCount[Codes.WorldNameToInt[world]] = int.Parse(count); //tracks the original check count of a world, used later for original - stored
                //data.worldHintNumber[Codes.WorldNameToInt[world]] = i + 1; //tracks which world contains which report - used for OnMouseDown
                //data.worldReportPairs.Add(world, i);
                //i++;
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            ReportsToggle(true);
            data.hintsLoaded = true;

            //Console.WriteLine("data.worldReportPairs = " + data.worldReportPairs["HollowBastion"]);

            SetProgressionHints(data.UsingProgressionHints);
        }

        private void PathHints(Dictionary<string, object> hintObject)
        {
            if (data.UsingProgressionHints)
            {
                ProgressionPathHints(hintObject);
                return;
            }

            data.ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    data.WorldsData[Codes.ConvertSeedGenName(world.Key)].checkCount.Add(Codes.ConvertSeedGenName(item));
                }

            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            foreach (int report in reportKeys)
            {
                var hinttext = reports[report.ToString()]["Text"].ToString();
                int hintproofs = 0;
                var hintworld = Codes.ConvertSeedGenName(reports[report.ToString()]["HintedWorld"].ToString());
                var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());

                //turn proof names to value. con = 1 | non = 10 | peace = 100
                List<string> hintprooflist = new List<string>(JsonSerializer.Deserialize<List<string>>(reports[report.ToString()]["ProofPath"].ToString()));
                foreach (string proof in hintprooflist)
                {
                    switch (proof)
                    {
                        case "Connection":
                            hintproofs += 1;
                            break;
                        case "Nonexistence":
                            hintproofs += 10;
                            break;
                        case "Peace":
                            hintproofs += 100;
                            break;
                    }
                }

                data.reportInformation.Add(new Tuple<string, string, int>(hinttext, hintworld, hintproofs));
                data.reportLocations.Add(location);
            }

            //set pathproof defaults
            foreach (string key in data.WorldsData.Keys.ToList())
            {
                //adjust grid sizes for path proof icons
                data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(0.1, GridUnitType.Star);

                //get grid for path proof collumn and set visibility
                Grid pathgrid = data.WorldsData[key].top.FindName(key + "Path") as Grid;
                pathgrid.Visibility = Visibility.Visible; //main grid
                foreach (Image child in pathgrid.Children)
                {
                    child.Visibility = Visibility.Hidden; //each icon hidden by default
                }
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            ReportsToggle(true);
            data.hintsLoaded = true;
        }

        private void ProgressionPathHints(Dictionary<string, object> hintObject)
        {
            data.ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());
            var progHints = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> progHintsKeys = progHints.Keys.Select(int.Parse).ToList();
            progHintsKeys.Sort();

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    data.WorldsData[Codes.ConvertSeedGenName(world.Key)].checkCount.Add(Codes.ConvertSeedGenName(item));
                }
            }

            //done here for timing
            SetProgressionHints(data.UsingProgressionHints);

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            //locally track the worlds that are empty and contain the "has nothing, sorry" text
            //then place these at the end of the hint list
            List<Tuple<string, string, int>> tempReportInformation = new List<Tuple<string, string, int>>();
            List<string> tempReportLocations = new List<string>();
            foreach (int hint in progHintsKeys)
            {
                var hinttext = progHints[hint.ToString()]["Text"].ToString();
                int hintproofs = 0;
                var hintworld = Codes.ConvertSeedGenName(progHints[hint.ToString()]["HintedWorld"].ToString());
                var location = Codes.ConvertSeedGenName(progHints[hint.ToString()]["Location"].ToString());

                //turn proof names to value. con = 1 | non = 10 | peace = 100
                List<string> hintprooflist = new List<string>(JsonSerializer.Deserialize<List<string>>(progHints[hint.ToString()]["ProofPath"].ToString()));
                foreach (string proof in hintprooflist)
                {
                    switch (proof)
                    {
                        case "Connection":
                            hintproofs += 1;
                            break;
                        case "Nonexistence":
                            hintproofs += 10;
                            break;
                        case "Peace":
                            hintproofs += 100;
                            break;
                    }
                }

                if (hinttext.Contains("has nothing, sorry"))
                    tempReportInformation.Add(new Tuple<string, string, int>(hinttext, hintworld, hintproofs));
                else
                    data.reportInformation.Add(new Tuple<string, string, int>(hinttext, hintworld, hintproofs));
                data.reportLocations.Add(location);
            }

            if (tempReportInformation.Count > 0)
            {
                foreach (var loc in tempReportInformation)
                    data.reportInformation.Add(loc);
                foreach (var loc in tempReportLocations)
                    data.reportLocations.Add(loc);
            }

            //set pathproof defaults
            foreach (string key in data.WorldsData.Keys.ToList())
            {
                //adjust grid sizes for path proof icons
                data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(0.1, GridUnitType.Star);

                //get grid for path proof collumn and set visibility
                Grid pathgrid = data.WorldsData[key].top.FindName(key + "Path") as Grid;
                pathgrid.Visibility = Visibility.Visible; //main grid
                foreach (Image child in pathgrid.Children)
                {
                    child.Visibility = Visibility.Hidden; //each icon hidden by default
                }
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            ReportsToggle(true);
            data.hintsLoaded = true;
        }

        private void SpoilerHints(Dictionary<string, object> hintObject)
        {
            if (data.UsingProgressionHints)
            {
                ProgressionSpoilerHints(hintObject);
                return;
            }

            bool TMP_bossReports = false;
            data.ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());
            List<string> reveals = new List<string>(JsonSerializer.Deserialize<List<string>>(hintObject["reveal"].ToString()));
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            //set if world value should change color on completion
            if (reveals.Contains("complete"))
            {
                data.SpoilerWorldCompletion = true;
            }

            //set if reports should reveal items or not
            if (reveals.Contains("reportmode"))
            {
                data.SpoilerReportMode = true;
                ReportsToggle(true);
            }

            //reports reveal bosses
            if(reveals.Contains("bossreports"))
            {
                ReportsToggle(true);
                TMP_bossReports = true;
            }

            if (!reveals.Contains("reportmode") && !reveals.Contains("bossreports"))
                ReportsToggle(false);

            Dictionary<string, int> counts = new Dictionary<string, int>
            {
                {"Fire", 1 }, {"Blizzard", 1 }, {"Thunder", 1 },
                {"Cure", 1 }, {"Magnet", 1 }, {"Reflect", 1},
                {"TornPage", 1}, {"MunnyPouch", 1},
            };

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (string item in world.Value)
                {
                    //Ignore reports as ICs if report mode is false
                    //if (!data.SpoilerReportMode && item.Contains("Report"))
                    //    continue;

                    if (item.Contains("Report") && !data.SpoilerReportMode && !TMP_bossReports)
                        continue;

                    string worldname = Codes.ConvertSeedGenName(world.Key);
                    string checkname = Codes.ConvertSeedGenName(item);

                    data.WorldsData[worldname].checkCount.Add(checkname);

                    //add ghosts if report mode is off
                    if (!data.SpoilerReportMode)
                    {
                        //Skip adding ghosts for item types that aren't in reveals list 
                        if (!reveals.Contains(Codes.FindItemType(item)))
                        {
                            continue;
                        }

                        WorldGrid grid = data.WorldsData[worldname].worldGrid;
                        if (counts.Keys.ToList().Contains(checkname))
                        {
                            grid.Add_Ghost(data.GhostItems["Ghost_" + checkname + counts[checkname]]);
                            counts[checkname] += 1;
                        }
                        else
                        {
                            grid.Add_Ghost(data.GhostItems["Ghost_" + checkname]);
                        }
                    }
                }
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                if (data.SpoilerWorldCompletion)
                    data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            //add setup report info if report mode is on
            if (data.SpoilerReportMode)
            {
                data.SpoilerRevealTypes.AddRange(reveals);

                foreach (var report in reportKeys)
                {
                    string worldstring = reports[report.ToString()]["World"].ToString();
                    int dummyvalue = 0;
                    if (worldstring.StartsWith("Nothing_"))
                    {
                        worldstring = worldstring.Remove(0, 8);
                        dummyvalue = -1;
                    }

                    var worldhint = Codes.ConvertSeedGenName(worldstring);
                    var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());

                    data.reportInformation.Add(new Tuple<string, string, int>(worldhint, null, dummyvalue));
                    data.reportLocations.Add(location);
                }
                data.hintsLoaded = true;
            }
            else if (data.BossRandoFound && TMP_bossReports)
            {
                //get random based on seed hash
                Random rand = new Random(data.BossRandoSeed);
                
                //setup lists
                List<string> keyList = new List<string>(data.BossList.Keys);

                //Remove bosses for worlds not enabled and remove "duplicates"
                foreach (var key in data.BossList.Keys)
                {
                    //remove duplicates
                    if (Codes.bossDups.Contains(key))
                    {
                        keyList.Remove(key);
                        continue;
                    }
                        
                    if (!data.enabledWorlds.Contains(Codes.bossLocations[key]))
                        keyList.Remove(key);
                    else if (key.Contains("(Data)"))
                    {
                        //special case for some datas. we normally don't want
                        //to hint datas unless the world the normally are in is off
                        // (only applies for datas where the data fight is in a different world)
                        switch(key)
                        {
                            case "Axel (Data)":
                                if (data.enabledWorlds.Contains("STT"))
                                    keyList.Remove(key);
                            break;
                            case "Luxord (Data)":
                            case "Roxas (Data)":
                            case "Xigbar (Data)":
                                if (data.enabledWorlds.Contains("TWTNW"))
                                    keyList.Remove(key);
                                break;
                            default:
                                keyList.Remove(key);
                                break;
                        }
                    }
                }

                //get report info
                foreach (var report in reportKeys)
                {
                    //get a boss
                    string boss = keyList[rand.Next(0, keyList.Count)];
                    //get boss types
                    string origType = Codes.FindBossType(boss);
                    string replaceType = Codes.FindBossType(data.BossList[boss]);

                    //prioritize special arenas and bosses (50%?)
                    while (origType == "boss_other" && replaceType == "boss_other")
                    {
                        int reroll = rand.Next(1, 10);
                        if (reroll > 5) //50% chance to keep basic bosses
                        {
                            break;
                        }

                        boss = keyList[rand.Next(0, keyList.Count)];
                        origType = Codes.FindBossType(boss);
                        replaceType = Codes.FindBossType(data.BossList[boss]);
                    }

                    //report location and final hint string
                    string worldhint;

                    if (boss == data.BossList[boss])
                    {
                        string tmp_origBoss = boss;
                        if (tmp_origBoss == "Hades II (1)" || tmp_origBoss == "Hades II" || tmp_origBoss == "Hades I")
                        {
                            tmp_origBoss = "Hades";
                        }
                        if (tmp_origBoss == "Pete OC II")
                        {
                            tmp_origBoss = "Pete OC";
                        }

                        worldhint = tmp_origBoss + " is unchanged";
                    }
                    else 
                    {
                        string tmp_origBoss = boss;
                        string tmp_replBoss = data.BossList[boss];

                        if (tmp_origBoss == "Hades II (1)" || tmp_origBoss == "Hades II" || tmp_origBoss == "Hades I")
                        {
                            tmp_origBoss = "Hades";
                        }
                        if (tmp_origBoss == "Pete OC II")
                        {
                            tmp_origBoss = "Pete OC";
                        }

                        if (tmp_replBoss == "Hades II (1)" || tmp_replBoss == "Hades II" || tmp_replBoss == "Hades I")
                        {
                            tmp_replBoss = "Hades";
                        }
                        if (tmp_replBoss == "Pete OC II")
                        {
                            tmp_replBoss = "Pete OC";
                        }

                        worldhint = tmp_origBoss + " became " + tmp_replBoss;
                    }
                    
                    int dummyvalue = -12345; //use this for boss reports i guess
                    data.reportInformation.Add(new Tuple<string, string, int>(worldhint, null, dummyvalue));
                    var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());
                    data.reportLocations.Add(location);

                    keyList.Remove(boss);
                }
                
                data.hintsLoaded = true;
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            SetProgressionHints(data.UsingProgressionHints);
        }

        private void ProgressionSpoilerHints(Dictionary<string, object> hintObject)
        {
            bool TMP_bossReports = false;
            data.ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());
            List<string> reveals = new List<string>(JsonSerializer.Deserialize<List<string>>(hintObject["reveal"].ToString()));
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            //set if world value should change color on completion
            if (reveals.Contains("complete"))
            {
                data.SpoilerWorldCompletion = true;
            }

            //set if reports should reveal items or not
            if (reveals.Contains("reportmode"))
            {
                data.SpoilerReportMode = true;
                ReportsToggle(true);
            }

            //reports reveal bosses
            if (reveals.Contains("bossreports"))
            {
                ReportsToggle(true);
                TMP_bossReports = true;
            }

            if (!reveals.Contains("reportmode") && !reveals.Contains("bossreports"))
                ReportsToggle(false);

            Dictionary<string, int> counts = new Dictionary<string, int>
            {
                {"Fire", 1 }, {"Blizzard", 1 }, {"Thunder", 1 },
                {"Cure", 1 }, {"Magnet", 1 }, {"Reflect", 1},
                {"TornPage", 1}, {"MunnyPouch", 1},
            };

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (string item in world.Value)
                {
                    //Ignore reports as ICs if report mode is false
                    //if (!data.SpoilerReportMode && item.Contains("Report"))
                    //    continue;

                    if (item.Contains("Report") && !data.SpoilerReportMode && !TMP_bossReports)
                        continue;

                    string worldname = Codes.ConvertSeedGenName(world.Key);
                    string checkname = Codes.ConvertSeedGenName(item);

                    data.WorldsData[worldname].checkCount.Add(checkname);

                    //add ghosts if report mode is off
                    if (!data.SpoilerReportMode)
                    {
                        //Skip adding ghosts for item types that aren't in reveals list 
                        if (!reveals.Contains(Codes.FindItemType(item)))
                        {
                            continue;
                        }

                        WorldGrid grid = data.WorldsData[worldname].worldGrid;
                        if (counts.Keys.ToList().Contains(checkname))
                        {
                            grid.Add_Ghost(data.GhostItems["Ghost_" + checkname + counts[checkname]]);
                            counts[checkname] += 1;
                        }
                        else
                        {
                            grid.Add_Ghost(data.GhostItems["Ghost_" + checkname]);
                        }
                    }
                }
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                if (data.SpoilerWorldCompletion)
                    data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }

            //add setup report info if report mode is on
            if (data.SpoilerReportMode)
            {
                data.SpoilerRevealTypes.AddRange(reveals);

                foreach (var report in reportKeys)
                {
                    string worldstring = reports[report.ToString()]["World"].ToString();
                    int dummyvalue = 0;
                    if (worldstring.StartsWith("Nothing_"))
                    {
                        worldstring = worldstring.Remove(0, 8);
                        dummyvalue = -1;
                    }

                    var worldhint = Codes.ConvertSeedGenName(worldstring);
                    var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());


                    data.reportInformation.Add(new Tuple<string, string, int>(worldhint, null, dummyvalue));
                    data.reportLocations.Add(location);

                    Console.WriteLine("WORLDSTRING = " + worldstring);
                    Console.WriteLine("LOCATION = " + location);
                    Console.WriteLine(data.reportInformation.Count);
                }
                data.hintsLoaded = true;
            }
            else if (data.BossRandoFound && TMP_bossReports)
            {
                //get random based on seed hash
                Random rand = new Random(data.BossRandoSeed);

                //setup lists
                List<string> keyList = new List<string>(data.BossList.Keys);

                //Remove bosses for worlds not enabled and remove "duplicates"
                foreach (var key in data.BossList.Keys)
                {
                    if (!data.enabledWorlds.Contains(Codes.bossLocations[key]))
                        keyList.Remove(key);
                    else if (key.Contains("Cups"))
                        keyList.Remove(key);
                    else if (key == "Hades II")
                        keyList.Remove(key);
                    else if (key.Contains("(Data)"))
                    {
                        //special case for some datas. we normally don't want
                        //to hint datas unless the world the normally are in is off
                        // (only applies for datas where the data fight is in a different world)
                        switch (key)
                        {
                            case "Axel (Data)":
                                if (data.enabledWorlds.Contains("STT"))
                                    keyList.Remove(key);
                                break;
                            case "Luxord (Data)":
                            case "Roxas (Data)":
                            case "Xigbar (Data)":
                                if (data.enabledWorlds.Contains("TWTNW"))
                                    keyList.Remove(key);
                                break;
                            default:
                                keyList.Remove(key);
                                break;
                        }
                    }
                }

                //get report info
                foreach (var report in reportKeys)
                {
                    //get a boss
                    string boss = keyList[rand.Next(0, keyList.Count)];
                    //get boss types
                    string origType = Codes.FindBossType(boss);
                    string replaceType = Codes.FindBossType(data.BossList[boss]);

                    //prioritize special arenas and bosses (50%?)
                    while (origType == "boss_other" && replaceType == "boss_other")
                    {
                        int reroll = rand.Next(1, 10);
                        if (reroll > 5) //50% chance to keep basic bosses
                        {
                            break;
                        }

                        boss = keyList[rand.Next(0, keyList.Count)];
                        origType = Codes.FindBossType(boss);
                        replaceType = Codes.FindBossType(data.BossList[boss]);
                    }

                    //report location and final hint string
                    string worldhint;

                    if (boss == data.BossList[boss])
                    {
                        string tmp_origBoss = boss;
                        if (tmp_origBoss == "Hades II (1)")
                        {
                            tmp_origBoss = "Hades";
                        }
                        if (tmp_origBoss == "Pete OC II")
                        {
                            tmp_origBoss = "Pete";
                        }

                        worldhint = tmp_origBoss + " is unchanged";
                    }
                    else
                    {
                        string tmp_origBoss = boss;
                        string tmp_replBoss = data.BossList[boss];

                        if (tmp_origBoss == "Hades II (1)")
                        {
                            tmp_origBoss = "Hades";
                        }
                        if (tmp_origBoss == "Pete OC II")
                        {
                            tmp_origBoss = "Pete";
                        }

                        if (tmp_replBoss == "Hades II (1)")
                        {
                            tmp_replBoss = "Hades";
                        }
                        if (tmp_replBoss == "Pete OC II")
                        {
                            tmp_replBoss = "Pete";
                        }

                        worldhint = tmp_origBoss + " became " + tmp_replBoss;
                    }

                    int dummyvalue = -12345; //use this for boss reports i guess
                    data.reportInformation.Add(new Tuple<string, string, int>(worldhint, null, dummyvalue));
                    var location = Codes.ConvertSeedGenName(reports[report.ToString()]["Location"].ToString());
                    data.reportLocations.Add(location);

                    keyList.Remove(boss);
                }

                data.hintsLoaded = true;
            }

            //start adding score data
            if (data.ScoreMode)
                ScoreModifier(hintObject);

            SetProgressionHints(data.UsingProgressionHints);
        }

        private void ScoreModifier(Dictionary<string, object> hintObject)
        {
            var points = JsonSerializer.Deserialize<Dictionary<string, int>>(hintObject["checkValue"].ToString());

            //set point values
            foreach (var point in points)
            {
                if (data.PointsDatanew.Keys.Contains(point.Key))
                {
                    data.PointsDatanew[point.Key] = point.Value;
                }
                else
                {
                    Console.WriteLine($"Something went wrong in setting point values. Unknown Key: {point.Key}");
                }
            }
        }

        /// <summary>
        /// points hints and logic
        /// </summary>

        //used to be a ton of ints
        //split into two dictionarys now as it's much easier to handle and uses far less if statements.
        private Dictionary<string, int> WorldPoints = new Dictionary<string, int>()
        {
            {"SimulatedTwilightTown", 0},
            {"TwilightTown", 0},
            {"HollowBastion", 0},
            {"LandofDragons", 0},
            {"BeastsCastle", 0},
            {"OlympusColiseum", 0},
            {"DisneyCastle", 0},
            {"PortRoyal", 0},
            {"Agrabah", 0},
            {"HalloweenTown", 0},
            {"PrideLands", 0},
            {"Atlantica", 0},
            {"HundredAcreWood", 0},
            {"SpaceParanoids", 0},
            {"TWTNW", 0},
            {"DriveForms", 0},
            {"SorasHeart", 0},
            {"PuzzSynth", 0}
        };
        private Dictionary<string, int> WorldPoints_c = new Dictionary<string, int>()
        {
            {"SimulatedTwilightTown", 0},
            {"TwilightTown", 0},
            {"HollowBastion", 0},
            {"LandofDragons", 0},
            {"BeastsCastle", 0},
            {"OlympusColiseum", 0},
            {"DisneyCastle", 0},
            {"PortRoyal", 0},
            {"Agrabah", 0},
            {"HalloweenTown", 0},
            {"PrideLands", 0},
            {"Atlantica", 0},
            {"HundredAcreWood", 0},
            {"SpaceParanoids", 0},
            {"TWTNW", 0},
            {"DriveForms", 0},
            {"SorasHeart", 0},
            {"PuzzSynth", 0}
        };

        private void PointsHints(Dictionary<string, object> hintObject)
        {
            data.ShouldResetHash = true;

            var worldsP = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());
            var reportsP = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            var points = JsonSerializer.Deserialize<Dictionary<string, int>>(hintObject["checkValue"].ToString());

            List<int> reportKeysP = reportsP.Keys.Select(int.Parse).ToList();
            reportKeysP.Sort();

            //set point values
            foreach (var point in points)
            {
                if (data.PointsDatanew.Keys.Contains(point.Key))
                {
                    data.PointsDatanew[point.Key] = point.Value;
                }
                else
                {
                    Console.WriteLine($"Something went wrong in setting point values. error: {point.Key}");
                }
            }

            //Fallback values for older seeds
            if (!points.Keys.Contains("report"))
                data.PointsDatanew["report"] = data.PointsDatanew["page"];
            if (!points.Keys.Contains("bonus"))
                data.PointsDatanew["bonus"] = 10;
            if (!points.Keys.Contains("complete"))
                data.PointsDatanew["complete"] = 10;
            if (!points.Keys.Contains("formlv"))
                data.PointsDatanew["formlv"] = 3;
            if (!points.Keys.Contains("visit"))
                data.PointsDatanew["visit"] = 1;
            if (!points.Keys.Contains("other"))
                data.PointsDatanew["other"] = data.PointsDatanew["ability"];

            if (!points.Keys.Contains("collection_proof"))
                data.PointsDatanew["collection_proof"] = 0;
            if (!points.Keys.Contains("collection_form"))
                data.PointsDatanew["collection_form"] = 0;
            if (!points.Keys.Contains("collection_magic"))
                data.PointsDatanew["collection_magic"] = data.PointsDatanew["magic"];
            if (!points.Keys.Contains("collection_summon"))
                data.PointsDatanew["collection_summon"] = 0;
            if (!points.Keys.Contains("collection_ability"))
                data.PointsDatanew["collection_ability"] = 0;
            if (!points.Keys.Contains("collection_page"))
                data.PointsDatanew["collection_page"] = data.PointsDatanew["collection_page"] * 5;
            if (!points.Keys.Contains("collection_report"))
                data.PointsDatanew["collection_report"] = 0;
            if (!points.Keys.Contains("collection_pouches"))
                data.PointsDatanew["collection_pouches"] = data.PointsDatanew["other"];
            if (!points.Keys.Contains("collection_visit"))
                data.PointsDatanew["collection_visit"] = 0;



            //get point totals for each world
            foreach (var world in worldsP)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    
                    string itemName = item;
                    string itemType = Codes.FindItemType(item);

                    //Zero point items are not ICs
                    if (data.PointsDatanew[itemType] == 0)
                        continue;

                    data.WorldsData[Codes.ConvertSeedGenName(world.Key)].checkCount.Add(Codes.ConvertSeedGenName(item));

                    //string itemType = Codes.FindItemType(item);
                    if (data.PointsDatanew.Keys.Contains(itemType))
                    {
                        WorldPoints[Codes.ConvertSeedGenName(world.Key)] += data.PointsDatanew[itemType];
                    }
                    else
                    {
                        Console.WriteLine($"Something went wrong in getting world points. error: {itemType}");
                    }
                }
            }

            //set points for each world
            if (!data.UsingProgressionHints)
            {
                foreach (var key in data.WorldsData.Keys.ToList())
                {
                    if (key == "GoA")
                        continue;

                    data.WorldsData[key].worldGrid.WorldComplete();

                    if (WorldPoints.Keys.Contains(key))
                    {
                        SetWorldValue(data.WorldsData[key].value, WorldPoints[key]);
                    }
                    else
                    {
                        Console.WriteLine($"Something went wrong in setting world point numbers. error: {key}");
                    }
                }
            }

            //set hints for each report
            foreach (var reportP in reportKeysP)
            {
                var worldP = Codes.ConvertSeedGenName(reportsP[reportP.ToString()]["World"].ToString());
                var checkP = reportsP[reportP.ToString()]["check"].ToString();
                var locationP = Codes.ConvertSeedGenName(reportsP[reportP.ToString()]["Location"].ToString());

                data.reportInformation.Add(new Tuple<string, string, int>(worldP, checkP, 0));
                data.reportLocations.Add(locationP);
            }

            ReportsToggle(true);
            data.hintsLoaded = true;
            WorldPoints_c = WorldPoints;
            SetProgressionHints(data.UsingProgressionHints);
        }

        public int GetPoints(string worldName)
        {
            if (WorldPoints_c.Keys.Contains(worldName))
            {
                return WorldPoints_c[worldName];
            }
            else
            {
                return 0;
            }
        }

        public void SetPoints(string name, int value)
        {
            if (WorldPoints_c.Keys.Contains(name))
            {
                WorldPoints_c[name] = value;
            }
        }

        public void UpdatePointScore(int points)
        {
            if (data.mode != Mode.DAHints && !data.ScoreMode)
                return;

            int WorldBlue = 0;
            int num = PointTotal + points; //get new point total
            PointTotal = num; //set new point total

            //adjust point score based on bonus and form levels
            //do this after setting new PointTotal value to avoid score
            //increasing forever when adding/removing items
            if (aTimer != null)
            {
                int BonusTotal = stats.BonusLevel * data.PointsDatanew["bonus"];
                int Valorlv = (valor.VisualLevel - 1) * data.PointsDatanew["formlv"];
                int Wisdomlv = (wisdom.VisualLevel - 1) * data.PointsDatanew["formlv"];
                int Limitlv = (limit.VisualLevel - 1) * data.PointsDatanew["formlv"];
                int Masterlv = (master.VisualLevel - 1) * data.PointsDatanew["formlv"];
                int Finallv = (final.VisualLevel - 1) * data.PointsDatanew["formlv"];
                int Deaths = DeathCounter * data.PointsDatanew["deaths"];

                num += BonusTotal + Valorlv + Wisdomlv + Limitlv + Masterlv + Finallv + Deaths;
            }

            //add bonus points for completeing a world
            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                if (data.WorldsData[key].complete && data.WorldsData[key].checkCount.Count != 0)
                    WorldBlue += data.PointsDatanew["complete"];
            }
            num += WorldBlue;

            //add bonus points for collecting all multis in a set
            if (data.PointsDatanew["collection_magic"] > 0)
            {
                if (WorldGrid.Real_Fire == 3)
                    num += data.PointsDatanew["collection_magic"];
                if (WorldGrid.Real_Blizzard == 3)
                    num += data.PointsDatanew["collection_magic"];
                if (WorldGrid.Real_Thunder == 3)
                    num += data.PointsDatanew["collection_magic"];
                if (WorldGrid.Real_Cure == 3)
                    num += data.PointsDatanew["collection_magic"];
                if (WorldGrid.Real_Magnet == 3)
                    num += data.PointsDatanew["collection_magic"];
                if (WorldGrid.Real_Reflect == 3)
                    num += data.PointsDatanew["collection_magic"];
            }
            if (data.PointsDatanew["collection_page"] > 0)
            {
                if (WorldGrid.Real_Pages == 5)
                    num += data.PointsDatanew["collection_page"];
            }
            if (data.PointsDatanew["collection_pouches"] > 0)
            {
                if (WorldGrid.Real_Pouches == 2)
                    num += data.PointsDatanew["collection_pouches"];
            }
            if (data.PointsDatanew["collection_form"] > 0)
            {
                if (AntiFormOption.IsChecked)
                {
                    if (WorldGrid.Form_Count == 6)
                        num += data.PointsDatanew["collection_form"];
                }
                else
                {
                    if (WorldGrid.Form_Count == 5)
                        num += data.PointsDatanew["collection_form"];
                }
            }
            if (data.PointsDatanew["collection_proof"] > 0)
            {
                if (PromiseCharmOption.IsChecked)
                {
                    if (WorldGrid.Proof_Count == 4)
                        num += data.PointsDatanew["collection_proof"];
                }
                else
                {
                    if (WorldGrid.Proof_Count == 3)
                        num += data.PointsDatanew["collection_proof"];
                }
            }
            if (data.PointsDatanew["collection_summon"] > 0)
            {
                if (WorldGrid.Summon_Count == 4)
                    num += data.PointsDatanew["collection_summon"];
            }
            if (data.PointsDatanew["collection_ability"] > 0)
            {
                if (WorldGrid.Ability_Count == 2)
                    num += data.PointsDatanew["collection_ability"];
            }
            if (data.PointsDatanew["collection_visit"] > 0)
            {
                if (WorldGrid.Visit_Count == 11)
                    num += data.PointsDatanew["collection_visit"];
            }
            if (data.PointsDatanew["collection_report"] > 0)
            {
                if (WorldGrid.Report_Count == 13)
                    num += data.PointsDatanew["collection_report"];
            }


            ScoreValue.Text = num.ToString();
        }

        //static public int WorldNameToIndex(string worldName)
        //{
        //    switch (worldName)
        //    {
        //        case "SorasHeart":
        //            return 0;
        //        case "DriveForms":
        //            return 1;
        //        case "SimulatedTwilightTown":
        //            return 2;
        //        case "TwilightTown":
        //            return 3;
        //        case "HollowBastion":
        //            return 4;
        //        case "BeastsCastle":
        //            return 5;
        //        case "OlympusColiseum":
        //            return 6;
        //        case "Agrabah":
        //            return 7;
        //        case "LandofDragons":
        //            return 8;
        //        case "HundredAcreWood":
        //            return 9;
        //        case "PrideLands":
        //            return 10;
        //        case "DisneyCastle":
        //            return 11;
        //        case "HalloweenTown":
        //            return 12;
        //        case "PortRoyal":
        //            return 13;
        //        case "SpaceParanoids":
        //            return 14;
        //        case "TWTNW":
        //            return 15;
        //        case "Atlantica":
        //            return 17;
        //        case "PuzzSynth":
        //            return 18;
        //        default: //GoA
        //            return 16;
        //    }
        //}

        public List<string> JokeHints = new List<string>
        {
            "\"Call my shorty Zemnas the way she give me dome\" -Raisin",
            "Have you tried contacting Tech Support Nomura?",
            "Soul Eater is a Keyblade",
            "Soul Eater is not a Keyblade",
            "This report was is in the chest you just opened",
            "I heard Xemnas is already half Xehanort",
            "Bad luck and misfortune will infest your pathetic soul for all eternity",
            "Have you tried freezing Demyx's bubbles?",
            "Phil Cup hoards Pumpkinhead",
            "Jungle Slider 50 Fruits has the Promise Charm",
            "Put me back, please",
            "They put bugs in Riku!",
            "The knowledge, it fills me. It is neat.",
            "Doubleflight is locked by winning Fruitball",
            "Barbossa but with a squid face is holding Proof of Fantasies",
            "Grinding 5000 munny in STT will reward you with 5000 munny",
            "Two Cycling the Wardrobe Push will reward you with nothing",
            "Have you considered enabling Dodge Slash?",
            "Xehanort is a meany head",
            "Computer Password: Sea Salt Ice Cream",
            "A talking rat king showed up today and ate my ice cream",
            "Oui fycdat ouin desa dnyhcmydehk drec",
            "Roxas was placed into a simulation to mine bitcoin",
            "Have you checked the third song of Atlantica?",
            "Pull the pedestal to get the Master Form Keyblade",
            "Violence is on the Path to Peace",
            "Have you tried checking vanilla?",
            "Stop is held by Ruler of the Sky",
            "I heard that Sora can't read",
            "Tron can be synthesized using two Gales and a Dark Matter",
            "Steal Piglet's belongings before saving them",
            "One of the Seven Seeing-stones can be found in DiZ's basement",
            "Use the lock-on button to find the old lady's cat",
            "The proof of owned lamers is on the 10th Seifer Struggle in TT3",
            "Dog Street is on the Way of the Hero",
            "Get up on the Hydra's back!",
            "If only you could sell this useless hint",
            "The dog in the sack in STT is Pluto's Nobody",
            "Reading Yen Sid's book rewards you with confusion",
            "Mission 3 of Asteroid Sweep is a foolish choice",
            "Defeat Hayabusa to get Fireglow",
            "Have you tried checking the world that takes place on Earth?",
            "Defeating the Pirate Ship in Phantom Storm will reward with 300 crabs",
            "Leon's real name is Smitty Werbenjägermanjensen",
            "Need exp? Grind the Bolt Towers in Minnie Escort",
            "Have you tried suplexing the Phantom Train in STT yet?",
            "Collect the 7 Chaos Emeralds to unlock the Door to Darkness",
            "Try to BLJ to cross the gap for COR skip",
            "Before you attempt Shan Yu Skip, we need to talk about parallel universes",
            "Jump, Aerial Dodge, Magnet",
            "Found Genie? Consider DNFing if you have",
            "Chicken Little can be found in Thanksgiving Town",
            "It's Oogie Boogie, they put bugs in him!",
            "Lion Dash can be found in the first room of Pride Lands",
            "Doing 1k will make you a True Warrior of the Three Kingdoms",
            "There's a free shield in the graveyard",
            "Die it's faster",
            "Piglet's grandpa's name is Trespassers Will",
            "This is Auron's hint, and you're not a part of it",
            "Be sure to have 7 ethers for the Hyenas fight",
            "ARC, Reload!"
        };

        public void SetProgressionHints(bool usingProgHints)
        {
            //if it calls here and not in progression or using outdated seed methods somehow
            if (!usingProgHints || data.mode == Mode.Hints || data.mode == Mode.AltHints)
                return;

            //Per Hint Mode Changes
            else if (data.mode == Mode.OpenKHHints)
            {
                //Need to shuffle the hints given so there is no metagaming
                //Random random = new Random(data.ProgressionHash);
                //string temp = "";
                //int tempIndex = 0;
                //for (int i = 0; i < data.HintRevealOrder.Count; i++)
                //{
                //    tempIndex = random.Next(data.HintRevealOrder.Count);
                //    temp = data.HintRevealOrder[i];
                //    data.HintRevealOrder[i] = data.HintRevealOrder[tempIndex];
                //    data.HintRevealOrder[tempIndex] = temp;
                //}

                //set progression points display
                data.ProgressionPoints = 0;
                data.ProgressionCurrentHint = 0;
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            }
            else if (data.mode == Mode.OpenKHAltHints)
            {
                // get world count from options/ data, use a hash from options / data
                Console.WriteLine("WORLDS ENABLED COUNT = " + data.WorldsEnabled + "\nPROGRESSION HASH = " + data.ProgressionHash);
                //set the seed of math.random with progressionhash
                Random random = new Random(data.ProgressionHash);
                //Console.WriteLine("RNG TEST = " + random.Next(data.WorldsEnabled));
                //shuffle list created from shananas function change
                int nextIndex = 0;
                //Tuple<string, string, int> tempTuple;
                string tempTuple;
                for (int i = 0; i < data.HintRevealOrder.Count; i++)
                {
                    nextIndex = random.Next(data.HintRevealOrder.Count);
                    tempTuple = data.HintRevealOrder[nextIndex];
                    data.HintRevealOrder[nextIndex] = data.HintRevealOrder[i];
                    data.HintRevealOrder[i] = tempTuple;
                }

                Console.WriteLine("~~~~~~~~~~~~~~~~~");
                foreach (string name in data.HintRevealOrder)
                    Console.WriteLine(name);
                Console.WriteLine("data.HintRevealOrder.count = " + data.HintRevealOrder.Count);
                Console.WriteLine("~~~~~~~~~~~~~~~~~");

                //set progression points display
                data.ProgressionPoints = 0;
                data.ProgressionCurrentHint = 0;
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            } //DONE
            else if (data.mode == Mode.DAHints) //points
            {
                //get world count from options/data, use a hash from options/data
                Console.WriteLine("WORLDS ENABLED COUNT = " + data.WorldsEnabled + "\nPROGRESSION HASH = " + data.ProgressionHash);
                //set the seed of math.random with progressionhash
                Random random = new Random(data.ProgressionHash);
                //Console.WriteLine("RNG TEST = " + random.Next(data.WorldsEnabled));
                //shuffle already created list from Options
                string temp = "";
                int tempIndex = 0;
                for (int i = 0; i < data.WorldsEnabled; i++)
                {
                    tempIndex = random.Next(data.WorldsEnabled);
                    temp = data.HintRevealOrder[i];
                    data.HintRevealOrder[i] = data.HintRevealOrder[tempIndex];
                    data.HintRevealOrder[tempIndex] = temp;
                }

                //set progression points display
                data.ProgressionPoints = 0;
                data.ProgressionCurrentHint = 0;
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            }
            else if (data.mode == Mode.PathHints)
            {
                foreach (string world in data.HintRevealOrder)
                {
                    data.WorldsData[world].hintedProgression = true;
                }

                //set progression points display
                data.ProgressionPoints = 0;
                data.ProgressionCurrentHint = 0;
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            }
            else if (data.mode == Mode.SpoilerHints)
            {
                //set progression points display
                data.ProgressionPoints = 0;
                data.ProgressionCurrentHint = 0;
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            }
        }

        public void AddProgressionPoints(int points)
        {
            if (!data.UsingProgressionHints)
                return;

            #region Debug stuff
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            //Console.WriteLine("Current Hint Cost = " + data.HintCosts[data.ProgressionCurrentHint]);
            //Console.WriteLine("Current Progression Hint = " + data.ProgressionCurrentHint);
            //Console.WriteLine("Points added = " + points);
            //Console.WriteLine("New Points at = " + (data.ProgressionPoints + points));
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            #endregion

            data.ProgressionPoints += points;
            data.TotalProgressionPoints += points;

            if (data.ProgressionCurrentHint >= data.HintCosts.Count - 1)
            {
                //update points anyway
                ProgressionCollectedValue.Text = data.TotalProgressionPoints.ToString();
                ProgressionTotalValue.Text = "X";
                return;
            }

            //loop in the event that one progression point rewards a lot
            while (data.ProgressionPoints >= data.HintCosts[data.ProgressionCurrentHint] && data.ProgressionCurrentHint < data.HintCosts.Count && data.ProgressionCurrentHint < data.WorldsEnabled)
            {
                #region More Debug
                //Console.WriteLine("Current Progression Hint = " + data.ProgressionCurrentHint);
                //Console.WriteLine("data.HintCosts.count = " + data.HintCosts.Count);
                //Console.WriteLine("PROGRESSION CURRENT HINT = " + data.ProgressionCurrentHint);
                //update points and current hint
                #endregion
                data.ProgressionPoints -= data.HintCosts[data.ProgressionCurrentHint];
                data.ProgressionCurrentHint++;

                //reveal hints/world
                ProgressionReveal(data.ProgressionCurrentHint - 1);

                if (data.ProgressionCurrentHint >= data.HintCosts.Count - 1) //revealed last hint
                    break;
            }

            if (data.ProgressionCurrentHint >= data.HintCosts.Count - 1)
            {
                //update points
                ProgressionCollectedValue.Text = data.TotalProgressionPoints.ToString();
                ProgressionTotalValue.Text = "X";
                //Console.WriteLine("Revealed last hint!");
            }
            else
            {
                //update points
                ProgressionCollectedValue.Text = data.ProgressionPoints.ToString();
                ProgressionTotalValue.Text = data.HintCosts[data.ProgressionCurrentHint].ToString();
            }
            data.WorldsData["GoA"].value.Text = data.ProgressionCurrentHint.ToString();
        }

        public void ProgressionReveal(int hintNum)
        {
            //shouldn't ever get here but break in case
            if (!data.UsingProgressionHints || data.mode == Mode.Hints || data.mode == Mode.AltHints)
                return;

            else if (data.mode == Mode.OpenKHHints) //jsmartee
            {
                string RealWorldName = data.reportInformation[hintNum].Item2;
                Console.WriteLine("Jsmartee Revealing " + RealWorldName);
                data.WorldsData[RealWorldName].hintedProgression = true;

                data.WorldsData[RealWorldName].worldGrid.ProgressionReport_Jsmartee(hintNum);
            }
            else if (data.mode == Mode.OpenKHAltHints) //shans
            {
                Console.WriteLine("data.reportInformation.count = " + data.HintRevealOrder.Count);
                Console.WriteLine("hintNum = " + hintNum);
                string RealWorldName = data.HintRevealOrder[hintNum];
                Console.WriteLine("Shananas Revealing " + RealWorldName);
                data.WorldsData[RealWorldName].hintedProgression = true;

                data.WorldsData[RealWorldName].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[RealWorldName].value, data.WorldsData[RealWorldName].worldGrid.Children.Count);

                string codesRealWorldName = Codes.GetHintTextName(RealWorldName);
                SetHintText(codesRealWorldName, "is now unhidden!", "", true, false, false);
                data.HintRevealsStored.Add(new Tuple<string, string, string, bool, bool, bool>(codesRealWorldName, "is now unhidden!", "", true, false, false));
                //Console.WriteLine("SOME CHECK COUNT THING = " + data.WorldsData[RealWorldName].worldGrid.Children.Count);
            }
            else if (data.mode == Mode.DAHints) //points
            {
                //potential problem
                string RealWorldName = data.HintRevealOrder[hintNum];
                Console.WriteLine("Points Revealing " + RealWorldName);
                data.WorldsData[RealWorldName].hintedProgression = true;

                data.WorldsData[RealWorldName].worldGrid.WorldComplete();

                if (WorldPoints.Keys.Contains(RealWorldName))
                {
                    SetWorldValue(data.WorldsData[RealWorldName].value, WorldPoints[RealWorldName]);
                }
                else
                {
                    Console.WriteLine($"Something went wrong in setting world point numbers. error: {RealWorldName}");
                }

                data.HintRevealsStored.Add(new Tuple<string, string, string, bool, bool, bool>(Codes.GetHintTextName(RealWorldName), "has been revealed!", "", true, false, false));
                SetHintText(Codes.GetHintTextName(RealWorldName), "has been revealed!", "", true, false, false);
            }
            else if (data.mode == Mode.PathHints) //path
            {
                string RealWorldName = data.reportInformation[hintNum].Item2;
                Console.WriteLine("Path Revealing " + RealWorldName);
                data.WorldsData[RealWorldName].hintedProgression = true;

                data.WorldsData[RealWorldName].worldGrid.ProgressionReport_Path(hintNum);
            }
            else if (data.mode == Mode.SpoilerHints) //spoiler
            {
                string RealWorldName = data.reportInformation[hintNum].Item1;
                Console.WriteLine("Spoiler Revealing " + RealWorldName);
                data.WorldsData[RealWorldName].hintedProgression = true;

                SetWorldValue(data.WorldsData[RealWorldName].value, data.WorldsData[RealWorldName].worldGrid.Children.Count);
                data.WorldsData[RealWorldName].worldGrid.ProgressionReport_Spoiler(hintNum);

                data.HintRevealsStored.Add(new Tuple<string, string, string, bool, bool, bool>(Codes.GetHintTextName(RealWorldName), "has been revealed!", "", true, false, false));
                SetHintText(Codes.GetHintTextName(RealWorldName), "has been revealed!", "", true, false, false);
            }
        }

        public int GetProgressionPointsReward(string worldName, int prog)
        {
            int temp = 0;
            switch (worldName)
            {
                case "SimulatedTwilightTown":
                    //if the world is done, give the bonus point right away
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        //if the progression marker was > 0, give the bonus - no bonus for 0 at prog point
                        temp = (data.STT_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.STT_ProgressionValues[prog - 1] > 0) //store the bonus point for later
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.STT_ProgressionValues[prog - 1] + temp;
                case "TwilightTown":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.TT_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.TT_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.TT_ProgressionValues[prog - 1] + temp;
                case "HollowBastion":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.HB_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.HB_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.HB_ProgressionValues[prog - 1] + temp;
                case "CavernofRemembrance":
                    if (data.WorldsData["HollowBastion"].complete && data.WorldsData["HollowBastion"].hintedProgression)
                        temp = (data.CoR_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.CoR_ProgressionValues[prog - 1] > 0)
                        data.WorldsData["HollowBastion"].worldGrid.WorldCompleteProgressionBonus();
                    return data.CoR_ProgressionValues[prog - 1] + temp;
                case "BeastsCastle":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.BC_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.BC_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.BC_ProgressionValues[prog - 1] + temp;
                case "OlympusColiseum":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.OC_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.OC_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.OC_ProgressionValues[prog - 1] + temp;
                case "Agrabah":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.AG_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.AG_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.AG_ProgressionValues[prog - 1] + temp;
                case "LandofDragons":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.LoD_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.LoD_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.LoD_ProgressionValues[prog - 1] + temp;
                case "HundredAcreWood":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.HAW_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.HAW_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.HAW_ProgressionValues[prog - 1] + temp;
                case "PrideLands":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.PL_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.PL_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.PL_ProgressionValues[prog - 1] + temp;
                case "Atlantica":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.AT_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.AT_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.AT_ProgressionValues[prog - 1] + temp;
                case "DisneyCastle":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.DC_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.DC_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.DC_ProgressionValues[prog - 1] + temp;
                case "HalloweenTown":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.HT_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.HT_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.HT_ProgressionValues[prog - 1] + temp;
                case "PortRoyal":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.PR_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.PR_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.PR_ProgressionValues[prog - 1] + temp;
                case "SpaceParanoids":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.SP_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.SP_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.SP_ProgressionValues[prog - 1] + temp;
                case "TWTNW":
                    if (data.WorldsData[worldName].complete && data.WorldsData[worldName].hintedProgression)
                        temp = (data.TWTNW_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    else if (data.TWTNW_ProgressionValues[prog - 1] > 0)
                        data.WorldsData[worldName].worldGrid.WorldCompleteProgressionBonus();
                    return data.TWTNW_ProgressionValues[prog - 1] + temp;
                case "GoA":
                    //if (data.WorldsData["HollowBastion"].complete && data.WorldsData["HollowBastion"].hintedProgression)
                    //    temp = (data.HB_ProgressionValues[prog - 1] > 0 ? data.WorldCompleteBonus : 0);
                    //else if (data.HB_ProgressionValues[prog - 1] > 0)
                    //    data.WorldsData["HollowBastion"].worldGrid.WorldCompleteProgressionBonus();
                    //return data.CoR_ProgressionValues[prog - 1] + temp;
                    return 0;
                default: //return if any other world
                    return 0;
            }
        }
    }
}