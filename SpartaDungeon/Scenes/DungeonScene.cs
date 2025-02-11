using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaDungeon.Managers;

namespace SpartaDungeon.Scenes
{
    public class DungeonScene: BaseScene
    {
        public Player player;
        public Monster monster;

        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public DungeonScene(string name) : base(name) { }
        #endregion
        // 
        public override void Awake()
        {
            player = DataManager.Instance.player;
        }
        public override void Start()
        {
            Console.Clear();
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("──────▄██▀▀▀▀▄");
            Thread.Sleep(50);
            Console.WriteLine("────▄███▀▀▀▀▀▀▀▄");
            Thread.Sleep(50);
            Console.WriteLine("──▄████▀▀DUNGEON▀▄");
            Thread.Sleep(50);
            Console.WriteLine("▄█████▀▀▀       ▀▀▀▄");
            Thread.Sleep(1000);
            Console.WriteLine("-STAGE-");
            Console.WriteLine("1. 고블린");
            Console.WriteLine("2. 오크");
            Console.WriteLine("3. 트롤");
            Console.WriteLine("4. 드래곤");
            Console.WriteLine("9. 돌아가기");
        }
        public override void Update()
        {
        int choice = InputManager.Instance.GetValidNumber("-입력-", 1, 9);
            if (choice == 9)
            {
                Console.WriteLine("마을로 돌아갑니다.");
                return;
            }

            monster = choice switch
            {
                1 => new Goblin(),
                2 => new Oak(),
                3 => new Troll(),
                4 => new Dragon(),
                _ => null
            };

            if (monster != null)
            {
                Battle();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            
        }
        public bool Battle()
        {
            Console.WriteLine();
            Console.WriteLine($"{monster.data.name}과(와)의 전투 시작!");
            Console.WriteLine();
            Thread.Sleep(1500);
            while (player.data.hp > 0 && monster.data.hp > 0)
            {
                Console.Clear();
                monster.Ascii();
                Console.WriteLine($"[체력: {monster.data.hp}][공격력: {monster.data.attack}][방어력: {monster.data.defence}]");
                PlayerTurn();
                if (monster.data.hp <= 0) break; // 몬스터가 죽으면 루프 종료
                if (monster.data.name == "드래곤" && monster.data.hp <= (monster.data.maxHp * 0.3)) // 드래곤의 체력이 30%에 달하면 공격력 증가
                {
                    monster.data.attack += 50;
                    Console.WriteLine("!!ヽ(ﾟдﾟヽ)(ﾉﾟдﾟ)ﾉ!!");
                    Thread.Sleep(500);
                    Console.WriteLine($"{monster.data.name}의 공격력 증가!");
                    Thread.Sleep(2000);
                }
                if (monster.data.name == "트롤") // 트롤 턴제 회복
                {
                    if (monster.data.maxHp - 10 > monster.data.hp)
                    {
                        Console.WriteLine("트롤의 체력이 소량 회복되었습니다.");
                        monster.data.hp += 5;
                        Thread.Sleep(1000);
                    }
                }
                MonsterTurn();
                if (player.data.hp <= 0) break; // 플레이어가 죽으면 루프 종료
            }

            // 전투 결과 출력
            if (monster.data.hp <= 0)
            {
                Console.WriteLine($"{monster.data.name}을(를) 처치했습니다! 보상을 획득합니다.");
                player.data.exp += monster.data.level;
                player.data.gold += monster.data.gold;
                Console.WriteLine($"경험치: {monster.data.level}+");
                Console.WriteLine($"Gold: {monster.data.gold}+");
                VictoryMessages.RandomVictoryMessage();
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.WriteLine("Enter...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("패배했습니다... 잠시 후 마을로 돌아갑니다.");
                Thread.Sleep(3000);
            }

            // 장면 전환
            SceneManager.Instance.LoadScene("town");
            return player.data.hp > 0;
        }
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine($"플레이어 체력: {player.data.hp}");
            Console.WriteLine($"{player.data.name}의 턴 (공격하려면 Enter)");
            Console.ReadLine();
            float damage = Math.Max(player.data.attack - monster.data.defence, 1); // 1은 최소 대미지
            monster.data.hp -= damage;
            if (player.data.attack - monster.data.defence <= 0)
            {
                damage = 1;
            }
            Console.WriteLine("(。･`з･)ﾉ");
            Console.WriteLine($"{monster.data.name}에게 {damage} 피해");
            Thread.Sleep(1000);
        }
        private void MonsterTurn()
        {
            Console.WriteLine();
            float damage = Math.Max(monster.data.attack - player.data.defence, 1);
            player.data.hp -= damage;
            if (monster.data.attack - player.data.defence <= 0)
            {
                damage = 1;
            }
            
            Console.WriteLine("\x1b[38;2;255;0;0mヽ( `皿´ )ﾉ\x1b[0m");
            Console.WriteLine($"{player.data.name}에게 {damage} 피해");
            Thread.Sleep(1000);
        }

        // 축하
        public class VictoryMessages
        {
            private static readonly List<string> victoryArts = new()
            {
                // "  ✨🏆 VICTORY 🏆✨\n     ___________\n    '._==_==_=_.'\n    .-\\:      /-.\n   | (|:.     |) |\n    '-|:.     |-'\n      \\::.    /\n       '::. .'\n         ) (\n       _.' '._",
                
                // "🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥\n🔥 🎉 VICTORY! 🎉 🔥\n🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥\n    \\        /\n     \\  🏆  /\n      (🔥🔥)\n       (🔥)\n        \\/",
                
                // "      ✨🌟✨\n  🌟 VICTORY! 🌟\n      ✨🌟✨",
                
                // "  ⚡⚡⚡⚡⚡⚡\n ⚡ 🎉 WIN 🎉 ⚡\n  ⚡⚡⚡⚡⚡⚡",
                
                // " 🏆 Victory! 🏆\n   🛡️   ⚔️   🛡️",
                
                // " 🏆 VICTORY!! 🏆\n   🚩        🚩\n   | WINNER |\n   |________|",
                
                // "  💰💰💰💰💰💰💰\n  💰 YOU WIN! 💰\n  💰💰💰💰💰💰💰",
                
                // "      👑🏆👑\n  🎉 VICTORY! 🎉\n      👑🏆👑",

                "\x1b[38;2;255;255;255m  ██╗    ██╗██╗████████╗ ██████╗ ██████╗ ██╗   ██╗\n\x1b[38;2;255;200;200m  ██║    ██║██║╚══██╔══╝██╔═══██╗██╔══██╗╚██║ ██╔╝\n\x1b[38;2;255;150;150m  ╚██╗  ██╔╝██║   ██║   ██║   ██║██████╔╝ ╚████╔╝\n\x1b[38;2;255;100;100m   ╚██╗██╔╝ ██║   ██║   ██║   ██║██╔██╔╝   ╚██╔╝\n\x1b[38;2;255;50;50m    ╚███╔╝  ██║   ██║   ╚██████╔╝██║ ███╗   ██║\n\x1b[38;2;255;0;0m      ╚═╝   ╚═╝   ╚═╝    ╚═════╝ ╚═╝ ╚══╝   ╚═╝\x1b[0m"
            };

            public static void RandomVictoryMessage()
            {
                Random random = new Random();
                int index = random.Next(victoryArts.Count);
                Console.WriteLine(victoryArts[index]);
            }
        }
    }
}
