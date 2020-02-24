using System;
using System.Collections.Generic;

public class Player
{
    public Player(int id, string name, string socketID, WebSocketSharp.WebSocket webSocket)
    {
        this.id = id;
        this.name = name;
        this.socketID = socketID;
        this.webSocket = webSocket;
    }
    public Block currentBlock;
    public int id;
    public string name;
    public string socketID;
    public WebSocketSharp.WebSocket webSocket;
    public Tuple<int, int> currentBlockPosition;
}