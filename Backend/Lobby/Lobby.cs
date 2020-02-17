using System;
using System.Collections.Generic;

public class Lobby
{
    public Lobby(string id, int maxPlayers)
    {
        this.id = id;
        this.maxPlayers = maxPlayers;
    }
    public long clock = 0;
    public List<Player> players;
    public GameState game;
    public int numPlayers = 1;
    public string id;
    public int maxPlayers;
    public int[,] board;
}