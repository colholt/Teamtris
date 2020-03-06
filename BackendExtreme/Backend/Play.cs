using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * #class Play |
 * @author ??? | 
 * @language csharp | 
 * @desc TODO |
 */
public class Play : WebSocketBehavior
{
    private Dictionary<string, Lobby> lobbies;
    private Thread thread;
    private int count;
    private string bean;
    private int no;

    /**
     * #function Play::Play |
     * @author ??? |
	 * @desc TODO |
     * @header public Play() | 
	 */
    public Play() : this(null)
    {
    }

    public Play(Dictionary<string, Lobby> lobbies)
    {
        this.lobbies = lobbies;
        // thread = new Thread(SendState);
        // thread.Start();
    }

    protected override void OnOpen()
    {
        Console.WriteLine("new player in play service");
        count++;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        // if (packet.type == Packets.PLAYER_INPUT)
        // {
        //     PlayerInputPacket playerInputPacket = JsonConvert.DeserializeObject<PlayerInputPacket>(packet.data);
        //     Player currentPlayer = null;
        //     foreach (Player player in lobbies[playerInputPacket.lobbyID].players)
        //     {
        //         if (player.socketID == ID)
        //             currentPlayer = player;
        //     }
        //     if (currentPlayer == null)
        //         // no valid player found in lobby
        //         return;
        //     if (playerInputPacket.move == Move.MOVE_DOWN)
        //     {
        //         // move down
        //         // check collision
        //         currentPlayer.currentBlockPosition = new Tuple<int, int>(currentPlayer.currentBlockPosition.Item1 - 1, currentPlayer.currentBlockPosition.Item2);
        //     }
        //     if (playerInputPacket.move == Move.MOVE_LEFT)
        //     {
        //         // move left
        //         // check collision
        //         currentPlayer.currentBlockPosition = new Tuple<int, int>(currentPlayer.currentBlockPosition.Item1, currentPlayer.currentBlockPosition.Item2 - 1);
        //     }
        //     if (playerInputPacket.move == Move.MOVE_RIGHT)
        //     {
        //         // move right
        //         // check collision
        //         currentPlayer.currentBlockPosition = new Tuple<int, int>(currentPlayer.currentBlockPosition.Item1, currentPlayer.currentBlockPosition.Item2 + 1);
        //     }
        //     if (playerInputPacket.move == Move.HARD_DROP)
        //     {
        //         // hard drop
        //     }
        //     // soft drop is continuous move down
        //     if (playerInputPacket.move == Move.SOFT_DROP)
        //     {
        //         // soft drop
        //     }
        //     if (playerInputPacket.move == Move.ROTATE_CCW)
        //     {
        //         // rotate counter clockwise
        //     }
        //     if (playerInputPacket.move == Move.ROTATE_CW)
        //     {
        //         // move clockwise
        //         currentPlayer.currentBlock.RotateMatrix(); // probably this should change position of the block
        //     }
        // }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        // thread.Abort(); // terminate thread on socket close
        count--;
    }

}