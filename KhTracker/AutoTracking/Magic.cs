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
        private int sttAddress;
        //private bool useSTTAddress;

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
        public Magic(MemoryReader mem, int address, int sttAddr, int offset, string name) : base(mem, address, offset, name)
        {
            sttAddress = sttAddr;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data;

            //if (useSTTAddress)
            //    data = memory.ReadMemory(sttAddress + ADDRESS_OFFSET, Bytes);
            //else
            //    data = base.UpdateMemory();

            data = base.UpdateMemory();

            if (Obtained == false && data[0] > 0)
            {
                Obtained = true;
                //App.logger.Record(Name + " obtained");
            }

            if (Level < data[0])
            {
                Level = data[0];
                if (App.logger != null)
                    App.logger.Record(Name + " level " + Level.ToString() + " obtained");
            }

            return null;
        }

        //public void UseSTTAddress(bool toggle)
        //{
        //    useSTTAddress = toggle;
        //}
    }
}