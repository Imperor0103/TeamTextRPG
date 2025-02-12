using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        ///// <summary>
        ///// 플레이어가 가지고 있어야 할 것이지만, 
        ///// Equipment가 추상클래스라서 이걸 플레이어가 가지고 있으면 불러오기가 안된다
        ///// </summary>
        public List<Equipment> ownedList;
        // 장착여부 확인, 데이터 로드시 장착무기를 연결할 변수가 필요하다
        public List<Equipment> armedList;   // 해당 무기 장착여부를 검색하기 위해 사용


        // 인벤토리, 장착관리의 아이템
        public void PrintInventory()
        {
            Console.Clear();
            Console.WriteLine($"[{SceneManager.Instance.GetCurrentScene().GetName()}]\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < ItemManager.Instance.ownedList.Count; i++)
            {
                // 장착관리씬 한정해서 장착한 아이템은 [E] 출력
                var item = ItemManager.Instance.ownedList[i];
                if (SceneManager.Instance.GetCurrentScene().GetName() == "equip"
                    && IsEquiped(ItemManager.Instance.ownedList[i]))
                {
                    // 글씨 색 바꾸어보자
                    Console.ForegroundColor = ConsoleColor.Green;
                    // 배경색 바꾸기: Console.BackgroundColor
                    Console.Write($"{i + 1} ");
                    Console.Write($"[E] | ");
                }
                else
                {
                    Console.Write($"{i + 1} ");
                    Console.Write($"    | ");
                }
                Console.Write($"이름: {item.Name} | ");
                Console.Write($"종류: {item.ItemType} | ");
                Console.Write($"공격력: {item.Attack} | ");
                Console.Write($"방어력: {item.Defence} | ");
                Console.Write($"hp추가: {item.Hp} | ");
                Console.Write($"mp추가: {item.Mp} | ");
                Console.Write($"{item.Description} \n");
                Console.ResetColor();   // 색깔 원래대로
            }
            Console.WriteLine();
        }

        // 해당 아이템을 장착하고 있는지 여부
        public bool IsEquiped(Equipment item)
        {
            return ItemManager.Instance.armedList.Any(armedItem => armedItem.Name == item.Name);
        }
        public bool IsOwned(Equipment item)
        {
            return ItemManager.Instance.ownedList.Any(ownedItem => ownedItem.Name == item.Name);
        }
        public void PrintPrice(Equipment item)
        {
            // 이건 상점에서만 출력되는 메세지이다
            // 구매한 것과 아닌 것의 출력 메세지는 달라야한다
            if (!IsOwned(item))
            {
                Console.Write($"{item.Price}");
            }
            else // 구매했다
            {
                Console.Write("구매완료");
            }
        }
        public Equipment CreateItem (string name, int itemtype, string itemtext,int classtype, string classtext,float attack, float defense, float hp, float mp, string description, int price)
        {
            switch (itemtype)
            {
                case 1: // 무기                     
                    {
                        Weapon weapon = new Weapon(name, itemtype, itemtext, classtype, classtext, attack, defense, hp, mp, description, price);
                        SceneManager.Instance.GetCurrentScene().objectList.Add(weapon);
                        ItemManager.Instance.equipmentList.Add(weapon);
                        return weapon;
                    }
                case 2: // 갑옷
                    {
                        Armor armor = new Armor(name, itemtype, itemtext, classtype, classtext, attack, defense, hp, mp, description, price);
                        SceneManager.Instance.GetCurrentScene().objectList.Add(armor);
                        equipmentList.Add(armor);
                        return armor;
                    }
                case 3: // 포션
                    {
                        // 포션 추가해보기
                        Potion potion = new Potion(name, itemtype, itemtext, classtype, classtext, attack, defense, hp, mp, description, price);
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
