using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        // 생성자 만들지 않아도 됨
        public List<Equipment> equimentList = new List<Equipment>();

        public void PrintItem()
        {
            Console.WriteLine($"[{SceneManager.Instance.GetCurrentScene().GetName()}]\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < ItemManager.Instance.equimentList.Count; i++)
            {
                // 장착했다면 [E] 출력
                Console.Write($"이름: {ItemManager.Instance.equimentList[i].name} | ");
                Console.Write($"종류: {ItemManager.Instance.equimentList[i].itemType} | ");
                Console.Write($"공격력: {ItemManager.Instance.equimentList[i].attack} | ");
                Console.Write($"방어력: {ItemManager.Instance.equimentList[i].defence} | ");
                Console.Write($"hp추가: {ItemManager.Instance.equimentList[i].hp} | ");
                Console.Write($"mp추가: {ItemManager.Instance.equimentList[i].mp} | ");
                Console.Write($"{ItemManager.Instance.equimentList[i].description} | ");
                // 아래의 메세지만 따로 출력하는 메서드 만든다
                PrintPrice(ItemManager.Instance.equimentList[i]);
            }
        }
        //public bool IsEquiped(Equipment item)
        //{
        //    if ()
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public bool IsOwned(Equipment item)
        {
            // 플레이어의 아이템 리스트에 item이 있는지 확인한다
            if (DataManager.Instance.player.ownedList.Contains(item))
            {
                return true;
            }
            return false;
        }
        public void PrintPrice(Equipment item)
        {
            // 구매한 것과 아닌 것의 출력 메세지는 달라야한다
            if (!IsOwned(item))
            {
                Console.Write($"{item.price}");
            }
            else // 구매했다
            {
                Console.Write("구매완료");
            }
        }
    }
}
