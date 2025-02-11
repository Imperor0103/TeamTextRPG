using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Equipment item = new Armor();
            Equipment item = ItemManager.Instance.CreateItem("존나 쌘 갑옷", 2, 1, 0f, 9999f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);
            Equipment item2 = ItemManager.Instance.CreateItem("존나 쌘 검", 1, 1, 9999f, 0f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);
            Equipment item3 = ItemManager.Instance.CreateItem("존나 쌘 갑옷22", 2, 1, 0f, 9999f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);
            Equipment item4 = ItemManager.Instance.CreateItem("존나 쌘 검22", 1, 1, 9999f, 0f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);
            Equipment item5 = ItemManager.Instance.CreateItem("존나 쌘 갑옷333", 2, 1, 0f, 9999f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);
            Equipment item6 = ItemManager.Instance.CreateItem("존나 쌘 검333", 1, 1, 9999f, 0f, 0f, 0f, "날 아무도 막을수 없으셈", 9999);

        }
        public override void Start()
        {
            for (int i = 0; i < ItemManager.Instance.equipmentList.Count; i++)
            {
                Console.Write($"{i + 1}");
                Console.Write(ItemManager.Instance.equipmentList[i].name + " | ");
                Console.Write(ItemManager.Instance.equipmentList[i].defence);
            }
        }
        public override void Update()
        {
            Console.WriteLine("1. 새로시작");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("3. 종료");

            Console.Write("\n선택을 입력하세요: ");

            switch (Console.ReadLine())
            {
                case "1":
                    break;
                case "2":
                    SceneManager.Instance.LoadScene("saveLoad");
                    break;
                case "3":
                    break;
            }

        }
    }
}
