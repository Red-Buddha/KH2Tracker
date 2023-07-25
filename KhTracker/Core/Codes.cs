using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KhTracker
{
    public class Codes
    {
        //Helpers
        public string FindCode(string code)
        {
            if (Free.Contains(code))
            {
                return "GoA";
            }
            else if (SimulatedTwilightTown.Contains(code))
            {
                return "SimulatedTwilightTown";
            }
            else if (TwilightTown.Contains(code))
            {
                return "TwilightTown";
            }
            else if (HollowBastion.Contains(code))
            {
                return "HollowBastion";
            }
            else if (LandOfDragons.Contains(code))
            {
                return "LandofDragons";
            }
            else if (BeastsCastle.Contains(code))
            {
                return "BeastsCastle";
            }
            else if (OlympusColiseum.Contains(code))
            {
                return "OlympusColiseum";
            }
            else if (DisneyCastle.Contains(code))
            {
                return "DisneyCastle";
            }
            else if (PortRoyal.Contains(code))
            {
                return "PortRoyal";
            }
            else if (Agrabah.Contains(code))
            {
                return "Agrabah";
            }
            else if (HalloweenTown.Contains(code))
            {
                return "HalloweenTown";
            }
            else if (PrideLands.Contains(code))
            {
                return "PrideLands";
            }
            else if (Atlantica.Contains(code))
            {
                return "Atlantica";
            }
            else if (AcreWood.Contains(code))
            {
                return "HundredAcreWood";
            }
            else if (SpaceParanoids.Contains(code))
            {
                return "SpaceParanoids";
            }
            else if (TheWorldThatNeverWas.Contains(code))
            {
                return "TWTNW";
            }
            else if (Forms.Contains(code))
            {
                return "DriveForms";
            }
            else if (Levels.Contains(code))
            {
                return "SorasHeart";
            }

            return "";
        }

        public static string GetHintTextName(string worldName)
        {
            switch(worldName)
            {
                case "SimulatedTwilightTown":
                    return "Simulated Twilight Town";
                case "TwilightTown":
                    return "Twilight Town";
                case "HollowBastion":
                    return "Hollow Bastion";
                case "LandofDragons":
                    return "Land of Dragons";
                case "BeastsCastle":
                    return "Beast's Castle";
                case "OlympusColiseum":
                    return "Olympus Coliseum";
                case "DisneyCastle":
                    return "Disney Castle";
                case "PortRoyal":
                    return "Port Royal";
                case "HalloweenTown":
                    return "Halloween Town";
                case "PrideLands":
                    return "Pride Lands";
                case "HundredAcreWood":
                    return "Hundred Acre Wood";
                case "SpaceParanoids":
                    return "Space Paranoids";
                case "DriveForms":
                    return "Drive Forms";
                case "SorasHeart":
                    return "Sora's Heart";
                case "PuzzSynth":
                    return "Creations";
                default: 
                    return worldName;
            }
        }

        public static string GetWorldName(string worldName)
        {
            switch (worldName)
            {
                case "Simulated Twilight Town":
                    return "SimulatedTwilightTown";
                case "Twilight Town":
                    return "TwilightTown";
                case "Hollow Bastion":
                    return "HollowBastion";
                case "Land of Dragons":
                    return "LandofDragons";
                case "Beast's Castle":
                    return "BeastsCastle";
                case "Olympus Coliseum":
                    return "OlympusColiseum";
                case "Disney Castle":
                    return "DisneyCastle";
                case "Port Royal":
                    return "PortRoyal";
                case "Halloween Town":
                    return "HalloweenTown";
                case "Pride Lands":
                    return "PrideLands";
                case "Hundred Acre Wood":
                    return "HundredAcreWood";
                case "Space Paranoids":
                    return "SpaceParanoids";
                case "Drive Forms":
                    return "DriveForms";
                case "Sora's Heart":
                    return "SorasHeart";
                case "Creations":
                    return "PuzzSynth";
                default:
                    return worldName;
            }
        }

        public string GetDefault(int index)
        {
            return Default[index];
        }

        public static string FindShortName(string name)
        {
            if (shortNames.ContainsKey(name))
                return shortNames[name];
            else
                return name;
        }

        public static string FindItemType(string name)
        {
            if (itemTypes.Keys.Contains(name))
                return itemTypes[name];
            else
                return "Unknown";
        }

        public static string GetTextColor(string name)
        {
            if (getColors.Keys.Contains(name))
                return getColors[name];
            else if (name.EndsWith("important checks"))
                return "Gold";
            else
                return "DefWhite";
        }

        public static string FindItemName(string name)
        {
            if (convertItemNames.ContainsKey(name))
                return convertItemNames[name];
            else
                return name;
        }

        public static string ConvertSeedGenName(string name)
        {
            if (convertOpenKH.ContainsKey(name))
                return convertOpenKH[name];
            else
                return "Unknown";
        }

        public static string FindBossType(string name)
        {
            if (bossTypes.Keys.Contains(name))
                return bossTypes[name];
            else
                return "boss_other";
        }

        //Dictionaries
        public Dictionary<int, string> itemCodes = new Dictionary<int, string>
        {
            {21, "Fire"},
            {22, "Blizzard"},
            {23, "Thunder"},
            {24, "Cure"},
            {25, "Ukulele"},
            {26, "Valor"},
            {27, "Wisdom"},
            {29, "Final"},
            {31, "Master"},
            {32, "TornPage"},
            {87, "Magnet"},
            {88, "Reflect"},
            {159, "Lamp"},
            {160, "Feather"},
            {226, "Report1"},
            {227, "Report2"},
            {228, "Report3"},
            {229, "Report4"},
            {230, "Report5"},
            {231, "Report6"},
            {232, "Report7"},
            {233, "Report8"},
            {234, "Report9" },
            {235, "Report10"},
            {236, "Report11"},
            {237, "Report12"},
            {238, "Report13"},
            {383, "Baseball"},
            {415, "SecondChance"},
            {416, "OnceMore"},
            {524, "PromiseCharm"},
            {563, "Limit"},
            {593, "Connection"},
            {594, "Nonexistence"},
            {595, "Peace"},
            {54, "AuronWep"},
            {55, "MulanWep"},
            {59, "BeastWep"},
            {60, "JackWep"},
            {61, "SimbaWep"},
            {62, "SparrowWep"},
            {72, "AladdinWep"},
            {74, "TronWep"},
            {369, "MembershipCard"},
            {375, "IceCream"},
            {376, "Picture"},
            {30, "Anti"},
            {537, "HadesCup"},
            {370, "OlympusStone"},
            {462, "UnknownDisk"},
            {362, "MunnyPouch1"},
            {535, "MunnyPouch2"}
        };

        public static Dictionary<string, string> shortNames = new Dictionary<string, string>()
        {
            {"Secret Ansem's Report 1", "Ansem's Report"},
            {"Secret Ansem's Report 2", "Ansem's Report"},
            {"Secret Ansem's Report 3", "Ansem's Report"},
            {"Secret Ansem's Report 4", "Ansem's Report"},
            {"Secret Ansem's Report 5", "Ansem's Report"},
            {"Secret Ansem's Report 6", "Ansem's Report"},
            {"Secret Ansem's Report 7", "Ansem's Report"},
            {"Secret Ansem's Report 8", "Ansem's Report"},
            {"Secret Ansem's Report 9", "Ansem's Report"},
            {"Secret Ansem's Report 10", "Ansem's Report"},
            {"Secret Ansem's Report 11", "Ansem's Report"},
            {"Secret Ansem's Report 12", "Ansem's Report"},
            {"Secret Ansem's Report 13", "Ansem's Report"},
            {"Baseball Charm (Chicken Little)", "Baseball Charm"},
            {"Lamp Charm (Genie)", "Lamp Charm"},
            {"Ukulele Charm (Stitch)", "Ukulele Charm"},
            {"Feather Charm (Peter Pan)", "Feather Charm"},
            {"PromiseCharm", "Promise Charm"},
            {"Battlefields of War (Auron)", "Battlefields of War"},
            {"Sword of the Ancestor (Mulan)", "Sword of the Ancestor"},
            {"Beast's Claw (Beast)", "Beast's Claw"},
            {"Bone Fist (Jack Skellington)", "Bone Fist"},
            {"Proud Fang (Simba)", "Proud Fang"},
            {"Skill and Crossbones (Jack Sparrow)", "Skill and Crossbones"},
            {"Scimitar (Aladdin)", "Scimitar"},
            {"Identity Disk (Tron)", "Identity Disk"},
            {"Sora's Heart",            "Levels" },
            {"Drive Forms",             "Drives" },
            {"Simulated Twilight Town", "STT" },
            {"Twilight Town",           "TT" },
            {"Hollow Bastion",          "HB" },
            {"Beast's Castle",          "BC" },
            {"Olympus Coliseum",        "OC" },
            {"Agrabah",                 "AG" },
            {"Land of Dragons",         "LoD" },
            {"Hundred Acre Wood",       "100AW" },
            {"Pride Lands",             "PL" },
            {"Disney Castle",           "DC" },
            {"Halloween Town",          "HT" },
            {"Port Royal",              "PR" },
            {"Space Paranoids",         "SP" },
            {"Atlantica",               "AT" }
        };

        public static Dictionary<string, string> itemTypes = new Dictionary<string, string>()
        {
            {"Report1", "report"},
            {"Report2", "report"},
            {"Report3", "report"},
            {"Report4", "report"},
            {"Report5", "report"},
            {"Report6", "report"},
            {"Report7", "report"},
            {"Report8", "report"},
            {"Report9", "report"},
            {"Report10", "report"},
            {"Report11", "report"},
            {"Report12", "report"},
            {"Report13", "report"},
            {"Fire", "magic"},
            {"Fire1", "magic"},
            {"Fire2", "magic"},
            {"Fire3", "magic"},
            {"Blizzard", "magic"},
            {"Blizzard1", "magic"},
            {"Blizzard2", "magic"},
            {"Blizzard3", "magic"},
            {"Thunder", "magic"},
            {"Thunder1", "magic"},
            {"Thunder2", "magic"},
            {"Thunder3", "magic"},
            {"Cure", "magic"},
            {"Cure1", "magic"},
            {"Cure2", "magic"},
            {"Cure3", "magic"},
            {"Reflect", "magic"},
            {"Reflect1", "magic"},
            {"Reflect2", "magic"},
            {"Reflect3", "magic"},
            {"Magnet", "magic"},
            {"Magnet1", "magic"},
            {"Magnet2", "magic"},
            {"Magnet3", "magic"},
            {"Valor", "form"},
            {"Wisdom", "form"},
            {"Limit", "form"},
            {"Master", "form"},
            {"Final", "form"},
            {"OnceMore", "ability"},
            {"SecondChance", "ability"},
            {"TornPage", "page"},
            {"TornPage1", "page"},
            {"TornPage2", "page"},
            {"TornPage3", "page"},
            {"TornPage4", "page"},
            {"TornPage5", "page"},
            {"Baseball", "summon"},
            {"Lamp", "summon"},
            {"Ukulele", "summon"},
            {"Feather", "summon"},
            {"Connection", "proof"},
            {"Nonexistence", "proof"},
            {"Peace", "proof"},
            {"PromiseCharm", "proof"},
            {"AuronWep", "visit"},
            {"MulanWep", "visit"},
            {"BeastWep", "visit"},
            {"JackWep", "visit"},
            {"SimbaWep", "visit"},
            {"SparrowWep", "visit"},
            {"AladdinWep", "visit"},
            {"TronWep", "visit"},
            {"MembershipCard", "visit"},
            {"IceCream", "visit"},
            {"Picture", "visit"},
            {"Anti", "form"},
            {"HadesCup", "other"},
            {"OlympusStone", "other"},
            {"UnknownDisk", "other"},
            {"MunnyPouch", "other"},
            {"MunnyPouch1", "other"},
            {"MunnyPouch2", "other"},           
            //ghost versions
            {"Ghost_Report1", "report"},
            {"Ghost_Report2", "report"},
            {"Ghost_Report3", "report"},
            {"Ghost_Report4", "report"},
            {"Ghost_Report5", "report"},
            {"Ghost_Report6", "report"},
            {"Ghost_Report7", "report"},
            {"Ghost_Report8", "report"},
            {"Ghost_Report9", "report"},
            {"Ghost_Report10", "report"},
            {"Ghost_Report11", "report"},
            {"Ghost_Report12", "report"},
            {"Ghost_Report13", "report"},
            {"Ghost_Fire1", "magic"},
            {"Ghost_Fire2", "magic"},
            {"Ghost_Fire3", "magic"},
            {"Ghost_Blizzard1", "magic"},
            {"Ghost_Blizzard2", "magic"},
            {"Ghost_Blizzard3", "magic"},
            {"Ghost_Thunder1", "magic"},
            {"Ghost_Thunder2", "magic"},
            {"Ghost_Thunder3", "magic"},
            {"Ghost_Cure1", "magic"},
            {"Ghost_Cure2", "magic"},
            {"Ghost_Cure3", "magic"},
            {"Ghost_Reflect1", "magic"},
            {"Ghost_Reflect2", "magic"},
            {"Ghost_Reflect3", "magic"},
            {"Ghost_Magnet1", "magic"},
            {"Ghost_Magnet2", "magic"},
            {"Ghost_Magnet3", "magic"},
            {"Ghost_Valor", "form"},
            {"Ghost_Wisdom", "form"},
            {"Ghost_Limit", "form"},
            {"Ghost_Master", "form"},
            {"Ghost_Final", "form"},
            {"Ghost_OnceMore", "ability"},
            {"Ghost_SecondChance", "ability"},
            {"Ghost_TornPage1", "page"},
            {"Ghost_TornPage2", "page"},
            {"Ghost_TornPage3", "page"},
            {"Ghost_TornPage4", "page"},
            {"Ghost_TornPage5", "page"},
            {"Ghost_Baseball", "summon"},
            {"Ghost_Lamp", "summon"},
            {"Ghost_Ukulele", "summon"},
            {"Ghost_Feather", "summon"},
            {"Ghost_Connection", "proof"},
            {"Ghost_Nonexistence", "proof"},
            {"Ghost_Peace", "proof"},
            {"Ghost_PromiseCharm", "proof"},
            {"Ghost_AuronWep", "visit"},
            {"Ghost_MulanWep", "visit"},
            {"Ghost_BeastWep", "visit"},
            {"Ghost_JackWep", "visit"},
            {"Ghost_SimbaWep", "visit"},
            {"Ghost_SparrowWep", "visit"},
            {"Ghost_AladdinWep", "visit"},
            {"Ghost_TronWep", "visit"},
            {"Ghost_MembershipCard", "visit"},
            {"Ghost_IceCream", "visit"},
            {"Ghost_Picture", "visit"},
            {"Ghost_Anti", "form"},
            {"Ghost_HadesCup", "other"},
            {"Ghost_OlympusStone", "other"},
            {"Ghost_UnknownDisk", "other"},
            {"Ghost_MunnyPouch1", "other"},
            {"Ghost_MunnyPouch2", "other"},  
            //seedgen names
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
            {"Promise Charm", "proof"},
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
            {"Anti-Form", "form"},
            {"Hades Cup Trophy", "other"},
            {"Olympus Stone", "other"},
            {"Unknown Disk", "other"},
            {"Munny Pouch", "other"}
        };

        public static Dictionary<string, string> convertItemNames = new Dictionary<string, string>()
        {
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
            {"Valor Form", "Valor"},
            {"Wisdom Form", "Wisdom"},
            {"Limit Form", "Limit"},
            {"Master Form", "Master"},
            {"Final Form", "Final"},
            {"Once More", "OnceMore"},
            {"Second Chance", "SecondChance"},
            {"Baseball Charm (Chicken Little)", "Baseball"},
            {"Lamp Charm (Genie)", "Lamp"},
            {"Ukulele Charm (Stitch)", "Ukulele"},
            {"Feather Charm (Peter Pan)", "Feather"},
            {"Proof of Connection", "Connection"},
            {"Proof of Nonexistence", "Nonexistence"},
            {"Proof of Peace", "Peace"},
            {"PromiseCharm", "PromiseCharm"},
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
            {"Anti-Form", "Anti"},
            {"Hades Cup Trophy", "HadesCup"},
            {"Olympus Stone", "OlympusStone"},
            {"Unknown Disk", "UnknownDisk"}
        };

        public static Dictionary<string, string> convertOpenKH = new Dictionary<string, string>()
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
            {"Secret Ansem's Report 14", "Report14"},
            {"Secret Ansem's Report 15", "Report15"},
            {"Secret Ansem's Report 16", "Report16"},
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
            {"Anti-Form", "Anti"},
            {"Hades Cup Trophy", "HadesCup"},
            {"Olympus Stone", "OlympusStone"},
            {"Unknown Disk", "UnknownDisk"},
            {"Garden of Assemblage", "GoA"},
            {"", "GoA"},
            {"Critical Bonuses", "GoA"},
            {"Creations", "PuzzSynth"},
            {"Empty", "Empty"},
            {"Munny Pouch", "MunnyPouch" }
        };

        public static Dictionary<string, string> bossTypes = new Dictionary<string, string>()
        {
            {"Armor Xemnas I", "boss_other"},
            {"Armor Xemnas II", "boss_other"},
            {"Axel (Data)", "boss_datas"},
            {"Axel I", "boss_other"},
            {"Axel II", "boss_other"},
            {"Barbossa", "boss_other"},
            {"Blizzard Lord", "boss_other"},
            {"Blizzard Lord (Cups)", "boss_other"},
            {"Cerberus", "boss_other"},
            {"Cerberus (Cups)", "boss_other"},
            {"Cloud", "boss_other"},
            {"Cloud (1)", "boss_other"},
            {"Cloud (2)", "boss_other"},
            {"Dark Thorn", "boss_other"},
            {"Demyx", "boss_other"},
            {"Demyx (Data)", "boss_datas"},
            {"Grim Reaper I", "boss_other"},
            {"Grim Reaper II", "boss_other"},
            {"Hades Cups", "boss_other"},
            {"Hades II", "boss_other"},
            {"Hades II (1)", "boss_other"},
            {"Hayner", "boss_other"},
            {"Hercules", "boss_other"},
            {"Hostile Program", "boss_other"},
            {"Larxene", "boss_as"},
            {"Larxene (Data)", "boss_datas"},
            {"Leon", "boss_other"},
            {"Leon (1)", "boss_other"},
            {"Leon (2)", "boss_other"},
            {"Leon (3)", "boss_other"},
            {"Lexaeus", "boss_as"},
            {"Lexaeus (Data)", "boss_datas"},
            {"Luxord", "boss_other"},
            {"Luxord (Data)", "boss_datas"},
            {"Marluxia", "boss_as"},
            {"Marluxia (Data)", "boss_datas"},
            {"Past Pete", "boss_other"},
            {"Pete Cups", "boss_other"},
            {"Pete OC II", "boss_other"},
            {"Prison Keeper", "boss_other"},
            {"Roxas", "boss_other"},
            {"Roxas (Data)", "boss_datas"},
            {"Saix", "boss_other"},
            {"Saix (Data)", "boss_datas"},
            {"Sark", "boss_other"},
            {"Scar", "boss_other"},
            {"Seifer", "boss_other"},
            {"Seifer (1)", "boss_other"},
            {"Seifer (2)", "boss_other"},
            {"Seifer (3)", "boss_other"},
            {"Seifer (4)", "boss_other"},
            {"Sephiroth", "boss_sephi"},
            {"Setzer", "boss_other"},
            {"Shan-Yu", "boss_other"},
            {"Terra", "boss_terra"},
            {"The Beast", "boss_other"},
            {"The Experiment", "boss_other"},
            {"Thresholder", "boss_other"},
            {"Tifa", "boss_other"},
            {"Tifa (1)", "boss_other"},
            {"Tifa (2)", "boss_other"},
            {"Twilight Thorn", "boss_other"},
            {"Vexen", "boss_as"},
            {"Vexen (Data)", "boss_datas"},
            {"Vivi", "boss_other"},
            {"Volcano Lord", "boss_other"},
            {"Volcano Lord (Cups)", "boss_other"},
            {"Xaldin", "boss_other"},
            {"Xaldin (Data)", "boss_datas"},
            {"Xemnas", "boss_other"},
            {"Xemnas (Data)", "boss_datas"},
            {"Final Xemnas", "boss_other"},
            {"Final Xemnas (Data)", "boss_datas"},
            {"Xigbar", "boss_other"},
            {"Xigbar (Data)", "boss_datas"},
            {"Yuffie", "boss_other"},
            {"Yuffie (1)", "boss_other"},
            {"Yuffie (2)", "boss_other"},
            {"Yuffie (3)", "boss_other"},
            {"Zexion", "boss_as"},
            {"Zexion (Data)", "boss_datas"},
            {"Hydra", "boss_other"},
            {"Jafar", "boss_other"},
            {"Storm Rider", "boss_other"},
            {"Groundshaker", "boss_other"},
            {"Boat Pete", "boss_other"},
            {"Pete TR", "boss_other"},
            {"Oogie Boogie", "boss_other"},
            {"MCP", "boss_other"}
        };

        public static Dictionary<string, string> bossLocations = new Dictionary<string, string>()
        {
            //Orgmembers (AS)
            {"Marluxia", "DC"},
            {"Marluxia (Data)", "DC"},
            {"Lexaeus", "AG"},
            {"Lexaeus (Data)", "AG"},
            {"Larxene", "SP"},
            {"Larxene (Data)", "SP"},
            {"Vexen", "HT"},
            {"Vexen (Data)", "HT"},
            {"Zexion", "OC"},
            {"Zexion (Data)", "OC"},
            //Orgmembers (Normal)
            {"Armor Xemnas I", "TWTNW"},
            {"Armor Xemnas II", "TWTNW"},
            {"Final Xemnas", "TWTNW"},
            {"Final Xemnas (Data)", "TWTNW"},
            {"Axel (Data)", "TT"},
            {"Axel I", "STT"},
            {"Axel II", "STT"},
            {"Demyx", "HB"},
            {"Demyx (Data)", "HB"},
            {"Luxord", "TWTNW"},
            {"Luxord (Data)", "PR"},
            {"Roxas", "TWTNW"},
            {"Roxas (Data)", "STT"},
            {"Saix", "TWTNW"},
            {"Saix (Data)", "PL"},
            {"Xaldin", "BC"},
            {"Xaldin (Data)", "BC"},
            {"Xemnas", "TWTNW"},
            {"Xemnas (Data)", "TWTNW"},
            {"Xigbar", "TWTNW"},
            {"Xigbar (Data)", "LoD"},
            //
            {"Blizzard Lord", "AG"},
            {"Volcano Lord", "AG"},
            {"Jafar", "AG"},
            //
            {"Barbossa", "PR"},
            {"Grim Reaper I", "PR"},
            {"Grim Reaper II", "PR"},
            //
            {"Thresholder", "BC"},
            {"The Beast", "BC"},
            {"Shadow Stalker", "BC"},
            {"Dark Thorn", "BC"},
            //
            {"Hostile Program", "SP"},
            {"Sark", "SP"},
            {"MCP", "SP"},
            {"Past Pete", "DC"},
            {"Boat Pete", "DC"},
            {"Pete TR", "DC"},
            //
            {"Prison Keeper", "HT"},
            {"Oogie Boogie", "HT"},
            {"The Experiment", "HT"},
            //
            {"Shan-Yu", "LoD"},
            {"Riku", "LoD"},
            {"Storm Rider", "LoD"},
            //
            {"Scar", "PL"},
            {"Groundshaker", "PL"},
            //STT
            {"Twilight Thorn", "STT"},
            {"Hayner", "STT"},
            {"Vivi", "STT"},
            {"Setzer", "STT"},
            //
            {"Seifer", "STT"},
            {"Seifer (1)", "STT"},
            {"Seifer (2)", "STT"},
            {"Seifer (3)", "TT"},
            {"Seifer (4)", "TT"},
            //
            {"Sephiroth", "HB"},
            {"Terra", "DC"},
            //
            {"Hades I", "OC"},  //should probably skip this one for hinting and whatnot
            {"Hades Escape", "OC" }, //should probably skip this one for hinting and whatnot
            {"Cerberus", "OC"},
            {"Pete OC II", "OC"},
            {"Hydra", "OC"},
            {"Hades II", "OC"},
            {"Hades II (1)", "OC"},
            //Cups
            {"Blizzard Lord (Cups)", "OC"},
            {"Volcano Lord (Cups)", "OC"},
            {"Cerberus (Cups)", "OC"},
            {"Hercules", "OC"},
            {"Hades Cups", "OC"},
            {"Pete Cups", "OC"},
            {"Cloud", "OC"},
            {"Cloud (1)", "OC"},
            {"Cloud (2)", "OC"},
            {"Leon", "OC"},
            {"Leon (1)", "OC"},
            {"Leon (2)", "OC"},
            {"Leon (3)", "OC"},
            {"Tifa", "OC"},
            {"Tifa (1)", "OC"},
            {"Tifa (2)", "OC"},
            {"Yuffie", "OC"},
            {"Yuffie (1)", "OC"},
            {"Yuffie (2)", "OC"},
            {"Yuffie (3)", "OC"},
        };

        public static List<string> bossDups = new List<string>()
        {
            {"Hades Escape"},
            {"Hades I"},
            {"Hades II"},
            {"Blizzard Lord (Cups)"},
            {"Volcano Lord (Cups)"},
            {"Cerberus (Cups)"},
            {"Hades Cups"},
            {"Pete Cups"},
            {"Cloud (1)"},
            {"Cloud (2)"},
            {"Leon (1)"},
            {"Leon (2)"},
            {"Leon (3)"},
            {"Tifa (1)"},
            {"Tifa (2)"},
            {"Yuffie (1)"},
            {"Yuffie (2)"},
            {"Yuffie (3)"},
            //NOTE: temp solution, just don't hint seifer at all for now.
            //seifer 1 is always shadow roxas as of this note and all other seifers except tutorial don't work
            {"Seifer"},
            {"Seifer (1)"},
            {"Seifer (2)"},
            {"Seifer (3)"},
            {"Seifer (4)"},
        };

        public static Dictionary<string, string> getColors = new Dictionary<string, string>()
        {
            {"Sora's Heart", "Pink" },
            {"Drive Forms", "Gold" },
            {"Simulated Twilight Town", "WhiteBlue" },
            {"Twilight Town", "Orange" },
            {"Hollow Bastion", "Pink" },
            {"Beast's Castle", "Pink" },
            {"Olympus Coliseum", "Green" },
            {"Agrabah", "Gold" },
            {"Land of Dragons", "Red" },
            {"Hundred Acre Wood", "Gold" },
            {"Pride Lands", "Red" },
            {"Disney Castle", "LightBlue" },
            {"Halloween Town", "Purple" },
            {"Port Royal", "DarkBlue" },
            {"Space Paranoids", "DarkBlue" },
            {"TWTNW", "White" },
            {"Atlantica", "LightBlue" },
            {"Creations", "Pink"},
            {"GoA", "WhiteBlue"},
            {"Secret Ansem's Report 1", "White"},
            {"Secret Ansem's Report 2", "White"},
            {"Secret Ansem's Report 3", "White"},
            {"Secret Ansem's Report 4", "White"},
            {"Secret Ansem's Report 5", "White"},
            {"Secret Ansem's Report 6", "White"},
            {"Secret Ansem's Report 7", "White"},
            {"Secret Ansem's Report 8", "White"},
            {"Secret Ansem's Report 9", "White"},
            {"Secret Ansem's Report 10", "White"},
            {"Secret Ansem's Report 11", "White"},
            {"Secret Ansem's Report 12", "White"},
            {"Secret Ansem's Report 13", "White"},
            {"Valor Form", "Red"},
            {"Wisdom Form", "LightBlue"},
            {"Limit Form", "Orange"},
            {"Master Form", "Gold"},
            {"Final Form", "White"},
            {"Anti-Form", "DarkBlue"},
            {"Once More", "LightBlue"},
            {"Second Chance", "Green"},
            {"Baseball Charm", "Gold"},
            {"Lamp Charm", "Purple"},
            {"Ukulele Charm", "LightBlue"},
            {"Feather Charm", "Red"},
            {"Proof of Connection", "OffWhite"},
            {"Proof of Nonexistence", "OffWhite"},
            {"Proof of Peace", "OffWhite"},
            {"Promise Charm", "Pink"},
            {"Battlefields of War", "Green"},
            {"Sword of the Ancestor", "Orange"},
            {"Beast's Claw", "Pink"},
            {"Bone Fist", "Purple"},
            {"Proud Fang", "Red"},
            {"Skill and Crossbones", "DarkBlue"},
            {"Scimitar", "Gold"},
            {"Identity Disk", "DarkBlue"},
            {"Membership Card", "Purple"},
            {"Ice Cream", "LightBlue"},
            {"Picture", "Orange"},
            {"Hades Cup Trophy", "DarkBlue"},
            {"Olympus Stone", "Gold"},
            {"Unknown Disk", "White"},
            {"Munny Pouch", "Red"},
            {"Fire Element", "Orange" },
            {"Blizzard Element", "DarkBlue" },
            {"Thunder Element", "Gold" },
            {"Cure Element", "Green" },
            {"Magnet Element", "Pink" },
            {"Reflect Element", "WhiteBlue" },
            {"Torn Pages", "Gold" }
        };

        public static Dictionary<string, int> HashInt = new Dictionary<string, int>
        {
            {"ability-unequip", 1},
            {"accessory", 2},
            {"ai-mode-frequent", 3},
            {"ai-mode-moderate", 4},
            {"ai-mode-rare", 5},
            {"ai-settings", 6},
            {"armor", 7},
            {"button-circle", 8},
            {"button-cross", 9},
            {"button-l1", 10},
            {"button-l2", 11},
            {"button-r1", 12},
            {"button-r2", 13},
            {"button-square", 14},
            {"button-triangle", 15},
            {"exclamation-mark", 16},
            {"form", 17},
            {"gumi-block", 18},
            {"gumi-blueprint", 19},
            {"gumi-brush", 20},
            {"gumi-gear", 21},
            {"gumi-ship", 22},
            {"item-consumable", 23},
            {"item-key", 24},
            {"item-tent", 25},
            {"magic", 26},
            {"material", 27},
            {"party", 28},
            {"question-mark", 29},
            {"rank-a", 30},
            {"rank-b", 31},
            {"rank-c", 32},
            {"rank-s", 33},
            {"weapon-keyblade", 34},
            {"weapon-shield", 35},
            {"weapon-staff", 36}
        };

        //private Dictionary<string, int> GetItemPool = new Dictionary<string, int>()
        //{
        //    {"Report1", 0},
        //    {"Report2", 0},
        //    {"Report3", 0},
        //    {"Report4", 0},
        //    {"Report5", 0},
        //    {"Report6", 0},
        //    {"Report7", 0},
        //    {"Report8", 0},
        //    {"Report9", 0},
        //    {"Report10", 0},
        //    {"Report11", 0},
        //    {"Report12", 0},
        //    {"Report13", 0},
        //    {"Fire1", 1},
        //    {"Fire2", 1},
        //    {"Fire3", 1},
        //    {"Blizzard1", 1},
        //    {"Blizzard2", 1},
        //    {"Blizzard3", 1},
        //    {"Thunder1", 1},
        //    {"Thunder2", 1},
        //    {"Thunder3", 1},
        //    {"Cure1", 1},
        //    {"Cure2", 1},
        //    {"Cure3", 1},
        //    {"HadesCup", 1},
        //    {"OlympusStone", 1},
        //    {"Reflect1", 2},
        //    {"Reflect2", 2},
        //    {"Reflect3", 2},
        //    {"Magnet1", 2},
        //    {"Magnet2", 2},
        //    {"Magnet3", 2},
        //    {"Valor", 2},
        //    {"Wisdom", 2},
        //    {"Limit", 2},
        //    {"Master", 2},
        //    {"Final", 2},
        //    {"Anti", 2},
        //    {"OnceMore", 2},
        //    {"SecondChance", 2},
        //    {"UnknownDisk", 3},
        //    {"TornPage1", 3},
        //    {"TornPage2", 3},
        //    {"TornPage3", 3},
        //    {"TornPage4", 3},
        //    {"TornPage5", 3},
        //    {"Baseball", 3},
        //    {"Lamp", 3},
        //    {"Ukulele", 3},
        //    {"Feather", 3},
        //    {"Connection", 3},
        //    {"Nonexistence", 3},
        //    {"Peace", 3},
        //    {"PromiseCharm", 3},
        //    {"BeastWep", 4},
        //    {"JackWep", 4},
        //    {"SimbaWep", 4},
        //    {"AuronWep", 4},
        //    {"MulanWep", 4},
        //    {"SparrowWep", 4},
        //    {"AladdinWep", 4},
        //    {"TronWep", 4},
        //    {"MembershipCard", 4},
        //    {"Picture", 4},
        //    {"IceCream", 4},
        //    {"Ghost_Report1", 5},
        //    {"Ghost_Report2", 5},
        //    {"Ghost_Report3", 5},
        //    {"Ghost_Report4", 5},
        //    {"Ghost_Report5", 5},
        //    {"Ghost_Report6", 5},
        //    {"Ghost_Report7", 5},
        //    {"Ghost_Report8", 5},
        //    {"Ghost_Report9", 5},
        //    {"Ghost_Report10", 5},
        //    {"Ghost_Report11", 5},
        //    {"Ghost_Report12", 5},
        //    {"Ghost_Report13", 5},
        //    {"Ghost_Fire1", 6},
        //    {"Ghost_Fire2", 6},
        //    {"Ghost_Fire3", 6},
        //    {"Ghost_Blizzard1", 6},
        //    {"Ghost_Blizzard2", 6},
        //    {"Ghost_Blizzard3", 6},
        //    {"Ghost_Thunder1", 6},
        //    {"Ghost_Thunder2", 6},
        //    {"Ghost_Thunder3", 6},
        //    {"Ghost_Cure1", 6},
        //    {"Ghost_Cure2", 6},
        //    {"Ghost_Cure3", 6},
        //    {"Ghost_HadesCup", 6},
        //    {"Ghost_OlympusStone", 6},
        //    {"Ghost_Reflect1", 7},
        //    {"Ghost_Reflect2", 7},
        //    {"Ghost_Reflect3", 7},
        //    {"Ghost_Magnet1", 7},
        //    {"Ghost_Magnet2", 7},
        //    {"Ghost_Magnet3", 7},
        //    {"Ghost_Valor", 7},
        //    {"Ghost_Wisdom", 7},
        //    {"Ghost_Limit", 7},
        //    {"Ghost_Master", 7},
        //    {"Ghost_Final", 7},
        //    {"Ghost_Anti", 7},
        //    {"Ghost_OnceMore", 7},
        //    {"Ghost_SecondChance", 7},
        //    {"Ghost_UnknownDisk", 8},
        //    {"Ghost_TornPage1", 8},
        //    {"Ghost_TornPage2", 8},
        //    {"Ghost_TornPage3", 8},
        //    {"Ghost_TornPage4", 8},
        //    {"Ghost_TornPage5", 8},
        //    {"Ghost_Baseball", 8},
        //    {"Ghost_Lamp", 8},
        //    {"Ghost_Ukulele", 8},
        //    {"Ghost_Feather", 8},
        //    {"Ghost_Connection", 8},
        //    {"Ghost_Nonexistence", 8},
        //    {"Ghost_Peace", 8},
        //    {"Ghost_PromiseCharm", 8},
        //    {"Ghost_BeastWep", 9},
        //    {"Ghost_JackWep", 9},
        //    {"Ghost_SimbaWep", 9},
        //    {"Ghost_AuronWep", 9},
        //    {"Ghost_MulanWep", 9},
        //    {"Ghost_SparrowWep", 9},
        //    {"Ghost_AladdinWep", 9},
        //    {"Ghost_TronWep", 9},
        //    {"Ghost_MembershipCard", 9},
        //    {"Ghost_Picture", 9},
        //    {"Ghost_IceCream", 9}
        //};

        //Code Lists

        string[] Default = new string[]
        {
            "HollowBastion",
            "TwilightTown",
            "TWTNW",
            "BeastsCastle",
            "OlympusColiseum",
            "PortRoyal",
            "HollowBastion",
            "TWTNW",
            "TWTNW",
            "TwilightTown",
            "TWTNW",
            "TWTNW",
            "TWTNW"
        };

        string[] Free = new string[] {
        "11CE05E2",
        "11CE05EE",
        "11CE05FA",
        "11D18DDE",
        "11D18DDC",
        "11D18DE8",
        "11D18DE4",
        "11D18DE6",
        "11D18DE0",
        "11D18DE2" };

        string[] SimulatedTwilightTown = new string[] {
        "11CE016E",
        "11CE017A",
        "11CE0186",
        "11CE0192",
        "11CE019E",
        "11CE01AA",
        "11CE01B6",
        "11CE01C2",
        "11CE01CE",
        "11CE01DA",
        "11CE01E6",
        "11CE01F2",
        "11CE01FE",
        "11CE020A",
        "11CE0216",
        "11CE0222",
        "21D10FA8",
        "21D10CB8",
        "21D11278",
        "11CE0636",
        "11CE0606",
        "11CE0612",
        "11CE061E",
        "11CE062A",
        "11CE0642",
        "11CE064E",

        "11CE0B0A"}; // data roxas

        string[] TwilightTown = new string[] {
         "11CE022E",
         "11CE023A",
         "11CE0246",
         "11CE0252",
         "11CE025E",
         "11CE026A",
         "11CE0276",
         "11CE0282",
         "11CE028E",
         "11CE029A",
         "11CE02A6",
         "11CE02B2",
         "11CE02BE",
         "11CE02CA",
         "11CE02D6",
         "11CE02E2",
         "11CE02EE",
         "11CE02FA",
         "11CE0306",
         "11CE0312",
         "11CE031E",
         "11CE032A",
         "11CE0336",
         "11CE0342",
         "11CE034E",
         "11CE035A",
         "11CE0366",
         "11CE0372",
         "11CE037E",
         "11CE038A",
         "11CE0396",
         "11CE03A2",
         "11CE03AE",
         "11CE03BA",
         "11CE03C6",
         "11CE03D2",
         "11CE03DE",
         "11CE03EA",
         "11CE03F6",
         "21D110E8",
         "11CE065A",
         "11CE0666",
         "11CE0672",
         "11CE067E",
         "11CE07E6",
         "11CE07F2",
         "11CE07FE",
         "11CE0966",
         "11CE09AE",
         "11CE0A0E",

         "11CE0ACE" }; //data axel

        string[] HollowBastion = new string[] {
        "11CDFF3A",
        "11CDFF46",
        "11CDFF52",
        "11CDFF5E",
        "11CDFF6A",
        "11CDFF76",
        "11CDFF82",
        "11CDFF8E",
        "11CDFF9A",
        "11CDFFA6",
        "11CDFFB2",
        "11CDFFBE",
        "11CDFFCA",
        "11CDFFD6",
        "11CDFFE2",
        "11CDFFEE",
        "11CDFFFA",
        "11CE0006",
        "11CE0012",
        "11CE001E",
        "11CE002A",
        "11CE0036",
        "21D10E98",
        "21D10BA8",
        "21D11068",
        "11CE068A",
        "11CE0696",
        "11CE06A2",
        "11CE0702",
        "11CE080A",
        "11CE0822",
        "11CE082E",
        "11CE083A",
        "11CE0936",
        "11CE0942",
        "11CE09A2",
        "11CE09EA",
        "11CE0B3A", //shroom
        "11CE0B2E", //shroom
        
        "11CE04E6", //cor
        "11CE04F2",
        "11CE04FE",
        "11CE050A",
        "11CE0516",
        "11CE0522",
        "11CE052E",
        "11CE053A",
        "11CE0546",
        "11CE0552",
        "11CE055E",
        "11CE056A",
        "11CE0576",
        "11CE0582",
        "11CE058E",
        "11CE059A",
        "11CE05A6",
        "11CE05B2",
        "11CE05BE",
        "11CE05CA",
        "11CE05D6",

        "11CE0AB6" }; //data demyx
        
        string[] LandOfDragons = new string[] {
        "11CDF72A",
        "11CDF736",
        "11CDF742",
        "11CDF74E",
        "11CDF75A",
        "11CDF766",
        "11CDF772",
        "11CDF77E",
        "11CDF78A",
        "11CDF796",
        "11CDF7A2",
        "11CDF7AE",
        "11CDF7BA",
        "11CDF7C6",
        "11CDF7D2",
        "11CDF7DE",
        "11CDF7EA",
        "11CDF7F6",
        "11CDF802",
        "11CDF80E",
        "11CDF81A",
        "21D10DF8",
        "21D108C8",
        "21D10908",
        "11CE06D2",
        "11CE06C6",
        "11CE06DE",
        "11CE06EA",

        "11CE0AE6" }; //data xigbar
        
        string[] BeastsCastle = new string[] {
        "11CDFBF2",
        "11CDFBFE",
        "11CDFC0A",
        "11CDFC16",
        "11CDFC22",
        "11CDFC2E",
        "11CDFC3A",
        "11CDFC46",
        "11CDFC52",
        "11CDFC5E",
        "11CDFC6A",
        "11CDFC76",
        "11CDFC82",
        "11CDFC8E",
        "11CDFC9A",
        "11CDFCA6",
        "11CDFCB2",
        "11CDFCBE",
        "11CDFCCA",
        "11CDFCD6",
        "11CDFCE2",
        "21D10758",
        "21D10788",
        "21D107C8",
        "11CE06F6",
        "11CE0852",
        "11CE085E",
        "11CE09C6",

        "11CE0AC2" }; //data xaldin

        string[] OlympusColiseum = new string[] {
        "11CDFB02",
        "11CDFB0E",
        "11CDFB1A",
        "11CDFB26",
        "11CDFB32",
        "11CDFB3E",
        "11CDFB4A",
        "11CDFB56",
        "11CDFB62",
        "11CDFB6E",
        "11CDFB7A",
        "11CDFB86",
        "11CDFB92",
        "11CDFB9E",
        "11CDFBAA",
        "11CDFBB6",
        "11CDFBC2",
        "11CDFBCE",
        "11CDFBDA",
        "11CDFBE6",
        "21D10808",
        "21D10FE8",
        "21D10828",
        "21D10858",
        "21D10888",
        "11CE070E",
        "11CE071A",
        "11CE09D2",
        "11CE0726",
        "11CE0882",
        "11CE088E",

        "11CE073E", //cups
        "11CE074A",
        "11CE07CE",
        "11CE07DA",
        "11CE089A",
        "11CE08A6",
        "11CE094E",
        "11CE095A",
        "11CE0996", //hades cup

        "11CE0A56", //AS zexion
        "11CE0A92"}; //data zexion

        string[] DisneyCastle = new string[] {
        "11CDF9B2",
        "11CDF9BE",
        "11CDF9CA",
        "11CDF9D6",
        "11CDF9E2",
        "11CDF9EE",
        "11CDF9FA",
        "11CDFA06",
        "21D10D28",
        "11CE0756",
        "11CE0B16", //terra
        "11CE0B22", //terra

        "11CDF95E", //timeless river
        "11CDF96A",
        "11CDF976",
        "11CDF982",
        "11CDF98E",
        "11CDF99A",
        "11CDF9A6",
        "21D10988",
        "21D109B8",
        "11CE076E",
        "11CE0732",
        "11CE0762",

        "11CE0A6E", //AS marluxia
        "11CE0AAA"}; //data marluxia

        string[] PortRoyal = new string[] {
        "11CDFE3E",
        "11CDFE4A",
        "11CDFE56",
        "11CDFE62",
        "11CDFE6E",
        "11CDFE7A",
        "11CDFE86",
        "11CDFE92",
        "11CDFE9E",
        "11CDFEAA",
        "11CDFEB6",
        "11CDFEC2",
        "11CDFECE",
        "11CDFEDA",
        "11CDFEE6",
        "11CDFEF2",
        "11CDFEFE",
        "11CDFF0A",
        "11CDFF16",
        "11CDFF22",
        "11CDFF2E",
        "21D110B8",
        "21D10AA8",
        "21D11028",
        "21D10AE8",
        "11CE077A",
        "11CE0786",
        "11CE086A",
        "11CE0876",
        "11CE09DE",

        "11CE0AFE"}; //data luxord

        string[] Agrabah = new string[] {
        "11CDF826",
        "11CDF832",
        "11CDF83E",
        "11CDF84A",
        "11CDF856",
        "11CDF862",
        "11CDF86E",
        "11CDF87A",
        "11CDF886",
        "11CDF892",
        "11CDF89E",
        "11CDF8AA",
        "11CDF8B6",
        "11CDF8C2",
        "11CDF8CE",
        "11CDF8DA",
        "11CDF8E6",
        "11CDF8F2",
        "11CDF8FE",
        "11CDF90A",
        "11CDF916",
        "11CDF922",
        "11CDF92E",
        "11CDF93A",
        "11CDF946",
        "11CDF952",
        "21D10DB8",
        "21D10CE8",
        "21D10978",
        "11CE0792",
        "11CE079E",
        "11CE08B2",
        
        "11CE0A4A", //AS lexaeus
        "11CE0A86"}; //data lexaeus

        string[] HalloweenTown = new string[] {
        "11CDFD96",
        "11CDFDA2",
        "11CDFDAE",
        "11CDFDBA",
        "11CDFDC6",
        "11CDFDD2",
        "11CDFDDE",
        "11CDFDEA",
        "11CDFDF6",
        "11CDFE02",
        "11CDFE0E",
        "11CDFE1A",
        "11CDFE26",
        "11CDFE32",
        "21D109E8",
        "11CE07AA",
        "11CE08BE",
        "11CE08CA",
        "11CE08D6",

        "11CE0A3E", //AS vexen
        "11CE0A7A"}; //data vexen

        string[] PrideLands = new string[] {
        "11CE0042",
        "11CE004E",
        "11CE005A",
        "11CE0066",
        "11CE0072",
        "11CE007E",
        "11CE008A",
        "11CE0096",
        "11CE00A2",
        "11CE00AE",
        "11CE00BA",
        "11CE00C6",
        "11CE00D2",
        "11CE00DE",
        "11CE00EA",
        "11CE00F6",
        "11CE0102",
        "11CE010E",
        "11CE011A",
        "11CE0126",
        "11CE0132",
        "11CE013E",
        "11CE014A",
        "11CE0156",
        "11CE0162",
        "21D10C18",
        "11CE07B6",
        "11CE07C2",

        "11CE0AF2"}; //data saix

        string[] Atlantica = new string[] {
        "11CE0846",
        "11CE08E2",
        "11CE08EE",
        "11CE08FA"};

        string[] AcreWood = new string[] {
        "11CDFA12",
        "11CDFA1E",
        "11CDFA2A",
        "11CDFA36",
        "11CDFA42",
        "11CDFA4E",
        "11CDFA5A",
        "11CDFA66",
        "11CDFA72",
        "11CDFA7E",
        "11CDFA8A",
        "11CDFA96",
        "11CDFAA2",
        "11CDFAAE",
        "11CDFABA",
        "11CDFAC6",
        "11CDFAD2",
        "11CDFADE",
        "11CDFAEA",
        "11CDFAF6",
        "11CE0906",
        "11CE0912",
        "11CE091E",
        "11CE092A"};

        string[] SpaceParanoids = new string[] {
        "11CDFCEE",
        "11CDFCFA",
        "11CDFD06",
        "11CDFD12",
        "11CDFD1E",
        "11CDFD2A",
        "11CDFD36",
        "11CDFD42",
        "11CDFD4E",
        "11CDFD5A",
        "11CDFD66",
        "11CDFD72",
        "11CDFD7E",
        "11CDFD8A",
        "21D10C38",
        "21D11078",
        "21D10C78",
        "11CE0816",
        "11CE0A62", //AS larxene
        "11CE0A9E"}; //data larxene

        string[] TheWorldThatNeverWas = new string[] {
        "11CE0402",
        "11CE040E",
        "11CE041A",
        "11CE0426",
        "11CE0432",
        "11CE043E",
        "11CE044A",
        "11CE0456",
        "11CE0462",
        "11CE046E",
        "11CE047A",
        "11CE0486",
        "11CE0492",
        "11CE049E",
        "11CE04AA",
        "11CE04B6",
        "11CE04C2",
        "11CE04CE",
        "11CE04DA",
        "21D111E8",
        "21D10B58",
        "11CE0972",
        "11CE097E",
        "11CE098A",
        "11CE09BA",
        "11CE09F6",
        "11CE0A02",
        "11CE0A1A",
        "11CE0A26",
        "11CE0A32",

        "11CE0ADA"}; //data xemnas

        string[] Forms = new string[] {
        "11D1A22E", //valor
        "11D1A236",
        "11D1A23E",
        "11D1A246",
        "11D1A24E",
        "11D1A256",
        "11D1A266", //wisdom
        "11D1A26E",
        "11D1A276",
        "11D1A27E",
        "11D1A286",
        "11D1A28E",
        "11D1A29E", //limit
        "11D1A2A6",
        "11D1A2AE",
        "11D1A2B6",
        "11D1A2BE",
        "11D1A2C6",
        "11D1A2D6", //master
        "11D1A2DE",
        "11D1A2E6",
        "11D1A2EE",
        "11D1A2F6",
        "11D1A2FE",
        "11D1A30E", //Final
        "11D1A316",
        "11D1A31E",
        "11D1A326",
        "11D1A32E",
        "11D1A336"};

        string[] Levels = new string[] {
        "11D0B6C0", //Lvl 2
        "11D0B6E0", //Lvl 4
        "11D0B710", //Lvl 7
        "11D0B730", //Lvl 9
        "11D0B740", //Lvl 10
        "11D0B760", //Lvl 12
        "11D0B780", //Lvl 14
        "11D0B790", //Lvl 15
        "11D0B7B0", //Lvl 17
        "11D0B7E0", //Lvl 20
        "11D0B810", //Lvl 23
        "11D0B830", //Lvl 25
        "11D0B860", //Lvl 28
        "11D0B880", //Lvl 30
        "11D0B8A0", //Lvl 32
        "11D0B8C0", //Lvl 34
        "11D0B8E0", //Lvl 36
        "11D0B910", //Lvl 39
        "11D0B930", //Lvl 41
        "11D0B960", //Lvl 44
        "11D0B980", //Lvl 46
        "11D0B9A0", //Lvl 48
        "11D0B9C0", //Lvl 50

        "11D0B6D0", //Lvl 3
        "11D0B6F0", //Lvl 5
        "11D0B700", //Lvl 6
        "11D0B720", //Lvl 8
        "11D0B750", //Lvl 11
        "11D0B770", //Lvl 13
        "11D0B7A0", //Lvl 16
        "11D0B7C0", //Lvl 18
        "11D0B7D0", //Lvl 19
        "11D0B7F0", //Lvl 21
        "11D0B800", //Lvl 22
        "11D0B820", //Lvl 24
        "11D0B840", //Lvl 26
        "11D0B850", //Lvl 27
        "11D0B870", //Lvl 29
        "11D0B890", //Lvl 31
        "11D0B8B0", //Lvl 33
        "11D0B8D0", //Lvl 35
        "11D0B8F0", //Lvl 37
        "11D0B920", //Lvl 40
        "11D0B940", //Lvl 42
        "11D0B950", //Lvl 43
        "11D0B970", //Lvl 45
        "11D0B990", //Lvl 47
        "11D0B9B0", //Lvl 49

        "11D0B9D0", //Lvl 51-99
        "11D0B9E0",
        "11D0B9F0",
        "11D0BA00",
        "11D0BA10",
        "11D0BA20",
        "11D0BA30",
        "11D0BA40",
        "11D0BA50",
        "11D0BA60",
        "11D0BA70",
        "11D0BA80",
        "11D0BA90",
        "11D0BAA0",
        "11D0BAB0",
        "11D0BAC0",
        "11D0BAD0",
        "11D0BAE0",
        "11D0BAF0",
        "11D0BB00",
        "11D0BB10",
        "11D0BB20",
        "11D0BB30",
        "11D0BB40",
        "11D0BB50",
        "11D0BB60",
        "11D0BB70",
        "11D0BB80",
        "11D0BB90",
        "11D0BBA0",
        "11D0BBB0",
        "11D0BBC0",
        "11D0BBD0",
        "11D0BBE0",
        "11D0BBF0",
        "11D0BC00",
        "11D0BC10",
        "11D0BC20",
        "11D0BC30",
        "11D0BC40",
        "11D0BC50",
        "11D0BC60",
        "11D0BC70",
        "11D0BC80",
        "11D0BC90",
        "11D0BCA0",
        "11D0BCB0",
        "11D0BCC0",
        "11D0BCD0"};
    }
}
