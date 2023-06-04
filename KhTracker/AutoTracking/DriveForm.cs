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
        private int visualLevel = 0;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }
        public int VisualLevel
        {
            get { return visualLevel; }
            set
            {
                visualLevel = value;
                OnPropertyChanged("VisualLevel");
            }
        }
        private int byteNum;
        private int levelAddr;
        private int genieFixAddr;
        public bool genieFix;

        public DriveForm(MemoryReader mem, int address, int offset, int byteNumber, int levelAddress, string name) : base(mem, address, offset, name)
        {
            byteNum = byteNumber;
            levelAddr = levelAddress;
            Bytes = 2;
        }

        public DriveForm(MemoryReader mem, int address, int offset, int byteNumber, int levelAddress, int genieFixAddress, string name) : base(mem, address, offset, name)
        {
            byteNum = byteNumber;
            levelAddr = levelAddress;
            genieFixAddr = genieFixAddress;
            genieFix = false;
            Bytes = 2;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            bool flag = new BitArray(data)[byteNum];
            if (Obtained == false && flag == true)
            {
                Obtained = true;
                //App.logger.Record(Name + " obtained");
            }

            byte[] levelData = memory.ReadMemory(levelAddr + ADDRESS_OFFSET, 1);
            previousLevels[0] = previousLevels[1];
            previousLevels[1] = previousLevels[2];
            previousLevels[2] = Level;

            VisualLevel = levelData[0];

            if (Level < levelData[0])
            {
                Level = levelData[0];
                if (App.logger != null)
                    App.logger.Record(Name + " level " + Level.ToString() + " obtained");
            }

            if (genieFixAddr != 0)
            {
                byte[] genieData = memory.ReadMemory(genieFixAddr + ADDRESS_OFFSET, 1);
                genieFix = Convert.ToBoolean(genieData[0]);
            }
            
            return null;
        }
    }
}