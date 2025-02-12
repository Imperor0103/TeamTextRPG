using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class EquipScene : BaseScene
    {
        public Player player;
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public EquipScene(string name) : base(name) { }
        #endregion

        public override void Awake()
        {
        }
        public override void Start()
        {
            player = DataManager.Instance.player;
        }

        public override void Update()
        {
            // 장착관리창
            // 화면출력

            ItemManager.Instance.PrintInventory();
            // 출력메뉴를 보여준다
            Console.WriteLine("0.나가기\n\n"); // 
            //Console.Write($"장착하고싶은 아이템의 번호를 입력해주세요. \n>>");
            // 입력받기
            int input = InputManager.Instance.GetValidNumber("장착하고싶은 아이템의 번호를 입력해주세요.",
                0, ItemManager.Instance.ownedList.Count);
            if (input == 0)
            {
                SceneManager.Instance.LoadScene("inventory");
            }
            else
            {
                int arrIdx = input - 1;
                player.EquipItem(ItemManager.Instance.ownedList[arrIdx]);
            }
        }
    }
}
