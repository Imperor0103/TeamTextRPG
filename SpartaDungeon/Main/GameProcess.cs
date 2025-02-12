using SpartaDungeon.Managers;
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
                questManager.Init();
            }
            if (skillManager == null)
            {
                skillManager = SkillManager.Instance;
            }
        }
        // 1번만 호출되는 메서드
        public void Start()
        {
            isPlaying = true;
            sceneManager.InitFirstScene();
        }
        public void Loop()
        {
            // isPlaying이 false가 되면 게임 종료
            while (isPlaying)
            {
                Update();
            }
        }
        public void Update()
        {
            sceneManager.Update();
        }
    }
}
