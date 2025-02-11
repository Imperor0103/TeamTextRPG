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
        // 캐릭터 새로 생성
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
        // 캐릭터 불러오기
        public Player()
        {
            if (ownedList == null)
            {
                if (ownedList == null)
                {
                    ownedList = new List<Equipment>();

                }
                if (armedList == null)
                {
                    armedList = new List<Equipment>();
                }
                // PlayerData는 불러올 때 생성해서 플레이어의 data에 대입한다
            }
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
        }

        // 무기장착은 플레이어에서 한다
        public void EquipItem(Equipment item) // 입력받은건 가지고 있는 아이템
        {
            if (item != null)
            {
                // 장비하고 있는 아이템과 "같은" 것이면 해제, 다르면 장착
                if (armedList.Contains(item))   // 장비한 아이템은 armedList이 참조를 가지고 있다
                {
                    // 해당 아이템이 weapon인지 armor인지 판단 후 해제
                    armedList.Remove(item); // 리스트에서 제거하기전에 item을 null로 만들면 null참조 오류
                    if (item is Weapon)
                    {
                        weapon = null;
                        Console.WriteLine($"{item.name}을 해제했습니다.");
                    }
                    else if (item is Armor)
                    {
                        armor = null;
                        Console.WriteLine($"{item.name}을 해제했습니다.");
                    }
                }
                // 장비하고 있는 아이템과 다르다면
                else
                {
                    if (item.itemType == 1) // 무기
                    {
                        if (weapon == null)
                        {
                            weapon = item as Weapon;
                            armedList.Add(weapon);      // 새 장비 추가
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
                            armedList.Add(armor);       // 새 장비 추가
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
        // 경험치 체크와 레벨업
        public void CheckLevelUp()
        {

        }
    }
}
