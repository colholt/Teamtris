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
        [SetUp]
        public void Setup()
        {
            lobbies = new Dictionary<string, Lobby>();
            lobbyManager = new LobbyManager(lobbies);
        }

        [Test]
        public void MultipleLobbies()
        {
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            lobbyManager.createLobby(4, "bob", 5, "no", (WebSocketSharp.WebSocket)null);
            TestContext.Progress.WriteLine(lobbies.Keys.Count);
            Assert.That(lobbies.Keys.Count, Is.EqualTo(2));
        }
    }
}