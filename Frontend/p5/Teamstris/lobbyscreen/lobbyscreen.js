class LobbyScreen {
    constructor(player) {
        this.player = player;
        this.team = new Team();
        this.team.addPlayer(this.player);
        this.lobbyGameState = 0;
        this.botNames = ["Arnold", "Steve", "John"];
        this.playerCards = [];
        this.playerCards.push(new PlayerCard(this.player, windowWidth/2, (windowHeight/2 + windowHeight/10), 1, windowHeight/60));
        /** @todo. Make the L in lobby fall with this thing! */
        // this.titleAnimation = [300, 500, 400, 700] //drops the peices 
        if(this.player.owner == true) { // If the player is the owner, we need to ask for the token
            var data = JSON.stringify({"maxPlayers":"4","name": this.player.username,"playerID": this.player.id})
            // console.log(JSON.stringify({"type": "1", "data": data}));
            socket.send(JSON.stringify({"type": "1", "data": data}));
            /* 							X, Y 				 , W  			  , H 				 , gamestate, default color	*/
            buttonList.push(new Buttons(windowWidth/2.5, windowHeight / 3.1, windowWidth / 7, windowHeight / 12, 1, "red"));
            buttonList[buttonList.length - 1].text = "Add bot"; // Text to put in the button
            buttonList[buttonList.length - 1].hoverColor = "yellow"; // What color to make the button on mouse hover
            buttonList[buttonList.length - 1].id = "addbot"; // ID of the button

            buttonList.push(new Buttons(windowWidth/2.5, windowHeight / 2.4, windowWidth / 7, windowHeight / 12, 1, "blue"));
            buttonList[buttonList.length - 1].text = "Remove bot"; // Text to put in the button
            buttonList[buttonList.length - 1].hoverColor = "yellow"; // What color to make the button on mouse hover
            buttonList[buttonList.length - 1].id = "removebot"; // ID of the button

            buttonList.push(new Buttons(windowWidth/2.5, windowHeight / 4.4, windowWidth / 7, windowHeight / 12, 1, "green"));
            buttonList[buttonList.length - 1].text = "Start"; // Text to put in the button
            buttonList[buttonList.length - 1].hoverColor = "yellow"; // What color to make the button on mouse hover
            buttonList[buttonList.length - 1].id = "startgame"; // ID of the button
        }

        /* Going to handle all the connections from the backend */
        socket.onmessage = (event) => {

            var e = JSON.parse(event.data);
            console.log("HERE WE GO ");
            console.log(e);
            if(e.board != undefined ){
                team = this.team;
                player = this.player;
                mGameScreen.SetupSocket();
                gameState = 2;
                return;
            }
            if(e.players != undefined) {
                console.log("players:");
                console.log(e.players[e.players.length-1].name);
                var newPlayer = new Player();
            } else if(e.lobbyID !== undefined) { 
                this.team.lobbyToken = e.lobbyID.toUpperCase();
            }
        };
    }
 
    /**
     * draw: This function will be called 60 times a second by sketch.js
     * 
     * @param void
     * 
     * @returns void
     * 
     */
    draw() {
        this.drawTitle();
        this.drawToken();
        if (this.lobbyGameState == 0 && this.player.owner) {
            teamNameAsker(this.team);
        } else {
            this.playerCards.forEach(playerCard => {
                playerCard.draw();
                if(playerCard.player != this.player) { // checking to see if this playerCard is me
                    playerCard.drawUsername();
                }
            });
            Buttonloop();
        }
    }

    /**
     * addAndRemoveBotButton: This function will be called when a person 
     *                        clicks add or remove bot in the lobby screen.
     * 
     * @param addOrRemove - This param can either be the following
     *                      "addbot": Clicked add bot button
     *                      "removebot": Clicked remove bot button
     * @returns void
     * 
     */
    addAndRemoveBotButton(addOrRemove) {
        if( addOrRemove == "addbot" ) { // For add bot button
            var data = JSON.stringify({"action": 1, "lobbyid":this.team.lobbyToken.toLowerCase()})
            socket.send(JSON.stringify({"type": "7", "data": data}));
            /* Make sure that we are not going over 4 players + bots */
            this.newPlayerJoins(new Player(this.botNames[this.team.playersInTeam.length - 1], -1, false));
        } else if( addOrRemove == "removebot" ) { // for remove bot button
            var data = JSON.stringify({"action": 0, "lobbyid":this.team.lobbyToken.toLowerCase()})
            socket.send(JSON.stringify({"type": "7", "data": data}));
            for( var i = this.team.playersInTeam.length-1; i > 0; i-- ) {
                var player = this.team.playersInTeam[i];
                if(player.id == -1) {
                    this.playerLeaves(player);
                    break;
                }
            }
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
        // if(this.team.lobbyToken.length == 4) {
            text("Token: " + this.team.lobbyToken,windowWidth/10,windowHeight/1.1) // draw the token
        // } 
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

    /**
     * newPlayerJoins - this function will be called when a new user joins
     *                  the lobby.
     * 
     * @param player 
     * 
     * @returns true: if new player was added
     *          false: if new player was denied from lobby (Lobby is full)
     * 
     */
    newPlayerJoins(player) {
        console.log("New player joined! " + this.team.playersInTeam.length);
        if((this.team.playersInTeam.length) == 4) return false;
        this.team.playersInTeam.push(player);
        switch(this.team.playersInTeam.length) {
            case 2: 
                this.playerCards.push(new PlayerCard(player, windowWidth/6, (windowHeight/4), .6, windowHeight/60));
                break;
            case 3:
                this.playerCards.push(new PlayerCard(player, windowWidth/6, (windowHeight/1.5), .6, windowHeight/60));
                break;
            case 4:
                this.playerCards.push(new PlayerCard(player, windowWidth/1.2, (windowHeight/2.7), .6, windowHeight/60));
                break;
        }
        // add new player to playercard and team
        return true;
    }

    /**
     * playerLeaves - this function will be called when a user leaves
     *                the lobby.
     * 
     * @param player 
     * 
     * @returns void
     * 
     */
    playerLeaves(player) {
        console.log("Player leaves!");
        console.log(player);
        this.team.playersInTeam.forEach(function(playerInList, index, object) {
            if(playerInList == player) {
                object.splice(index, 1);
            }
        });
        this.playerCards.forEach(function(playercard, index, object) {
            if(playercard.player == player) {
                object.splice(index, 1);
            }
        });
    }

    /**
     * keyPressedLobby: Is called when the user clicks a key
     * 
     * @param void (but really given a keycode)
     * 
     * @returns void
     * 
     */
    keyPressedLobby() {
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

    /**
     * mouseClickedLobby: Is called when the user clicks anywhere on the screen
     * 
     * @param void
     * 
     * @returns void
     * 
     */
    mouseClickedLobby() {
        switch (this.lobbyGameState) {
            case 0: //if owner is entering username
                if(checkMouseTeamAccept()) {
                    this.lobbyGameState = 1;
                }
                break;
            case 1: 
                if(ClickedLoop() == "addbot" || ClickedLoop() == "removebot") {
                    this.addAndRemoveBotButton(ClickedLoop());
                } else if(ClickedLoop() == "startgame") {
                    var data = JSON.stringify({"lobbyid":this.team.lobbyToken.toLowerCase()})
                    socket.send(JSON.stringify({"type": "2", "data": data}));
                    team = this.team;
                    player = this.player;
                    mGameScreen.SetupSocket();
                    gameState = 2;
                }
                break;
        }
    }
}

module.exports = [LobbyScreen];