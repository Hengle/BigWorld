using System;
using System.Collections.Generic;


namespace BigWorldGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {     
            using (var game = new MainGame())
            {
                game.Window.Position = new engenious.Point(10, 10);
                game.Run();
            }
        }
    }
}