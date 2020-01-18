using System;
using System.Collections.Generic;

public class Player
{
    public Player(int id)
    {
        this.id = id;
    }
    public Block currentBlock;
    private int id;
    public Tuple<int, int> currentBlockPosition;
}