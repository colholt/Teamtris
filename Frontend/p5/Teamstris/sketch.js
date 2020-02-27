var socket;
/**
 * Global lobbyscreen object.
 *  
 * I do this because I want to make the object in the startscreen instead of here. 
 */
var mLobbyScreen;
var mStartScreen;
var mGameScreen;

var team;
// var player;
function setup() {
  /* Create our canvas with the max windowWidth and windowHeight */
  createCanvas(windowWidth, windowHeight)
  /* Have all our rectangles in center mode */
  rectMode(CENTER);
  /* Have all our text in center mode */
  textAlign(CENTER, CENTER);

  /* Connecting to our WebSocket the backend */
  socket = new WebSocket('ws://198.199.64.158:5202/lobby');

  /* Tells us what screen we are in */
  gameState = 0;
  /* Creating my startscreen object */
  mStartScreen = new StartScreen();
  /* Creating my lobbyscreen object */
  // mLobbyScreen = new LobbyScreen();
  /* Createing my gamescreen object */
  mGameScreen = new GameScreen();
}

function draw() {
  background(0);
  switch (gameState) {
    case 0:
      mStartScreen.draw();
      break;
    case 1:
      mLobbyScreen.draw();
      break;
    case 2:
      mGameScreen.draw();
      break;
    case 3:

      break;
  }
}

module.exports = [setup, draw, mLobbyScreen, mStartScreen, socket];