using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public struct PlayerData
    {
        public string name;
        public eClassType classType;
        public int level;
        public float attack;
        public float defence;
        public float maxHp;
        public float hp;
        public float maxMp;
        public float mp;
        public int exp;
        public int gold;
    }

    public enum eClassType
    {
        NONE = -1,
        WARRIOR = 1 << 0,   // 0001
        MAGE = 1 << 1,      // 0010
        ARCHER = 1 << 2,    // 0100
        // ALL은 할당 후, and연산 처리 -> 결과가 1개 나오면 그 타입을 출력함
        ALL = WARRIOR | MAGE | ARCHER  // 0111
    }

    public class Player
    {
        public PlayerData data;
        public List<Equipment> ownedList;
        // 장착여부 확인, 데이터 로드시 장착무기를 연결할 변수가 필요하다
        public List<Equipment> armedList;   // 해당 무기 장착여부를 검색하기 위해 사용
        public Armor? armor;
        public Weapon? weapon;

        public Player(PlayerData playerData)
        {
            if (ownedList == null)
            {
                ownedList = new List<Equipment>();

            }
            if (armedList == null)
            {
                armedList = new List<Equipment>();
            }
            data = playerData;
        }
        // 플레이어 함수들
        // 플레이어 상태보기
        public void ShowStatus()
        {
            Console.Clear();
            Console.Write($"[상태 보기]\n캐릭터의 정보가 표시됩니다.\n\n");
            Console.Write($"Lv. {DataManager.Instance.player.data.level} \n");
            Console.Write($"{DataManager.Instance.player.data.name} ({DataManager.Instance.player.data.classType}) \n");
            //
            Console.Write($"공격력 : {DataManager.Instance.player.data.attack} ");
            /// 아이템 공격력 추가
            if (DataManager.Instance.player.weapon != null)
            {
                Console.Write($"(+{DataManager.Instance.player.weapon.attack}) ");
            }
            Console.Write("\n");
            //
            Console.Write($"방어력 : {DataManager.Instance.player.data.defence} ");
            /// 아이템 방어력 추가
            if (DataManager.Instance.player.armor != null)
            {
                Console.Write($"(+{DataManager.Instance.player.armor.defence}) ");
            }
            Console.Write("\n");
            //
            Console.Write($"체 력 : {DataManager.Instance.player.data.hp} / {DataManager.Instance.player.data.maxHp} \n");
            Console.Write($"exp : {DataManager.Instance.player.data.exp} / 다음레벨까지 남은 경험치: {10 * DataManager.Instance.player.data.level - DataManager.Instance.player.data.exp} \n");
            Console.Write($"Gold : {DataManager.Instance.player.data.gold} G\n\n");
            Console.Write($"0. 나가기 \n\n");
            Console.Write($"원하시는 행동을 입력해주세요. \n>>");
            bool isValid = false;
            int num;
            while (!isValid)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out num))
                {
                    switch (num)
                    {
                        case 0:
                            isValid = true;
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.계속하려면 enter.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.계속하려면 enter.");
                    Console.ReadLine();
                }
            }
        }

        // 무기장착은 플레이어에서 한다
        public void EquipItem(Equipment item)
        {
            if (item != null)
            {
                if (item.itemType == 1) // 무기
                {
                    if (weapon == null)
                    {
                        weapon = item as Weapon;
                    }
                    else
                    {
                        // 기존의 장비 해제 후 착용
                        armedList.Remove(weapon);   // 기존 장비 제거
                        weapon = item as Weapon;
                        armedList.Add(weapon);      // 새 장비 추가
                    }
                }
                else if (item.itemType == 2)    // 갑옷
                {
                    // 있다면 착용, 없다면 안한다
                    if (armor == null)
                    {
                        armor = item as Armor;
                    }
                    else
                    {
                        // 기존의 장비 해제 후 착용
                        armedList.Remove(armor);    // 기존 장비 제거
                        armor = item as Armor;
                        armedList.Add(armor);       // 새 장비 추가
                    }
                }
                else
                {
                    Console.WriteLine("장비할 수 없는 아이템입니다");
                }
            }
            else
            {
                Console.WriteLine("아이템이 null입니다");  // 디버그
            }
        }

        // 무기, 갑옷 장착에 의한 공격력, 방어력 갱신
        public float PlayerAttack()
        {
            if (DataManager.Instance.player.weapon != null)
            {
                return data.attack + DataManager.Instance.player.weapon.attack;
            }
            else
            {
                return data.attack;
            }
        }
        public float PlayerDefence()
        {
            if (DataManager.Instance.player.armor != null)
            {
                return data.defence + DataManager.Instance.player.armor.defence;
            }
            else
            {
                return data.defence;
            }
        }


        // 전투와 관련된 로직

    }
}
