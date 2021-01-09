using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    class ItemCodes
    {

        public Dictionary<int, string> levelCodes;

        public ItemCodes()
        {
            levelCodes = new Dictionary<int, string>();
            levelCodes.Add(21, "Fire");
            levelCodes.Add(22, "Blizzard");
            levelCodes.Add(23, "Thunder");
            levelCodes.Add(24, "Cure");
            levelCodes.Add(25, "Ukulele");
            levelCodes.Add(26, "Valor");
            levelCodes.Add(27, "Wisdom");
            levelCodes.Add(29, "Final");
            levelCodes.Add(31, "Master");
            levelCodes.Add(32, "TornPage");
            levelCodes.Add(87, "Magnet");
            levelCodes.Add(88, "Reflect");
            levelCodes.Add(159, "Lamp");
            levelCodes.Add(160, "Feather");
            levelCodes.Add(226, "Report1");
            levelCodes.Add(227, "Report2");
            levelCodes.Add(228, "Report3");
            levelCodes.Add(229, "Report4");
            levelCodes.Add(230, "Report5");
            levelCodes.Add(231, "Report6");
            levelCodes.Add(232, "Report7");
            levelCodes.Add(233, "Report8");
            levelCodes.Add(234, "Report9");
            levelCodes.Add(235, "Report10");
            levelCodes.Add(236, "Report11");
            levelCodes.Add(237, "Report12");
            levelCodes.Add(238, "Report13");
            levelCodes.Add(383, "Baseball");
            levelCodes.Add(415, "SecondChance");
            levelCodes.Add(416, "OnceMore");
            levelCodes.Add(524, "PromiseCharm");
            levelCodes.Add(563, "Limit");
            levelCodes.Add(593, "ProofofConnection");
            levelCodes.Add(594, "ProofofNonexistence");
            levelCodes.Add(595, "ProofofPeace");
        }
    }
}
