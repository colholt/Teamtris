using System;
using System.Collections.Generic;

public class GameState
{

    public long clock = 0;
    public Dictionary<int, Player> players;
    public Bot bot;
    public Board board;
    public GameState(int m, int n)
    {
        this.board = new Board(n, m); // is this right
    }

}