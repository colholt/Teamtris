using System;
using System.Collections.Generic;

public class Block
{
    bool[,] data;
    int color;
    public Block(bool[,] data, int color)
    {
        this.data = data;
        this.color = color;
    }
    public int id;
    public Tuple<int, int> pos;
}