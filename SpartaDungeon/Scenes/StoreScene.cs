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
            ItemManager.Instance.CreateItem("낡은 갑옷", 2, 1, 0, 50, 0, 0, "낡았지만 아직은 쓸만하다", 150);
            ItemManager.Instance.CreateItem("빛바랜 검", 1, 1, 30, 0, 0, 0, "색이 빠진 낡은 검, 겨우 휘두룰수 있는 정도다", 100);
            ItemManager.Instance.CreateItem("희미한 마력의 로브", 2, 2, 0, 40, 0, 0, "가죽 덩어리 같지만, 희미한 마력이 느껴진다", 150);
            ItemManager.Instance.CreateItem("편백나무 막대기", 1, 2, 70, 0, 0, 0, "피톤치드가 나오는 그럭저럭한 막대기", 100);
            ItemManager.Instance.CreateItem("가죽 갑옷", 2, 3, 0, 35, 0, 0, "가볍고 꽤 질긴 가죽으로 만든 갑옷", 150);
            ItemManager.Instance.CreateItem("참나무 곡궁", 1, 3, 90, 0, 0, 0, "조금 썩었지만, 가볍고 튼튼한 활", 100);
        }
        public override void Start()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine("상점에 들어왔습니다!");
            Console.WriteLine($"[보유 골드] {DataManager.Instance.player.data.gold}G\n");
            Console.WriteLine("구매할 아이템을 선택하세요!\n");

            var storeItems = ItemManager.Instance.equipmentList;

            for (int i = 0; i < storeItems.Count; i++)
            {
                var item = storeItems[i];
                Console.WriteLine($"{i + 1}. {item.name} | 종류: {item.itemType} | 직업: {item.classType} | 공격력: {item.attack} | 방어력: {item.defence} | {item.description} | {item.price}G");
            }

            Console.WriteLine("\n1. 아이템 구매하기");
            Console.WriteLine("2. 아이템 판매하기");
            Console.WriteLine("3. 상점 나가기");

            Console.Write("\n선택: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");

            }

            if (choice == 3)
            {
                Console.WriteLine("상점을 나갑니다.");
                Console.ResetColor();
                SceneManager.Instance.LoadScene("town");
               
            }
            else if (choice == 1)
            {
                BuyItem();
            }
            else if (choice == 2)
            {
                SellItem();
            }
            else
            {
                Console.WriteLine("잘못된 선택입니다.");
            }
        }


        private void BuyItem()
        {
            Console.Write("\n구매할 아이템 번호를 입력하세요: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > ItemManager.Instance.equipmentList.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            var item = ItemManager.Instance.equipmentList[itemIndex - 1];
            var player = DataManager.Instance.player;

            if (ItemManager.Instance.IsOwned(item))
            {
                Console.WriteLine("이미 구매한 아이템입니다!");
            }
            else if (player.data.gold >= item.price)
            {
                player.data.gold -= item.price;
                player.ownedList.Add(item);
               
                Console.WriteLine($"{item.name}을 구매하였습니다!");
                Console.WriteLine($"남은 골드: {player.data.gold}G");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다!");
            }
        }

        private void SellItem()
        {
            var player = DataManager.Instance.player;

            if (player.ownedList.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("\n[보유 중인 아이템 목록]");
            for (int i = 0; i < player.ownedList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.ownedList[i].name} | 판매 가격: {player.ownedList[i].price / 2}G");
            }

            Console.Write("\n판매할 아이템 번호를 입력하세요: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > player.ownedList.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            var item = player.ownedList[itemIndex - 1];
            player.ownedList.Remove(item);
            player.data.gold += item.price / 2;

            Console.WriteLine($"{item.name}을 {item.price / 2}G에 판매하였습니다!");
            Console.WriteLine($"현재 골드: {player.data.gold}G");


        }
    }
}

