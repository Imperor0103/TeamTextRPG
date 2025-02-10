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

    public class Goblin : Monster
    {
        public Goblin() : base(new MonsterData
        {
            name = "고블린",
            // level = 1,
            attack = 5,
            defence = 2,
            maxHp = 20,
            hp = 20,
            // maxMp = 0,
            // mp = 0,
            // gold = 10
        }) { }
    }
    public class Oak : Monster
    {
        public Oak() : base(new MonsterData
        {
            name = "오크",
            // level = 1,
            attack = 10,
            defence = 5,
            maxHp = 50,
            hp = 50,
            // maxMp = 0,
            // mp = 0,
            // gold = 10
        }) { }
    }
    public class Dragon : Monster
    {
        public Dragon() : base(new MonsterData
        {
            name = "드래곤",
            // level = 1,
            attack = 50,
            defence = 20,
            maxHp = 100,
            hp = 100,
            // maxMp = 0,
            // mp = 0,
            // gold = 10
        }) { }
    }
}
