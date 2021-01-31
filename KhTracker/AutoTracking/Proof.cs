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
            bool flag = new BitArray(data)[0];
            if (Obtained == false && flag == true)
            {
                Obtained = true;
                App.logger.Record(Name + " obtained");
            }
            return null;
        }
    }
}