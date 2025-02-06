using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    // 부모의 인스턴스 생성을 막기 위해 추상 클래스로 만들었다
    public abstract class BaseScene
    {
        string sceneName;
        public BaseScene(string name)
        {
            sceneName = name;
        }
        // 재정의하지 않았으므로 부모의 메서드를 재활용해서 사용
        public string GetName()
        {
            return sceneName;
        }
        // 자식 클래스에서 반드시 구현
        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
    }
}
