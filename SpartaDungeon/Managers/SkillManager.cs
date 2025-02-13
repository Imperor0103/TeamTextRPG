using Newtonsoft.Json;
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

        // 배열에 직접 저장할 것
        public List<Skill> playerSkillList;

        private string questFilePath; // 기본 스킬 데이터 경로

        public void Init()
        {
            if (allSkillList == null)
            {
                allSkillList = new List<Skill>();
            }
            if (playerSkillList == null)
            {
                playerSkillList = new List<Skill>();
            }
            questFilePath = GetSkillFilePath();
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

        //private void SaveDefaultSkills(Player player)
        //{
        //    List<object> defaultSkills = new List<object>()
        //    {
        //        /// 추가할 스킬이 있다면 여기에 기록



        //        (object) new QuestData { Name = "고블린 5마리 잡기", QuestType = eQuestType.KILL, State = eQuestState.NOTSTARTED, Description = "던전의 고블린을 5마리 처치하세요.", Target = "고블린", Count = 0, IsCleared = false, Ext = 50, Gold = 100, rewardEquipment = null, requiredQuest = "" },
        //        (object) new QuestData { Name = "소량의 체력 포션 3개 모으기", QuestType = eQuestType.COLLECT, State = eQuestState.NOTSTARTED, Description = "약초상에게 줄 포션 3개를 모아 오세요.", Target = "소량의 체력 포션", Count = 0, IsCleared = false, Ext = 30, Gold = 50, rewardEquipment = null, requiredQuest = "" },
        //        (object) new QuestData {Name = "드래곤 잡기", QuestType = eQuestType.KILL, State = eQuestState.NOTSTARTED, Description = "마을을 위협하는 드래곤을 처치하세요.", Target = "드래곤", Count = 0, IsCleared = false, Ext = 500, Gold = 1000, rewardEquipment = null, requiredQuest = "고블린 5마리 잡기"}
        //    };
        //    string jsonStr = JsonConvert.SerializeObject(defaultQuests, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        // 혹시 모를 null 대비
        //        NullValueHandling = NullValueHandling.Include,  // null값도 불러온다
        //        MissingMemberHandling = MissingMemberHandling.Ignore,

        //        TypeNameHandling = TypeNameHandling.All  // 다형성 정보 포함하여 직렬화
        //    });
        //    // quest.json파일에 저장
        //    File.WriteAllText(questFilePath, jsonStr);
        //    Console.WriteLine("기본 퀘스트 데이터 저장 완료!");
        //    Thread.Sleep(1000);
        //}


        public void InitSkill(Player player)
        {
            // 선택한 플레이어의 스킬들을 미리 만든다
            // 예시
            if (player.data.classType == eClassType.WARRIOR)
            {
                // 스킬 4개 만들기
                Skill skill_1 = new Skill(1, "백호참", eClassType.WARRIOR, 10, 10, 10, 10, 10);
                allSkillList.Add(skill_1);
                // skill_2, skill_3, skill_4도 이런식으로

            }
            else if (player.data.classType == eClassType.MAGE)
            {
                // 스킬 4개 만들기



            }
            else if (player.data.classType == eClassType.ARCHER)
            {
                // 스킬 4개 만들기



            }
        }

        // 이렇게 직접만들지 말고 아래의 메서드를 통해 인덱스에 넣는다
        //public Skill skill_1 { get; private set; }    // 공격스킬
        //public Skill skill_2 { get; private set; }    // 공격력상승
        //public Skill skill_3 { get; private set; }    // 방어력상승
        //public Skill skill_4 { get; private set; }    // 체력회복

        // 스킬 배우기 메서드
        public void LearnSkill(Player player, Skill skill)
        {
            // 스킬의 직업과 플레이어 직업을 체크 && 레벨체크(플레이어의 레벨 > 스킬레벨)
            if (player.data.classType == skill.skillData.ClassType &&
                player.data.level >= skill.skillData.Level)
            {
                // 스킬의 Id를 비교해서 "Id-1" 인덱스에 집어넣는다
                // 1번 스킬은 0번 인덱스에 저장
                playerSkillList[skill.skillData.Id - 1] = skill;
            }
        }

        // 스킬 사용하기 메서드(반환형이 있는것과 없는것을 만든다)
        public float UseSkill_1(Player player)
        {
            // 마나 소모
            player.data.mp -= playerSkillList[0].skillData.Mp;

            // 공격한다(데미지를 리턴해서 몬스터한테 주면 그게 공격임)
            float damage = 0;
            return damage;
        }
        public void UseSkill_2(Player player)
        {
            // 마나 소모
            player.data.mp -= playerSkillList[1].skillData.Mp;
            // 플레이어의 공격력을 "몇초간" 올린다
            player.data.attack += playerSkillList[1].skillData.Attack;
            // 플레이어의 공격력을 몇초간 올리는 방법

        }
        public void UseSkill_3(Player player)
        {
            // 마나 소모
            player.data.mp -= playerSkillList[2].skillData.Mp;
            // 플레이어의 방어력을 "몇초간" 올린다
            player.data.attack += playerSkillList[2].skillData.Defence;
            // 플레이어의 공격력을 몇초간 올리는 방법

        }

        public void UseSkill_4(Player player)
        {
            // 마나 소모
            player.data.mp -= playerSkillList[3].skillData.Mp;

            // 플레이의 체력을 회복한다
            player.data.hp += playerSkillList[0].skillData.Hp;
        }
        // 자료형은 바뀔 수 있음
    }
}
