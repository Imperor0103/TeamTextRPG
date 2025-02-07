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
            Equipment item = new Armor();
            ItemManager.Instance.equpimentList.Add(item);
        }
        public override void Start()
        {
            for (int i = 0; i < ItemManager.Instance.equpimentList.Count; i++)
            {
                Console.Write($"{i + 1}");
                Console.Write(ItemManager.Instance.equpimentList[i].name + " | ");
                Console.Write(ItemManager.Instance.equpimentList[i].defence);
            }
        }
        public override void Update()
        {
          
        }
    }
}
