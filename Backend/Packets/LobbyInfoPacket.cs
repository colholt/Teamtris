using System.Collections.Generic;
public class LobbyInfoPacket
{
    public List<Player> players { get; set; }
    public string lobbyID { get; set; }
    public int maxPlayers { get; set; }
    public int dataType { get; set; }
}