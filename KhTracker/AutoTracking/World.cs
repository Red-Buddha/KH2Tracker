using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    class World
    {
        private Dictionary<int, string> worldCodes;

        public string previousworldName;
        private string world;
        public string worldName
        {
            get { return world; }
            set
            {
                if (world != value)
                {
                    world = value;
                    if (App.logger != null)
                        App.logger.RecordWorld(value);
                }
            }
        }
        public int worldNum;
        private int worldAddress;
        private int eventCompleteAddress;
        private int SttAddress;

        public int roomNumber;
        public int eventID1;
        public int eventID2;
        public int eventID3;
        public int eventComplete;
        public int inStt;

        public int test1;
        public int test2;
        public int test3;
        public int test4;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public World(MemoryReader mem, int offset, int address, int completeAddress, int sttAddress)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            worldAddress = address;
            eventCompleteAddress = completeAddress;
            SttAddress = sttAddress;

            worldCodes = new Dictionary<int, string>
            {
                { 01, "GoA" }, // Title Demo
                { 02, "TwilightTown" },
                { 03, "DestinyIsland" },
                { 04, "HollowBastion" },
                { 05, "BeastsCastle" },
                { 06, "OlympusColiseum" },
                { 07, "Agrabah" },
                { 08, "LandofDragons" },
                { 09, "HundredAcreWood" },
                { 10, "PrideLands" },
                { 11, "Atlantica" },
                { 12, "DisneyCastle" },
                { 13, "DisneyCastle" }, // Timeless River
                { 14, "HalloweenTown" },
                { 16, "PortRoyal" },
                { 17, "SpaceParanoids" },
                { 18, "TWTNW" },
                { 255, "GoA" }
            };
        }

        public void UpdateMemory()
        {
            previousworldName = worldName;

            //this shouldn't happen, but use unknown as the world in case it ever does
            if (worldName == null)
                worldName = "Unknown";

            byte[] worldData = memory.ReadMemory(worldAddress + ADDRESS_OFFSET, 9);
            worldNum = worldData[0];
            roomNumber = worldData[1];
            eventID1 = worldData[4];
            eventID2 = worldData[6];
            eventID3 = worldData[8];

            test1 = worldData[2];
            test2 = worldData[3];
            test3 = worldData[5];
            test4 = worldData[7];

            byte[] eventData = memory.ReadMemory(eventCompleteAddress + ADDRESS_OFFSET, 1);
            eventComplete = eventData[0];

            byte[] sttData = memory.ReadMemory(SttAddress + ADDRESS_OFFSET, 1);
            inStt = sttData[0];


            string tempWorld;
            if (worldCodes.ContainsKey(worldNum))
            {
                tempWorld = worldCodes[worldNum];
            }
            else
            {
                tempWorld = "";
            }
            
            // Handle AS fights
            if (tempWorld == "HollowBastion")
            {
                if (roomNumber == 26)
                    worldName = "GoA";
                else if (roomNumber == 32)
                    worldName = "HalloweenTown"; // Vexen
                else if (roomNumber == 33 && (eventID3 == 122 || eventID1 == 123 || eventID1 == 142     // AS Lexaeus
                                            || eventID3 == 132 || eventID1 == 133 || eventID1 == 147))  // Data Lexaeus
                    worldName = "Agrabah"; // Lexaeus
                else if (roomNumber == 33 && (eventID3 == 128 || eventID1 == 129 || eventID1 == 143     // AS Larxene
                                            || eventID3 == 138 || eventID1 == 139 || eventID1 == 148))  // Data Larxene
                    worldName = "SpaceParanoids"; // Larxene
                else if (roomNumber == 34)
                    worldName = "OlympusColiseum"; // Zexion
                else if (roomNumber == 38)
                    worldName = "DisneyCastle"; // Marluxia
                else
                    worldName = "HollowBastion";
            }
            // Handle STT
            else if (tempWorld == "TwilightTown")
            {
                if (inStt == 13)
                    worldName = "SimulatedTwilightTown";
                else if ((roomNumber == 32 && eventID1 == 1) || (roomNumber == 1 && eventID1 == 52))
                    worldName = "GoA"; // Crit bonuses
                else
                    worldName = "TwilightTown";
            }
            // Handle Data fights
            else if (tempWorld == "TWTNW")
            {
                if (roomNumber == 10 && (eventID1 == 108))
                    worldName = "LandofDragons"; // Xigbar
                else if (roomNumber == 15 && (eventID1 == 110))
                    worldName = "PrideLands"; // Saix
                else if (roomNumber == 14 && (eventID1 == 112))
                    worldName = "PortRoyal"; // Luxord
                else if (roomNumber == 21 && (eventID1 == 114))
                    worldName = "SimulatedTwilightTown"; // Roxas
                else
                    worldName = "TWTNW";
            }
            else
            {
                if (worldName != tempWorld && tempWorld != "")
                {
                    worldName = tempWorld;
                }
            }

            //(App.Current.MainWindow as MainWindow).HintText.Content = worldName;
        }
    }
}
