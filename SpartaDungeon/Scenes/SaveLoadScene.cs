using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class SaveLoadScene: BaseScene
    {

        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public SaveLoadScene(string name): base(name) { }
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
            Console.WriteLine("[저장 / 불러오기] \n현재까지의 데이터를 저장하거나, 이전의 데이터를 불러올 수 있습니다.\n");
            // 저장슬롯 보여준다
            Console.WriteLine("[저장 슬롯]\n");
            DataManager.Instance.ShowSaveSlots();

        }
    }
}
