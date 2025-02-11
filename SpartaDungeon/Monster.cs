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
        public string ascii;
    }

    public class Monster
    {
        public MonsterData data;

        public Monster(MonsterData monsterData)
        {
            data = monsterData;
        }

        public virtual void Ascii()
        {
            Console.WriteLine(data.ascii);
        }
    }

    public class Goblin : Monster
    {
        public Goblin() : base(new MonsterData
        {
            name = "고블린",
            level = 1,
            attack = 5,
            defence = 2,
            maxHp = 20,
            hp = 20,
            gold = 10,
            // maxMp = 0,
            // mp = 0,
            ascii = "(*｀ω´*)"
        }) { }
    }
    public class Oak : Monster
    {
        public Oak() : base(new MonsterData
        {
            name = "오크",
            level = 5,
            attack = 10,
            defence = 5,
            maxHp = 50,
            hp = 50,
            gold = 10,
            // maxMp = 0,
            // mp = 0,
            ascii = "(｀_´メ)"
        }) { }
    }
    public class Dragon : Monster
    {
        private List<(string line, ConsoleColor color)> asciiColors;
        public Dragon() : base(new MonsterData
        {
            name = "드래곤",
            level = 50,
            attack = 100,
            defence = 50,
            maxHp = 1000,
            hp = 1000,
            gold = 10
            // maxMp = 0,
            // mp = 0,
        })
        {
            asciiColors = new List<(string, ConsoleColor)>
            {
                (@"                     ^\    ^\", ConsoleColor.Red),
                (@"                    / \\  /  \\", ConsoleColor.Red),
                (@"                   /.  \\/    \\      |\___/|", ConsoleColor.Red),
                (@"  *----*          /  / |  \\    \  __/  ･`  ･\", ConsoleColor.Red),
                (@"  |   /          /  /  |   \\    \_\/  \     \\", ConsoleColor.Red),
                (@"  / /\/         /   /   |    \\   _\/    '@___@", ConsoleColor.Red),
                (@"  /  /         /    /    |     \\ _\/       |", ConsoleColor.Red),
                (@"  |  |       /     /     |      \\\/        |", ConsoleColor.Red),
                (@"  \  |     /_     /      |       \\  )   \ _|_", ConsoleColor.Red),
                (@"  \   \       ~-./_ _    |    .- ; (  \_ _ _,\'", ConsoleColor.Red),
                (@"  ~    ~.           .-~-.|.-*      _        {-,", ConsoleColor.Red),
                (@"  \      ~-. _ .-~                 \      /\'", ConsoleColor.Red),
                (@"  \                   }            {   .*", ConsoleColor.Red),
                (@"  ~.                 '-/        /.-~----.", ConsoleColor.Red),
                (@"      ~- _             /        >..----.\\\", ConsoleColor.Red),
                (@"          ~ - - - - ^}_ _ _ _ _ _ _.-\\\", ConsoleColor.Red)
            };
        }

        public override void Ascii() // 드래곤 아스키 오버라이딩
        {
            asciiColors.ForEach(line =>
            {
                Console.ForegroundColor = line.color;
                Console.WriteLine(line.line);
            });
            Console.ResetColor();
        }
    }
}
