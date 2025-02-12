using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class TownScene : BaseScene
    {
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public TownScene(string name) : base(name) { }
        #endregion

        public override void Awake()
        {
            Console.Clear();
            Console.Write("마을 도착!");
        }
        public override void Start()
        {
        }

        public override void Update()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("1.상태보기");
            Console.WriteLine("2.인벤토리");
            Console.WriteLine("3.상점");
            Console.WriteLine("4.던전입장");
            Console.WriteLine("5.여관");
            Console.WriteLine("6.저장 / 불러오기");
            Console.WriteLine();

            switch (InputManager.Instance.GetValidNumber("\n선택을 입력하세요", 1, 6))
            {
                case 1:
                    Console.WriteLine("상태보기를 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("status");
                    return;
                case 2:
                    Console.WriteLine("인벤토리를 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("inventory");
                    return;
                case 3:
                    Console.WriteLine("상점을 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("store");
                    return;
                case 4:
                    Console.WriteLine("던전입장를 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("dungeon");
                    return;
                case 5:
                    Console.WriteLine("여관을 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("sauna");
                    return;
                case 6:
                    Console.WriteLine("저장 / 불러오기를 선택했습니다.\n");
                    SceneManager.Instance.LoadScene("saveLoad");
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    break;
            }
        }
    }
}
