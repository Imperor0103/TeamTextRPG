using Newtonsoft.Json;

namespace SpartaDungeon.Managers
{
    public class GameData
    {
        public int fileIndex;
        public Player player;

        // 아이템을 가지고 있어야 할 것 같다
        // 다형성을 유지하기위해 object형식으로 선언
        public List<object> ownedList;   // 아이템매니저의 ownedList에 있는 것을 저장
        public List<object> armedList;   // 아이템매니저의 armedList에 있는 것을 저장

        // 불러올 때
        public GameData()
        {
            if (ownedList == null)
            {
                ownedList = new List<object>();
            }
            if (armedList == null)
            {
                armedList = new List<object>();
            }
            int arrayIndex = 0;
            player = new Player();
        }
        // 저장할 때
        public GameData(int idx, Player p, List<Equipment> owned, List<Equipment> armed)
        {
            fileIndex = idx;
            player = p;
            /// 저장할 때 object로 변환
            ownedList = owned.Cast<object>().ToList();
            armedList = armed.Cast<object>().ToList();
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
        // Slot배열에 데이터가 있는지 없는지 검사하여 콘솔창에 표시
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
                            NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
                            MissingMemberHandling = MissingMemberHandling.Ignore,

                            TypeNameHandling = TypeNameHandling.All
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
            /// 아이템 매니저의 데이터 복사
            GameData tmpData = new GameData(fileIdx, player, ItemManager.Instance.ownedList, ItemManager.Instance.armedList);

            string jsonStr = JsonConvert.SerializeObject(tmpData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,      // null값도 json에 저장
                MissingMemberHandling = MissingMemberHandling.Ignore,  // JSON 데이터에 C# 객체에 정의되지 않은 속성이 있을 때, 이를 무시하고 계속 진행할 수 있습니다 

                TypeNameHandling = TypeNameHandling.All // 추상클래스인경우를 대비하여 실제 자료형을 기록한다
            });            // SaveFiles에 세이브파일생성
            File.WriteAllText(GetFilePath(fileIdx), jsonStr);
        }
        // 특정 슬롯에 연결된 세이브파일을 불러온다
        public bool LoadData(int fileIdx)
        {
            /// 불러오기를 하는 경우, ItemManager에 있던 기존의 리스트를 비워주지 않으면 같은 아이템이 뒤에 추가로 들어가는 문제가 발생
            ItemManager.Instance.ownedList.Clear();
            ItemManager.Instance.armedList.Clear();

            player = new Player();
            GameData gameData = new GameData();
            string path = GetFilePath(fileIdx);
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
                    MissingMemberHandling = MissingMemberHandling.Ignore, // JSON 데이터에 C# 객체에 정의되지 않은 속성이 있을 때, 이를 무시하고 계속 진행할 수 있습니다 

                    TypeNameHandling = TypeNameHandling.All
                };
                gameData = JsonConvert.DeserializeObject<GameData>(jsonData, settings);
                // 데이터매니저의 플레이어에 데이터 저장
                player.data = gameData.player.data;
                player.weapon = gameData.player.weapon;
                player.armor = gameData.player.armor;

                /// 아이템들이 object형식이므로 다시 원래의 자료형으로 바꿔준 다음, 아이템매니저의 리스트에 복사
                /// 나중에 LINQ 공부하고 나서 수정해보자
                foreach (var item in gameData.ownedList)
                {
                    if (item is Armor armor)
                    {
                        ItemManager.Instance.ownedList.Add(armor);
                    }
                    else if (item is Weapon weapon)
                    {
                        ItemManager.Instance.ownedList.Add(weapon);
                    }
                }
                foreach (var item in gameData.armedList)
                {
                    if (item is Armor armor)
                    {
                        ItemManager.Instance.armedList.Add(armor);
                    }
                    else if (item is Weapon weapon)
                    {
                        ItemManager.Instance.armedList.Add(weapon);
                    }
                }

