using System;
using System.Collections.Generic;
using System.Linq;
public class Block
{
    int[][] data;
    int color;
    public int id;
    public Tuple<int, int> pos;

    public Block(int[][] data, int color)
    {
        this.data = data;
        this.color = color;
    }
    
     /** 
        @@param
            int[][] matrix - contains where each one of the dots on the block are filled
        @@return
            int[][] matrix - contains where each one of the dots on the block are filled after rotation
     */
     public int[][] RotateMatrix(int[][] matrix) {
        int length = matrix[0].Length;
        int[][] retVal = new int[length][];
        for(int x = 0; x < length; x++)
        {
            retVal[x] = matrix.Select(p => p[x]).ToArray();
        }
        return retVal;
    }

}