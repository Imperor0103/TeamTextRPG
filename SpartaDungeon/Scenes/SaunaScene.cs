using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class SaunaScene : BaseScene
    {
        Player player;

        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public SaunaScene(string name) : base(name) { }
        #endregion

        public override void Awake()
        {
            player = DataManager.Instance.player;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("여관");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.data.gold})");
            Console.WriteLine();
            Console.WriteLine("1.잠자기");
            Console.WriteLine("2.습식사우나");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write("행동을 선택하세요: ");
            switch (Console.ReadLine())
            {
                case "1":
                    RentedRoom(); break;
                case "2":
                    Rest(); break;
                case "0":
                    Console.WriteLine("마을로 돌아갑니다.\n");
                    SceneManager.Instance.LoadScene("town");
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    break;
            }
        }
        private void RentedRoom()
        {
            Console.WriteLine("잠을 잡니다.\n");
            if(player.data.hp == player.data.maxHp)
            {
                Thread.Sleep(1000);
                Console.WriteLine("일어나려면 Enter...");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine($"골드: {player.data.gold} -> {player.data.gold - 500}");
                Console.WriteLine("일어나려면 Enter...");
                Console.ReadLine();
                player.data.gold -= 500;
                player.data.hp = 100;
            }
            return;
        }

        private void Rest()
        {
            Console.WriteLine("습식사우나에 들어갑니다.");
            if (player.data.hp >= player.data.maxHp)
            {
                Console.WriteLine("체력이 이미 가득 찼습니다.");
                Console.WriteLine();
                return;
            }
            Console.WriteLine("휴식 중... (종료하려면 아무 키나 누르세요)");

            Task healTask = Task.Run(() =>
            {
                while (player.data.hp < player.data.maxHp)
                {
                    Thread.Sleep(2000);
                    player.data.hp = Math.Min(player.data.hp + 1, player.data.maxHp);
                    Console.WriteLine($"체력 +1 \n현재 체력: {player.data.hp}\n");
                    if (player.data.hp >= player.data.maxHp)
                    {
                        Console.WriteLine("체력이 가득 찼습니다.\n");
                        break;
                    }
                }
            });
            Task.WaitAny(healTask, Task.Run(() => Console.ReadKey()));
            Console.WriteLine("휴식 종료");
        }
    }
}
