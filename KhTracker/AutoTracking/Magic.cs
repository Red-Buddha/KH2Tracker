using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace KhTracker
{
    class Magic : ImportantCheck
    {
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
        public Magic(MemoryReader mem, int address, int offset, string name) : base(mem, address, offset, name)
        {

        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            if (Obtained == false && data[0] > 0)
            {
                Obtained = true;
            }

            Level = data[0];

            return null;
        }

    }
}