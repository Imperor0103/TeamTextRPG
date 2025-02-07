using SpartaDungeon.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    // 모든 씬을 관리한다
    public class SceneManager : Singleton<SceneManager>
    {
        BaseScene prevScene;    // 이전에 열렸던 씬
        BaseScene currentScene; // 현재 열려있는 씬

        public Dictionary<string, BaseScene> sceneDictionary;

        // Scene을 생성해 sceneDictionary에 저장한다
        public void Init()
        {
            // 멤버의 초기화
            if (sceneDictionary == null)
            {
                sceneDictionary = new Dictionary<string, BaseScene>();
            }
            // 씬을 저장한 순서가 유니티의 BuildIndex의 순서
            BaseScene entryScene = new EntryScene("entry");
            sceneDictionary["entry"] = entryScene;
            BaseScene townScene = new TownScene("town");
            sceneDictionary["town"] = townScene;
            BaseScene inventoryScene = new InventoryScene("inventory");
            sceneDictionary["inventory"] = inventoryScene;
            BaseScene storeScene = new StoreScene("store");
            sceneDictionary["store"] = storeScene;
            BaseScene saveLoadScene = new SaveLoadScene("saveLoad");
            sceneDictionary["saveLoad"] = saveLoadScene;
            BaseScene dungeonScene = new DungeonScene("dungeon");
            sceneDictionary["dungeon"] = dungeonScene;
            // 처음 시작은 entry
            currentScene = entryScene;
        }
        // 1번만 호출되는 메서드
        public void InitFirstScene()
        {
            currentScene.Awake();
            currentScene.Start();
        }
        public void Update()
        {
            currentScene.Update();
        }
        // 씬을 로드하면, 현재 열려있던 씬은 prevScene, 새로 여는 씬은 currentScene에 저장된다
        public void LoadScene(string name)
        {
            prevScene = currentScene;
            currentScene = sceneDictionary[name];
            currentScene.Awake();
            currentScene.Start();
        }
        // 유니티의 GetActiveScene과 같다
        public BaseScene GetCurrentScene()
        {
            return currentScene;    // 현재 씬 리턴
        }
        public BaseScene GetPrevScene()
        {
            return prevScene;   // 이전 씬 리턴
        }
        // 유니티의 BuildIndex, sceneCount 가져오는 기능
        public int GetCurrentSceneIndex()
        {
            int buildIndex = 0;
            foreach (var pair in sceneDictionary)
            {
                if (pair.Value == currentScene)
                {
                    return buildIndex;
                }
                buildIndex++;
            }
            return -1;  // 못찾으면 -1 리턴
        }
    }
}
