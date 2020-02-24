class LobbyScreen {
    constructor(player) {
        console.log("LobbyScreen made: ");
        console.log(player)
        this.player = player;
        this.team = new Team();
        this.team.addPlayer(this.player);
        this.lobbyGameState = 0;
        this.playerCards = [];
        this.playerCards.push(new PlayerCard(this.player, windowWidth/2, (windowHeight/2 + windowHeight/10), 1, windowHeight/60));
        /** @todo. Make the L in lobby fall with this thing! */
        // this.titleAnimation = [300, 500, 400, 700] //drops the peices 
        if(this.player.owner == true) { // If the player is the owner, we need to ask for the token
            var data = JSON.stringify({"maxPlayers":"4","name": "bob","playerID": "1"})
            // console.log(JSON.stringify({"type": "1", "data": data}));
            socket.send(JSON.stringify({"type": "1", "data": data}));
            socket.onmessage =  (event) => {
                // console.log(event);
                var e = JSON.parse(event.data);
                if(e.lobbyID !== undefined) { 
                    this.team.lobbyToken = e.lobbyID;
                } else {
                    this.team.lobbyToken = "oh shit";
                }
                // console.log("this.team.lobbyToken: " + this.team.lobbyToken);
            };
        }
    }
 
    draw() {
        this.drawTitle();
        this.drawToken();
        if (this.lobbyGameState == 0) {
            teamNameAsker(this.team);
        } else {
            this.playerCards.forEach(playerCard => {
                playerCard.draw();
            });
        }
    }

    /**
     * drawToken: This function will draw the token for the owner and all the 
     *            players.
     * 
     * @param void
     * 
     * @returns void
     * 
     */
    drawToken() {
        push(); // push the settings
        fill(255); // fill white
        textSize(windowWidth/30) // text size relative to screen width
        text("Token: " + this.team.lobbyToken,windowWidth/10,windowHeight/1.1) // draw the token
        pop(); // pop the settings
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
        text("obby", windowWidth / 18, obbyPosY = windowHeight / 7.2) // writing obby onto the screen
        fill(255, 0, 0); // fill first three squares with red
        rect(-windowWidth / 19 - spaceBetweenSquares, windowHeight / 6, squareSize, squareSize)
        rect(-windowWidth / 19 - spaceBetweenSquares, windowHeight / 6 - spaceBetweenSquares, squareSize, squareSize)
        rect(-windowWidth / 19 - spaceBetweenSquares, windowHeight / 6 - (2 * spaceBetweenSquares), squareSize, squareSize)
        fill(0, 0, 255); // fill last rect with blue
        rect(-windowWidth / 19, windowHeight / 6, squareSize, squareSize)
        /* Some scaling ideas, done delete steve! (or anyone else) */
        // stroke("white")
        // line(-windowWidth, windowHeight/6 + squareSize/2, windowWidth, windowHeight/6 + squareSize/2);
        // stroke("yellow")
        // line(-windowWidth, obbyPosY + textsizee / 2, windowWidth, obbyPosY + textsizee / 2);
        pop();
    }

    newPlayerJoins() {
        console.log("New player joined!")
        // add new player to playercard and team
    }

    PlayerLeaves() {
        console.log("Player leaves!")
    }

    keyPressedLobby() {
        console.log("KeyCode: " + keyCode)
        switch (this.lobbyGameState) {
            case 0: // If we are on the username text box
                if (keyCode == 8) { // "pressed delete"
                    /* Remove the last char from the username field */
                    this.team.teamName = this.team.teamName.substring(0, this.team.teamName.length - 1);
                } else if (this.team.teamName.length < 11) { // make sure the length of the string doesnt get too long
                    if ((keyCode >= 65 && keyCode <= 90) || keyCode == 32) { // checks to see if its a letter or a space
                        this.team.teamName += String.fromCharCode(keyCode); // add the pressed key to the username
                    }
                }
                break; // run.
        }
    }

    mouseClickedLobby() {
        switch (this.lobbyGameState) {
            case 0: //if owner is entering username
                if(checkMouseTeamAccept()) {
                    this.lobbyGameState = 1;
                }
                break;
        }
    }
}