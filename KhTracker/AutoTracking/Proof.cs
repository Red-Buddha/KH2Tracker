using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KhTracker
{
    class Proof : ImportantCheck
    {
        public Proof(MemoryReader mem, int address, int offset, string name) : base(mem, address, offset, name)
        {
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Obtained = new BitArray(data)[0];
            return null;
        }
    }
}