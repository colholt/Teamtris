class LobbyScreen {
    constructor(player) {
        console.log("LobbyScreen made: ");
        console.log(player)
        this.player = player;
        /** @todo. Make the L in lobby fall with this thing! */
        // this.titleAnimation = [300, 500, 400, 700] //drops the peices 
        
    }

    draw() {
        this.drawTitle()
    }

    /**
    * drawTitle: This function will draw the title (Lobby) onto the 
    *            Lobby screen.
    * 
    * @param void
    * 
    * @returns void
    * 
    */
   drawTitle() {
        push(); // Push my settings
        let squareSize = windowWidth / 40; // The size of all the squares making up the T's
        let spaceBetweenSquares = windowWidth / 25;
        translate(windowWidth / 2, 0); //Translate to the top middle of the screen
        fill(255, 0, 0); // Fill red
        textAlign(CENTER, CENTER);
        let textsizee; // TEMP
        textSize(textsizee = windowWidth / 12); // Set text size relative to windowWidth
        let obbyPosY; // TEMP
        text("obby", windowWidth/18, obbyPosY = windowHeight / 7.2) // writing obby onto the screen
        fill(255,0,0); // fill first three squares with red
        rect(-windowWidth/19 - spaceBetweenSquares, windowHeight/6 ,squareSize, squareSize)
        rect(-windowWidth/19 - spaceBetweenSquares, windowHeight/6 - spaceBetweenSquares,squareSize, squareSize)
        rect(-windowWidth/19 - spaceBetweenSquares, windowHeight/6 - (2*spaceBetweenSquares),squareSize, squareSize)
        fill(0,0,255); // fill last rect with blue
        rect(-windowWidth/19, windowHeight/6 ,squareSize, squareSize)
        /* Some scaling ideas, done delete steve! (or anyone else) */
        // stroke("white")
        // line(-windowWidth, windowHeight/6 + squareSize/2, windowWidth, windowHeight/6 + squareSize/2);
        // stroke("yellow")
        // line(-windowWidth, obbyPosY + textsizee / 2, windowWidth, obbyPosY + textsizee / 2);
        pop();
   }

    newPlayerJoins() {
        console.log("New player joined!")
    }

    keyPressedLobby() {
        console.log("KeyCode: " + keyCode)
    }

    mouseClickedLobby() {
        console.log("mouseclicked")
    }
}