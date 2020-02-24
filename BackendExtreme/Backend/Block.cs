using System;
using System.Collections.Generic;
using System.Linq;
public class Block
{
    public int[][] data;
    public int color;
    public int id;
    public Tuple<int, int> pos;
    int size = 4;

    public Block(int[][] data, int color)
    {
        this.data = data;
        this.color = color;
    }
    
     /** 
        @@return
            int[][] matrix - contains where each one of the dots on the block are filled after rotation
     */
     public int[][] RotateMatrix() {
        // int length = data[0].Length;
        // int[][] retVal = new int[length][];
        // for(int x = 0; x < length; x++)
        // {
        //     retVal[x] = data.Select(p => p[x]).ToArray();
        // }
        // return retVal;
        for (int i = 0; i < size/2; i++) { 
            for (int j = i; j < size-i-1; j++) { 
                int temp = data[i][j]; 
                data[i][j] = data[size-1-j][i]; 
                data[size-1-j][i] = data[size-1-i][size-1-j]; 
                data[size-1-i][size-1-j] = data[j][size-1-i]; 
                data[j][size-1-i] = temp; 
            } 
        } 
        return data;
    }


     /** 
        @@param
            int[][] data - the data at the bottommost left position
     */
    public Tuple<int, int> FindBottomLeft() {
        Tuple<int, int> bottomLeft = new Tuple<int, int>(data.Length, data[0].Length);

        // update if less than the left most
        int leftMost = 26;

        // update if more than the top most
        int bottomMost = 0;

        // go through to find the bottom left most dot so that the counting can start there - finds the one that is the most bottom and left
        for(int i = 0; i < data[0].Length; i++) {
            for(int j = data.Length - 1; j >= 0; j--) {
                if(data[j][i] == 1) {
                    // j is the row and i is the column

                    // check for row
                    if (j >= bottomMost) {
                        bottomMost = j;
                    }

                    // check for column
                    if (i <= leftMost) {
                        leftMost = i;
                    }
                    // bottomLeft = new Tuple<int, int>(j, i);
                    // Console.WriteLine("Tuple " + bottomLeft.Item1 + " " + bottomLeft.Item2);
                    // return bottomLeft;
                }
            }
        }

        // create a tuple with both sides
        bottomLeft = new Tuple<int, int>(bottomMost, leftMost);
        return bottomLeft;
    }

}