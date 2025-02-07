using SpartaDungeon.Managers;
using System.ComponentModel;

namespace SpartaDungeon
{
    public interface ItemData
    {
        string name { get; }
        int itemType { get; }                                                                                                                              // ALL: 모두 장착가능
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
        public  int itemType { get; set; }                                                                                                                              // ALL: 모두 장착가능
        public  float attack { get; set; }
        public  float defence { get; set; }
        public float hp { get; set; }
        public float mp { get; set; }
        public string description { get; set; }
        public  int price { get; set; }
    }

    public class Armor : Equipment 
    {
        public Armor() 
        { 
            name = "존나 쌘 갑옷"; 
            itemType = 1; 
            defence = 9999; 
            description = "날 아무도 막을수 없으셈 ㅋㅋ"; 
            price = 9999; 
        } 
    }
    
    /* public enum eItemType
    {
        WEAPON = 1,
        ARMOR = 2,
    }

    public class Item
    {
        public ItemData itemData;

        public Item()
        {

        }

        public void SetData(string name, eItemType itemType, eClassType classType, float att, float def, float h, float m, string des, int pr)
        {
            itemData.name = name;
            itemData.itemType = itemType;
            itemData.classType = classType;
            itemData.attack = att;
            itemData.defence = def;
            itemData.hp = h;
            itemData.mp = m;
            itemData.description = des;
            itemData.price = pr;
        }
    }
    */
}
