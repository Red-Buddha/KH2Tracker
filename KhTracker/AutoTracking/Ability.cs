using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    class Ability : ImportantCheck
    {
        const int ADDRESS_START = 0x2544;
        const int ADDRESS_END = 0x25CC;

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

        public Ability(MemoryReader mem, int address, int offset, string name, int save) : base(mem, address, offset, name)
        {
            Bytes = 158;
            levelOffset = 0;
            this.Address = ADDRESS_START + save;
        }

        public Ability(MemoryReader mem, int address, int offset, int levOffset, string name) : base(mem, address, offset, name)
        {
            Bytes = 2;
            levelOffset = levOffset;
        }

        public override byte[] UpdateMemory()
        {
            if(levelOffset == 0)
            {
                byte[] abilityData = base.UpdateMemory();
                for (int i = 0; i < abilityData.Length; i += 2)
                {
                    if (abilityData[i+1] == 1 && abilityData[i] == 159 && this.Name == "SecondChance")
                    {
                        if (!this.Obtained) { this.Obtained = true; }
                    }
                    if (abilityData[i + 1] == 1 && abilityData[i] == 160 && this.Name == "OnceMore")
                    {
                        if (!this.Obtained) { this.Obtained = true; }
                    }
                }
                return null;
            }
            byte[] data = base.UpdateMemory();
            int convertedData = BitConverter.ToUInt16(data, 0);
            int equipped = 0;
            if (levelOffset > 0 && convertedData > 0)
            {
                if (convertedData > 32768)
                {
                    equipped = 32768;
                }

                int curLevel = convertedData - levelOffset - equipped;
                //if (curLevel > Level)
                {
                    Level = curLevel;
                    if (curLevel > Level)
                        App.logger?.Record(Name + " level " + Level.ToString() + " obtained");
                    else if (curLevel < Level)
                        App.logger?.Record(Name + " level " + Level.ToString() + " removed");
                }
            }
            else
            {
                Level = 0;
            }

            if (Obtained == false && Level >= 1)
            {
                Obtained = true;
                //App.logger.Record(Name + " obtained");
            }
            return null;
        }
    }
}