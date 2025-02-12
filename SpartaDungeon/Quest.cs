using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public struct QuestData
    {
        public string Name { get; set; }
        public eQuestType QuestType { get; set; }   // 퀘스트 종류
        public eQuestState State { get; set; }  // 상태
        public string Description { get; set; } // 설명
        public string Target { get; set; } // 잡아야 할 몬스터 이름
        public int Count { get; set; }  // 잡아야 할 몬스터의 수 
        public bool IsCleared { get; set; }    // 달성여부
        public int Ext { get; set; }   // 보상(경험치)
        public int Gold { get; set; }   // 보상(gold)
        public Equipment? rewardEquipment;   // 보상(아이템), 하지만 이건 추상클래스이므로 저장, 불러오기때 주의
        public string requiredQuest;     // 선행퀘스트의 이름
    }

    public enum eQuestType
    {
        NONE,   // 기본값
        KILL,       // 사냥
        COLLECT,    // 수집
    }
    public enum eQuestState
    {
        NONE,   // 기본값
        NOTSTARTED, // 시작 전
        INPROGRESS, // 진행 중
        COMPLETED,  // 완료
    }

    // 퀘스트의 객체는 Npc와 QuestManager가 가지고 있다
    public class Quest
    {
        // 퀘스트 1개가 가질 수 있는 정보
        public QuestData questData;
        // QuestData에 추상클래스 Equipement의 다형성을 이용하고 있으므로 저장, 불러오기때 주의한다
        // 아마 저장할때 쓸 것 같다
        public Quest(string n, eQuestType t, eQuestState s, string des, string tar, int c, bool i, int e, int g, Equipment? rew, string req)
        {
            questData.Name = n;
            questData.QuestType = t;
            questData.State = s;
            questData.Description = des;
            questData.Target = tar;
            questData.Count = c;
            questData.IsCleared = i;
            questData.Ext = e;
            questData.Gold = g;
            questData.rewardEquipment = rew;
            questData.requiredQuest = req;
        }
        // 불러오기 전용
        public Quest()
        {
            questData.Name = "";
            questData.QuestType = eQuestType.NONE;
            questData.State = eQuestState.NONE;
            questData.Description = "";
            questData.Target = "";
            questData.Count = 0;
            questData.IsCleared = false;
            questData.Ext = 0;
            questData.Gold = 0;
            questData.rewardEquipment = null;   // 기본값 null, 보상이 있는 경우 Weapon, Armor, Potion 중에 들어간다
            questData.requiredQuest = "";
        }
        // 불러올때 새로운 퀘스트를 저장하기 위함
        public Quest(QuestData data)
        {
            questData.Name= data.Name;
            questData.QuestType= data.QuestType;
            questData.State = data.State;
            questData.Description = data.Description;
            questData.Target = data.Target;
            questData.Count = data.Count;
            questData.IsCleared = data.IsCleared;
            questData.Ext = data.Ext;
            questData.Gold= data.Gold;
            questData.requiredQuest= data.requiredQuest;
            questData.requiredQuest = data.requiredQuest;
        }
    }
}
