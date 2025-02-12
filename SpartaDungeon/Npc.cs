using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Npc
    {
        public List<Quest> questList;   

        public Npc()
        {
            if (questList == null)
            {
                questList = new List<Quest>();
            }

        }


    }
}
