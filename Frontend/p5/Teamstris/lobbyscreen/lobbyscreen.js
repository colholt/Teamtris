class LobbyScreen {
    constructor() {
        console.log("LobbyScreen made");
        this.titleAnimation = [300, 500, 400, 700] //drops the peices 
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
       let squareSize = windowWidth / 28; // The size of all the squares making up the T's
       let spaceBetweenSquares = windowWidth / 19;
       translate(windowWidth / 2, 0); //Translate to the top middle of the screen
       fill(255, 0, 0); // Fill red
       textSize(windowHeight / 6); // Set text size relative to windowWidth
       text("obby", windowWidth/18, windowHeight / 9)
       pop(); // reset settings
   }

    newPlayerJoins() {

    }

    keyPressedLobby() {

    }

    mouseClickedLobby() {
        
    }
}