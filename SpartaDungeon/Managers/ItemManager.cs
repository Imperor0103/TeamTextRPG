using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    public class ItemManager: Singleton<ItemManager>
    {
        // 생성자 만들지 않아도 됨
        public List<Equipment> equpimentList = new List<Equipment>();   

    }
}
