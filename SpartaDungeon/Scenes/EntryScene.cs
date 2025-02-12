using SpartaDungeon.Main;
using SpartaDungeon.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scenes
{
    public class EntryScene : BaseScene
    {
        Player player;
        PlayerData data;

        #region 새로운 생성자 만들기 금지
        // 생성자에서는 현재 씬의 이름만 설정한다. 씬에 있는 멤버들의 초기화는 Awake나 Start에서 한다
        public EntryScene(string name) : base(name) { }
        #endregion
        //
        public override void Awake()
        {
            Console.Clear();
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Console.WriteLine("게임 시작 화면\n");

            Console.WriteLine("1. 새로시작");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("0. 종료");

            switch (InputManager.Instance.GetValidNumber("\n선택을 입력하세요", 0, 2))
            {
                case 1:
                    CreateNewGame();
                    break;
                case 2:
                    SceneManager.Instance.LoadScene("saveLoad");
                    break;
                case 0:
                    Quit();
                    break;
            }
        }

        private void CreateNewGame()
        {
            CreateNewCharacter();

            Console.WriteLine("\n\n[ 새로생성 ]\n");

            bool isNameSaved = false; 

            // 이름이 저장될 때까지 반복
            while (!isNameSaved) 
            {
                Console.Write("원하시는 이름을 설정해주세요: ");

                player.data.name = Console.ReadLine();

                Console.WriteLine($"\n입력하신 이름은 '{player.data.name}'입니다.\n");

                // 올바른 입력이 들어올 때까지 반복
                while (true)
                {
                    Console.WriteLine("Y. 저장 / N. 취소");

                    Console.Write("\n선택을 입력하세요: ");

                    string choice = Console.ReadLine();

                    if (choice == "Y" || choice == "y")
                    {
                        Console.WriteLine("\n이름을 저장합니다.\n");
                        isNameSaved = true;
                        SelectClass();
                        break;
                    }
                    else if (choice == "N" || choice == "n")
                    {
                        Console.WriteLine("이름 설정을 취소합니다. 다시 입력해주세요.\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    }
                }
            }
        }

        private void CreateNewCharacter()
        {
            data = new PlayerData()
            {
                name = "",
                classType = eClassType.NONE,
                level = 1,
                attack = 10f,
                defence = 5f,
                maxHp = 100f,
                hp = 100f,
                maxMp = 100f,
                mp = 0f,
                exp = 0,
                gold = 1500
            };

            player = new Player(data);

            DataManager.Instance.player = player;
        }

        private void SelectClass()
        {
            // 올바른 입력이 들어올 때까지 반복
            while (true)
            {
                Console.WriteLine("\n1. 전사 / 2. 마법사 / 3. 궁수: ");

                switch (InputManager.Instance.GetValidNumber("\n직업을 선택하세요.", 1, 3))
                {
                    case 1:
                        player.data.classType = eClassType.WARRIOR;
                        Console.WriteLine("전사를 선택했습니다.\n");
                        Thread.Sleep(1000);
                        SceneManager.Instance.LoadScene("town");
                        return;
                    case 2:
                        player.data.classType = eClassType.MAGE;
                        Console.WriteLine("마법사를 선택했습니다.\n");
                        player.data.attack = 14f;
                        player.data.maxHp = 80f;
                        player.data.hp = 80f;
                        player.data.maxMp = 200f;
                        player.data.mp = 0f;
                        player.data.defence = 4f;
                        Thread.Sleep(1000);
                        SceneManager.Instance.LoadScene("town");
                        return;
                    case 3:
                        player.data.classType = eClassType.ARCHER;
                        Console.WriteLine("궁수를 선택했습니다.\n");
                        player.data.attack = 13f;
                        player.data.maxHp = 90f;
                        player.data.hp = 90f;
                        player.data.defence = 3f;
                        Thread.Sleep(1000);
                        SceneManager.Instance.LoadScene("town");
                        return;
                }
            }
        }

        private void Quit()
        {
            Console.WriteLine("게임을 종료합니다.");
            GameProcess.isPlaying = false;
        }
    }
}
