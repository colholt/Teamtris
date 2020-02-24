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
        this.gameState = gameState;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        if (packet.type == Packets.STATE_START)
        {

        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
    }
}
