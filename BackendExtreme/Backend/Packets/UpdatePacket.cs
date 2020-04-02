public class UpdatePacket
{
    public int playerID;
    public string move;
    public int type = Packets.UPDATE; // 8
    public int[][] shapeIndices;
}