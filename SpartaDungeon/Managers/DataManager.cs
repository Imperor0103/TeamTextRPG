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

        public void ShowSaveSlots()
        {
            Console.Clear();
            Console.WriteLine($"[{SceneManager.Instance.GetCurrentScene().GetName()}]\n현재까지의 데이터를 저장하거나, 이전의 데이터를 불러올 수 있습니다.\n");
            Console.WriteLine("[저장 슬롯]\n");
            CheckSlot();

        }
        public void CheckSlot()
        {

        }
    }
}
