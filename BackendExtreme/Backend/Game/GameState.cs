using System;
using System.Collections.Generic;

public class GameState
{

    public long clock = 0;
    public int start_time;
    public int current_time;
    public int type = 100;
    public Dictionary<int, Player> players;
    public Bot bot;
    public Board board;
    public GameState(int rows, int cols)
    {
        this.board = new Board(rows, cols);
    }

}