using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Play : WebSocketBehavior
{
    private GameState gameState;
    private Thread thread;
    private int count;
    private string bean;

    public Play() : this(null)
    {
        Console.WriteLine("nulltown");
        // gameState = new GameState(10, 29);
        // gameState.players = new List<Player>();
        // gameState.board = new int[10, 29];
        // bean += 'a';
        // // create new thread to send the game state to player
        // if (gameState != null)
        // {
        //     thread = new Thread(SendState);
        //     thread.Start();
        // }
    }

    public Play(GameState gameState)
    {
        bean += 'a';
        Console.WriteLine(gameState);
        this.gameState = gameState;
        // thread = new Thread(SendState);
        // thread.Start();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Sessions.Broadcast(e.Data);
        gameState.players.Add(new Player(5));
    }

    protected override void OnClose(CloseEventArgs e)
    {
        thread.Abort(); // terminate thread on socket close
    }

    private void SendState()
    {
        while (true)
        {
            Thread.Sleep(5000);
            if (gameState != null)
                Console.WriteLine("sending one");
            gameState.clock = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Send(JsonConvert.SerializeObject(gameState));
        }
    }
}