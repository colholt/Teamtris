using System;
using System.Collections.Generic;

public class GameState
{

    public long clock = 0;
    public List<Player> players;
    public Bot bot;
    public Board board;
    public GameState(){}

}