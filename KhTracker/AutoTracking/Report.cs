using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace KhTracker
{
    class Report : ImportantCheck
    {
        public string Content;
        private int byteNum;

        public Report(MemoryReader mem, int address, int offset, int byteNumber, string name) : base(mem, address, offset, name)
        {
            byteNum = byteNumber;
            Bytes = 2;
        }
        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Obtained = new BitArray(data)[byteNum];
            return null;
        }
    }
}