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

        public string world;
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
            worldCodes.Add(02, "TwilightTown");
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
            if (worldCodes.ContainsKey(worldNum))
            {
                // simplify determining if in stt
                if (world == "SimulatedTwilightTown" && worldCodes[worldNum] != "HollowBastion")
                    return;

                world = worldCodes[worldNum];
            }
            else
            {
                world = "";
            }
            
            // Handle crit bonus start
            if (world == "TwilightTown" && roomNumber == 32 && eventID1 == 1 && eventID2 == 1 && eventID3 == 1)
            {
                world = "GoA";
            }
            // Handle Goa
            if (world == "HollowBastion" && roomNumber == 26)
            {
                world = "GoA";
            }
            // Handle STT
            if (world == "TwilightTown")
            {
                // probably need to track every save point for safety
                if ((roomNumber == 2 && eventID1 == 63) || (roomNumber == 21 && eventID1 == 7))
                {
                    world = "SimulatedTwilightTown";
                }
            }
        }
    }
}
