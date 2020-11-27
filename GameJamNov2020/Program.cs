using System;

namespace GameJamNov2020
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameJamNov2020())
                game.Run();
        }
    }
}
