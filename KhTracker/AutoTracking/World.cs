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

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public World(MemoryReader mem, int offset, int address, int completeAddress, int sttAddress)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            worldAddress = address;
            eventCompleteAddress = completeAddress;
            SttAddress = sttAddress;

            worldCodes = new Dictionary<int, string>();
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
            worldCodes.Add(255, "GoA");
        }

        public void UpdateMemory()
        {
            previousworldName = worldName;

            byte[] worldData = memory.ReadMemory(worldAddress + ADDRESS_OFFSET, 9);
            worldNum = worldData[0];
            roomNumber = worldData[1];
            eventID1 = worldData[4];
            eventID2 = worldData[6];
            eventID3 = worldData[8];

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
