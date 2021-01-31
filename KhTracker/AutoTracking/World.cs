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

        private string world;
        public string worldName
        {
            get { return world; }
            set
            {
                if (world != value)
                {
                    world = value;
                    App.logger.RecordWorld(value);
                }
            }
        }
        private int worldNum;
        private int worldAddress;

        public int roomNumber;
        public int eventID1;
        public int eventID2;
        public int eventID3;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public World(MemoryReader mem, int offset, int address)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            worldAddress = address;

            worldCodes = new Dictionary<int, string>();
            worldCodes.Add(01, "EndofSea");
            worldCodes.Add(02, "TwilightTown");
            worldCodes.Add(03, "DestinyIsland");
            worldCodes.Add(04, "HollowBastion");
            worldCodes.Add(05, "BeastsCastle");
            worldCodes.Add(06, "OlympusColiseum");
            worldCodes.Add(07, "Agrabah");
            worldCodes.Add(08, "LandofDragons");
            worldCodes.Add(09, "HundredAcreWood");
            worldCodes.Add(10, "PrideLands");
            worldCodes.Add(11, "Atlantica");
            worldCodes.Add(12, "DisneyCastle");
            worldCodes.Add(13, "DisneyCastle"); // Timeless River
            worldCodes.Add(14, "HalloweenTown");
            worldCodes.Add(16, "PortRoyal");
            worldCodes.Add(17, "SpaceParanoids");
            worldCodes.Add(18, "TWTNW");

        }

        public void UpdateMemory()
        {
            byte[] worldData = memory.ReadMemory(worldAddress + ADDRESS_OFFSET, 9);
            worldNum = worldData[0];
            roomNumber = worldData[1];
            eventID1 = worldData[4];
            eventID2 = worldData[6];
            eventID3 = worldData[8];

            string tempWorld;
            if (worldCodes.ContainsKey(worldNum))
            {
                // simplify determining if in stt
                if (worldName == "SimulatedTwilightTown" && !(worldCodes[worldNum] == "HollowBastion" && roomNumber == 26))
                    return;

                tempWorld = worldCodes[worldNum];
            } 
            else
            {
                tempWorld = "";
            }
            
            // Handle crit bonus start
            if (tempWorld == "TwilightTown" && roomNumber == 32 && eventID1 == 1 && eventID2 == 1 && eventID3 == 1)
            {
                worldName = "GoA";
            }
            // Handle AS fights
            else if (tempWorld == "HollowBastion")
            {
                if (roomNumber == 26)
                    worldName = "GoA";
                else if (roomNumber == 32)
                    worldName = "HalloweenTown"; // Vexen
                else if (roomNumber == 33 && eventID1 == 123)
                    worldName = "Agrabah"; // Lexaeus
                else if (roomNumber == 33 && eventID1 == 129)
                    worldName = "SpaceParanoids"; // Larxene
                else if (roomNumber == 34)
                    worldName = "OlympusColiseum"; // Zexion
                else if (roomNumber == 38)
                    worldName = "DisneyCastle"; // Marluxia
            }
            // Handle STT
            else if (tempWorld == "TwilightTown")
            {
                // probably need to track every save point for safety
                if ((roomNumber == 2 && eventID1 == 63) || (roomNumber == 21 && eventID1 == 7))
                {
                    worldName = "SimulatedTwilightTown";
                }
            }
            // Handle Data fights
            else if (tempWorld == "TWTNW")
            {
                if (roomNumber == 10 && eventID1 == 108)
                    worldName = "LandofDragons"; // Xigbar
                else if (roomNumber == 15 && eventID1 == 110)
                    worldName = "PrideLands"; // Saix
                else if (roomNumber == 14 && eventID1 == 112)
                    worldName = "PortRoyal"; // Luxord
                else if (roomNumber == 21 && eventID1 == 114)
                    worldName = "SimulatedTwilightTown"; // roxas
            }
            else
            {
                if (worldName != tempWorld)
                {
                    worldName = tempWorld;
                }
            }
        }
    }
}
