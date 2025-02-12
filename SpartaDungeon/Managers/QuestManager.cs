using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    public class QuestManager: Singleton<QuestManager>
    {
        // 퀘스트의 타입이 존재한다

        // 퀘스트 클래스는
        // 1.퀘스트 타입(enum): 사냥, 수집
        // 2.퀘스트 상태(enum): 시작전, 진행중, 완료 (enum으로 하면 따로 bool값 선언하지 않아도 될 듯)
        // 3.퀘스트 정보: name, description, count(사냥: 몇마리, 수집: 몇개) , rewards(보상아이템), rewardCount(보상개수)


        // 퀘스트매니저가 가지고
        // npc 만들건가? 
        // npc가 퀘스트를 가지고 있고
        // 퀘스트매니저는 플레이어가 해결한 퀘스트를 가지고 있겠지
        // 플레이어가 퀘스트정보를 가지면 

        public List<Quest> clearedQList = new List<Quest>();    // 클리어한 퀘스트 저장

        // npc 알고 있어야겠다
        public Npc npc;

        // 퀘스트 클리어
        public void CLearQuest(Quest quest)
        {
            

        }

        // 클리어한 퀘스트
    }
}
