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
        public eQuestType QuestType { get; set; }
        public eQuestState State { get; set; }
        public string Description { get; set; }
    }

    public enum eQuestType
    {
        KILL,
        COLLECT,
    }
    public enum eQuestState
    {

    }

    // 퀘스트의 객체는 Npc와 QuestManager가 가지고 있다
    public class Quest
    {
        // 퀘스트 1개가 가질 수 있는 정보
        public string Name {  get; set; }
        public string Description { get; set; }

    }
}
