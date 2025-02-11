namespace SpartaDungeon
{
    public interface ItemData
    {
        string Name { get; }
        int ItemType { get; }   // 1: 무기 2: 갑옷, 3: 포션, 
        // string itemText { get {switch문을 작성} }
        // 기존의 switch문(확장 용이)
        string ItemText
        {
            get
            {
                switch (ItemType)
                {
                    case 1: return "무기";
                    case 2: return "갑옷";
                    case 3: return "포션";
                    default: return "알 수 없음";
                }
            }
        }
        int ClassType { get; }   // 1: 전사 2: 마법사 3: 궁수 4: ALL
        // switch 표현식 사용(간결한 방법)
        string classText => ClassType switch
        {
            1 => "전사",
            2 => "마법사",
            3 => "궁수",
            _ => "캐릭터 직업이 없습니다(다시 불러오기)"
        };

        float Attack { get; }
        float Defence { get; }
        float Hp { get; }
        float Mp { get; }
        string Description { get; }
        int Price { get; }
    }

    public abstract class Equipment : ItemData
    {
        public string Name { get; set; }
        public int ItemType { get; set; }
        public string ItemText { get; set; }
        public int ClassType { get; set; }
        public string ClassText { get; set; }
        public float Attack { get; set; }
        public float Defence { get; set; }
        public float Hp { get; set; }
        public float Mp { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsEquipable { get; set; }


    }
    public class Armor : Equipment // 정확하게 명시해서 작성할것
    {
        public Armor(string name, int itemtype, string itemtext,int classtype, string classtext,float attack, float defense, float hp, float mp, string description, int price)
        {
            Name = name;
            ItemType = itemtype;
            ItemText = itemtext;
            ClassType = classtype;
            Attack = attack;
            Defence = defense;
            Hp = hp;
            Mp = mp;
            Description = description;
            Price = price;
        }
    }

    public class Weapon : Equipment
    {
        public Weapon(string name, int itemtype, string itemtext,int classtype, string classtext,float attack, float defense, float hp, float mp, string description, int price)
        {
            Name = name;
            ItemType = itemtype;
            ItemText = itemtext;
            ClassType = classtype;
            ClassText = classtext;
            Attack = attack;
            Defence = defense;
            Hp = hp;
            Mp = mp;
            Description = description;
            Price = price;
        }
    }

    public class Potion : Equipment
    {
        public Potion(string name, int itemtype, string itemtext,int classtype, string classtext, float attack, float defense, float hp, float mp, string description, int price)
        {
            Name = name;
            ItemType = itemtype;
            ItemText = itemtext;
            ClassType = classtype;
            ClassText = classtext;
            Attack = attack;
            Defence = defense;
            Hp = hp;
            Mp = mp;
            Description = description;
            Price = price;
        }
    }
}

