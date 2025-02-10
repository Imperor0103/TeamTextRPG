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
            
        }
        public override void Start()
        {
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
            Console.WriteLine("3. 드래곤");
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
                3 => new Dragon(),
                _ => null
            };

            if (monster != null)
            {
                Console.WriteLine($"{monster.data.name}과(와)의 전투를 시작합니다!");
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
            Console.WriteLine($"[체력: {monster.data.maxHp}][공격력: {monster.data.attack}]");
            Console.WriteLine();
            Thread.Sleep(1000);
            while (player.data.hp > 0 && monster.data.hp > 0)
            {
                PlayerTurn();
                if (monster.data.hp <= 0) break; // 몬스터가 죽으면 루프 종료

                MonsterTurn();
                if (player.data.hp <= 0) break; // 플레이어가 죽으면 루프 종료

                Thread.Sleep(1000);
            }

            // 전투 결과 출력
            if (player.data.hp <= 0)
            {
                Console.WriteLine("⚔️ 당신은 전투에서 패배했습니다... 마을로 돌아갑니다.");
                return false;
            }
            
            Console.WriteLine($"🎉 {monster.data.name}을(를) 처치했습니다! 보상을 획득합니다.");
            // 보상 로직 추가
            return true;

        }
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine($"{player.data.name}의 턴 (공격하려면 Enter)");
            Console.ReadLine();
            float damage = Math.Max(player.data.attack - monster.data.defence, 1); // 1은 최소 대미지
            monster.data.hp -= damage;
            Console.WriteLine($"{monster.data.name}에게 {player.data.attack} 피해");
            Thread.Sleep(1000);
        }
        private void MonsterTurn()
        {
            Console.WriteLine();
            Console.WriteLine($"{monster.data.name}의 턴");
            float damage = Math.Max(monster.data.attack - player.data.defence, 1);
            player.data.hp -= damage;
            Console.WriteLine($"{player.data.name}에게 {monster.data.attack} 피해");
            Thread.Sleep(1000);
        }
    }
}
