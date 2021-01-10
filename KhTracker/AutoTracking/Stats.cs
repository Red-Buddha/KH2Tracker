using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KhTracker
{
    class Stats : INotifyPropertyChanged
    {
        public int previousLevel;
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
        private int strength;
        public int Strength
        {
            get { return strength; }
            set
            {
                strength = value;
                OnPropertyChanged("Strength");
            }
        }
        private int magic;
        public int Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                OnPropertyChanged("Magic");
            }
        }
        private int defense;
        public int Defense
        {
            get { return defense; }
            set
            {
                defense = value;
                OnPropertyChanged("Defense");
            }
        }

        private int levelAddress;
        private int strengthAddress;
        private int magicAddress;
        private int defenseAddress;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public Stats(MemoryReader mem, int offset, int lvlAddress, int strAddress, int magAddress, int defAddress)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            levelAddress = lvlAddress;
            strengthAddress = strAddress;
            magicAddress = magAddress;
            defenseAddress = defAddress;
        }

        // this is not working
        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        public void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void UpdateMemory()
        {
            byte[] levelData = memory.ReadMemory(levelAddress + ADDRESS_OFFSET, 1);
            previousLevel = level;
            Level = levelData[0];
            byte[] strengthData = memory.ReadMemory(strengthAddress + ADDRESS_OFFSET, 1);
            Strength = strengthData[0];
            byte[] magicData = memory.ReadMemory(magicAddress + ADDRESS_OFFSET, 1);
            Magic = magicData[0];
            byte[] defenseData = memory.ReadMemory(defenseAddress + ADDRESS_OFFSET, 1);
            Defense = defenseData[0];
        }
    }
}
