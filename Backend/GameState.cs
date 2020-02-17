using System;
using System.Collections.Generic;

public class GameState
{
    public GameState(int m, int n)
    {
        board = new int[m, n];
    }
    public long clock = 0;
    public Dictionary<int, Player> players;
    public int[,] board;
}