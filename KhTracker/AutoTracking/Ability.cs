using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    class Ability : ImportantCheck
    {
        const int ADDRESS_START = 0x0032E074;
        const int ADDRESS_END = 0x0032E112;

        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        private int levelOffset;

        public Ability(MemoryReader mem, int address, int offset, string name) : base(mem, address, offset, name)
        {
            Bytes = 2;
            levelOffset = 0;
        }

        public Ability(MemoryReader mem, int address, int offset, int levOffset, string name) : base(mem, address, offset, name)
        {
            Bytes = 2;
            levelOffset = levOffset;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            int convertedData = BitConverter.ToUInt16(data, 0);
            int equipped = 0;
            if (levelOffset > 0 && convertedData > 0)
            {
                if (convertedData > 32768)
                {
                    equipped = 32768;
                }

                Level = convertedData - levelOffset - equipped;
            }
            else
            {
                Level = 0;
            }

            if (Obtained == false && Level >= 1)
            {
                Obtained = true;
            }
            return null;
        }
    }
}