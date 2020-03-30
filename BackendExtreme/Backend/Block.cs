using System;
using System.Collections.Generic;
using System.Linq;

/**
 * #class Block |
 * @author ??? | 
 * @language csharp | 
 * @desc TODO |
 */
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
            Tuple<int, int>data - the data at the bottommost positions across all columns
     */
     public int[] GetBottomBlocks(int[,] shiftedOverPiece) {
        int[] bottomIndicies = new int[4];

        int numRows = shiftedOverPiece.GetLength(0);
        int numCols = shiftedOverPiece.GetLength(1);

        // Console.WriteLine("HERE " + numRows + " " + numCols);

        for(int i = 0; i < numCols; i++) {
            bool found1 = false;
            for(int j = numRows - 1; j >= 0; j--) {
                if(shiftedOverPiece[j,i] == 1) {
                    bottomIndicies[i] = j;
                    found1 = true;
                    break;
                }
            }
            if(!found1) {
                bottomIndicies[i] = 4;
            }
        }

        return bottomIndicies;
     }

      /** 
        @@param
            Tuple<int, int>data - the data at the bottommost positions across all columns
     */
     public int[] GetBottomBlocksAsJaggedArray(int[][] shiftedOverPiece) {
        int[] bottomIndicies = new int[4];

        int numRows = 4;
        int numCols = 4;

        // Console.WriteLine("HERE " + numRows + " " + numCols);

        for(int i = 0; i < numCols; i++) {
            bool found1 = false;
            for(int j = numRows - 1; j >= 0; j--) {
                if(shiftedOverPiece[j][i] == 1) {
                    bottomIndicies[i] = j;
                    found1 = true;
                    break;
                }
            }
            if(!found1) {
                bottomIndicies[i] = 4;
            }
        }
        return bottomIndicies;
     }


     /** 
        @@return 
            Tuple<int, int> data - the data at the bottommost left position
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


    /** 
        @return shifted data for the block to the bottommost corner
     */
    public int[][] ShiftDataBottom() {
        // positions of where the piece exists in the data in a tuple with both the ints for row and column
        List<Tuple<int, int>> dotPositions = new List<Tuple<int, int>>();
        
        // go through all the rows and get all the places where there is a true
        for(int row = 0; row < this.data.Length; row++) {
            dotPositions.AddRange(this.data[row].Select((b,i) => b == 1 ? i : -1).Where(i => i != -1).Select(index => new Tuple<int, int>(row, index)));
        }

        // get the bottom most left piece
        Tuple<int, int> bottomLeft = this.FindBottomLeft();
        int bottomLeftRow = bottomLeft.Item1;
        int bottomLeftCol = bottomLeft.Item2;

        // shifted over piece information
        int[][] shiftedOverPiece = new int[][] {
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

        // shift over the dot positions
        foreach(Tuple<int, int>  positionOfDot in dotPositions) {
            // dot to be tested
            int dotRowOnPiece = positionOfDot.Item1;
            int dotColOnPiece = positionOfDot.Item2;

            // shift over the dot to get rid of extra space
            int modRowOnPiece = 3 - (bottomLeftRow - dotRowOnPiece);
            int modDotCol = dotColOnPiece - bottomLeftCol;

            // fill in new piece with this dot and this is the piece we have to fit on the board at this specific column
            shiftedOverPiece[modRowOnPiece][modDotCol] = 1;
        }

        this.data = shiftedOverPiece;

        return shiftedOverPiece;
    }


    /** 
        @@return 
            bool valid - return whether the shape is valid
     */
    public bool CheckValidity() {
        bool isValid = true;
        int numOnes = 0;

        // check each piece
        for(int i = 0; i < data[0].Length; i++) {
            for(int j = 0; j < data.Length; j++) {
                if(data[i][j] == 1) {
                    numOnes += 1;

                    bool nextToPiece = false;

                    // check left
                    if(i -1 >= 0 && data[i-1][j] == 1) {
                        nextToPiece = true;
                    }

                    // check right
                    if(i + 1 <= data[0].Length - 1 && data[i+1][j] == 1) {
                        nextToPiece = true;
                    }

                    // check up
                    if(j-1 >= 0 && data[i][j-1] == 1) {
                        nextToPiece = true;
                    }

                    // check down
                     if(j + 1 <= data.Length - 1 && data[i][j + 1] == 1) {
                        nextToPiece = true;
                    }

                    if(nextToPiece == false) {
                        isValid = false;
                        break;
                    }
                }
            }
        }
        if(numOnes == 1) {
            return true;
        }
        return isValid;
    }



    public override bool Equals(object b1) {
        // Console.WriteLine("EQUALS");
        for(int i = 0; i < this.data.Length; i++) {
            for(int j = 0; j < this.data[0].Length; j++) {
                Block b = (Block)b1;
                if(this.data[i][j] != b.data[i][j]) {
                    return false;
                }
            }
        }
        return true;
    }
}

class BlockEqualityComparer:IEqualityComparer<Block> {

    public bool Equals(Block b1, Block b2) {
        // Console.WriteLine("IN THE EUQLS");
        for(int i = 0; i < b1.data.Length; i++) {
            for(int j = 0; j < b1.data[0].Length; j++) {
                if(b1.data[i][j] != b2.data[i][j]) {
                    return false;
                }
            }
        }
        //  Console.WriteLine("RETURNING VALUE");
        return true;
    }

    public int GetHashCode(Block b) {
        return b.GetHashCode();
    }
}