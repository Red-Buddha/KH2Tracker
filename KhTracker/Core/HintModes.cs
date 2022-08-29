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

            }
            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetWorldValue(data.WorldsData[key].value, 0);
            }
        }

        private void JsmarteeHints(Dictionary<string, object> hintObject)
        {
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

        private void PathHints(Dictionary<string, object> hintObject)
        {
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

            ReportsToggle(true);
            data.hintsLoaded = true;
        }

        private void SpoilerHints(Dictionary<string, object> hintObject)
        {
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
            else
                ReportsToggle(false);

            Dictionary<string, int> counts = new Dictionary<string, int>
            {
                {"Fire", 1 }, {"Blizzard", 1 }, {"Thunder", 1 },
                {"Cure", 1 }, {"Magnet", 1 }, {"Reflect", 1},
                {"TornPage", 1},
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
                    if (!data.SpoilerReportMode && item.Contains("Report"))
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
        ///TODO: update to use new font system instead of images
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

            int num = PointTotal + points; //get new point total
            int WorldBlue = 0;
            int BonusPoints = data.PointsDatanew["bonus"];
            int FormPoints = data.PointsDatanew["formlv"];
            int CompletePoints = data.PointsDatanew["complete"];

            PointTotal = num; //set new point total

            //adjust point score based on bonus and form levels
            //do this after setting new PointTotal value to avoid score
            //increasing forever when adding/removing items
            if (aTimer != null)
            {
                int BonusTotal = stats.BonusLevel * BonusPoints;
                int Valorlv = (valor.Level - 1) * FormPoints;
                int Wisdomlv = (wisdom.Level - 1) * FormPoints;
                int Limitlv = (limit.Level - 1) * FormPoints;
                int Masterlv = (master.Level - 1) * FormPoints;
                int Finallv = (final.Level - 1) * FormPoints;
                num += BonusTotal + Valorlv + Wisdomlv + Limitlv + Masterlv + Finallv;
            }

            //add bonus points for completeing a world
            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                if (data.WorldsData[key].complete && data.WorldsData[key].checkCount.Count != 0)
                    WorldBlue += CompletePoints;
            }

            num += WorldBlue;

            ScoreValue.Text = num.ToString();
        }

        static public int WorldNameToIndex(string worldName)
        {
            switch (worldName)
            {
                case "SorasHeart":
                    return 0;
                case "DriveForms":
                    return 1;
                case "SimulatedTwilightTown":
                    return 2;
                case "TwilightTown":
                    return 3;
                case "HollowBastion":
                    return 4;
                case "BeastsCastle":
                    return 5;
                case "OlympusColiseum":
                    return 6;
                case "Agrabah":
                    return 7;
                case "LandofDragons":
                    return 8;
                case "HundredAcreWood":
                    return 9;
                case "PrideLands":
                    return 10;
                case "DisneyCastle":
                    return 11;
                case "HalloweenTown":
                    return 12;
                case "PortRoyal":
                    return 13;
                case "SpaceParanoids":
                    return 14;
                case "TWTNW":
                    return 15;
                case "Atlantica":
                    return 17;
                case "PuzzSynth":
                    return 18;
                default: //GoA
                    return 16;
            }
        }

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
    }
}