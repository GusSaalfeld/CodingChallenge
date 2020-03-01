using System;

namespace C__implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            GameplayManager game = new GameplayManager();
            game.playMatch(16);
            Console.WriteLine("Match over!");
        }
    }
}
