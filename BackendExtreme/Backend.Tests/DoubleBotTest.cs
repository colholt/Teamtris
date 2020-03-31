using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class DoubleBotTests
    {
        private GameState game;
        private List<Block> bot1Blocks;
        private List<Block> bot2Blocks;
        private List<Block> blocks = new List<Block>();
        private List<List<Block>> allBlocks;

        private DoubleBot bot;
        Prints botInfoPrinter;

        /* 
         Setup the board and pieces for testing
         */
        [SetUp]
        public void Setup()
        {
            botInfoPrinter = new Prints();

            game = new GameState(8, 8);
            game.bot = new DoubleBot();

            bot = new DoubleBot();


            game.board.board =  new int[,]{
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 1},
                {1, 0, 0, 1, 0, 1, 1, 1},
                {1, 1, 0, 1, 1, 1, 1, 1},
                {1, 0, 1, 1, 1, 1, 1, 1}
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
                new int[] {1, 1, 1, 0}, 
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

            int[][] b10 = new int[][] {
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b11 = new int[][] {
                new int[] {0, 0, 1, 1}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b12 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 0, 0}, 
            };

            int[][] b13 = new int[][] {
                new int[] {0, 0, 1, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };


            // BOT 1 BLOCKS
            Block block1A = new Block(b1, 1);
            Block block2A = new Block(b2, 1);
            Block block3A = new Block(b3, 1);
            Block block4A = new Block(b4, 1);
            Block block5A = new Block(b5, 1);
            Block block6A = new Block(b6, 1);
            Block block7A = new Block(b7, 1);
            Block block8A = new Block(b8, 1);
            Block block9A = new Block(b9, 1);
            Block block10A = new Block(b10, 1);
            Block block11A = new Block(b11, 1);
            Block block12A = new Block(b12, 1);
            Block block13A = new Block(b13, 1);
            bot1Blocks = new List<Block>();
            bot1Blocks.Add(block1A);
            bot1Blocks.Add(block2A);
            bot1Blocks.Add(block3A);
            bot1Blocks.Add(block4A);
            bot1Blocks.Add(block5A);
            bot1Blocks.Add(block6A);
            bot1Blocks.Add(block7A);
            bot1Blocks.Add(block8A);
            bot1Blocks.Add(block9A);
            bot1Blocks.Add(block10A);
            bot1Blocks.Add(block11A);
            bot1Blocks.Add(block12A);
            bot1Blocks.Add(block13A);

            // BOT 2 BLOCKS
            Block block1B = new Block(b1, 1);
            Block block2B = new Block(b2, 1);
            Block block3B = new Block(b3, 1);
            Block block4B = new Block(b4, 1);
            Block block5B = new Block(b5, 1);
            Block block6B = new Block(b6, 1);
            Block block7B = new Block(b7, 1);
            Block block8B = new Block(b8, 1);
            Block block9B = new Block(b9, 1);
            Block block10B = new Block(b10, 1);
            Block block11B = new Block(b11, 1);
            Block block12B = new Block(b12, 1);
            Block block13B = new Block(b13, 1);
            bot2Blocks = new List<Block>();
            bot1Blocks.Add(block1B);
            bot1Blocks.Add(block2B);
            bot1Blocks.Add(block3B);
            bot1Blocks.Add(block4B);
            bot1Blocks.Add(block5B);
            bot1Blocks.Add(block6B);
            bot1Blocks.Add(block7B);
            bot1Blocks.Add(block8B);
            bot1Blocks.Add(block9B);
            bot1Blocks.Add(block10B);
            bot1Blocks.Add(block11B);
            bot1Blocks.Add(block12B);
            bot1Blocks.Add(block13B);

            allBlocks = new List<List<Block>>();
            allBlocks.Add(bot1Blocks); 
            allBlocks.Add(bot2Blocks);
        }


        /*
            Helper for placing the pieces on the board
            @@param board - board that piece must be placed on
                    dots - the dots of the piece 
                    clearLines - whether lines must be removed on the board when they are cleared
         */
        public Tuple<int[,], int[,]> PlacePieceOnBoard(int [,] board, List<Tuple<int, int>> dots, int numToPlace, bool clearLines = true) {
            int[,] newBoard = new int[board.GetLength(0),board.GetLength(1)];

            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    newBoard[i,j] = board[i,j];
                }
            }

            foreach(Tuple<int, int> dot in dots) {
                board[dot.Item1, dot.Item2] = 1;
                newBoard[dot.Item1, dot.Item2] = numToPlace;
            }

            if(!clearLines) {
                Tuple<int[,], int[,]> allBoards1 = new Tuple<int[,], int[,]>(board, newBoard);
                return allBoards1;
            }

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // remove the line completed
            for(int i = rows - 1; i >= 0; i--) {
                // check to see if all the columns have a value of 1
                bool hasAllFilled = true;
                for(int j = 0; j < cols; j++) {
                    if(board[i,j] != 1 && board[i,j] != 2) {
                        hasAllFilled = false;
                        break;
                    }
                }

                // everything is filled in the row, so need to shift all the rows down by 1
                if(hasAllFilled) {
                    for(int k = i - 1; k > 0; k--) {
                        for(int y = 0; y < cols; y++) {
                            board[k + 1, y] = board[k, y];
                            newBoard[k + 1, y] = newBoard[k, y];
                        }
                    }

                    // if first row, just need to replace no need to fill again
                    for(int j = 0; j < cols; j++) {
                        board[0,j] = 0;
                        newBoard[0,j] = 0;
                    }

                    i = i + 1;
                }
            }

            
            Tuple<int[,], int[,]> allBoards = new Tuple<int[,], int[,]>(board, newBoard);
            return allBoards;
        }


        /*
            Assert that only block orientations are in the list
        */
        [Test]
        public void BlockOrientations1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BLOCK ORIENTATIONS 1--------------------------------------");
            try {
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
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block block1 = new Block(b1, 1);
                Block block2 = new Block(b2, 1);
                botInfoPrinter.PrintJaggedArr(block1.data, false);
                botInfoPrinter.PrintJaggedArr(block2.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int expectedOrientations = 8;

                int orientations = bot.GetAllOrientations(block1, block2).Count;

                Assert.That(orientations, Is.EqualTo(expectedOrientations));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that only block orientations are in the list
        */
        [Test]
        public void BlockOrientations2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BLOCK ORIENTATIONS 2--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block block1 = new Block(b1, 1);
                Block block2 = new Block(b2, 1);
                botInfoPrinter.PrintJaggedArr(block1.data, false);
                botInfoPrinter.PrintJaggedArr(block2.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int expectedOrientations = 4;

                int orientations = bot.GetAllOrientations(block1, block2).Count;

                Assert.That(orientations, Is.EqualTo(expectedOrientations));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that only block orientations are in the list
        */
        [Test]
        public void BlockOrientations3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BLOCK ORIENTATIONS 3--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block block1 = new Block(b1, 1);
                Block block2 = new Block(b2, 1);
                botInfoPrinter.PrintJaggedArr(block1.data, false);
                botInfoPrinter.PrintJaggedArr(block2.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int expectedOrientations = 16;

                int orientations = bot.GetAllOrientations(block1, block2).Count;

                Assert.That(orientations, Is.EqualTo(expectedOrientations));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that only block orientations are in the list
        */
        [Test]
        public void BlockOrientations4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BLOCK ORIENTATIONS 4--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block block1 = new Block(b1, 1);
                Block block2 = new Block(b2, 1);
                botInfoPrinter.PrintJaggedArr(block1.data, false);
                botInfoPrinter.PrintJaggedArr(block2.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int expectedOrientations = 2;

                int orientations = bot.GetAllOrientations(block1, block2).Count;

                Assert.That(orientations, Is.EqualTo(expectedOrientations));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that only block orientations are in the list
        */
        [Test]
        public void BlockOrientations5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BLOCK ORIENTATIONS 5--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block block1 = new Block(b1, 1);
                Block block2 = new Block(b2, 1);
                botInfoPrinter.PrintJaggedArr(block1.data, false);
                botInfoPrinter.PrintJaggedArr(block2.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int expectedOrientations = 16;

                int orientations = bot.GetAllOrientations(block1, block2).Count;

                Assert.That(orientations, Is.EqualTo(expectedOrientations));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that the shape provided to the bot is valid with invalid shape
        */
        [Test]
        public void TestShapeFormationInvalid1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED INVALID 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                List<Block> newBlocks2 = new List<Block>();
                Block b = new Block(b1, 1);
                Block a = new Block(b2, 1);
                newBlocks.Add(b);
                newBlocks2.Add(a);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                allB.Add(newBlocks2);
                List<Tuple<int, int>> piecePlaced = bot.GetSingleMove(game.board, allB); 
                TestContext.Progress.WriteLine("--------------------------------------");
                Assert.Fail("Expected exception not thrown");
            } catch (Exception e) {
                string expectedMessage = "Shape formation is incorrect";
                Assert.AreEqual(expectedMessage, e.Message);
            }        
        }


        /*
            Assert that the shape provided to the bot is valid with invalid shape
        */
        [Test]
        public void TestShapeFormationInvalid2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED INVALID 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
               int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                List<Block> newBlocks2 = new List<Block>();
                Block b = new Block(b1, 1);
                Block a = new Block(b2, 1);
                newBlocks.Add(b);
                newBlocks2.Add(a);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                allB.Add(newBlocks2);
                List<Tuple<int, int>> piecePlaced = bot.GetSingleMove(game.board, allB); 
                TestContext.Progress.WriteLine("--------------------------------------");
                Assert.Fail("Expected exception not thrown");
            } catch (Exception e) {
                string expectedMessage = "Shape formation is incorrect";
                Assert.AreEqual(expectedMessage, e.Message);
            }        
        }

         /*
            Assert that the shape provided to the bot is valid with invalid shape
        */
        [Test]
        public void TestShapeFormationInvalid3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED INVALID 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
               int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                List<Block> newBlocks2 = new List<Block>();
                Block b = new Block(b1, 1);
                Block a = new Block(b2, 1);
                newBlocks.Add(b);
                newBlocks2.Add(a);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                allB.Add(newBlocks2);
                List<Tuple<int, int>> piecePlaced = bot.GetSingleMove(game.board, allB); 
                TestContext.Progress.WriteLine("--------------------------------------");
                Assert.Fail("Expected exception not thrown");
            } catch (Exception e) {
                string expectedMessage = "Shape formation is incorrect";
                Assert.AreEqual(expectedMessage, e.Message);
            }        
        }

         /*
            Assert that the shape provided to the bot is valid with invalid shape
        */
        [Test]
        public void TestShapeFormationInvalid4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED INVALID 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                List<Block> newBlocks2 = new List<Block>();
                Block b = new Block(b1, 1);
                Block a = new Block(b2, 1);
                newBlocks.Add(b);
                newBlocks2.Add(a);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                allB.Add(newBlocks2);
                List<Tuple<int, int>> piecePlaced = bot.GetSingleMove(game.board,allB); 
                TestContext.Progress.WriteLine("--------------------------------------");
                Assert.Fail("Expected exception not thrown");
            } catch (Exception e) {
                string expectedMessage = "Shape formation is incorrect";
                Assert.AreEqual(expectedMessage, e.Message);
            }        
        }

        /*
            Assert that the shape provided to the bot is valid with invalid shape
        */
        [Test]
        public void TestShapeFormationInvalid5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED INVALID 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 1}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 1}, 
                };
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                List<Block> newBlocks2 = new List<Block>();
                Block b = new Block(b1, 1);
                Block a = new Block(b2, 1);
                newBlocks.Add(b);
                newBlocks2.Add(a);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                allB.Add(newBlocks2);
                List<Tuple<int, int>> piecePlaced = bot.GetSingleMove(game.board, allB); 
                TestContext.Progress.WriteLine("--------------------------------------");
                Assert.Fail("Expected exception not thrown");
            } catch (Exception e) {
                string expectedMessage = "Shape formation is incorrect";
                Assert.AreEqual(expectedMessage, e.Message);
            }        
        }


        /*
            Assert that the formation with the best shape is chosen
        */
        [Test]
        public void ShapePlacement1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------DOUBLE BOT SHAPE PLACEMENT 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                List<Block> newBlocksBot1 = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot1Block = new Block(b1, 1);
                newBlocksBot1.Add(bot1Block);
                TestContext.Progress.Write("Bot 1 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot1[0].data, false);

                List<Block> newBlocksBot2 = new List<Block>();
                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot2Block = new Block(b2, 1);
                newBlocksBot2.Add(bot2Block);
                TestContext.Progress.Write("Bot 2 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot2[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocksBot1);
                allB.Add(newBlocksBot2);
                List<List<Tuple<int, int>>> piecePlaced = bot.GetMove(game.board, allB); 

                List<Tuple<int, int>> bot1Pieces = piecePlaced[0];
                List<Tuple<int, int>> bot2Pieces = piecePlaced[1];

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, bot1Pieces, 2, false);
                game.board.board = allBoards.Item2;
                allBoards = PlacePieceOnBoard(game.board.board, bot2Pieces, 3, false);
                TestContext.Progress.Write("Board AFTER bots placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 2, 0, 1, 1},
                    {1, 3, 3, 1, 2, 1, 1, 1},
                    {1, 1, 3, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }


        /*
            Assert that the formation with the best shape is chosen
        */
        [Test]
        public void ShapePlacement2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------DOUBLE BOT SHAPE PLACEMENT 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                List<Block> newBlocksBot1 = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot1Block = new Block(b1, 1);
                newBlocksBot1.Add(bot1Block);
                TestContext.Progress.Write("Bot 1 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot1[0].data, false);

                List<Block> newBlocksBot2 = new List<Block>();
                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot2Block = new Block(b2, 1);
                newBlocksBot2.Add(bot2Block);
                TestContext.Progress.Write("Bot 2 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot2[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocksBot1);
                allB.Add(newBlocksBot2);
                List<List<Tuple<int, int>>> piecePlaced = bot.GetMove(game.board, allB); 

                List<Tuple<int, int>> bot1Pieces = piecePlaced[0];
                List<Tuple<int, int>> bot2Pieces = piecePlaced[1];

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, bot1Pieces, 2, false);
                game.board.board = allBoards.Item2;
                allBoards = PlacePieceOnBoard(game.board.board, bot2Pieces, 3, false);
                TestContext.Progress.Write("Board AFTER bots placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {2, 3, 3, 0, 0, 0, 1, 1},
                    {1, 0, 3, 1, 0, 1, 1, 1},
                    {1, 1, 3, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }


        /*
            Assert that the formation with the best shape is chosen
        */
        [Test]
        public void ShapePlacement3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------DOUBLE BOT SHAPE PLACEMENT 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                List<Block> newBlocksBot1 = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot1Block = new Block(b1, 1);
                newBlocksBot1.Add(bot1Block);
                TestContext.Progress.Write("Bot 1 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot1[0].data, false);

                List<Block> newBlocksBot2 = new List<Block>();
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    
                };
                Block bot2Block = new Block(b2, 1);
                newBlocksBot2.Add(bot2Block);
                TestContext.Progress.Write("Bot 2 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot2[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocksBot1);
                allB.Add(newBlocksBot2);
                List<List<Tuple<int, int>>> piecePlaced = bot.GetMove(game.board, allB); 

                List<Tuple<int, int>> bot1Pieces = piecePlaced[0];
                List<Tuple<int, int>> bot2Pieces = piecePlaced[1];

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, bot1Pieces, 2, false);
                game.board.board = allBoards.Item2;
                allBoards = PlacePieceOnBoard(game.board.board, bot2Pieces, 3, false);
                TestContext.Progress.Write("Board AFTER bots placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {3, 3, 3, 2, 2, 2, 1, 1},
                    {1, 3, 3, 1, 2, 1, 1, 1},
                    {1, 1, 3, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }


        /*
            Assert that the formation with the best shape is chosen
        */
        [Test]
        public void ShapePlacement4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------DOUBLE BOT SHAPE PLACEMENT 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                List<Block> newBlocksBot1 = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot1Block = new Block(b1, 1);
                newBlocksBot1.Add(bot1Block);
                TestContext.Progress.Write("Bot 1 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot1[0].data, false);

                List<Block> newBlocksBot2 = new List<Block>();
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    
                };
                Block bot2Block = new Block(b2, 1);
                newBlocksBot2.Add(bot2Block);
                TestContext.Progress.Write("Bot 2 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot2[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocksBot1);
                allB.Add(newBlocksBot2);
                List<List<Tuple<int, int>>> piecePlaced = bot.GetMove(game.board, allB); 

                List<Tuple<int, int>> bot1Pieces = piecePlaced[0];
                List<Tuple<int, int>> bot2Pieces = piecePlaced[1];

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, bot1Pieces, 2, false);
                game.board.board = allBoards.Item2;
                allBoards = PlacePieceOnBoard(game.board.board, bot2Pieces, 3, false);
                TestContext.Progress.Write("Board AFTER bots placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {3, 3, 3, 0, 0, 0, 1, 1},
                    {1, 2, 3, 1, 0, 1, 1, 1},
                    {1, 1, 3, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }


        /*
            Assert that the formation with the best shape is chosen
        */
        [Test]
        public void ShapePlacement5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------DOUBLE BOT SHAPE PLACEMENT 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {0, 0, 0, 1, 0, 1, 1, 1},
                    {0, 1, 0, 1, 1, 1, 1, 1},
                    {0, 0, 1, 1, 1, 1, 1, 1}
                };

                List<Block> newBlocksBot1 = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block bot1Block = new Block(b1, 1);
                newBlocksBot1.Add(bot1Block);
                TestContext.Progress.Write("Bot 1 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot1[0].data, false);

                List<Block> newBlocksBot2 = new List<Block>();
                int[][] b2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    
                };
                Block bot2Block = new Block(b2, 1);
                newBlocksBot2.Add(bot2Block);
                TestContext.Progress.Write("Bot 2 Block");
                botInfoPrinter.PrintJaggedArr(newBlocksBot2[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocksBot1);
                allB.Add(newBlocksBot2);
                List<List<Tuple<int, int>>> piecePlaced = bot.GetMove(game.board, allB); 

                List<Tuple<int, int>> bot1Pieces = piecePlaced[0];
                List<Tuple<int, int>> bot2Pieces = piecePlaced[1];

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, bot1Pieces, 2, false);
                game.board.board = allBoards.Item2;
                allBoards = PlacePieceOnBoard(game.board.board, bot2Pieces, 3, false);
                TestContext.Progress.Write("Board AFTER bots placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {2, 2, 0, 1, 0, 1, 1, 1},
                    {2, 1, 3, 1, 1, 1, 1, 1},
                    {0, 0, 1, 1, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1, 1, 1}
                };
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }
    }
}