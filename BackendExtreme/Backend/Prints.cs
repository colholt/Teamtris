using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;

/**
 * #class Prints |
 * @author ??? | 
 * @language csharp | 
 * @desc TODO |
 */
public class Prints {

     /** 
        @@param
            int[][] data - the data to be printed
     */
    public void PrintJaggedArr(int[][] data, bool normalMode = true) {
         if(normalMode) {
            Console.WriteLine();
        } else {
            TestContext.Progress.WriteLine();
        }

        StringBuilder sb = new StringBuilder();
        for (int n = 0; n < data.Length; n++) { 
  
            // Print the row number 
            if (normalMode) {
                Console.Write("Row({0}): ", n);
            } else {
                sb.Append("Row(" + n + "): ");
            }
  
            for (int k = 0; k < data[n].Length; k++) { 
                if (normalMode) {
                    // Print the elements in the row 
                    Console.Write("{0} ", data[n][k]); 
                } else {
                    sb.Append(data[n][k] + " ");
                } 
            } 
            if (normalMode) {
                Console.WriteLine(); 
            } else {
                sb.AppendLine();
            }
        } 
        if (normalMode) {
            Console.WriteLine(); 
        } else {
            TestContext.Progress.WriteLine(sb.ToString());
        }
    }


    /** 
        @@param
            int[,] data - the data to be printed
     */
    public void PrintMultiDimArr(int[,] data, bool normalMode = true) {
        if(normalMode) {
            Console.WriteLine();
        } else {
            TestContext.Progress.WriteLine();
        }

        StringBuilder sb = new StringBuilder();
        
        for (int n = 0; n < data.GetLength(0); n++) { 
  
            // Print the row number 
            if (normalMode) {
                Console.Write("Row({0}): ", n);
            } else {
                sb.Append("Row(" + n + "): ");
            }
  
            for (int k = 0; k < data.GetLength(1); k++) { 
                if (normalMode) {
                    // Print the elements in the row 
                    Console.Write("{0} ", data[n,k]); 
                } else {
                    sb.Append(data[n,k] + " ");
                }
            } 
            if (normalMode) {
                Console.WriteLine(); 
            } else {
                sb.AppendLine();
            }
        } 
        if (normalMode) {
            Console.WriteLine(); 
        } else {
            TestContext.Progress.WriteLine(sb.ToString());
        }
    }


     /** 
        @@param
            List<Tuple<int, int>> positions - the data for the positions
     */
    public void PrintPositions(List<Tuple<int, int>> positions, bool normalMode = true) {
        Console.WriteLine();
        foreach (var tuple in positions) {
            Console.WriteLine("({0}, {1})", tuple.Item1, tuple.Item2);
        }
        Console.WriteLine();
    }


    /**
        @@param 
            HashSet<int> data - data to be printed
     */
    public void PrintSet(HashSet<int> set, bool normalMode = true) {
        Console.WriteLine();
        Console.Write("{");
        foreach (int i in set)
        {
            Console.Write(" {0}", i);
        }
        Console.WriteLine(" }");
        Console.WriteLine();
    }


    /**
        @@param 
            HashSet<int, int> data - data to be printed
     */
    public void PrintSetTuples(HashSet<Tuple<int, int>> set, bool normalMode = true) {
        Console.WriteLine();
        Console.Write("{");
        foreach (var tuple in set)
        {
            Console.Write(" ({0},{1})", tuple.Item1, tuple.Item2);
        }
        Console.WriteLine(" }");
        Console.WriteLine();
    }


    /**
        @@param 
            int[,] board - current state of board
            List<Tuple<int, int>> dots - the dots that fill up the piece
     */
    public void PrintBoardWithPiece(int[,] board, List<Tuple<int, int>> dots, bool normalMode = true) {
       int[,] newBoard = new int[board.GetLength(0),board.GetLength(1)];

       for(int i = 0; i < board.GetLength(0); i++){
           for(int j = 0; j < board.GetLength(1); j++){
               newBoard[i,j] = board[i,j];
           }
       }

       foreach(Tuple<int, int> dot in dots) {
           newBoard[dot.Item1, dot.Item2] = 2;
       }

       PrintMultiDimArr(newBoard, normalMode);
    }

    

