using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    class Rewards
    {
        public List<Tuple<int, string>> levelChecks;
        public List<Tuple<int, string>> valorChecks;
        public List<Tuple<int, string>> wisdomChecks;
        public List<Tuple<int, string>> limitChecks;
        public List<Tuple<int, string>> masterChecks;
        public List<Tuple<int, string>> finalChecks;
        ItemCodes itemCodes;

        public int ADDRESS_OFFSET;
        
        MemoryReader memory;

        public Rewards(MemoryReader mem, int offset)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            itemCodes = new ItemCodes();
            levelChecks = new List<Tuple<int, string>>();
            valorChecks = new List<Tuple<int, string>>();
            wisdomChecks = new List<Tuple<int, string>>();
            limitChecks = new List<Tuple<int, string>>();
            masterChecks = new List<Tuple<int, string>>();
            finalChecks = new List<Tuple<int, string>>();
            ReadRewards();
        }

        // populate reward lists
        public void ReadRewards()
        {
            // if sword
            ReadReward(0x01D0B6C0, 2, levelChecks, 2);
            ReadReward(0x01D0B6E0, 2, levelChecks, 4);
            ReadReward(0x01D0B710, 2, levelChecks, 7);
            ReadReward(0x01D0B730, 2, levelChecks, 9);
            ReadReward(0x01D0B740, 2, levelChecks, 10);
            ReadReward(0x01D0B760, 2, levelChecks, 12);
            ReadReward(0x01D0B780, 2, levelChecks, 14);
            ReadReward(0x01D0B790, 2, levelChecks, 15);
            ReadReward(0x01D0B7B0, 2, levelChecks, 17);
            ReadReward(0x01D0B7E0, 2, levelChecks, 20);
            ReadReward(0x01D0B810, 2, levelChecks, 23);
            ReadReward(0x01D0B830, 2, levelChecks, 25);
            ReadReward(0x01D0B860, 2, levelChecks, 28);
            ReadReward(0x01D0B880, 2, levelChecks, 30);
            ReadReward(0x01D0B8A0, 2, levelChecks, 32);
            ReadReward(0x01D0B8C0, 2, levelChecks, 34);
            ReadReward(0x01D0B8E0, 2, levelChecks, 36);
            ReadReward(0x01D0B910, 2, levelChecks, 39);
            ReadReward(0x01D0B930, 2, levelChecks, 41);
            ReadReward(0x01D0B960, 2, levelChecks, 44);
            ReadReward(0x01D0B980, 2, levelChecks, 46);
            ReadReward(0x01D0B9A0, 2, levelChecks, 48);
            ReadReward(0x01D0B9C0, 2, levelChecks, 50);

            //// if staff
            //ReadReward(0x01D0B6C2, 2, levelChecks, 2);
            //ReadReward(0x01D0B6E2, 2, levelChecks, 4);
            //ReadReward(0x01D0B712, 2, levelChecks, 7);
            //ReadReward(0x01D0B732, 2, levelChecks, 9);
            //ReadReward(0x01D0B742, 2, levelChecks, 10);
            //ReadReward(0x01D0B762, 2, levelChecks, 12);
            //ReadReward(0x01D0B782, 2, levelChecks, 14);
            //ReadReward(0x01D0B792, 2, levelChecks, 15);
            //ReadReward(0x01D0B7B2, 2, levelChecks, 17);
            //ReadReward(0x01D0B7E2, 2, levelChecks, 20);
            //ReadReward(0x01D0B812, 2, levelChecks, 23);
            //ReadReward(0x01D0B832, 2, levelChecks, 25);
            //ReadReward(0x01D0B862, 2, levelChecks, 28);
            //ReadReward(0x01D0B882, 2, levelChecks, 30);
            //ReadReward(0x01D0B8A2, 2, levelChecks, 32);
            //ReadReward(0x01D0B8C2, 2, levelChecks, 34);
            //ReadReward(0x01D0B8E2, 2, levelChecks, 36);
            //ReadReward(0x01D0B912, 2, levelChecks, 39);
            //ReadReward(0x01D0B932, 2, levelChecks, 41);
            //ReadReward(0x01D0B962, 2, levelChecks, 44);
            //ReadReward(0x01D0B982, 2, levelChecks, 46);
            //ReadReward(0x01D0B9A2, 2, levelChecks, 48);
            //ReadReward(0x01D0B9C2, 2, levelChecks, 50);

            //// if shield
            //ReadReward(0x01D0B6C4, 2, levelChecks, 2);
            //ReadReward(0x01D0B6E4, 2, levelChecks, 4);
            //ReadReward(0x01D0B714, 2, levelChecks, 7);
            //ReadReward(0x01D0B734, 2, levelChecks, 9);
            //ReadReward(0x01D0B744, 2, levelChecks, 10);
            //ReadReward(0x01D0B764, 2, levelChecks, 12);
            //ReadReward(0x01D0B784, 2, levelChecks, 14);
            //ReadReward(0x01D0B794, 2, levelChecks, 15);
            //ReadReward(0x01D0B7B4, 2, levelChecks, 17);
            //ReadReward(0x01D0B7E4, 2, levelChecks, 20);
            //ReadReward(0x01D0B814, 2, levelChecks, 23);
            //ReadReward(0x01D0B834, 2, levelChecks, 25);
            //ReadReward(0x01D0B864, 2, levelChecks, 28);
            //ReadReward(0x01D0B884, 2, levelChecks, 30);
            //ReadReward(0x01D0B8A4, 2, levelChecks, 32);
            //ReadReward(0x01D0B8C4, 2, levelChecks, 34);
            //ReadReward(0x01D0B8E4, 2, levelChecks, 36);
            //ReadReward(0x01D0B914, 2, levelChecks, 39);
            //ReadReward(0x01D0B934, 2, levelChecks, 41);
            //ReadReward(0x01D0B964, 2, levelChecks, 44);
            //ReadReward(0x01D0B984, 2, levelChecks, 46);
            //ReadReward(0x01D0B9A4, 2, levelChecks, 48);
            //ReadReward(0x01D0B9C4, 2, levelChecks, 50);

            // valor
            ReadReward(0x01D1A22E, 2, valorChecks, 2);
            ReadReward(0x01D1A236, 2, valorChecks, 3);
            ReadReward(0x01D1A23E, 2, valorChecks, 4);
            ReadReward(0x01D1A246, 2, valorChecks, 5);
            ReadReward(0x01D1A24E, 2, valorChecks, 6);
            ReadReward(0x01D1A256, 2, valorChecks, 7);

            // wisdom
            ReadReward(0x01D1A266, 2, wisdomChecks, 2);
            ReadReward(0x01D1A26E, 2, wisdomChecks, 3);
            ReadReward(0x01D1A276, 2, wisdomChecks, 4);
            ReadReward(0x01D1A27E, 2, wisdomChecks, 5);
            ReadReward(0x01D1A286, 2, wisdomChecks, 6);
            ReadReward(0x01D1A28E, 2, wisdomChecks, 7);

            // limit
            ReadReward(0x01D1A29E, 2, limitChecks, 2);
            ReadReward(0x01D1A2A6, 2, limitChecks, 3);
            ReadReward(0x01D1A2AE, 2, limitChecks, 4);
            ReadReward(0x01D1A2B6, 2, limitChecks, 5);
            ReadReward(0x01D1A2BE, 2, limitChecks, 6);
            ReadReward(0x01D1A2C6, 2, limitChecks, 7);

            // master
            ReadReward(0x01D1A2D6, 2, masterChecks, 2);
            ReadReward(0x01D1A2DE, 2, masterChecks, 3);
            ReadReward(0x01D1A2E6, 2, masterChecks, 4);
            ReadReward(0x01D1A2EE, 2, masterChecks, 5);
            ReadReward(0x01D1A2F6, 2, masterChecks, 6);
            ReadReward(0x01D1A2FE, 2, masterChecks, 7);

            // final
            ReadReward(0x01D1A30E, 2, finalChecks, 2);
            ReadReward(0x01D1A316, 2, finalChecks, 3);
            ReadReward(0x01D1A31E, 2, finalChecks, 4);
            ReadReward(0x01D1A326, 2, finalChecks, 5);
            ReadReward(0x01D1A32E, 2, finalChecks, 6);
            ReadReward(0x01D1A336, 2, finalChecks, 7);
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
            if (itemCodes.levelCodes.ContainsKey(num))
            {
                name = itemCodes.levelCodes[num];
                return true;
            }
            name = "";
            return false;
        }
    }
}
