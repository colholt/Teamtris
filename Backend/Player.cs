using System;
using System.Collections.Generic;

public class Player
{
    public Player(int id, string name, string socketID)
    {
        this.id = id;
        this.name = name;
        this.socketID = socketID;
    }
    public Block currentBlock;
    public int id;
    public string name;
    public string socketID;
    private int websocketjunk;
    public Tuple<int, int> currentBlockPosition;
}