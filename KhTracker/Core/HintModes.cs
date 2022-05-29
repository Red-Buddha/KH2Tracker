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
        private Dictionary<string, string> convertOpenKH = new Dictionary<string, string>()
        {
            {"Level", "SorasHeart" },
            {"Form Levels", "DriveForms" },
            {"Simulated Twilight Town", "SimulatedTwilightTown" },
            {"Twilight Town", "TwilightTown" },
            {"Hollow Bastion", "HollowBastion" },
            {"Beast's Castle", "BeastsCastle" },
            {"Olympus Coliseum", "OlympusColiseum" },
            {"Agrabah", "Agrabah" },
            {"Land of Dragons", "LandofDragons" },
            {"Hundred Acre Wood", "HundredAcreWood" },
            {"Pride Lands", "PrideLands" },
            {"Disney Castle / Timeless River", "DisneyCastle" },
            {"Halloween Town", "HalloweenTown" },
            {"Port Royal", "PortRoyal" },
            {"Space Paranoids", "SpaceParanoids" },
            {"The World That Never Was", "TWTNW" },
            {"Atlantica", "Atlantica" },
            {"Proof of Connection", "Connection" },
            {"Proof of Nonexistence", "Nonexistence" },
            {"Proof of Peace", "Peace" },
            {"PromiseCharm", "PromiseCharm" },
            {"Valor Form", "Valor" },
            {"Wisdom Form", "Wisdom" },
            {"Limit Form", "Limit" },
            {"Master Form", "Master" },
            {"Final Form", "Final" },
            {"Fire Element", "Fire" },
            {"Blizzard Element", "Blizzard" },
            {"Thunder Element", "Thunder" },
            {"Cure Element", "Cure" },
            {"Magnet Element", "Magnet" },
            {"Reflect Element", "Reflect" },
            {"Ukulele Charm (Stitch)", "Ukulele" },
            {"Baseball Charm (Chicken Little)", "Baseball" },
            {"Lamp Charm (Genie)", "Lamp" },
            {"Feather Charm (Peter Pan)", "Feather" },
            {"Torn Pages", "TornPage" },
            {"Second Chance", "SecondChance" },
            {"Once More", "OnceMore" },
            {"Secret Ansem's Report 1", "Report1"},
            {"Secret Ansem's Report 2", "Report2"},
            {"Secret Ansem's Report 3", "Report3"},
            {"Secret Ansem's Report 4", "Report4"},
            {"Secret Ansem's Report 5", "Report5"},
            {"Secret Ansem's Report 6", "Report6"},
            {"Secret Ansem's Report 7", "Report7"},
            {"Secret Ansem's Report 8", "Report8"},
            {"Secret Ansem's Report 9", "Report9"},
            {"Secret Ansem's Report 10", "Report10"},
            {"Secret Ansem's Report 11", "Report11"},
            {"Secret Ansem's Report 12", "Report12"},
            {"Secret Ansem's Report 13", "Report13"},
            {"Battlefields of War (Auron)", "AuronWep"},
            {"Sword of the Ancestor (Mulan)", "MulanWep"},
            {"Beast's Claw (Beast)", "BeastWep"},
            {"Bone Fist (Jack Skellington)", "JackWep"},
            {"Proud Fang (Simba)", "SimbaWep"},
            {"Skill and Crossbones (Jack Sparrow)", "SparrowWep"},
            {"Scimitar (Aladdin)", "AladdinWep"},
            {"Identity Disk (Tron)", "TronWep"},
            {"Membership Card", "MembershipCard"},
            {"Ice Cream", "IceCream"},
            {"Picture", "Picture"},
            {"Garden of Assemblage", "GoA"},
            {"", "GoA"},
            {"Critical Bonuses", "GoA"},
            {"Creations", "PuzzSynth"}
        };

        private void ShanHints(Dictionary<string, object> hintObject)
        {
            ShouldResetHash = true;
            var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());

            foreach (var world in worlds)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
                }

            }
            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetReportValue(data.WorldsData[key].hint, 0);
            }
        }

        private void JsmarteeHints(Dictionary<string, object> hintObject)
        {
            ShouldResetHash = true;
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());
            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            foreach (var report in reportKeys)
            {
                var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
                var count = reports[report.ToString()]["Count"].ToString();
                var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
                data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
                data.reportLocations.Add(location);
            }
            ReportsToggle(true);
            data.hintsLoaded = true;
        }

        private void PathHints(Dictionary<string, object> hintObject)
        {
            ShouldResetHash = true;
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
                    data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
                }

            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "GoA")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetReportValue(data.WorldsData[key].hint, 0);
            }

            foreach (int report in reportKeys)
            {
                var hinttext = reports[report.ToString()]["Text"].ToString();
                int hintproofs = 0;
                var hintworld = convertOpenKH[reports[report.ToString()]["HintedWorld"].ToString()];
                var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];

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

                data.pathreportInformation.Add(new Tuple<string, string, int>(hinttext, hintworld, hintproofs));
                data.reportLocations.Add(location);
            }

            //set pathproof defaults
            foreach (string key in data.WorldsData.Keys.ToList())
            {
                //adjust grid sizes for path proof icons
                data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(2.25, GridUnitType.Star);
                Grid grid = data.WorldsData[key].world.Parent as Grid;
                grid.ColumnDefinitions[3].Width = new GridLength(2.2, GridUnitType.Star);

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

        /// <summary>
        /// points hints and logic
        /// </summary>

        //this used to be a bunch of different lists for each type in the data class that were built on init
        //that was kinda dumb so i removed all of that and instead have a single list here. far easier to update now
        private Dictionary<string, string> GetItemType = new Dictionary<string, string>()
        {
            {"Fire Element", "magic"},
            {"Blizzard Element", "magic"},
            {"Thunder Element", "magic"},
            {"Cure Element", "magic"},
            {"Magnet Element", "magic"},
            {"Reflect Element", "magic"},
            {"Ukulele Charm (Stitch)", "summon"},
            {"Lamp Charm (Genie)", "summon"},
            {"Feather Charm (Peter Pan)", "summon"},
            {"Baseball Charm (Chicken Little)", "summon"},
            {"Valor Form", "form"},
            {"Wisdom Form", "form"},
            {"Final Form", "form"},
            {"Master Form", "form"},
            {"Limit Form", "form"},
            {"Second Chance", "ability"},
            {"Once More", "ability"},
            {"PromiseCharm", "proof"},
            {"Proof of Connection", "proof"},
            {"Proof of Nonexistence", "proof"},
            {"Proof of Peace", "proof"},
            {"Torn Pages", "page"},
            {"Secret Ansem's Report 1", "report"},
            {"Secret Ansem's Report 2", "report"},
            {"Secret Ansem's Report 3", "report"},
            {"Secret Ansem's Report 4", "report"},
            {"Secret Ansem's Report 5", "report"},
            {"Secret Ansem's Report 6", "report"},
            {"Secret Ansem's Report 7", "report"},
            {"Secret Ansem's Report 8", "report"},
            {"Secret Ansem's Report 9", "report"},
            {"Secret Ansem's Report 10", "report"},
            {"Secret Ansem's Report 11", "report"},
            {"Secret Ansem's Report 12", "report"},
            {"Secret Ansem's Report 13", "report"},
            {"Battlefields of War (Auron)", "visit"},
            {"Sword of the Ancestor (Mulan)", "visit"},
            {"Beast's Claw (Beast)", "visit"},
            {"Bone Fist (Jack Skellington)", "visit"},
            {"Proud Fang (Simba)", "visit"},
            {"Skill and Crossbones (Jack Sparrow)", "visit"},
            {"Scimitar (Aladdin)", "visit"},
            {"Identity Disk (Tron)", "visit"},
            {"Membership Card", "visit"},
            {"Ice Cream", "visit"},
            {"Picture", "visit"}
        };
 
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
            ShouldResetHash = true;

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

            //get point totals for each world
            foreach (var world in worldsP)
            {
                if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                {
                    continue;
                }
                foreach (var item in world.Value)
                {
                    data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);

                    string itemType = CheckItemType(item);
                    if (data.PointsDatanew.Keys.Contains(itemType))
                    {
                        WorldPoints[convertOpenKH[world.Key]] += data.PointsDatanew[itemType];
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

                data.WorldsData[key].worldGrid.WorldPointsComplete();

                if (WorldPoints.Keys.Contains(key))
                {
                    SetReportValue(data.WorldsData[key].hint, WorldPoints[key]);
                }
                else
                {
                    Console.WriteLine($"Something went wrong in setting world point numbers. error: {key}");
                }
            }

            //set hints for each report
            foreach (var reportP in reportKeysP)
            {
                var worldP = convertOpenKH[reportsP[reportP.ToString()]["World"].ToString()];
                var checkP = reportsP[reportP.ToString()]["check"].ToString();
                var locationP = convertOpenKH[reportsP[reportP.ToString()]["Location"].ToString()];

                data.pointreportInformation.Add(new Tuple<string, string>(worldP, checkP));
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
            if (data.mode != Mode.DAHints)
                return;

            int[] FinalNum = new int[] { 0, 0, 0, 0 }; //Default 0000

            if (!CheckCountOption.IsChecked)
            {
                score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                ScoreSpacer.Width = new GridLength(15.0, GridUnitType.Star);
                broadcast.score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                broadcast.scorespacer.Width = new GridLength(1.6, GridUnitType.Star);

                Score1000.Visibility = Visibility.Hidden;
                Score100.Visibility = Visibility.Hidden;
                Score10.Visibility = Visibility.Hidden;
                Score1.Visibility = Visibility.Visible;
                broadcast.Score1000.Visibility = Visibility.Hidden;
                broadcast.Score100.Visibility = Visibility.Hidden;
                broadcast.Score10.Visibility = Visibility.Hidden;
                broadcast.Score1.Visibility = Visibility.Visible;
            }

            int num = PointTotal + points; //get new point total
            int BonusTotal = 0;
            int Valorlv = 0;
            int Wisdomlv = 0;
            int Limitlv = 0;
            int Masterlv = 0;
            int Finallv = 0;
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
                BonusTotal = stats.BonusLevel * BonusPoints;
                Valorlv = (valor.Level - 1) * FormPoints;
                Wisdomlv = (wisdom.Level - 1) * FormPoints;
                Limitlv = (limit.Level - 1) * FormPoints;
                Masterlv = (master.Level - 1) * FormPoints;
                Finallv = (final.Level - 1) * FormPoints;
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

            //split point total into separate digits
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num /= 10;
            }

            //Set number images depending on number of digits in point total
            if (listOfInts.Count == 4)
            {
                FinalNum[0] = listOfInts[0];
                FinalNum[1] = listOfInts[1];
                FinalNum[2] = listOfInts[2];
                FinalNum[3] = listOfInts[3];

                if (!CheckCountOption.IsChecked)
                {
                    score1000col.Width = new GridLength(1.0, GridUnitType.Star);
                    ScoreSpacer.Width = new GridLength(18.0, GridUnitType.Star);
                    broadcast.score1000col.Width = new GridLength(1.0, GridUnitType.Star);
                    broadcast.scorespacer.Width = new GridLength(2.0, GridUnitType.Star);

                    Score1000.Visibility = Visibility.Visible;
                    Score100.Visibility = Visibility.Visible;
                    Score10.Visibility = Visibility.Visible;
                    Score1.Visibility = Visibility.Visible;
                    broadcast.Score1000.Visibility = Visibility.Visible;
                    broadcast.Score100.Visibility = Visibility.Visible;
                    broadcast.Score10.Visibility = Visibility.Visible;
                    broadcast.Score1.Visibility = Visibility.Visible;
                }

            }
            if (listOfInts.Count == 3)
            {
                FinalNum[0] = listOfInts[0];
                FinalNum[1] = listOfInts[1];
                FinalNum[2] = listOfInts[2];

                if (!CheckCountOption.IsChecked)
                {
                    score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    ScoreSpacer.Width = new GridLength(15.0, GridUnitType.Star);
                    broadcast.score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    broadcast.scorespacer.Width = new GridLength(1.6, GridUnitType.Star);

                    Score1000.Visibility = Visibility.Hidden;
                    Score100.Visibility = Visibility.Visible;
                    Score10.Visibility = Visibility.Visible;
                    Score1.Visibility = Visibility.Visible;
                    broadcast.Score1000.Visibility = Visibility.Hidden;
                    broadcast.Score100.Visibility = Visibility.Visible;
                    broadcast.Score10.Visibility = Visibility.Visible;
                    broadcast.Score1.Visibility = Visibility.Visible;
                }

            }
            else if (listOfInts.Count == 2)
            {
                FinalNum[0] = listOfInts[0];
                FinalNum[1] = listOfInts[1];

                if (!CheckCountOption.IsChecked)
                {
                    score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    ScoreSpacer.Width = new GridLength(15.0, GridUnitType.Star);
                    broadcast.score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    broadcast.scorespacer.Width = new GridLength(1.6, GridUnitType.Star);

                    Score1000.Visibility = Visibility.Hidden;
                    Score100.Visibility = Visibility.Hidden;
                    Score10.Visibility = Visibility.Visible;
                    Score1.Visibility = Visibility.Visible;
                    broadcast.Score1000.Visibility = Visibility.Hidden;
                    broadcast.Score100.Visibility = Visibility.Hidden;
                    broadcast.Score10.Visibility = Visibility.Visible;
                    broadcast.Score1.Visibility = Visibility.Visible;
                }

            }
            else if (listOfInts.Count == 1)
            {
                FinalNum[0] = listOfInts[0];

                if (!CheckCountOption.IsChecked)
                {
                    score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    ScoreSpacer.Width = new GridLength(15.0, GridUnitType.Star);
                    broadcast.score1000col.Width = new GridLength(0.0, GridUnitType.Star);
                    broadcast.scorespacer.Width = new GridLength(1.6, GridUnitType.Star);

                    Score1000.Visibility = Visibility.Hidden;
                    Score100.Visibility = Visibility.Hidden;
                    Score10.Visibility = Visibility.Hidden;
                    Score1.Visibility = Visibility.Visible;
                    broadcast.Score1000.Visibility = Visibility.Hidden;
                    broadcast.Score100.Visibility = Visibility.Hidden;
                    broadcast.Score10.Visibility = Visibility.Hidden;
                    broadcast.Score1.Visibility = Visibility.Visible;
                }

            }

            Score1000.Source = GetDataNumber("S")[FinalNum[3]];
            Score100.Source = GetDataNumber("S")[FinalNum[2]];
            Score10.Source = GetDataNumber("S")[FinalNum[1]];
            Score1.Source = GetDataNumber("S")[FinalNum[0]];

            broadcast.Score1000.Source = GetDataNumber("S")[FinalNum[3]];
            broadcast.Score100.Source = GetDataNumber("S")[FinalNum[2]];
            broadcast.Score10.Source = GetDataNumber("S")[FinalNum[1]];
            broadcast.Score1.Source = GetDataNumber("S")[FinalNum[0]];
        }

        private string CheckItemType(string item)
        {
            //need to retur some kinda value just in case
            if (GetItemType.Keys.Contains(item))
                return GetItemType[item];
            else
                return "Unknown";
        }

        ///
        /// Timed hints stuff
        /// 

        private void TimeHints(Dictionary<string, object> hintObject)
        {
            ShouldResetHash = true;
            var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());

            //get time needed to pass fore each report
            int hintTime = JsonSerializer.Deserialize<int>(hintObject["Time"].ToString());
            data.timedHintsTimer = hintTime;

            //hint style
            //futureproofing. idea is that maybe have hints affect the tracker in ways similar to other hint modes.
            string hintStyle = JsonSerializer.Deserialize<string>(hintObject["Style"].ToString());

            List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            reportKeys.Sort();

            int i = 0;
            foreach (var report in reportKeys)
            {
                var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
                var count = reports[report.ToString()]["Count"].ToString();
                var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
                data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
                data.reportLocations.Add(location);

                data.worldStoredHintCount[WorldNameToIndex(data.reportLocations[i])]++;
                data.worldStoredOrigCount[WorldNameToIndex(world)] = int.Parse(count);
                data.worldHintNumber[WorldNameToIndex(world)] = i;
                i++;
            }

            ReportsToggle(false);
            data.hintsLoaded = true;

            //string hintFileName = entry.FullName.Substring(0, entry.FullName.IndexOf("."));
            ////Console.WriteLine("Seed name: " + hintFileName);
            ////Console.WriteLine("Hashcode: " + hintFileName.GetHashCode());
            //data.ShuffleHintOrder(hintFileName.GetHashCode());
            ////Console.WriteLine(data.PrintHintOrder(data.worldHintNumber));
            //data.lastStoredSeedHashTemp = hintFileName.GetHashCode();

            data.seedTimeLoaded = DateTime.UtcNow.GetHashCode();
            Console.WriteLine("utc hashcode: " + data.seedTimeLoaded);

            //AlternateTimeText();

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
    }
}