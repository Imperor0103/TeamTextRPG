using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace SpartaDungeon.Managers
{
    public class QuestManager : Singleton<QuestManager>
    {
        public Dictionary<string, Quest> completedQuestDictionary;    // 클리어한 퀘스트 저장
        public Dictionary<string, Quest> ongoingQuestDictionary;    // 진행중인 퀘스트 저장
        // npc는 QuestManager 초기화할때 Init 호출해서 생성(추가되는 npc는 모두 그렇게 처리한다)
        public Npc npc;

        // 기본 퀘스트 데이터 저장, 불러오기 관련해서 ChatGPT의 도움을 받았다
        private string questFilePath; // 기본 퀘스트 데이터 경로

        public void Init()
        {
            if (npc == null)
            {
                npc = new Npc();
            }
            if (completedQuestDictionary == null)
            {
                completedQuestDictionary = new Dictionary<string, Quest>();
            }
            if (ongoingQuestDictionary == null)
            {
                ongoingQuestDictionary = new Dictionary<string, Quest>();
            }
            // 처음 세이브파일을 만들때만 사용해야한다
            // 파일을 찾을 수 없을때만 실행한다
            questFilePath = GetQuestFilePath();
            if (!File.Exists(questFilePath))
            {
                // 퀘스트 내용을 json 저장(최초 1회)
                // quest.json을 만들고, 이 파일이 없으면 생성한다
                Console.WriteLine("퀘스트 데이터 파일이 없습니다. 새로 생성합니다.");
                SaveDefaultQuests(npc); // 저장하고
                LoadQuestData(npc);     // 불러와서 npc의 questList에 대입
            }
            else
            {
                // quest.json이 있으면 불러와서 대입해야지
                LoadQuestData(npc);
            }
        }
        // 퀘스트 내용을 저장한 json파일을 만든다
        private string GetQuestFilePath()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;    // exe파일 생성위치
            DirectoryInfo directory = new DirectoryInfo(exeDirectory);
            // 3단계 상위 폴더(프로젝트 폴더)로 이동
            for (int i = 0; i < 3; i++)
            {
                directory = directory.Parent;
            }
            // 프로젝트 폴더에 Json 폴더 없으면 생성한다
            string saveDirectory = Path.Combine(directory.FullName, "Json");
            Directory.CreateDirectory(saveDirectory);
            return Path.Combine(saveDirectory, $"quest.json");
        }
        // json에 퀘스트 내용 저장하기
        private void SaveDefaultQuests(Npc npc)
        {
            List<object> defaultQuests = new List<object>()
            {
                /// 추가할 퀘스트가 있다면 여기에 기록
                (object) new QuestData { Name = "고블린 5마리 잡기", QuestType = eQuestType.KILL, State = eQuestState.NOTSTARTED, Description = "던전의 고블린을 5마리 처치하세요.", Target = "고블린", Count = 0, IsCleared = false, Ext = 50, Gold = 100, rewardEquipment = null, requiredQuest = "" },
                (object) new QuestData { Name = "소량의 체력 포션 3개 모으기", QuestType = eQuestType.COLLECT, State = eQuestState.NOTSTARTED, Description = "약초상에게 줄 포션 3개를 모아 오세요.", Target = "소량의 체력 포션", Count = 0, IsCleared = false, Ext = 30, Gold = 50, rewardEquipment = null, requiredQuest = "" },
                (object) new QuestData {Name = "드래곤 잡기", QuestType = eQuestType.KILL, State = eQuestState.NOTSTARTED, Description = "마을을 위협하는 드래곤을 처치하세요.", Target = "드래곤", Count = 0, IsCleared = false, Ext = 500, Gold = 1000, rewardEquipment = null, requiredQuest = "고블린 5마리 잡기"}
            };
            string jsonStr = JsonConvert.SerializeObject(defaultQuests, Formatting.Indented, new JsonSerializerSettings
            {
                // 혹시 모를 null 대비
                NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
                MissingMemberHandling = MissingMemberHandling.Ignore,

                TypeNameHandling = TypeNameHandling.All  // 다형성 정보 포함하여 직렬화
            });
            // quest.json파일에 저장
            File.WriteAllText(questFilePath, jsonStr);
            Console.WriteLine("기본 퀘스트 데이터 저장 완료!");
            Thread.Sleep(1000);
        }
        // 퀘스트 내용 불러와서 npc에 대입한다
        // 저장파일에서 불러온 퀘스트 진행상황, 완료한 퀘스트 처리는 DataManager에서 한다
        private void LoadQuestData(Npc npc)
        {
            // 호출하는 곳에서 파일경로 체크를 했으니 여기서는 하지 않는다
            try
            {
                string jsonStr = File.ReadAllText(questFilePath);
                List<object>? loadedObjs;
                if (string.IsNullOrEmpty(jsonStr))
                {
                    // 빈 파일 처리
                    loadedObjs = new List<object>();
                    return;
                }
                loadedObjs = JsonConvert.DeserializeObject<List<object>>(jsonStr, new JsonSerializerSettings
                {
                    // 혹시 모를 null 대비
                    NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
                    MissingMemberHandling = MissingMemberHandling.Ignore,

                    TypeNameHandling = TypeNameHandling.All  // 다형성 처리
                });
                if (loadedObjs != null)
                {
                    foreach (var obj in loadedObjs)
                    {
                        if (obj is QuestData questData)
                        {
                            npc.questList[questData.Name] = new Quest(questData);
                        }
                    }
                }
                Console.WriteLine($"기본 퀘스트 데이터 {loadedObjs.Count}개 불러오기 완료!");
                Thread.Sleep(1000);
                //
                Console.Clear();
                Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
            //
            }
            catch (Exception ex)
            {
                // 예외 처리 로직
                Console.WriteLine($"Error loading quest data: {ex.Message}");
            }
        }

        // 퀘스트 시작하기
        public void StartQuest(Quest quest)
        {
            if (!ongoingQuestDictionary.ContainsKey(quest.questData.Name))
            {
                ongoingQuestDictionary[quest.questData.Name] = quest;
                Console.WriteLine($"퀘스트 시작: {quest.questData.Name}");
            }
        }
        // 퀘스트 완료 처리
        public void CompleteQuest(Quest quest)
        {
            if (quest.questData.State == eQuestState.COMPLETED)
            {
                completedQuestDictionary[quest.questData.Name] = quest;
                Console.WriteLine($"퀘스트 {quest.questData.Name} 완료!");
            }
        }
        // 진행중인 퀘스트 출력
        public void PrintOngoingQuest()
        {
            if (ongoingQuestDictionary.Count > 0)
            {
                int count = 1;
                foreach (var quest in ongoingQuestDictionary.Values)
                {
                    Console.WriteLine($"{count} | {quest.questData.Name} ");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("진행중인 퀘스트가 없습니다");
            }
        }
        // 완료한 퀘스트 출력
        public void PrintCompletedQuest()
        {
            if (completedQuestDictionary.Count > 0)
            {
                int count = 1;
                foreach (var quest in completedQuestDictionary.Values)
                {
                    Console.WriteLine($"{count} | {quest.questData.Name} ");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("완료한 퀘스트가 없습니다");
            }
        }
        // npc가 가지고 있는 퀘스트 출력
        public void PrintAllQuest(Npc npc)
        {
            int count = 1;
            foreach (var quest in npc.questList.Values)
            {
                if (quest.questData.IsCleared)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"{count} | " +
                    $"{quest.questData.Name} | " +
                    $"{quest.questData.Description} | " +
                    $"목표: {quest.questData.Target} | " +
                    $"개수: {quest.questData.Target} | " +
                    $"필수퀘스트: {quest.questData.requiredQuest}");
                Console.ResetColor();   // 색깔 원래대로
                count++;
            }
            Console.WriteLine();
        }
        public void SelectQuest(Npc npc)
        {
            // 입력받기
            Quest selectedQuest = new Quest();
            int input = InputManager.Instance.GetValidNumber("퀘스트 번호(1~3 중 선택)\n0. 나가기", 0, 3);
            switch (input)
            {
                case 1:
                    // 퀘스트1 선택
                    selectedQuest = npc.questList.ElementAt(0).Value; // "고블린 5마리 잡기"
                    break;
                case 2:
                    // 퀘스트2 선택
                    selectedQuest = npc.questList.ElementAt(1).Value; // "포션 10개 모으기"
                    break;
                case 3:
                    // 퀘스트3 선택
                    selectedQuest = npc.questList.ElementAt(2).Value; // "드래곤 잡기"
                    break;
                case 0:
                    SceneManager.Instance.LoadScene("town");
                    break;
            }
            // 같은 퀘스트 중복 받기 금지(ongoing, completed 모두 확인)
            if (ongoingQuestDictionary.ContainsKey(selectedQuest.questData.Name))
            {
                Console.WriteLine($"{selectedQuest.questData.Name} 는 이미 진행중입니다.");
                return; // 진행 중이면 선택 불가
            }
            else if (completedQuestDictionary.ContainsKey(selectedQuest.questData.Name))
            {
                Console.WriteLine($"{selectedQuest.questData.Name} 는 이미 완료했습니다.");
                return; // 완료했다면 선택 불가
            }
            // 선행 퀘스트나 조건을 만족하는지 체크
            if (IsQualified(selectedQuest))
            {
                Console.WriteLine($"퀘스트 '{selectedQuest.questData.Name}'을(를) 선택했습니다.");
                ongoingQuestDictionary[selectedQuest.questData.Name] = selectedQuest;
            }
            else
            {
                Console.WriteLine($"아직은 퀘스트 '{selectedQuest.questData.Name}'을(를) 받을 수 없습니다");
            }
        }


        // 퀘스트 해결여부 체크
        public bool IsQuestCleared(Quest quest)
        {
            if (quest.questData.IsCleared)
            {
                Console.WriteLine("이미 해결한 퀘스트입니다");
                return true;
            }
            return false;
        }

        // 퀘스트 레벨, 선행퀘스트 체크
        public bool IsQualified(Quest quest)
        {
            // 레벨제한은 구현이 쉬워서 넘어간다
            // 선행퀘스트 조건만 한다
            // 해결한 퀘스트인지 먼저 확인한다
            if (!quest.questData.IsCleared)
            {
                if (quest.questData.requiredQuest == "") // 선행퀘스트의 기본값은 "" 이다
                {
                    // 선행조건 없는 퀘스트
                    return true;
                }
                else
                {
                    // 클리어한 퀘스트에 있는지 문자열 비교
                    if (completedQuestDictionary.ContainsKey(quest.questData.requiredQuest))
                    {
                        // 선행퀘스트가 완료됨
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                // 이미 해결한 퀘스트
                return false;
            }
        }

        /// 퀘스트 해결 확인(이건 NPC와 대화할때 확인한다)
        public void CheckQuestSolved(Player player, Quest quest)
        {
            if (quest.questData.Name == "고블린 5마리 잡기")
            {
                if (quest.questData.Count >= 5)
                {
                    quest.questData.IsCleared = true;
                    SolveGoblinHuntQuest(player, quest);
                }
                else
                {
                    Console.WriteLine($"고블린을 {5 - quest.questData.Count}마리 더 잡아야 합니다");
                }
            }
            else if (quest.questData.Name == "소량의 체력 포션 3개 모으기")
            {
                // 포션체크는 여기서
                // LINQ를 잘 못써서 ChatGPT의 도움을 받았다
                foreach (var item in ItemManager.Instance.ownedList)
                {
                    if (item.Name == "소량의 체력 포션")
                    {
                        quest.questData.Count++;
                    }
                }
                if (quest.questData.Count >= 3)
                {
                    quest.questData.IsCleared = true;
                    CollectPotionQuest(player, quest);
                }
                else
                {
                    Console.WriteLine($"포션이{3 - quest.questData.Count}개 더 필요합니다");
                }
            }
            else if (quest.questData.Name == "드래곤 잡기")
            {
                if (quest.questData.Count >= 1)
                {
                    quest.questData.IsCleared = true;
                    SolveDragonHuntQuest(player, quest);
                }
                else
                {
                    Console.WriteLine("아직 드래곤을 잡지 않았군요");
                }
            }
        }
        // 고블린 퀘스트 해결
        public void SolveGoblinHuntQuest(Player player, Quest quest)
        {
            // 보상: 경험치 50, Gold 100
            Console.WriteLine("고블린 퀘스트 완료. 보상으로 경험치 50, Gold 100을 받았다");
            player.data.exp += 50;
            player.data.gold += 100;
            player.CheckLevelUp();
        }
        // 포션 퀘스트 해결
        public void CollectPotionQuest(Player player, Quest quest)
        {
            // 보상: 경험치 30, Gold 50
            Console.WriteLine("포션 퀘스트 완료. 보상으로 경험치 30, Gold 50을 받았다");
            player.data.exp += 30;
            player.data.gold += 50;
            player.CheckLevelUp();
        }
        // 드래곤 퀘스트 해결
        public void SolveDragonHuntQuest(Player player, Quest quest)
        {
            // 보상: 경험치 500, Gold 1000
            Console.WriteLine("드래곤 퀘스트 완료. 보상으로 경험치 500, Gold 1000을 받았다");
            player.data.exp += 500;
            player.data.gold += 1000;
            player.CheckLevelUp();
        }
        public void RewardPlayer(Npc npc, Player player)
        {
            // 플레이어에게 보상
            foreach (var quest in ongoingQuestDictionary.Values)
            {
                CheckQuestSolved(player, quest);   // 퀘스트 완료한 것이 있는지 확인한다
                // 완료한 것은 ongoing에서 제거하고 completed에 넣는다
                if (quest.questData.IsCleared)
                {
                    ongoingQuestDictionary.Remove(quest.questData.Name);
                    completedQuestDictionary[quest.questData.Name] = quest;
                }
            }
        }
    }
}
