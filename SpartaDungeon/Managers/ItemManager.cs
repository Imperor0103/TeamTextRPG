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
        public List<Equipment> equimentList = new List<Equipment>();    // 아이템 저장

        // 인벤토리의 아이템
        public void PrintInventory()
        {
            Console.Clear();
            Console.WriteLine($"[{SceneManager.Instance.GetCurrentScene().GetName()}]\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < equimentList.Count; i++)
            {
                // 인벤토리씬 한정해서 장착한 아이템은 [E] 출력
                if (IsEquiped(equimentList[i]))
                {
                    Console.Write($"[E]");
                }
                Console.Write($"{i + 1} ");
                Console.Write($"이름: {equimentList[i].name} | ");
                Console.Write($"종류: {equimentList[i].itemType} | ");
                Console.Write($"공격력: {equimentList[i].attack} | ");
                Console.Write($"방어력: {equimentList[i].defence} | ");
                Console.Write($"hp추가: {equimentList[i].hp} | ");
                Console.Write($"mp추가: {equimentList[i].mp} | ");
                Console.Write($"{equimentList[i].description} | ");
            }
        }
        // 해당 아이템을 장착하고 있는지 여부
        public bool IsEquiped(Equipment item)
        {
            // 해당 아이템을 플레이어가 장착하고있다면
            if (DataManager.Instance.player.armedList.Contains(item))
            {
                return true;
            }
            return false;
        }
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
            // 이건 상점에서만 출력되는 메세지이다
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
        public Equipment CreateItem(string n, int t, int c, float a, float d, float h, float m, string des, int p)
        {
            if (t == 1) // 무기
            {
                Weapon weapon = new Weapon(n, t, c, a, d, h, m, des, p);
                SceneManager.Instance.GetCurrentScene().objectList.Add(weapon);
                ItemManager.Instance.equimentList.Add(weapon);
                return weapon;
            }
            else if (t == 2) // 갑옷
            {
                Armor armor = new Armor(n, t, c, a, d, h, m, des, p);
                SceneManager.Instance.GetCurrentScene().objectList.Add(armor);
                equimentList.Add(armor);
                return armor;
            }
            else // 포션
            {
                // 포션 추가해보기
                Potion potion = new Potion(n, t, c, a, d, h, m, des, p);
                SceneManager.Instance.GetCurrentScene().objectList.Add(potion);
                equimentList.Add(potion);
                return potion;
            }
        }
    }
}
