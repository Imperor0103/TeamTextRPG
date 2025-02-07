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
        public void Battle(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }
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
            int choice = InputManager.GetValidNumber("-입력-", 1, 9);
            {
                Monster monster = choice switch
                {
                    1 => new Goblin(),
                    2 => new Oak(),
                    3 => new Dragon(),
                    _ => null
                };
                if (choice == 9)
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        public bool Battle()
        {
            Console.WriteLine();
            Console.WriteLine($"{monster.Name}과(와)의 전투 시작!");
            Console.WriteLine($"[체력: {monster.Health}][공격력: {monster.Attack}]");
            Console.WriteLine();
            return false;
        }
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine($"{player.name}의 턴 (공격하려면 Enter)");
            Console.ReadLine();
            Console.WriteLine($"{monster.name}에게 {player.attack} 데미지!");
            Thread.Sleep(1000);
        }
        private void MonsterTurn()
        {
            Console.WriteLine();
            Console.WriteLine($"{monster.Name}의 턴");
            Console.WriteLine($"{player.name}에게 {monster.Attack} 피해");
            Thread.Sleep(1000);
        }
    }
}
