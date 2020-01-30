function keyPressed() {
  switch (gameState) {
    case 0:
      mStartScreen.keyPressedStart();
      break;
    case 1:
      // mLobbyScreen.keyPressedLobby();
      break;
    case 2:
      // mGameScreen.keyPressedGame();
      break;
    case 3:
      // mScoreScreen.keyPressedScore();
      break;
  }
}