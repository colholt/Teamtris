using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LobbyManager : WebSocketBehavior
{
    private Thread thread;
    private int count;
    private Dictionary<string, Lobby> lobbies;
    private string bean;


    public LobbyManager(Dictionary<string, Lobby> lobbies)
    {
        this.lobbies = lobbies;
        // thread = new Thread(SendState);
        // thread.Start();
    }

    protected override void OnOpen()
    {
        Console.WriteLine("user joined");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        string socketID = ID;
        // join packet -- 0
        if (packet.type == Packets.JOIN)
        {
            JoinPacket jPacket = JsonConvert.DeserializeObject<JoinPacket>(packet.data);
            joinLobby(jPacket.lobbyID, jPacket.playerID, jPacket.name, socketID);
        }
        // create packet -- 1
        else if (packet.type == Packets.CREATE)
        {
            try
            {
                Console.WriteLine("making new create packet");
                // Create a lobby with given parameters
                CreatePacket createPacket = JsonConvert.DeserializeObject<CreatePacket>(packet.data);
                createLobby(createPacket.maxPlayers, createPacket.name, createPacket.playerID, socketID, Context.WebSocket);
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("hi");
                Console.WriteLine(nre);
            }
        }
        // start packet -- 2
        else if (packet.type == 2)
        {
            startGame(packet);
        }
        else if (packet.type == Packets.PLAYER_INPUT)
        {
            PlayerInputPacket playerInputPacket = JsonConvert.DeserializeObject<PlayerInputPacket>(packet.data);
            Player currentPlayer = null;
            foreach (Player player in lobbies[playerInputPacket.lobbyID].players)
            {
                if (player.socketID == ID)
                    currentPlayer = player;
            }
            if (currentPlayer == null)
                // no valid player found in lobby
                return;
            // validate move
            foreach (Player player in lobbies[playerInputPacket.lobbyID].players)
            {
                if (player.socketID != ID)
                {
                    if (player.currentBlockPosition != null)
                    {
                        foreach (int[] pos1 in player.currentBlockPosition)
                        {
                            foreach (int[] pos2 in playerInputPacket.shapeIndices)
                            {
                                if (pos1 == pos2)
                                {
                                    Send("COLLISION");
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            currentPlayer.currentBlockPosition = playerInputPacket.shapeIndices;
            // UpdatePacket update = new UpdatePacket();
            // update.playerID = currentPlayer.id;
            // update.move = playerInputPacket.move;
            playerInputPacket.playerID = currentPlayer.id;
            UpdatePacket update = processInput(playerInputPacket);
            foreach (Player player in lobbies[playerInputPacket.lobbyID].players)
            {
                if (update.move == "FREEZE")
                {
                    foreach (int[] pos in playerInputPacket.shapeIndices)
                    {
                        // FREEZE
                        lobbies[playerInputPacket.lobbyID].game.board.board[pos[0], pos[1]] = pos[2];
                    }
                }
                if (player.socketID != ID)
                {
                    Sessions.SendTo(JsonConvert.SerializeObject(update), player.socketID);
                }
            }

            // on place piece put on board ;GJ
        }
        else if (packet.type == Packets.BOT_UPDATE) // 7
        {
            BotPacket bot = JsonConvert.DeserializeObject<BotPacket>(packet.data);
            if (bot.action == 1)
            {
                lobbies[bot.lobbyID].botCount++;
                lobbies[bot.lobbyID].numPlayers++;
            }
            else if (bot.action == 0)
            {
                lobbies[bot.lobbyID].botCount--;
                lobbies[bot.lobbyID].numPlayers--;
            }
        }
        else if (packet.type == Packets.SHAPE_BLUEPRINT)
        {
            ShapeBlueprintPacket sbp = JsonConvert.DeserializeObject<ShapeBlueprintPacket>(packet.data);
            foreach (Player player in lobbies[sbp.lobbyID].players)
            {
                if (player.socketID != ID)
                {
                    Sessions.SendTo(JsonConvert.SerializeObject(sbp), player.socketID);
                }
                else
                {
                    //player.currentBlock = sbp.shapeBlueprint;
                }
            }
        }
        else
        {
            Console.WriteLine("bad packet");
            Console.WriteLine(packet.type);
            Send("bad packet!!! :(");
        }
    }

    public void startGame(Packet packet)
    {
        PlayPacket playPacket = JsonConvert.DeserializeObject<PlayPacket>(packet.data);
        // 5 times number of players plus 5
        Lobby gameLobby = lobbies[playPacket.lobbyID];
        GameState game = new GameState(20, 5 * gameLobby.numPlayers + 5); // wat
        // game.board.board[0,1] = 2;
        // game.board.board[1,1] = 2;
        // game.board.board[2,1] = 2;
        // game.board.board[3,1] = 2;
        // game.board.board[4,1] = 2;
        // game.board.board[5,1] = 2;
        // game.board.board[6,1] = 2;
        // game.board.board[7,1] = 2;
        game.start_time = DateTime.Now.Millisecond;
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        switch (gameLobby.botCount)
        {
            case 1:
                gameLobby.bot = new SingleBot();
                break;
            case 2:
                gameLobby.bot = new DoubleBot();
                break;
            case 3:
                gameLobby.bot = new TripleBot();
                break;
            default:
                gameLobby.bot = null;
                break;
        }
        for (int i = 0; i < gameLobby.players.Count; i++)
        {
            players[gameLobby.players[i].id] = gameLobby.players[i];
        }
        game.players = players;
        gameLobby.game = game;
        // update all players that game will start
        alertLobby(0, playPacket.lobbyID, Packets.START);
        gameLobby.lobbyState = LobbyState.PLAYING;
    }

    public UpdatePacket processInput(PlayerInputPacket pip)
    {
        UpdatePacket update = new UpdatePacket();
        update.playerID = pip.playerID;
        update.move = pip.move;
        return update;
    }

    public void createLobby(int maxPlayers, string name, int id, string socketID, WebSocketSharp.WebSocket socketContext, string token = "-1")
    {
        int playerID = 1;
        if (token == "-1")
        {
            token = getToken();
        }
        // initialize a new lobby, player, and list of players
        Lobby newLobby = new Lobby(token, maxPlayers);
        Player newPlayer = new Player(1, name, socketID, socketContext);
        newLobby.players = new List<Player>();

        newLobby.players.Add(newPlayer);
        lobbies[newLobby.id] = newLobby;

        // Create a packet to confirm creation of new lobby
        ConfirmationPacket confirmationPacket = new ConfirmationPacket();
        confirmationPacket.lobbyID = newLobby.id;
        confirmationPacket.playerID = playerID;
        Send(JsonConvert.SerializeObject(confirmationPacket));

        Console.WriteLine(maxPlayers + name + id);
        Console.WriteLine("sending new lobby");
    }

    public LobbyInfoPacket joinLobby(string lobbyID, int playerID, string name, string socketID)
    {
        lobbyID = lobbyID.ToLower();
        Lobby lobby;
        if (lobbies == null)
        {
            Console.WriteLine("lobbies is null");
        }
        if (lobbies.ContainsKey(lobbyID))
        {
            lobby = lobbies[lobbyID];
            if (lobby.numPlayers < lobby.maxPlayers)
            {
                int newPlayerID = lobby.players.Count + 1;
                lobby.numPlayers += 1;
                Player newPlayer = new Player(newPlayerID, name, socketID, Context.WebSocket);
                lobby.players.Add(newPlayer);
                // send message to user of lobby id
                ConfirmationPacket confirmationPacket = new ConfirmationPacket();
                confirmationPacket.lobbyID = lobbyID;
                confirmationPacket.playerID = newPlayerID;
                alertLobby(playerID, lobbyID, Packets.UPDATE);
                LobbyInfoPacket lobbyInfoPacket = new LobbyInfoPacket();
                lobbyInfoPacket.lobbyID = lobby.id;
                lobbyInfoPacket.players = lobby.players;
                lobbyInfoPacket.lobbyID = lobbyID;
                lobbyInfoPacket.maxPlayers = lobby.maxPlayers;
                lobbyInfoPacket.dataType = Packets.UPDATE;
                Send(JsonConvert.SerializeObject(lobbyInfoPacket));
                Send(JsonConvert.SerializeObject(confirmationPacket, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore }));
                return lobbyInfoPacket;
            }
            else
            {
                // maxplayers reached :(
            }
        }
        else if (lobbyID == "testing")
        {
            lobby = lobbies["five"];
            lobby.players.Add(new Player(1, "bob", "no", (WebSocketSharp.WebSocket)null));
            return alertLobby(0, "testing", Packets.UPDATE);
        }
        else
        {
            //send message invalid ID
            Send("bad");
        }
        return null;
    }

    private string getToken()
    {
        var chars = "abcdefghijklmnopqrstuvwxyz";
        var stringChars = new char[4];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        return finalString;
    }

    static int RandomNumber(int min, int max)
    {
        Random random = new Random(); return random.Next(min, max);

    }

    public LobbyInfoPacket alertLobby(int playerID, string lobbyID, int type)
    {
        Console.WriteLine("checking for lobbyid " + lobbyID);
        LobbyInfoPacket lobbyInfoPacket = new LobbyInfoPacket();
        Lobby lobby;
        if (lobbies.ContainsKey(lobbyID))
        {
            lobby = lobbies[lobbyID];
            for (int j = 0; j < lobby.players.Count; j++)
            {
                lobbyInfoPacket.players = lobby.players;
                lobbyInfoPacket.lobbyID = lobbyID;
                lobbyInfoPacket.maxPlayers = lobby.maxPlayers;
                lobbyInfoPacket.dataType = type;
                try
                {
                    Sessions.SendTo(JsonConvert.SerializeObject(lobbyInfoPacket), lobby.players[j].socketID);
                }
                catch (Exception)
                {
                    // testing or invalid session
                }
            }
            return lobbyInfoPacket;
        }
        else if (lobbyID == "testing")
        {
            lobby = lobbies["five"];
            lobbyInfoPacket.players = lobby.players;
            lobbyInfoPacket.lobbyID = lobbyID;
            lobbyInfoPacket.maxPlayers = lobby.maxPlayers;
            lobbyInfoPacket.dataType = type;
            return lobbyInfoPacket;
        }
        else
        {
            Send("invalid ID");
            return lobbyInfoPacket;
        }
    }

    public void stateUpdate(string lobbyID)
    {
        Console.WriteLine("state update for lobby " + lobbyID);
        Lobby lobby = lobbies[lobbyID];
        for (int j = 0; j < lobby.players.Count; j++)
        {
            // Sessions.SendTo(JsonConvert.SerializeObject(lobbyInfoPacket), lobby.players[j].socketID);
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        // thread.Abort(); // terminate thread on socket close

        // On socket close remove player from lobby
        foreach (var lobby in lobbies.Values)
        {
            for (int j = 0; j < lobby.players.Count; j++)
            {
                if (lobby.players[j].socketID == ID)
                {
                    Console.WriteLine("deleting player " + lobby.players[j].name);
                    lobby.players.Remove(lobby.players[j]);
                    alertLobby(-1, lobby.id, -1);
                    // if lobby is void of players, remove the lobby
                    break;
                }
            }
            if (lobby.players.Count == 0)
            {
                Console.WriteLine("deleting lobby " + lobby.id);
                lobbies.Remove(lobby.id);
                break;
            }
        }
    }
}