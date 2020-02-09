using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Bot {

    public abstract String GetMove(Board board, List<Block> blocks);
   
}

public class SingleBot : Bot {
    public SingleBot() {
        // create a board for this bot
        Console.WriteLine("I AM A BOT");
    }

    /** 
        @@param
            int[][] board - current enviornment
            List<Block> blocks - contains the list of all the blocks to try to fit in this location
     */
    public override String GetMove(Board board, List<Block> blocks) {
        // get the max height of each column of the baord 
        int[] maxHeights = board.GetMaxHeights();

        return "I AM RETURNING A BOARD";
    }
}