                player.weapon = gameData.player.weapon;
                player.armor = gameData.player.armor;
                return true;
            }
            return false;
        }
        // 선택한 슬롯에 데이터가 있는지 체크
        public void CheckSelectedSlot(GameData[] slots, int fileIdx)
        {
            // 매개변수로 넘어온 것은 배열의 인덱스
            // 현재 씬은 SaveLoadScene이므로 이전 씬의 데이터를 저장한다
            // 이때 이전 씬이 entry인 경우는 저장할 데이터가 없으므로 저장하지 않는다
            int arrIdx = fileIdx - 1;
            if (IsSlotVacant(slots[arrIdx]))
            {
                // entry씬일 때는 저장할 수 없다
                if (SceneManager.Instance.GetPrevScene().GetName() == "entry")
                {
                    Console.WriteLine("저장할 데이터가 없습니다");
                    Thread.Sleep(1000);
                }
                // 슬롯에 데이터가 없다 -> 자동으로 저장
                SaveDataToSlot(slots, fileIdx);
            }
            else
            {
                // 슬롯에 데이터가 있다 -> 덮어쓰기, 삭제 중에서 선택
                Console.WriteLine("1.슬롯에 저장된 데이터 불러오기\n2.슬롯에 새로운 데이터 덮어쓰기\n3.슬롯에 저장된 데이터 삭제\n0.이전화면으로 돌아가기\n"); // \n4.게임 저장\n5.게임 불러오기\n6.종료
                int input = InputManager.Instance.GetValidNumber("원하시는 행동을 입력해주세요.", 0, 3);
                switch (input)
                {
                    case 0:
                        // 아직 SaveLoadScene이므로 씬을 바꾸면 안된다
                        break;
                    case 1:
                        LoadDataFromSlot(slots, arrIdx);
                        break;
                    case 2:
                        // entry씬일 때는 저장할 수 없다
                        if (SceneManager.Instance.GetPrevScene().GetName() == "entry")
                        {
                            Console.WriteLine("저장할 데이터가 없습니다");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            SaveDataToSlot(slots, fileIdx);
                        }
                        break;
                    case 3:
                        DeleteSaveFile(slots, arrIdx);
                        break;
                    default:
                        break;
                }
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
        // 슬롯에 데이터 저장하기(슬롯이 비어있는지 여부는 이전에 이미 판단했다)
        public void SaveDataToSlot(GameData[] slots, int fileIndex)
        {
            // 데이터매니저가 가진 player를 저장할 새 데이터를 생성
            int arrIdx = fileIndex - 1;
            GameData newData = new GameData(fileIndex, player, ItemManager.Instance.ownedList, ItemManager.Instance.armedList);
            // 데이터매니저의 player가 null인 경우에도 newData가 생성되는것에 주의
            // 플레이어가 null이 아닐때에만 저장
            if (newData.player != null)
            {
                SaveData(fileIndex);
                slots[arrIdx] = newData;
                Console.WriteLine($"슬롯 {slots[arrIdx].fileIndex}번에 새로운 데이터를 저장했습니다. 계속하려면 Enter를 누르세요.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("저장할 데이터가 없습니다.");
            }
        }
        // 슬롯에서 데이터파일 불러오기
        public void LoadDataFromSlot(GameData[] slots, int arrIdx)
        {
            int fileIndex = arrIdx + 1;
            bool isLoaded = LoadData(fileIndex);
            if (isLoaded)
            {
                Console.WriteLine($"슬롯 {slots[arrIdx].fileIndex}번의 데이터를 불러왔습니다. 계속하려면 Enter를 누르세요.");
                Console.ReadLine();
                // 마을씬으로 바꾼다
                SceneManager.Instance.LoadScene("town"); // 
            }
            else
            {
                Console.WriteLine($"저장된 데이터를 불러오는 데 실패했습니다. 계속하려면 Enter를 누르세요.");
                Console.ReadLine();
            }
        }
        // 세이브파일 삭제
        public void DeleteSaveFile(GameData[] slots, int arrIdx)
        {
            // 매개변수로 들어가는 슬롯에 있는 데이터 삭제
            int fileIdx = arrIdx + 1;
            string filePath = GetFilePath(slots[arrIdx].fileIndex);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // 슬롯 배열에서도 데이터를 제거
            slots[arrIdx] = null;
            Console.WriteLine($"슬롯 {fileIdx}번의 데이터가 삭제되었습니다.계속하려면 Enter를 누르세요.");
            Console.ReadLine();
        }
    }
}
