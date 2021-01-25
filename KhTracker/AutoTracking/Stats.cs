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
        public int previousPreviousPreviousLevel;
        private int previousPreviousLevel;
        private int previousLevel;
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
        private string weapon;
        public string Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                OnPropertyChanged("Weapon");
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

        public int form;

        private int levelAddress;
        private int statsAddress;
        private int formAddress;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public Stats(MemoryReader mem, int offset, int lvlAddress, int statsAddr, int formAddr)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            levelAddress = lvlAddress;
            statsAddress = statsAddr;
            formAddress = formAddr;
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
            byte[] levelData = memory.ReadMemory(levelAddress + ADDRESS_OFFSET, 2);

            if (levelData[0] == 0 && Weapon != "Sword")
                Weapon = "Sword";
            else if (levelData[0] == 1 && Weapon != "Shield")
                Weapon = "Shield";
            else if (levelData[0] == 2 && Weapon != "Staff")
                Weapon = "Staff";

            previousPreviousPreviousLevel = previousPreviousLevel;
            previousPreviousLevel = previousLevel;
            previousLevel = level;
            if (Level != levelData[1])
                Level = levelData[1];

            byte[] statsData = memory.ReadMemory(statsAddress + ADDRESS_OFFSET, 5);
            if (Strength != statsData[0])
                Strength = statsData[0];
            if (Magic != statsData[2])
                Magic = statsData[2];
            if (Defense != statsData[4])
                Defense = statsData[4];

            byte[] modelData = memory.ReadMemory(formAddress + ADDRESS_OFFSET, 1);
            form = modelData[0];
        }
    }
}
