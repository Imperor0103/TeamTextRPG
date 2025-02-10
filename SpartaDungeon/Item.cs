namespace SpartaDungeon
{
    public interface ItemData
    {
        string name { get; }
        int itemType { get; }   // 1: 무기 2: 갑옷, 3: 포션, 
        int classType { get; }   // 1: 전사 2: 마법사 3: 궁수 4: ALL
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
        public class Armor : Equipment
        {
            public Armor(string n, int t, int c, float a, float d, float m, string des, int p) 
            {
            name = n;
            itemType = t;
            classType = c;
            attack = a;
            defence = d;
            hp = m;
            mp = p;
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

