namespace SpartaDungeon
{
    public interface ItemData
    {
        string name { get; }
        int itemType { get; }   // 1: 무기 2: 갑옷, 3: 포션, 
        // string itemText { get {switch문을 작성} }
        // 기존의 switch문(확장 용이)
        string itemText
        {
            get
            {
                switch (itemType)
                {
                    case 1: return "무기";
                    case 2: return "갑옷";
                    case 3: return "포션";
                    default: return "알 수 없음";
                }
            }
        }
        int classType { get; }   // 1: 전사 2: 마법사 3: 궁수 4: ALL
        // switch 표현식 사용(간결한 방법)
        string classText => classType switch
        {
            1 => "전사",
            2 => "마법사",
            3 => "궁수",
            _ => "캐릭터 직업이 없습니다(다시 불러오기)"
        };

        float attack { get; }
        float defence { get; }
        float hp { get; }
        float mp { get; }
        string description { get; }
        int price { get; }
    }

    public abstract class Equipment : ItemData
    {
        public string name { get; set; }
        public int itemType { get; set; }
        public int classType { get; set; }
        public float attack { get; set; }
        public float defence { get; set; }
        public float hp { get; set; }
        public float mp { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public bool IsEquipable { get; set; }


    }
    public class Armor : Equipment // 정확하게 명시해서 작성할것
    {
        public Armor(string n, int t, int c, float a, float d, float h, float m, string des, int p)
        {
            name = n;
            itemType = t;
            classType = c;
            attack = a;
            defence = d;
            hp = h;
            mp = m;
            description = des;
            price = p;
        }
    }

    public class Weapon : Equipment
    {
        public Weapon(string n, int t, int c, float a, float d, float h, float m, string des, int p)
        {
            name = n;
            itemType = t;
            classType = c;
            attack = a;
            defence = d;
            hp = h;
            mp = m;
            description = des;
            price = p;
        }
    }

    public class Potion : Equipment
    {
        public Potion(string n, int t, int c, float a, float d, float h, float m, string des, int p)
        {
            name = n;
            itemType = t;
            classType = c;
            attack = a;
            defence = d;
            hp = h;
            mp = m;
            description = des;
            price = p;
        }
    }
}

