using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private GameState game;
        private List<Block> blocks;
        Prints botInfoPrinter;

        /* 
         Setup the board and pieces for testing
         */
        [SetUp]
        public void Setup()
        {
            botInfoPrinter = new Prints();

            game = new GameState(6, 6);
            game.bot = new SingleBot();

            game.board.board =  new int[,]{
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {1, 0, 0, 1, 0, 1},
                {1, 1, 0, 1, 1, 1},
                {1, 0, 1, 1, 1, 1}
            };

            int[][] b1 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
            };

            int[][] b2 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

             int[][] b3 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 1, 0}, 
            };

            int[][] b4 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 1, 0}, 
            };

            int[][] b5 = new int[][] {
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 1, 0}, 
            };

            int[][] b6 = new int[][] {
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b7 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 1, 1, 1}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b8 = new int[][] {
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 1, 1, 1}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b9 = new int[][] {
                new int[] {0, 1, 1, 1}, 
                new int[] {0, 1, 1, 1}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            Block block1 = new Block(b1, 1);
            Block block2 = new Block(b2, 1);
            Block block3 = new Block(b3, 1);
            Block block4 = new Block(b4, 1);
            Block block5 = new Block(b5, 1);
            Block block6 = new Block(b6, 1);
            Block block7 = new Block(b7, 1);
            Block block8 = new Block(b8, 1);
            Block block9 = new Block(b9, 1);

            blocks = new List<Block>();

            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            blocks.Add(block5);
            blocks.Add(block6);
            blocks.Add(block7);
            blocks.Add(block8);
            blocks.Add(block9);
        }

    /*
        Assert that a move can be taken without erroring
     */
    [Test]
        public void MakeMoves() {
            try {
                foreach(Block block in blocks) {
                    List<Block> newBlocks = new List<Block>();
                    newBlocks.Add(block);
                    List<Tuple<int, int>> piecePlaced = game.bot.GetMove(game.board, newBlocks); 
                    System.Diagnostics.Debug.WriteLine("BOARD WITH PIECE\n\n\n\n");
                    botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced);
                }
            } catch (Exception e) {
                Assert.Fail("Expected no exception " + e.Message);
            }        
        }
    }
}