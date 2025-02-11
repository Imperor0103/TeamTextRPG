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
        public List<Equipment> equipmentList = new List<Equipment>();    // 아이템 저장

        // 인벤토리, 장착관리의 아이템
        public void PrintInventory()
        {
            Console.Clear();
            Console.WriteLine($"[{SceneManager.Instance.GetCurrentScene().GetName()}]\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < DataManager.Instance.player.ownedList.Count; i++)
            {
                // 장착관리씬 한정해서 장착한 아이템은 [E] 출력
                if (SceneManager.Instance.GetCurrentScene().GetName() == "equip"
                    && IsEquiped(DataManager.Instance.player.ownedList[i]))
                {
                    // 글씨 색 바꾸어보자
                    Console.ForegroundColor = ConsoleColor.Green;
                    // 배경색 바꾸기: Console.BackgroundColor
                    Console.Write($"[E]");
                }
                else
                {
                    Console.ResetColor();   // 색깔 원래대로
                }
                var item = DataManager.Instance.player.ownedList[i];
                Console.Write($"{i + 1} ");
                Console.Write($"이름: {item.name} | ");
                Console.Write($"종류: {item.itemType} | ");
                Console.Write($"공격력: {item.attack} | ");
                Console.Write($"방어력: {item.defence} | ");
                Console.Write($"hp추가: {item.hp} | ");
                Console.Write($"mp추가: {item.mp} | ");
                Console.Write($"{item.description} | ");
            }
            Console.WriteLine();
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
            switch (t)
            {
                case 1: // 무기                     
                    {
                        Weapon weapon = new Weapon(n, t, c, a, d, h, m, des, p);
                        SceneManager.Instance.GetCurrentScene().objectList.Add(weapon);
                        ItemManager.Instance.equipmentList.Add(weapon);
                        return weapon;
                    }
                case 2: // 갑옷
                    {
                        Armor armor = new Armor(n, t, c, a, d, h, m, des, p);
                        SceneManager.Instance.GetCurrentScene().objectList.Add(armor);
                        equipmentList.Add(armor);
                        return armor;
                    }
                case 3: // 포션
                    {
                        // 포션 추가해보기
                        Potion potion = new Potion(n, t, c, a, d, h, m, des, p);
                        SceneManager.Instance.GetCurrentScene().objectList.Add(potion);
                        equipmentList.Add(potion);
                        return potion;
                    }
                default:
                    Console.WriteLine("아이템이 생성되지 않았습니다");
                    return null;
            }
        }
        // 포션 사용 메서드(화면에 표시된 아이템 번호(실제 인덱스는 아이템 번호 -1)를 누르면 사용
        public void UsePotion(int scrNum)
        {

        }
    }
}
