﻿using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Collections.Generic;
using Newtonsoft.Json;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace Teamtris
{
    /**
    * #class Program |
    * @author JavaComSci, Colholt | 
    * @language csharp | 
    * @desc TODO |
    */
    class Program
    {
        static void Main(string[] args)
        {

            // // initialize game state
            GameState game = new GameState(6, 8);
            game.players = new Dictionary<int, Player>();
            Dictionary<string, Lobby> lobbies = new Dictionary<string, Lobby>();

            // // printing
            Prints infoPrinter = new Prints();
            
            // // CHANGE - RECIEVED FROM THE FRONTEND - REPLACEMENT
            int numBots = 1;

            switch(numBots) {
                case 1: 
                    game.bot = new SingleBot();
                    break;
                case 2:
                    game.bot = new DoubleBot();
                    break;
                case 3:
                    game.bot = new TripleBot();
                    break;
                default:
                    game.bot = null;
                    break;
            }
            

            List<Block> bot1Blocks = new List<Block>();
            List<Block> bot2Blocks = new List<Block>();
            List<Block> bot3Blocks = new List<Block>();
            RandomPiece randomPiece = new RandomPiece();
            
            // game.board.board =  new int[,]{
            //     {0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 1},
            //     {0, 0, 0, 1, 1, 1},
            //     {1, 0, 1, 1, 1, 1}
            // };

            // game.board.board = new int[,]{
            //     {0, 0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 0, 0},
            //     {0, 0, 0, 0, 0, 1, 1},
            //     {0, 0, 0, 1, 0, 1, 1},
            //     {1, 0, 1, 1, 1, 1, 1},
            // };
            game.board.board = new int[,]{
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 1, 0},
                {0, 0, 0, 1, 0, 1, 1, 0},
                {0, 0, 0, 1, 1, 1, 1, 1},
            };
            int[][] block11 = new int[][] {
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 1, 1, 0}, 
                new int[] {0, 1, 0, 0}, 
                new int[] {0, 0, 0, 0}, 
            };
            int[][] block21 = new int[][] {
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 1, 0, 0}, 
                new int[] {1, 1, 0, 0}, 
                new int[] {0, 1, 0, 0}, 
            };
            int[][] block31 = new int[][] {
                new int[] {0, 0, 0, 0}, 
                new int[] {0, 1, 0, 0}, 
                new int[] {0, 1, 0, 0}, 
                new int[] {0, 1, 0, 0}, 
            };
            // int[][] block11 = randomPiece.GenerateRandomPiece();
            // int[][] block21 = randomPiece.GenerateRandomPiece();
            // int[][] block31 = randomPiece.GenerateRandomPiece();
            // bot1Blocks.Add(new Block(block11, 1));
            // bot2Blocks.Add(new Block(block21, 1));
            // bot3Blocks.Add(new Block(block31, 1));
            // List<List<Block>> blocks = new List<List<Block>>();
            // blocks.Add(bot1Blocks);
            // blocks.Add(bot2Blocks);
            // blocks.Add(bot3Blocks);

            // game.bot.GetMove(game.board, blocks);
            // try {
            //     game.bot.GetMove(game.board, blocks);
            // } catch (Exception e) {
            //     Console.WriteLine("Recieved error: "  + e.Message);
            // }

            // connection and adding to the db scores
            // List<string> players = new List<string>();
            // players.Add("modified");
            // players.Add("hi");
            // players.Add(null);
            // players.Add(null);
            // ScoresInfo scoresInfo = new ScoresInfo("Team HIHIOWE", players, 1, 6000);
            // SQLConnection.AddTeamScore(scoresInfo);   
            // Tuple<List<ScoresInfo>, ScoresInfo> retrievedInfo = SQLConnection.GetTopTeamsAndCurrentTeam("Team HIHIOWE");
            // Console.WriteLine("Top teams");
            // infoPrinter.PrintScoreList(retrievedInfo.Item1);
            // Console.WriteLine("Current team");
            // infoPrinter.PrintScoreInfo(retrievedInfo.Item2);


            // List<ScoresInfo> retrieved = SQLConnection.GetTopTeams();
            // infoPrinter.PrintScoreList(retrieved);


            // string teamName = "Team10";
            // int score = 100;
            // string scoreInfo = "Best achieving score: " + score;

            // PointF firstLocation = new PointF(120f, 200f);
            // PointF secondLocation = new PointF(120f, 240f);

            // Bitmap bitmap = new System.Drawing.Bitmap("canvas.png");
            // string imageFilePath = "canvas.bmp";

            // using(Graphics graphics = Graphics.FromImage(bitmap))
            // {
            //     using (Font arialFont =  new Font("Arial", 20))
            //     {
            //         graphics.DrawString(teamName, arialFont, Brushes.Red, firstLocation);
            //         int i = teamName.Length;
            //         while(i < scoreInfo.Length - 2) {
            //             graphics.DrawString(".", arialFont, Brushes.Red, new PointF((120 + teamName.Length * 8) + (10 * i), 200f));
            //             i++;
            //         }
            //         graphics.DrawString(scoreInfo, arialFont, Brushes.Blue, secondLocation);
            //     }
            // }

            // string outputFileName =imageFilePath;
            // using (MemoryStream memory = new MemoryStream())
            // {
            //     using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
            //     {
            //         bitmap.Save(memory, ImageFormat.Jpeg);
            //         byte[] bytes = memory.ToArray();
            //         fs.Write(bytes, 0, bytes.Length);
            //     }
            // }


            // string teamName = "AHHHH";
            // int score = 50;
            // string scoreInfo = "Best achieving score: " + score;

            // PointF firstLocation = new PointF(320f, 400f);
            // PointF secondLocation = new PointF(320f, 490f);

            // Bitmap b = null;
            // Bitmap bitmap;
            // if(b == null) {
            //     bitmap = new System.Drawing.Bitmap("canvas.png");
            // } else {
            //     bitmap = b;
            // }
            

            // using(Graphics graphics = Graphics.FromImage(bitmap))
            // {
            //     using (Font arialFont =  new Font("Arial", 50))
            //     {
            //         graphics.DrawString(teamName, arialFont, Brushes.Red, firstLocation);
            //         int i = teamName.Length;
            //         graphics.DrawString(scoreInfo, arialFont, Brushes.Blue, secondLocation);
            //     }
            // }

            // string imageFilePath = "canvas.bmp";
            // string outputFileName = imageFilePath;
            // using (MemoryStream memory = new MemoryStream())
            // {
            //     using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
            //     {
            //         bitmap.Save(memory, ImageFormat.Jpeg);
            //         byte[] bytes = memory.ToArray();
            //         fs.Write(bytes, 0, bytes.Length);
            //     }
            // }


            // Bitmap bImage = bitmap;
            // System.IO.MemoryStream ms = new MemoryStream();
            // bImage.Save(ms, ImageFormat.Jpeg);
            // byte[] byteImage = ms.ToArray();
            // var encodedImage= Convert.ToBase64String(byteImage); 
            // Console.WriteLine("ENCODED IMAGE " +  encodedImage);


            // create localhost web socket server on port 5202
            var wssv = new WebSocketServer("ws://0.0.0.0:5202");
            wssv.Start();
            wssv.AddWebSocketService<LobbyManager>("/lobby", () => new LobbyManager(lobbies));
            wssv.AddWebSocketService<Play>("/play", () => new Play(lobbies));
            wssv.AddWebSocketService<ScoresManager>("/scores", () => new ScoresManager());
            wssv.AddWebSocketService<ShareManager>("/share", () => new ShareManager());
            wssv.AddWebSocketService<LegendManager>("/legend", () => new LegendManager());
            wssv.AddWebSocketService<ScoresDirectManager>("/scoresDirect", () => new ScoresDirectManager());
            GameManager gameManager = new GameManager(lobbies);
            Console.WriteLine("Starting to check for sockets");
            // start game broadcasting service
            gameManager.startGame();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
