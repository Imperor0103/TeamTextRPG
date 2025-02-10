﻿using SpartaDungeon.Managers;
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
        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public EquipScene(string name) : base(name) { }
        #endregion

        public override void Awake()
        {

        }

        public override void Start()
        {

        }

        public override void Update()
        {
            // 장착관리창
            // 화면출력
            ItemManager.Instance.PrintInventory();
            // 출력메뉴를 보여준다
            Console.WriteLine("0.나가기\n"); // 
            // 입력받기
            string input = InputManager.Instance.GetValidString("장착하고싶은 아이템의 번호를 입력해주세요.");
            switch (int.Parse(input))
            {
                case 1:
                    // 장착하기
                    break;
                case 0:
                    SceneManager.Instance.LoadScene("inventory");
                    break;
            }

        }

    }
}
