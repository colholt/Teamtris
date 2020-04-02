using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

public class TeamManager : WebSocketBehavior
{
    private Thread thread;
    private int count;
    private string bean;

    public TeamManager(){}

    protected override void OnOpen()
    {
        Console.WriteLine("team recieved");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        string socketID = ID;

      
    }
}

    
