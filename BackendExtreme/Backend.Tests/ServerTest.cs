using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace Tests
{
    public class STests
    {
        private WebSocket webSocket;
        private string lastMsg;
        private Dictionary<string, Lobby> lobbies;
        private LobbyManager lobbyManager;
        private ScoresManager scoresManager;
        [SetUp]
        public void Setup()
        {
            lobbies = new Dictionary<string, Lobby>();
            lobbyManager = new LobbyManager(lobbies);
            scoresManager = new ScoresManager();
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
            Assert.That(lobbies["five"].game.board.board[1, 2] == 1);
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
    }
}