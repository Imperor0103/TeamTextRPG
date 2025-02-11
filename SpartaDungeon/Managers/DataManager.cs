using Newtonsoft.Json;

namespace SpartaDungeon.Managers
{
    public class GameData
    {
        public int fileIndex;
        public Player player;
        // 불러올 때
        public GameData()
        {
            int arrayIndex = 0;
            player = new Player();
        }
        // 저장할 때
        public GameData(int idx, Player p)
        {
            fileIndex = idx;
            player = p;
        }
    }

    // 저장, 불러오기
    public class DataManager : Singleton<DataManager>
    {
        // 생성자 만들지 않아도 됨
        public Player player;   // 저장을 위해 플레이어는 DataManager의 멤버변수 

        public void ShowSaveSlots(GameData[] slots)
        {
            CheckSlots(slots);
            // 슬롯 정보를 보여준다
            for (int i = 0; i < slots.Length; i++)
            {
                Console.Write($"- {i + 1} ");
                if (slots[i] != null)
                {
                    // 해당 슬롯에 이미 저장 데이터가 있다면 저장 데이터를 표시
                    // 플레이어의 이름, 레벨, 무기, 갑옷, 돈 이정도만 표시하자
                    Console.Write($"이름:{slots[i].player.data.name} | " + $"레벨:{slots[i].player.data.level} | ");
                    if (slots[i].player.weapon != null)
                    {
                        Console.Write($"무기:{slots[i].player.weapon.Name} | ");
                    }
                    else
                    {
                        Console.Write($"무기:없음 | ");
                    }
                    if (slots[i].player.armor != null)
                    {
                        Console.Write($"갑옷:{slots[i].player.armor.Name} | ");
                    }
                    else
                    {
                        Console.Write($"갑옷:없음 | ");
                    }
                    Console.Write($"Gold :{slots[i].player.data.gold} G \n");
                }
                else
                {
                    Console.WriteLine("비어있는 저장슬롯");
                }
            }
        }
        // Slot배열에 데이터가 있는지 없는지 검사하여 
        public void CheckSlots(GameData[] slots)
        {
            // SaveLoadScene이 가진 저장슬롯을 보여준다
            // json이 저장되어있는 폴더를 검색하여, 해당 저장파일이 존재하면, 파일이름을 슬롯에 넣어야한다
            for (int i = 0; i < slots.Length; i++)
            {
                int fileIndex = i + 1;  // 파일이름에 사용한 번호(fileIndex)
                string filePath = GetFilePath(fileIndex);
                if (File.Exists(filePath))
                {
                    try
                    {
                        // 파일에서 JSON 데이터를 읽는다
                        string jsonData = File.ReadAllText(filePath);
                        // 역직렬화(JSON, XML -> object(원래의 객체))를 위한 설정
                        var settings = new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Include,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        // JSON 데이터를 GameData 객체로 변환
                        GameData loadedData = JsonConvert.DeserializeObject<GameData>(jsonData);
                        slots[i] = loadedData;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"슬롯{fileIndex} 데이터를 불러오는 중 오류 발생: {e.Message}");
                        slots[i] = null;
                    }
                }
                else
                {
                    // 파일이 없으면 해당 슬롯은 비어있는 상태
                    slots[i] = null;
                }
            }
        }
        // 세이브파일의 경로 가져오기
        public string GetFilePath(int fileIdx)
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;    // exe파일 생성위치
            DirectoryInfo directory = new DirectoryInfo(exeDirectory);
            // 3단계 상위 폴더(프로젝트 폴더)로 이동
            for (int i = 0; i < 3; i++)
            {
                directory = directory.Parent;
            }
            // 프로젝트 폴더에 SaveFiles 폴더 없으면 생성한다
            string saveDirectory = Path.Combine(directory.FullName, "SaveFiles");
            Directory.CreateDirectory(saveDirectory);
            return Path.Combine(saveDirectory, $"savefile_{fileIdx}.json");
        }
        // 폴더에 같은 이름의 세이브파일이 있는지 체크
        public bool IsSaveFile(int fileIdx)
        {
            return File.Exists(GetFilePath(fileIdx));
        }
        public void SaveData(int fileIdx)
        {
            // 파일, 슬롯의 인덱스를 받고, 저장한다
            GameData tmpData = new GameData(fileIdx, player);
            string jsonStr = JsonConvert.SerializeObject(tmpData);
            // SaveFiles에 세이브파일생성
            File.WriteAllText(GetFilePath(fileIdx), jsonStr);
        }
        public bool LoadData(int fileIdx)
        {
            player = new Player();
            GameData gameData = new GameData();
            string path = GetFilePath(fileIdx);
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,  // null값도 json에 저장
                    MissingMemberHandling = MissingMemberHandling.Ignore, // JSON 데이터에 C# 객체에 정의되지 않은 속성이 있을 때, 이를 무시하고 계속 진행할 수 있습니다 
                };
                gameData = JsonConvert.DeserializeObject<GameData>(jsonData, settings);
                // 데이터매니저의 플레이어에 데이터 저장
                player.data.name = gameData.player.data.name;
                player.data.classType = gameData.player.data.classType;
                player.data.level = gameData.player.data.level;
                player.data.attack = gameData.player.data.attack;
                player.data.defence = gameData.player.data.defence;
                player.data.maxHp = gameData.player.data.maxHp;
                player.data.hp = gameData.player.data.hp;
                player.data.maxMp = gameData.player.data.maxMp;
                player.data.mp = gameData.player.data.mp;
                player.data.exp = gameData.player.data.exp;
                player.data.gold = gameData.player.data.gold;
                // 플레이어의 소지아이템 저장
                foreach (var data in gameData.player.ownedList)
                {
                    player.ownedList.Add(data);
                }
                // 플레이어의 장착여부, 장착아이템 저장
                foreach (var data in gameData.player.armedList)
                {
                    player.armedList.Add(data);
                }
                player.weapon = gameData.player.weapon;
                player.armor = gameData.player.armor;
                return true;
            }
            return false;
        }
        // 선택한 슬롯에 데이터가 있는지 체크
        public void CheckSelectedSlot(GameData[] slots, int arrIdx)
        {
            // 매개변수로 넘어온 것은 배열의 인덱스
            // 현재 씬은 SaveLoadScene이므로 이전 씬의 데이터를 저장한다
            // 이때 이전 씬이 entry인 경우는 저장할 데이터가 없으므로 저장하지 않는다
            if (SceneManager.Instance.GetPrevScene() != null)
            {
                if (IsSlotVacant(slots[arrIdx]))
                {
                    // 슬롯에 데이터가 없다 -> 자동으로 저장

                }
                else
                {
                    // 슬롯에 데이터가 있다 -> 덮어쓰기, 삭제 중에서 선택

                }
            }
            else
            {
                Console.WriteLine("저장할 데이터가 없습니다");
            }
        }
        // 슬롯에 세이브파일이 이미 있는지 체크
        public bool IsSlotVacant(GameData slot)
        {
            if (slot == null)
            {
                return true;
            }
            return false;
        }
        // 세이브파일 변경
        public void OverwriteSaveFile(int arrIdx)
        {

        }
        // 세이브파일 삭제
        public void DeleteSaveFile(int fileIdx)
        {

        }
    }
}
