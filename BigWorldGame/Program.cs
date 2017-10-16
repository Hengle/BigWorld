using System;
using System.Collections.Generic;
using engenious;

namespace BigWorldGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var game = new MainGame())
                game.Run();
        }
    }
}