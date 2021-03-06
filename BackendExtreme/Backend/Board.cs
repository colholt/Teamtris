using System;
using System.Collections.Generic;
using System.Linq;

/**
 * #class Board |
 * @author ??? | 
 * @language csharp | 
 * @desc Board class to include the information that 
 * the board will need to have for a bot |
 */
public class Board
{
    /**
     * #function Board::board |
     * @author ??? |
	 * @desc TODO |
     * @header public int[,] board() | 
	 * @param type name : what it does |
	 * @returns type : what it does | 
	 */
    public int[,] board { get; set; }

    /**
     * #function Board::numFilledFloor |
     * @author ??? |
	 * @desc has the number of heights that are 
     * not 0 - meaning that they have already been filled |
     * @header public int numFilledFloor() | 
	 * @param type name : what it does |
	 * @returns type : what it does | 
	 */
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

        // Console.WriteLine("[{0}]", string.Join(", ", heights));
        this.maxHeights = heights;
    }

    /*
        @@param int[,] board - board to be copied
        @@return int[,] copied board - board that has been copied
    */
    public int[,] CopyBoard(int[,] data) {
        int[,] copiedBoard = new int[data.GetLength(0), data.GetLength(1)];

        for(int i = 0; i < data.GetLength(0); i++) {
            for(int j = 0; j < data.GetLength(1); j++) {
                copiedBoard[i,j] = data[i,j];
            }
        }
        return copiedBoard;
    }


    /*
        @@param int[,] board - board to be checked
        @@return bool emptyBoard - is the abord emptu
    */
    public bool IsBoardEmpty(int[,] data) {
        for(int i = 0; i < data.GetLength(0); i++) {
            for(int j = 0; j < data.GetLength(1); j++) {
                if(data[i,j] != 0) {
                    return false;
                }
            }
        }
        return true;
    }


    /*
        @@param int[,] board - board to be checked
        @@return int[,] filledBoard - board with next piece considered
    */
    public int[,] FillBoardWithPieceConsidered(int[,] data, List<Tuple<int, int>> nextDots) {
       foreach(Tuple<int, int> dot in nextDots) {
            // make that one be filled
            data[dot.Item1, dot.Item2] = 1;

            // make that whole column be filled
            for(int i = 0; i < data.GetLength(0); i++) {
                data[i, dot.Item2] = 1;
            }
       }

       return data;
    }


    /*
        @@param int[,] board - board to be checked
        @@return int[,] filledBoard - board with next piece considered
    */
    public int[,] FillBoardWithPiece(int[,] data, List<Tuple<int, int>> nextDots) {
       foreach(Tuple<int, int> dot in nextDots) {
            // make that one be filled
            data[dot.Item1, dot.Item2] = 1;

            // make that whole column be filled
            // for(int i = dot.Item1; i < data.GetLength(0); i++) {
            //     data[i, dot.Item2] = 1;
            // }
       }

       return data;
    }

}