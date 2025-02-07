using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    public class ItemManager: Singleton<ItemManager>
    {
        // 생성자 만들지 않아도 됨
        /* public List<Item> itemList;
         public ItemManager()
         {
             if (itemList == null)
             {
                 itemList = new List<Item>();
             }
             CreateItem("존나 쌘 갑옷", eItemType.ARMOR, eClassType.WARRIOR, 0f, 10, 0f, 0f, "휘뚜루 마뚜루 막아대는 졸라 썐 갑옷이다", 9999);
             CreateItem("존나 쌘 검", eltemType.
         }

         private void CreateItem(string name, eItemType itemType, eClassType classType, float att, float def, float h, float m, string des, int pr)
         {
             Item item = new Item();
             item.SetData(name,itemType,classType,att,def,h,m,des,pr);
         }
        */

        public List<Equipment> equimentList = new List<Equipment>();



    }
}
