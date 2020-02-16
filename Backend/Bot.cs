using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Bot {

    public abstract String GetMove(Board board, List<Block> blocks);
   
}

public class SingleBot : Bot {
    Prints botInfoPrinter;

    public SingleBot() {
        // create a board for this bot
        Console.WriteLine("I AM A BOT");
        botInfoPrinter = new Prints();
    }


    /** 
        @@param
            Board board - contains the the board that we want to make the move on
            Block block - contains the block that we want to fit
            int rotation - which roation we are trying to fit for
     */
    public void getFit(Board board, Block block, int rotation) {

        // positions of where the piece exists in the data in a tuple with both the ints for row and column
        List<Tuple<int, int>> dotPositions = new List<Tuple<int, int>>();
        
        // go through all the rows and get all the places where there is a true
        for(int row = 0; row < block.data.Length; row++) {
            dotPositions.AddRange(block.data[row].Select((b,i) => b == 1 ? i : -1).Where(i => i != -1).Select(index => new Tuple<int, int>(row, index)));
        }


        // print out the board
        Console.WriteLine("BOARD");
        botInfoPrinter.PrintMultiDimArr(board.board);

        // print the piece to be fit
        Console.WriteLine("PIECE");
        botInfoPrinter.PrintJaggedArr(block.data);

        // print the positions of the pieces
        botInfoPrinter.PrintPositions(dotPositions);


        // find the position at the bottommost left corner of the piece because that is where we start placing the piece on the board
        Tuple<int, int> bottomLeft = block.FindBottomLeft();

        // break it up into the bottom left row and column
        int bottomLeftRow = bottomLeft.Item1;
        int bottomLeftCol = bottomLeft.Item2;

        Console.WriteLine("THE BOTTOM LEFT ROW OF PIECE: " + bottomLeftRow);
        Console.WriteLine("THE BOTTOM LEFT COLUMN OF PIECE: " + bottomLeftCol);


        // go through each of the starting positions of the piece to find out the fit
        for(int i = 0; i < board.board.GetLength(1); i++) {
            // whether piece is compatible with other pieces
            bool compatiblePiece = true;

            int[,] shiftedOverPiece = new int[4,4];

            // look at each of the positions on the piece and see if there is a conflict on the board
            foreach(Tuple<int, int>  positionOfDot in dotPositions) {

                // dot to be tested
                int dotRowOnPiece = positionOfDot.Item1;
                int dotColOnPiece = positionOfDot.Item2;

                // shift over the dot to get rid of extra space
                int modRowOnPiece = 3 - (bottomLeftRow - dotRowOnPiece);
                int modDotCol = dotColOnPiece - bottomLeftCol;

                // fill in new piece with this dot and this is the piece we have to fit on the board at this specific column
                shiftedOverPiece[modRowOnPiece, modDotCol] = 1;

                Console.WriteLine("New Piece Getting Modified");
                // print out the piece
                botInfoPrinter.PrintMultiDimArr(shiftedOverPiece);

                // NEED TO SHIFT EVERYTHING ON THE BOARD

                // dot on the board and placement of the dot on the board

                
            }
        }

    }



    /** 
        @@param
            int[][] board - current enviornment
            List<Block> blocks - contains the list of all the blocks to try to fit in this location
     */
    public override String GetMove(Board board, List<Block> blocks) {
        // get the max height of each column of the baord 
        board.FindMaxHeights();

        // test out each of the pieces
        foreach(Block block in blocks) {

            // get the fit of the board with the area and whether a piece can clear a line
            getFit(board, block, 1);
            block.data = block.RotateMatrix();
            // getFit(board, block, 2);
            // block.data = block.RotateMatrix();
            // getFit(board, block, 3);
            // block.data = block.RotateMatrix();
            // getFit(board, block, 4);

        }

        return "I AM RETURNING A BOARD";
    }
}



public class DoubleBot : Bot {
    public override String GetMove(Board board, List<Block> blocks) {
        return "";
    }
}



public class TripleBot : Bot {
    public override String GetMove(Board board, List<Block> blocks) {
        return "";
    }
}



 /* deprecated for more efficient algorithm
                // go through all the parts of the data that we recieve starting at the bottom left corner and make sure it fits
                // ---|--
                // number of rows and the number of rows is fixed at 20
                for(int j = data.Length - 1; j >= 0 && j < board[i] + ROWS; j--) {
                    // number of columns to go through and only go as much as the board allows to
                    for(int k = 0; k < data[0].Length && k + i < board.Length; k++) {
                        // a dot exists there
                        if(data[j][k]) {
                        }
                    }
                }
             */