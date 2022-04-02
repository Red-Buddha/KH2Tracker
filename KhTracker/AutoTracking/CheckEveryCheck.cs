using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    /* Author: Coraccio aka Racci
     * Implemented to track Final Form after being forced
     * (Searches all checks except for Sora/Drive levels, where it only looks through the important checks already scanned by Rewards)
     */
    class CheckEveryCheck
    {
        public MemoryReader memory;
        public int ADDRESS_OFFSET;
        public int Save;
        public int Sys3;
        public int Btl0;
        public World World;
        public Stats Stats;
        public Rewards Rewards;
        public DriveForm Valor;
        public DriveForm Wisdom;
        public DriveForm Limit;
        public DriveForm Master;
        public DriveForm Final;
        private bool onLevels = false;
        private Dictionary<string, int> targetLevel;
        private bool onValor = false;
        private bool onWisdom = false;
        private bool onLimit = false;
        private bool onMaster = false;
        private bool onFinal = false;
        private Dictionary<string, int> targetDriveLevel;

        private int target;
        public int Target
        {
            get { return target; }
        }
        private List<Check> checksList;
        private List<Check> targetCheck;    // list in case there are duplicates
        private bool targetObtained = false;
        public bool TargetObtained
        {
            get { return targetObtained; }
        }

        public CheckEveryCheck(MemoryReader mem, int offset, int saveAnchor, int sysAnchor, int battleAnchor, World world, Stats stats, Rewards rewards,
            DriveForm valor, DriveForm wisdom, DriveForm limit, DriveForm master, DriveForm final)
        {
            memory = mem;
            ADDRESS_OFFSET = offset;
            Save = saveAnchor;
            Sys3 = sysAnchor;
            Btl0 = battleAnchor;
            World = world;
            Stats = stats;
            Rewards = rewards;
            Valor = valor;
            Wisdom = wisdom;
            Limit = limit;
            Master = master;
            Final = final;

            checksList = new List<Check>();

            #region Chest
            // Chest (317)
            checksList.Add(new Check(this, Sys3 + 0x1442A, Save + 0x23AC, 1));   // Bamboo Grove
            checksList.Add(new Check(this, Sys3 + 0x14436, Save + 0x23D9, 7));   // Bamboo Grove
            checksList.Add(new Check(this, Sys3 + 0x14442, Save + 0x23DA, 0));   // Bamboo Grove
            checksList.Add(new Check(this, Sys3 + 0x1444E, Save + 0x23AD, 1));   // Checkpoint
            checksList.Add(new Check(this, Sys3 + 0x1445A, Save + 0x23AD, 2));   // Checkpoint
            checksList.Add(new Check(this, Sys3 + 0x14466, Save + 0x23AD, 3));   // Mountain Trail
            checksList.Add(new Check(this, Sys3 + 0x14472, Save + 0x23AD, 4));   // Mountain Trail
            checksList.Add(new Check(this, Sys3 + 0x1447E, Save + 0x23AD, 5));   // Mountain Trail
            checksList.Add(new Check(this, Sys3 + 0x1448A, Save + 0x23AD, 6));   // Mountain Trail
            checksList.Add(new Check(this, Sys3 + 0x14496, Save + 0x23AD, 7));   // Village Cave
            checksList.Add(new Check(this, Sys3 + 0x144A2, Save + 0x23AE, 0));   // Village Cave
            checksList.Add(new Check(this, Sys3 + 0x144AE, Save + 0x23AE, 1));   // Ridge
            checksList.Add(new Check(this, Sys3 + 0x144BA, Save + 0x23AE, 2));   // Ridge
            checksList.Add(new Check(this, Sys3 + 0x144C6, Save + 0x23AE, 3));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x144D2, Save + 0x23AE, 4));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x144DE, Save + 0x23AE, 5));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x144EA, Save + 0x23AE, 6));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x144F6, Save + 0x23AE, 7));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x14502, Save + 0x23AF, 0));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x1450E, Save + 0x23AF, 1));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x1451A, Save + 0x23AF, 2));   // Throne Room
            checksList.Add(new Check(this, Sys3 + 0x14526, Save + 0x23AF, 3));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x14532, Save + 0x23AF, 4));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x1453E, Save + 0x23AF, 5));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x1454A, Save + 0x23AF, 6));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x14556, Save + 0x23AF, 7));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x14562, Save + 0x23B0, 0));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x1456E, Save + 0x23DA, 3));   // Agrabah
            checksList.Add(new Check(this, Sys3 + 0x14562, Save + 0x23B0, 1));   // Bazaar
            checksList.Add(new Check(this, Sys3 + 0x14562, Save + 0x23B0, 2));   // Bazaar
            checksList.Add(new Check(this, Sys3 + 0x14592, Save + 0x23B0, 3));   // Bazaar
            checksList.Add(new Check(this, Sys3 + 0x1459E, Save + 0x23B0, 4));   // Bazaar
            checksList.Add(new Check(this, Sys3 + 0x145AA, Save + 0x23B0, 5));   // Bazaar
            checksList.Add(new Check(this, Sys3 + 0x145B6, Save + 0x23B0, 6));   // Palace Walls
            checksList.Add(new Check(this, Sys3 + 0x145C2, Save + 0x23DB, 5));   // Palace Walls
            checksList.Add(new Check(this, Sys3 + 0x145CE, Save + 0x23B0, 7));   // The Cave of Wonders: Entrance
            checksList.Add(new Check(this, Sys3 + 0x145DA, Save + 0x23B1, 0));   // The Cave of Wonders: Entrance
            checksList.Add(new Check(this, Sys3 + 0x145E6, Save + 0x23B1, 2));   // The Cave of Wonders: Valley of Stone
            checksList.Add(new Check(this, Sys3 + 0x145F2, Save + 0x23B1, 3));   // The Cave of Wonders: Valley of Stone
            checksList.Add(new Check(this, Sys3 + 0x145FE, Save + 0x23B1, 4));   // The Cave of Wonders: Valley of Stone
            checksList.Add(new Check(this, Sys3 + 0x1460A, Save + 0x23B1, 5));   // The Cave of Wonders: Valley of Stone
            checksList.Add(new Check(this, Sys3 + 0x14616, Save + 0x23D8, 7));   // The Cave of Wonders: Chasm of Challenges
            checksList.Add(new Check(this, Sys3 + 0x14622, Save + 0x23B1, 6));   // The Cave of Wonders: Chasm of Challenges
            checksList.Add(new Check(this, Sys3 + 0x1462E, Save + 0x23DA, 4));   // The Cave of Wonders: Treasure Room
            checksList.Add(new Check(this, Sys3 + 0x1463A, Save + 0x23DA, 5));   // The Cave of Wonders: Treasure Room
            checksList.Add(new Check(this, Sys3 + 0x14646, Save + 0x23B1, 1));   // Ruined Chamber
            checksList.Add(new Check(this, Sys3 + 0x14652, Save + 0x23D8, 6));   // Ruined Chamber
            checksList.Add(new Check(this, Sys3 + 0x1465E, Save + 0x23B2, 0));   // Cornerstone Hill
            checksList.Add(new Check(this, Sys3 + 0x1466A, Save + 0x23B2, 1));   // Cornerstone Hill
            checksList.Add(new Check(this, Sys3 + 0x14676, Save + 0x23B2, 3));   // Pier
            checksList.Add(new Check(this, Sys3 + 0x14682, Save + 0x23B2, 4));   // Pier
            checksList.Add(new Check(this, Sys3 + 0x1468E, Save + 0x23B2, 5));   // Waterway
            checksList.Add(new Check(this, Sys3 + 0x1469A, Save + 0x23B2, 6));   // Waterway
            checksList.Add(new Check(this, Sys3 + 0x146A6, Save + 0x23B2, 7));   // Waterway
            checksList.Add(new Check(this, Sys3 + 0x146B2, Save + 0x23B4, 1));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146BE, Save + 0x23B4, 2));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146CA, Save + 0x23B4, 3));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146D6, Save + 0x23B4, 4));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146E2, Save + 0x23B4, 5));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146EE, Save + 0x23B4, 6));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x146FA, Save + 0x23B4, 7));   // Courtyard (DC)
            checksList.Add(new Check(this, Sys3 + 0x14706, Save + 0x23B4, 0));   // Library
            checksList.Add(new Check(this, Sys3 + 0x14712, Save + 0x23C9, 7));   // Pooh Bear's House
            checksList.Add(new Check(this, Sys3 + 0x1471E, Save + 0x23B5, 4));   // Pooh Bear's House
            checksList.Add(new Check(this, Sys3 + 0x1472A, Save + 0x23B5, 5));   // Pooh Bear's House
            checksList.Add(new Check(this, Sys3 + 0x14736, Save + 0x23B6, 4));   // Piglet's House
            checksList.Add(new Check(this, Sys3 + 0x14742, Save + 0x23B6, 2));   // Piglet's House
            checksList.Add(new Check(this, Sys3 + 0x1474E, Save + 0x23B6, 3));   // Piglet's House
            checksList.Add(new Check(this, Sys3 + 0x1475A, Save + 0x23CA, 0));   // Rabbit's House
            checksList.Add(new Check(this, Sys3 + 0x14766, Save + 0x23B5, 7));   // Rabbit's House
            checksList.Add(new Check(this, Sys3 + 0x14772, Save + 0x23B6, 0));   // Rabbit's House
            checksList.Add(new Check(this, Sys3 + 0x1477E, Save + 0x23B6, 5));   // Kanga's House
            checksList.Add(new Check(this, Sys3 + 0x1478A, Save + 0x23B6, 6));   // Kanga's House
            checksList.Add(new Check(this, Sys3 + 0x14796, Save + 0x23B6, 7));   // Kanga's House
            checksList.Add(new Check(this, Sys3 + 0x147A2, Save + 0x23B7, 1));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147AE, Save + 0x23B7, 2));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147BA, Save + 0x23B7, 3));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147C6, Save + 0x23B7, 4));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147D2, Save + 0x23B7, 6));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147DE, Save + 0x23B7, 7));   // The Spooky Cave
            checksList.Add(new Check(this, Sys3 + 0x147EA, Save + 0x23C9, 6));   // Starry Hill
            checksList.Add(new Check(this, Sys3 + 0x147F6, Save + 0x23B5, 1));   // Starry Hill
            checksList.Add(new Check(this, Sys3 + 0x14802, Save + 0x23B8, 0));   // Underworld Entrance
            checksList.Add(new Check(this, Sys3 + 0x1480E, Save + 0x23B9, 6));   // Cave of the Dead: Passage
            checksList.Add(new Check(this, Sys3 + 0x1481A, Save + 0x23B9, 7));   // Cave of the Dead: Passage
            checksList.Add(new Check(this, Sys3 + 0x14826, Save + 0x23BA, 0));   // Cave of the Dead: Passage
            checksList.Add(new Check(this, Sys3 + 0x14832, Save + 0x23BA, 1));   // Cave of the Dead: Passage
            checksList.Add(new Check(this, Sys3 + 0x1483E, Save + 0x23BA, 2));   // Cave of the Dead: Passage
            checksList.Add(new Check(this, Sys3 + 0x1484A, Save + 0x23B8, 4));   // Cave of the Dead: Inner Chamber
            checksList.Add(new Check(this, Sys3 + 0x14856, Save + 0x23B8, 3));   // Cave of the Dead: Inner Chamber
            checksList.Add(new Check(this, Sys3 + 0x14862, Save + 0x23B8, 5));   // Underworld Caverns: Entrance
            checksList.Add(new Check(this, Sys3 + 0x1486E, Save + 0x23B8, 6));   // Underworld Caverns: Entrance
            checksList.Add(new Check(this, Sys3 + 0x1487A, Save + 0x23DA, 6));   // Underworld Caverns: Entrance
            checksList.Add(new Check(this, Sys3 + 0x14886, Save + 0x23BA, 3));   // Underworld Caverns: The Lost Road
            checksList.Add(new Check(this, Sys3 + 0x14892, Save + 0x23BA, 4));   // Underworld Caverns: The Lost Road
            checksList.Add(new Check(this, Sys3 + 0x1489E, Save + 0x23BA, 5));   // Underworld Caverns: The Lost Road
            checksList.Add(new Check(this, Sys3 + 0x148AA, Save + 0x23BA, 6));   // Underworld Caverns: The Lost Road
            checksList.Add(new Check(this, Sys3 + 0x148B6, Save + 0x23BA, 7));   // Underworld Caverns: Atrium
            checksList.Add(new Check(this, Sys3 + 0x148C2, Save + 0x23BB, 0));   // Underworld Caverns: Atrium
            checksList.Add(new Check(this, Sys3 + 0x148CE, Save + 0x23B9, 4));   // The Lock
            checksList.Add(new Check(this, Sys3 + 0x148DA, Save + 0x23B9, 0));   // The Lock
            checksList.Add(new Check(this, Sys3 + 0x148E6, Save + 0x23B9, 2));   // The Lock
            checksList.Add(new Check(this, Sys3 + 0x148F2, Save + 0x23BB, 5));   // Courtyard (BC)
            checksList.Add(new Check(this, Sys3 + 0x148FE, Save + 0x23BB, 6));   // Courtyard (BC)
            checksList.Add(new Check(this, Sys3 + 0x1490A, Save + 0x23DA, 7));   // Courtyard (BC)
            checksList.Add(new Check(this, Sys3 + 0x14916, Save + 0x23BB, 2));   // Belle's Room
            checksList.Add(new Check(this, Sys3 + 0x14922, Save + 0x23BB, 3));   // Belle's Room
            checksList.Add(new Check(this, Sys3 + 0x1492E, Save + 0x23BB, 7));   // The East Wing
            checksList.Add(new Check(this, Sys3 + 0x1493A, Save + 0x23BC, 0));   // The East Wing
            checksList.Add(new Check(this, Sys3 + 0x14946, Save + 0x23BC, 1));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x14952, Save + 0x23BC, 2));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x1495E, Save + 0x23BC, 3));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x1496A, Save + 0x23BC, 4));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x14976, Save + 0x23BB, 5));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x14982, Save + 0x23DB, 0));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x1498E, Save + 0x23BD, 0));   // Dungeon
            checksList.Add(new Check(this, Sys3 + 0x1499A, Save + 0x23BD, 1));   // Dungeon
            checksList.Add(new Check(this, Sys3 + 0x149A6, Save + 0x23BD, 2));   // Secret Passage
            checksList.Add(new Check(this, Sys3 + 0x149B2, Save + 0x23BD, 5));   // Secret Passage
            checksList.Add(new Check(this, Sys3 + 0x149BE, Save + 0x23BD, 3));   // Secret Passage
            checksList.Add(new Check(this, Sys3 + 0x149CA, Save + 0x23BC, 6));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x149D6, Save + 0x23BC, 7));   // The West Wing
            checksList.Add(new Check(this, Sys3 + 0x149E2, Save + 0x23BB, 4));   // Beast's Room
            checksList.Add(new Check(this, Sys3 + 0x149EE, Save + 0x23CA, 2));   // Pit Cell
            checksList.Add(new Check(this, Sys3 + 0x149FA, Save + 0x23BD, 6));   // Pit Cell
            checksList.Add(new Check(this, Sys3 + 0x14A06, Save + 0x23BE, 1));   // Canyon
            checksList.Add(new Check(this, Sys3 + 0x14A12, Save + 0x23BE, 2));   // Canyon
            checksList.Add(new Check(this, Sys3 + 0x14A1E, Save + 0x23BE, 3));   // Canyon
            checksList.Add(new Check(this, Sys3 + 0x14A2A, Save + 0x23DB, 6));   // Canyon
            checksList.Add(new Check(this, Sys3 + 0x14A36, Save + 0x23BE, 4));   // I/O Tower: Hallway
            checksList.Add(new Check(this, Sys3 + 0x14A42, Save + 0x23BE, 5));   // I/O Tower: Hallway
            checksList.Add(new Check(this, Sys3 + 0x14A4E, Save + 0x23BF, 1));   // I/O Tower: Communications Room
            checksList.Add(new Check(this, Sys3 + 0x14A5A, Save + 0x23DA, 1));   // I/O Tower: Communications Room
            checksList.Add(new Check(this, Sys3 + 0x14A66, Save + 0x23BF, 4));   // Central Computer Mesa
            checksList.Add(new Check(this, Sys3 + 0x14A72, Save + 0x23BF, 5));   // Central Computer Mesa
            checksList.Add(new Check(this, Sys3 + 0x14A7E, Save + 0x23BF, 6));   // Central Computer Mesa
            checksList.Add(new Check(this, Sys3 + 0x14A8A, Save + 0x23D9, 0));   // Central Computer Mesa
            checksList.Add(new Check(this, Sys3 + 0x14A96, Save + 0x23C0, 2));   // Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14AA2, Save + 0x23C0, 3));   // Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14AAE, Save + 0x23C0, 1));   // Dr. Finkelstein's Lab
            checksList.Add(new Check(this, Sys3 + 0x14ABA, Save + 0x23BF, 7));   // Halloween Town Square
            checksList.Add(new Check(this, Sys3 + 0x14AC6, Save + 0x23C0, 0));   // Halloween Town Square
            checksList.Add(new Check(this, Sys3 + 0x14AD2, Save + 0x23C0, 4));   // Hinterlands
            checksList.Add(new Check(this, Sys3 + 0x14ADE, Save + 0x23C0, 5));   // Hinterlands
            checksList.Add(new Check(this, Sys3 + 0x14AEA, Save + 0x23C0, 6));   // Hinterlands
            checksList.Add(new Check(this, Sys3 + 0x14AF6, Save + 0x23C1, 0));   // Candy Cane Lane
            checksList.Add(new Check(this, Sys3 + 0x14B02, Save + 0x23C1, 1));   // Candy Cane Lane
            checksList.Add(new Check(this, Sys3 + 0x14B0E, Save + 0x23C1, 2));   // Candy Cane Lane
            checksList.Add(new Check(this, Sys3 + 0x14B1A, Save + 0x23C1, 3));   // Candy Cane Lane
            checksList.Add(new Check(this, Sys3 + 0x14B26, Save + 0x23C1, 6));   // Santa's House
            checksList.Add(new Check(this, Sys3 + 0x14B32, Save + 0x23C1, 4));   // Santa's House
            checksList.Add(new Check(this, Sys3 + 0x14B3E, Save + 0x23C2, 1));   // Rampart
            checksList.Add(new Check(this, Sys3 + 0x14B4A, Save + 0x23C2, 2));   // Rampart
            checksList.Add(new Check(this, Sys3 + 0x14B56, Save + 0x23C2, 3));   // Rampart
            checksList.Add(new Check(this, Sys3 + 0x14B62, Save + 0x23C2, 4));   // Town
            checksList.Add(new Check(this, Sys3 + 0x14B6E, Save + 0x23C2, 5));   // Town
            checksList.Add(new Check(this, Sys3 + 0x14B7A, Save + 0x23C2, 6));   // Town
            checksList.Add(new Check(this, Sys3 + 0x14B86, Save + 0x23C2, 7));   // Town
            checksList.Add(new Check(this, Sys3 + 0x14B92, Save + 0x23C3, 1));   // Isla de Muerta: Cave Mouth
            checksList.Add(new Check(this, Sys3 + 0x14B9E, Save + 0x23C3, 2));   // Isla de Muerta: Cave Mouth
            checksList.Add(new Check(this, Sys3 + 0x14BAA, Save + 0x23CA, 7));   // Isla de Muerta: Powder Store
            checksList.Add(new Check(this, Sys3 + 0x14BB6, Save + 0x23CB, 0));   // Isla de Muerta: Powder Store
            checksList.Add(new Check(this, Sys3 + 0x14BC2, Save + 0x23C3, 4));   // Isla de Muerta: Moonlight Nook
            checksList.Add(new Check(this, Sys3 + 0x14BCE, Save + 0x23C3, 5));   // Isla de Muerta: Moonlight Nook
            checksList.Add(new Check(this, Sys3 + 0x14BDA, Save + 0x23CB, 1));   // Isla de Muerta: Moonlight Nook
            checksList.Add(new Check(this, Sys3 + 0x14BE6, Save + 0x23C3, 3));   // Ship Graveyard: The Interceptor's Hold
            checksList.Add(new Check(this, Sys3 + 0x14BF2, Save + 0x23C3, 6));   // Ship Graveyard: Seadrift Keep
            checksList.Add(new Check(this, Sys3 + 0x14BFE, Save + 0x23C3, 7));   // Ship Graveyard: Seadrift Keep
            checksList.Add(new Check(this, Sys3 + 0x14C0A, Save + 0x23CB, 2));   // Ship Graveyard: Seadrift Keep
            checksList.Add(new Check(this, Sys3 + 0x14C16, Save + 0x23C4, 0));   // Ship Graveyard: Seadrift Row
            checksList.Add(new Check(this, Sys3 + 0x14C22, Save + 0x23C4, 1));   // Ship Graveyard: Seadrift Row
            checksList.Add(new Check(this, Sys3 + 0x14C2E, Save + 0x23CB, 3));   // Ship Graveyard: Seadrift Row
            checksList.Add(new Check(this, Sys3 + 0x14C3A, Save + 0x23C6, 1));   // Borough
            checksList.Add(new Check(this, Sys3 + 0x14C46, Save + 0x23C6, 2));   // Borough
            checksList.Add(new Check(this, Sys3 + 0x14C52, Save + 0x23C6, 3));   // Borough
            checksList.Add(new Check(this, Sys3 + 0x14C5E, Save + 0x23C8, 7));   // Borough
            checksList.Add(new Check(this, Sys3 + 0x14C6A, Save + 0x23DB, 1));   // Borough
            checksList.Add(new Check(this, Sys3 + 0x14C76, Save + 0x23C9, 4));   // Postern
            checksList.Add(new Check(this, Sys3 + 0x14C82, Save + 0x23C5, 4));   // Postern
            checksList.Add(new Check(this, Sys3 + 0x14C8E, Save + 0x23C5, 5));   // Postern
            checksList.Add(new Check(this, Sys3 + 0x14C9A, Save + 0x23C6, 7));   // Corridors
            checksList.Add(new Check(this, Sys3 + 0x14CA6, Save + 0x23C7, 0));   // Corridors
            checksList.Add(new Check(this, Sys3 + 0x14CB2, Save + 0x23C7, 1));   // Corridors
            checksList.Add(new Check(this, Sys3 + 0x14CBE, Save + 0x23C9, 1));   // Corridors
            checksList.Add(new Check(this, Sys3 + 0x14CCA, Save + 0x23C4, 7));   // Ansem's Study
            checksList.Add(new Check(this, Sys3 + 0x14CD6, Save + 0x23C4, 6));   // Ansem's Study
            checksList.Add(new Check(this, Sys3 + 0x14CE2, Save + 0x23C9, 3));   // Restoration Site
            checksList.Add(new Check(this, Sys3 + 0x14CEE, Save + 0x23DB, 2));   // Restoration Site
            checksList.Add(new Check(this, Sys3 + 0x14CFA, Save + 0x23C4, 2));   // Crystal Fissure
            checksList.Add(new Check(this, Sys3 + 0x14D06, Save + 0x23D9, 1));   // Crystal Fissure
            checksList.Add(new Check(this, Sys3 + 0x14D12, Save + 0x23C4, 3));   // Crystal Fissure
            checksList.Add(new Check(this, Sys3 + 0x14D1E, Save + 0x23C4, 4));   // Crystal Fissure
            checksList.Add(new Check(this, Sys3 + 0x14D2A, Save + 0x23D9, 3));   // Postern
            checksList.Add(new Check(this, Sys3 + 0x14D36, Save + 0x23C9, 5));   // Heartless Manufactory
            checksList.Add(new Check(this, Sys3 + 0x14D42, Save + 0x23D9, 4));   // Gorge
            checksList.Add(new Check(this, Sys3 + 0x14D4E, Save + 0x23CF, 0));   // Gorge
            checksList.Add(new Check(this, Sys3 + 0x14D5A, Save + 0x23CF, 1));   // Gorge
            checksList.Add(new Check(this, Sys3 + 0x14D66, Save + 0x23CE, 5));   // Elephant Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14D72, Save + 0x23CE, 6));   // Elephant Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14D7E, Save + 0x23CE, 7));   // Elephant Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14D8A, Save + 0x23DB, 3));   // Elephant Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14D96, Save + 0x23DB, 4));   // Elephant Graveyard
            checksList.Add(new Check(this, Sys3 + 0x14DA2, Save + 0x23D0, 3));   // Pride Rock
            checksList.Add(new Check(this, Sys3 + 0x14DAE, Save + 0x23CD, 4));   // Pride Rock
            checksList.Add(new Check(this, Sys3 + 0x14DBA, Save + 0x23CD, 5));   // Pride Rock
            checksList.Add(new Check(this, Sys3 + 0x14DC6, Save + 0x23CE, 0));   // Wildebeest Valley
            checksList.Add(new Check(this, Sys3 + 0x14DD2, Save + 0x23CE, 1));   // Wildebeest Valley
            checksList.Add(new Check(this, Sys3 + 0x14DDE, Save + 0x23CE, 2));   // Wildebeest Valley
            checksList.Add(new Check(this, Sys3 + 0x14DEA, Save + 0x23CE, 3));   // Wildebeest Valley
            checksList.Add(new Check(this, Sys3 + 0x14DF6, Save + 0x23CE, 4));   // Wildebeest Valley
            checksList.Add(new Check(this, Sys3 + 0x14E02, Save + 0x23CF, 2));   // Wastelands
            checksList.Add(new Check(this, Sys3 + 0x14E0E, Save + 0x23CF, 3));   // Wastelands
            checksList.Add(new Check(this, Sys3 + 0x14E1A, Save + 0x23CF, 4));   // Wastelands
            checksList.Add(new Check(this, Sys3 + 0x14E26, Save + 0x23CF, 5));   // Jungle
            checksList.Add(new Check(this, Sys3 + 0x14E32, Save + 0x23CF, 6));   // Jungle
            checksList.Add(new Check(this, Sys3 + 0x14E3E, Save + 0x23CF, 7));   // Jungle
            checksList.Add(new Check(this, Sys3 + 0x14E4A, Save + 0x23D0, 0));   // Oasis
            checksList.Add(new Check(this, Sys3 + 0x14E56, Save + 0x23D9, 5));   // Oasis
            checksList.Add(new Check(this, Sys3 + 0x14E62, Save + 0x23D0, 1));   // Oasis
            checksList.Add(new Check(this, Sys3 + 0x14E6E, Save + 0x23CA, 1));   // Station of Serenity
            checksList.Add(new Check(this, Sys3 + 0x14E7A, Save + 0x23D7, 1));   // Station of Calling
            checksList.Add(new Check(this, Sys3 + 0x14E86, Save + 0x23D1, 5));   // Central Station (STT)
            checksList.Add(new Check(this, Sys3 + 0x14E92, Save + 0x23D1, 6));   // Central Station (STT)
            checksList.Add(new Check(this, Sys3 + 0x14E9E, Save + 0x23D1, 7));   // Central Station (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EAA, Save + 0x23D2, 3));   // Sunset Terrace (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EB6, Save + 0x23D2, 4));   // Sunset Terrace (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EC2, Save + 0x23D2, 5));   // Sunset Terrace (STT)
            checksList.Add(new Check(this, Sys3 + 0x14ECE, Save + 0x23D2, 6));   // Sunset Terrace (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EDA, Save + 0x23D4, 2));   // Mansion: Foyer (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EE6, Save + 0x23D4, 3));   // Mansion: Foyer (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EF2, Save + 0x23D4, 4));   // Mansion: Foyer (STT)
            checksList.Add(new Check(this, Sys3 + 0x14EFE, Save + 0x23D5, 0));   // Mansion: Dining Room (STT)
            checksList.Add(new Check(this, Sys3 + 0x14F0A, Save + 0x23D5, 1));   // Mansion: Dining Room (STT)
            checksList.Add(new Check(this, Sys3 + 0x14F16, Save + 0x23D5, 4));   // Mansion: Library (STT)
            checksList.Add(new Check(this, Sys3 + 0x14F22, Save + 0x23D6, 0));   // Mansion: Basement Corridor (STT)
            checksList.Add(new Check(this, Sys3 + 0x14F2E, Save + 0x23D4, 0));   // The Old Mansion
            checksList.Add(new Check(this, Sys3 + 0x14F3A, Save + 0x23D4, 1));   // The Old Mansion
            checksList.Add(new Check(this, Sys3 + 0x14F46, Save + 0x23D3, 3));   // The Woods
            checksList.Add(new Check(this, Sys3 + 0x14F52, Save + 0x23D3, 4));   // The Woods
            checksList.Add(new Check(this, Sys3 + 0x14F5E, Save + 0x23D3, 5));   // The Woods
            checksList.Add(new Check(this, Sys3 + 0x14F6A, Save + 0x23D0, 5));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14F76, Save + 0x23D0, 6));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14F82, Save + 0x23D0, 7));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14F8E, Save + 0x23D1, 0));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14F9A, Save + 0x23D1, 1));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14FA6, Save + 0x23D1, 2));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14FB2, Save + 0x23D8, 5));   // Tram Common
            checksList.Add(new Check(this, Sys3 + 0x14FBE, Save + 0x23D2, 0));   // Central Station (TT)
            checksList.Add(new Check(this, Sys3 + 0x14FCA, Save + 0x23D2, 1));   // Central Station (TT)
            checksList.Add(new Check(this, Sys3 + 0x14FD6, Save + 0x23D2, 2));   // Central Station (TT)
            checksList.Add(new Check(this, Sys3 + 0x14FE2, Save + 0x23D6, 2));   // The Tower
            checksList.Add(new Check(this, Sys3 + 0x14FEE, Save + 0x23D6, 3));   // The Tower
            checksList.Add(new Check(this, Sys3 + 0x14FFA, Save + 0x23DB, 7));   // The Tower
            checksList.Add(new Check(this, Sys3 + 0x15006, Save + 0x23D6, 4));   // Tower: Entryway
            checksList.Add(new Check(this, Sys3 + 0x15012, Save + 0x23D6, 5));   // Tower: Entryway
            checksList.Add(new Check(this, Sys3 + 0x1501E, Save + 0x23D6, 6));   // Tower: Sorcerer's Loft
            checksList.Add(new Check(this, Sys3 + 0x1502A, Save + 0x23D6, 7));   // Tower: Wardrobe
            checksList.Add(new Check(this, Sys3 + 0x15036, Save + 0x23D8, 0));   // Underground Concourse
            checksList.Add(new Check(this, Sys3 + 0x15042, Save + 0x23D8, 1));   // Underground Concourse
            checksList.Add(new Check(this, Sys3 + 0x1504E, Save + 0x23D8, 2));   // Underground Concourse
            checksList.Add(new Check(this, Sys3 + 0x1505A, Save + 0x23D8, 3));   // Underground Concourse
            checksList.Add(new Check(this, Sys3 + 0x15066, Save + 0x23D7, 6));   // Tunnelway
            checksList.Add(new Check(this, Sys3 + 0x15072, Save + 0x23D7, 7));   // Tunnelway
            checksList.Add(new Check(this, Sys3 + 0x1507E, Save + 0x23D2, 7));   // Sunset Terrace (TT)
            checksList.Add(new Check(this, Sys3 + 0x1508A, Save + 0x23D3, 0));   // Sunset Terrace (TT)
            checksList.Add(new Check(this, Sys3 + 0x15096, Save + 0x23D3, 1));   // Sunset Terrace (TT)
            checksList.Add(new Check(this, Sys3 + 0x150A2, Save + 0x23D3, 2));   // Sunset Terrace (TT)
            checksList.Add(new Check(this, Sys3 + 0x150AE, Save + 0x23D4, 5));   // Mansion: Foyer (TT)
            checksList.Add(new Check(this, Sys3 + 0x150BA, Save + 0x23D4, 6));   // Mansion: Foyer (TT)
            checksList.Add(new Check(this, Sys3 + 0x150C6, Save + 0x23D4, 7));   // Mansion: Foyer (TT)
            checksList.Add(new Check(this, Sys3 + 0x150D2, Save + 0x23D5, 2));   // Mansion: Dining Room (TT)
            checksList.Add(new Check(this, Sys3 + 0x150DE, Save + 0x23D5, 3));   // Mansion: Dining Room (TT)
            checksList.Add(new Check(this, Sys3 + 0x150EA, Save + 0x23D5, 5));   // Mansion: Library (TT)
            checksList.Add(new Check(this, Sys3 + 0x150F6, Save + 0x23D6, 1));   // Mansion: Basement Corridor (TT)
            checksList.Add(new Check(this, Sys3 + 0x15102, Save + 0x23CB, 4));   // Fragment Crossing
            checksList.Add(new Check(this, Sys3 + 0x1510E, Save + 0x23CB, 5));   // Fragment Crossing
            checksList.Add(new Check(this, Sys3 + 0x1511A, Save + 0x23CB, 6));   // Fragment Crossing
            checksList.Add(new Check(this, Sys3 + 0x15126, Save + 0x23CB, 7));   // Fragment Crossing
            checksList.Add(new Check(this, Sys3 + 0x15132, Save + 0x23CD, 3));   // Memory's Skyscraper
            checksList.Add(new Check(this, Sys3 + 0x1513E, Save + 0x23DC, 0));   // Memory's Skyscraper
            checksList.Add(new Check(this, Sys3 + 0x1514A, Save + 0x23DC, 1));   // Memory's Skyscraper
            checksList.Add(new Check(this, Sys3 + 0x15156, Save + 0x23CA, 5));   // The Brink of Despair
            checksList.Add(new Check(this, Sys3 + 0x15162, Save + 0x23DA, 2));   // The Brink of Despair
            checksList.Add(new Check(this, Sys3 + 0x1516E, Save + 0x23CC, 0));   // Nothing's Call
            checksList.Add(new Check(this, Sys3 + 0x1517A, Save + 0x23CC, 1));   // Nothing's Call
            checksList.Add(new Check(this, Sys3 + 0x15186, Save + 0x23CA, 6));   // Twilight's View
            checksList.Add(new Check(this, Sys3 + 0x15192, Save + 0x23CC, 2));   // Naught's Skyway
            checksList.Add(new Check(this, Sys3 + 0x1519E, Save + 0x23CC, 3));   // Naught's Skyway
            checksList.Add(new Check(this, Sys3 + 0x151AA, Save + 0x23CC, 4));   // Naught's Skyway
            checksList.Add(new Check(this, Sys3 + 0x151B6, Save + 0x23CC, 7));   // Ruin and Creation's Passage
            checksList.Add(new Check(this, Sys3 + 0x151C2, Save + 0x23CD, 0));   // Ruin and Creation's Passage
            checksList.Add(new Check(this, Sys3 + 0x151CE, Save + 0x23CD, 1));   // Ruin and Creation's Passage
            checksList.Add(new Check(this, Sys3 + 0x151DA, Save + 0x23CD, 2));   // Ruin and Creation's Passage
            checksList.Add(new Check(this, Sys3 + 0x151E6, Save + 0x23DC, 2));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x151F2, Save + 0x23DC, 3));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x151FE, Save + 0x23DC, 4));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x1520A, Save + 0x23DC, 5));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x15216, Save + 0x23DC, 6));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x15222, Save + 0x23DC, 7));   // Cavern of Remembrance: Depths
            checksList.Add(new Check(this, Sys3 + 0x1522E, Save + 0x23DD, 0));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x1523A, Save + 0x23DD, 1));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x15246, Save + 0x23DD, 2));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x15252, Save + 0x23DD, 3));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x1525E, Save + 0x23DD, 4));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x1526A, Save + 0x23DD, 5));   // Cavern of Remembrance: Mining Area
            checksList.Add(new Check(this, Sys3 + 0x15276, Save + 0x23DD, 6));   // Cavern of Remembrance: Engine Chamber
            checksList.Add(new Check(this, Sys3 + 0x15282, Save + 0x23DD, 7));   // Cavern of Remembrance: Engine Chamber
            checksList.Add(new Check(this, Sys3 + 0x1528E, Save + 0x23DE, 0));   // Cavern of Remembrance: Engine Chamber
            checksList.Add(new Check(this, Sys3 + 0x1529A, Save + 0x23DE, 1));   // Cavern of Remembrance: Engine Chamber
            checksList.Add(new Check(this, Sys3 + 0x152A6, Save + 0x23DE, 2));   // Cavern of Remembrance: Mineshaft
            checksList.Add(new Check(this, Sys3 + 0x152B2, Save + 0x23DE, 3));   // Cavern of Remembrance: Mineshaft
            checksList.Add(new Check(this, Sys3 + 0x152BE, Save + 0x23DE, 4));   // Cavern of Remembrance: Mineshaft
            checksList.Add(new Check(this, Sys3 + 0x152CA, Save + 0x23DE, 5));   // Cavern of Remembrance: Mineshaft
            checksList.Add(new Check(this, Sys3 + 0x152D6, Save + 0x23DE, 6));   // Cavern of Remembrance: Mineshaft
            checksList.Add(new Check(this, Sys3 + 0x152E2, Save + 0x23DF, 1));   // Garden of Assemblage
            checksList.Add(new Check(this, Sys3 + 0x152EE, Save + 0x23DF, 2));   // Garden of Assemblage
            checksList.Add(new Check(this, Sys3 + 0x152FA, Save + 0x23DF, 3));   // Garden of Assemblage
            #endregion

            #region Bonus
            // Bonus (120)
            checksList.Add(new Check(this, Btl0 + 0x2A9D8, Save + 0x3704, 2));   // Thresholder - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2A9DA, Save + 0x3704, 2));   // Thresholder - Slot 2    (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AA08, Save + 0x3704, 3));   // Dark Thorn - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AA0A, Save + 0x3704, 3));   // Dark Thorn - Slot 2     (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AA48, Save + 0x3704, 4));   // Xaldin - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AA4A, Save + 0x3704, 4));   // Xaldin - Slot 2         (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AA88, Save + 0x3704, 5));   // Cerberus - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AA8A, Save + 0x3704, 5));   // Cerberus - Slot 2       (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AAA8, Save + 0x3704, 6));   // Pete I - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AAAA, Save + 0x3704, 6));   // Pete I - Slot 2         (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AAD8, Save + 0x3704, 7));   // Hydra - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AADA, Save + 0x3704, 7));   // Hydra - Slot 2          (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AB08, Save + 0x3705, 0));   // Hades - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AB0A, Save + 0x3705, 0));   // Hades - Slot 2          (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AB48, Save + 0x3705, 1));   // Shan Yu - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AB48, Save + 0x3705, 1));   // Shan Yu - Slot 2        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AB88, Save + 0x3705, 2));   // Storm Rider - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AB8A, Save + 0x3705, 2));   // Storm Rider - Slot 2    (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ABC8, Save + 0x3705, 4));   // Beast - Slot 1          (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ABCA, Save + 0x3705, 4));   // Beast - Slot 2          (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ABF8, Save + 0x3705, 7));   // Genie Jafar - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2ABFA, Save + 0x3705, 7));   // Genie Jafar - Slot 2    (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AC08, Save + 0x3706, 0));   // Boat Pete - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AC0A, Save + 0x3706, 0));   // Boat Pete - Slot 2      (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AC38, Save + 0x3706, 1));   // Pete II - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AC3A, Save + 0x3706, 1));   // Pete II - Slot 2        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AC68, Save + 0x3706, 2));   // Prison Keeper - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AC6A, Save + 0x3706, 2));   // Prison Keeper - Slot 2  (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ACA8, Save + 0x3706, 3));   // Oogie Boogie - Slot 1   (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ACAA, Save + 0x3706, 3));   // Oogie Boogie - Slot 2   (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ACE8, Save + 0x3706, 4));   // The Experiment - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ACEA, Save + 0x3706, 4));   // The Experiment - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AD28, Save + 0x3706, 5));   // Barbossa - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AD2A, Save + 0x3706, 5));   // Barbossa - Slot 2       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AD68, Save + 0x3706, 6));   // Grim Reaper II - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AD6A, Save + 0x3706, 6));   // Grim Reaper II - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ADA8, Save + 0x3706, 7));   // Xigbar - Slot 1         (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ADAA, Save + 0x3706, 7));   // Xigbar - Slot 2         (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2ADD8, Save + 0x3707, 0));   // Luxord - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2ADDA, Save + 0x3707, 0));   // Luxord - Slot 2         (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ADE8, Save + 0x3707, 1));   // Saïx - Slot 1           (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2ADEA, Save + 0x3707, 1));   // Saïx - Slot 2           (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AE18, Save + 0x3707, 2));   // Xemnas I - Slot 1       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AE1A, Save + 0x3707, 2));   // Xemnas I - Slot 2       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AE28, Save + 0x3707, 4));   // Demyx II - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AE2A, Save + 0x3707, 4));   // Demyx II - Slot 2       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AE58, Save + 0x3707, 5));   // Scar - Slot 1           (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AE5A, Save + 0x3707, 5));   // Scar - Slot 2           (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AE98, Save + 0x3707, 6));   // Groundshaker - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AE9A, Save + 0x3707, 6));   // Groundshaker - Slot 2   (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AEB8, Save + 0x3707, 7));   // Hostile Program - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AEBA, Save + 0x3707, 7));   // Hostile Program - Slot 2 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AEF8, Save + 0x3708, 0));   // MCP - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AEFA, Save + 0x3708, 0));   // MCP - Slot 2            (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AF38, Save + 0x3708, 1));   // Twilight Thorn - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AF3A, Save + 0x3708, 1));   // Twilight Thorn - Slot 2 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AF48, Save + 0x3708, 2));   // Axel II - Slot 1        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AF4A, Save + 0x3708, 2));   // Axel II - Slot 2        (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AF58, Save + 0x3708, 3));   // Sephiroth - Slot 1      (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AF5A, Save + 0x3708, 3));   // Sephiroth - Slot 2      (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AF68, Save + 0x3708, 5));   // Volcanic Lord & Blizzard Lord - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AF6A, Save + 0x3708, 5));   // Volcanic Lord & Blizzard Lord - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AFA8, Save + 0x3708, 6));   // Queen Minnie Escort - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2AFAA, Save + 0x3708, 6));   // Queen Minnie Escort - Slot 2 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AFB8, Save + 0x3708, 7));   // The Interceptor Barrels - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AFBA, Save + 0x3708, 7));   // The Interceptor Barrels - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2AFF8, Save + 0x3709, 0));   // Lock, Shock & Barrel - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2AFFA, Save + 0x3709, 0));   // Lock, Shock & Barrel - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B038, Save + 0x3709, 2));   // Abu Escort - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B03A, Save + 0x3709, 2));   // Abu Escort - Slot 2     (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B078, Save + 0x3709, 3));   // Village Cave Heartless - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B07A, Save + 0x3709, 3));   // Village Cave Heartless - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B098, Save + 0x3709, 5));   // Dataspace Monitors - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B09A, Save + 0x3709, 5));   // Dataspace Monitors - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B0D8, Save + 0x3709, 6));   // Treasure Room Heartless - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B0DA, Save + 0x3709, 6));   // Treasure Room Heartless - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B118, Save + 0x3709, 7));   // Bailey Nobodies - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B11A, Save + 0x3709, 7));   // Bailey Nobodies - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B128, Save + 0x370A, 1));   // Hyenas I - Slot 1       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B12A, Save + 0x370A, 1));   // Hyenas I - Slot 2       (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B158, Save + 0x370A, 2));   // Hyenas II - Slot 1      (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B15A, Save + 0x370A, 2));   // Hyenas II - Slot 2      (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B228, Save + 0x370A, 6));   // Station of Serenity Nobodies - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B22A, Save + 0x370A, 6));   // Station of Serenity Nobodies - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B238, Save + 0x370B, 0));   // The Old Mansion Nobodies - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B23A, Save + 0x370B, 0));   // The Old Mansion Nobodies - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B268, Save + 0x370B, 1));   // Phil's Training - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B26A, Save + 0x370B, 1));   // Phil's Training - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B278, Save + 0x370B, 2));   // Demyx I - Slot 1        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B27A, Save + 0x370B, 2));   // Demyx I - Slot 2        (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B2A8, Save + 0x370B, 3));   // Grim Reaper I - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B2AA, Save + 0x370B, 3));   // Grim Reaper I - Slot 2  (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B2E8, Save + 0x370B, 4));   // 1000 Heartless - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B2EA, Save + 0x370B, 4));   // 1000 Heartless - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B2F8, Save + 0x370B, 5));   // Solar Sailer Heartless - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B2FA, Save + 0x370B, 5));   // Solar Sailer Heartless - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B338, Save + 0x370B, 6));   // The Interceptor Pirates - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B33A, Save + 0x370B, 6));   // The Interceptor Pirates - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B368, Save + 0x370B, 7));   // Betwixt and Between Nobodies - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B36A, Save + 0x370B, 7));   // Betwixt and Between Nobodies - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B378, Save + 0x370C, 0));   // Vexen - Slot 1          (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B37A, Save + 0x370C, 0));   // Vexen - Slot 2          (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B3A8, Save + 0x370C, 1));   // Lexaeus - Slot 1        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B3AA, Save + 0x370C, 1));   // Lexaeus - Slot 2        (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B3D8, Save + 0x370C, 2));   // Zexion - Slot 1         (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B3DA, Save + 0x370C, 2));   // Zexion - Slot 2         (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B408, Save + 0x370C, 3));   // Marluxia - Slot 1       (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B40A, Save + 0x370C, 3));   // Marluxia - Slot 2       (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B438, Save + 0x370C, 4));   // Larxene - Slot 1        (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B43A, Save + 0x370C, 4));   // Larxene - Slot 2        (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B468, Save + 0x370C, 5));   // Roxas - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B46A, Save + 0x370C, 5));   // Roxas - Slot 2          (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B478, Save + 0x370C, 6));   // Lingering Will - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B47A, Save + 0x370C, 6));   // Lingering Will - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B4A8, Save + 0x370C, 7));   // Xemnas II - Slot 1      (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B4AA, Save + 0x370C, 7));   // Xemnas II - Slot 2      (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B4C8, Save + 0x370D, 0));   // Transport to Remembrance Nobodies III - Slot 1 (Statsanity)
            checksList.Add(new Check(this, Btl0 + 0x2B4CA, Save + 0x370D, 0));   // Transport to Remembrance Nobodies III - Slot 2 (Plando)
            checksList.Add(new Check(this, Btl0 + 0x2B4F8, Save + 0x370D, 1));   // Axel I - Slot 1
            checksList.Add(new Check(this, Btl0 + 0x2B4FA, Save + 0x370D, 1));   // Axel I - Slot 2         (Plando)
            #endregion

            #region popup
            // Popup (92, excluding AS and Data) (using corresponding progress flags)
            checksList.Add(new Check(this, Sys3 + 0x15606, Save + 0x1DB4, 6));   // Sweet Memories
            checksList.Add(new Check(this, Sys3 + 0x15612, Save + 0x1DB4, 6));   // Spooky Cave Map
            checksList.Add(new Check(this, Sys3 + 0x1561E, Save + 0x1DB5, 5));   // Starry Hill Cure
            checksList.Add(new Check(this, Sys3 + 0x1562A, Save + 0x1DB5, 5));   // Starry Hill Orichalcum+
            checksList.Add(new Check(this, Sys3 + 0x15492, Save + 0x1D74, 4));   // Agrabah Map
            checksList.Add(new Check(this, Sys3 + 0x1549E, Save + 0x1D72, 4));   // Lamp Charm
            checksList.Add(new Check(this, Sys3 + 0x155B2, Save + 0x1D77, 3));   // Wishing Lamp
            checksList.Add(new Check(this, Sys3 + 0x15546, Save + 0x1DF4, 2));   // Undersea Kingdom Map
            checksList.Add(new Check(this, Sys3 + 0x155E2, Save + 0x1DF5, 3));   // Mysterious Abyss
            checksList.Add(new Check(this, Sys3 + 0x155EE, Save + 0x1DF4, 1));   // Last Song Blizzard
            checksList.Add(new Check(this, Sys3 + 0x155FA, Save + 0x1DF4, 1));   // Last Song Orichalcum+
            checksList.Add(new Check(this, Sys3 + 0x153F6, Save + 0x1D32, 5));   // Dark Thorn Cure
            checksList.Add(new Check(this, Sys3 + 0x15552, Save + 0x1D39, 0));   // Rumbling Rose
            checksList.Add(new Check(this, Sys3 + 0x1555E, Save + 0x1D39, 0));   // Castle Walls Map
            checksList.Add(new Check(this, Sys3 + 0x156C6, Save + 0x1D31, 2));   // Ansem Report 4
            checksList.Add(new Check(this, Sys3 + 0x15456, Save + 0x1E10, 4));   // Disney Castle Map
            checksList.Add(new Check(this, Sys3 + 0x1546E, Save + 0x1E32, 4));   // Window of Time Map
            checksList.Add(new Check(this, Sys3 + 0x15432, Save + 0x1E33, 2));   // Monochrome
            checksList.Add(new Check(this, Sys3 + 0x15462, Save + 0x1E11, 7));   // Wisdom Form
            checksList.Add(new Check(this, Sys3 + 0x15816, Save + 0x1E14, 2));   // Proof of Connection (sometimes slightly early)
            checksList.Add(new Check(this, Sys3 + 0x15822, Save + 0x1E14, 2));   // Manifest Illusion (early)
            checksList.Add(new Check(this, Sys3 + 0x154AA, Save + 0x1E53, 2));   // Oogie Magnet
            checksList.Add(new Check(this, Sys3 + 0x155BE, Save + 0x1E55, 1));   // Present
            checksList.Add(new Check(this, Sys3 + 0x155CA, Save + 0x1E55, 4));   // Decoy Presents
            checksList.Add(new Check(this, Sys3 + 0x155D6, Save + 0x1E56, 0));   // Decisive Pumpkin
            checksList.Add(new Check(this, Sys3 + 0x1538A, Save + 0x3DA9, 7));   // Marketplace Map (Random flag? PS2:32F8D9(80))
            checksList.Add(new Check(this, Sys3 + 0x15396, Save + 0x1D10, 6));   // Membership Card
            checksList.Add(new Check(this, Sys3 + 0x153A2, Save + 0x1D10, 6));   // Merlin Blizzard
            checksList.Add(new Check(this, Sys3 + 0x156EA, Save + 0x1D18, 3));   // Ansem Report 7 (late)
            checksList.Add(new Check(this, Sys3 + 0x15402, Save + 0x3DAA, 4));   // Baseball Charm (Random flag? PS2:32F8DA(10))
            checksList.Add(new Check(this, Sys3 + 0x1550A, Save + 0x1D12, 6));   // Master Form
            checksList.Add(new Check(this, Sys3 + 0x15636, Save + 0x1D1D, 4));   // Sleeping Lion
            checksList.Add(new Check(this, Sys3 + 0x15522, Save + 0x1D14, 6));   // FF Fights Cure
            checksList.Add(new Check(this, Sys3 + 0x156A2, Save + 0x1D19, 3));   // Ansem Report 1
            checksList.Add(new Check(this, Sys3 + 0x1552E, Save + 0x1D23, 0));   // Ice Cream
            checksList.Add(new Check(this, Sys3 + 0x1553A, Save + 0x1D23, 0));   // Picture
            checksList.Add(new Check(this, Sys3 + 0x1582E, Save + 0x1D27, 5));   // Winner's Proof
            checksList.Add(new Check(this, Sys3 + 0x1583A, Save + 0x1D27, 5));   // Proof of Peace
            checksList.Add(new Check(this, Sys3 + 0x15642, Save + 0x1D1F, 7));   // Fenrir
            checksList.Add(new Check(this, Sys3 + 0x153D2, Save + 0x1D94, 6));   // Encampment Area Map
            checksList.Add(new Check(this, Sys3 + 0x153C6, Save + 0x1D96, 0));   // Mission 3 AP Boost
            checksList.Add(new Check(this, Sys3 + 0x153DE, Save + 0x1D96, 6));   // Village Area Map
            checksList.Add(new Check(this, Sys3 + 0x153EA, Save + 0x1D92, 2));   // Hidden Dragon
            checksList.Add(new Check(this, Sys3 + 0x1540E, Save + 0x1D5A, 4));   // Coliseum Map
            checksList.Add(new Check(this, Sys3 + 0x1541A, Save + 0x1D5B, 3));   // Olympus Stone
            checksList.Add(new Check(this, Sys3 + 0x156D2, Save + 0x1D5B, 3));   // Ansem Report 5
            checksList.Add(new Check(this, Sys3 + 0x15426, Save + 0x1D55, 7));   // Hero's Crest
            checksList.Add(new Check(this, Sys3 + 0x15582, Save + 0x1D5F, 2));   // Auron's Statue
            checksList.Add(new Check(this, Sys3 + 0x1558E, Save + 0x1D56, 5));   // Guardian Soul
            checksList.Add(new Check(this, Sys3 + 0x1543E, Save + 0x1D57, 6));   // Pain & Panic Cup Protect Belt (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x1544A, Save + 0x1D57, 6));   // Pain & Panic Cup Serenity Gem (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x154CE, Save + 0x1D58, 0));   // Cerberus Cup Rising Dragon (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x154DA, Save + 0x1D58, 0));   // Cerberus Cup Serenity Crystal (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x1559A, Save + 0x1D58, 1));   // Titan Cup Genji Shield (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x155A6, Save + 0x1D58, 2));   // Titan Cup Skillful Ring (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x1564E, Save + 0x1D58, 4));   // Goddess of Fate Cup Fatal Crest (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x1565A, Save + 0x1D58, 4));   // Goddess of Fate Cup Orichalcum+ (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x15696, Save + 0x1D5A, 1));   // Hades Cup Trophy (slightly early)
            checksList.Add(new Check(this, Sys3 + 0x1547A, Save + 0x1E92, 4));   // Isla de Muerta Map
            checksList.Add(new Check(this, Sys3 + 0x15486, Save + 0x1E93, 6));   // Follow the Wind
            checksList.Add(new Check(this, Sys3 + 0x1556A, Save + 0x1E95, 2));   // Cursed Medallion
            checksList.Add(new Check(this, Sys3 + 0x15576, Save + 0x1E95, 2));   // Ship Graveyard Map
            checksList.Add(new Check(this, Sys3 + 0x156DE, Save + 0x1E95, 7));   // Ansem Report 6
            checksList.Add(new Check(this, Sys3 + 0x154B6, Save + 0x1DD2, 1));   // Circle of Life
            checksList.Add(new Check(this, Sys3 + 0x154C2, Save + 0x1DD4, 7));   // Scar Fire
            checksList.Add(new Check(this, Sys3 + 0x15336, Save + 0x1CD6, 3));   // Twilight Town Map
            checksList.Add(new Check(this, Sys3 + 0x15306, Save + 0x1CD6, 5));   // Olette Munny Pouch
            checksList.Add(new Check(this, Sys3 + 0x15312, Save + 0x1CDC, 2));   // Champion Belt
            checksList.Add(new Check(this, Sys3 + 0x1531E, Save + 0x1CDC, 2));   // Medal
            checksList.Add(new Check(this, Sys3 + 0x1532A, Save + 0x1CDC, 2));   // "The Struggle" Trophy
            checksList.Add(new Check(this, Sys3 + 0x15342, Save + 0x1CE0, 6));   // Namine's Sketches
            checksList.Add(new Check(this, Sys3 + 0x1534E, Save + 0x1CE0, 6));   // Mansion Map
            checksList.Add(new Check(this, Sys3 + 0x15516, Save + 0x1EB2, 3));   // Photon Debugger
            checksList.Add(new Check(this, Sys3 + 0x1535A, Save + 0x1CE3, 7));   // Mickey Munny Pouch
            checksList.Add(new Check(this, Sys3 + 0x15366, Save + 0x1CE3, 7));   // Crystal Orb
            checksList.Add(new Check(this, Sys3 + 0x156AE, Save + 0x1CE3, 7));   // Ansem Report 2
            checksList.Add(new Check(this, Sys3 + 0x15372, Save + 0x1CE5, 2));   // Star Seeker
            checksList.Add(new Check(this, Sys3 + 0x1537E, Save + 0x1CE5, 2));   // Valor Form
            checksList.Add(new Check(this, Sys3 + 0x154E6, Save + 0x1CE6, 4));   // Seifer's Trophy
            checksList.Add(new Check(this, Sys3 + 0x154F2, Save + 0x1CE6, 7));   // Oathkeeper (early)
            checksList.Add(new Check(this, Sys3 + 0x154FE, Save + 0x1CE6, 7));   // Limit Form (early)
            checksList.Add(new Check(this, Sys3 + 0x1570E, Save + 0x1CE8, 3));   // Ansem Report 10
            checksList.Add(new Check(this, Sys3 + 0x15666, Save + 0x1CE9, 1));   // Bond of Flame
            checksList.Add(new Check(this, Sys3 + 0x15672, Save + 0x1ED1, 1));   // Two Become One
            checksList.Add(new Check(this, Sys3 + 0x156F6, Save + 0x1ED1, 1));   // Ansem Report 8
            checksList.Add(new Check(this, Sys3 + 0x156BA, Save + 0x1ED2, 2));   // Ansem Report 3
            checksList.Add(new Check(this, Sys3 + 0x1567E, Save + 0x1ED2, 4));   // Oblivion
            checksList.Add(new Check(this, Sys3 + 0x1568A, Save + 0x1ED2, 4));   // Castle That Never Was Map
            checksList.Add(new Check(this, Sys3 + 0x15702, Save + 0x1ED2, 7));   // Ansem Report 9
            checksList.Add(new Check(this, Sys3 + 0x15726, Save + 0x1ED3, 2));   // Ansem Report 12 (early)
            checksList.Add(new Check(this, Sys3 + 0x1571A, Save + 0x1ED3, 6));   // Ansem Report 11
            checksList.Add(new Check(this, Sys3 + 0x15732, Save + 0x1ED4, 5));   // Ansem Report 13
            #endregion

            // Popup for Absent Silhouette and Data (using defeat cutscene IDs) (18)
            checksList.Add(new Check(this, Sys3 + 0x1573E, World, "HalloweenTown",          0x20, 0x79));   // AS Vexen
            checksList.Add(new Check(this, Sys3 + 0x1577A, World, "HalloweenTown",          0x20, 0x79));   // Data Vexen
            checksList.Add(new Check(this, Sys3 + 0x1574A, World, "Agrabah",                0x21, 0x7B));   // AS Lexaeus
            checksList.Add(new Check(this, Sys3 + 0x15786, World, "Agrabah",                0x21, 0x7B));   // Data Lexaeus
            checksList.Add(new Check(this, Sys3 + 0x15756, World, "OlympusColiseum",        0x22, 0x7D));   // AS Zexion
            checksList.Add(new Check(this, Sys3 + 0x15792, World, "OlympusColiseum",        0x22, 0x7D));   // Data Zexion
            checksList.Add(new Check(this, Sys3 + 0x1576E, World, "DisneyCastle",           0x26, 0x7F));   // AS Marluxia
            checksList.Add(new Check(this, Sys3 + 0x157AA, World, "DisneyCastle",           0x26, 0x7F));   // Data Marluxia
            checksList.Add(new Check(this, Sys3 + 0x15762, World, "SpaceParanoids",         0x21, 0x81));   // AS Larxene
            checksList.Add(new Check(this, Sys3 + 0x1579E, World, "SpaceParanoids",         0x21, 0x81));   // Data Larxene
            checksList.Add(new Check(this, Sys3 + 0x157DA, World, "TWTNW",                  0x14, 0x6A));   // Data Xemnas
            checksList.Add(new Check(this, Sys3 + 0x157E6, World, "LandofDragons",          0x0A, 0x6C));   // Data Xigbar
            checksList.Add(new Check(this, Sys3 + 0x157C2, World, "BeastsCastle",           0x0F, 0x63));   // Data Xaldin
            checksList.Add(new Check(this, Sys3 + 0x157F2, World, "PrideLands",             0x0F, 0x6E));   // Data Saix
            checksList.Add(new Check(this, Sys3 + 0x157CE, World, "TwilightTown",           0x14, 0xD4));   // Data Axel
            checksList.Add(new Check(this, Sys3 + 0x157B6, World, "HollowBastion",          0x04, 0x8D));   // Data Demyx
            checksList.Add(new Check(this, Sys3 + 0x157FE, World, "PortRoyal",              0x0E, 0x70));   // Data Luxord
            checksList.Add(new Check(this, Sys3 + 0x1580A, World, "SimulatedTwilightTown",  0x15, 0x72));   // Data Roxas

            // Handle Sora levels and Drive levels separately, since Rewards already scans through them
        }

        public bool TrackCheck(int targetValue)
        {
            target = targetValue;
            FindTargetCheck();
            return UpdateTargetMemory();
        }

        private void FindTargetCheck()
        {
            targetCheck = new List<Check>();
            foreach (Check check in checksList)
            {
                byte[] checkValueBytes = memory.ReadMemory(check.addrCheck + ADDRESS_OFFSET, 2);
                int checkValue = BitConverter.ToInt16(checkValueBytes, 0);
                if (target == checkValue)
                {
                    targetCheck.Add(check);
                }
            }

            // Search through levels, handled separately using Rewards
            onLevels = false;
            onValor = false;
            onWisdom = false;
            onLimit = false;
            onMaster = false;
            onFinal = false;
            targetLevel = null;
            targetDriveLevel = null;
            if (MainWindow.data.codes.itemCodes.ContainsKey(target))
            {
                string checkName = MainWindow.data.codes.itemCodes[target];

                // Sora levels
                int swordLevel = 100;
                int shieldLevel = 100;
                int staffLevel = 100;
                Tuple<int, string> swordReward = Rewards.GetLevelRewards("Sword").FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> shieldReward = Rewards.GetLevelRewards("Shield").FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> staffReward = Rewards.GetLevelRewards("Staff").FirstOrDefault(reward => reward.Item2 == checkName);
                if (swordReward != null)
                {
                    swordLevel = swordReward.Item1;
                }
                if (shieldReward != null)
                {
                    shieldLevel = shieldReward.Item1;
                }
                if (staffReward != null)
                {
                    staffLevel = staffReward.Item1;
                }
                if (!(swordReward == null && shieldReward == null && staffReward == null))
                {
                    targetLevel = new Dictionary<string, int>
                    {
                        {"Sword", swordLevel},
                        {"Shield", shieldLevel},
                        {"Staff", staffLevel}
                    };
                    onLevels = true;
                }

                // Drive levels
                Tuple<int, string> valorReward = Rewards.valorChecks.FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> wisdomReward = Rewards.wisdomChecks.FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> limitReward = Rewards.limitChecks.FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> masterReward = Rewards.masterChecks.FirstOrDefault(reward => reward.Item2 == checkName);
                Tuple<int, string> finalReward = Rewards.finalChecks.FirstOrDefault(reward => reward.Item2 == checkName);
                if (!(valorReward == null && wisdomReward == null && limitReward == null && masterReward == null && finalReward == null))
                {
                    targetDriveLevel = new Dictionary<string, int>();
                    if (valorReward != null)
                    {
                        onValor = true;
                        targetDriveLevel.Add("Valor", valorReward.Item1);
                    }
                    if (wisdomReward != null)
                    {
                        onWisdom = true;
                        targetDriveLevel.Add("Wisdom", wisdomReward.Item1);
                    }
                    if (limitReward != null)
                    {
                        onLimit = true;
                        targetDriveLevel.Add("Limit", limitReward.Item1);
                    }
                    if (masterReward != null)
                    {
                        onMaster = true;
                        targetDriveLevel.Add("Master", masterReward.Item1);
                    }
                    if (finalReward != null)
                    {
                        onFinal = true;
                        targetDriveLevel.Add("Final", finalReward.Item1);
                    }
                }
            }
        }

        public bool UpdateTargetMemory()
        {
            targetObtained = false;
            if (onLevels)
            {
                if (Stats.Level >= targetLevel[Stats.Weapon])
                {
                    targetObtained = true;
                }
            }
            if (onValor)
            {
                if (Valor.Level >= targetDriveLevel["Valor"])
                {
                    targetObtained = true;
                }
            }
            if (onWisdom)
            {
                if (Wisdom.Level >= targetDriveLevel["Wisdom"])
                {
                    targetObtained = true;
                }
            }
            if (onLimit)
            {
                if (Limit.Level >= targetDriveLevel["Limit"])
                {
                    targetObtained = true;
                }
            }
            if (onMaster)
            {
                if (Master.Level >= targetDriveLevel["Master"])
                {
                    targetObtained = true;
                }
            }
            if (onFinal)
            {
                if (Final.Level >= targetDriveLevel["Final"])
                {
                    targetObtained = true;
                }
            }

            if (!targetObtained && targetCheck != null)
            {
                foreach (Check check in targetCheck)
                {
                    if (check.UpdateMemory())
                    {
                        targetObtained = true;
                        break;
                    }
                }
            }
            return targetObtained;
        }

        class Check
        {
            public CheckEveryCheck outer;

            public int addrCheck;
            public int addrObtained;
            public int bit;
            public int bytes = 1;
            private bool asdata = false;
            public World World;
            public string worldName;
            public int roomID;
            public int eventID;
            private bool obtained = false;
            public bool Obtained
            {
                get { return obtained; }
            }

            public Check(CheckEveryCheck outerClass, int addressCheck, int addressObtained, int bitIndex)
            {
                outer = outerClass;
                addrCheck = addressCheck;
                addrObtained = addressObtained;
                bit = bitIndex;
            }

            // for Absent Silhouette and Data fight popups where no flag exists; instead uses defeat cutscene IDs
            public Check(CheckEveryCheck outerClass, int addressCheck, World world, string worldName, int roomID, int eventID)
            {
                outer = outerClass;
                addrCheck = addressCheck;
                World = world;
                this.worldName = worldName;
                this.roomID = roomID;
                this.eventID = eventID;
                asdata = true;
            }

            public bool UpdateMemory()
            {
                if (asdata)         // assumes World is updated
                {
                    if (!obtained)
                    {
                        obtained = (World.worldName == worldName && World.roomNumber == roomID && World.eventID3 == eventID);
                    }
                }
                else
                {
                    byte[] data = outer.memory.ReadMemory(addrObtained + outer.ADDRESS_OFFSET, bytes);
                    obtained = new BitArray(data)[bit];
                }
                return obtained;
            }
        }

    }
}
