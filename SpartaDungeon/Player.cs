using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public struct PlayerData
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

    public enum eClassType
    {
        WARRIOR = 1,
        MAGE,
        ARCHER
    }

    public class Player
    {
        // Properties

        // 뫄

        // 배고프다

        // 구렁이

        //코끼리

        int Lion = 14;
        public void Test1()
        {
            Console.WriteLine("Merge Test");
        }
    }
}
