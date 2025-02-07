using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public struct MonsterData
    {
        public string name;
        public int level;
        public float attack;
        public float defence;
        public float maxHp;
        public float hp;
        public float maxMp;
        public float mp;
        public int gold;
    }

    public class Monster
    {
        public MonsterData data;

        public Monster(MonsterData monsterData)
        {
            data = monsterData;
        }
    }
}
