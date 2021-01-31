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
        public int[] previousLevels = new int[3];
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
            bool flag = new BitArray(data)[byteNum];
            if (Obtained == false && flag == true)
            {
                Obtained = true;
                App.logger.Record(Name + " obtained");
            }

            byte[] levelData = memory.ReadMemory(levelAddr + ADDRESS_OFFSET, 1);
            previousLevels[0] = previousLevels[1];
            previousLevels[1] = previousLevels[2];
            previousLevels[2] = Level;

            if (Level < levelData[0])
            {
                Level = levelData[0];
                App.logger.Record(Name + " level " + Level.ToString() + " obtained");
            }
            
            return null;
        }
    }
}