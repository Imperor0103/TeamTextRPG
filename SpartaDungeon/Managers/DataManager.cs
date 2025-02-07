using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    // 저장, 불러오기
    public class DataManager: Singleton<DataManager>
    {
        // 생성자 만들지 않아도 됨
        public Player player;   // 저장을 위해 플레이어는 DataManager의 멤버변수 

    }
}
