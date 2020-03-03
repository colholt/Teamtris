
// #function General::keyPressed |
// @author Steven Dellamore, Richard Hansen |
// @desc Will be called whenever the presses a key.
// Once called, it will go straight into a switch to decide where to 
// route to based on the gameState
// @link{keyPressedGeneralVar1} 
// The varibles @inline{gameState}, @inline{mStartScreen},
// @inline{mLobbyScreen} are all defined in sketch.js | 
// @param void : takes no parameters |
// @returns void : returns nothing |
// @header mouseClicked() |
function keyPressed() {
  // #code keyPressedGeneralVar1 javascript
  switch (gameState) {
    case 0:
      mStartScreen.keyPressedStart();
      break;
    case 1:
      mLobbyScreen.keyPressedLobby();
      break;
    case 2:
      mGameScreen.keyPressedGame();
      break;
    case 3:
      // mScoreScreen.keyPressedScore();
      break;
  }
  // |
}