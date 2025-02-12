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
        public string skill;
        public string ascii;
    }

    public class Monster
    {
        public MonsterData data;

        public Monster(MonsterData monsterData)
        {
            data = monsterData;
        }

        public virtual void Skill()
        {
            Console.WriteLine(data.skill);
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
            level = 5,
            attack = 5,
            defence = 2,
            maxHp = 20,
            hp = 20,
            gold = 10,
            maxMp = 2,
            mp = 0,
            skill = "찌르기",
            ascii = "(*｀ω´*)"
        }) { }
        public override void Skill()
        {
            float activeSkill = data.attack;
            data.attack += 5;
            Console.WriteLine($"{data.name}의 {data.skill}");
            Console.WriteLine($"{data.attack}피해");
            data.attack = activeSkill;
        }
    }
    public class Oak : Monster
    {
        public Oak() : base(new MonsterData
        {
            name = "오크",
            level = 10,
            attack = 20,
            defence = 10,
            maxHp = 50,
            hp = 50,
            gold = 50,
            maxMp = 5,
            mp = 0,
            skill = "광폭화",
            ascii = "\\(｀_´メ)"
        }) { }
        public override void Skill()
        {
            data.attack += 10;
            Console.WriteLine($"{data.name}의 {data.skill}");
            Console.WriteLine($"{data.attack}피해");
        }
    }
    public class Troll : Monster
    {
        public Troll() : base(new MonsterData
        {
            name = "트롤",
            level = 20,
            attack = 50,
            defence = 25,
            maxHp = 100,
            hp = 100,
            gold = 100,
            maxMp = 2,
            mp = 0,
            skill = "회복",
            ascii = "(｀ㅠ´メ)"
        }) { }
        public override void Skill()
        {
            data.hp = Math.Min(data.hp + 5, data.maxHp);
            Console.WriteLine("");
            Console.WriteLine($"{data.name}의 체력이 5 회복되었습니다.");
        }
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
            gold = 10,
            maxMp = 5,
            mp = 0,
            skill = "브레스",
            ascii = @"
                     ^\    ^\
                    / \\  /  \\
                   /.  \\/    \\      |\___/|
  *----*          /  / |  \\    \  __/   O  O\
  |   /          /  /  |   \\    \_\/  \     \\
  / /\/         /   /   |    \\   _\/    '@___@
  /  /         /    /    |     \\ _\/       |U
  |  |       /     /     |      \\\/        |
  \  |     /_     /      |       \\  )   \ _|_
  \   \       ~-./_ _    |    .- ; (  \_ _ _,\'
  ~    ~.           .-~-.|.-*      _        {-,
  \      ~-. _ .-~                 \      /\'
  \                   }            {   .* 
  ~.                 '-/        /.-~----.
      ~- _             /        >..----.\\\
          ~ - - - - ^}_ _ _ _ _ _ _.-\\\"
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
        public override void Skill()
        {
            float activeSkill = data.attack;
            data.attack += 50;
            Console.WriteLine($"{data.name}의 {data.skill}");
            Console.WriteLine($"{data.attack}피해");
            data.attack = activeSkill;
        }
    }
}
