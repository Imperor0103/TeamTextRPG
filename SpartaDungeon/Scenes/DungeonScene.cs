﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaDungeon.Managers;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaDungeon.Scenes
{
    public class DungeonScene : BaseScene
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
            Console.OutputEncoding = Encoding.UTF8;
            //
            Console.Clear();
            Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
            //
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
            Console.WriteLine("0. 나가기");
        }

        public override void Update()
        {
            int choice = InputManager.Instance.GetValidNumber("-입력-", 0, 4);
            if (choice == 0)
            {
                Console.WriteLine("마을로 돌아갑니다.");
                SceneManager.Instance.LoadScene("town");
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
            //
            Console.Clear();
            Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
            //
            Console.WriteLine($"{monster.data.name}과(와)의 전투 시작!");
            Console.WriteLine();
            Thread.Sleep(1500);

            bool phase2 = false; // 페이즈를 위한 bool
            if (monster.data.name == "드래곤")
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(@"                 /           /   ");
                Thread.Sleep(10);
                Console.WriteLine(@"                /' .,,,,  ./     ");
                Thread.Sleep(10);
                Console.WriteLine(@"               /';'     ,/       ");
                Thread.Sleep(10);
                Console.WriteLine(@"              / /   ,,//,`'`     ");
                Thread.Sleep(10);
                Console.WriteLine(@"             ( ,, '_,  ,,,' ``   ");
                Thread.Sleep(10);
                Console.WriteLine(@"             |    ///  ,,, ;'  ' ");
                Thread.Sleep(1000);
                Console.WriteLine(@"            /    .   ,''/' `,``  ");
                Thread.Sleep(100);
                Console.WriteLine(@"           /   .     ./, `,, ` ; ");
                Thread.Sleep(100);
                Console.WriteLine(@"        ,./  .   ,-,',` ,,/''\,' ");
                Thread.Sleep(10);
                Console.WriteLine(@"       |   /; ./,,'`,,'' |   |   ");
                Thread.Sleep(10);
                Console.WriteLine(@"       |     /   ','    /    |   ");
                Thread.Sleep(10);
                Console.WriteLine(@"        \___/'   '     |     |   ");
                Thread.Sleep(10);
                Console.WriteLine(@"          `,,'  |      /     `\  ");
                Thread.Sleep(10);
                Console.WriteLine(@"               /      |        ~\");
                Thread.Sleep(10);
                Console.WriteLine(@"              '       (          ");
                Thread.Sleep(10);
                Console.WriteLine(@"             :                   ");
                Thread.Sleep(10);
                Console.WriteLine(@"            ; .         \--      ");
                Thread.Sleep(10);
                Console.WriteLine(@"          :   \         ;        ");
                Thread.Sleep(2000);
                Console.WriteLine();
                Console.WriteLine("Enter...");
                Console.ReadLine();
            }
            while (player.data.hp > 0 && monster.data.hp > 0) // 전투 루틴
            {
                // 플레이어가 제대로 된 입력을 할때까지 루프를 돌아야한다
                while (true)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
                    //
                    MonsterStatus(phase2); // 몬스터 정보
                    bool isValidAction = PlayerTurn();   // 플레이어가 제대로 턴을 마치면 true를 리턴
                    if (isValidAction)
                    {
                        break;
                    }
                }

                if (monster.data.hp <= 0) break; // 몬스터가 죽으면 루프 종료
                if (Phase2(ref phase2)) // 2페이즈에 진압하기 위한 bool값
                {
                    phase2 = true;
                    Phase2Statuse(); // 2페이즈 능력치 상승
                }
                MonsterTurn();
                if (player.data.hp <= 0) break; // 플레이어가 죽으면 루프 종료
                monster.data.mp += 1; // 몬스터의 스킬 차징
            }
            return BattleResult(); // 결과
        }

        private void MonsterStatus(bool phase2) // 몬스터의 정보
        {
            if (!phase2)
            {
                Console.WriteLine($"{monster.data.ascii}");
            }
            else
            {
                monster.Ascii();
            }

            Console.WriteLine($"[체력: {monster.data.hp}][공격력: {monster.data.attack}][방어력: {monster.data.defence}]");
        }

        // bool값을 리턴하게 수정해서 메서드 이름을 바꾸고 싶었지만, 마땅한 이름이 떠오르지 않는다
        private bool PlayerTurn()
        {
            float attackPower;
            float damage;

            Console.WriteLine();
            Console.WriteLine($"플레이어 체력: {player.data.hp}");
            Console.WriteLine($"{player.data.name}의 턴 (1.일반 공격 | 2.스킬 )");
            int input = InputManager.Instance.GetValidNumber("원하시는 행동을 입력해주세요", 1, 2);
            switch (input)
            {
                case 1:
                    // 일반 공격플레이어의 최종 공격력 계산
                    NormalAttack(out attackPower, out damage);
                    Thread.Sleep(1000);
                    return true;    // 올바른 공격을 했으니 true
                case 2:
                    bool isSkillUsed = SkillManager.Instance.IsSkillUsed(player);
                    return isSkillUsed;
            }
            // 잘못 입력
            return false;
        }
        // 플레이어의 일반공격
        public void NormalAttack(out float attackPower, out float damage)
        {
            // 일반 공격플레이어의 최종 공격력 계산
            attackPower = player.PlayerAttack();
            damage = Math.Max(attackPower - monster.data.defence, 1);
            monster.data.hp -= damage;
            // 피해 계산 (최소 1 보장)
            Console.WriteLine("(。･`з･)ﾉ");
            Console.WriteLine($"{monster.data.name}에게 {damage} 피해!");
        }


        private bool Phase2(ref bool phase2) // 2페이즈에 진압하기 위한 bool값
        {
            if (monster.data.name == "드래곤" && monster.data.hp <= (monster.data.maxHp * 0.3) && !phase2)
            {
                return true;
            }
            return false;
        }
        private void Phase2Statuse() // 2페이즈 능력치 상승
        {
            monster.data.attack += 50;
            Console.WriteLine("!!ヽ(ﾟдﾟヽ)(ﾉﾟдﾟ)ﾉ!!");
            Thread.Sleep(500);
            Console.WriteLine($"{monster.data.name}의 공격력 증가!");
            Thread.Sleep(2000);
        }

        private void MonsterTurn()
        {
            float damage;
            Console.WriteLine();
            Console.WriteLine("\x1b[38;2;255;0;0mヽ( `皿´ )ﾉ\x1b[0m");
            if (monster.data.mp >= monster.data.maxMp)
            {
                float monAttack = monster.data.attack;
                Console.WriteLine($"{monster.data.skill}발동!");
                monster.Skill();
                damage = Math.Max(monster.data.attack - player.PlayerDefence(), 1);
                player.data.hp -= damage;
                monster.data.attack = monAttack;
                monster.data.mp = monster.data.mp - monster.data.maxMp;
            }
            else
            {
                damage = Math.Max(monster.data.attack - player.PlayerDefence(), 1);
                Console.WriteLine($"{player.data.name}에게 {damage} 피해");
                player.data.hp -= damage;
            }
            Thread.Sleep(1000);
        }

        private bool BattleResult() // 결과
        {
            if (monster.data.hp <= 0)
            {
                /// ongoing퀘스트에 고블린, 드래곤 퀘스트가 있는지 확인하고
                /// 몬스터가 고블린, 드래곤이면 count를 1 올린다
                foreach (var questPair in QuestManager.Instance.ongoingQuestDictionary)
                {
                    var quest = questPair.Value;
                    if (quest.questData.Name == "고블린 5마리 잡기" && monster.data.name == "고블린")
                    {
                        quest.questData.Count += 1;  // 고블린 처치 수 증가
                        if (quest.questData.Count < 5)
                        {
                            Console.WriteLine($"고블린을 {5 - quest.questData.Count}마리 더 잡아야 합니다");
                        }
                        else
                        {
                            Console.WriteLine($"고블린을 5마리 이상 잡았으니 Npc에게 가십시오");
                        }
                    }
                    else if (quest.questData.Name == "드래곤 잡기" && monster.data.name == "드래곤")
                    {
                        quest.questData.Count += 1;  // 드래곤 처치 수 증가
                        Console.WriteLine($"드래곤을 잡았으니 Npc에게 가십시오");
                    }
                }

                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"{monster.data.name}을(를) 처치했습니다! 보상을 획득합니다.");
                player.data.exp += monster.data.level;
                player.data.gold += monster.data.gold;
                Console.WriteLine($"Gold: {monster.data.gold}+");
                Console.WriteLine($"경험치: {monster.data.level}+");
                player.CheckLevelUp();
                Thread.Sleep(3000);
                //
                Console.Clear();
                Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
                //
                VictoryAscii.RandomVictory();
                Console.WriteLine();
                Console.WriteLine("Enter...");
                Console.ReadLine();
                SceneManager.Instance.LoadScene("town");
                return true;
            }
            else
            {
                player.data.hp = 1;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("  ~~~");
                Console.WriteLine(" (´･ω･)  =3");
                Console.WriteLine(" /　 ⌒ヽ");
                Console.WriteLine("(人＿＿つ_つ");
                Console.WriteLine("패배했습니다... 잠시 후 마을로 돌아갑니다.");
                Thread.Sleep(1000);
                Console.WriteLine("...");
                Thread.Sleep(1000);
                Console.WriteLine("...");
                Thread.Sleep(1000);

                SceneManager.Instance.LoadScene("town");
                return false;
            }
        }

        // 축하
        public class VictoryAscii
        {
            private static readonly List<string> victoryascii = new()
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

            public static void RandomVictory()
            {
                Random random = new Random();
                int index = random.Next(victoryascii.Count);
                Console.WriteLine(victoryascii[index]);
            }
        }
    }
}
