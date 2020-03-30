using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ScoresDirectManager : WebSocketBehavior
{
    private Thread thread;
    private int count;
    private string bean;

    public ScoresDirectManager(){}

    protected override void OnOpen()
    {
        Console.WriteLine("score recieved");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        string socketID = ID;
        
        Console.WriteLine("DIRECT MANAGER ");
        // score packet -- 11
        if (packet.type == Packets.SCORES_DIRECT) {
            Console.WriteLine("IN THE PACKET ");
            // just get top 10 scores
            GetTopScores();
        }
    }

    public ManyScoresPacket GetTopScores() {
        List<ScoresInfo> retrievedInfo = SQLConnection.GetTopTeams();

        // make into json a packet with the top teams info to return back
        ManyScoresPacket multipleScoresPacket = new ManyScoresPacket();
        multipleScoresPacket.topTeamInfos = retrievedInfo;
        multipleScoresPacket.currentTeamInfo = null;
        var convertedInfo = JsonConvert.SerializeObject(multipleScoresPacket);
        Send(convertedInfo);

        return multipleScoresPacket;
    }

}
