using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace Tests
{
    public class STests
    {
        private WebSocket webSocket;
        private string lastMsg;
        private Dictionary<string, Lobby> lobbies;
        private Lobby lobby;
        private LobbyManager lobbyManager;
        private ScoresManager scoresManager;
        private ShareManager shareManager;
        private LegendManager legendManager;

        [SetUp]
        public void Setup()
        {
            lobbies = new Dictionary<string, Lobby>();
            lobbyManager = new LobbyManager(lobbies);
            scoresManager = new ScoresManager();
            shareManager = new ShareManager();
            legendManager = new LegendManager();
        }

        [Test]
        public void MultipleLobbies()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            TestContext.Progress.WriteLine(lobbies.Keys.Count);
            Assert.That(lobbies.Keys.Count, Is.EqualTo(2));
        }

        [Test]
        public void PlayerJoinAlert()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null, "five");
            LobbyInfoPacket lip = lobbyManager.joinLobby("testing", 1, "bob", "no");
            Assert.That(lip.dataType == Packets.UPDATE);
        }

        [Test]
        public void PlayerStateUpdate()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null, "five");
            LobbyInfoPacket lip = lobbyManager.joinLobby("testing", 1, "bob", "no");
            Assert.That(lobbies["five"].players.Count == 2);
        }

        [Test]
        public void GameStateAlert()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null, "five");
            LobbyInfoPacket lip = lobbyManager.joinLobby("testing", 1, "bob", "no");
            Packet packet = new Packet();
            packet.data = "{'lobbyID': 'five'}";
            packet.type = 2;
            lobbyManager.startGame(packet);
            PlayerInputPacket pip = new PlayerInputPacket();
            pip.lobbyID = "five";
            pip.move = "left";
            UpdatePacket up = lobbyManager.processInput(pip);
            Assert.That(up.move == "left");
        }

        [Test]
        public void BlockFreezeStateUpdate()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null, "five");
            LobbyInfoPacket lip = lobbyManager.joinLobby("testing", 1, "bob", "no");
            Packet packet = new Packet();
            packet.data = "{'lobbyID': 'five'}";
            packet.type = 2;
            lobbyManager.startGame(packet);
            PlayerInputPacket pip = new PlayerInputPacket();
            pip.lobbyID = "five";
            pip.move = "freeze";
            int[][] indices = new int[][] { new int[] { 1, 2 } };
            pip.shapeIndices = indices;
            UpdatePacket up = lobbyManager.processInput(pip);
        }

        [Test]
        public void EndGameDetection()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null, "five");
            EndPacket endPacket = new EndPacket();
            Assert.That(lobbyManager.checkGameEnd(endPacket) == true);
        }


        [Test]
        public void ScoreInfoWithOnePlayer()
        {
            String d = "{\"teamName\":\"Team1\",\"playerNames\":[\"Player1\"],\"teamScore\":3,\"timePlayed\":50}";
            ScoresPacket sPacket = JsonConvert.DeserializeObject<ScoresPacket>(d);

            ScoresInfo scoresInfo = scoresManager.CreateScoresInfo(sPacket);

            Assert.That(scoresInfo.playerNames.Count, Is.EqualTo(4));
            Assert.That(scoresInfo.teamName, Is.EqualTo("Team1"));
            Assert.That(scoresInfo.teamScore, Is.EqualTo(3));
            Assert.That(scoresInfo.timePlayed, Is.EqualTo(50));
            Assert.That(scoresInfo.playerNames[0], Is.EqualTo("Player1"));
            Assert.That(scoresInfo.playerNames[1], Is.EqualTo(null));
            Assert.That(scoresInfo.playerNames[2], Is.EqualTo(null));
            Assert.That(scoresInfo.playerNames[3], Is.EqualTo(null));
        }

        [Test]
        public void ScoreInfoWithTwoPlayers()
        {
            String d = "{\"teamName\":\"Team1\",\"playerNames\":[\"Player1\", \"Player2\"],\"teamScore\":3,\"timePlayed\":50}";
            ScoresPacket sPacket = JsonConvert.DeserializeObject<ScoresPacket>(d);

            ScoresInfo scoresInfo = scoresManager.CreateScoresInfo(sPacket);

            Assert.That(scoresInfo.playerNames.Count, Is.EqualTo(4));
            Assert.That(scoresInfo.teamName, Is.EqualTo("Team1"));
            Assert.That(scoresInfo.teamScore, Is.EqualTo(3));
            Assert.That(scoresInfo.timePlayed, Is.EqualTo(50));
            Assert.That(scoresInfo.playerNames[0], Is.EqualTo("Player1"));
            Assert.That(scoresInfo.playerNames[1], Is.EqualTo("Player2"));
            Assert.That(scoresInfo.playerNames[2], Is.EqualTo(null));
            Assert.That(scoresInfo.playerNames[3], Is.EqualTo(null));
        }

        [Test]
        public void ScoreInfoWithThreePlayers()
        {
            String d = "{\"teamName\":\"Team1\",\"playerNames\":[\"Player1\", \"Player2\", \"Player3\"],\"teamScore\":3,\"timePlayed\":50}";
            ScoresPacket sPacket = JsonConvert.DeserializeObject<ScoresPacket>(d);

            ScoresInfo scoresInfo = scoresManager.CreateScoresInfo(sPacket);

            Assert.That(scoresInfo.playerNames.Count, Is.EqualTo(4));
            Assert.That(scoresInfo.teamName, Is.EqualTo("Team1"));
            Assert.That(scoresInfo.teamScore, Is.EqualTo(3));
            Assert.That(scoresInfo.timePlayed, Is.EqualTo(50));
            Assert.That(scoresInfo.playerNames[0], Is.EqualTo("Player1"));
            Assert.That(scoresInfo.playerNames[1], Is.EqualTo("Player2"));
            Assert.That(scoresInfo.playerNames[2], Is.EqualTo("Player3"));
            Assert.That(scoresInfo.playerNames[3], Is.EqualTo(null));
        }

        [Test]
        public void ScoreInfoWithAllPlayers()
        {
            String d = "{\"teamName\":\"Team1\",\"playerNames\":[\"Player1\", \"Player2\", \"Player3\", \"Player4\"],\"teamScore\":3,\"timePlayed\":50}";
            //    {
            //     "teamName":"Team1",
            //     "playerNames":["Player1", "Player2", "Player3", "Player4"],
            //     "teamScorw":3,
            //     "timePlayed":50
            //     }
            ScoresPacket sPacket = JsonConvert.DeserializeObject<ScoresPacket>(d);

            ScoresInfo scoresInfo = scoresManager.CreateScoresInfo(sPacket);

            Assert.That(scoresInfo.playerNames.Count, Is.EqualTo(4));
            Assert.That(scoresInfo.teamName, Is.EqualTo("Team1"));
            Assert.That(scoresInfo.teamScore, Is.EqualTo(3));
            Assert.That(scoresInfo.timePlayed, Is.EqualTo(50));
            Assert.That(scoresInfo.playerNames[0], Is.EqualTo("Player1"));
            Assert.That(scoresInfo.playerNames[1], Is.EqualTo("Player2"));
            Assert.That(scoresInfo.playerNames[2], Is.EqualTo("Player3"));
            Assert.That(scoresInfo.playerNames[3], Is.EqualTo("Player4"));
        }


        [Test]
        public void ShareInfo1()
        {
            String d = "{\"teamName\":\"Team1\",\"playerNames\":[\"Player1\", \"Player2\", \"Player3\", \"Player4\"],\"teamScore\":3,\"timePlayed\":50}";
            ScoresPacket sPacket = JsonConvert.DeserializeObject<ScoresPacket>(d);

            ScoresInfo scoresInfo = scoresManager.CreateScoresInfo(sPacket);

            List<ScoresInfo> scoresList = new List<ScoresInfo>();

            // Bitmap bitmap = new System.Drawing.Bitmap("canvas.bmp"); 

            // var encoded = shareManager.CreateDetails(Tuple.Create<List<ScoresInfo>, ScoresInfo>(scoresList, scoresInfo), null);

            PointF firstLocation = new PointF(320f, 400f);
            PointF secondLocation = new PointF(320f, 490f);

            Bitmap bitmap = new Bitmap(200, 100);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 50))
                {
                    graphics.DrawString("teamName", arialFont, Brushes.Red, firstLocation);
                    graphics.DrawString("500", arialFont, Brushes.Blue, secondLocation);
                }
            }

            Bitmap bImage = bitmap;
            System.IO.MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var encodedImage = Convert.ToBase64String(byteImage);

            // Assert.That(encodedImage, Is.Not.Contains(" ") );
        }

        [Test]
        public void RemoveFirstRow()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemoveMultipleRows()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeColumn()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 1},
                    {1, 0, 0, 1, 0, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 2},
                    {1, 1, 1, 1, 1, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 0},
                    {1, 0, 0, 1, 0, 1, 1, 0}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeMultipleColumns()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 1, 1},
                    {1, 1, 0, 1, 0, 1, 1, 1},
                    {1, 2, 1, 1, 1, 1, 1, 2},
                    {1, 1, 1, 1, 0, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 1, 0},
                    {1, 0, 0, 1, 0, 1, 1, 0},
                    {1, 0, 1, 1, 0, 1, 1, 0}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeSecondRowColumn()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {1, 2, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 1, 1, 0, 1, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeArea()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {1, 3, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 1, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeAreaChainReaction()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {1, 3, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 3, 0, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeAreaColumnReaction()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 1, 0, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0, 0},
                    {0, 3, 0, 1, 0, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0, 0},
                    {1, 2, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1, 1, 1}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0, 0},
                    {1, 0, 1, 1, 0, 1, 1, 1}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void RemovePowerCubeAreaManyCubes()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            Lobby lobby = new Lobby("1", 0);
            lobby.game = new GameState(8, 8);
            lobby.game.board.board = new int[,] {
                    {0, 0, 0, 0, 1, 1, 1, 0},
                    {0, 0, 0, 0, 1, 3, 1, 0},
                    {0, 0, 0, 0, 1, 1, 1, 0},
                    {0, 1, 1, 1, 1, 1, 0, 0},
                    {0, 1, 1, 1, 1, 1, 0, 0},
                    {1, 1, 1, 3, 1, 1, 1, 1},
                    {0, 1, 1, 1, 1, 2, 0, 0},
                    {0, 1, 1, 1, 1, 1, 0, 0}};
            lobbyManager.checkRows(lobby);
            int[,] resBoard = lobby.game.board.board;
            int[,] testBoard = new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0}};
            bool isSameBoard = true;
            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (resBoard[i, j] != testBoard[i, j])
                        isSameBoard = false;
                }
            }
            Prints prints = new Prints();
            Console.WriteLine("RESULTING BOARD");
            prints.PrintMultiDimArr(lobby.game.board.board);
            Assert.That(isSameBoard, Is.EqualTo(true));
        }

        [Test]
        public void LegendInfo()
        {
            var legendInfo = legendManager.CreateLegend();
            Assert.That(legendInfo, Is.Not.Contains(" "));
        }
    }
}