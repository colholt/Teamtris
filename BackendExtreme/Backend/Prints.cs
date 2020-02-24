using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;

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
}