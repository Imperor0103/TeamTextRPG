using SpartaDungeon.Managers;

namespace SpartaDungeon.Scenes
{
    public class StoreScene : BaseScene
    {
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public StoreScene(string name) : base(name) { }
        #endregion
        //
        public override void Awake()
        {
           
        }
        public override void Start()
        {

        }
        public override void Update()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("상점에 들어왔습니다!");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"Gold: 800G\n"); // {player.Gold} ?
                Console.WriteLine("구매할 아이템을 선택하세요!");
                for (int i = 0; i < ItemManager.Instance.equimentList.Count; i++)
                {
                    Console.Write($"{i + 1}" + " . ");
                    Console.Write(ItemManager.Instance.equimentList[i].name + " | ");
                    Console.Write("방어력: " + ItemManager.Instance.equimentList[i].defence + " | ");
                    Console.Write(ItemManager.Instance.equimentList[i].description + " | ");
                    Console.Write(ItemManager.Instance.equimentList[i].price + "G");                   
                }
                Console.WriteLine();
                Console.WriteLine("\n1. 아이템 구매하기\n2. 아이템 판매하기\n3. 상점 나가기");

               
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= ItemManager.Instance.equimentList.Count) // 
                {
                    var item = ItemManager.Instance.equimentList[choice - 1]; // itemManager
                    {
                        if (!ItemManager.Instance.IsOwned(item) && 800 >= item.price)
                        {
                            Console.WriteLine($"{item.name}을 구매하였습니다!");
                            // player.Gold -= item.price;
                            Console.WriteLine($"Gold: 800G\n"); // {player.Gold} ?
                            break;
                        }
                        else if (ItemManager.Instance.IsOwned(item))
                        {
                            Console.WriteLine("이미 구매한 아이템입니다!");
                            break;
                        }
                        else if (800 < item.price)
                        {
                            Console.WriteLine("골드가 부족합니다!");
                            break;
                        }
                        else if (choice == 2)
                        {
                            break;
                        }
                        else if (choice == 3)
                        {
                            Console.WriteLine("상점을 나갑니다.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                        }
                        Console.ReadKey();

                    }
                }
            }



        }
    }
}

