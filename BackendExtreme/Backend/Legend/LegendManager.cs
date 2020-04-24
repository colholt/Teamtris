using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LegendManager : WebSocketBehavior
{
    private Thread thread;
    private int count;
    private string bean;

    public LegendManager(){}

    protected override void OnOpen()
    {
        Console.WriteLine("legend info recieved");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        string socketID = ID;

        // create the legend and send back
        CreateLegend();
    }

    public void CreateLegend() {
        LegendPacket legendPacket = new LegendPacket();
        var convertedInfo = JsonConvert.SerializeObject(legendPacket);
        Send(convertedInfo);
    }
 
}

    
