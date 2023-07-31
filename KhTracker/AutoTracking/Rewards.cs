using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace KhTracker
{
    class Rewards
    {
        public List<Tuple<int, string>> swordChecks;
        public List<Tuple<int, string>> shieldChecks;
        public List<Tuple<int, string>> staffChecks;
        public List<Tuple<int, string>> valorChecks;
        public List<Tuple<int, string>> wisdomChecks;
        public List<Tuple<int, string>> limitChecks;
        public List<Tuple<int, string>> masterChecks;
        public List<Tuple<int, string>> finalChecks;

        public int ADDRESS_OFFSET;
        //int Bt10;
        int Lvup;
        int Fmlv;

        MemoryReader memory;

        public Rewards(MemoryReader mem, int offset, int bt10)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            //Bt10 = bt10;
            Lvup = GetSubOffset(bt10, 5);
            Fmlv = GetSubOffset(bt10, 16);
            swordChecks = new List<Tuple<int, string>>();
            shieldChecks = new List<Tuple<int, string>>();
            staffChecks = new List<Tuple<int, string>>();
            valorChecks = new List<Tuple<int, string>>();
            wisdomChecks = new List<Tuple<int, string>>();
            limitChecks = new List<Tuple<int, string>>();
            masterChecks = new List<Tuple<int, string>>();
            finalChecks = new List<Tuple<int, string>>();
            ReadRewards();
        }

        // populate reward lists
        private void ReadRewards()
        {
            //level array
            // lv99 checks only (31, 33, 47, 49, 53, 59, 65, 73, 85, 99)
            int[] soraLevels = {2, 4, 7, 9, 10, 12, 14, 15, 17, 20, 23, 25, 28, 30, 31, 
                32, 33, 34, 36, 39, 41, 44, 46, 47, 48, 49, 50, 53, 59, 65, 73, 85, 99};

            // if sword
            foreach (int level in soraLevels)
            {
                ReadReward(Lvup + (level * 0x10), 2, swordChecks, level);
            }

            // if shield
            foreach (int level in soraLevels)
            {
                ReadReward(Lvup + (level * 0x10) + 0x2, 2, shieldChecks, level);
            }

            // if staff
            foreach (int level in soraLevels)
            {
                ReadReward(Lvup + (level * 0x10) + 0x4, 2, shieldChecks, level);
            }

            //forms
            int offset = 0;
            for (int type = 0; type < 6; ++type)
            {
                //each type has 7 levels
                for (int level = 1; level < 8; ++level)
                {
                    //skip level 1 (impossible to obtain item)
                    if (level == 1)
                    {
                        offset = offset + 0x8;
                        continue;
                    }

                    switch (type)
                    {
                        case 0: //summons
                            //do nothing for now
                            break;
                        case 1: //valor
                            ReadReward(Fmlv + offset + 0x2, 2, valorChecks, level);
                            break;
                        case 2: //wisdom
                            ReadReward(Fmlv + offset + 0x2, 2, wisdomChecks, level);
                            break;
                        case 3: //limit
                            ReadReward(Fmlv + offset + 0x2, 2, limitChecks, level);
                            break;
                        case 4: //master
                            ReadReward(Fmlv + offset + 0x2, 2, masterChecks, level);
                            break;
                        case 5: //final
                            ReadReward(Fmlv + offset + 0x2, 2, finalChecks, level);
                            break;
                    }

                    offset = offset + 0x8;
                }
            }
        }

        private void ReadReward(int address, int byteCount, List<Tuple<int, string>> rewards, int level)
        {
            int num = address + ADDRESS_OFFSET;
            byte[] reward = memory.ReadMemory(num, byteCount);
            int i = BitConverter.ToInt16(reward, 0);
            if (IsImportant(i, out string name))
            {
                rewards.Add(new Tuple<int, string>(level, name));
            }
        }

        private bool IsImportant(int num, out string name)
        {
            if (MainWindow.data.codes.itemCodes.ContainsKey(num))
            {
                name = MainWindow.data.codes.itemCodes[num];
                return true;
            }
            name = "";
            return false;
        }

        public List<Tuple<int, string>> GetLevelRewards(string weapon)
        {
            if (weapon == "Sword")
            {
                return swordChecks;
            }
            else if (weapon == "Shield")
            {
                return shieldChecks;
            }
            else
            {
                return staffChecks;
            }
        }
    
        //we can't use hard addresses anymore, so we need to do checks and
        //such to get the correct offsets from pointers and whatnot
        private int GetSubOffset(int offset, int subfile)
        {
            int baseAddress = offset;
            offset = offset + (subfile * 0x10);

            if (subfile == 5) //lvup
            {
                //double check correct subfile name
                if (ReadMemString(offset + 0x4) == "lvup")
                {
                    return GetAddress(baseAddress, offset) + 0x3C;
                }
                else
                    return 0;
            }
            else if (subfile == 16) //fmlv
            {
                //double check correct subfile name
                if (ReadMemString(offset + 0x4) == "fmlv")
                {
                    return GetAddress(baseAddress, offset) + 0x8;
                }
                else
                    return 0;
            }

            return 0; 
        }
        
        private int ReadMemInt(int address)
        {
            address = address + ADDRESS_OFFSET;
            return BitConverter.ToInt32(memory.ReadMemory(address, 4), 0);
        }

        private int GetAddress(int baseAddress, int subAddress)
        {
            int off1 = ReadMemInt(baseAddress + 0x8);
            int off2 = ReadMemInt(subAddress + 0x8);
            int offFinal = off2 - off1;
            return baseAddress + offFinal;
        }

        private string ReadMemString(int address)
        {
            address = address + ADDRESS_OFFSET;
            string result = Encoding.Default.GetString(memory.ReadMemory(address, 4), 0, 4);
            return result.TrimEnd('\0');
        }
    }
}
