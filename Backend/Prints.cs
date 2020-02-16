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
}