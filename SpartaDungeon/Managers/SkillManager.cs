using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    // 스킬 매니저가 가져야 할 것
    // 플레이어가 배운 스킬 저장: 리스트(또는 배열)
    public class SkillManager : Singleton<SkillManager>
    {
        // 스킬매니저는 모든 스킬을 관리한다
        public List<Skill> allSkillList;

        // 플레이어가 배운 스킬이 저장된다
        public List<Skill> playerSkillList;

        Player player;  // DataManager가 가진 player
        private string skillFilePath; // 기본 스킬 데이터 경로

        public void Init()
        {
            // DataManager를 먼저 초기화했다
            player = DataManager.Instance.player;
            if (allSkillList == null)
            {
                allSkillList = new List<Skill>();
            }
            if (playerSkillList == null)
            {
                playerSkillList = new List<Skill>();
            }
            skillFilePath = GetSkillFilePath();
            if (!File.Exists(skillFilePath))
            {
                // 기본 스킬 내용을 json에 저장(최초 1회)
                // skill.json을 만들고, 이 파일이 없으면 생성한다
                Console.WriteLine("퀘스트 데이터 파일이 없습니다. 새로 생성합니다.");
                SaveDefaultSkills(player); // 저장하고
                LoadSkillData();     // 불러와서 npc의 questList에 대입
            }
        }
        private string GetSkillFilePath()
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
            return Path.Combine(saveDirectory, $"skill.json");
        }

        // 플레이어 생성 이후에 호출해야한다
        // 기본 스킬데이터를 만들어서 저장
        private void SaveDefaultSkills(Player player)
        {
            List<object> defaultSkills = new List<object>()
            {
                /// 추가할 스킬이 있다면 여기에 기록
                (object) new Skill(1,"알파스트라이크",eClassType.WARRIOR,3,"공격력 * 2 로 하나의 적을 공격합니다.", 2*player.data.attack, 0f, 0f, 10f),
                (object) new Skill(2,"더블스트라이크",eClassType.WARRIOR,7,"공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다(적이 1명인 경우 1명을 2번 공격).",1.5f*player.data.attack, 0f, 0f, 15f),
                (object) new Skill(3,"파이어볼",eClassType.MAGE,3,"공격력 * 2 로 하나의 적을 공격합니다.", 2*player.data.attack, 0f, 0f, 10f),
                (object) new Skill(4,"체력회복",eClassType.MAGE,7,"전체 체력의 절반만큼 체력을 회복합니다.", 0f, 0f, Math.Min(0.5f*player.data.maxHp,player.data.maxHp), 15f),
                (object) new Skill(5,"아이스애로우",eClassType.MAGE,3,"공격력 * 2 로 하나의 적을 공격합니다.", 2*player.data.attack, 0f, 0f, 10f),
                (object) new Skill(6,"멀티플샷",eClassType.MAGE,7,"공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다(적이 1명인 경우 1명을 2번 공격).",1.5f*player.data.attack, 0f, 0f, 15f)
            };
            string jsonStr = JsonConvert.SerializeObject(defaultSkills, Formatting.Indented, new JsonSerializerSettings
            {
                // 혹시 모를 null 대비
                NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
                MissingMemberHandling = MissingMemberHandling.Ignore,

                TypeNameHandling = TypeNameHandling.All  // 다형성 정보 포함하여 직렬화
            });
            // quest.json파일에 저장
            File.WriteAllText(skillFilePath, jsonStr);
            Console.WriteLine("기본 스킬 데이터 저장 완료!");
            Thread.Sleep(1000);
        }

        // 스킬 내용 불러와서 SkillManager에 대입한다
        private void LoadSkillData()
        {
            // 호출하는 곳에서 파일경로 체크를 했으니 여기서는 하지 않는다
            try
            {
                string jsonStr = File.ReadAllText(skillFilePath);
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
                        if (obj is JObject jObject)
                        {
                            Skill? sk = jObject.ToObject<Skill>();
                            if (sk != null)
                            {
                                // 매개변수로 player를 받지 않은건, 현재 스킬 관리를 SkillManager에서 하기 때문
                                playerSkillList.Add(sk);
                            }
                        }
                    }
                }
                Console.WriteLine($"기본 스킬 데이터 {loadedObjs.Count}개 불러오기 완료!");
                Thread.Sleep(1000);
                //
                Console.Clear();
                Console.SetCursorPosition(0, 0); /// 커서를 왼쪽 맨 위로 이동
                //
            }
            catch (Exception ex)
            {
                // 예외 처리 로직
                Console.WriteLine($"Error loading skill data: {ex.Message}");
            }
        }

        // 플레이어가 특정레벨이 되면 스킬을 해금한다
        public void UnlockSkill()
        {
            // 전체 스킬리스트에서 플레이어의 스킬리스트에 없는것 중
            // 플레이어의 레벨 >= 스킬의 제한레벨인것은
            // 플레이어의 스킬리스트에 저장한다


        }



        // 플레이어가 스킬을 사용한다



    }
}
