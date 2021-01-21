using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace KhTracker
{
    class DriveForm : ImportantCheck
    {
        public bool previousPreviousObtained;
        public bool previousObtained;
        public int previousPreviousPreviousLevel;
        private int previousPreviousLevel;
        private int previousLevel;
        private int level = 0;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }
        private int byteNum;
        private int levelAddr;

        public DriveForm(MemoryReader mem, int address, int offset, int byteNumber, int levelAddress, string name) : base(mem, address, offset, name)
        {
            byteNum = byteNumber;
            levelAddr = levelAddress;
            Bytes = 2;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            previousPreviousObtained = previousObtained;
            previousObtained = Obtained;
            Obtained = new BitArray(data)[byteNum];
            byte[] levelData = memory.ReadMemory(levelAddr + ADDRESS_OFFSET, 1);
            previousPreviousPreviousLevel = previousPreviousLevel;
            previousPreviousLevel = previousLevel;
            previousLevel = Level;

            Level = levelData[0];
            if (levelData[0] > 1)
            {
                Level = levelData[0];
            }
            return null;
        }
    }
}