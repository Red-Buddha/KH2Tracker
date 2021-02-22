using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KhTracker
{
    class Summon : ImportantCheck
    {
        private int byteNum;

        public Summon(MemoryReader mem, int address, int offset, int byteNumber, string name) : base(mem, address, offset, name)
        {
            Bytes = 2;
            byteNum = byteNumber;
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
            return null;
        }
    }
}