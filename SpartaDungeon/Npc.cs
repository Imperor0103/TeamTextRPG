using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public struct NpcData
    {
        public string name;
        public eClassType classType;
        public int level;
        public float attack;
        public float defence;
        public float maxHp;
        public float hp;
        public float maxMp;
        public float mp;
        public int exp;
        public int gold;

    }

    public class Npc
    {
        // Npc가 가지고 있는 고유한 퀘스트를 저장
        public Dictionary<string, Quest> questList;

        // Npc는 퀘스트를 멤버로 가지지 않는다.
        // 퀘스트 데이터(json)파일을 읽고

        public Npc()
        {
            if (questList == null)
            {
                questList = new Dictionary<string, Quest>();
            }
        }
    }
}
