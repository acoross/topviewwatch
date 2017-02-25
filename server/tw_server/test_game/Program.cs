using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tw_server;

namespace test_game
{
    class Program
    {
        static async Task MainAsync()
        {
            Game game = new Game();

            var player = new PlayerObject(game);
            player.name = "shin";

            game.Register(player);

            Task mainLoop = game.MainLoop();

            while (true)
            {
                var k = Console.ReadKey();
                if (k.Key == ConsoleKey.D)
                {
                    Input.SetButton("left shift");
                }
            }

            await mainLoop;
        }

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }
    }
}
