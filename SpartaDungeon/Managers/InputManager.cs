using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        // 생성자 만들지 않아도 됨

        // 숫자 입력
        public int GetValidNumber(string prompt, int min, int max) // (출력, ~부터, ~까지)
        {
            int choice;
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다");
            }
        }

        // 문자열 입력
        public string GetValidString(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("잘못된 입력입니다");
                Console.Write(">> ");
                input = Console.ReadLine()?.Trim();
            }
            return input;
        }
    }
}
