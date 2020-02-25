using System;
using System.Collections.Generic;
using System.Linq;

/*
    Board class to include the information that the board will need to have for a bot
 */
public class Board
{
    public int[,] board { get; set; }

    // has the number of heights that are not 0 - meaning that they have already been filled
    public int numFilledFloor { get; set; }

    public int[] maxHeights { get; set; }

    public int height { get; set; }

    public int width { get; set; }

    public Board(int n, int m)
    {
        this.board = new int[n, m];
        this.height = n;
        this.width = m;
    }

    /** 
       @@param
       @@return
           int[] heights - contains the height of each column on the board
    */
    public void FindMaxHeights()
    {
        // go through the heights of the columns backwards to see which one is the last one that shows up
        // go backwards till reaching the first one in the column

        // make an array of length of the number of columns there are
        int[] heights = new int[this.board.GetLength(1)];

        int numCols = this.board.GetLength(1);
        int numRows = this.board.GetLength(0);

        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                // check if the board has a number there meaning a dot there
                if (this.board[j, i] == 1)
                {
                    // heights[i] = j - 1;
                    heights[i] = numRows - j;
                    // add it to the number that has been filled in the floor to see how many still need to be filled
                    if (heights[i] != 0)
                    {
                        numFilledFloor += 1;
                    }
                    break;
                }
            }
        }

        Console.WriteLine("[{0}]", string.Join(", ", heights));
        this.maxHeights = heights;
    }
}