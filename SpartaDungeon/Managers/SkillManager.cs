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

        /// <summary>
        /// LoadSkillData에서 Player의 정보를 필요로 하기 때문에
        /// 플레이어를 만들고 직업선택이 끝난 다음 호출해야한다
        /// </summary>
        public void Init()
        {
            // DataManager를 먼저 초기화했다
            /// 
            player = DataManager.Instance.player;
            /// 아래의 allSkillList, playerSkillList는
            /// Init에서 생성하면 안된다. 
            /// 
            /// 불러올때는 null이라서 불러온 데이터를 저장을 못한다
            /// 
            /// 따라서 리스트는 SkillManager 초기화 할때 같이 하기로 한다
            //if (allSkillList == null)
            //{
            //    allSkillList = new List<Skill>();
            //}
            //if (playerSkillList == null)
            //{
            //    playerSkillList = new List<Skill>();
            //}

            skillFilePath = GetSkillFilePath();
            if (!File.Exists(skillFilePath))
            {
                // 기본 스킬 내용을 json에 저장(최초 1회)
                // skill.json을 만들고, 이 파일이 없으면 생성한다
                Console.WriteLine("퀘스트 데이터 파일이 없습니다. 새로 생성합니다.");
                SaveDefaultSkills(player); // 저장하고
                LoadSkillData();     // 불러와서 npc의 questList에 대입
            }
            else
            {
                // skill.json이 있으면 불러와서 대입해야지
                LoadSkillData();
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
                (object) new Skill(1, "알파스트라이크",eClassType.WARRIOR, eSkillType.ATTACK , 3, "공격력 * 2 로 하나의 적을 공격합니다.", 1, 2*player.PlayerAttack(), 0f, 0f, 10f),
                (object) new Skill(2, "더블스트라이크",eClassType.WARRIOR, eSkillType.ATTACK , 7, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다(적이 1명인 경우 1명을 2번 공격).", 2, 1.5f*player.PlayerAttack(), 0f, 0f, 15f),
                (object) new Skill(3, "파이어볼",eClassType.MAGE, eSkillType.ATTACK , 3, "공격력 * 2 로 하나의 적을 공격합니다.", 1, 2*player.PlayerAttack(), 0f, 0f, 10f),
                (object) new Skill(4, "체력회복",eClassType.MAGE, eSkillType.HEAL ,7, "전체 체력의 절반만큼 체력을 회복합니다.", 1, 0f, 0f, Math.Min(0.5f*player.data.maxHp,player.data.maxHp), 15f),
                (object) new Skill(5, "아이스애로우", eClassType.ARCHER, eSkillType.ATTACK, 3, "공격력 * 2 로 하나의 적을 공격합니다.", 1, 2*player.PlayerAttack(), 0f, 0f, 10f),
                (object) new Skill(6, "멀티플샷", eClassType.ARCHER, eSkillType.ATTACK, 7, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다(적이 1명인 경우 1명을 2번 공격).", 2, 1.5f*player.PlayerAttack(), 0f, 0f, 15f)
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

        // skill.json 스킬 내용 불러와서 SkillManager에 대입한다
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
                        if (obj is Skill skill)
                        {
                            // 매개변수로 player를 받지 않은건, 현재 스킬 관리를 SkillManager에서 하기 때문
                            /// 새로 캐릭터를 생성할떄는 Init을 호출하여 allSkillList에 추가하고 있지만
                            /// 캐릭터를 불러올때 추가하는 allSkillList에 추가하는 것 또한 필요하다
                            /// 그것은 DataManager의 LoadData에서 한다
                            allSkillList.Add(skill);   //
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
        public void UnlockSkill(Player player)
        {
            // 전체 스킬리스트에서 플레이어의 스킬리스트에 없는것 중
            // 플레이어의 레벨 >= 스킬의 제한레벨인것은
            // 플레이어의 스킬리스트에 저장한다
            foreach (Skill skill in allSkillList)
            {
                // 플레이어가 배울 수 있는 기술 중에서
                if (skill.skillData.ClassType == player.data.classType)
                {
                    // 플레이어가 배우지 않았고, 레벨 조건을 충족하는 경우에 추가
                    if (!playerSkillList.Contains(skill) && player.data.level >= skill.skillData.Level)
                    {
                        playerSkillList.Add(skill);
                        Console.WriteLine($"새로운 스킬 {skill.skillData.Name} 를 배웠습니다");
                    }
                }
            }
        }
        // 플레이어의 기술 출력
        public void PrintPlayerSkill()
        {
            if (playerSkillList.Count > 0)
            {
                for (int i = 0; i < playerSkillList.Count; i++)
                {
                    Console.Write($"{i + 1} | ");
                    Console.Write($"{playerSkillList[i].skillData.Name} | ");
                    Console.Write($"{playerSkillList[i].skillData.Description} \n");
                }
            }
            else
            {
                Console.Write($"플레이어가 사용할 수 있는 기술이 없습니다.");
            }
        }
        // 
        public bool IsSkillUsed(Player player)
        {
            PrintPlayerSkill();
            // 1~2 기술선택
            // 0.나가기           
            int input = InputManager.Instance.GetValidNumber("사용하고 싶은 기술의 번호를 입력하세요", 0, playerSkillList.Count);
            if (input == 0)
            {
                return false;
            }
            else
            {
                int arrIndex = input - 1;
                if (playerSkillList[arrIndex] != null)
                {
                    UseSkill(playerSkillList[arrIndex], player, out float dam);
                    return true;
                }
                else
                {
                    Console.WriteLine("다시 입력하십시오");
                    return false;
                }
            }
        }


        /// <summary>
        /// 제네릭이 아니라면 out 키워드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skill"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public void UseSkill(Skill skill, Player player, out float damage)
        {
            damage = 0f; /// 밖에서 0f로 초기화
            // 해당 스킬의 Count 만큼 반복 사용한다
            Console.WriteLine($"{player.data.name}가 {skill.skillData.Name}을 사용했습니다!");
            if (skill.skillData.SkillType == eSkillType.ATTACK)
            {
                damage = skill.skillData.Attack;
                Console.WriteLine($"적에게 {damage}의 피해를 입혔습니다!");
                // float를 리턴
            }
            else if (skill.skillData.SkillType == eSkillType.HEAL)
            {
                // 리턴이 없다
                Console.WriteLine($"{player.data.name}가 {skill.skillData.Name}을 사용했습니다!");
                float healAmount = Math.Min(skill.skillData.Hp, player.data.maxMp - player.data.hp);
                player.data.hp += healAmount;
                Console.WriteLine($"{player.data.name}가 {healAmount}만큼 체력을 회복했습니다!");
            }
            else if (skill.skillData.SkillType == eSkillType.BUFF)
            {
                // 리턴이 없다
                Console.WriteLine($"{player.data.name}가 {skill.skillData.Name}을 사용했습니다!");
                Console.WriteLine($"{player.data.name}에게 버프 효과가 적용되었습니다!");
            }
        }

        /// <summary>
        /// 제네릭을 활용해서 리턴을 다르게 할 수 있다
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="player"></param>
        // 플레이어가 스킬을 사용한다
        public T UseSkill<T>(Skill skill, Player player)
        {
            // 해당 스킬의 Count 만큼 반복 사용한다
            Console.WriteLine($"{player.data.name}가 {skill.skillData.Name}을 사용했습니다!");

            if (skill.skillData.SkillType == eSkillType.ATTACK)
            {
                float damage = skill.skillData.Attack;
                Console.WriteLine($"적에게 {damage}의 피해를 입혔습니다!");
                // float를 리턴
                return (T)(object)damage;
            }
            else if (skill.skillData.SkillType == eSkillType.HEAL)
            {
                float healAmount = Math.Min(skill.skillData.Hp, player.data.maxMp);
                // 리턴이 없다
                return default;
            }
            else if (skill.skillData.SkillType == eSkillType.BUFF)
            {

                // 리턴이 없다
                return default;
            }
            return default;
        }

    }
}
