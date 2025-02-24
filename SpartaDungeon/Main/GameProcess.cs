﻿using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Main
{
    // 매니저를 추가할 경우 GameProcess의 멤버로 선언하고, GameProcess의 Awake에서 싱글턴으로 생성할것
    public class GameProcess
    {
        SceneManager sceneManager;
        ItemManager itemManager;
        DataManager dataManager;
        InputManager inputManager;
        QuestManager questManager;
        SkillManager skillManager;

        public static bool isPlaying;   // 종료하는 곳에서 false로 바꾼다

        public GameProcess()
        {
            Awake();
        }
        // 멤버 초기화는 Awake메서드에서 이루어진다
        public void Awake()
        {
            if (sceneManager == null)
            {
                sceneManager = SceneManager.Instance;
                // 씬을 초기화한다(유니티의 Build와 유사)
                sceneManager.Init();
            }
            if (itemManager == null)
            {
                itemManager = ItemManager.Instance;
            }
            if (dataManager == null)
            {
                dataManager = DataManager.Instance;
            }
            if (inputManager == null)
            {
                inputManager = InputManager.Instance;
            }
            if (questManager == null)
            {
                questManager = QuestManager.Instance;
            }
            if (skillManager == null)
            {
                skillManager = SkillManager.Instance;

                /// 아래의 allSkillList, playerSkillList는
                /// Init에서 생성하면 안된다. 
                ///
                /// 불러올때는 null이라서 불러온 데이터를 저장을 못한다
                ///
                /// 따라서 리스트는 SkillManager 초기화 할때 같이 하기로 한다

                if (skillManager.allSkillList == null)
                {
                    skillManager.allSkillList = new List<Skill>();
                }
                if (skillManager.playerSkillList == null)
                {
                    skillManager.playerSkillList = new List<Skill>();
                }
            }
            //// 버퍼 크기와 화면 크기 동기화:
            //// Console.SetWindowSize()와 Console.SetBufferSize()를 사용하여 콘솔의 화면 크기와 버퍼 크기를 맞추면
            //// Console.Clear 호출 후에 스크롤이 내려가지 않는다
            //Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
            ///// 콘솔 버퍼의 크기 고정
            //Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
        // 1번만 호출되는 메서드
        public void Start()
        {
            isPlaying = true;
            sceneManager.InitFirstScene();
        }
        public void Update()
        {
            // isPlaying이 false가 되면 게임 종료
            while (isPlaying)
            {
                sceneManager.Update();
            }
        }
    }
}
