using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    public class Codes
    {
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
            if (worldName == "SimulatedTwilightTown")
            {
                return "Simulated Twilight Town";
            }
            else if (worldName == "TwilightTown")
            {
                return "Twilight Town";
            }
            else if (worldName == "HollowBastion")
            {
                return "Hollow Bastion";
            }
            else if (worldName == "LandofDragons")
            {
                return "Land of Dragons";
            }
            else if (worldName == "BeastsCastle")
            {
                return "Beasts Castle";
            }
            else if (worldName == "OlympusColiseum")
            {
                return "Olympus Coliseum";
            }
            else if (worldName == "DisneyCastle")
            {
                return "Disney Castle";
            }
            else if (worldName == "PortRoyal")
            {
                return "Port Royal";
            }
            else if (worldName == "HalloweenTown")
            {
                return "Halloween Town";
            }
            else if (worldName == "PrideLands")
            {
                return "Pride Lands";
            }
            else if (worldName == "HundredAcreWood")
            {
                return "Hundred Acre Wood";
            }
            else if (worldName == "SpaceParanoids")
            {
                return "Space Paranoids";
            }
            else if (worldName == "DriveForms")
            {
                return "Drive Forms";
            }
            else if (worldName == "SorasHeart")
            {
                return "Soras Heart";
            }
            else if (worldName == "PuzzSynth")
            {
                return "Creations";
            }
            return worldName;
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
            {462, "UnknownDisk"}

        };

        public static Dictionary<string, string> shortNames = new Dictionary<string, string>()
        {
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
            {"Identity Disk (Tron)", "Identity Disk"}
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
            {"Fire1", "magic"},
            {"Fire2", "magic"},
            {"Fire3", "magic"},
            {"Blizzard1", "magic"},
            {"Blizzard2", "magic"},
            {"Blizzard3", "magic"},
            {"Thunder1", "magic"},
            {"Thunder2", "magic"},
            {"Thunder3", "magic"},
            {"Cure1", "magic"},
            {"Cure2", "magic"},
            {"Cure3", "magic"},
            {"Reflect1", "magic"},
            {"Reflect2", "magic"},
            {"Reflect3", "magic"},
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
            {"Unknown Disk", "other"}
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
            {"Creations", "PuzzSynth"}
        };

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
