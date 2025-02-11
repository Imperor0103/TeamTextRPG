using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class SaveLoadScene : BaseScene
    {
        GameData[] saveSlots;
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public SaveLoadScene(string name) : base(name) { }
        #endregion
        //
        public override void Awake()
        {
            saveSlots = new GameData[5];
        }
        public override void Start()
        {

        }
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine("[저장 / 불러오기] \n현재까지의 데이터를 저장하거나, 이전의 데이터를 불러올 수 있습니다.\n");
            // 저장슬롯 보여준다
            Console.WriteLine("[저장 슬롯]\n");
            DataManager.Instance.ShowSaveSlots(saveSlots);
            Console.WriteLine("1~5 중에서 슬롯을 선택하여 저장, 불러오기, 삭제 가능\n0.이전화면으로 돌아가기\n"); // \n4.게임 저장\n5.게임 불러오기\n6.종료

            int input = InputManager.Instance.GetValidNumber("저장하고 싶은 슬롯의 번호를 입력해주세요.", 0, saveSlots.Count());
            if (input == 0)
            {
                string prevSceneName = SceneManager.Instance.GetPrevScene().GetName();
                SceneManager.Instance.LoadScene(prevSceneName);
            }
            else
            {
                // 선택한 세이브 슬롯에 데이터가 있는지 없는지 검사
                DataManager.Instance.CheckSelectedSlot(saveSlots, input);
            }
        }
    }
}
