using System;
using System.Collections.Generic;

public class Prints {

     /** 
        @@param
            int[][] data - the data to be printed
     */
    public void PrintJaggedArr(int[][] data) {
        Console.WriteLine();
        for (int n = 0; n < data.Length; n++) { 
  
            // Print the row number 
            Console.Write("Row({0}): ", n); 
  
            for (int k = 0; k < data[n].Length; k++) { 
  
                // Print the elements in the row 
                Console.Write("{0} ", data[n][k]); 
            } 
            Console.WriteLine(); 
        } 
        Console.WriteLine();
    }


    /** 
        @@param
            int[,] data - the data to be printed
     */
    public void PrintMultiDimArr(int[,] data) {
        Console.WriteLine();
        for (int n = 0; n < data.GetLength(0); n++) { 
  
            // Print the row number 
            Console.Write("Row({0}): ", n); 
  
            for (int k = 0; k < data.GetLength(1); k++) { 
  
                // Print the elements in the row 
                Console.Write("{0} ", data[n,k]); 
            } 
            Console.WriteLine(); 
        } 
        Console.WriteLine();
    }


     /** 
        @@param
            List<Tuple<int, int>> positions - the data for the positions
     */
    public void PrintPositions(List<Tuple<int, int>> positions) {
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
    public void PrintSet(HashSet<int> set) {
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
    public void PrintSetTuples(HashSet<Tuple<int, int>> set) {
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
    public void PrintBoardWithPiece(int[,] board, List<Tuple<int, int>> dots) {
       int[,] newBoard = new int[board.GetLength(0),board.GetLength(1)];

       for(int i = 0; i < board.GetLength(0); i++){
           for(int j = 0; j < board.GetLength(1); j++){
               newBoard[i,j] = board[i,j];
           }
       }

       foreach(Tuple<int, int> dot in dots) {
           newBoard[dot.Item1, dot.Item2] = 2;
       }

       PrintMultiDimArr(newBoard);
    }
}