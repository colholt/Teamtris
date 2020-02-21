using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Bot {

    public abstract List<Tuple<int, int>> GetMove(Board board, List<Block> blocks);
   
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
        @@return
            List<Tuple<int, List<Tuple<int, int>>, int, int>> compatiblePieces - information about the pieces that are compatible on the board
     */
    public List<Tuple<int, List<Tuple<int, int>>, int, int, int>> getFit(Board board, Block block, int rotation) {

        // has all the positions compatible for this piece with the rotation, location on board, area covered, and if it can clear line and next line
        List<Tuple<int, List<Tuple<int, int>>, int, int, int>> compatiblePieces = new List<Tuple<int, List<Tuple<int, int>>, int, int, int>>();

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

        // piece with shifted over information
        int[,] shiftedOverPiece = new int[4,4];

        // to calculate the width of the piece
        int minCol = 5;
        int maxCol = -1;

        // get the indicies of the piece that have the bottomLeftRow
        HashSet<int> colsWithHighestHeight = new HashSet<int>();

        // shifted over dot positions
        List<Tuple<int, int>> shiftedDotPositions = new List<Tuple<int, int>>();

        // look at each of the positions on the piece and see if there is a conflict on the board
        foreach(Tuple<int, int>  positionOfDot in dotPositions) {

            // dot to be tested
            int dotRowOnPiece = positionOfDot.Item1;
            int dotColOnPiece = positionOfDot.Item2;

            // shift over the dot to get rid of extra space
            int modRowOnPiece = 3 - (bottomLeftRow - dotRowOnPiece);
            int modDotCol = dotColOnPiece - bottomLeftCol;

            // put into the list of indicies with the large heights
            if (modRowOnPiece == 3) {
                colsWithHighestHeight.Add(modDotCol);
            }

            // fill in new piece with this dot and this is the piece we have to fit on the board at this specific column
            shiftedOverPiece[modRowOnPiece, modDotCol] = 1;

            // add to the list of shifted dot positions
            shiftedDotPositions.Add(Tuple.Create(modRowOnPiece, modDotCol));

            // calculate the min and max of the columns
            minCol = Math.Min(minCol, dotColOnPiece);
            maxCol = Math.Max(maxCol, dotColOnPiece);
        }

        int widthOfPiece = maxCol - minCol + 1;
        Console.WriteLine("WIDTH OF PIECE " + widthOfPiece);

        
        Console.WriteLine("COLUMNS WITH THE LOWEST ROWS");
        botInfoPrinter.PrintSet(colsWithHighestHeight);


        Console.WriteLine();
        Console.WriteLine("SHIFTED OVER PIECE");
        botInfoPrinter.PrintMultiDimArr(shiftedOverPiece);


        // go through each of the starting positions of the piece to find out the fit
        for(int i = 0; i < board.board.GetLength(1) - widthOfPiece + 1; i++) {

            // where should the dot starting spot be placed with this location of the column
            int startingPieceHeightOnBoard = board.height - board.maxHeights[colsWithHighestHeight.First()] - 1;
            Console.WriteLine("\nSTARTING PIECE HEIGHT ON BOARD FOR COLUMN " + i + " IS " + startingPieceHeightOnBoard);
            // find the starting height for the pieces
            foreach(int j in colsWithHighestHeight) {
                Console.WriteLine("COL ON BOARD " + (j+i) + " HEIGHT IS " + (board.height - board.maxHeights[j+i] - 1 ));
                // see if that spot is okay with the max height and continue till it is okay with that height starting with
                while(board.height - board.maxHeights[j+i] - 1 < startingPieceHeightOnBoard) {
                    startingPieceHeightOnBoard = board.height - board.maxHeights[j+i] - 1;
                }
            }
            Console.WriteLine("MODIFIED PIECE HEIGHT ON BOARD FOR COLUMN " + i + " IS " + startingPieceHeightOnBoard + "\n");


            // compatible board info
            List<Tuple<int, int>> compatibleBoard = new List<Tuple<int, int>>();

            // modified board with the pieces in position
            int[,] modifiedBoard = new int[board.height,board.width];

            // dots nearby for area covered
            HashSet<Tuple<int, int>> dotsNearby = new HashSet<Tuple<int,int>>();

            // dots that fill the floor
            int dotsFillingFloor = 0;

            // see if all dots can be placed on the board without indexing issues in the column space
            foreach(Tuple<int, int> shiftedDotPosition in shiftedDotPositions) {
                // column to check for the spacing
                int shiftedDotRow = shiftedDotPosition.Item1;
                int shiftedDotCol = shiftedDotPosition.Item2;

                Console.WriteLine("SHIFTED DOT POSITION (" + shiftedDotRow + "," + shiftedDotCol + ")");

                Console.WriteLine("STARTING POSITION ON BOARD " + startingPieceHeightOnBoard +  " " + shiftedDotRow);
                // check whether the 2 heights are more than height of the board
                if((board.height - startingPieceHeightOnBoard - 1) + (4 - shiftedDotRow) >= board.height) {
                    Console.WriteLine("\n\n\n\n\n\n\nI AM HEREHEHEH");
                    compatibleBoard = null;
                    break;
                } 

                // shifted on the board size
                int shiftedForBoardRow = startingPieceHeightOnBoard - (3 - shiftedDotRow); 
                int shiftedForBoardCol = i + shiftedDotCol;

                // add to the tuples on the actual board
                compatibleBoard.Add(Tuple.Create(shiftedForBoardRow, shiftedForBoardCol));

                Console.WriteLine(" \n\n\n\n\n\n\n\n\n I AM HEREHEHEKLFJSKLJFLKSDJFKLJSDLFKJSDKLFJkl" + shiftedForBoardRow + " " + shiftedForBoardCol);
                // put it on the board as well
                modifiedBoard[shiftedForBoardRow, shiftedForBoardCol] = 1;

                // see which dots are nearby
                // up
                if(shiftedForBoardRow - 1 >= 0 && board.board[shiftedForBoardRow - 1,shiftedForBoardCol] == 1) {
                    dotsNearby.Add(Tuple.Create(shiftedForBoardRow - 1, shiftedForBoardCol));
                }

                // down
                if(shiftedForBoardRow + 1 < board.height && board.board[shiftedForBoardRow + 1,shiftedForBoardCol] == 1) {
                    dotsNearby.Add(Tuple.Create(shiftedForBoardRow + 1, shiftedForBoardCol));
                }

                // left
                if(shiftedForBoardCol - 1 >= 0 && board.board[shiftedForBoardRow,shiftedForBoardCol - 1] == 1) {
                    dotsNearby.Add(Tuple.Create(shiftedForBoardRow, shiftedForBoardCol - 1));
                }

                // right
                if(shiftedForBoardCol + 1 < board.width && board.board[shiftedForBoardRow,shiftedForBoardCol + 1] == 1) {
                    dotsNearby.Add(Tuple.Create(shiftedForBoardRow, shiftedForBoardCol + 1));
                }

                // check for touching the floor
                if(shiftedForBoardRow + 1 == board.height) {
                    dotsNearby.Add(Tuple.Create(shiftedForBoardRow + 1, shiftedForBoardCol));
                    dotsFillingFloor += 1;
                }
            }

            Console.WriteLine("MODIFIED BOARD");
            botInfoPrinter.PrintMultiDimArr(modifiedBoard);

            Console.WriteLine("DOTS NEARBY");
            botInfoPrinter.PrintSetTuples(dotsNearby);

            Console.WriteLine("NUM FILLING FLOOR " + board.numFilledFloor);
            Console.WriteLine("DOTS FILLING FLOOR " + dotsFillingFloor);

            // piece starting at this column and row is actually compatible then add its information
            if(compatibleBoard != null) {
                if(dotsFillingFloor + board.numFilledFloor == board.height) {
                    // creates tetris
                    Console.WriteLine("CREATING TETRIS");
                    compatiblePieces.Add(Tuple.Create(rotation, compatibleBoard, dotsNearby.Count, 1, 0));
                } else {
                    // no tetris
                    compatiblePieces.Add(Tuple.Create(rotation, compatibleBoard, dotsNearby.Count, 0, 0));
                }
            }
        }

        return compatiblePieces;
    }



    /** 
        @@param
            int[][] board - current enviornment
            List<Block> blocks - contains the list of all the blocks to try to fit in this location
     */
    public override List<Tuple<int, int>> GetMove(Board board, List<Block> blocks) {
        // get the max height of each column of the baord 
        board.FindMaxHeights();

        // compatible pieces
        // has all the positions compatible for this piece with the rotation, location on board, area covered, and if it can clear a line
        List<Tuple<int, List<Tuple<int, int>>, int, int, int>> compatiblePieces = new List<Tuple<int, List<Tuple<int, int>>, int, int, int>>();

        // test out each of the pieces
        foreach(Block block in blocks) {

            // get the fit of the board with the area and whether a piece can clear a line
            compatiblePieces.AddRange(getFit(board, block, 1));
            block.data = block.RotateMatrix();
            compatiblePieces.AddRange(getFit(board, block, 2));
            block.data = block.RotateMatrix();
            compatiblePieces.AddRange(getFit(board, block, 3));
            block.data = block.RotateMatrix();
            compatiblePieces.AddRange(getFit(board, block, 1));
        }

        // compatible pieces has all the pieces that are compatible with the board and has the information about the rotation, location on board, area covered, and if that piece can clear a line
        compatiblePieces.Sort((x, y) => {
            // sort by whether a line has been cleared and puts those first if there is
            int result = y.Item4.CompareTo(x.Item4);
            // sort by the area covered
            return result == 0 ? y.Item3.CompareTo(x.Item3) : result;
        });

        // best piece to place with information to place on board
        List<Tuple<int, int>> bestPiecePlacement = compatiblePieces[0].Item2;

        Console.WriteLine("BEST PIECE FOR BOARD");
        botInfoPrinter.PrintPositions(bestPiecePlacement);
        Console.WriteLine("BOARD");
        botInfoPrinter.PrintMultiDimArr(board.board);
        Console.WriteLine("BOARD WITH PIECE");
        botInfoPrinter.PrintBoardWithPiece(board.board, bestPiecePlacement);

        return bestPiecePlacement;
    }
}



public class DoubleBot : Bot {
    public override List<Tuple<int, int>> GetMove(Board board, List<Block> blocks) {
        return null;
    }
}



public class TripleBot : Bot {
    public override List<Tuple<int, int>> GetMove(Board board, List<Block> blocks) {
        return null;
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


// methodology for sorting appraoch - adopted different way to do this
/*  find the best piece of all
        // see if any have a line done
        bool anyLineDone = compatiblePieces.Any(a => a.Item4 == 1);

        if(anyLineDone) {
            // a line was completed in one of the pieces, so need to lead with the pieces that lead to tye line being done
            Console.WriteLine("A line was done");
            // order by the completion and then the area covered
            compatiblePieces.Sort((x, y) => {
                int result = y.Item4.CompareTo(x.Item4);
                return result == 0 ? y.Item3.CompareTo(x.Item3) : result;
            });
        } else {
            compatiblePieces.Sort((x, y) => y.Item3.CompareTo(x.Item3));
            Console.WriteLine("No line can be done, find the best piece");
        }
*/