    /**
        @@param 
            int[] arr - array to be printed
     */
    public void PrintArr(int[] arr, bool normalMode = true) {
        for(int i = 0; i < arr.Length; i++) {
            Console.Write("{0} ", arr[i]);
        } 

        Console.WriteLine();
    }


    /**
        @@param 
            ScoresInfo scoresInfo - information to be printed
     */
    public void PrintScoreInfo(ScoresInfo scoresInfo, bool normalMode = true) {
        if(scoresInfo == null) {
            Console.WriteLine("No scores \n");
            return;
        }
        
        Console.WriteLine("Team Name " + scoresInfo.teamName);
        Console.WriteLine("Players Info ");
        foreach(String p in scoresInfo.playerNames) {
            if(p != null) {
                Console.WriteLine("Player: " + p);
            }
        }
        Console.WriteLine("Team Score " + scoresInfo.teamScore);
        Console.WriteLine("Time Played " + scoresInfo.timePlayed);
        if(scoresInfo.rank != -1) {
            Console.WriteLine("Rank " + scoresInfo.rank);
        }
        Console.WriteLine();
    }

    /**
        @@param 
            List<ScoresInfo> scoresInfo - information to be printed
     */
    public void PrintScoreList(List<ScoresInfo> scoresInfo, bool normalMode = true) {
        if(scoresInfo == null) {
            Console.WriteLine("No scores \n");
            return;
        }

        foreach(ScoresInfo score in scoresInfo){
            PrintScoreInfo(score);
        }
        Console.WriteLine();
    }


    /**
        @@param 
            List<Tuple<int [][], int [][]>> all orientations - information to be printed
     */
    public void PrintAllOrientationsAsList(List<Tuple<Block, Block, int>> allOrientations, bool normalMode = true) {
        int rotationNum = 0;

        foreach(Tuple<Block, Block, int> blockShape in allOrientations) {
            rotationNum++;
            Console.WriteLine("BLOCK ROTATION " + rotationNum);
            Console.WriteLine("BOT 1");
            PrintJaggedArr(blockShape.Item1.data);
            Console.WriteLine("BOT 2");
            PrintJaggedArr(blockShape.Item2.data);
        }

        Console.WriteLine();
    }


    /**
        @@param 
            Set<Tuple<int [][], int [][]>> all orientations - information to be printed
     */
    public void PrintAllOrientationsAsSet(HashSet<Tuple<Block, Block, int>> allOrientations, bool normalMode = true) {
        int rotationNum = 0;

        foreach(Tuple<Block, Block, int> blockShape in allOrientations) {
            rotationNum++;
            Console.WriteLine("BLOCK ROTATION " + rotationNum);
            Console.WriteLine("BOT 1");
            PrintJaggedArr(blockShape.Item1.data);
            Console.WriteLine("BOT 2");
            PrintJaggedArr(blockShape.Item2.data);
        }

        Console.WriteLine();
    }


    /**
        @@param 
            List<CompatiblePiece> compatiblePieces
     */
    public void PrintCompatiblePieces(int[,] board, List<CompatiblePiece> compatiblePieces, bool normalMode = true) {
        foreach(CompatiblePiece compatiblePiece in compatiblePieces) {
           Console.WriteLine("LOCATION ON BOARD");
           PrintBoardWithPiece(board, compatiblePiece.locationOnBoard);
           Console.WriteLine("AREA COVERED " + compatiblePiece.area);
           Console.WriteLine("ROWS CLEARED " + compatiblePiece.numLinesCleared);
        }
        
        Console.WriteLine();
    }


    /**
        @@param 
            List<CompatiblePiece> compatiblePieces
     */
    public void PrintAllCompatiblePieces(int[,] board, List<Tuple<CompatiblePiece, CompatiblePiece>> allCompatiblePieces, bool normalMode = true) {
        foreach(Tuple<CompatiblePiece, CompatiblePiece> compatiblePieces in allCompatiblePieces) {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("BOT 1");
            PrintBoardWithPiece(board, compatiblePieces.Item1.locationOnBoard);
            Console.WriteLine("BOT 2");
            PrintBoardWithPiece(board, compatiblePieces.Item2.locationOnBoard);
            Console.WriteLine("ROWS CLEARED " + compatiblePieces.Item2.numLinesCleared);
            Console.WriteLine("-----------------------------------");
        }
        
        Console.WriteLine();
    }
}


