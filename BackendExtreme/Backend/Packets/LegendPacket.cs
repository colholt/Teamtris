using System;
using System.Collections.Generic;

public class LegendPacket
{
    public List<string> ROTATE;
    public List<string> DOWN;
    public List<string> RIGHT;
    public List<string> LEFT;

    public LegendPacket() {
        ROTATE = new List<string>{"i"};
        DOWN = new List<string>{"s"};
        LEFT = new List<string>{"a"};
        RIGHT = new List<string>{"d"};
    }
}