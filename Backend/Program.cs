using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Teamtris
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize game state
            GameState game = new GameState(10, 29);
            game.players = new List<Player>();
            game.board = new int[10, 29];

            // create localhost web socket server on port 5202
            var wssv = new WebSocketServer("ws://0.0.0.0:5202");
            wssv.Start();
            wssv.AddWebSocketService<Play>("/play", () => new Play(game));

            // create thread to broadcast message every x milliseconds
            Thread thread = new Thread(() =>
            {
                while (true) { Thread.Sleep(5000); wssv.WebSocketServices.Broadcast(JsonConvert.SerializeObject(game)); }
            });

            thread.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
