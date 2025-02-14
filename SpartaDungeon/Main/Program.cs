namespace SpartaDungeon.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameProcess gameProcess = new GameProcess();
            gameProcess.Start();
            gameProcess.Update();
        }
    }
}
