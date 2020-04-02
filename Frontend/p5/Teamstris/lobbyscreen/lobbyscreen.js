/**
 * #class LobbyScreen |
 * @author Steven Dellamore |
 * @language javascript | 
 * @desc The lobby screen is where players will create or 
 * join lobbys and wait to be put into game. The owner will 
 * be the one that has the start button and will bring
 * everyone into game. |
 */
class LobbyScreen {

    /**
     * #function LobbyScreen::constructor |
     * @author Steven Dellamore |
	 * @desc The Constructor is in chage of
     * createing all the init values for the lobby
     * screen to use and create all the buttons. The owner
     * will be the only ones with the "add bot", "remove bot"
     * , "Start". The lobby screen will also be communicating to 
     * and from the backend, the setup starts in this function.
     * @link{contorLobbyScreenVar1} |
     * @header constructor(player) | 
     * @param Player player : The player that is joining the lobby |
	 * @returns LobbyScreen : An object of Lobby Screen | 
	 */
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
                mGameScreen = new GameScreen(this.team.numPlayers, this.player.id);
                team = this.team;
                player = this.player;
                console.log("ID: " + player.id + " Num players: " + team.numPlayers)
                mGameScreen.SetupSocket();
                gameState = 2;
                return;
            }
            if(e.dataType === 15) {
                if(!player.owner){
                    this.addAndRemoveBotButton("addbot", false);
                }
            } else if(e.dataType === 16){
                if(!player.owner){
                    this.addAndRemoveBotButton("removebot");
                }
            } else if(e.team != undefined ) {
                this.team.teamName = e.team.team;
            } else if(e.players != undefined) {
                if(e.dataType === -1){
                    console.log("YES" + e.players[0].name + ":" + this.player.username);
                    if(e.players[0].name == this.player.username) {
                        console.log("IM THE OWNER FAM");
                        this.player.owner = true;
                        this.changeOwnerToMe();
                    }
                    return;
                } else if(e.dataType === 8){
                    console.log("players:");
                    this.team.playersInTeam = [];
                    this.playerCards = [];
                    for(var i = 0; i < e.players.length; i++) {
                        var ownerr = false;
                        if(e.players.id == 1) {ownerr = true}
                        var newPlayer = new Player(e.players[i].name, e.players[i].id, ownerr);
                        if(e.players[i].name == this.player.username) {
                            this.team.playersInTeam.push(newPlayer)
                            this.playerCards.push(new PlayerCard(this.player, windowWidth/2, (windowHeight/2 + windowHeight/10), 1, windowHeight/60));
                        }
                    }
                    for(var i = 0; i < e.players.length; i++){
                        var ownerr = false;
                        if(e.players.id == 1) {ownerr = true}
                        var newPlayer = new Player(e.players[i].name, e.players[i].id, ownerr);
                        if(e.players[i].name == this.player.username){
                            console.log("My name is " + e.players[i].name)
                            this.player.id = e.players[i].id
                            this.team.numPlayers = e.players.length
                            // this.team.playersInTeam.push(newPlayer)
                        } else {
                            console.log("his name is " + e.players[i].name)
                            this.newPlayerJoins(newPlayer)
                        }
                    }
                }
            } else if(e.lobbyID !== undefined) { 
                this.team.lobbyToken = e.lobbyID.toUpperCase();
            }
        };
    }

    /**
     * #function LobbyScreen::changeOwnerToMe |
     * @author Steven Dellamore |
	 * @desc When the owner leaves the backend will send
     * a notification with the new owner. If the new owner
     * is this current player, then we will have to start
     * drawing the owner buttons on the screen so they 
     * can start the game. We use the @inline{Buttons()} class
     * to aid us in this process. |
     * @header changeOwnerToMe() | 
	 */
    changeOwnerToMe() {
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
 
    /**
     * #function LobbyScreen::draw |
     * @author Steven Dellamore |
	 * @desc This will be called 60 times a second when
     * @inline{gameState == 1}. @inline{"sketch.js"} is where 
     * it takes care of the routing for this. |
     * @header draw() | 
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
     * #function LobbyScreen::addAndRemoveBotButton |
     * @author Steven Dellamore |
	 * @desc Will be called when the user clicks on either the "Add bot"
     * or "Remove bot" button. @inline{mouseClickedLobby()} is who calls this 
     * function. This function will form and send a response based on which button you 
     * clicked. \\Add Bot: 
     * @link{addBotVar1} 
     * Remove bot:
     * @link{removeBotVar1} |
     * @param addOrRemove : This param can either be the following \\
     * "addbot": Clicked add bot button. \\
     * "removebot": Clicked remove bot button |
     * @header addAndRemoveBotButton(addOrRemove) | 
	 */
    addAndRemoveBotButton(addOrRemove, t = true) {
        if( addOrRemove == "addbot" ) { // For add bot button
            // #code addBotVar1 javascript 
            if(t){
                var data = JSON.stringify({"action": 1, "lobbyid":this.team.lobbyToken.toLowerCase()})
                socket.send(JSON.stringify({"type": "7", "data": data}));
            }
            // |
            /* Make sure that we are not going over 4 players + bots */
            this.newPlayerJoins(new Player(
                this.botNames[this.team.playersInTeam.length - 1], -1, false));
        
        
        } else if( addOrRemove == "removebot" ) { // for remove bot button
            for( var i = this.team.playersInTeam.length-1; i > 0; i-- ) {
                var player = this.team.playersInTeam[i];
                if(player.id == -1) {
                    if(t){
                    // #code removeBotVar1 javascript 
                        var data = JSON.stringify({"action": 0, "lobbyid":this.team.lobbyToken.toLowerCase()})
                        socket.send(JSON.stringify({"type": "7", "data": data}));
                    }
                    // |
                    this.playerLeaves(player);
                    break;
                }
            }
        }
       
    }

    /**
     * #function LobbyScreen::drawToken |
     * @author Steven Dellamore |
	 * @desc draws the token onto the screen for everyone
     * in the lobby
     * @link{drawToken1} |
     * @header drawToken() | 
	 */
    drawToken() {
        push(); // push the settings
        fill(255); // fill white
        textSize(windowWidth/30) // text size relative to screen width
        // if(this.team.lobbyToken.length == 4) {
        // #code drawToken1 javascript
        // draw the token with p5 text() function
        text("Token: " + this.team.lobbyToken,windowWidth/10,windowHeight/1.1) 
        // |
        // } 
        pop(); // pop the settings
    }

    /**
     * #function LobbyScreen::drawTitle |
     * @author Steven Dellamore |
	 * @desc draws the title (@inline{"lobby"}) onto the screen for all
     * players in the lobby. |
     * @header drawTitle() | 
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
     * #function LobbyScreen::newPlayerJoins |
     * @author Steven Dellamore |
	 * @desc Will check if a new player can be added to the lobby. 
     * If they can, a new player card will be made for the new user.
     * @link{playerCardExp} Otherwise the function will return false 
     * and nothing will be changed. |
     * @header newPlayerJoins(player) | 
     * @param Player player : The player trying to join the lobby |
     * @returns boolean : If the new player is added. \\
     * true: if new player was added. \\
     * false: if new player was denied from lobby (Lobby is full). |
	 */
    newPlayerJoins(player) {
        console.log("New player joined! " + this.team.playersInTeam.length);
        if((this.team.playersInTeam.length) == 4) return false;
        this.team.playersInTeam.push(player);
        switch(this.team.playersInTeam.length) {
            case 2: 
                // #code playerCardExp javascript
                // add a new playercard to the playerCard array
                this.playerCards.push(
                    new PlayerCard(
                        player, windowWidth/6, (windowHeight/4), .6, windowHeight/60));
                // |
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
     * #function LobbyScreen::playerLeaves |
     * @author Steven Dellamore |
	 * @desc Will eject a player from the team's list and 
     * allow another spot for a new player to join. @link{playerLeavesExp}
     * This shows how we go through the array and splice out the player
     * that has left. |
     * @header playerLeaves(player) | 
     * @param Player player : The player trying to leave the lobby |
	 */
    playerLeaves(player) {
        console.log("Player leaves!");
        console.log(player);
        // #code playerLeavesExp javascript
        this.team.playersInTeam.forEach(function(playerInList, index, object) {
            if(playerInList == player) {
                object.splice(index, 1);
            }
        });
        // |
        this.playerCards.forEach(function(playercard, index, object) {
            if(playercard.player == player) {
                object.splice(index, 1);
            }
        });
    }

    /**
     * #function LobbyScreen::keyPressedLobby |
     * @author Steven Dellamore |
	 * @desc This function will handle all key presses by the user.
     * Since we only have one place to press a key, the team box, when
     * @inline{this.lobbyGameState} is set to @inline{0}, we will append
     * to the teamname. The teamname cannot go above 11 chars or below 0. |
     * @header keyPressedLobby() | 
	 */
    keyPressedLobby() {
        switch (this.lobbyGameState) {
            case 0: 
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
     * #function LobbyScreen::mouseClickedLobby |
     * @author Steven Dellamore |
	 * @desc Handles all the mouse clicks when the user clicks
     * on the lobby screen. @link{keypressLobbyExp}
     * @inline{"mouseClicked.js"} is responsible for routing 
     * clicks to the correct screen. |
     * @header mouseClickedLobby() | 
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
                    if(this.team.numPlayers == 1){
                        this.player.id = 1;
                    }
                    mGameScreen = new GameScreen(this.team.numPlayers, this.player.id);
                    team = this.team;
                    player = this.player;
                    mGameScreen.SetupSocket();
                    console.log("ID: " + player.id + " Num players: " + team.numPlayers)
                    socket.send(JSON.stringify({"type": "14", "team": this.team.teamName, "lobbyid":this.team.lobbyToken.toLowerCase()}));
                    gameState = 2;
                }
                break;
        }
    }
}

module.exports = [LobbyScreen];