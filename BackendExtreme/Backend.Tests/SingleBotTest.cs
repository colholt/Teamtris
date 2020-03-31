using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private GameState game;
        private List<Block> bot1Blocks;
        private List<Block> blocks = new List<Block>();
        private List<List<Block>> allBlocks;
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



            Block block1 = new Block(b1, 1);
            Block block2 = new Block(b2, 1);
            Block block3 = new Block(b3, 1);
            Block block4 = new Block(b4, 1);
            Block block5 = new Block(b5, 1);
            Block block6 = new Block(b6, 1);
            Block block7 = new Block(b7, 1);
            Block block8 = new Block(b8, 1);
            Block block9 = new Block(b9, 1);
            Block block10 = new Block(b10, 1);
            Block block11 = new Block(b11, 1);
            Block block12 = new Block(b12, 1);
            Block block13= new Block(b13, 1);

            bot1Blocks = new List<Block>();

            bot1Blocks.Add(block1);
            bot1Blocks.Add(block2);
            bot1Blocks.Add(block3);
            bot1Blocks.Add(block4);
            bot1Blocks.Add(block5);
            bot1Blocks.Add(block6);
            bot1Blocks.Add(block7);
            bot1Blocks.Add(block8);
            bot1Blocks.Add(block9);
            bot1Blocks.Add(block10);
            bot1Blocks.Add(block11);
            bot1Blocks.Add(block12);
            bot1Blocks.Add(block13);

            allBlocks = new List<List<Block>>();
            allBlocks.Add(bot1Blocks); 

            blocks = allBlocks[0];
        }


        /*
            Helper for placing the pieces on the board
            @@param board - board that piece must be placed on
                    dots - the dots of the piece 
                    clearLines - whether lines must be removed on the board when they are cleared
         */
        public Tuple<int[,], int[,]> PlacePieceOnBoard(int [,] board, List<Tuple<int, int>> dots, bool clearLines = true) {
            int[,] newBoard = new int[board.GetLength(0),board.GetLength(1)];

            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    newBoard[i,j] = board[i,j];
                }
            }

            foreach(Tuple<int, int> dot in dots) {
                board[dot.Item1, dot.Item2] = 1;
                newBoard[dot.Item1, dot.Item2] = 2;
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
            Assert that a piece can be rotated once
        */
        [Test]
        public void RotatePieceOnce1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE ONCE 1--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int[][] expectedRotation = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated once
        */
        [Test]
        public void RotatePieceOnce2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE ONCE 2--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int[][] expectedRotation = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated once
        */
        [Test]
        public void RotatePieceOnce3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE ONCE 3--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int[][] expectedRotation = new int[][] {
                    new int[] {0, 0, 0, 1}, 
                    new int[] {0, 0, 0, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated once
        */
        [Test]
        public void RotatePieceOnce4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE ONCE 4--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int[][] expectedRotation = new int[][] {
                    new int[] {0, 0, 0, 1}, 
                    new int[] {0, 0, 0, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated once
        */
        [Test]
        public void RotatePieceOnce5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE ONCE 5--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                TestContext.Progress.WriteLine("--------------------------------------");

                int[][] expectedRotation = new int[][] {
                    new int[] {0, 0, 0, 1}, 
                    new int[] {0, 0, 0, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 1}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation));

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated twice
        */
        [Test]
        public void RotatePieceTwice1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE TWICE 1--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated twice
        */
        [Test]
        public void RotatePieceTwice2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE TWICE 2--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated twice
        */
        [Test]
        public void RotatePieceTwice3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE TWICE 3--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 1}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated twice
        */
        [Test]
        public void RotatePieceTwice4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE TWICE 4--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated twice
        */
        [Test]
        public void RotatePieceTwice5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE TWICE 5--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated three times
        */
        [Test]
        public void RotatePieceThrice1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE THRICE 1--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation3 = new int[][] {
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation3));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated three times
        */
        [Test]
        public void RotatePieceThrice2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE THRICE 2--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation3 = new int[][] {
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation3));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated three times
        */
        [Test]
        public void RotatePieceThrice3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE THRICE 3--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation3 = new int[][] {
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation3));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated three times
        */
        [Test]
        public void RotatePieceThrice4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE THRICE 4--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation3 = new int[][] {
                    new int[] {0, 1, 1, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation3));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated three times
        */
        [Test]
        public void RotatePieceThrice5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE THRICE 5--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 1}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation1));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation2 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 1, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation2));

                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);

                int[][] expectedRotation3 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };

                Assert.That(b.data, Is.EqualTo(expectedRotation3));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated four times is the same
        */
        [Test]
        public void RotatePieceFour1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE FOUR 1--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                Assert.That(b.data, Is.EqualTo(b1));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

        /*
            Assert that a piece can be rotated four times is the same
        */
        [Test]
        public void RotatePieceFour2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE FOUR 2--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 1, 1, 0}, 
                    new int[] {0, 0, 1, 0}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                Assert.That(b.data, Is.EqualTo(b1));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

        /*
            Assert that a piece can be rotated four times is the same
        */
        [Test]
        public void RotatePieceFour3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE FOUR 3--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 0, 1, 1}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                Assert.That(b.data, Is.EqualTo(b1));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated four times is the same
        */
        [Test]
        public void RotatePieceFour4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE FOUR 4--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 0}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                Assert.That(b.data, Is.EqualTo(b1));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a piece can be rotated four times is the same
        */
        [Test]
        public void RotatePieceFour5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------ROTATE PIECE FOUR 5--------------------------------------");
            try {
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 1, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                };

                TestContext.Progress.WriteLine("--------------------------------------");
                Block b = new Block(b1, 1);
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                b.data = b.RotateMatrix();
                botInfoPrinter.PrintJaggedArr(b.data, false);
                Assert.That(b.data, Is.EqualTo(b1));
                TestContext.Progress.WriteLine("--------------------------------------");

            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

        /*
            Assert that a move can be taken without erroring for trying a piece
        */
        [Test]
        public void MakeDiffPieceMove1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE DIFF PIECE MOVE 1--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                newBlocks.Add(blocks[0]);
                Block block = blocks[0];
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.Write("Board with piece");
                botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced, false);
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

        /*
            Assert that a move can be taken without erroring for trying a piece
        */
        [Test]
        public void MakeDiffPieceMove2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE DIFF PIECE MOVE 2--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[1];
                newBlocks.Add(blocks[1]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.Write("Board with piece");
                botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced, false);
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring for trying a piece
        */
        [Test]
        public void MakeDiffPieceMove3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE DIFF PIECE MOVE 3--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[2];
                newBlocks.Add(blocks[2]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.Write("Board with piece");
                botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced, false);
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring for trying a piece
        */
        [Test]
        public void MakeDiffPieceMove4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE DIFF PIECE MOVE 4--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[3];
                newBlocks.Add(blocks[3]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.Write("Board with piece");
                botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced, false);
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

        /*
            Assert that a move can be taken without erroring for trying a piece
        */
        [Test]
        public void MakeDiffPieceMove5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE DIFF PIECE MOVE 5--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[4];
                newBlocks.Add(blocks[4]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.Write("Board with piece");
                botInfoPrinter.PrintBoardWithPiece(game.board.board, piecePlaced, false);
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that the bottom left of a piece can be found
        */
        [Test]
        public void FindBottomLeft1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BOTTOM LEFT 1--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[0];
                newBlocks.Add(blocks[0]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                Tuple<int, int> bottomLeftInfo = block.FindBottomLeft();
                Tuple<int, int> expectedTuple = new Tuple<int, int>(3,2);
                Assert.That(bottomLeftInfo, Is.EqualTo(expectedTuple));
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

         /*
            Assert that the bottom left of a piece can be found
        */
        [Test]
        public void FindBottomLeft2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BOTTOM LEFT 2--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[1];
                newBlocks.Add(blocks[1]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                Tuple<int, int> bottomLeftInfo = block.FindBottomLeft();
                Tuple<int, int> expectedTuple = new Tuple<int, int>(2,2);
                Assert.That(bottomLeftInfo, Is.EqualTo(expectedTuple));
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

         /*
            Assert that the bottom left of a piece can be found
        */
        [Test]
        public void FindBottomLeft3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BOTTOM LEFT 3--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[2];
                newBlocks.Add(blocks[2]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                Tuple<int, int> bottomLeftInfo = block.FindBottomLeft();
                Tuple<int, int> expectedTuple = new Tuple<int, int>(3,0);
                Assert.That(bottomLeftInfo, Is.EqualTo(expectedTuple));
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

         /*
            Assert that the bottom left of a piece can be found
        */
        [Test]
        public void FindBottomLeft4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BOTTOM LEFT 4--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[3];
                newBlocks.Add(blocks[3]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                Tuple<int, int> bottomLeftInfo = block.FindBottomLeft();
                Tuple<int, int> expectedTuple = new Tuple<int, int>(3,1);
                Assert.That(bottomLeftInfo, Is.EqualTo(expectedTuple));
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }

         /*
            Assert that the bottom left of a piece can be found
        */
        [Test]
        public void FindBottomLeft5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------BOTTOM LEFT 5--------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[4];
                newBlocks.Add(blocks[4]);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(block.data, false);
                Tuple<int, int> bottomLeftInfo = block.FindBottomLeft();
                Tuple<int, int> expectedTuple = new Tuple<int, int>(3,1);
                Assert.That(bottomLeftInfo, Is.EqualTo(expectedTuple));
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception recieved " + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring when same piece placed twice on the board
        */
        [Test]
        public void MakeMoveWithSamePieceTwice() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MOVE WITH SAME PIECE TWICE--------------------------------------");
            try {
                List<Block> newBlocks = new List<Block>();
                newBlocks.Add(blocks[0]);
                TestContext.Progress.WriteLine("--------------------------------------");
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                for(int i = 0; i < 2; i++) {
                    List<List<Block>> b = new List<List<Block>>();
                    b.Add(newBlocks);
                    List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                    Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced);

                    game.board.board = allBoards.Item1;
                    
                    TestContext.Progress.Write("Board after piece placed");
                    botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);
                }
                TestContext.Progress.WriteLine("--------------------------------------");

                // point back to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring when multiple pieces placed on the board
        */
        [Test]
        public void MakeMultipleMovesWithEmptyBoard() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE MULTIPLE MOVES 1--------------------------------------");
            try {
                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0}
                };

                // try to play a game with the pieces
                bool gameDone = false;
                int count = 0;
                int x = 0;
                while(x < 10) {
                    for(int i = 0; i < 13; i++) {
                        x++;
                        Block block = blocks[i];
                        List<Block> newBlocks = new List<Block>();
                        newBlocks.Add(block);

                        TestContext.Progress.Write(count);
                        count++;

                        // TestContext.Progress.Write("Piece to be placed");
                        // botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                        // TestContext.Progress.Write("Board BEFORE piece placed");
                        // botInfoPrinter.PrintMultiDimArr(game.board.board, false);
                        List<List<Block>> b = new List<List<Block>>();
                        b.Add(newBlocks);
                        List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                        // TestContext.Progress.Write(piecePlaced[0].Item1 + " " + piecePlaced[0].Item2);
                        if(piecePlaced == null) {
                            TestContext.Progress.Write("Piece that could not be placed");
                            botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                            gameDone = true;
                            break;
                        }

                        Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced);

                        game.board.board = allBoards.Item1;
                    
                        TestContext.Progress.Write("Board AFTER piece placed");
                        botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);
                        // botInfoPrinter.PrintMultiDimArr(allBoards.Item1, false);
                    }

                    if(gameDone == true) {
                        TestContext.Progress.Write("Board is filled");
                        break;
                    }
                }

                TestContext.Progress.WriteLine("--------------------------------------");

                // point back to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };
               
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring when single pieces are place don the baord
        */
        [Test]
        public void MakeMultipleMovesWithDot() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE MULTIPLE MOVES 2--------------------------------------");
            try {
                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0}
                };

                // try to play a game with the pieces
                bool gameDone = false;
                int count = 0;
                int x = 0;
                while(x < 10) {
                    for(int i = 11; i < 13; i++) {
                        x++;
                        Block block = blocks[i];
                        List<Block> newBlocks = new List<Block>();
                        newBlocks.Add(block);
                        // TestContext.Progress.Write("Piece to be placed");
                        // botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                        TestContext.Progress.Write(count);
                        count++;

                        List<List<Block>> b = new List<List<Block>>();
                        b.Add(newBlocks);
                        List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b, true); 

                        if(piecePlaced == null) {
                            TestContext.Progress.Write("Piece that could not be placed");
                            botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                            gameDone = true;
                            break;
                        }

                        Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced);

                        game.board.board = allBoards.Item1;
                    
                        // TestContext.Progress.Write("Board AFTER piece placed");
                        botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);
                        // botInfoPrinter.PrintMultiDimArr(allBoards.Item1, false);
                    }

                    if(gameDone == true) {
                        TestContext.Progress.Write("Board is filled");
                        break;
                    }
                }

                TestContext.Progress.WriteLine("--------------------------------------");

                // point back to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };
               
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring when multiple pieces placed on the board
        */
        [Test]
        public void MakeMultipleMovesRandBoard1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE MULTIPLE MOVES RANDOM BOARD 1--------------------------------------");
            try {
                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 1, 1, 1, 0, 0},
                    {1, 1, 1, 1, 0, 0}
                };

                // try to play a game with the pieces
                bool gameDone = false;
                int count = 0;
                int x = 0;
                while(x < 10) {
                    for(int i = 9; i < 13; i++) {
                        x++;
                        Block block = blocks[i];
                        List<Block> newBlocks = new List<Block>();
                        newBlocks.Add(block);

                        TestContext.Progress.Write(count);
                        count++;

                        // TestContext.Progress.Write("Piece to be placed");
                        // botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                        // TestContext.Progress.Write("Board BEFORE piece placed");
                        // botInfoPrinter.PrintMultiDimArr(game.board.board, false);

                        List<List<Block>> b = new List<List<Block>>();
                        b.Add(newBlocks);
                        List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                        // TestContext.Progress.Write(piecePlaced[0].Item1 + " " + piecePlaced[0].Item2);
                        if(piecePlaced == null) {
                            TestContext.Progress.Write("Piece that could not be placed");
                            botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                            gameDone = true;
                            break;
                        }

                        Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced);

                        // check to see if the baord is empty
                        bool boardEmpty = game.board.IsBoardEmpty(allBoards.Item1);

                        if(boardEmpty) {
                            TestContext.Progress.Write("Board is cleared");
                            gameDone = true;
                            break;
                        }

                        game.board.board = allBoards.Item1;
                    
                        TestContext.Progress.Write("Board AFTER piece placed");
                        botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);
                        // botInfoPrinter.PrintMultiDimArr(allBoards.Item1, false);
                    }

                    if(gameDone == true) {
                        TestContext.Progress.Write("Board is filled");
                        break;
                    }
                }

                TestContext.Progress.WriteLine("--------------------------------------");

                // point back to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };
               
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that a move can be taken without erroring when multiple pieces placed on the board
        */
        [Test]
        public void MakeMultipleMovesRandBoard2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------MAKE MULTIPLE MOVES RANDOM BOARD 2--------------------------------------");
            try {
                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                // try to play a game with the pieces
                bool gameDone = false;
                int count = 0;
                int x = 0;
                while(x < 10) {
                    for(int i = 9; i < 13; i++) {
                        x++;
                        Block block = blocks[i];
                        List<Block> newBlocks = new List<Block>();
                        newBlocks.Add(block);

                        TestContext.Progress.Write(count);
                        count++;

                        // TestContext.Progress.Write("Piece to be placed");
                        // botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                        // TestContext.Progress.Write("Board BEFORE piece placed");
                        // botInfoPrinter.PrintMultiDimArr(game.board.board, false);

                        List<List<Block>> b = new List<List<Block>>();
                        b.Add(newBlocks);
                        List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                        // TestContext.Progress.Write(piecePlaced[0].Item1 + " " + piecePlaced[0].Item2);
                        if(piecePlaced == null) {
                            TestContext.Progress.Write("Piece that could not be placed");
                            botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                            gameDone = true;
                            break;
                        }

                        Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced);

                        // check to see if the baord is empty
                        bool boardEmpty = game.board.IsBoardEmpty(allBoards.Item1);

                        if(boardEmpty) {
                            TestContext.Progress.Write("Board is cleared");
                            gameDone = true;
                            break;
                        }

                        game.board.board = allBoards.Item1;
                    
                        TestContext.Progress.Write("Board AFTER piece placed");
                        botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);
                        // botInfoPrinter.PrintMultiDimArr(allBoards.Item1, false);
                    }

                    if(gameDone == true) {
                        TestContext.Progress.Write("Board is filled");
                        break;
                    }
                }

                TestContext.Progress.WriteLine("--------------------------------------");

                // point back to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };
               
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }



        /*
            Assert that the shape provided to the bot is valid with valid shape
        */
        [Test]
        public void TestShapeFormation1Valid() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED VALID 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[0];
                newBlocks.Add(blocks[0]);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that the shape provided to the bot is valid with valid shape
        */
        [Test]
        public void TestShapeFormation2Valid() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED VALID 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[1];
                newBlocks.Add(blocks[1]);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that the shape provided to the bot is valid with valid shape
        */
        [Test]
        public void TestShapeFormation3Valid() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED VALID 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[2];
                newBlocks.Add(blocks[2]);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }

        
        /*
            Assert that the shape provided to the bot is valid with valid shape
        */
        [Test]
        public void TestShapeFormation4Valid() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED VALID 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[3];
                newBlocks.Add(blocks[3]);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
            }        
        }


        /*
            Assert that the shape provided to the bot is valid with valid shape
        */
        [Test]
        public void TestShapeFormation5Valid() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PROVIDED VALID 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");
                List<Block> newBlocks = new List<Block>();
                Block block = blocks[4];
                newBlocks.Add(blocks[4]);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 
                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved" + e.Message);
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
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 
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
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 
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
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 
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
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board,allB); 
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
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 
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
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                List<List<Block>> allB= new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 2, 2, 1, 1, 1},
                    {1, 2, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 2, 0, 1, 0, 1},
                    {1, 2, 2, 1, 1, 1},
                    {1, 2, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                
                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 1, 2, 2, 2, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 2, 2, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block b = new Block(b1, 1);
                newBlocks.Add(b);
                TestContext.Progress.Write("Piece to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);

                List<List<Block>> allB = new List<List<Block>>();
                allB.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, allB); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 0, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 2, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNext1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 0, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 2, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNext2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 2, 2, 2, 0},
                    {1, 2, 2, 2, 2, 1},
                    {1, 2, 2, 2, 2, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNext3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 1, 2, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNext4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {0, 0, 0, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {1, 1, 1, 0}, 
                    new int[] {1, 1, 1, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 2, 2, 2, 0},
                    {1, 0, 2, 2, 2, 0},
                    {1, 0, 2, 2, 2, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNext5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 2, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNextNext1() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT NEXT 1-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                int[][] b3 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block3 = new Block(b3, 1);
                newBlocks.Add(block3);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[2].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 2, 2, 0, 0},
                    {1, 0, 2, 2, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNextNext2() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT NEXT 2-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 1, 1}, 
                    new int[] {1, 1, 1, 1}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                int[][] b3 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block3 = new Block(b3, 1);
                newBlocks.Add(block3);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[2].data, false);
                
                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 2, 2, 2, 0},
                    {1, 2, 2, 2, 2, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNextNext3() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT NEXT 3-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                int[][] b3 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block3 = new Block(b3, 1);
                newBlocks.Add(block3);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[2].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 2, 2, 0, 0},
                    {1, 0, 2, 2, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNextNext4() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT NEXT 4-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                int[][] b3 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block3 = new Block(b3, 1);
                newBlocks.Add(block3);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[2].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 0, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 2, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
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
        public void ShapePlacementWithNextNext5() {
            TestContext.Progress.WriteLine("\n\n\n\n\n--------------------------------------SHAPE PLACEMENT WITH NEXT NEXT 5-------------------------------------");
            try {
                TestContext.Progress.WriteLine("--------------------------------------");

                // point it to new board
                game.board.board = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                List<Block> newBlocks = new List<Block>();
                int[][] b1 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block1 = new Block(b1, 1);
                newBlocks.Add(block1);

                int[][] b2 = new int[][] {
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {1, 1, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block2 = new Block(b2, 1);
                newBlocks.Add(block2);

                int[][] b3 = new int[][] {
                    new int[] {1, 0, 0, 0}, 
                    new int[] {1, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                    new int[] {0, 0, 0, 0}, 
                };
                Block block3 = new Block(b3, 1);
                newBlocks.Add(block3);

                TestContext.Progress.Write("Pieces to be placed");
                botInfoPrinter.PrintJaggedArr(newBlocks[0].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[1].data, false);
                botInfoPrinter.PrintJaggedArr(newBlocks[2].data, false);

                List<List<Block>> b = new List<List<Block>>();
                b.Add(newBlocks);
                List<Tuple<int, int>> piecePlaced = game.bot.GetSingleMove(game.board, b); 

                Tuple<int[,], int[,]> allBoards = PlacePieceOnBoard(game.board.board, piecePlaced, false);
                    
                TestContext.Progress.Write("Board AFTER piece placed");
                botInfoPrinter.PrintMultiDimArr(allBoards.Item2, false);

                int[,] expectedBoard = new int[,]{
                    {1, 0, 0, 0, 0, 0},
                    {1, 2, 2, 0, 0, 0},
                    {1, 2, 2, 0, 0, 1},
                    {1, 2, 1, 1, 1, 1},
                    {1, 0, 0, 1, 0, 0},
                    {1, 1, 1, 1, 0, 1}
                };

                Assert.That(allBoards.Item2, Is.EqualTo(expectedBoard));

                 // point it to old board
                game.board.board = new int[,]{
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 1},
                    {1, 0, 1, 1, 1, 1}
                };

                TestContext.Progress.WriteLine("--------------------------------------");
            } catch (Exception e) {
                Assert.Fail("Expected no exception but recieved " + e.Message);
            }        
        }

    }

}