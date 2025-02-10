using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class InventoryScene : BaseScene
    {
        // 초기 멤버

        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public InventoryScene(string name) : base(name) { }
        #endregion
        //
        public override void Awake()
        {
            // 인벤토리의 멤버 초기화
            // 딱히 없다. 플레이어의 아이템을 보여주는데, 플레이어의 아이템은 DataManager가 가지고 있어서 안전하다

            // 디버그
            DataManager.Instance.player = new Player(new PlayerData()
            {
                name = "",
                classType = eClassType.NONE,
                level = 1,
                attack = 10f,
                defence = 5f,
                maxHp = 100f,
                hp = 100f,
                maxMp = 100f,
                mp = 0f,
                exp = 0,
                gold = 1500
            });
        }
        public override void Start()
        {
        }
        public override void Update()
        {
            // 화면출력
            ItemManager.Instance.PrintInventory();
            // 출력메뉴를 보여준다
            Console.WriteLine("1.장착 관리\n0.나가기\n"); // 
            // 입력받기
            string input = InputManager.Instance.GetValidString("원하시는 행동을 입력해주세요");
            switch (int.Parse(input))
            {
                case 1:
                    // 장착하기 씬으로 이동
                    SceneManager.Instance.LoadScene("equip");
                    break;
                case 0:
                    SceneManager.Instance.LoadScene("town");
                    break;
            }
            // 화면전환
        }
    }
}
