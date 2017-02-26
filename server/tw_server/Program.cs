using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Acoross.tw.Rpc;

namespace tw_server
{
    class TWSession : Acoross.tw.Rpc.RpcSession
    {
        Game game;
        PlayerObject pobj = null;

        public TWSession(Game game, Socket systemsocket) : base(systemsocket)
        {
            this.game = game;
        }

        Vector2 convert(vec2 v2)
        {
            return new Vector2(v2.x, v2.y);
        }

        public override Task<bool> Dash(DashReq req)
        {
            pobj.StartCoroutine("Dash", convert(req.orgPos), convert(req.dir), req.speed, req.actionTime, req.effectCount);
            return Task.FromResult(true);
        }

        public override Task<bool> Login(LoginReq req)
        {
            pobj = game.Instantiate<PlayerObject>((game) =>
            {
                return new PlayerObject(game, this, req.id);
            });

            Console.WriteLine($"login: {req.id}");
            return Task.FromResult(true);
        }

        public void OnEnd2()
        {
            Console.WriteLine($"{pobj?.name} destroyed");
            pobj?.Destroy(pobj);
        }
    }

    class Program
    {
        static async Task RunServer()
        {
            Game game = new Game();

            Task gameLoop = game.MainLoop();
            Console.WriteLine("game started");

            using (var server = new Acoross.Network.Rpc.RpcServer(System.Net.IPAddress.Any, 7777))
            {
                server.SetSessionBuilder(sock =>
                {
                    var session = new TWSession(game, sock);

                    session.OnEnd(t=>
                    {
                        session.OnEnd2();
                    });

                    Console.WriteLine($"{sock.RemoteEndPoint} connected");
                    return session;
                });

                Task serverTask = server.Start();
                Console.WriteLine("server started");

                await Task.WhenAll(serverTask, gameLoop);
            }
        }

        static void Main(string[] args)
        {
            RunServer().Wait();
        }
    }
}
