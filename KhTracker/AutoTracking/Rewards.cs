using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int Bt10;
        
        MemoryReader memory;

        public Rewards(MemoryReader mem, int offset, int bt10)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            Bt10 = bt10;
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
        public void ReadRewards()
        {
            // if sword
            ReadReward(Bt10 + 0x25940, 2, swordChecks, 2);
            ReadReward(Bt10 + 0x25960, 2, swordChecks, 4);
            ReadReward(Bt10 + 0x25990, 2, swordChecks, 7);
            ReadReward(Bt10 + 0x259B0, 2, swordChecks, 9);
            ReadReward(Bt10 + 0x259C0, 2, swordChecks, 10);
            ReadReward(Bt10 + 0x259E0, 2, swordChecks, 12);
            ReadReward(Bt10 + 0x25A00, 2, swordChecks, 14);
            ReadReward(Bt10 + 0x25A10, 2, swordChecks, 15);
            ReadReward(Bt10 + 0x25A30, 2, swordChecks, 17);
            ReadReward(Bt10 + 0x25A60, 2, swordChecks, 20);
            ReadReward(Bt10 + 0x25A90, 2, swordChecks, 23);
            ReadReward(Bt10 + 0x25AB0, 2, swordChecks, 25);
            ReadReward(Bt10 + 0x25AE0, 2, swordChecks, 28);
            ReadReward(Bt10 + 0x25B00, 2, swordChecks, 30);
            ReadReward(Bt10 + 0x25B10, 2, swordChecks, 31); // 99
            ReadReward(Bt10 + 0x25B20, 2, swordChecks, 32);
            ReadReward(Bt10 + 0x25B30, 2, swordChecks, 33); // 99
            ReadReward(Bt10 + 0x25B40, 2, swordChecks, 34);
            ReadReward(Bt10 + 0x25B60, 2, swordChecks, 36);
            ReadReward(Bt10 + 0x25B90, 2, swordChecks, 39);
            ReadReward(Bt10 + 0x25BB0, 2, swordChecks, 41);
            ReadReward(Bt10 + 0x25BE0, 2, swordChecks, 44);
            ReadReward(Bt10 + 0x25C00, 2, swordChecks, 46);
            ReadReward(Bt10 + 0x25C10, 2, swordChecks, 47); // 99
            ReadReward(Bt10 + 0x25C20, 2, swordChecks, 48);
            ReadReward(Bt10 + 0x25C30, 2, swordChecks, 49); // 99
            ReadReward(Bt10 + 0x25C40, 2, swordChecks, 50);
            ReadReward(Bt10 + 0x25C70, 2, swordChecks, 53); // 99
            ReadReward(Bt10 + 0x25CD0, 2, swordChecks, 59); // 99
            ReadReward(Bt10 + 0x25D30, 2, swordChecks, 65); // 99
            ReadReward(Bt10 + 0x25DB0, 2, swordChecks, 73); // 99
            ReadReward(Bt10 + 0x25E70, 2, swordChecks, 85); // 99
            ReadReward(Bt10 + 0x25F50, 2, swordChecks, 99); // 99

            // if shield
            ReadReward(Bt10 + 0x25942, 2, shieldChecks, 2);
            ReadReward(Bt10 + 0x25962, 2, shieldChecks, 4);
            ReadReward(Bt10 + 0x25992, 2, shieldChecks, 7);
            ReadReward(Bt10 + 0x259B2, 2, shieldChecks, 9);
            ReadReward(Bt10 + 0x259C2, 2, shieldChecks, 10);
            ReadReward(Bt10 + 0x259E2, 2, shieldChecks, 12);
            ReadReward(Bt10 + 0x25A02, 2, shieldChecks, 14);
            ReadReward(Bt10 + 0x25A12, 2, shieldChecks, 15);
            ReadReward(Bt10 + 0x25A32, 2, shieldChecks, 17);
            ReadReward(Bt10 + 0x25A62, 2, shieldChecks, 20);
            ReadReward(Bt10 + 0x25A92, 2, shieldChecks, 23);
            ReadReward(Bt10 + 0x25AB2, 2, shieldChecks, 25);
            ReadReward(Bt10 + 0x25AE2, 2, shieldChecks, 28);
            ReadReward(Bt10 + 0x25B02, 2, shieldChecks, 30);
            ReadReward(Bt10 + 0x25B12, 2, shieldChecks, 31); // 99
            ReadReward(Bt10 + 0x25B22, 2, shieldChecks, 32);
            ReadReward(Bt10 + 0x25B32, 2, shieldChecks, 33); // 99
            ReadReward(Bt10 + 0x25B42, 2, shieldChecks, 34);
            ReadReward(Bt10 + 0x25B62, 2, shieldChecks, 36);
            ReadReward(Bt10 + 0x25B92, 2, shieldChecks, 39);
            ReadReward(Bt10 + 0x25BB2, 2, shieldChecks, 41);
            ReadReward(Bt10 + 0x25BE2, 2, shieldChecks, 44);
            ReadReward(Bt10 + 0x25C02, 2, shieldChecks, 46);
            ReadReward(Bt10 + 0x25C12, 2, shieldChecks, 47); // 99
            ReadReward(Bt10 + 0x25C22, 2, shieldChecks, 48);
            ReadReward(Bt10 + 0x25C32, 2, shieldChecks, 49); // 99
            ReadReward(Bt10 + 0x25C42, 2, shieldChecks, 50);
            ReadReward(Bt10 + 0x25C72, 2, shieldChecks, 53); // 99
            ReadReward(Bt10 + 0x25CD2, 2, shieldChecks, 59); // 99
            ReadReward(Bt10 + 0x25D32, 2, shieldChecks, 65); // 99
            ReadReward(Bt10 + 0x25DB2, 2, shieldChecks, 73); // 99
            ReadReward(Bt10 + 0x25E72, 2, shieldChecks, 85); // 99
            ReadReward(Bt10 + 0x25F52, 2, shieldChecks, 99); // 99

            // if staff
            ReadReward(Bt10 + 0x25944, 2, staffChecks, 2);
            ReadReward(Bt10 + 0x25964, 2, staffChecks, 4);
            ReadReward(Bt10 + 0x25994, 2, staffChecks, 7);
            ReadReward(Bt10 + 0x259B4, 2, staffChecks, 9);
            ReadReward(Bt10 + 0x259C4, 2, staffChecks, 10);
            ReadReward(Bt10 + 0x259E4, 2, staffChecks, 12);
            ReadReward(Bt10 + 0x25A04, 2, staffChecks, 14);
            ReadReward(Bt10 + 0x25A14, 2, staffChecks, 15);
            ReadReward(Bt10 + 0x25A34, 2, staffChecks, 17);
            ReadReward(Bt10 + 0x25A64, 2, staffChecks, 20);
            ReadReward(Bt10 + 0x25A94, 2, staffChecks, 23);
            ReadReward(Bt10 + 0x25AB4, 2, staffChecks, 25);
            ReadReward(Bt10 + 0x25AE4, 2, staffChecks, 28);
            ReadReward(Bt10 + 0x25B04, 2, staffChecks, 30);
            ReadReward(Bt10 + 0x25B14, 2, staffChecks, 31); // 99
            ReadReward(Bt10 + 0x25B24, 2, staffChecks, 32);
            ReadReward(Bt10 + 0x25B34, 2, staffChecks, 33); // 99
            ReadReward(Bt10 + 0x25B44, 2, staffChecks, 34);
            ReadReward(Bt10 + 0x25B64, 2, staffChecks, 36);
            ReadReward(Bt10 + 0x25B94, 2, staffChecks, 39);
            ReadReward(Bt10 + 0x25BB4, 2, staffChecks, 41);
            ReadReward(Bt10 + 0x25BE4, 2, staffChecks, 44);
            ReadReward(Bt10 + 0x25C04, 2, staffChecks, 46);
            ReadReward(Bt10 + 0x25C14, 2, staffChecks, 47); // 99
            ReadReward(Bt10 + 0x25C24, 2, staffChecks, 48);
            ReadReward(Bt10 + 0x25C34, 2, staffChecks, 49); // 99
            ReadReward(Bt10 + 0x25C44, 2, staffChecks, 50);
            ReadReward(Bt10 + 0x25C74, 2, staffChecks, 53); // 99
            ReadReward(Bt10 + 0x25CD4, 2, staffChecks, 59); // 99
            ReadReward(Bt10 + 0x25D34, 2, staffChecks, 65); // 99
            ReadReward(Bt10 + 0x25DB4, 2, staffChecks, 73); // 99
            ReadReward(Bt10 + 0x25E74, 2, staffChecks, 85); // 99
            ReadReward(Bt10 + 0x25F54, 2, staffChecks, 99); // 99

            // valor
            ReadReward(Bt10 + 0x344AE, 2, valorChecks, 2);
            ReadReward(Bt10 + 0x344B6, 2, valorChecks, 3);
            ReadReward(Bt10 + 0x344BE, 2, valorChecks, 4);
            ReadReward(Bt10 + 0x344C6, 2, valorChecks, 5);
            ReadReward(Bt10 + 0x344CE, 2, valorChecks, 6);
            ReadReward(Bt10 + 0x344D6, 2, valorChecks, 7);

            // wisdom
            ReadReward(Bt10 + 0x344E6, 2, wisdomChecks, 2);
            ReadReward(Bt10 + 0x344EE, 2, wisdomChecks, 3);
            ReadReward(Bt10 + 0x344F6, 2, wisdomChecks, 4);
            ReadReward(Bt10 + 0x344FE, 2, wisdomChecks, 5);
            ReadReward(Bt10 + 0x34506, 2, wisdomChecks, 6);
            ReadReward(Bt10 + 0x3450E, 2, wisdomChecks, 7);

            // limit
            ReadReward(Bt10 + 0x3451E, 2, limitChecks, 2);
            ReadReward(Bt10 + 0x34526, 2, limitChecks, 3);
            ReadReward(Bt10 + 0x3452E, 2, limitChecks, 4);
            ReadReward(Bt10 + 0x34536, 2, limitChecks, 5);
            ReadReward(Bt10 + 0x3453E, 2, limitChecks, 6);
            ReadReward(Bt10 + 0x34546, 2, limitChecks, 7);

            // master
            ReadReward(Bt10 + 0x34556, 2, masterChecks, 2);
            ReadReward(Bt10 + 0x3455E, 2, masterChecks, 3);
            ReadReward(Bt10 + 0x34566, 2, masterChecks, 4);
            ReadReward(Bt10 + 0x3456E, 2, masterChecks, 5);
            ReadReward(Bt10 + 0x34576, 2, masterChecks, 6);
            ReadReward(Bt10 + 0x3457E, 2, masterChecks, 7);

            // final
            ReadReward(Bt10 + 0x3458E, 2, finalChecks, 2);
            ReadReward(Bt10 + 0x34596, 2, finalChecks, 3);
            ReadReward(Bt10 + 0x3459E, 2, finalChecks, 4);
            ReadReward(Bt10 + 0x345A6, 2, finalChecks, 5);
            ReadReward(Bt10 + 0x345AE, 2, finalChecks, 6);
            ReadReward(Bt10 + 0x345B6, 2, finalChecks, 7);
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
    }
}
