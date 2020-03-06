using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * #class Player |
 * @author ??? | 
 * @language csharp | 
 * @desc TODO |
 */
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
    [JsonIgnore]
    public WebSocketSharp.WebSocket webSocket;
    public int[][] currentBlockPosition;
}