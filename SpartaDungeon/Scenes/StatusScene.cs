using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class StatusScene : BaseScene
    {
        Player player;
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public StatusScene(string name) : base(name) { }
        #endregion

        // 자식 클래스에서 반드시 구현
        public override void Awake()
        {

        }
        public override void Start()
        {
            player = DataManager.Instance.player;
        }
        public override void Update()
        {
            //
            Console.Clear();
            Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
            //

            player.ShowStatus();

            Console.WriteLine("0.나가기\n\n"); // 

            int input = InputManager.Instance.GetValidNumber("원하시는 행동을 입력해주세요", 0, 0);
            if (input == 0)
            {
                string prevSceneName = SceneManager.Instance.GetPrevScene().GetName();
                SceneManager.Instance.LoadScene(prevSceneName);
            }
        }

    }
}
