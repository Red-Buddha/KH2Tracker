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
        //next level check stuff
        private int[] levelChecks1 = { 1, 1 };
        private int[] levelChecks50 = { 0, 2, 4, 7, 9, 10, 12, 14, 15, 17, 20, 23, 25, 28, 30, 32, 34, 36, 39, 41, 44, 46, 48, 50 };
        private int[] levelChecks99 = { 0, 7, 9, 12, 15, 17, 20, 23, 25, 28, 31, 33, 36, 39, 41, 44, 47, 49, 53, 59, 65, 73, 85, 99 };
        private int[] currentCheckArray;
        private int nextLevelCheck = 0;

        public int[] previousLevels = new int[3];
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
        private int bonuslevel;
        public int BonusLevel
        {
            get { return bonuslevel; }
            set
            {
                bonuslevel = value;
                OnPropertyChanged("BonusLevel");
            }
        }

        //show next level check
        private int levelCheck;
        public int LevelCheck
        {
            get { return levelCheck; }
            set
            {
                levelCheck = value;
                OnPropertyChanged("LevelCheck");
            }
        }

        public int form;

        private int levelAddress;
        private int statsAddress;
        private int formAddress;
        private int bonusAddress;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public Stats(MemoryReader mem, int offset, int lvlAddress, int statsAddr, int formAddr, int bonusLvl)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            levelAddress = lvlAddress;
            statsAddress = statsAddr;
            formAddress = formAddr;
            bonusAddress = bonusLvl;
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

            previousLevels[0] = previousLevels[1];
            previousLevels[1] = previousLevels[2];
            previousLevels[2] = Level;
            
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

            byte[] BonusData = memory.ReadMemory(bonusAddress + ADDRESS_OFFSET, 1);
            BonusLevel = BonusData[0];

            //change levelreward number
            if (level >= currentCheckArray[currentCheckArray.Length - 1])
            {
                LevelCheck = currentCheckArray[currentCheckArray.Length - 1];
                return;
            }

            if (Level >= currentCheckArray[nextLevelCheck])
            {
                nextLevelCheck++;
                LevelCheck = currentCheckArray[nextLevelCheck];
            }
        }

        public void SetMaxLevelCheck(int lvl)
        {
            switch(lvl)
            {
                case 50:
                    currentCheckArray = levelChecks50;
                    break;
                case 99:
                    currentCheckArray = levelChecks99;
                    break;
                default:
                    currentCheckArray = levelChecks1;
                    break;
            }
        }

        public void SetNextLevelCheck(int lvl)
        {
            for (int i = 0; i < currentCheckArray.Length; i++)
            {
                if (lvl < currentCheckArray[i])
                {
                    nextLevelCheck = i;
                    LevelCheck = currentCheckArray[nextLevelCheck];
                    break;
                }
            }
        }
    }
}
