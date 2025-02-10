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
            currentScene = storeScene;
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
            // 씬을 교체하기전에 씬에 있는 모든 오브젝트를 삭제한다
            Destory();
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
        public void Destory()
        {
            // 씬이 닫히기 전, objectList에서 오브젝트의 참조를 삭제
            for (int i = 0; i < currentScene.objectList.Count; i++)
            {
                currentScene.objectList[i] = null;
                // null이 된다고 해서 반드시 삭제되는건 아니다.
                // 다른 곳에서 참조하고 있으면 GC가 수거하지 않는다
                // 예: 아이템을 플레이어가 소유하면, 해당 아이템은 플레이어가 참조하니까 사라지지않지
            }
        }
    }
}
